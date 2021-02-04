<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="_190382D_ASsignment.HomePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home Page</title>
    <link href="Stylesheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style1 {
            width: 42%;
        }
        .auto-style2 {
            width: 161px;
        }
        .auto-style3 {
            height: 27px;
        }
        .auto-style4 {
            width: 839px;
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
                    <li><a href="HomePage.aspx">Home</a></li>
                    <li><a href="ChangePassword.aspx">Change Password</a></li>
                    <li><asp:Button ID="btn_logout" runat="server" Text="Logout" Visible="false" BackColor="#CCA9EB" ForeColor="White" OnClick="btn_logout_Click"/></li>
                </ul>
              </nav>
            </div>
        </header>
        <div class="container">
            <fieldset>
                <legend>Home Page</legend>
                <br />
                <asp:Label ID="lbl_HPMessage" runat="server" EnableViewState="False"/>
                <br />
                <table class="auto-style1" style="background-color: #C0C0C0">
                    <tr>
                        <td class="auto-style3" colspan="2"><strong>User Profile</strong></td>
                    </tr>
                    <tr>
                        <td class="auto-style2">Name: </td>
                        <td class="auto-style4">
                            <asp:Label ID="lbl_hpname" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2">Email:</td>
                        <td class="auto-style4">
                            <asp:Label ID="lbl_hpemail" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style2">Birthdate:</td>
                        <td class="auto-style4">
                            <asp:Label ID="lbl_hpdob" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
            </fieldset>
        </div>
    </form>
</body>
</html>