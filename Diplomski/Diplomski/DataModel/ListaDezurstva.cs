using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.DataModel
{
    public class ListaDezurstva : List<Dezurstvo>
    {
        public Dezurstvo GetBestFit(Preference preference, LokalnePreference lokalnePreference)
        {
            Dezurstvo best = null;
            int bestCnt = 0;

            foreach (var dezurstvo in this)
            {
                bool overlap = false;
                foreach (var dez in MainWindow.User.mojaDezurstva)
                {
                    if (Nedostupan(dezurstvo, lokalnePreference) || dez.Overlap(dezurstvo))
                    {
                        overlap = true;
                        break;
                    }
                }

                if (overlap)
                    continue;

                int pauza = 1440;
                foreach (var dez in MainWindow.User.mojaDezurstva)
                {
                    int p = dez.Pauza(dezurstvo);
                    pauza = Math.Min(p, pauza);
                }

                int flag = dezurstvo.GetFlag(pauza);
                int x = ~(flag ^ preference.GetFlag());
                int cnt = 0;
                for (int i = 0; i < 3; i++)
                {
                    cnt += (x & (1 << i)) != 0 ? 1 : 0;
                }

                cnt += ViseUPeriodu(dezurstvo, lokalnePreference) ? 1 : 0;

                if (cnt > bestCnt)
                {
                    best = dezurstvo;
                    bestCnt = cnt;
                    if(bestCnt == 4)
                    {
                        return best;
                    }
                }
            }

            return best;
        }

        private static bool Nedostupan(Dezurstvo dezurstvo, LokalnePreference lokalnePreference)
        {
            if(lokalnePreference.DatumNedostupan_od != null && lokalnePreference.DatumNedostupan_do != null)
            {
                return lokalnePreference.DatumNedostupan_od <= dezurstvo.Vreme.Date && dezurstvo.Vreme.Date < lokalnePreference.DatumNedostupan_do;
            }
            else
            {
                return false;
            }
        }

        private static bool ViseUPeriodu(Dezurstvo dezurstvo, LokalnePreference lokalnePreference)
        {
            if(lokalnePreference.DatumVise_od != null && lokalnePreference.DatumVise_do != null)
            {
                return lokalnePreference.DatumVise_od <= dezurstvo.Vreme.Date && dezurstvo.Vreme.Date < lokalnePreference.DatumVise_do;
            }
            else
            {
                return false;
            }
        }
    }
}
