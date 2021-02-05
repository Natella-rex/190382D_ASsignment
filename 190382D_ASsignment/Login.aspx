<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="_190382D_ASsignment.Login" ValidateRequest = "false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SITConnect</title>
    <%--Captcha V3--%>
    <script src="https://www.google.com/recaptcha/api.js?render=6LfMnz4aAAAAAKDqjbmW6_kjSVyHv-Lxy92JskZ4"></script>
    <link href="Stylesheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            margin-left: 0px;
        }
        .auto-style3 {
            width: 197px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
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
        <div class="container">
            <h2>SIT Connect- Login</h2>
            <asp:Label ID="lbl_lgmsg" runat="server" Text="Login Error Message" ForeColor="Red" Visible="False"></asp:Label>
            <br />
            <asp:Label ID="lbl_lgjsonmsg" runat="server" Text="Login JSON Message" ForeColor="Blue" Visible="False"></asp:Label>
            <br />
            <br />
            <table class="auto-style1">
                <tr>
                    <td class="auto-style3">User ID/Email</td>
                    <td>
                        <asp:TextBox ID="tb_logid" runat="server" Width="275px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">Password</td>
                    <td>
                        <asp:TextBox ID="tb_logpwd" runat="server" CssClass="auto-style2" Width="200px" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">&nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style3">&nbsp;</td>
                    <td>
                        <asp:Button ID="btn_login" runat="server" Height="35px" Text="Login" Width="100px" BackColor="#CCA9EB" ForeColor="White" OnClick="btn_login_Click" />
                    </td>
                </tr>
            </table>
            <input type="hidden" id="g-recaptcha-response" name="g-recaptcha-response"/>
        </div>
    </form>
    <script>
        grecaptcha.ready(function () {
            grecaptcha.execute('6LfMnz4aAAAAAKDqjbmW6_kjSVyHv-Lxy92JskZ4', { action: 'Login' }).then(function (token) {
                document.getElementById("g-recaptcha-response").value = token;
            });
        });
    </script>
</body>
</html>