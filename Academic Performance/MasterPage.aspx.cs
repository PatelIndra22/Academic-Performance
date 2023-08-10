using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using Microsoft.Reporting.WebForms;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization.Formatters.Binary;

namespace Academic_Performance
{
    public partial class HomePage : System.Web.UI.Page
    {
        SqlConnection Connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Academic_PerformanceConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            Connection.Open();
            SqlDataAdapter command = new SqlDataAdapter("Select * from Webpage_Design where PageName='HomePage'", Connection);
            DataSet ds = new DataSet();
            command.Fill(ds);
            string logo = ds.Tables[0].Rows[0][3].ToString();
            Logo.Style.Add(HtmlTextWriterStyle.BackgroundImage, "url(Image/" + logo + ")");
            Title.InnerHtml = ds.Tables[0].Rows[1][3].ToString();
            Menu.InnerHtml = ds.Tables[0].Rows[2][3].ToString();
            Footer.InnerHtml = ds.Tables[0].Rows[4][3].ToString();
            string background = ds.Tables[0].Rows[5][3].ToString();
            Background.Attributes["style"] = "background-image: linear-gradient(rgba(0,0,0,0.7),rgba(0,0,0,0.7)),url(Image/" + background + ")";
            Connection.Close();

            int root;
            if (Request.QueryString["Id"] == null)
                root = 0;
            else
                root = Convert.ToInt32(Request.QueryString["Id"].Substring(0, 1));

            switch (root)
            {
                case 1:
                    
                    Connection.Open();
                    SqlDataAdapter command2 = new SqlDataAdapter("Select * from Webpage_Design where PageName='RegistrationPage'", Connection);
                    DataSet ds2 = new DataSet();
                    command2.Fill(ds2);
                    BodyText.InnerHtml = ds2.Tables[0].Rows[0][3].ToString();
                    Footer.Style.Add(HtmlTextWriterStyle.MarginTop, "38vh");
                    Connection.Close();

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "clientscript", "$('#regno').change(function () { window.location.href = 'MasterPage.aspx?Id=1&regno=' + document.getElementById('regno').value; });", true);
                    //enter data into database

