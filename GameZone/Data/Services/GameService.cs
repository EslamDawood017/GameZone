

using GameZone.Models;

namespace GameZone.Data.Services
{
	public class GameService : IGameService
	{
		private readonly AppDbContext _context;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly string _ImagesPath;

		public GameService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
		{
			_context = context;
			_webHostEnvironment = webHostEnvironment;
			_ImagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
		}
		public async Task Create(CreateGameFormViewModel game)
		{
			string CoverName = await SaveCover(game.Cover);

			var path = Path.Combine(_ImagesPath, CoverName);

			using(var stream = File.Create(path))
			{
				await game.Cover.CopyToAsync(stream);
			}

			Game NewGame = new Game();

			NewGame.Name = game.Name;
			NewGame.Description = game.Description;
			NewGame.CategoryId  = game.CategoryId;
			NewGame.Cover = CoverName;
			NewGame.Devices = game.SelectedDevices.Select( d => new GameDevice { DeviceId = d}).ToList();

			_context.Games.Add(NewGame);
			_context.SaveChanges(); 

		}

        

        public IEnumerable<Game> GetAll()
		{
			return _context.Games
				.AsNoTracking()
				.Include(g => g.Category)
				.Include(g => g.Devices)
				.ThenInclude(d => d.Device)
				.ToList();
		}

        public Game? GetById(int id)
        {
            return _context.Games
                .AsNoTracking()
                .Include(g => g.Category)
                .Include(g => g.Devices)
                .ThenInclude(d => d.Device)
                .SingleOrDefault(g => g.Id == id);
        }
        public async Task<Game?> Update(EditeGameViewModel gameModel)
		{
			var game = _context.Games
				.Include(d => d.Devices)
				.FirstOrDefault(g => g.Id == gameModel.Id);
				;

			if(game is null)
				return null;

			var hasNewCover = gameModel.Cover != null;
			var OldCover = game.Cover;

			game.Name = gameModel.Name;
			game.Description = gameModel.Description;
			game.CategoryId = gameModel.CategoryId;
			game.Devices = gameModel.SelectedDevices.Select( d => new GameDevice { DeviceId = d }).ToList();

			if(hasNewCover)
			{
				game.Cover = await SaveCover(gameModel.Cover!);
			}

			var effectedRows = _context.SaveChanges();

			if (effectedRows > 0)
			{
				if(hasNewCover)
				{
					var cover = Path.Combine(_ImagesPath, OldCover);
					File.Delete(cover);
				}
				return game;
			}
			else
			{
                var cover = Path.Combine(_ImagesPath, game.Cover);
                File.Delete(cover);
                return null;
			}
				


		}

		private async Task<string> SaveCover(IFormFile cover)
		{
            string CoverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";

            var path = Path.Combine(_ImagesPath, CoverName);

            using (var stream = File.Create(path))
            {
                await cover.CopyToAsync(stream);
            }
			return CoverName;
        }
        public bool Delete(int id)
        {
			var isDeleted = false;

			var game = _context.Games.Find(id);

			if (game is null)
			{
				return isDeleted;
			}

			_context.Games.Remove(game);
			var effectedRows = _context.SaveChanges();

			if (effectedRows > 0)
			{
				isDeleted = true;

				var cover = Path.Combine(_ImagesPath,game.Cover);
				File.Delete(cover);
			}

			return isDeleted;
        }
    }
}
