using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.DataModel
{
    public class Dezurstvo
    {
        public int id;
        private string rok;
        public int id_korisnika;
        private string ime_korisnika;
        private DateTime vreme;
        private string predmet;
        private string sala;


        public string ImeKorisnika { get => ime_korisnika; set => ime_korisnika = value; }
        public string Rok { get => rok; set => rok = value; }
        public DateTime Vreme { get => vreme; set => vreme = value; }
        public string Predmet { get => predmet; set => predmet = value; }
        public string Sala { get => sala; set => sala = value; }

        public Dezurstvo()
        {
        }

        public Dezurstvo(int id, string rok, int id_korisnika, DateTime vreme, string predmet, string sala, string ime)
        {
            this.id = id;
            this.rok = rok;
            this.id_korisnika = id_korisnika;
            this.vreme = vreme;
            this.predmet = predmet;
            this.sala = sala;
            this.ime_korisnika = ime;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3}", this.rok, this.predmet, this.vreme.ToString(), this.sala);
        }

        public int GetFlag(int pauza)
        {
            int flag = 0;
            if(Vreme.DayOfWeek == DayOfWeek.Saturday || Vreme.DayOfWeek == DayOfWeek.Sunday)
            {
                flag |= 1;
                flag <<= 1;
            }

            if(Vreme.Hour >= 16)
            {
                flag |= 1;
                flag <<= 1;
            }

            if(pauza <= 30)
            {
                flag |= 1;
            }

            return flag;
        }

        public bool Overlap(Dezurstvo dezurstvo)
        {
            if(this.Vreme.Date == dezurstvo.Vreme.Date)
            {
                if(this.Vreme.TimeOfDay < dezurstvo.Vreme.TimeOfDay.Add(new TimeSpan(3,0,0)) && this.Vreme.TimeOfDay > dezurstvo.Vreme.TimeOfDay)
                {
                    return true;
                }

                if (dezurstvo.Vreme.TimeOfDay < this.Vreme.TimeOfDay.Add(new TimeSpan(3, 0, 0)) &&  dezurstvo.Vreme.TimeOfDay > this.Vreme.TimeOfDay)
                {
                    return true;
                }
            }

            return false;
        }

        public int Pauza(Dezurstvo dezurstvo)
        {
            int pauza = 0;

            if (this.Vreme.Date == dezurstvo.Vreme.Date)
            {
                pauza = (int)Math.Abs(this.Vreme.TimeOfDay.TotalMinutes - dezurstvo.Vreme.TimeOfDay.TotalMinutes) - 180;
                if(pauza < 0)
                {
                    pauza = 1440;
                }
            }
            else
            {
                pauza = 1440;
            }

            return pauza;
        }
    }
}
