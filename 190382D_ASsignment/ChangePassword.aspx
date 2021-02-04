<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="_190382D_ASsignment.ChangePassword" %>

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
            width: 240px;
        }
        .auto-style4 {
            width: 240px;
            height: 29px;
        }
        .auto-style5 {
            height: 29px;
        }
        </style>
</head>
<body>
    <header>
            <div class="container">
              <img src="Images/SITConnectlogo.png" alt="logo" class="logo" width="199" height="75" />
              <nav>
                <ul>
                    <li><a href="HomePage.aspx">Home</a></li>
                    <li><a href="ChangePassword.aspx">Change Password</a></li>
                    <li><asp:Button ID="btn_logout" runat="server" Text="Logout" Visible="true" BackColor="#CCA9EB" ForeColor="White" OnClick="btn_logout_Click"/></li>
                </ul>
              </nav>
            </div>
        </header>
    <form id="form1" runat="server">
        <div class="container">
            <h2>SIT Connect- Change Password</h2>
            <asp:Label ID="lbl_pwdmsg" runat="server" Text="Pwd Error Message" ForeColor="Red" Visible="False"></asp:Label>
            <br />
            <br />
            <table class="auto-style1">
                <tr>
                    <td class="auto-style4">New Password</td>
                    <td class="auto-style5">
                        <asp:TextBox ID="tb_newpwd" runat="server" Width="250px" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">Confirm New Password</td>
                    <td>
                        <asp:TextBox ID="tb_cfmnewpwd" runat="server" CssClass="auto-style2" Width="250px" TextMode="Password"></asp:TextBox>
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
                        <asp:Button ID="btn_confirm" runat="server" Height="35px" Text="Confirm" Width="100px" BackColor="#CCA9EB" ForeColor="White" OnClick="btn_login_Click" />
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