using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMPatchReportingGUI.Models
{
    public class DateOpenClosedStatDto
    {
        public String Date { get; set; }
        public int Fixed { get; set; }
        public int NotFixed { get; set; }
        public int NotApplicable { get; set; }
    }
}
