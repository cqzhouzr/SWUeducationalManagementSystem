<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WebUserControl_查询排课信息.ascx.cs" Inherits="WebUserControl_查询排课信息" %>
<asp:SqlDataSource ID="SqlDataSource_学期" runat="server" 
    ConnectionString="<%$ ConnectionStrings:教务系统ConnectionString %>" 
    SelectCommand="SELECT [学期] FROM [学期]"></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource_排课信息" runat="server" 
    ConnectionString="<%$ ConnectionStrings:教务系统ConnectionString %>" 
    
    SelectCommand="SELECT 课程计划ID, 课程名称, 理论_实验, 教室编号, 起止周, 星期, 节次, 单双, 职工号, 姓名, 职称, 学年, 学期, 学分, 课程类别, 教学班组成 FROM View_排课 WHERE (学年 = @学年) AND (学期 = @学期) AND (职工号 = @职工号)">
    <SelectParameters>
        <asp:ControlParameter ControlID="DropDownList_学年" Name="学年" 
            PropertyName="SelectedValue" Type="String" />
        <asp:ControlParameter ControlID="DropDownList_学期" Name="学期" 
            PropertyName="SelectedValue" Type="String" />
        <asp:SessionParameter Name="职工号" SessionField="UID" />
    </SelectParameters>
</asp:SqlDataSource>
学年：<asp:DropDownList ID="DropDownList_学年" runat="server" AutoPostBack="True" onselectedindexchanged="DropDownList_学年_SelectedIndexChanged"
    DataSourceID="SqlDataSource_学期" DataTextField="学期" DataValueField="学期">
</asp:DropDownList>
&nbsp;学期：<asp:DropDownList ID="DropDownList_学期" runat="server" AutoPostBack="True" onselectedindexchanged="DropDownList_学期_SelectedIndexChanged" >
    <asp:ListItem>1</asp:ListItem>
    <asp:ListItem>2</asp:ListItem>
    <asp:ListItem>3</asp:ListItem>
</asp:DropDownList>
<asp:Button ID="Button1" runat="server"  Text="查询" />
<asp:GridView ID="GridView_排课信息" runat="server" AutoGenerateColumns="False" 
    Caption="排课信息" CssClass="customers" DataSourceID="SqlDataSource_排课信息" 
    OnRowCommand="GridView_OnRowCommand">
    <Columns>
        <asp:TemplateField HeaderText="课程计划ID" SortExpression="课程计划ID">
            <ItemTemplate>
                <asp:Label ID="Label_课程计划ID" runat="server" Text='<%# Bind("课程计划ID") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("课程计划ID") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="课程名称" HeaderText="课程名称" SortExpression="课程名称" />
        <asp:BoundField DataField="理论_实验" HeaderText="理论_实验" SortExpression="理论_实验" />
        <asp:BoundField DataField="教室编号" HeaderText="教室编号" SortExpression="教室编号" />
        <asp:BoundField DataField="起止周" HeaderText="起止周" SortExpression="起止周" />
        <asp:BoundField DataField="星期" HeaderText="星期" SortExpression="星期" />
        <asp:BoundField DataField="节次" HeaderText="节次" SortExpression="节次" />
        <asp:BoundField DataField="单双" HeaderText="单双" SortExpression="单双" />
        <asp:BoundField DataField="职工号" HeaderText="职工号" SortExpression="职工号" />
        <asp:BoundField DataField="姓名" HeaderText="姓名" SortExpression="姓名" />
        <asp:BoundField DataField="职称" HeaderText="职称" SortExpression="职称" />
        <asp:ButtonField CommandName="Select" HeaderText="操作" ShowHeader="True" 
            Text="选择" />
    </Columns>
    <EmptyDataTemplate>
        <asp:Label ID="Label_学期" runat="server" 
            Text='<%# 获得dropdownlist学期()+"学期无教学任务！" %>'></asp:Label>
    </EmptyDataTemplate>
</asp:GridView>

