using DSMPatchReportingGUI.Controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSMPatchReportingGUI.HelperClasses
{
    public static class HTMLHelper
    {
        private static string LastDSMPR_TITLE = String.Empty;

        public static void WriteHTMLHeader(ref ReportWriter writer, string DSMPR_TITLE)
        {
            StreamReader read = new StreamReader(Environment.CurrentDirectory.ToString() + "\\templates\\header.html");
            string html = read.ReadToEnd();

            LastDSMPR_TITLE = DSMPR_TITLE;

            html = html.Replace("{{_DSMPR_REPORT_TITLE_}}", DSMPR_TITLE);

            writer.Write(html);
        }

        internal static void WriteHTMLBody(ref ReportWriter writer, string dataHTML)
        {
            StreamReader read = new StreamReader(Environment.CurrentDirectory.ToString() + "\\templates\\body.html");
            string html = read.ReadToEnd();

            html = html.Replace("{{_DSMPR_REPORT_DATA_}}", dataHTML);
            html = html.Replace("{{_DSMPR_REPORT_TITLE_}}", LastDSMPR_TITLE);

            writer.Write(html);
        }

        public static void WriteHTMLFooter(ref ReportWriter writer)
        {
            StreamReader read = new StreamReader(Environment.CurrentDirectory.ToString() + "\\templates\\footer.html");
            string html = read.ReadToEnd();

            writer.Write(html);
        }
    }
}
