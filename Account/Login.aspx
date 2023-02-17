<%@ Page Title="登录" Language="C#" MasterPageFile="~/Site_login.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Account_Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
<script>
    function clearLabel() {

        x = document.getElementById("MainContent_Label_loginfailure");
        x.innerHTML="";
    }
</script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        登录
    </h2>
    <p>
        请输入用户名和密码。
        
    </p>
   
            <span class="failureNotification">
                <asp:Literal ID="FailureText" runat="server"></asp:Literal>
            </span>
            <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification" 
                 ValidationGroup="LoginUserValidationGroup"/>
            <div class="accountInfo">
                <fieldset class="login">
                    <legend>帐户信息</legend>
                    <p>
                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="TextBox_UserName">用户名:</asp:Label>
                        <asp:TextBox ID="TextBox_UserName" runat="server" CssClass="textEntry" 
                            ></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="TextBox_UserName" 
                             CssClass="failureNotification" ErrorMessage="必须填写“用户名”。" ToolTip="必须填写“用户名”。" 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                         
                    </p>
                    <p>
                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="TextBox_Password">密码:</asp:Label>
                        <asp:TextBox ID="TextBox_Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="TextBox_Password" 
                             CssClass="failureNotification" ErrorMessage="必须填写“密码”。" ToolTip="必须填写“密码”。" 
                             ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                       
                    </p>
                   
                </fieldset>
                <asp:Label ID="Label_loginfailure" class="failureNotification" runat="server" Text=""></asp:Label>
                <p class="submitButton">
                    
                    <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="登录" 
                        ValidationGroup="LoginUserValidationGroup" onclick="LoginButton_Click"/>
                </p>
            </div>
            
 
     
</asp:Content>
