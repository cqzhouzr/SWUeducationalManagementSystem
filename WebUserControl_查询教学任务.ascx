<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WebUserControl_查询教学任务.ascx.cs" Inherits="WebUserControl_查询教学任务" %>
学年：<asp:DropDownList 
    ID="DropDownList_学年" runat="server" DataTextField="学期" DataValueField="学期" 
    AutoPostBack="True" 
    onselectedindexchanged="DropDownList_学年_SelectedIndexChanged">
    <asp:ListItem>2021-2022</asp:ListItem>
    <asp:ListItem>2022-2023</asp:ListItem>
</asp:DropDownList>
&nbsp;学期：<asp:DropDownList ID="DropDownList_学期" runat="server" 
    AutoPostBack="True" onselectedindexchanged="DropDownList_学期_SelectedIndexChanged" 
    >
    <asp:ListItem>1</asp:ListItem>
    <asp:ListItem>2</asp:ListItem>
    <asp:ListItem>3</asp:ListItem>
</asp:DropDownList>
<asp:Button ID="Button3" runat="server" Text="查询" />

<asp:SqlDataSource ID="SqlDataSource_学期" runat="server" 
    ConnectionString="<%$ ConnectionStrings:教务系统ConnectionString %>" 
    SelectCommand="SELECT [学期] FROM [学期]"></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource_教学任务" runat="server" 
    ConnectionString="<%$ ConnectionStrings:教务系统ConnectionString %>" 
    
    SelectCommand="SELECT * FROM [View_教学任务] WHERE (([学年] = @学年) AND ([学期] = @学期)) AND ([职工号]=@职工号)">
    <SelectParameters>
        <asp:ControlParameter ControlID="DropDownList_学年" Name="学年" 
            PropertyName="SelectedValue" Type="String" />
        <asp:ControlParameter ControlID="DropDownList_学期" Name="学期" 
            PropertyName="SelectedValue" Type="String" />
        <asp:SessionParameter Name="职工号" SessionField="UID" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:GridView ID="GridView_教学任务" runat="server" AutoGenerateColumns="False" 
    Caption="教学任务" CssClass="customers" DataSourceID="SqlDataSource_教学任务" 
    OnRowCommand="GridView_OnRowCommand">
    <Columns>
        <asp:TemplateField HeaderText="ID" SortExpression="ID">
            <ItemTemplate>
                <asp:Label ID="Label_课程计划ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ID") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="课程代码" HeaderText="课程代码" SortExpression="课程代码" />
        <asp:BoundField DataField="课程名称" HeaderText="课程名称" SortExpression="课程名称" />
        <asp:TemplateField HeaderText="教学班组成" SortExpression="教学班组成">
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" 
                    Text='<%# Eval("教学班组成").ToString().Substring(0, 10) + "..." %>' 
                    ToolTip='<%# Eval("教学班组成") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("教学班组成") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="选课人数" HeaderText="选课人数" SortExpression="选课人数" />
        <asp:TemplateField HeaderText="状态" SortExpression="状态">
            <ItemTemplate>
                <asp:Label ID="Label_状态" runat="server" Text='<%# Bind("状态") %>'></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("状态") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="学分" HeaderText="学分" SortExpression="学分" />
        <asp:BoundField DataField="课程类别" HeaderText="课程类别" SortExpression="课程类别" />
        <asp:BoundField DataField="考核方式" HeaderText="考核方式" SortExpression="考核方式" />
        <asp:BoundField DataField="讲授课学时" HeaderText="讲授课学时" SortExpression="讲授课学时" />
        <asp:BoundField DataField="实验课学时" HeaderText="实验课学时" SortExpression="实验课学时" />
        <asp:ButtonField CommandName="Select" HeaderText="操作" ShowHeader="True" 
            Text="选择" />
    </Columns>
    <EmptyDataTemplate>
        <asp:Label ID="Label_学期" runat="server" Text='<%# 获得dropdownlist学期()+"学期无教学任务！" %>' ></asp:Label>        
    </EmptyDataTemplate>
</asp:GridView>

