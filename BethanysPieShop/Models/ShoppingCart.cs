using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Models
{
	public class ShoppingCart
	{
		private readonly AppDbContext _appDbContext;
		public string ShoppingCartId { get; set; }
		public List<ShoppingCartItem> ShoppingCartItems { get; set; }
		private ShoppingCart(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}
		public static ShoppingCart GetCart(IServiceProvider services)
		{
			ISession session = services.GetRequiredService<IHttpContextAccessor>() ?
				.HttpContext.Session;
			var context = services.GetService<AppDbContext>();
			string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
			session.SetString("CartId", cartId);

			return new ShoppingCart(context) { ShoppingCartId = cartId };
		}
		public void AddToCart(Pie pie, int amount)
		{
			var shoppingCartItem = _appDbContext.shoppingCartItems.SingleOrDefault(
				s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);
			if(shoppingCartItem == null)
			{
				shoppingCartItem = new ShoppingCartItem
				{
					ShoppingCartId = ShoppingCartId,
					Pie = pie,
					Amount = 1
				};
				_appDbContext.shoppingCartItems.Add(shoppingCartItem);
			} else
			{
				_appDbContext.shoppingCartItems.Remove(shoppingCartItem);
			}
			_appDbContext.SaveChanges();
		}
		public int RemoveFromCart(Pie pie)
		{
			var shoppingCartItem = _appDbContext.shoppingCartItems.SingleOrDefault(
				 s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId
				);
			var localAmount = 0;
			if(shoppingCartItem != null)
			{
				if(shoppingCartItem.Amount > 1)
				{
					shoppingCartItem.Amount--;
					localAmount = shoppingCartItem.Amount;
				}	else
				{
					_appDbContext.shoppingCartItems.Remove(shoppingCartItem);
				}
			}
			_appDbContext.SaveChanges();
			return localAmount;
		}
		public List<ShoppingCartItem> GetShoppingCartItems()
		{
			return ShoppingCartItems ??
				(ShoppingCartItems =
				_appDbContext.shoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
				.Include(s => s.Pie)
				.ToList());
		}
		public void ClearCart()
		{
			var cartItems = _appDbContext
				.shoppingCartItems
				.Where(cart => cart.ShoppingCartId == ShoppingCartId);
			_appDbContext.shoppingCartItems.RemoveRange(cartItems);
			_appDbContext.SaveChanges();
		}
		public decimal GetShoppingCartTotal()
		{
			var total = _appDbContext.shoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
				.Select(c => c.Pie.Price * c.Amount).Sum();
			return total;
		}
	}
}
