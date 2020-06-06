using DSMPatchReportingGUI.HelperClasses;
using DSMPatchReportingGUI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DSMPatchReportingGUI.Models.ExportDataType;

namespace DSMPatchReportingGUI.Controller
{
    public static class ReportController
    {
        private readonly static string m_dataDirectory = @"C:\ProgramData\DSMPatchReporting\data";
        public static String DataDirectory { get { return m_dataDirectory;  } }

        public static void CreateVulnReport(DATA_TYPE fileType, List<PatchCountOfComplianceDto> data)
        {
            string fileName = GetDataTypeFileName(fileType, "VulnReport");
            if (!Directory.Exists(m_dataDirectory))
                Directory.CreateDirectory(m_dataDirectory);
            ReportWriter writer = new ReportWriter(fileName);

            if (fileType == DATA_TYPE.CSV)
            {
                WriteCSVVulnReport(data, writer);
                writer.StartFile();
            }

            if (fileType == DATA_TYPE.XLS)
            {
                WriteCSVVulnReport(data, writer);
                Process.Start(Environment.CurrentDirectory.ToString() + "\\bin\\convertVR.cmd", fileName);
            }

            if (fileType == DATA_TYPE.HTML)
            {
                WriteHTMLVulnReport(data, writer);
                Process.Start(fileName);
            }

            LoaderHelper.LoaderKill();
        }

        private static void WriteHTMLVulnReport(List<PatchCountOfComplianceDto> data, ReportWriter writer)
        {
            HTMLHelper.WriteHTMLHeader(ref writer, "Vulnerablitiy Report");

            string dataHTML = String.Empty;

            dataHTML += "<table id=\"DataTable\" class=\"table table-bordered table-striped\">" +
                "<thead><tr><th>Patch</th><th>Compliance</th><th>Count</th></tr></thead>";

            dataHTML += "<tbody>";

            foreach(var d in data)
            {

                string rowClass = "";
                if (d.Compliance.ToLower().Contains("notfixed"))
                {
                    rowClass = "danger";
                }
                if (d.Compliance.ToLower().Contains("fixed") && !d.Compliance.ToLower().Contains("notfixed"))
                {
                    rowClass = "success";
                }

                if (rowClass != "")
                {
                    dataHTML += "<tr class=\"" + rowClass + "\"><td>" + d.Patch + "</td><td>" + d.Compliance + "</td><td>" + d.Count + "</td></tr>";
                }
                else
                {
                    dataHTML += "<tr><td>" + d.Patch + "</td><td>" + d.Compliance + "</td><td>" + d.Count + "</td></tr>";
                }
                
            }

            dataHTML += "</tbody></table>";

            HTMLHelper.WriteHTMLBody(ref writer, dataHTML);
            HTMLHelper.WriteHTMLFooter(ref writer);
            writer.Close();
        }



        private static void WriteCSVVulnReport(List<PatchCountOfComplianceDto> data, ReportWriter writer)
        {
            writer.WriteLine("Patch;Compliance;Count");

            foreach (var v in data.OrderByDescending(n => n.Count))
            {
                writer.WriteLine(v.Patch + ";" + v.Compliance + ";" + v.Count);
            }
            writer.Close();
        }

        private static string GetFileName()
        {
            return m_dataDirectory + "\\" + DateTime.Now.ToString("yyyyMMdd-hhmmss");
        }

        internal static void CreateLatestReport(DATA_TYPE fileType, List<PatchData> data)
        {
            string fileName = GetDataTypeFileName(fileType, "LatestSecurity");
            if (!Directory.Exists(m_dataDirectory))
                Directory.CreateDirectory(m_dataDirectory);

            var writer = new ReportWriter(fileName);

            if (fileType == DATA_TYPE.CSV)
            {
                WriteCSVLastestReport(data, writer);
                writer.StartFile();
            }

            if (fileType == DATA_TYPE.XLS)
            {
                WriteCSVLastestReport(data, writer);
                Process.Start(Environment.CurrentDirectory.ToString() + "\\bin\\convertLR.cmd", fileName);
            }

            if (fileType == DATA_TYPE.HTML)
            {
                WriteHTMLLatestReport(data, writer);
                Process.Start(fileName);
            }

            LoaderHelper.LoaderKill();
        }

