using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Academic_Performance
{
    public partial class MarksUploader : System.Web.UI.Page
    {
        SqlConnection Connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Academic_PerformanceConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void submitbtn_Click(object sender, EventArgs e)
        {
            marksuploader.SaveAs(Server.MapPath(@"~/StudentMarks\") + Path.GetFileName(marksuploader.PostedFile.FileName));
            string[] line = File.ReadAllLines(Server.MapPath(@"~/StudentMarks\") + Path.GetFileName(marksuploader.PostedFile.FileName));
            string[][] data = new string[line.Length][];
            int i = 0;
            while (i < line.Length)
            {       
                data[i] = line[i].Split(',');
                i++;
            }

            foreach (var item in data)
            {
                //Connection.Open();
                //SqlCommand cmd = new SqlCommand("insert into Marks values(" + Convert.ToInt64(item[0].ToString()) + ",'" + item[1].ToString() + "'," + Convert.ToDouble(item[2].ToString()) + "," + Convert.ToDouble(item[3].ToString()) + "," + Convert.ToDouble(item[4].ToString()) + "," + Convert.ToDouble(item[5].ToString()) + "," + Convert.ToDouble(item[6].ToString()) + "," + Convert.ToDouble(item[7].ToString()) + "," + Convert.ToDouble(item[8].ToString()) + "," + Convert.ToDouble(item[9].ToString()) + ")", Connection);
                //cmd.ExecuteNonQuery();
                //Connection.Close();

                Connection.Open();
                SqlCommand cmd1 = new SqlCommand("select * from Marks where RegNo=" + Convert.ToInt64(item[0].ToString()), Connection);
                SqlDataReader sqlData = cmd1.ExecuteReader();
                string ParentsEmail = "";
                double AIT_311 = 0.0;
                double AIT_312 = 0.0;
                double AIT_313 = 0.0;
                double AIT_314 = 0.0;
                double AIT_315 = 0.0;
                double AGRI_311 = 0.0;
                double AGRI_312 = 0.0;
                double AGRI_313 = 0.0;
                while (sqlData.Read())
                {
                    AIT_311 = (Convert.ToDouble(sqlData[2]) + Convert.ToDouble(item[2])) / 2;
                    AIT_312 = (Convert.ToDouble(sqlData[3]) + Convert.ToDouble(item[3])) / 2;
                    AIT_313 = (Convert.ToDouble(sqlData[4]) + Convert.ToDouble(item[4])) / 2;
                    AIT_314 = (Convert.ToDouble(sqlData[5]) + Convert.ToDouble(item[5])) / 2;
                    AIT_315 = (Convert.ToDouble(sqlData[6]) + Convert.ToDouble(item[6])) / 2;
                    AGRI_311 = (Convert.ToDouble(sqlData[7]) + Convert.ToDouble(item[7])) / 2;
                    AGRI_312 = (Convert.ToDouble(sqlData[8]) + Convert.ToDouble(item[8])) / 2;
                    AGRI_313 = (Convert.ToDouble(sqlData[9]) + Convert.ToDouble(item[9])) / 2;
                }
                ParentsEmail = item[1].ToString();
                Connection.Close();

                Connection.Open();
                SqlCommand cmd2 = new SqlCommand("update Marks set Parents_Email='" + ParentsEmail.ToString() + "', AIT_311=" + AIT_311.ToString() + ",AIT_312=" + AIT_312.ToString() + ",AIT_313=" + AIT_313.ToString() + ",AIT_314=" + AIT_314.ToString() + ",AIT_315=" + AIT_315.ToString() + ",AGRI_311=" + AGRI_311.ToString() + ",AGRI_312=" + AGRI_312.ToString() + ",AGRI_313=" + AGRI_313.ToString() + " where RegNo=" + item[0].ToString(), Connection);
                cmd2.ExecuteNonQuery();
                Connection.Close();
            }

        }
    }
}