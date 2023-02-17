<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="测试类.aspx.cs" Inherits="测试类" %>

<%@ Register src="WebUserControl_查询教学任务.ascx" tagname="WebUserControl_查询教学任务" tagprefix="uc1" %>

<%@ Register src="WebUserControl_查询排课信息.ascx" tagname="WebUserControl_查询排课信息" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <uc2:WebUserControl_查询排课信息 ID="WebUserControl_查询排课信息1" runat="server" />
    
</asp:Content>

