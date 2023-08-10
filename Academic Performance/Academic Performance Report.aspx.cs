using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Net;
using System.Net.Mail;

namespace Academic_Performance
{
    public partial class Academic_Performance_Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AcademicPerformanceReport.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
                AcademicPerformanceReport.LocalReport.ReportPath = Server.MapPath("~/AcademicPerformanceReport.rdlc");
                SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Academic_PerformanceConnectionString"].ConnectionString);
                SqlDataAdapter sqlData1 = new SqlDataAdapter("Select * from AcademicPerformanceReport_Design", connection);
                SqlDataAdapter sqlData2 = new SqlDataAdapter("Select * from Student where RegNo=" + Request.QueryString["Id"], connection);
                SqlDataAdapter sqlData3 = new SqlDataAdapter("Select * from Marks where RegNo=" + Request.QueryString["Id"], connection);
                DataSet ds1 = new DataSet();
                DataSet ds2 = new DataSet();
                DataSet ds3 = new DataSet();
                sqlData1.Fill(ds1);
                sqlData2.Fill(ds2);
                sqlData3.Fill(ds3);
                ReportDataSource rds1 = new ReportDataSource("DataSet1", ds1.Tables[0]);
                ReportDataSource rds2 = new ReportDataSource("DataSet2", ds2.Tables[0]);
                ReportDataSource rds3 = new ReportDataSource("DataSet3", ds3.Tables[0]);

                AcademicPerformanceReport.LocalReport.DataSources.Clear();

                AcademicPerformanceReport.LocalReport.DataSources.Add(rds1);
                AcademicPerformanceReport.LocalReport.DataSources.Add(rds2);
                AcademicPerformanceReport.LocalReport.DataSources.Add(rds3);


                using (MailMessage msg = new MailMessage("PrincipalofAIT@gmail.com", ds3.Tables[0].Rows[0][1].ToString()))
                {
                    msg.Subject = "Academic Performance Report";
                    msg.Body = "<h2 style='color:black; font-family: Times New Roman; font-weight: bold'>A Report of Academic Performance of Your Son ..</h2>";
                    msg.IsBodyHtml = true;
                    msg.Attachments.Add(new Attachment(ExportReportToPDF(Server.MapPath("~/Reports/"), "Report.pdf")));
                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    NetworkCredential credential = new NetworkCredential();
                    credential.UserName = "PrincipalofAIT@gmail.com";
                    credential.Password = "tumdbdnnwkkgpprf";
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = credential;
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Send(msg);
                }
            }
        }

        private string ExportReportToPDF(string path, string reportName)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string filenameExtension;
            byte[] bytes = AcademicPerformanceReport.LocalReport.Render("PDF", null, out mimeType, out encoding, out filenameExtension, out streamids, out warnings);
            string filename = path + reportName;
            using (var fs = new System.IO.FileStream(filename, System.IO.FileMode.Create))
            {
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
            }

            return filename;
        }
    }
}