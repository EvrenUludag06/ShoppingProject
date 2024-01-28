using Microsoft.AspNetCore.Mvc;

namespace Shopping.WebUI.Controllers
{
	public class ProductController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
