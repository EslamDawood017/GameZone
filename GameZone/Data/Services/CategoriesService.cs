using GameZone.Models;

namespace GameZone.Data.Services
{
	public class CategoriesService : ICategoriesService
	{
		private readonly AppDbContext _context ;

		public CategoriesService(AppDbContext  context)
        {
            _context = context;
        }
        public void Add(Category entity)
		{
			_context.Categories.Add(entity);
			_context.SaveChanges();
		}

		public void Delete(int id)
		{
			var removedCategory = _context.Categories.FirstOrDefault(c => c.Id == id);
			if(removedCategory != null)
			{
				_context.Categories.Remove(removedCategory);
				_context.SaveChanges();
			}
		}

		public IEnumerable<Category> GetAll()
		{
			return _context.Categories.ToList();
		}

		public Category GetById(int id)
		{
			return _context.Categories.FirstOrDefault(g => g.Id == id);
		}

		public void Update(Category NewCategory)
		{
			var oldCat = GetById(NewCategory.Id);

			oldCat.Name = NewCategory.Name;

			_context.SaveChanges();

		}
		public IEnumerable<SelectListItem> GetAllCategoriesAsSelectListItem()
		{
			return _context.Categories
				.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
				.OrderBy(c => c.Text)
				.AsNoTracking();
		}
	}
}
