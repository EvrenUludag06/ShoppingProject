using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.Business.Dtos;
using Shopping.Business.Services;
using Shopping.WebUI.Areas.Admin.Models;
using System.Data;

namespace Shopping.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class CategoryController : Controller
	{
		private readonly ICategoryService _categoryService;
		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}
		public IActionResult List()
		{
			var listCategoryDtos = _categoryService.GetCategories();

			var viewModel = listCategoryDtos.Select(x => new CategoryListViewModel
			{
				Id = x.Id,
				Name = x.Name
			}).ToList();

			return View(viewModel);
		}

		[HttpGet]
		public IActionResult New()
		{
			return View("Form", new CategoryFormViewModel());
		}

		[HttpGet]
		public IActionResult Update(int id)
		{
			var updateCategoryDto = _categoryService.GetCategory(id);

			var viewModel = new CategoryFormViewModel()
			{
				Id = updateCategoryDto.Id,
				Name = updateCategoryDto.Name,
				Description = updateCategoryDto.Description
			};

			return View("form", viewModel);
		}

		[HttpPost]
		public IActionResult Save(CategoryFormViewModel formData)
		{
			if (!ModelState.IsValid)
			{
				return View("form", formData);
			}

			if (formData.Id == 0)
			{
				var addCategoryDto = new AddCategoryDto()
				{
					Name = formData.Name,
					Description = formData.Description
				};

				var result = _categoryService.AddCategory(addCategoryDto);

				if (result)
				{
					return RedirectToAction("List");
				}
				else
				{
					ViewBag.ErrorMessage = "Bu isimde bir kategori zaten mevcut.";
					return View("Form", formData);
				}
			}

			else
			{
				var updateCategoryDto = new UpdateCategoryDto()
				{
					Id = formData.Id,
					Name = formData.Name,
					Description = formData.Description
				};

				_categoryService.UpdateCategory(updateCategoryDto);


				return RedirectToAction("List");
			}

		}
		public IActionResult Delete(int id)
		{
			_categoryService.DeleteCategory(id);
			return RedirectToAction("List");
		}
	}
}
