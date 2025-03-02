

using GameZone.Models;
using Microsoft.AspNetCore.Authorization;

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        private readonly ICategoriesService _CategoriesRepository;
        private readonly IDevicesService _devicesRepository;
        private readonly IGameService _gameService;

		

		public GamesController(AppDbContext context , ICategoriesService repository, IDevicesService devicesRepository,
            IGameService gameService)
		{
			
			_CategoriesRepository = repository;
			_devicesRepository = devicesRepository;
			_gameService = gameService;
		}
        public IActionResult Index()
        {
            var games = _gameService.GetAll();
            return View(games);
        }
        [HttpGet]
        
        public IActionResult Create()
        {
            
            CreateGameFormViewModel viewModel = new CreateGameFormViewModel();

            viewModel.Categories = _CategoriesRepository.GetAllCategoriesAsSelectListItem();


			viewModel.Devices = _devicesRepository.GetAllDevicesAsSelectListItem();

			return View(viewModel);
        }
		[HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(CreateGameFormViewModel viewModel)
        {
            if(!ModelState.IsValid) 
            {
                viewModel.Categories = _CategoriesRepository.GetAllCategoriesAsSelectListItem();

				viewModel.Devices = _devicesRepository.GetAllDevicesAsSelectListItem();

				return View(viewModel);
            }
            else
            {
                await _gameService.Create(viewModel);
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id)
        {
            var game = _gameService.GetById(id);

            if (game is null)
                return NotFound();

            return View(game);

        }
        [HttpGet]
        public IActionResult Update(int id)
        {
            var game = _gameService.GetById(id);

			if (game is null)
				return NotFound();
			EditeGameViewModel ViewModel = new EditeGameViewModel();

            ViewModel.Id = id;
            ViewModel.Name = game.Name;
            ViewModel.Description = game.Description;
            ViewModel.SelectedDevices = game.Devices.Select(d => d.DeviceId).ToList();
            ViewModel.CategoryId = game.CategoryId;
            ViewModel.Categories = _CategoriesRepository.GetAllCategoriesAsSelectListItem();
            ViewModel.Devices = _devicesRepository.GetAllDevicesAsSelectListItem();
            ViewModel.CoverPath = game.Cover;

			return View(ViewModel);
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(EditeGameViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _CategoriesRepository.GetAllCategoriesAsSelectListItem();

                viewModel.Devices = _devicesRepository.GetAllDevicesAsSelectListItem();

                return View(viewModel);
            }  
            
            var game = await _gameService.Update(viewModel);

            if (game is null)
                return BadRequest();
            
            return RedirectToAction(nameof(Index));
        }
       
        public IActionResult Delete(int id)
        {
            var isDeleted = _gameService.Delete(id);

            return isDeleted ? RedirectToAction("Index") : BadRequest();
            
        }
        

    }
}
