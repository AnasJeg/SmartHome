using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome
{
    internal class Users
    {
        private int id;
        private string name;
        private string password;
        private string role;
        private static int cnt = 0;

      

        public Users(string name, string password, string role)
        {
            this.id = ++cnt;
            this.name = name;
            this.password = password;
            this.role = role;
        }
        public Users()
        {
        }
        public Users(string role)
        {
            this.role = role;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Password { get => password; set => password = value; }
        public string Role { get => role; set => role = value; }

        public string ToString()
        {
            return this.role;
        }
    }
}
