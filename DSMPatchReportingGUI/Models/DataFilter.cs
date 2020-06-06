using System;

namespace DSMPatchReportingGUI.Models
{
    public class DataFilter
    {
        public string ComputerName { get; set; }
        public int DataSetCount { get; set; }

        public DateTime? SpecificDate { get; set; }
    }
}