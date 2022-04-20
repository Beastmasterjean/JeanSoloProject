using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1533508_soloProject.Model
{
    internal class Cellule
    {
        public int etat { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public int isAlive { get; set; }

        public Cellule() { }

        public Cellule(int etat, double x, double y, double width, double height, int isAlive)
        {
            this.etat = etat;
            X = x;
            Y = y;
            Width = width;
            Height = height;
            this.isAlive = isAlive;
        }
    }
}
