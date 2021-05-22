using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Models
{
	public class Order
	{
		[BindNever]
		public int OrderId { get; set; }

		public List<OrderDetail> OrderDetails { get; set; }
		[Required(ErrorMessage = "Please enter your first name")]
		[Display(Name = "First name")]
		public string FirstName { get; set; }
		[Required(ErrorMessage = "Please enter your last name")]
		[Display(Name = "Last name")]
		public string LastName { get; set; }
		[Required(ErrorMessage = "Please enter your adress")]
		[StringLength(100)]
		[Display(Name = "Adress Line 1")]
		public string AddressLine1 { get; set; }

		[Display(Name = "Adress Line 2")]
		public string AddressLine2 { get; set; }
		[Required(ErrorMessage = "Please enter your zip code")]
		[Display(Name ="Zip code")]
		[StringLength(10,MinimumLength =4)]
		public string ZipCode { get; set; }
		[Required(ErrorMessage = "Please enter your city")]
		[StringLength(50)]
		public string City { get; set; }
		[StringLength(10)]
		public string State { get; set; }
		[Required(ErrorMessage ="Please enter your country")]
		[StringLength(10)]
		public string Country { get; set; }

		[StringLength(25)]
		[DataType(DataType.PhoneNumber)]
		[Display(Name = "Phone number")]
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public decimal OrderTotal { get; set; }
		public DateTime OrderPlaced { get; set; }
	}
}
