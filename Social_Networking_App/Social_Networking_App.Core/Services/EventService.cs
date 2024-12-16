using Social_Networking_App.Core.Domains;
using Social_Networking_App.Core.Domains.IRepositories;
using Social_Networking_App.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Networking_App.Core.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepo _eventRepo;
        public EventService(IEventRepo eventRepo)
        {
            _eventRepo = eventRepo;
        }

        public async Task<bool> AddEvent(Event model)
        {
            if (model is null)
            {
                throw new ArgumentNullException("model");
            }
            else
            {
                return await _eventRepo.AddAsync(model);
            }

        }

        public async Task<IEnumerable<Event>> GetAllEvents()
        {
            return await _eventRepo.GetAllAsync();
        }

    }
}
