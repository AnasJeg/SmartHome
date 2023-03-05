using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHome
{
    internal class Zone
    {
        private int id;
        private string name;
        private int height;
        private int width;
        private static int cnt = 0;

       

        public Zone(String name,int x,int y) { 
            this.id = cnt++;
            this.name = name;
            this.height = y;
            this.width = x;
        }

        public string Name { get => name; set => name = value; }
        public int Height { get => height; set => height = value; }
        public int Width { get => width; set => width = value; }
        public int Id { get => id; }

    }
}
