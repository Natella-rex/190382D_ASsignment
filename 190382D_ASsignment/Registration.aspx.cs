using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions; // for Regular expression
using System.Drawing; // for change of color
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace _190382D_ASsignment
{
    public partial class Registration : System.Web.UI.Page
    {
        string ASsignmentDBConnectionString =
        System.Configuration.ConfigurationManager.ConnectionStrings["ASsignmentDBConnection"].ConnectionString;
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;
        protected void Page_Load(object sender, EventArgs e)
        {
            lbl_regpwdchk.Visible = false;
            lbl_regerrmsg.Visible = false;
            lbl_regjsonmsg.Visible = false;
            btn_checkPassword.Visible = false;
        }

        //Password Checker
        //protected void btn_checkPassword_Click(object sender, EventArgs e)
        //{
        //    //if (ValidateCaptcha()) //Captcha V3
        //    //{
        //    // implement codes for the button event
        //    // Extract data from textbox
            
        //    if (tb_regpwd.Text == "")
        //    {
        //        lbl_regpwdchk.Visible = false;
        //    }
        //    else if (tb_regpwd.Text == tb_regcfmpwd.Text)
        //    {
        //        lbl_regpwdchk.Visible = true;
        //        int scores = checkPassword(tb_regpwd.Text);
        //        string status = "";
        //        switch (scores)
        //        {
        //            case 1:
        //                status = "Very Weak";
        //                break;
        //            case 2:
        //                status = "Weak";
        //                break;
        //            case 3:
        //                status = "Medium";
        //                break;
        //            case 4:
        //                status = "Strong";
        //                break;
        //            case 5:
        //                status = "Excellent";
        //                break;
        //            default:
        //                break;
        //        }
        //        lbl_regpwdchk.Text = "Status : " + status;
        //        if (scores < 4)
        //        {
        //            lbl_regpwdchk.ForeColor = Color.Red;
        //            return;
        //        }
        //        lbl_regpwdchk.ForeColor = Color.Green;
        //    }
        //    else
        //    {
        //        lbl_regpwdchk.Visible = true;
        //        lbl_regpwdchk.Text = "Passwords must match!";
        //    }
        //    //}
        //}

        ////Captcha V3
        //public class MyObject
        //{
        //    public string success { get; set; }
        //    public List<string> ErrorMessage { get; set; }
        //}
        ////Captcha V3
        //public bool ValidateCaptcha()
        //{
        //    bool result = true;

        //    //When user submits the recaptcha form, the user gets a response POST parameter. 
        //    //captchaResponse consist of the user click pattern. Behaviour analytics! AI :)s
        //    string captchaResponse = Request.Form["g-recaptcha-response"];
        //    System.Diagnostics.Debug.WriteLine(captchaResponse);

        //    //To send a GET request to Google along with the response and Secret key.
        //    HttpWebRequest req = (HttpWebRequest)WebRequest.Create
        //        ("https://www.google.com/recaptcha/api/siteverify?secret=Server-Site-Key &response=" + captchaResponse);

        //    try
        //    {
        //        //Codes to receive the Response in JSON format from Google Server
        //        using (WebResponse wResponse = req.GetResponse())
        //        {
        //            using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
        //            {
        //                //The response in JSON format
        //                string jsonResponse = readStream.ReadToEnd();

        //                //To show the JSON response string for learning purpose
        //                lbl_regjsonmsg.Visible = true;
        //                lbl_regjsonmsg.Text = "JSON Message: " + jsonResponse.ToString();

        //                JavaScriptSerializer js = new JavaScriptSerializer();

        //                //Create jsonObject to handle the response e.g success or error
        //                //Deserialize Json
        //                MyObject jsonObject = js.Deserialize<MyObject>(jsonResponse);

        //                //Convert the string "False" to bool false or "True" to bool true
        //                result = Convert.ToBoolean(jsonObject.success);//
        //            }
        //        }
        //        return result;
        //    }
        //    catch (WebException ex)
        //    {
        //        throw ex;
        //    }
        //}
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

        protected void btn_reg_Click(object sender, EventArgs e)
        {
            if (tb_regpwd.Text == "")
            {
                lbl_regpwdchk.Visible = false;
            }
            else if (tb_regpwd.Text == tb_regcfmpwd.Text)
            {
                lbl_regpwdchk.Visible = true;
                int scores = checkPassword(tb_regpwd.Text);
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
                lbl_regpwdchk.Text = "Status : " + status;
                if (scores < 4)
                {
                    lbl_regpwdchk.ForeColor = Color.Red;
                    return;
                }
                lbl_regpwdchk.ForeColor = Color.Green;
            }
            else
            {
                lbl_regpwdchk.Visible = true;
                lbl_regpwdchk.Text = "Passwords must match!";
            }
            if (string.IsNullOrWhiteSpace(tb_regfirst.Text) && string.IsNullOrWhiteSpace(tb_regfirst.Text))
            {
                if (string.IsNullOrWhiteSpace(tb_regid.Text)){
                    if (string.IsNullOrWhiteSpace(tb_regdob.Text)){
                        if (string.IsNullOrWhiteSpace(tb_regpwd.Text)){
                            if (string.IsNullOrWhiteSpace(tb_regcfmpwd.Text)){
                                if (string.IsNullOrWhiteSpace(tb_regccname.Text)){
                                    if (string.IsNullOrWhiteSpace(tb_regcc.Text)){
                                        if (string.IsNullOrWhiteSpace(tb_regcvv.Text)){
                                            if (string.IsNullOrWhiteSpace(tb_regexp.Text)){
                                                lbl_regerrmsg.Visible = true;
                                                lbl_regerrmsg.Text = "Please don't leave any blanks!*";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                //Regex email = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                Regex email = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                RegexOptions.CultureInvariant | RegexOptions.Singleline);
                bool isValidEmail = email.IsMatch(tb_regid.Text);
                if (!isValidEmail)
                {
                    lbl_regerrmsg.Text = "Email is invalid";
                }
                else
                {
                    Regex card = new Regex(@"^.{16,19}$");
                    bool isValidCC = card.IsMatch(tb_regcc.Text);
                    Regex exp = new Regex(@"^\d{2}\/\d{2}$");
                    bool isValidExp = exp.IsMatch(tb_regexp.Text);
                    System.Diagnostics.Debug.WriteLine(tb_regexp.Text);

                    Regex cvv = new Regex(@"^.{3,4}$");
                    bool isValidCVV = cvv.IsMatch(tb_regcvv.Text);
                    SqlConnection connection = new SqlConnection(ASsignmentDBConnectionString);
                    SqlCommand cmd = new SqlCommand("select count(Email) from Account where Email = @email", connection);
                    cmd.Parameters.AddWithValue("@Email", tb_regid.Text);
                    try
                    {
                        connection.Open();
                       
                        int count = (int)cmd.ExecuteScalar();
                        if(count > 0)
                        {
                            lbl_regerrmsg.Visible = true;
                            lbl_regerrmsg.Text = "Email has already been registered";
                        }
                       
                            
                        
                        else if (!isValidCC)
                        {
                            lbl_regerrmsg.Visible = true;

                            lbl_regerrmsg.Text = "Credit card is invalid";
                        }
                            
                                
                        else if (!isValidExp)
                        {
                            lbl_regerrmsg.Visible = true;

                            lbl_regerrmsg.Text = "Expiry Date is invalid";
                        }

                                
                                    
                        else if (!isValidCVV)
                        {
                            lbl_regerrmsg.Visible = true;

                            lbl_regerrmsg.Text = "CVV is invalid";
                        }
                        else
                        {
                            //Crypto
                            //string pwd = get value from your Textbox
                            string pwd = tb_regpwd.Text.ToString().Trim(); ;
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
                            createAccount();
                            foreach (Control control in Page.Controls)
                            {
                                if (control is TextBox)
                                {
                                    TextBox txt = (TextBox)control;
                                    txt.Text = "";
                                }
                            }
                            Session["changepwdtime"] = DateTime.Now;
                            lbl_regerrmsg.Visible = true;
                            lbl_regerrmsg.ForeColor = Color.Green;
                            lbl_regerrmsg.Text = "Account Successfully Registered!";
                            //System.Threading.Thread.Sleep(5000);
                            Response.Redirect("Login.aspx", false);

                        }
                                
                                
                            
                        
                       
                        
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.ToString());
                    }
                    finally { connection.Close(); }
                    

                }
            }
        }
        //Crypto
        public void createAccount()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ASsignmentDBConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Account VALUES(@Fname, @Lname, @Email, @Dob, @PasswordHash, @PasswordSalt, @CCName, @CreditCard, @Cvv, @Expiry, @IV, @Key)"))
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@Fname", tb_regfirst.Text.Trim());
                            cmd.Parameters.AddWithValue("@Lname", tb_reglast.Text.Trim());
                            cmd.Parameters.AddWithValue("@Email", tb_regid.Text.Trim());
                            cmd.Parameters.AddWithValue("@Dob", tb_regdob.Text.Trim());
                            cmd.Parameters.AddWithValue("@PasswordHash", finalHash);
                            cmd.Parameters.AddWithValue("@PasswordSalt", salt);
                            cmd.Parameters.AddWithValue("@CCName", tb_regccname.Text.Trim());
                            cmd.Parameters.AddWithValue("@CreditCard", Convert.ToBase64String(encryptData(tb_regcc.Text.Trim())));
                            cmd.Parameters.AddWithValue("@Cvv", Convert.ToBase64String(encryptData(tb_regcvv.Text.Trim())));
                            cmd.Parameters.AddWithValue("@Expiry", Convert.ToBase64String(encryptData(tb_regexp.Text.Trim())));
                            cmd.Parameters.AddWithValue("@IV", Convert.ToBase64String(IV));
                            cmd.Parameters.AddWithValue("@Key", Convert.ToBase64String(Key));
                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();
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
    }
}