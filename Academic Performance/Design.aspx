<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Design.aspx.cs" Inherits="Academic_Performance.Design" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/ckeditor/ckeditor.js"></script>
    <link href="Scripts/ckeditor/css/bootstrap.min.css" rel="stylesheet" />
    <script src="Scripts/bootstrap.min.js"></script>    
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table border="1">
                <tr>
                    <td>Select Your Page : </td>
                    <td>
                        <asp:DropDownList ID="drppage" runat="server" OnSelectedIndexChanged="drppage_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Select Your PageContent : </td>
                    <td>
                        <asp:DropDownList ID="drppagecontent" runat="server" OnSelectedIndexChanged="drppagecontent_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="ckeditorlbl" runat="server" Text="Desing Your Content : " Visible="false"></asp:Label>
                    </td>
                    <td>
                        <CKEditor:CKEditorControl ID="CKEditorControl1" runat="server" Visible="false"></CKEditor:CKEditorControl>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="fileuploadlbl" runat="server" Text="Upload Image : " Visible="false"></asp:Label>
                    </td>
                    <td>
                        <asp:FileUpload ID="imageuploader" runat="server" Visible="false"/>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">
                        <asp:Button ID="submitbtn" runat="server" Text="Submit" OnClick="submitbtn_Click" />
                        <input id="cancelbtn" type="reset" value="Cancel" />
                    </th>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
