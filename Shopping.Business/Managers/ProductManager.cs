using Shopping.Business.Dtos;
using Shopping.Business.Services;
using Shopping.Data.Entities;
using Shopping.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopping.Business.Managers
{
	public class ProductManager : IProductService
	{
		private readonly IRepository<ProductEntity> _productRepository;
		public ProductManager(IRepository<ProductEntity> productRepository)
		{
			_productRepository = productRepository;
		}
		public void AddProduct(AddProductDto addProductDto)
		{
			var productEntity = new ProductEntity()
			{
				Name = addProductDto.Name,
				Description = addProductDto.Description,
				UnitInStock = addProductDto.UnitInStock,
				UnitPrice = addProductDto.UnitPrice,
				CategoryId = addProductDto.CategoryId,
				ImagePath = addProductDto.ImagePath
			};

			_productRepository.Add(productEntity);
		}
		public void DeleteProduct(int id)
		{
			_productRepository.Delete(id);
		}
		public void EditProduct(EditProductDto editProductDto)
		{
			var productEntity = _productRepository.GetById(editProductDto.Id);

			productEntity.Name = editProductDto.Name;
			productEntity.Description = editProductDto.Description;
			productEntity.UnitPrice = editProductDto.UnitPrice;
			productEntity.UnitInStock = editProductDto.UnitInStock;
			productEntity.CategoryId = editProductDto.CategoryId;

			if (editProductDto.ImagePath is not null)
			{
				productEntity.ImagePath = editProductDto.ImagePath;
			}

			_productRepository.Update(productEntity);
		}
		public EditProductDto GetProductById(int id)
		{
			var productEntity = _productRepository.GetById(id);

			var editProductDto = new EditProductDto()
			{
				Id = productEntity.Id,
				Name = productEntity.Name,
				Description = productEntity.Description,
				UnitInStock = productEntity.UnitInStock,
				UnitPrice = productEntity.UnitPrice,
				CategoryId = productEntity.CategoryId,
				ImagePath = productEntity.ImagePath
			};

			return editProductDto;
		}
		public List<ListProductDto> GetProducts()
		{
			var productEntities = _productRepository.GetAll().OrderBy(x => x.Category.Name).ThenBy(x => x.Name);

			var productDtoList = productEntities.Select(x => new ListProductDto
			{
				Id = x.Id,
				Name = x.Name,
				UnitPrice = x.UnitPrice,
				UnitInStock = x.UnitInStock,
				CategoryId = x.CategoryId,
				CategoryName = x.Category.Name,
				ImagePath = x.ImagePath
			}).ToList();

			return productDtoList;
		}
		public List<ListProductDto> GetProductsByCategoryId(int? categoryId)
		{
			if (categoryId.HasValue)
			{
				var productEntities = _productRepository.GetAll(x => x.CategoryId == categoryId).OrderBy(x => x.Name);

				var productDtos = productEntities.Select(x => new ListProductDto
				{
					Id = x.Id,
					Name = x.Name,
					UnitInStock = x.UnitInStock,
					UnitPrice = x.UnitPrice,
					CategoryId = x.CategoryId,
					CategoryName = x.Category.Name,
					ImagePath = x.ImagePath
				}).ToList();

				return productDtos;
			}

			else
			{
				return GetProducts();
			}
		}
	}
}
