using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace Academic_Performance
{
    public partial class Design : System.Web.UI.Page
    {
        SqlConnection Connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Academic_PerformanceConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(IsPostBack))
            {
                SqlDataAdapter sqlData = new SqlDataAdapter("Select Distinct(PageName) from Webpage_Design", Connection);
                DataSet ds = new DataSet();
                sqlData.Fill(ds);
                drppage.DataTextField = "PageName";
                drppage.DataSource = ds;
                drppage.DataBind();
                drppage.Items.Insert(0, new ListItem("Select"));
                drppagecontent.Items.Insert(0, new ListItem("Select"));
            }
        }

        protected void drppage_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlDataAdapter sqlData = new SqlDataAdapter("Select PageContentName from WebPage_Design where PageName='" + drppage.SelectedItem.ToString() + "'", Connection);
            DataSet ds = new DataSet();
            sqlData.Fill(ds);
            drppagecontent.Items.Clear();
            drppagecontent.DataTextField = "PageContentName";
            drppagecontent.DataSource = ds;
            drppagecontent.DataBind();
            drppagecontent.Items.Insert(0, new ListItem("Select"));
        }

        protected void drppagecontent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drppagecontent.SelectedItem.ToString().IndexOf("Select") == -1)
            {
                if (drppagecontent.SelectedItem.ToString().IndexOf("Image") != -1)
                {
                    ckeditorlbl.Visible = false;
                    CKEditorControl1.Visible = false;
                    fileuploadlbl.Visible = true;
                    imageuploader.Visible = true;
                }
                else
                {
                    fileuploadlbl.Visible = false;
                    imageuploader.Visible = false;
                    ckeditorlbl.Visible = true;
                    CKEditorControl1.Visible = true;

                    Connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter("Select * from Webpage_Design where PageName='" + drppage.SelectedItem.ToString() + "' and PageContentName='" + drppagecontent.SelectedItem.ToString() + "'", Connection);
                    DataSet ds = new DataSet();
                    command.Fill(ds);
                    CKEditorControl1.Text = ds.Tables[0].Rows[0][3].ToString();
                    Connection.Close();
                }
            }
            else
            {
                fileuploadlbl.Visible = false;
                imageuploader.Visible = false;
                ckeditorlbl.Visible = false;
                CKEditorControl1.Visible = false;
            }
        }

        protected void submitbtn_Click(object sender, EventArgs e)
        {
            if (drppagecontent.SelectedItem.ToString().IndexOf("Image") != -1)
            {
                if (drppagecontent.SelectedItem.ToString() == "Report BackGround Image")
                {
                    HttpPostedFile postedFile = imageuploader.PostedFile;
                    Stream stream = postedFile.InputStream;
                    BinaryReader reader = new BinaryReader(stream);
                    byte[] bytes = reader.ReadBytes((int)stream.Length);

                    Connection.Open();
                    SqlCommand command = new SqlCommand("uploadAcademicPerformanceReportBackGroundImage", Connection);
                    SqlParameter Image = new SqlParameter()
                    {
                        ParameterName = "@Picture",
                        Value = bytes
                    };
                    command.Parameters.Add(Image);
                    command.CommandType = CommandType.StoredProcedure;
                    command.ExecuteNonQuery();
                    Connection.Close();
                }
                else
                {
                    Connection.Open();
                    if (drppagecontent.SelectedItem.ToString() == "HomePage BackGround Image")
                    {
                        string HomePageBackGroundImageFile = imageuploader.FileName.ToString().Replace(" ", "");
                        SqlCommand command = new SqlCommand("update Webpage_Design set PageContent='" + HomePageBackGroundImageFile + "' where Design_Id=6", Connection);
                        command.ExecuteNonQuery();

                        imageuploader.SaveAs(Server.MapPath(@"~/Image\" + HomePageBackGroundImageFile));
                    }
                    else
                    {
                        string LogoImageFile = imageuploader.FileName.ToString().Replace(" ", "");
                        SqlCommand command = new SqlCommand("update Webpage_Design set PageContent='" + LogoImageFile + "' where Design_Id=1", Connection);
                        command.ExecuteNonQuery();

                        imageuploader.SaveAs(Server.MapPath(@"~/Image\" + LogoImageFile));
                    }
                    Connection.Close(); 
                }
            }
            else
            {
                Connection.Open();
                SqlCommand command = new SqlCommand("Update Webpage_Design Set PageContent='" + CKEditorControl1.Text.ToString() + "' where PageName='" + drppage.SelectedItem.ToString() + "' and PageContentName='" + drppagecontent.SelectedItem.ToString() + "'", Connection);
                command.ExecuteNonQuery();
                Connection.Close();
            }
        }        
    }
}