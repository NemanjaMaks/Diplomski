using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.DataModel
{
    public class LokalnePreference
    {
        private int id;
        private int id_korisnika;
        private int id_roka;
        private DateTime? datumNedostupan_od;
        private DateTime? datumNedostupan_do;
        private DateTime? datumVise_od;
        private DateTime? datumVise_do;

        public DateTime? DatumNedostupan_od { get => datumNedostupan_od; set => datumNedostupan_od = value; }
        public DateTime? DatumNedostupan_do { get => datumNedostupan_do; set => datumNedostupan_do = value; }
        public DateTime? DatumVise_od { get => datumVise_od; set => datumVise_od = value; }
        public DateTime? DatumVise_do { get => datumVise_do; set => datumVise_do = value; }
        public int Id { get => id; set => id = value; }
        public int Id_korisnika { get => id_korisnika; set => id_korisnika = value; }
        public int Id_roka { get => id_roka; set => id_roka = value; }

        public LokalnePreference()
        {
        }

        public LokalnePreference(int id, int id_korisnika, int id_roka, DateTime? datumNedostupan_od, DateTime? datumNedostupan_do, DateTime? datumVise_od, DateTime? datumVise_do)
        {
            this.id = id;
            this.id_korisnika = id_korisnika;
            this.id_roka = id_roka;
            this.datumNedostupan_od = datumNedostupan_od;
            this.datumNedostupan_do = datumNedostupan_do;
            this.datumVise_od = datumVise_od;
            this.datumVise_do = datumVise_do;
        }
    }
}
