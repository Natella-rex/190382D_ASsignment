using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions; // for Regular expression
using System.Drawing; // for change of color
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace _190382D_ASsignment
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        string ASsignmentDBConnectionString =
        System.Configuration.ConfigurationManager.ConnectionStrings["ASsignmentDBConnection"].ConnectionString;
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;
        protected void Page_Load(object sender, EventArgs e)
        {
            lbl_pwdmsg.Visible = false;
        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            if (Session["changepwdtime"] != null)
            {
                DateTime prevTime = Convert.ToDateTime(Session["changepwdtime"]);
                int differenceTime = currentTime.Subtract(prevTime).Minutes;
                
                if (differenceTime < 5)
                {
                    lbl_pwdmsg.Visible = true;
                    lbl_pwdmsg.Text = "You cannot change password again for another 5 minutes ";
                }
            }
            
           
            else
            {
                // implement codes for the button event
                // Extract data from textbox
                if (tb_newpwd.Text == "" || tb_cfmnewpwd.Text == "")
                {
                    lbl_pwdmsg.Visible = true;
                    lbl_pwdmsg.Text = "Please enter a password!";
                }
                else if (tb_newpwd.Text == tb_cfmnewpwd.Text)
                {
                    lbl_pwdmsg.Visible = true;
                    int scores = checkPassword(tb_newpwd.Text);
                    string status = "";
                    switch (scores)
                    {
                        case 1:
                            status = "Very Weak";
                            break;
                        case 2:
                            status = "Weak";
                            break;
                        case 3:
                            status = "Medium";
                            break;
                        case 4:
                            status = "Strong";
                            break;
                        case 5:
                            status = "Excellent";
                            break;
                        default:
                            break;
                    }
                    lbl_pwdmsg.Text = "Status : " + status;
                    if (scores < 3)
                    {
                        lbl_pwdmsg.ForeColor = Color.Red;
                        return;
                    }
                    else
                    {
                        lbl_pwdmsg.ForeColor = Color.Green;

                        //Crypto
                        //string pwd = get value from your Textbox
                        string pwd = tb_newpwd.Text.ToString().Trim(); ;
                        //Generate random "salt"
                        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                        byte[] saltByte = new byte[8];
                        //Fills array of bytes with a cryptographically strong sequence of random values.
                        rng.GetBytes(saltByte);
                        salt = Convert.ToBase64String(saltByte);
                        SHA512Managed hashing = new SHA512Managed();
                        string pwdWithSalt = pwd + salt;
                        byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwd));
                        byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                        finalHash = Convert.ToBase64String(hashWithSalt);
                        RijndaelManaged cipher = new RijndaelManaged();
                        cipher.GenerateKey();
                        Key = cipher.Key;
                        IV = cipher.IV;
                        Session["changepwdtime"] = DateTime.Now;
                        updateAccount();
                        foreach (Control control in Page.Controls)
                        {
                            if (control is TextBox)
                            {
                                TextBox txt = (TextBox)control;
                                txt.Text = "";
                                lbl_pwdmsg.Visible = true;
                                lbl_pwdmsg.ForeColor = Color.Green;
                                lbl_pwdmsg.Text = "Password Successfully Updated!";
                            }
                        }
                    }

                }
                else
                {
                    lbl_pwdmsg.Visible = true;
                    lbl_pwdmsg.Text = "Passwords must match!";
                }
            }

            
        }

        public void updateAccount()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ASsignmentDBConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE Account SET PasswordHash=@NPasswordHash, PasswordSalt=@NPasswordSalt where Email = @email"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@email", Session["LoggedIn"].ToString());
                            cmd.Parameters.AddWithValue("@NPasswordHash", finalHash);
                            cmd.Parameters.AddWithValue("@NPasswordSalt", salt);
                            cmd.Parameters.AddWithValue("@IV", Convert.ToBase64String(IV));
                            cmd.Parameters.AddWithValue("@Key", Convert.ToBase64String(Key));
                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
                            lbl_pwdmsg.Text = "Your password has been change successfully!";
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
        protected byte[] encryptData(string data)
        {
            byte[] cipherText = null;
            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = IV;
                cipher.Key = Key;
                ICryptoTransform encryptTransform = cipher.CreateEncryptor();
                //ICryptoTransform decryptTransform = cipher.CreateDecryptor();
                byte[] plainText = Encoding.UTF8.GetBytes(data);
                cipherText = encryptTransform.TransformFinalBlock(plainText, 0, plainText.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }
            return cipherText;
        }
        private int checkPassword(string password)
        {
            int score = 0;
            //include implementation here
            //score 0 very weak
            // if length of password is less than 8 chars
            if (password.Length < 8)
            {
                return 1;
            }
            //else if (password.Length > 12)
            //{
            //    return 6;
            //}
            else
            {
                score = 1;
            }
            //score 2 weak
            if (Regex.IsMatch(password, "[a-z]"))
            {
                score++;
            }
            //score 3 medium
            if (Regex.IsMatch(password, "[A-Z]"))
            {
                score++;
            }
            //score 4 strong
            if (Regex.IsMatch(password, "[0-9]"))
            {
                score++;
            }
            //score 5 Excellent
            if (Regex.IsMatch(password, "[^A-Za-z0-9]"))
            {
                score++;
            }
            return score;
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