using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Account_Login : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["USER"] = null;
        Session["UID"] = null;
        TextBox_UserName.Attributes.Add("OnFocus", "clearLabel();");
    }
   
    protected void LoginButton_Click(object sender, EventArgs e)
    {
        User user = new User();
        user.account = TextBox_UserName.Text;
        user.password = TextBox_Password.Text;
        UserManager manager = new UserManager();
        User result = manager.Login(user);
        if (result!=null)
        {
            Session["USER"] = result;
            Response.Redirect("~/default.aspx");
        }
        else
        {
            
            Label_loginfailure.Text = "● 登录失败,请输入正确的用户名和密码";
        }
    }

    
}
