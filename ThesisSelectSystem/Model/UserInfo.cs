using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UserInfo
    {
        public string account { get; set; }
        public string passwords { get; set; }
        public string salt { get; set; }
        public string roles { get; set; }
    }
}