        private static void WriteHTMLLatestReport(List<PatchData> data, ReportWriter writer)
        {
                HTMLHelper.WriteHTMLHeader(ref writer, "Neuste Sicherheitslücken Report");

                string dataHTML = String.Empty;

                dataHTML += "<table id=\"DataTable\" class=\"table table-bordered table-striped\">" +
                    "<thead><tr><th>Computer</th><th>Patch</th><th>Compliance</th><th>Found Date</th><th>Fix Date</th></tr></thead>";

                dataHTML += "<tbody>";

                foreach (var d in data)
                {

                    string rowClass = "";
                    if (d.Compliance.ToLower().Contains("notfixed"))
                    {
                        rowClass = "danger";
                    }
                    if (d.Compliance.ToLower().Contains("fixed") && !d.Compliance.ToLower().Contains("notfixed"))
                    {
                        rowClass = "success";
                    }

                    if (rowClass != "")
                    {
                        dataHTML += "<tr class=\"" + rowClass + "\"><td>" + d.Computer + "</td><td>" + d.Patch + "</td><td>" + d.Compliance + "</td><td>" + d.FoundDate + "</td><td>" + d.FixDate + "</td></tr>";
                    }
                    else
                    {
                        dataHTML += "<tr><td>" + d.Computer + "</td><td>" + d.Patch + "</td><td>" + d.Compliance + "</td><td>" + d.FoundDate + "</td><td>" + d.FixDate + "</td></tr>";
                    }
                }

                dataHTML += "</tbody></table>";

                HTMLHelper.WriteHTMLBody(ref writer, dataHTML);
                HTMLHelper.WriteHTMLFooter(ref writer);
                writer.Close();
            
        }

        private static void WriteCSVLastestReport(List<PatchData> data, ReportWriter writer)
        {
            writer.WriteLine("Computer;Patch;Compliance;FoundDate;FixDate");

            foreach (var v in data)
            {
                writer.WriteLine(v.Computer + ";" + v.Patch + ";" + v.Compliance + ";" + v.FoundDate + ";" + v.FixDate);
            }
            writer.Close();
        }

        private static string GetDataTypeFileName(DATA_TYPE dt, string ReportName)
        {
            switch(dt)
            {
                case DATA_TYPE.CSV:
                    return GetFileName() + "-" + ReportName + ".csv";
                case DATA_TYPE.PDF:
                    return GetFileName() + "-" + ReportName + ".pdf";
                case DATA_TYPE.XLS:
                    return GetFileName() + "-" + ReportName + ".csv";
                case DATA_TYPE.HTML:
                    return GetFileName() + "-" + ReportName + ".html";
                case DATA_TYPE.XML:
                    return GetFileName() + "-" + ReportName + ".xml";
                default:
                    return GetFileName() + "-" + ReportName + ".csv";
            }
            
        }

        internal static void CreateReportByDateReport(DATA_TYPE fileType, List<PatchData> data)
        {
            string fileName = GetDataTypeFileName(fileType, "ReportByDateReport");
            if (!Directory.Exists(m_dataDirectory))
                Directory.CreateDirectory(m_dataDirectory);
            ReportWriter writer = new ReportWriter(fileName);

            if (fileType == DATA_TYPE.CSV)
            {
                WriteCSVReportbyDateReport(data, writer);
                writer.StartFile();
            }

            if (fileType == DATA_TYPE.XLS)
            {
                WriteCSVReportbyDateReport(data, writer);
                Process.Start(Environment.CurrentDirectory.ToString() + "\\bin\\convertVR.cmd", fileName);
            }

            LoaderHelper.LoaderKill();
        }

        private static void WriteCSVReportbyDateReport(List<PatchData> data, ReportWriter writer)
        {
            writer.WriteLine("Computer;Patch;Compliance;FoundDate;FixDate");

            foreach (var v in data)
            {
                writer.WriteLine(v.Computer + ";" + v.Patch + ";" + v.Compliance + ";" + v.FoundDate + ";" + v.FixDate);
            }
            writer.Close();
        }

