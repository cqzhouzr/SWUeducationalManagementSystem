<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="设置考核参数.aspx.cs" Inherits="测试导入excel" %>

<%@ Register src="WebUserControl_查询教学任务.ascx" tagname="WebUserControl_查询教学任务" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <meta http-equiv="Page-Enter" content="blendTrans(Duration=0)">
    <meta http-equiv="Page-Exit" content="blendTrans(Duration=0)">
    <style>
    #popupcontent{ 
    position: absolute;
    visibility: hidden;   
    overflow: hidden;   
    border:1px solid #CCC;   
    background-color:#F9F9F9;   
    border:1px solid #333;   
    padding:5px;}
</style>
<script>
    function 设置考核表只读() {
        var tableObj = document.getElementById("MainContent_GridView_课程考核参数");
        for (var i = 1; i < tableObj.rows.length; i++) {    //遍历Table的所有Row
            var bl = tableObj.rows[i].getElementsByTagName('input');
            for (var j = 0; j < bl.length; j++) {
                bl[j].disabled = true;
                bl[j].setAttribute("style", "color:#999999");
            }
            var ddl = tableObj.rows[i].getElementsByTagName('select');
            for (var j = 0; j < ddl.length; j++)
                ddl[j].disabled = true;

        }
        var butt = document.getElementById("MainContent_Button_保存考核参数");
        butt.disabled = true;

    }
    function 检查分数比例() {
        总分 = 0
        str分制 = "";
        var tableObj = document.getElementById("MainContent_GridView_课程考核参数");
        for (var i = 1; i < tableObj.rows.length; i++) {    //遍历Table的所有Row
            var bl = tableObj.rows[i].cells[3].getElementsByTagName('input');
            var ddl = tableObj.rows[i].cells[4].getElementsByTagName('select');
            tableInfo = bl[0].value;
            //bl[0].setAttribute("value",tableInfo);
            分制 = ddl[0].value;
            //ddl[0].setAttribute("value",分制);
            tableInfo = tableInfo.trim();
            if (tableInfo!="")
                总分 += parseFloat(tableInfo);
            str分制 = str分制 + 分制;
        }
        if (总分 != 100) {
            alert("分数比列合计不是100！");
            return false;
        }
        var hide分制 = document.getElementById("MainContent_HiddenField_分制");
        if (hide分制.value!="" && hide分制.value != str分制)
        {
            if(confirm("分制发生变化，确定要根据变化的分制调整成绩吗？")==false)
                return false;
        }
        return true;        
    }

    var baseText = null;
    function showPopup(w, h) {
        var popUp = document.getElementById("popupcontent");
        var win = document.getElementById("button_导入成绩");
        popUp.style.top = win.offsetTop + 30 + "px"; // "200px";
        popUp.style.left = win.offsetLeft + "px"; //"200px";
        popUp.style.width = w + "px";
        popUp.style.height = h + "px";
        popUp.style.visibility = "visible";
    }
    function hidePopup() {

        var popUp = document.getElementById("popupcontent");
        popUp.style.visibility = "hidden";
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
   <asp:HiddenField ID="HiddenField_分制" runat="server" />
   <asp:HiddenField ID="HiddenField_课程计划ID" runat="server" />
   
   <uc1:WebUserControl_查询教学任务 ID="WebUserControl_查询教学任务1" runat="server"  OnClick="Button_Click" OnDropdownlist_changed="Dropdownlist_changed"/>
   <br/> 
   <div id="div_设置考核参数框" runat="server">
        <div  style="float:left; color: #CC3300;" runat="server" id="div_成绩模板"  >
            <asp:Label ID="Label_提示信息" runat="server" Text="Label"></asp:Label>
            <asp:LinkButton  style="TEXT-DECORATION: none"
                ID="LinkButton1" ToolTip="优-95，良-85，中-75，<br>合格-65，不合格-55" runat="server" OnClientClick="return false;">&#65533</asp:LinkButton>
        </div>
        <asp:GridView ID="GridView_课程考核参数" runat="server" AutoGenerateColumns="False" 
        CssClass="customers" Caption="考核参数">
        <Columns>
            <asp:TemplateField HeaderText="ID" InsertVisible="False" SortExpression="ID">
                <EditItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label_ID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="课程计划ID" SortExpression="课程计划ID">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("课程计划ID") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label_课程计划ID" runat="server" Text='<%# Bind("课程计划ID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="考核项目" SortExpression="考核项目">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("考核项目") %>' ></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label_考核项目" runat="server" Text='<%# Bind("考核项目") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="比例" SortExpression="比例">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("比例") %>' ></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>                    
                    <asp:TextBox ID="TextBox_比例" Text='<%# Bind("比例") %>' runat="server" 
                        Width="50px"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="分制" SortExpression="分制">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("分制") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>                    
                    <asp:DropDownList ID="DropDownList_分制" runat="server"  SelectedValue='<%# Eval("分制").ToString().Trim() %>'>
                        <asp:ListItem>五分制</asp:ListItem>
                        <asp:ListItem>百分制</asp:ListItem>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
        <div  style="float:right; color: #CC3300;" runat="server" id="div1"  Visible=true>
            <asp:Button ID="Button_保存考核参数" runat="server" Text="保存" 
                OnClientClick="return 检查分数比例();" onclick="Button_保存考核参数_Click"  />
        </div>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:教务系统ConnectionString %>" 
            SelectCommand="SELECT * FROM [考核]"></asp:SqlDataSource>
   </div>    
   <br/> 

</asp:Content>

