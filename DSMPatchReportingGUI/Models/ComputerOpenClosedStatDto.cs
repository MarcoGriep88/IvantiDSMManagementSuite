using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMPatchReportingGUI.Models
{
    public class ComputerOpenClosedStatDto
    {
        public String Computer { get; set; }
        public double PercentFixed { get; set; }
        public double PercentNotFixed { get; set; }
    }
}
