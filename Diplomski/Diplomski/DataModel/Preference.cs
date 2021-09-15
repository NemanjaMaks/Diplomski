using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.DataModel
{
    public class Preference
    {
        private int id;
        private int id_korisnika;
        private bool vikend;
        private bool uvece;
        private bool bezPauze;

        public int Id { get => id; set => id = value; }
        public int Id_korisnika { get => id_korisnika; set => id_korisnika = value; }
        public bool Vikend { get => vikend; set => vikend = value; }
        public bool Uvece { get => uvece; set => uvece = value; }
        public bool BezPauze { get => bezPauze; set => bezPauze = value; }

        public Preference()
        {
        }

        public Preference(int id, int id_korisnika, bool vikend, bool uvece, bool bezPauze)
        {
            this.id = id;
            this.id_korisnika = id_korisnika;
            this.vikend = vikend;
            this.uvece = uvece;
            this.bezPauze = bezPauze;
        }
    }
}
