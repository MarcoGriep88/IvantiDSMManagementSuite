using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Packless.Models
{
    public class PatchData
    {
        public int Id { get; set; }
        public String Computer { get; set; }
        public String Patch { get; set; }
        public String Compliance { get; set; }
        public DateTime FoundDate { get; set; }
        public DateTime? FixDate { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
