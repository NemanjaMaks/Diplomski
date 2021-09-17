using Diplomski.DatabaseHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.DataModel
{
    public class Zahtev
    {
        public int id;
        public int korisnik_od;
        public int korisnik_za;
        private string ime_korisnika;
        private string status;
        public int id_dezurstva_za;
        public int id_dezurstva_od;
        private string dezurstvo_od_str;
        private string dezurstvo_za_str;
        public Dezurstvo dezurstvo_od;
        public Dezurstvo dezurstvo_za;

        public string Ime { get => ime_korisnika; set => ime_korisnika = value; }
        public string PonudjenoDezurstvo { get => dezurstvo_od_str; set => dezurstvo_od_str = value; }
        public string TrazenoDezurstvo { get => dezurstvo_za_str; set => dezurstvo_za_str = value; }
        public string Status { get => status; set => status = value; }

        public Zahtev()
        {
        }

        public Zahtev(int id, int korisnik_od, int korisnik_za, string ime_korisnika, string status, int id_dezurstva_od, int id_dezurstva_za)
        {
            this.id = id;
            this.korisnik_od = korisnik_od;
            this.korisnik_za = korisnik_za;
            this.ime_korisnika = ime_korisnika;
            this.status = status;
            this.id_dezurstva_od = id_dezurstva_od;
            this.id_dezurstva_za = id_dezurstva_za;

            dezurstvo_od = SqlQueryHelper.GetDezurstvo(id_dezurstva_od);
            dezurstvo_za = SqlQueryHelper.GetDezurstvo(id_dezurstva_za);
            PonudjenoDezurstvo = string.Format("{0} {1} {2}", dezurstvo_od.Vreme.ToString(), dezurstvo_od.Predmet, dezurstvo_od.Sala);
            TrazenoDezurstvo = string.Format("{0} {1} {2}", dezurstvo_za.Vreme.ToString(), dezurstvo_za.Predmet, dezurstvo_za.Sala);
        }
    }
}
