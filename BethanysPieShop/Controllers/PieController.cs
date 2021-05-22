namespace BethanysPieShop.Controllers
{
	using BethanysPieShop.Models;
	using BethanysPieShop.ViewModels;
	using Microsoft.AspNetCore.Mvc;
	using System.Collections.Generic;
	using System.Linq;

	public class PieController : Controller
	{
		private readonly IPieRepository _pieRepository;

		private readonly ICategoryRepository _categoryRepository;

		public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
		{
			_pieRepository = pieRepository;
			_categoryRepository = categoryRepository;
		}

		public IActionResult List(string category)
		{
			IEnumerable<Pie> pies;
			string carrentCategory;

			if(string.IsNullOrEmpty(category))
			{
				pies = _pieRepository.AllPies.OrderBy(p => p.PieId);
				carrentCategory = "All pies";
			}else
			{
				pies = _pieRepository.AllPies.Where(p => p.Category.CategoryName == category).OrderBy(p => p.PieId);
				carrentCategory = _categoryRepository.AllCategories.FirstOrDefault(c => c.CategoryName == category )?.CategoryName;
			}
			return View(new PiesListViewModel
			{
				Pies = pies,
				CurrentCategory = category
			});

			PiesListViewModel piesListViewModel = new PiesListViewModel();
			piesListViewModel.Pies = _pieRepository.AllPies;

			piesListViewModel.CurrentCategory = "Cheese cakes";
			return View(piesListViewModel);
		}

		public IActionResult Detail(int id)
		{
			var pie = _pieRepository.GetPieById(id);
			if (pie == null)
				return NotFound();
			return View(pie);
		}
	}
}
