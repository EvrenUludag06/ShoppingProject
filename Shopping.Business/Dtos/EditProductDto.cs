using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Business.Dtos
{
	public class EditProductDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string? Description { get; set; }
		public decimal? UnitPrice { get; set; }
		public int UnitInStock { get; set; }
		public int CategoryId { get; set; }
		public string ImagePath { get; set; }
	}
}