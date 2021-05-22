using BethanysPieShop.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Controllers
{
	public class OrderController : Controller
	{
		private readonly IOrderRepository _orderRepository;
		private readonly ShoppingCart _shoppingCrat;

		public OrderController(IOrderRepository orderRepository, ShoppingCart shoppingCrat)
		{
			_orderRepository = orderRepository;
			_shoppingCrat = shoppingCrat;
		}

		public IActionResult Checkout()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Checkout(Order order)
		{
			var items = _shoppingCrat.GetShoppingCartItems();
			_shoppingCrat.ShoppingCartItems = items;
			if(_shoppingCrat.ShoppingCartItems.Count == 0)
			{
				ModelState.AddModelError("", "Your cart is empty, add some pies first");
			}

			if (ModelState.IsValid)
			{
				_orderRepository.CreateOrder(order);
				_shoppingCrat.ClearCart();
				return RedirectToAction("CheckoutComplete");
			}
			return View(order);
		}
		public IActionResult CheckoutComplete()
		{
			ViewBag.CheckoutCompleteMessage = "Thanks for your order. You'll soon enjoy our deliscious piece of cake";
			return View();
		}
	}
}
