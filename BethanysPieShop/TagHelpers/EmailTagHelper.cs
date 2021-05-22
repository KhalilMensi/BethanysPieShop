using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.TagHelpers
{
	public class EmailTagHelper	: TagHelper
	{
		private string Address { get; set; }
		private string Content { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = "a";
			output.Attributes.SetAttribute("href", "mailto:" + Address);
			output.Content.SetContent(Content);
		}
	}
}
