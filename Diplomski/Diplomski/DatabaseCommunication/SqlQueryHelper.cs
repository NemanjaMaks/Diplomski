using Diplomski.DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.DatabaseHelper
{
    public class SqlQueryHelper
    {

        public static Korisnik Login(string korIme, string sifra)
        {
            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                string sql = "select * from korisnik where korisnickoIme = @korIme and lozinka = @sifra";
                using(SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("korIme", korIme);
                    command.Parameters.AddWithValue("sifra", sifra);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Korisnik korisnik = new Korisnik();
                            korisnik.id = reader.GetInt32(0);
                            korisnik.id_katedre = reader.GetInt32(1);
                            korisnik.Ime = reader.GetString(2);
                            korisnik.KorisnickoIme = reader.GetString(3);
                            korisnik.Sifra = reader.GetString(4);
                            korisnik.JeAdmin = reader.GetBoolean(5);
                            korisnik.Zvanje = !reader.IsDBNull(6) ? reader.GetString(6) : null;
                            korisnik.Email = !reader.IsDBNull(7) ? reader.GetString(7) : null;

                            return korisnik;
                        }
                    }
                }
            }

            return null;
        }

        public static ListaDezurstva GetMojaDezurstva(int id_korisnika)
        {
            ListaDezurstva dezurstva = new ListaDezurstva();
            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                string sql = @"select d.id, i.nazivRoka, d.predmet, d.vreme, d.sala from Dezurstvo d
                                join IspitniRok i on d.id_roka = i.id
                                where d.id_korisnika = @id_korisnika
                                and i.aktivan = 1;";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("id_korisnika", id_korisnika);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Dezurstvo dezurstvo = new Dezurstvo();
                            dezurstvo.id = reader.GetInt32(0);
                            dezurstvo.Rok = reader.GetString(1);
                            dezurstvo.Predmet = reader.GetString(2);
                            dezurstvo.Vreme = reader.GetDateTime(3);
                            dezurstvo.Sala = reader.GetString(4);

                            dezurstvo.id_korisnika = id_korisnika;

                            dezurstva.Add(dezurstvo);
                        }
                    } 
                }
            }

            return dezurstva;
        }

        public static void DodajDezurstva(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                string rok = row.ItemArray[0].ToString();
                DateTime vreme = DateTime.ParseExact(string.Format("{0} {1}", row.ItemArray[1].ToString(), row.ItemArray[2].ToString()), "dd.MM.yyyy. H:mm", CultureInfo.InvariantCulture);
                string predmet = row.ItemArray[3].ToString();
                string sala = row.ItemArray[4].ToString();
                string nastavnik = row.ItemArray[5].ToString();
                DodajRok(rok);
                using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
                {
                    string sql = @"insert into Dezurstvo (id_korisnika, id_roka, vreme, predmet, sala) 
                                    values((select id from Korisnik where ime = @nastavnik), (select id from IspitniRok where nazivRoka = @rok), @vreme, @predmet, @sala)";
                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("nastavnik", nastavnik);
                        command.Parameters.AddWithValue("rok", rok);
                        command.Parameters.AddWithValue("vreme", vreme);
                        command.Parameters.AddWithValue("predmet", predmet);
                        command.Parameters.AddWithValue("sala", sala);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public static void DodajGlavnaDezurstva(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                string rok = row.ItemArray[0].ToString();
                DateTime vreme = DateTime.ParseExact(string.Format("{0} {1}", row.ItemArray[1].ToString(), row.ItemArray[2].ToString()), "dd.MM.yyyy. H:mm", CultureInfo.InvariantCulture);
                string predmet = row.ItemArray[3].ToString();
                string nastavnik = row.ItemArray[4].ToString();
                string saradnik = row.ItemArray[5].ToString();
                DodajRok(rok);
                using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
                {
                    string sql;
                    if (string.IsNullOrEmpty(saradnik))
                    {
                        sql = @"insert into GlavnoDezurstvo (id_glavniNastavnik, id_roka, vreme, predmet) 
                                    values((select id from Korisnik where ime = @nastavnik), (select id from IspitniRok where nazivRoka = @rok), @vreme, @predmet)";
                    }
                    else
                    {
                        sql = @"insert into GlavnoDezurstvo (id_glavniNastavnik, id_glavniSaradnik, id_roka, vreme, predmet) 
                                    values((select id from Korisnik where ime = @nastavnik), (select id from Korisnik where ime = @saradnik), 
                                    (select id from IspitniRok where nazivRoka = @rok), @vreme, @predmet)";
                    }
                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("nastavnik", nastavnik);
                        command.Parameters.AddWithValue("saradnik", saradnik);
                        command.Parameters.AddWithValue("rok", rok);
                        command.Parameters.AddWithValue("vreme", vreme);
                        command.Parameters.AddWithValue("predmet", predmet);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public static void DodajKorisnike(DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                string katedra = row.ItemArray[0].ToString();
                string ime = row.ItemArray[1].ToString();
                string zvanje = row.ItemArray[2].ToString();
                string korime = row.ItemArray[3].ToString();
                string lozinka = row.ItemArray[4].ToString();
                string mejl = row.ItemArray[5].ToString();
                using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
                {
                    string sql = @"insert into Korisnik (id_katedre, ime, korisnickoIme, lozinka, isAdmin, zvanje, email) 
                                    values((select id from Katedra where naziv_katedre = @katedra), @ime, @korime, @lozinka, 0, @zvanje, @mejl)";
                    using (SqlCommand command = new SqlCommand(sql, conn))
                    {
                        command.Parameters.AddWithValue("katedra", katedra);
                        command.Parameters.AddWithValue("ime", ime);
                        command.Parameters.AddWithValue("zvanje", zvanje);
                        command.Parameters.AddWithValue("korime", korime);
                        command.Parameters.AddWithValue("lozinka", lozinka);
                        command.Parameters.AddWithValue("mejl", mejl);
                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        public static void DodajRok(string nazivRoka)
        {
            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {

                string sql = @"BEGIN
                                    IF NOT EXISTS (select * from IspitniRok where nazivRoka = @nazivRoka)
                                    BEGIN
                                        insert into IspitniRok (nazivRoka, aktivan) 
                                        values(@nazivRoka, 1)
                                    END
                                END";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("nazivRoka", nazivRoka);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static ListaDezurstva GetDezurstva(int id_korisnika = -1)
        {
            ListaDezurstva dezurstva = new ListaDezurstva();
            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                string sql = @"select d.id, i.nazivRoka, d.predmet, d.vreme, d.sala, d.id_korisnika, k.ime
                                from Dezurstvo d
                                join IspitniRok i on d.id_roka = i.id
                                join Korisnik k on k.id = d.id_korisnika
                                where d.id_korisnika <> @id_korisnika
                                and i.aktivan = 1;";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("id_korisnika", id_korisnika);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Dezurstvo dezurstvo = new Dezurstvo();
                            dezurstvo.id = reader.GetInt32(0);
                            dezurstvo.Rok = reader.GetString(1);
                            dezurstvo.Predmet = reader.GetString(2);
                            dezurstvo.Vreme = reader.GetDateTime(3);
                            dezurstvo.Sala = reader.GetString(4);
                            dezurstvo.id_korisnika = reader.GetInt32(5);
                            dezurstvo.ImeKorisnika = reader.GetString(6);

                            dezurstva.Add(dezurstvo);
                        }
                    }
                }
            }

            return dezurstva;
        }

        public static Dezurstvo GetDezurstvo(int id_dezurstva)
        {
            Dezurstvo dezurstvo = null;
            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                string sql = @"select d.id, i.nazivRoka, d.predmet, d.vreme, d.sala, d.id_korisnika from Dezurstvo d
                                join IspitniRok i on d.id_roka = i.id
                                where d.id = @id_dezurstva;";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("id_dezurstva", id_dezurstva);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            dezurstvo = new Dezurstvo();
                            dezurstvo.id = reader.GetInt32(0);
                            dezurstvo.Rok = reader.GetString(1);
                            dezurstvo.Predmet = reader.GetString(2);
                            dezurstvo.Vreme = reader.GetDateTime(3);
                            dezurstvo.Sala = reader.GetString(4);
                            dezurstvo.id_korisnika = reader.GetInt32(5);
                        }
                    }
                }
            }

            return dezurstvo;
        }

        public static Preference GetPreference(int id_korisnika)
        {
            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                string sql = "select * from preference where id_korisnika = @id_korisnika";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("id_korisnika", id_korisnika);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Preference preference = new Preference();
                            preference.Id = reader.GetInt32(0);
                            preference.Id_korisnika = reader.GetInt32(1);
                            preference.BezPauze = reader.GetBoolean(2);
                            preference.Uvece = reader.GetBoolean(3);
                            preference.Vikend = reader.GetBoolean(4);

                            return preference;
                        }
                    }  
                }
            }

            //Ako nije pronasao preference za korisnika, dodaj nove i njih vrati
            CreatePreference(id_korisnika);
            return GetPreference(id_korisnika);
        }

        public static LokalnePreference GetLokalnePreference(int id_korisnika)
        {
            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                string sql = @"select lp.* from LokalnePreference lp
                                join IspitniRok i on i.id = lp.id_roka
                                where lp.id_korisnika = @id_korisnika
                                and i.aktivan = 1";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("id_korisnika", id_korisnika);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            LokalnePreference preference = new LokalnePreference();
                            preference.Id = reader.GetInt32(0);
                            preference.Id_korisnika = reader.GetInt32(1);
                            preference.Id_roka = reader.GetInt32(2);
                            preference.DatumNedostupan_od = reader.IsDBNull(3) ? (DateTime?)null : reader.GetDateTime(3);
                            preference.DatumNedostupan_do = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4);
                            preference.DatumVise_od = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5);
                            preference.DatumVise_do = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6);

                            return preference;
                        }
                    }
                }
            }

            //Ako nije pronasao preference za korisnika, dodaj nove i njih vrati
            CreateLokalnePreference(id_korisnika);
            return GetLokalnePreference(id_korisnika);
        }

        public static void ChangePreference(Preference preference)
        {
            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                string sql = @"update preference 
                        set vikend = @vikend,
                            uvece = @uvece,
                            bezPauze = @bezPauze
                        where id = @id";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("id", preference.Id);
                    command.Parameters.AddWithValue("vikend", preference.Vikend);
                    command.Parameters.AddWithValue("uvece", preference.Uvece);
                    command.Parameters.AddWithValue("bezPauze", preference.BezPauze);

                    command.ExecuteNonQuery();
                }
            }
        }

        private static void CreatePreference(int id_korisnika) 
        {
            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                string sql = "insert into preference (id_korisnika) values(@id_korisnika)";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("id_korisnika", id_korisnika);
                    command.ExecuteNonQuery();
                }
            }
        }

        private static void CreateLokalnePreference(int id_korisnika)
        {
            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                string sql = "insert into LokalnePreference (id_korisnika, id_roka) values(@id_korisnika, (select id from IspitniRok where aktivan = 1))";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("id_korisnika", id_korisnika);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void ChangeLokalnePreference(LokalnePreference preference)
        {
            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                string sql = @"update LokalnePreference 
                        set nedostupan_od = @nedostupan_od,
                            nedostupan_do = @nedostupan_do,
                            zelimVise_od = @vise_od,
                            zelimVise_do = @vise_do
                        where id = @id";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("id", preference.Id);
                    if (preference.DatumNedostupan_od == null)
                    {
                        command.Parameters.AddWithValue("nedostupan_od", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("nedostupan_od", preference.DatumNedostupan_od);
                    }

                    if (preference.DatumNedostupan_do == null)
                    {
                        command.Parameters.AddWithValue("nedostupan_do", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("nedostupan_do", preference.DatumNedostupan_do);
                    }

                    if (preference.DatumVise_od == null)
                    {
                        command.Parameters.AddWithValue("vise_od", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("vise_od", preference.DatumVise_od);
                    }

                    if (preference.DatumVise_do == null)
                    {
                        command.Parameters.AddWithValue("vise_do", DBNull.Value);
                    }
                    else
                    {
                        command.Parameters.AddWithValue("vise_do", preference.DatumVise_do);
                    }

                    command.ExecuteNonQuery();
                }
            }
        }

        public static List<Zahtev> GetPoslateZahteve(int id_korisnika)
        {
            List<Zahtev> zahtevi = new List<Zahtev>();
            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                string sql = @"select z.id, z.korisnik_od, z.korisnik_za, k.ime, z.status, z.id_dezurstva_od, z.id_dezurstva_za
                                from Zahtevi z
                                join Dezurstvo d on z.id_dezurstva_za = d.id
                                join Korisnik k on k.id = z.korisnik_za
                                where z.korisnik_od = @id_korisnika and status <> 'zatvoren';";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("id_korisnika", id_korisnika);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            int korisnik_od = reader.GetInt32(1);
                            int korisnik_za = reader.GetInt32(2);
                            string ime_korisnika = reader.GetString(3);
                            string status = reader.GetString(4);
                            int id_dezurstva_od = reader.GetInt32(5);
                            int id_dezurstva_za = reader.GetInt32(6);
                            Zahtev zahtev = new Zahtev(id, korisnik_od, korisnik_za, ime_korisnika, status, id_dezurstva_od, id_dezurstva_za);

                            zahtevi.Add(zahtev);
                        }
                    }
                }
            }

            return zahtevi;
        }

        public static List<Zahtev> GetPrimljeneZahteve(int id_korisnika)
        {
            List<Zahtev> zahtevi = new List<Zahtev>();
            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                string sql = @"select z.id, z.korisnik_od, z.korisnik_za, k.ime, z.status, z.id_dezurstva_od, z.id_dezurstva_za
                                from Zahtevi z
                                join Dezurstvo d on z.id_dezurstva_za = d.id
                                join Korisnik k on k.id = z.korisnik_od
                                where z.korisnik_za = @id_korisnika and status = 'poslat';";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("id_korisnika", id_korisnika);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            int korisnik_od = reader.GetInt32(1);
                            int korisnik_za = reader.GetInt32(2);
                            string ime_korisnika = reader.GetString(3);
                            string status = reader.GetString(4);
                            int id_dezurstva_od = reader.GetInt32(5);
                            int id_dezurstva_za = reader.GetInt32(6);
                            Zahtev zahtev = new Zahtev(id, korisnik_od, korisnik_za, ime_korisnika, status, id_dezurstva_od, id_dezurstva_za);

                            zahtevi.Add(zahtev);
                        }
                    }
                }
            }

            return zahtevi;
        }

        public static void RequestZamena(int id_dezurstva_za, int id_dezurstva_od, int id_korisnika_za, int id_korisnika_od)
        {
            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                string sql = @"insert into zahtevi (korisnik_od, korisnik_za, status, id_dezurstva_od, id_dezurstva_za)
                                values(@id_korisnika_od, @id_korisnika_za, 'poslat', @id_dezurstva_od, @id_dezurstva_za)";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("id_korisnika_od", id_korisnika_od);
                    command.Parameters.AddWithValue("id_korisnika_za", id_korisnika_za);
                    command.Parameters.AddWithValue("id_dezurstva_od", id_dezurstva_od);
                    command.Parameters.AddWithValue("id_dezurstva_za", id_dezurstva_za);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void ChangeZahtevStatus(string status, int id)
        {
            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                string sql = @"update zahtevi 
                                set status = @status
                                where id = @id";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("id", id);
                    command.Parameters.AddWithValue("status", status);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static void Zameni(int id_dezurstva_za, int id_dezurstva_od, int id_korisnika_za, int id_korisnika_od)
        {
            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                string sql = @"update dezurstvo 
                                set id_korisnika = @id_korisnika
                                where id = @id";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("id", id_dezurstva_za);
                    command.Parameters.AddWithValue("id_korisnika", id_korisnika_od);

                    command.ExecuteNonQuery();
                }
            }

            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                string sql = @"update dezurstvo 
                                set id_korisnika = @id_korisnika
                                where id = @id";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    command.Parameters.AddWithValue("id", id_dezurstva_od);
                    command.Parameters.AddWithValue("id_korisnika", id_korisnika_za);

                    command.ExecuteNonQuery();
                }
            }
        }

        public static PreferenceStatistika GetPreferenceStatistika()
        {
            string sql = @"select 'Rad vikendom', 'Da', (select count(*) from Preference where vikend = 1)
                            union all
                            select 'Rad vikendom', 'Ne',(select count(*) from Preference where vikend = 0)
                            union all
                            select 'Deo dana', 'Prva_polovina_dana',(select count(*) from Preference where uvece = 0)
                            union all
                            select 'Deo dana', 'Druga_polovina_dana',(select count(*) from Preference where uvece = 1)
                            union all
                            select 'Pauza', 'Bez_pauze',(select count(*) from Preference where bezPauze = 1)
                            union all
                            select 'Pauza', 'Sa_pauzom',(select count(*) from Preference where bezPauze = 0);";

            PreferenceStatistika statistika = new PreferenceStatistika();
            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string key = reader.GetString(0);
                            string name = reader.GetString(1);
                            int val = reader.GetInt32(2);

                            if (statistika.ContainsKey(key))
                            {
                                statistika[key].Add(new NameValuePair { Name = name, Value = val });
                            }
                            else
                            {
                                statistika.Add(key, new List<NameValuePair>() { new NameValuePair { Name = name, Value = val } });
                            }
                        }
                    }
                }
            }

            return statistika;
        }

        public static List<Korisnik> GetKorisnike()
        {
            List<Korisnik> korisnici = new List<Korisnik>();

            using (SqlConnection conn = DatabaseCommunication.SqlConnection.GetConnection())
            {
                string sql = @"select * from Korisnik";
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Korisnik korisnik = new Korisnik();
                            korisnik.id = reader.GetInt32(0);
                            korisnik.id_katedre = reader.GetInt32(1);
                            korisnik.Ime = reader.GetString(2);
                            korisnik.KorisnickoIme = reader.GetString(3);
                            korisnik.Sifra = reader.GetString(4);
                            korisnik.JeAdmin = reader.GetBoolean(5);
                            korisnik.Zvanje = !reader.IsDBNull(6) ? reader.GetString(6) : null;
                            korisnik.Email = !reader.IsDBNull(7) ? reader.GetString(7) : null;

                            korisnici.Add(korisnik);
                        }
                    }
                }
            }

            return korisnici;
        }
    }
}
