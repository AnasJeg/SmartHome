using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome
{
    internal class Session
    {
        public Session()
        {
        }

        public Users user  { get; set; }  
        public bool isLoggedIn {
            get
            {
                if (user != null)
                    return true;
                else 
                    return false;
            }
                }
    }
}
