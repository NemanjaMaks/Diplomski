using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Diplomski
{
    public class Global
    {

        public delegate void LoginHandler(object sender, LoginEventArgs args);
        public static LoginHandler LoginEvent;

        public delegate void ChangePageHandler(object sender, Page page);
        public static ChangePageHandler ChangePageEvent;


    }
}
