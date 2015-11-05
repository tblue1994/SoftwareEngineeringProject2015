using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsPhoneApp.Models;

namespace WindowsPhoneApp
{
    public class UserState
    {
        public static string CurrentId { get { return ActiveAccount.Id; } }
        public static Account ActiveAccount { get; set; }

		public static bool UseOldUnits = false;
    }
}
