namespace GameZone.Data.Services
{
	public interface IGameService
	{
		public Task Create(CreateGameFormViewModel game);
		public IEnumerable<Game> GetAll();
		public Game? GetById(int id);
		public Task<Game?> Update(EditeGameViewModel game);
		public bool Delete(int id);


		
	}
}
