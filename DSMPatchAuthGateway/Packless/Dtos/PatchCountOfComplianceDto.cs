using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Packless.Dtos
{
    public class PatchCountOfComplianceDto
    {
        public String Patch { get; set; }
        public int Count { get; set; }
        public String Compliance { get; set; }
    }
}
