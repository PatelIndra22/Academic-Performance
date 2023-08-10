<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Academic Performance Report.aspx.cs" Inherits="Academic_Performance.Academic_Performance_Report" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>    
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <rsweb:ReportViewer ID="AcademicPerformanceReport" runat="server" Width="908px" Height="500px" style="margin: auto; margin-left:180px"></rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