        internal static void CreateOpenClosedReport(DATA_TYPE fileType, List<DateOpenClosedStatDto> data)
        {
            string fileName = GetDataTypeFileName(fileType, "OpenClosedReport");
            if (!Directory.Exists(m_dataDirectory))
                Directory.CreateDirectory(m_dataDirectory);
            ReportWriter writer = new ReportWriter(fileName);

            if (fileType == DATA_TYPE.CSV)
            {
                WriteCSVOpenClosed(data, writer);
                writer.StartFile();
            }

            if (fileType == DATA_TYPE.XLS)
            {
                WriteCSVOpenClosed(data, writer);
                Process.Start(Environment.CurrentDirectory.ToString() + "\\bin\\convertOCR.cmd", fileName);
            }

            if (fileType == DATA_TYPE.XLS)
            {
                WriteHTMLOpenClosed(data, writer);
                Process.Start(fileName);
            }

            LoaderHelper.LoaderKill();
        }

        private static void WriteHTMLOpenClosed(List<DateOpenClosedStatDto> data, ReportWriter writer)
        {
            HTMLHelper.WriteHTMLHeader(ref writer, "Übersicht Offene und Geschlossene Lücken");

            string dataHTML = String.Empty;

            dataHTML += "<table id=\"DataTable\" class=\"table table-bordered table-striped\">" +
                "<thead><tr><th>Date</th><th>Fixed</th><th>Not Fixed</th><th>Not Applicable</th></tr></thead>";

            dataHTML += "<tbody>";

            foreach (var d in data)
            {
                dataHTML += "<tr><td>" + d.Date + "</td><td>" + d.Fixed + "</td><td>" + d.NotFixed + "</td><td>" + d.NotApplicable + "</td></tr>";
            }

            dataHTML += "</tbody></table>";

            HTMLHelper.WriteHTMLBody(ref writer, dataHTML);
            HTMLHelper.WriteHTMLFooter(ref writer);
            writer.Close();

        }

        private static void WriteCSVOpenClosed(List<DateOpenClosedStatDto> data, ReportWriter writer)
        {
            writer.WriteLine("Date;Fixed;NotFixed;NotApplicable");

            foreach (var v in data)
            {
                writer.WriteLine(v.Date + ";" + v.Fixed + ";" + v.NotFixed + ";" + v.NotApplicable);
            }
            writer.Close();
        }

        internal static void CreateComplianceStatusReport(DATA_TYPE fileType, List<ComputerOpenClosedStatDto> data)
        {
            string fileName = GetDataTypeFileName(fileType, "ComplianceStatusReport");
            if (!Directory.Exists(m_dataDirectory))
                Directory.CreateDirectory(m_dataDirectory);
            ReportWriter writer = new ReportWriter(fileName);

            if (fileType == DATA_TYPE.CSV)
            {
                WriteCSVFixedInPercent(data, writer);
                writer.StartFile();
            }

            if (fileType == DATA_TYPE.XLS)
            {
                WriteCSVFixedInPercent(data, writer);
                Process.Start(Environment.CurrentDirectory.ToString() + "\\bin\\convertFPercR.cmd", fileName);
            }

            if (fileType == DATA_TYPE.HTML)
            {
                WriteHTMLFixedInPercent(data, writer);
                Process.Start(fileName);
            }

            LoaderHelper.LoaderKill();
        }

        private static void WriteHTMLFixedInPercent(List<ComputerOpenClosedStatDto> data, ReportWriter writer)
        {
            HTMLHelper.WriteHTMLHeader(ref writer, "Compliance");

            string dataHTML = String.Empty;

            dataHTML += "<table id=\"DataTable\" class=\"table table-bordered table-striped\">" +
                "<thead><tr><th>Computer</th><th>PercentFixed</th><th>PercentNotFixed</th></tr></thead>";

            dataHTML += "<tbody>";

            foreach (var d in data)
            {
                dataHTML += "<tr><td>" + d.Computer + "</td><td>" + d.PercentFixed.ToString("#.00") + "% </td><td>" + d.PercentNotFixed.ToString("#.00") + "% </td></tr>";
            }

            dataHTML += "</tbody></table>";

            HTMLHelper.WriteHTMLBody(ref writer, dataHTML);
            HTMLHelper.WriteHTMLFooter(ref writer);
            writer.Close();
        }

