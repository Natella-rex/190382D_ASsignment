<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XSSDisplay.aspx.cs" Inherits="_190382D_ASsignment.XSSDisplay" ValidateRequest = "false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>XSS Display Error</title>
</head>
<body>
    <h2>SIT Connect- XSS Display Error</h2>
    <form id="form1" runat="server">
        <div>
            Unable to login due to malicious string detected.<br />
            You have entered the following:<br />
            <br />
&nbsp;<asp:Label ID="lbl_xssdisplay" runat="server" Text="Display"></asp:Label>
        </div>
    </form>
</body>
</html>
