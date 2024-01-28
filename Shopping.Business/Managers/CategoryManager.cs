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
	public class CategoryManager : ICategoryService
	{
		private readonly IRepository<CategoryEntity> _categoryRepository;
		public CategoryManager(IRepository<CategoryEntity> categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}
		public bool AddCategory(AddCategoryDto addCategoryDto)
		{
			var hasCategory = _categoryRepository.GetAll(x => x.Name.ToLower() == addCategoryDto.Name.ToLower()).ToList();

			if (hasCategory.Any())
			{
				return false;
			}

			var categoryEntity = new CategoryEntity()
			{
				Name = addCategoryDto.Name,
				Description = addCategoryDto.Description
			};

			_categoryRepository.Add(categoryEntity);

			return true;
		}
		public void DeleteCategory(int id)
		{
			_categoryRepository.Delete(id);
		}
		public List<ListCategoryDto> GetCategories()
		{
			var categoryEntities = _categoryRepository.GetAll().OrderBy(x => x.Name);

			var categoryDtoList = categoryEntities.Select(x => new ListCategoryDto
			{
				Id = x.Id,
				Name = x.Name,
				Description = x.Description
			}).ToList();

			return categoryDtoList;
		}
		public UpdateCategoryDto GetCategory(int id)
		{
			var categoryEntity = _categoryRepository.GetById(id);

			var updateCategoryDto = new UpdateCategoryDto()
			{
				Id = categoryEntity.Id,
				Name = categoryEntity.Name,
				Description = categoryEntity.Description
			};

			return updateCategoryDto;
		}
		public void UpdateCategory(UpdateCategoryDto updateCategoryDto)
		{
			var categoryEntity = _categoryRepository.GetById(updateCategoryDto.Id);

			categoryEntity.Name = updateCategoryDto.Name;
			categoryEntity.Description = updateCategoryDto.Description;

			_categoryRepository.Update(categoryEntity);
		}
	}
}
