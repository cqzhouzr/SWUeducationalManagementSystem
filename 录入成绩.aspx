<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="录入成绩.aspx.cs" Inherits="录入成绩" %>

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
    
    <asp:HiddenField ID="HiddenField_课程计划ID_录入成绩" runat="server" />
    <uc1:WebUserControl_查询教学任务 ID="WebUserControl_查询教学任务1" runat="server"  OnClick="Button_Click" OnDropdownlist_changed="Dropdownlist_changed"/>
     <br />
    <div  style="float:right;" runat="server" id="div_成绩模板" Visible=false>
            <asp:Button ID="Button_修改考核参数" runat="server" Text="修改考核参数" 
                onclick="Button_修改考核参数_Click" />
            <asp:Button ID="Button1" runat="server" Text="导出成绩录入excel模板" 
                onclick="Button_导出成绩录入excel模板_Click"  />
            <input ID="button_导入成绩" type="button" value="导入成绩" onClick="showPopup(300,180);">
            <asp:Button ID="Button_保存" runat="server" Text="保存" 
                onclick="Button_保存_Click"  
                onclientclick="return 遍历成绩表();"/>
            <asp:Button ID="Button_提交" runat="server" Text="提交" onclick="Button_提交_Click" 
                onclientclick="return confirm('您确定提交吗？提交后将不能修改成绩');" />
   </div>
    <br/>
    <div class="clear"></div>
    <asp:GridView ID="GridView_录入成绩" runat="server" 
                CssClass="customers" PageSize="15" 
                DataSourceID="SqlDataSource_录入成绩" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="序号" HeaderText="序号" SortExpression="序号" />
                    <asp:TemplateField HeaderText="学号" SortExpression="学号">
                        <ItemTemplate>
                            <asp:Label ID="Label_学号" runat="server" Text='<%# Bind("学号") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("学号") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="姓名" HeaderText="姓名" 
                        SortExpression="姓名" />
                    <asp:TemplateField HeaderText="平时" SortExpression="平时">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox_平时" runat="server" Text='<%# Bind("平时") %>' Width="60"  ></asp:TextBox>
                            <asp:DropDownList ID="DropDownList_平时" runat="server"
                                DataSourceID="SqlDataSource_等级" DataTextField="等级" DataValueField="等级"  
                                >
                            </asp:DropDownList>
                        </ItemTemplate>
                        <EditItemTemplate>
                            
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="实验" SortExpression="实验">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox_实验" runat="server" Text='<%# Bind("实验") %>'  Width="60"  ></asp:TextBox>
                             <asp:DropDownList ID="DropDownList_实验" runat="server" 
                                DataSourceID="SqlDataSource_等级" DataTextField="等级" DataValueField="等级"  
                                >
                            </asp:DropDownList>
                        </ItemTemplate>
                        <EditItemTemplate>
                            
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="半期" SortExpression="半期">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox_半期" runat="server" Text='<%# Bind("半期") %>'  Width="60"  ></asp:TextBox>
                            <asp:DropDownList ID="DropDownList_半期" runat="server" 
                                DataSourceID="SqlDataSource_等级" DataTextField="等级" DataValueField="等级"  
                                >
                            </asp:DropDownList>
                        </ItemTemplate>
                        <EditItemTemplate>
                            
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="期末" SortExpression="期末">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox_期末" runat="server" Text='<%# Bind("期末") %>'  Width="60"  ></asp:TextBox>
                            <asp:DropDownList ID="DropDownList_期末" runat="server" 
                                DataSourceID="SqlDataSource_等级" DataTextField="等级" DataValueField="等级"  
                                >
                            </asp:DropDownList>
                        </ItemTemplate>
                        <EditItemTemplate>
                            
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="总评" HeaderText="总评" SortExpression="总评" />
                    <asp:BoundField DataField="备注" HeaderText="备注" SortExpression="备注" />
                </Columns>                
            </asp:GridView>
    
    <asp:SqlDataSource ID="SqlDataSource_录入成绩" runat="server" 
    ConnectionString="<%$ ConnectionStrings:教务系统ConnectionString %>" 
    SelectCommand="SELECT row_number() over ( order by 学号 asc) as 序号,* FROM [View_成绩单] WHERE ([课程计划ID] = @课程计划ID)" 
        OldValuesParameterFormatString="original_{0}">
    <SelectParameters>
        <asp:ControlParameter ControlID="HiddenField_课程计划ID_录入成绩" Name="课程计划ID" 
            PropertyName="Value" Type="Int32" />
    </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource_等级" runat="server" 
        ConnectionString="<%$ ConnectionStrings:教务系统ConnectionString %>" 
        SelectCommand="SELECT [等级] FROM [等级]"></asp:SqlDataSource>
<div id="popupcontent" style="padding: 0px; margin: 0px">
    <div  style=" background-color: #009999; width: 100%; text-align: right; ">        
        <input type="button" value="X" onClick="hidePopup();"/>
    </div>    
    <div style="margin: 10px">
        <br />        
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <br />
        <br />
        <asp:Button ID="Button_开始导入" runat="server"  Text="开始导入" onclick="Button_开始导入成绩_Click" />
    </div>
</div>   

</asp:Content>

