<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="Academic_Performance.AdminPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <asp:Button ID="designbtn" runat="server" Text="Design" PostBackUrl="~/Design.aspx" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="marksbtn" runat="server" Text="Marks" PostBackUrl="~/Marks.aspx"/>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>