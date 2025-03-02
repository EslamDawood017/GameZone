using GameZone.Models;

namespace GameZone.Data.Services
{
	public class DevicesService : IDevicesService
	{
		private readonly AppDbContext _context;
        public DevicesService(AppDbContext context)
        {
			_context = context;
        }
        public void Add(Device entity)
		{
			_context.Devices.Add(entity);
			_context.SaveChanges();
		}

		public void Delete(int id)
		{
			var delDevice = _context.Devices.FirstOrDefault(d => d.Id == id);
			if (delDevice != null)
			{
				_context.Devices.Remove(delDevice);
				_context.SaveChanges();
			}
		}

		public IEnumerable<Device> GetAll()
		{
			return _context.Devices.ToList();
		}

		public Device GetById(int id)
		{
			return _context.Devices.FirstOrDefault(d => d.Id == id);
		}

		public void Update(Device newDevice)
		{
			var OldDevice = GetById(newDevice.Id);
			OldDevice.Name = newDevice.Name;
			OldDevice.Icon = newDevice.Icon;
			_context.SaveChanges();
		}
		public IEnumerable<SelectListItem> GetAllDevicesAsSelectListItem()
		{
			return _context.Devices
				.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
				.OrderBy(c => c.Text)
				.AsNoTracking();
		}
	}
}
