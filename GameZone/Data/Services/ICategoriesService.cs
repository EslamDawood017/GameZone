using GameZone.Models;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Data.Services
{
	public interface ICategoriesService
	{
		public void Add(Category entity);


		public void Delete(int id);


		public IEnumerable<Category> GetAll();


		public Category GetById(int id);


		public void Update(Category NewCategory);

		public IEnumerable<SelectListItem> GetAllCategoriesAsSelectListItem();
		
	}
}