                    if (Request.Form["regno"] != null && Request.Form["fname"] != null && Request.Form["lname"] != null && Request.Form["gender"] != null && Request.Form["dob"] != null && Request.Form["email"] != null && Request.Form["contectno"] != null && Request.Form["address"] != null && Request.Files["file"] != null)
                    {
                        string RegNo = Request.Form["regno"];
                        string FirstName = Request.Form["fname"];
                        string LastName = Request.Form["lname"];
                        string Gender = Request.Form["gender"];
                        string DateofBirth = Request.Form["dob"];
                        string Email = Request.Form["email"];
                        string ContectNo = Request.Form["contectno"];
                        string Address = Request.Form["address"];
                        HttpPostedFile postedFile = Request.Files["file"];
                        Stream stream = postedFile.InputStream;
                        BinaryReader binaryReader = new BinaryReader(stream);
                        byte[] data = binaryReader.ReadBytes((int)stream.Length);

                        if (Convert.ToInt64(Request.Form["regno"].ToString()) >= 3060820001 && Convert.ToInt64(Request.Form["regno"].ToString()) <= 3060820050)
                        {
                            string StoredProcedure = "";
                            Connection.Open();
                            SqlCommand cmd1 = new SqlCommand("select * from Student where Regno=" + RegNo, Connection);
                            SqlDataReader sqlData = cmd1.ExecuteReader();
                            while (sqlData.Read())
                            {
                                StoredProcedure = "updateStudentData";
                            }
                            if (StoredProcedure == "")
                                StoredProcedure = "insertStudentData";
                            Connection.Close();

                            Connection.Open();
                            SqlCommand cmd = new SqlCommand(StoredProcedure, Connection);
                            cmd.CommandType = CommandType.StoredProcedure;

                            SqlParameter regno = new SqlParameter()
                            {
                                ParameterName = "@RegNo",
                                Value = RegNo
                            };
                            cmd.Parameters.Add(regno);
                            SqlParameter firstname = new SqlParameter()
                            {
                                ParameterName = "@FName",
                                Value = FirstName
                            };
                            cmd.Parameters.Add(firstname);
                            SqlParameter lastname = new SqlParameter()
                            {
                                ParameterName = "@LName",
                                Value = LastName
                            };
                            cmd.Parameters.Add(lastname);
                            SqlParameter gender = new SqlParameter()
                            {
                                ParameterName = "@Gender",
                                Value = Gender
                            };
                            cmd.Parameters.Add(gender);
                            SqlParameter dateofbirth = new SqlParameter()
                            {
                                ParameterName = "@DOB",
                                Value = DateofBirth
                            };
                            cmd.Parameters.Add(dateofbirth);
                            SqlParameter email = new SqlParameter()
                            {
                                ParameterName = "@Email",
                                Value = Email
                            };
                            cmd.Parameters.Add(email);
                            SqlParameter contectno = new SqlParameter()
                            {
                                ParameterName = "@ContectNo",
                                Value = ContectNo
                            };
                            cmd.Parameters.Add(contectno);
                            SqlParameter address = new SqlParameter()
                            {
                                ParameterName = "@Address",
                                Value = Address
                            };
                            cmd.Parameters.Add(address);
                            SqlParameter picture = new SqlParameter()
                            {
                                ParameterName = "@Picture",
                                Value = data
                            };
                            cmd.Parameters.Add(picture);

                            cmd.ExecuteNonQuery();

                            if (StoredProcedure == "insertStudentData")
                            {
                                using (MailMessage msg = new MailMessage("PrincipalofAIT@gmail.com", Email))
                                {
                                    msg.Subject = "Registration";
                                    msg.Body = "<h2 style='color:black; font-family: Times New Roman; font-weight: bold'>Your Registration Completed Successfully ..</h2>";
                                    msg.IsBodyHtml = true;
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

                                ClientScript.RegisterClientScriptBlock(this.GetType(), "baps", "swal('Success ..','Your Data Submitted Successfully ..','success')", true);
                            }
                            else
                            {
                                using (MailMessage msg = new MailMessage("PrincipalofAIT@gmail.com", Email))
                                {
                                    msg.Subject = "Update Details";
                                    msg.Body = "<h2 style='color:black; font-family: Times New Roman; font-weight: bold'>Your Information Updated Successfully ..</h2>";
                                    msg.IsBodyHtml = true;
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

                                ClientScript.RegisterClientScriptBlock(this.GetType(), "baps", "swal('Success ..','Your Data Updated Successfully ..','success')", true);
                            }
                        }
                        else
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "baps", "swal('Incorrect !!','Your Data is Incorrect !!','error')", true);

                        Connection.Close();
                    }
                    else if (Request.QueryString["regno"] != null)
                    {
                        if (Convert.ToInt64(Request.QueryString["regno"].ToString()) >= 3060820001 && Convert.ToInt64(Request.QueryString["regno"].ToString()) <= 3060820050)
                        {
                            long regno = Convert.ToInt64(Request.QueryString["regno"]);
                            Connection.Open();
                            SqlCommand cmd = new SqlCommand("Select * from Student where RegNo=" + regno, Connection);
                            SqlDataReader sqlData = cmd.ExecuteReader();
                            if (sqlData.Read())
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "clientscript1", "document.getElementById('regno').setAttribute('value', '" + sqlData[0] + "');", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "clientscript11", "document.getElementById('regno').setAttribute('readonly', true);", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "clientscript2", "document.getElementById('fname').setAttribute('value', '" + sqlData[1] + "');", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "clientscript3", "document.getElementById('lname').setAttribute('value', '" + sqlData[2] + "');", true);
                                if (sqlData[3].ToString() == "male")
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "clientscript4", "document.getElementById('male').setAttribute('checked', 'true');", true);
                                else
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "clientscript4", "document.getElementById('female').setAttribute('checked', 'true');", true);
                                DateTime dt = DateTime.ParseExact(sqlData[4].ToString(), "dd-MM-yyyy hh:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "clientscript5", "document.getElementById('dob').setAttribute('value', '" + dt.ToString("yyyy-MM-dd") + "');", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "clientscript6", "document.getElementById('email').setAttribute('value', '" + sqlData[5] + "');", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "clientscript7", "document.getElementById('contectno').setAttribute('value', '" + sqlData[6] + "');", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "clientscript8", "document.getElementById('address').value = '" + sqlData[7] + "';", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "clientscript9", "document.getElementById('file').setAttribute('value', '" + sqlData[8] + "');", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "clientscript10", "document.getElementById('Submit').value = '" + "Update" + "';", true);
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "clientscript1", "document.getElementById('regno').setAttribute('value', '" + regno + "');", true);
                            }
                            Connection.Close();
                        }
                        else
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "baps", "swal('Incorrect !!','Your Data is Incorrect !!','error')", true);
                    }
                    
                    break;

                case 2:

                    Connection.Open();
                    SqlDataAdapter command3 = new SqlDataAdapter("Select * from Webpage_Design where PageName='AcademicPerformancePage'", Connection);
                    DataSet ds3 = new DataSet();
                    command3.Fill(ds3);
                    BodyText.InnerHtml = ds3.Tables[0].Rows[0][3].ToString();
                    Footer.Style.Add(HtmlTextWriterStyle.MarginTop, "118vh");
                    Connection.Close();

                    if (Request.Form["regno"] != null)
                    {
                        Connection.Open();
                        SqlCommand cmd = new SqlCommand("Select * from Student where RegNo=" + Convert.ToInt64(Request.Form["regno"].ToString()), Connection);
                        SqlDataReader sqlData  = cmd.ExecuteReader();
                        if (sqlData.Read() == true)
                            Response.Redirect("Academic Performance Report.aspx?Id=" + Request.Form["regno"]);
                        else
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "baps", "swal('Incorrect !!','Your are Not Register !!','error')", true);
                        Connection.Close(); 
                    }

                    break;

                default:
                    
                    Connection.Open();
                    SqlDataAdapter command1 = new SqlDataAdapter("Select * from Webpage_Design where PageName='HomePage'", Connection);
                    DataSet ds1 = new DataSet();
                    command1.Fill(ds1);
                    BodyText.InnerHtml = ds1.Tables[0].Rows[3][3].ToString();
                    Connection.Close();

                    break;
            }
        }
    }
}