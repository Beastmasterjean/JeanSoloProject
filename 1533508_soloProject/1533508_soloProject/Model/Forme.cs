using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1533508_soloProject.Model
{
    internal class Forme
    {
        List<Forme> formes;
        public List<Cellule> cellules { get; set; }

        public Forme(List<Cellule> cellules)
        {
            this.cellules = cellules;
        }
    }
}
