using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.DataModel
{
    public class NameValuePair
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

    public class PreferenceStatistika : Dictionary<string, List<NameValuePair>>
    {
    }
}
