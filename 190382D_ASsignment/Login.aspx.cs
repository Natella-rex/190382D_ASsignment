using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace _190382D_ASsignment
{
    public partial class Login : System.Web.UI.Page
    {
        string ASsignmentDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ASsignmentDBConnection"].ConnectionString;
        static string finalHash;
        static string salt;
        protected void Page_Load(object sender, EventArgs e)
        {
            lbl_lgmsg.Visible = false;
            lbl_lgjsonmsg.Visible = false;
        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            //Crypto
            string pwd = tb_logpwd.Text.ToString().Trim();
            string userid = tb_logid.Text.ToString().Trim();

            SHA512Managed hashing = new SHA512Managed();
            string dbHash = getDBHash(userid);
            string dbSalt = getDBSalt(userid);

            try
            {
                if (dbSalt != null && dbSalt.Length > 0 && dbHash != null && dbHash.Length > 0)
                {
                    string pwdWithSalt = pwd + dbSalt;
                    byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                    string userHash = Convert.ToBase64String(hashWithSalt);

                    if (userHash.Equals(dbHash))
                    {
                        Session["UserID"] = userid;
                        //Session Fixation
                        Session["LoggedIn"] = HttpUtility.HtmlEncode(tb_logid.Text.Trim());
                        
                        //creates a new GUID and save into the session
                        string guid = Guid.NewGuid().ToString();
                        Session["AuthToken"] = guid;

                        //now create a new cookie with this guid value
                        Response.Cookies.Add(new HttpCookie("AuthToken", guid));

                        Response.Redirect("HomePage.aspx", false);
                    }
                    else
                    {
                        //lbl_lgmsg.Visible = true;
                        //lbl_lgmsg.Text = "Userid or password is not valid. Please try again.";
                        Response.Redirect("Login.aspx", false);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }

            if (ValidateCaptcha()) //Captcha V3
            {
                if (tb_logid.Text == "" && tb_logpwd.Text == "")
                {
                    lbl_lgmsg.Visible = true;
                    lbl_lgmsg.Text = "Please enter your email/user id and password";
                }

                else if (tb_logpwd.Text == "")
                {
                    lbl_lgmsg.Visible = true;
                    lbl_lgmsg.Text = "Please enter your password";
                }
                else if (tb_logid.Text == "")
                {
                    lbl_lgmsg.Visible = true;
                    lbl_lgmsg.Text = "Please enter your email/user id";
                }

                //XSS Prevention
                // lbl_comments.Text = HttpUtility.HtmlEncode(tb_logid.Text); create lbl on login page to work
                //Response.Redirect("XSSDisplay.aspx?Comment=" + HttpUtility.UrlEncode(HttpUtility.HtmlEncode(tb_logid.Text)) + HttpUtility.UrlEncode(HttpUtility.HtmlEncode(tb_logpwd.Text)));

                else
                {
                    lbl_lgmsg.Visible = true;
                    lbl_lgmsg.Text = "Wrong username or password";
                }
            }
        }

        //Crypto
        protected string getDBHash(string userid)
        {
            string h = null;
            SqlConnection connection = new SqlConnection(ASsignmentDBConnectionString);
            string sql = "select PasswordHash FROM Account WHERE Email=@USERID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@USERID", userid);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        if (reader["PasswordHash"] != null)
                        {
                            if (reader["PasswordHash"] != DBNull.Value)
                            {
                                h = reader["PasswordHash"].ToString();
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return h;
        }
        protected string getDBSalt(string userid)
        {
            string s = null;
            SqlConnection connection = new SqlConnection(ASsignmentDBConnectionString);
            string sql = "select PASSWORDSALT FROM ACCOUNT WHERE Email=@USERID";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@USERID", userid);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["PASSWORDSALT"] != null)
                        {
                            if (reader["PASSWORDSALT"] != DBNull.Value)
                            {
                                s = reader["PASSWORDSALT"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return s;
        }

        //Captcha V3
        public class MyObject
        {
            public string success { get; set; }
            public List<string> ErrorMessage { get; set; }
        }
        //Captcha V3
        public bool ValidateCaptcha()
        {
            bool result = true;

            //When user submits the recaptcha form, the user gets a response POST parameter. 
            //captchaResponse consist of the user click pattern. Behaviour analytics! AI :)s
            string captchaResponse = Request.Form["g-recaptcha-response"];

            //To send a GET request to Google along with the response and Secret key.
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create
                ("https://www.google.com/recaptcha/api/siteverify?secret=6LfMnz4aAAAAAOp12_GnE1idKrIzJyD589GmxpoW &response=" + captchaResponse);

            try
            {
                //Codes to receive the Response in JSON format from Google Server
                using (WebResponse wResponse = req.GetResponse())
                {
                    using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                    {
                        //The response in JSON format
                        string jsonResponse = readStream.ReadToEnd();

                        //To show the JSON response string for learning purpose
                        lbl_lgjsonmsg.Visible = false;
                        lbl_lgjsonmsg.Text = "JSON Message: " + jsonResponse.ToString();

                        JavaScriptSerializer js = new JavaScriptSerializer();

                        //Create jsonObject to handle the response e.g success or error
                        //Deserialize Json
                        MyObject jsonObject = js.Deserialize<MyObject>(jsonResponse);

                        //Convert the string "False" to bool false or "True" to bool true
                        result = Convert.ToBoolean(jsonObject.success);//
                    }
                }
                return result;
            }
            catch(WebException ex)
            {
                throw ex;
            }
        }

    }

}