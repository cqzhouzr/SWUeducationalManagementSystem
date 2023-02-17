using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    public string operation="";
    protected void Page_Load(object sender, EventArgs e)
    {
        User user = (User)Session["USER"];
        if (user == null)
            Response.Redirect("~/account/login.aspx");
        Label_loginName.Text = user.name+"老师";


        /*User user = new User();
        user.account = "20052352";
        user.name = "张三";
        Session["USER"] = user;
        User u = (User)Session["USER"];
        Label_loginName.Text = u.name+"老师";*/
        Session["UID"] = user.account;
    }
    public void Setopertation(string myOperation)
    {
        this.Label_operation.Text = myOperation;
    } 
}
