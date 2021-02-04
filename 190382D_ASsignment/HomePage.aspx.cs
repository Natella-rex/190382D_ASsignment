using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;

namespace _190382D_ASsignment
{
    public partial class HomePage : System.Web.UI.Page
    {
        //Crypto
        string ASsignmentDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ASsignmentDBConnection"].ConnectionString;
        string userID = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoggedIn"] != null && Session["AuthToken"] != null && Request.Cookies["AuthToken"] != null)
            {
                if (!Session["AuthToken"].ToString().Equals(Request.Cookies["AuthToken"].Value))
                {
                    Response.Redirect("Login.aspx", false);
                }
                else
                {
                    if (Session["UserID"] != null)
                    {
                        //Session fixation
                        lbl_HPMessage.Text = "Welcome, you are logged in!";
                        lbl_HPMessage.ForeColor = System.Drawing.Color.Green;
                        btn_logout.Visible = true;

                        //Crypto
                        userID = (string)Session["UserID"];

                        displayUserProfile(userID);
                        if (Session["changepwdtime"] != null)
                        {
                            DateTime currentTime = DateTime.Now;

                            DateTime prevTime = Convert.ToDateTime(Session["changepwdtime"]);
                            int differenceTime = currentTime.Subtract(prevTime).Minutes;

                            if (differenceTime >= 15)
                            {
                                //lbl_HPMessage.Visible = true;
                                //lbl_HPMessage.Text = "Please change your password! You will be redirected in 5 secs...";
                                //System.Threading.Thread.Sleep(5000);
                                Response.Redirect("ChangePassword.aspx", false);
                            }
                        }

                    }

                }
            }
            else
            {
                Response.Redirect("Login.aspx", false);
            }
        }

        //Crypto
        protected void displayUserProfile(string userid)
        {
            SqlConnection connection = new SqlConnection(ASsignmentDBConnectionString);
            string sql = "SELECT * FROM Account WHERE Email=@userId";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@userId", userid);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["Email"] != DBNull.Value)
                        {
                            lbl_hpname.Text = reader["Fname"].ToString() + " " + reader["Lname"].ToString();
                            lbl_hpemail.Text = reader["Email"].ToString();
                            DateTime bdate = Convert.ToDateTime(reader["DateOfBirth"]);
                            lbl_hpdob.Text = bdate.ToShortDateString();
                        }
                    }
                }
            }//try
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                connection.Close();
            }
        }

        protected void btn_logout_Click(object sender, EventArgs e)
        {
            //whole session only, not cookies, everytime the user logs out
            Session.Clear(); //remove var stored in session so user cant access to prev sessionID
            Session.Abandon(); //remove var stored in session, user gets new session
            Session.RemoveAll();//Deletes the storage area too 

            Response.Redirect("Login.aspx", false);

            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
            }

            if (Request.Cookies["AuthToken"] != null)
            {
                Response.Cookies["AuthToken"].Value = string.Empty;
                Response.Cookies["AuthToken"].Expires = DateTime.Now.AddMonths(-20);
            }
        }
    }
}