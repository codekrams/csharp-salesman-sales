using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VertreterUmsaetze
{
    class Auftrag
    {
        private int id;
        private string auftragsnummer;
        private int vertreter_id;
        private string umsatz;

        public Auftrag(int id, string auftragsnummer, int vertreter_id, string umsatz)
        {
            this.id = id;
            this.auftragsnummer = auftragsnummer;
            this.vertreter_id = vertreter_id;
            this.umsatz = umsatz;
        }

        public int getID()
        {
            return id;
        }

        public string getAuftragsnummer()
        {
            return auftragsnummer;
        }

        public int getVertreter_ID()
        {
            return vertreter_id;
        }

        public string getUmsatz()
        {
            return umsatz;
        }
    }
}
