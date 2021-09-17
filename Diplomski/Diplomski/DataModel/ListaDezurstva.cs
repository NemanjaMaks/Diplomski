using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.DataModel
{
    public class ListaDezurstva : List<Dezurstvo>
    {
        public Dezurstvo GetBestFit(Preference preference)
        {
            Dezurstvo best = null;
            int bestCnt = 0;

            foreach (var dezurstvo in this)
            {
                bool overlap = false;
                foreach (var dez in MainWindow.User.mojaDezurstva)
                {
                    if (dez.Overlap(dezurstvo))
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

                if(cnt > bestCnt)
                {
                    best = dezurstvo;
                    bestCnt = cnt;
                    if(bestCnt == 3)
                    {
                        return best;
                    }
                }
            }

            return best;
        }
    }
}
