using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMPatchReportingGUI.Models
{
    public class PatchCountOfComplianceDto
    {
        public String Patch { get; set; }
        public int Count { get; set; }
        public String Compliance { get; set; }
    }
}
