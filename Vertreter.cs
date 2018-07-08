using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VertreterUmsaetze
{
    class Vertreter
    {
        private int id;
        private string nname;
        private string vname;

        public Vertreter(int id, string nname, string vname)
        {
            this.id = id;
            this.nname = nname;
            this.vname = vname;
        }

        public int getID()
        {
            return id;
        }

        public string getNname()
        {
            return nname;
        }

        public string getVname()
        {
            return vname;
        }

     
    }
}

