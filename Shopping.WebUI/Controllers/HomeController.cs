using Microsoft.AspNetCore.Mvc;

namespace Shopping.WebUI.Controllers
{
	public class HomeController : Controller
	{
		[Route("/")]
		[Route("urunler/{categoryName}/{categoryId}")]
		public IActionResult Index(int? categoryId)
		{
			ViewBag.CategoryId = categoryId;

			return View();
		}
	}
}
