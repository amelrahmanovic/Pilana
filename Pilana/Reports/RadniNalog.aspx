<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RadniNalog.aspx.cs" Inherits="Pilana.Reports.RadniNalog" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=14.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%" Height="100%" ShowBackButton="False" ShowFindControls="False" ShowPageNavigationControls="False" ShowRefreshButton="False" ShowZoomControl="False" ShowPrintButton="True"></rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
