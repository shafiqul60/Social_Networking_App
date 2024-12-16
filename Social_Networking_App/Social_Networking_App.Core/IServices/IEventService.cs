using Social_Networking_App.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Social_Networking_App.Core.IServices
{
    public interface IEventService
    {
        Task<bool> AddEvent(Event model);

        Task<IEnumerable<Event>> GetAllEvents();
    }
}
