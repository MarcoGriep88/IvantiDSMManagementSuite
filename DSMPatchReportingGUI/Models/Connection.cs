using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMPatchReportingGUI.Models
{
    public class Connection
    {
        public String BaseURL { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public JWT Token { get; set; }
    }
}