        private static void WriteCSVFixedInPercent(List<ComputerOpenClosedStatDto> data, ReportWriter writer)
        {
            writer.WriteLine("Computer;PercentFixed;PercentNotFixed");

            foreach (var v in data)
            {
                writer.WriteLine(v.Computer + ";" + v.PercentFixed.ToString("0.##") + ";" + v.PercentNotFixed.ToString("0.##"));
            }
            writer.Close();
        }

        internal static void CreateMasterStatusReport(DATA_TYPE fileType, List<DateOpenClosedStatDto> openclosed, List<PatchCountOfComplianceDto> issues, List<PatchData> patches)
        {
            string fileName = GetDataTypeFileName(fileType, "MasterReport");
            if (!Directory.Exists(m_dataDirectory))
                Directory.CreateDirectory(m_dataDirectory);
            ReportWriter writer = new ReportWriter(fileName);

            if (fileType == DATA_TYPE.CSV)
            {
                //WriteCSVFixedInPercent(data, writer);
                //writer.StartFile();
            }

            if (fileType == DATA_TYPE.XLS)
            {
                //WriteCSVFixedInPercent(data, writer);
                //Process.Start(Environment.CurrentDirectory.ToString() + "\\bin\\convertFPercR.cmd", fileName);
            }

            if (fileType == DATA_TYPE.HTML)
            {
                WriteHTMLMasterReport(openclosed, issues, patches, writer);
                Process.Start(fileName);
            }

            LoaderHelper.LoaderKill();
        }

        private static void WriteHTMLMasterReport(List<DateOpenClosedStatDto> openclosed, List<PatchCountOfComplianceDto> issues, List<PatchData> patches, ReportWriter writer)
        {
            HTMLHelper.WriteHTMLHeader(ref writer, "Zusammenfassender Report");

            string dataHTML = String.Empty;

            int sumFixed = patches.Count(x => x.Compliance == "Fixed");
            int sumNotFixed = patches.Count(x => x.Compliance == "NotFixed");
            int sumNotApplicable = patches.Count(x => x.Compliance == "NotApplicable");

            dataHTML += "<div class=\"chart col-12\"> <ul><li>Geschlossene Lücken: " + sumFixed + "</li><li>Offene Lücken: " + sumNotFixed + "</li><li>Nicht Anwendbar: " + sumNotApplicable + "</li></ul></div><br />";


            dataHTML += "<table id=\"DataTable\" class=\"table table-bordered table-striped\">" +
                "<thead><tr><th>Computer</th><th>Patch</th><th>Compliance</th><th>FoundDate</th><th>FixDate</th><th>CreatedAt</th></tr></thead>";

            dataHTML += "<tbody>";

            foreach (var d in patches)
            {
                string rowClass = "";
                if (d.Compliance.ToLower().Contains("notfixed"))
                {
                    rowClass = "danger";
                }
                if (d.Compliance.ToLower().Contains("fixed") && !d.Compliance.ToLower().Contains("notfixed"))
                {
                    rowClass = "success";
                }

                if (rowClass != "")
                {
                    dataHTML += "<tr class=\"" + rowClass + "\"><td>" + d.Computer + "</td><td>" + d.Patch + "</td><td>" + d.Compliance + "</td><td>" + d.FoundDate + "</td><td>" + d.FixDate + "</td><td>" + d.CreatedAt + "</td></tr>";
                }
                else
                {
                    dataHTML += "<tr><td>" + d.Computer + "</td><td>" + d.Patch + "</td><td>" + d.Compliance + "</td><td>" + d.FoundDate + "</td><td>" + d.FixDate + "</td><td>" + d.CreatedAt + "</td></tr>";
                }
            }

            dataHTML += "</tbody></table>";

            HTMLHelper.WriteHTMLBody(ref writer, dataHTML);
            HTMLHelper.WriteHTMLFooter(ref writer);
            writer.Close();
        }
    }
}
