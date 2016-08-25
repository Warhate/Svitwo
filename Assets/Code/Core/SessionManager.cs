using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Svitwo
{
    public interface ISessionContext
    {
        EntityManager EntityManager { get; }
    }

    public class SessionManager
    {
        ISessionContext _context;

        public void Init(ISessionContext context)
        {
            //load player
        }

        public void Shutdown()
        {
            
        }
    }
}
