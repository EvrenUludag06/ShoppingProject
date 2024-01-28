using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Business.Dtos
{
	public class ListProductDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public decimal? UnitPrice { get; set; }
		public int UnitInStock { get; set; }
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
		public string ImagePath { get; set; }
	}
}
