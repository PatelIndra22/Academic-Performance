<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Marks.aspx.cs" Inherits="Academic_Performance.MarksUploader" %>

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
                    <td>Upload Student Marks : </td>
                    <td>
                        <asp:FileUpload ID="marksuploader" runat="server" />
                    </td>
                    <td>
                        <asp:Button ID="submitbtn" runat="server" Text="Submit" OnClick="submitbtn_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
