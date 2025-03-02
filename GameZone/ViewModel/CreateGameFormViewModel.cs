using GameZone.Attributes;
using GameZone.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GameZone.ViewModel
{
	public class CreateGameFormViewModel : GameFormViewModel
	{
		[Required]
		[MaxSize(FileSettings.MaxFileSizeInbytes)]
		[AllowedExtention(FileSettings.Extentions)]
		public IFormFile Cover { get; set; } = default!;
	}
}
