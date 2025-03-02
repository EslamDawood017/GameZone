using GameZone.Attributes;
using System.Runtime.InteropServices;

namespace GameZone.ViewModel
{
	public class EditeGameViewModel : GameFormViewModel
	{
		public int Id { get; set; }
        
		[MaxSize(FileSettings.MaxFileSizeInbytes)]
		[AllowedExtention(FileSettings.Extentions)]
		public IFormFile? Cover { get; set; } = default!;
		public string? CoverPath {  get; set; }

	}
}
