<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="查询学生名单.aspx.cs" Inherits="查询授课任务" %>

<%@ Register src="WebUserControl_查询排课信息.ascx" tagname="WebUserControl_查询排课信息" tagprefix="uc1" %>

<%@ Register src="WebUserControl_查询教学任务.ascx" tagname="WebUserControl_查询教学任务" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">  
    <asp:SqlDataSource ID="SqlDataSource_学生信息" runat="server" 
        ConnectionString="<%$ ConnectionStrings:教务系统ConnectionString %>" 
        SelectCommand="SELECT * FROM [View_学生名单] WHERE [课程计划ID] = @课程计划ID">
        <SelectParameters>
            <asp:ControlParameter ControlID="HiddenField_课程计划ID" Name="课程计划ID" 
                PropertyName="Value" Type="Int32" />            
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:HiddenField ID="HiddenField_课程计划ID" runat="server" />
    <uc1:WebUserControl_查询排课信息 ID="WebUserControl_查询排课信息1" runat="server" OnClick="Button_Click" OnDropdownlist_changed="Dropdownlist_changed"/>
      
    <div class="clear"></div>

    <div  style="float:right;" runat="server" id="div_成绩模板" Visible=false>
            <asp:Button ID="Button2" runat="server" Text="导出" onclick="Button2_Click" />
   </div>
    <br/>
    <asp:GridView ID="GridView_学生名单" runat="server" AutoGenerateColumns="False" 
        CssClass="customers" DataSourceID="SqlDataSource_学生信息" Caption="学生名单">
        <Columns>
            <asp:BoundField DataField="学号" HeaderText="学号" SortExpression="学号" />
            <asp:BoundField DataField="姓名" HeaderText="姓名" SortExpression="姓名" />
            <asp:BoundField DataField="性别" HeaderText="性别" SortExpression="性别" />
            <asp:BoundField DataField="学院" HeaderText="学院" SortExpression="学院" />
            <asp:BoundField DataField="年级" HeaderText="年级" SortExpression="年级" />
            <asp:BoundField DataField="专业" HeaderText="专业" SortExpression="专业" />
            <asp:BoundField DataField="班级" HeaderText="班级" SortExpression="班级" />
        </Columns>
    </asp:GridView>
   
</asp:Content>

