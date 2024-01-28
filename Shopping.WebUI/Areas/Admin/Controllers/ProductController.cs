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
	public class ProductController : Controller
	{
		private readonly ICategoryService _categoryService;
		private readonly IWebHostEnvironment _environment;
		private readonly IProductService _productService;
		public ProductController(ICategoryService categoryService, IWebHostEnvironment environment, IProductService productService)
		{
			_categoryService = categoryService;
			_environment = environment;
			_productService = productService;
		}
		public IActionResult List()
		{
			var productDtos = _productService.GetProducts();

			var viewModel = productDtos.Select(x => new ListProductViewModel
			{
				Id = x.Id,
				Name = x.Name,
				CategoryId = x.CategoryId,
				CategoryName = x.CategoryName,
				UnitInStock = x.UnitInStock,
				UnitPrice = x.UnitPrice,
				ImagePath = x.ImagePath
			}).ToList();

			return View(viewModel);
		}

		[HttpGet]
		public IActionResult New()
		{
			ViewBag.Categories = _categoryService.GetCategories();
			return View("Form", new ProductFormViewModel());
		}

		[HttpGet]
		public IActionResult Edit(int id)
		{
			var editProductDto = _productService.GetProductById(id);

			var viewModel = new ProductFormViewModel()
			{
				Id = editProductDto.Id,
				Name = editProductDto.Name,
				Description = editProductDto.Description,
				UnitInStock = editProductDto.UnitInStock,
				UnitPrice = editProductDto.UnitPrice,
				CategoryId = editProductDto.CategoryId
			};

			ViewBag.ImagePath = editProductDto.ImagePath;
			ViewBag.Categories = _categoryService.GetCategories();
			return View("form", viewModel);
		}

		[HttpPost]
		public IActionResult Save(ProductFormViewModel formData)
		{
			if (!ModelState.IsValid)
			{
				if (formData.CategoryId == 0)
				{
					ViewBag.CatError = "Kategori Seçilmek zorunda.";
				}
				ViewBag.Categories = _categoryService.GetCategories();
				return View("Form", formData);
			}

			var newFileName = "";

			if (formData.File is not null)
			{
				var allowedFileTypes = new string[] { "image/jpeg", "image/jpg", "image/png", "image/jfif", "image/webp" };
				var allowedFileExtensions = new string[] { ".jpg", ".jpeg", ".png", ".jfif", ".webp" };
				var fileContentType = formData.File.ContentType;
				var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(formData.File.FileName);
				var fileExtension = Path.GetExtension(formData.File.FileName);

				if (!allowedFileTypes.Contains(fileContentType) ||
					!allowedFileExtensions.Contains(fileExtension))
				{
					ViewBag.FileError = "Dosya formatı veya içeriği hatalı";

					ViewBag.Categories = _categoryService.GetCategories();
					return View("Form", formData);
				}

				newFileName = fileNameWithoutExtension + "-" + Guid.NewGuid() + fileExtension;

				var folderPath = Path.Combine("images", "products");

				var wwwrootFolderPath = Path.Combine(_environment.WebRootPath, folderPath);

				var wwwrootFilePath = Path.Combine(wwwrootFolderPath, newFileName);


				Directory.CreateDirectory(wwwrootFolderPath);

				using (var fileStream = new FileStream(wwwrootFilePath, FileMode.Create))
				{
					formData.File.CopyTo(fileStream);
				}
			}

			if (formData.Id == 0)
			{
				var addProductDto = new AddProductDto()
				{
					Name = formData.Name,
					Description = formData.Description,
					UnitPrice = formData.UnitPrice,
					UnitInStock = formData.UnitInStock,
					CategoryId = formData.CategoryId,
					ImagePath = newFileName
				};

				_productService.AddProduct(addProductDto);
				return RedirectToAction("list");
			}

			else
			{
				var editProductDto = new EditProductDto()
				{
					Id = formData.Id,
					Name = formData.Name,
					Description = formData.Description,
					UnitInStock = formData.UnitInStock,
					UnitPrice = formData.UnitPrice,
					CategoryId = formData.CategoryId
				};

				if (formData.File is not null)
				{
					editProductDto.ImagePath = newFileName;
				}

				_productService.EditProduct(editProductDto);
				return RedirectToAction("List");
			}
		}
		public IActionResult Delete(int id)
		{
			_productService.DeleteProduct(id);

			return RedirectToAction("list");
		}
	}
}