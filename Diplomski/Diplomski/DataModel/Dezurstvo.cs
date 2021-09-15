using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.DataModel
{
    public class Dezurstvo
    {
        private int id;
        private string rok;
        private int id_korisnika;
        private DateTime vreme;
        private string predmet;
        private string sala;

        public string Sala { get => sala; set => sala = value; }
        public string Predmet { get => predmet; set => predmet = value; }
        public DateTime Vreme { get => vreme; set => vreme = value; }
        public int Id_korisnika { get => id_korisnika; set => id_korisnika = value; }
        public string Rok { get => rok; set => rok = value; }
        public int Id { get => id; set => id = value; }

        public Dezurstvo()
        {
        }

        public Dezurstvo(int id, string rok, int id_korisnika, DateTime vreme, string predmet, string sala)
        {
            this.id = id;
            this.rok = rok;
            this.id_korisnika = id_korisnika;
            this.vreme = vreme;
            this.predmet = predmet;
            this.sala = sala;
        }
    }
}
