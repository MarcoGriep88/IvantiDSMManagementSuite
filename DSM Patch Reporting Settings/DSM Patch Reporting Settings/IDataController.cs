using DSMPatchReportingGUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMPatchReportingGUI.Interfaces
{
    interface IDataController
    {
        JWT Login(string BaseURL, UserForLoginDto user);
    }
}
