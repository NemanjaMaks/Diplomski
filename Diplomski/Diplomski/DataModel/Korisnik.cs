using Diplomski.DatabaseHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.DataModel
{
    public class Korisnik
    {
        public int id;
        public int id_katedre;
        private string ime;
        private string korIme;
        private string sifra;
        private bool admin;
        private string zvanje;
        private string email;

        public ListaDezurstva mojaDezurstva;
        public Preference preference;
        public LokalnePreference lokalnePreference;
        public List<Zahtev> poslatiZahtevi;
        public List<Zahtev> primljeniZahtevi;

        public string Ime { get => ime; set => ime = value; }
        public string KorisnickoIme { get => korIme; set => korIme = value; }
        public string Sifra { get => sifra; set => sifra = value; }
        public bool JeAdmin { get => admin; set => admin = value; }
        public string Zvanje { get => zvanje; set => zvanje = value; }
        public string Email { get => email; set => email = value; }

        public Korisnik()
        {
        }

        public Korisnik(int id, int id_katedre, string name, string username, string password, bool isAdmin, string zvanje, string email)
        {
            this.id = id;
            this.id_katedre = id_katedre;
            this.ime = name;
            this.korIme = username;
            this.sifra = password;
            this.admin = isAdmin;
            this.zvanje = zvanje;
            this.email = email;
        }

        public void LoadDezurstva()
        {
            mojaDezurstva = SqlQueryHelper.GetMojaDezurstva(this.id);
        }

        public void LoadPreference()
        {
            preference = SqlQueryHelper.GetPreference(this.id);
        }
        public void LoadLokalnePreference()
        {
            lokalnePreference = SqlQueryHelper.GetLokalnePreference(this.id);
        }

        public void LoadPoslateZahteve()
        {
            poslatiZahtevi = SqlQueryHelper.GetPoslateZahteve(this.id);
        }

        public void LoadPrimljeneZahteve()
        {
            primljeniZahtevi = SqlQueryHelper.GetPrimljeneZahteve(this.id);
        }

    }
}
