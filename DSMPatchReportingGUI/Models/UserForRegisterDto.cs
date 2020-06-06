using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMPatchReportingGUI.Models
{
    public class UserForRegisterDto
    {
        public string Username { get; set; }

        //[StringLength(45, MinimumLength = 8, ErrorMessage = "You must specify password between 8 and 45 characters")]
        public string Password { get; set; }
    }
}
