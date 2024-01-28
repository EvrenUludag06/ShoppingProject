using Shopping.Business.Services;
using Shopping.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Shopping.WebUI.ViewComponents
{
	public class ProductsViewComponent : ViewComponent
	{
		private readonly IProductService _productService;

		public ProductsViewComponent(IProductService productService)
		{
			_productService = productService;
		}
		public IViewComponentResult Invoke(int? categoryId = null)
		{
			var productDtos = _productService.GetProductsByCategoryId(categoryId);

			var viewModel = productDtos.Select(x => new ProductViewModel
			{
				Id = x.Id,
				Name = x.Name,
				UnitPrice = x.UnitPrice,
				UnıtInStock = x.UnitInStock,
				CategoryName = x.CategoryName,
				ImagePath = x.ImagePath
			}).ToList();

			return View(viewModel);
		}
	}
}
