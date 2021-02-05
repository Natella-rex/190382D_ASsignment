<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registration.aspx.cs" Inherits="_190382D_ASsignment.Registration" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SITConnect</title>
    <link href="Stylesheet.css" rel="stylesheet" type="text/css" />
     <script src='https://www.google.com/recaptcha/api.js'></script>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            height: 29px;
        }
        .auto-style3 {
            height: 29px;
            width: 211px;
        }
        .auto-style4 {
            width: 211px;
        }
        .auto-style5 {
            width: 211px;
            height: 26px;
        }
        .auto-style6 {
            height: 26px;
        }
        .auto-style7 {
            height: 29px;
            width: 179px;
        }
        .auto-style8 {
            width: 179px;
        }
        .auto-style9 {
            height: 26px;
            width: 179px;
        }
        .auto-style10 {
            height: 29px;
            width: 323px;
        }
        .auto-style11 {
            width: 323px;
            height: 26px;
        }
        .auto-style12 {
            width: 323px;
        }
    </style>
     <script src='https://www.google.com/recaptcha/api.js'></script>

    <%--Password checker--%>
    <script type="text/javascript">
        function validate() {
            var str = document.getElementById('<%=tb_regpwd.ClientID%>').value;

            if (str.length < 8) {
                document.getElementById("lbl_regpwdchk").innerHTML = "Password Length Must be at Least 8 Characters";
                document.getElementById("lbl_regpwdchk").style.color = "Red";
                return ("too_short");
            }
            else if (str.search(/[0-9]/) == -1) {
                document.getElementById("lbl_regpwdchk").innerHTML = "Password require at least 1 number";
                document.getElementById("lbl_regpwdchk").style.color = "Red";
                return ("no_number");
            }
            else if (str.search(/[A-Z]/) == -1) {
                document.getElementById("lbl_regpwdchk").innerHTML = "Password require at least 1 uppercase";
                document.getElementById("lbl_regpwdchk").style.color = "Red";
                return ("no_upper");
            }
            else if (str.search(/[a-z]/) == -1) {
                document.getElementById("lbl_regpwdchk").innerHTML = "Password require at least 1 lowercase";
                document.getElementById("lbl_regpwdchk").style.color = "Red";
                return ("no_lower");
            }
            else if (str.search(/[^A-Za-z0-9]/) == -1) {
                document.getElementById("lbl_regpwdchk").innerHTML = "Password require at least 1 special characters";
                document.getElementById("lbl_regpwdchk").style.color = "Red";
                return ("no_specialcharacters");
            }

            document.getElementById("lbl_regpwdchk").innerHTML = "Excellent!";
            document.getElementById("lbl_regpwdchk").style.color = "Blue";
        }
    </script>
   
</head>
<body>
    <header>
    <div class="container">
      <img src="Images/SITConnectlogo.png" alt="logo" class="logo" width="199" height="75" />
      <nav>
        <ul>
          <li><a href="Registration.aspx">Register</a></li>
          <li><a href="Login.aspx">Login</a></li>
        </ul>
      </nav>
    </div>
  </header>
    <form id="form1" runat="server">
        <div class="container">
            <br />
            <h2>SIT Connect- Account Registration</h2>
            <asp:Label ID="lbl_regerrmsg" runat="server" Text="Register Error Message" ForeColor="Red" Visible="False"></asp:Label>
            <br />
            <asp:Label ID="lbl_regjsonmsg" runat="server" Text="Register JSON Message" ForeColor="Blue" Visible="False"></asp:Label><br />
            <p>
                <table class="auto-style1">
                    <tr>
                        <td class="auto-style3">First Name</td>
                        <td class="auto-style10"> <asp:TextBox ID="tb_regfirst" runat="server" Width="200px"></asp:TextBox></td>
                        <td class="auto-style7"> Last Name</td>
                        <td class="auto-style2"> <asp:TextBox ID="tb_reglast" runat="server" Width="200px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="auto-style3">Email (UserID)</td>
                        <td class="auto-style10"> <asp:TextBox ID="tb_regid" runat="server" Width="275px"></asp:TextBox></td>
                        <td class="auto-style7"> Date Of Birth</td>
                        <td class="auto-style2"> <asp:TextBox ID="tb_regdob" runat="server" Width="200px" TextMode="Date"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="auto-style3">Password </td>
                        <td class="auto-style10"> <asp:TextBox ID="tb_regpwd" runat="server" Width="200px" TextMode="Password"></asp:TextBox></td>
                        <td class="auto-style7"> 
                            <asp:Label ID="lbl_regpwdchk" runat="server" Text="pwd checker" Visible="False" ForeColor="Red"></asp:Label><!-- onkeyup="javascript:validate()" auto check while typing -->
                        </td>
                        <td class="auto-style2"> </td>
                    </tr>
                    <tr>
                        <td class="auto-style3">Confirm Password</td>
                        <td class="auto-style10"> <asp:TextBox ID="tb_regcfmpwd" runat="server" Width="200px" TextMode="Password"></asp:TextBox></td>
                        <td class="auto-style7"> </td>
                        <td class="auto-style2"> </td>
                    </tr>
                    <tr>
                        <td class="auto-style3">&nbsp;</td>
                        <td class="auto-style10"> &nbsp;</td>
                        <td class="auto-style7"> 
                            &nbsp;</td>
                        <td class="auto-style2">  
                            <asp:Button ID="btn_checkPassword" runat="server" Text="Check Password" Width="200px" BackColor="White" ForeColor="#CCA9EB" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style3"></td>
                        <td class="auto-style10"> </td>
                        <td class="auto-style7"> </td>
                        <td class="auto-style2"> </td>
                    </tr>
                    <tr>
                        <td class="auto-style2" colspan="2"><h3>Credit Card Details</h3></td>
                        <td class="auto-style7"> &nbsp;</td>
                        <td class="auto-style2"> &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="auto-style5">Name on Credit Card</td>
                        <td class="auto-style11"> <asp:TextBox ID="tb_regccname" runat="server" Width="200px"></asp:TextBox></td>
                        <td class="auto-style9"> Credit Card</td>
                        <td class="auto-style6"> <asp:TextBox ID="tb_regcc" runat="server" Width="275px" TextMode="Number"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="auto-style4">CVV</td>
                        <td class="auto-style12"> <asp:TextBox ID="tb_regcvv" runat="server" Width="80px" TextMode="Number"></asp:TextBox></td>
                        <td class="auto-style8"> Expiry Date(MM/YY)</td>
                        <td> <asp:TextBox ID="tb_regexp" runat="server" Width="200px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="auto-style5"></td>
                        <td class="auto-style11"> </td>
                        <td class="auto-style9"> </td>
                        <td class="auto-style6"> </td>
                    </tr>
                    <tr>
                        <td class="auto-style4">&nbsp;</td>
                        <td class="auto-style12">
                            <asp:Button ID="btn_reg" runat="server" Height="37px" Text="Register" Width="175px" BackColor="#CCA9EB" ForeColor="White" OnClick="btn_reg_Click" />
                        </td>
                        <td class="auto-style8">
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                </table>
            </p>
        </div>
    </form>
</body>
</html>