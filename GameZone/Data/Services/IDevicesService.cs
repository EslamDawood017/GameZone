using GameZone.Models;
using Microsoft.EntityFrameworkCore;

namespace GameZone.Data.Services
{
	public interface IDevicesService
	{
		public void Add(Device entity);
		public void Delete(int id);
		public IEnumerable<Device> GetAll();
		public Device GetById(int id);
		public void Update(Device newDevice);
		public IEnumerable<SelectListItem> GetAllDevicesAsSelectListItem();
		
	}
}
