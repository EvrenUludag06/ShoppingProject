using Shopping.Business.Services;
using Shopping.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Shopping.WebUI.ViewComponents
{
	public class CategoriesViewComponent : ViewComponent
	{
		private readonly ICategoryService _categoryService;
		public CategoriesViewComponent(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}
		public IViewComponentResult Invoke()
		{
			var categoryDtos = _categoryService.GetCategories();

			var viewModel = categoryDtos.Select(x => new CategoryViewModel
			{
				Id = x.Id,
				Name = x.Name
			}).ToList();

			return View(viewModel);
		}
	}
}