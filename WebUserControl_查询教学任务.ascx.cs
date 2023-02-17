using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUserControl_查询教学任务 : System.Web.UI.UserControl
{  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string str_课程计划ID = Request.QueryString["ID"];
            if (str_课程计划ID != null)
            {
                //DropDownList_学年.DataSource = "";
                //DropDownList_学年.DataBind();
                //SqlDataSource_学期.DataBind();
                //DropDownList_学年.DataSource = SqlDataSource_学期;
                //DropDownList_学年.DataBind();
                Course course = (Course)Session["course"];
                DropDownList_学年.SelectedValue = course.学年.ToString();
                DropDownList_学期.SelectedValue = course.学期;
                课程计划ID = Convert.ToString(course.ID);
            }
        }
    }
    public string 获得dropdownlist学期()
    {
        return DropDownList_学年.SelectedValue+"-"+DropDownList_学期.SelectedValue;
    }
    public string 课程计划ID  // 控件的自定义属性值
    {
        get { return (string)ViewState["课程计划ID"]; }
        set { ViewState["课程计划ID"] = value; }
    }
   
    string _课程状态 = "";
   
    public string 课程状态                  // 控件的自定义属性值
    {
        get
        {
            return _课程状态;
        }
        set
        {
            _课程状态 = value;
        }
    }

    protected void GridView_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            int index = Convert.ToInt32(e.CommandArgument);
            Label label_课程计划ID = (Label)GridView_教学任务.Rows[index].FindControl("Label_课程计划ID");
            Label label_课程状态 = (Label)GridView_教学任务.Rows[index].FindControl("Label_状态");
            课程计划ID = label_课程计划ID.Text;
            _课程状态 = label_课程状态.Text;
            
            if (Click != null) Click(this, new EventArgs());

        }
    }  
    public delegate void Click_Handle(object sender, EventArgs e);  // 自定义事件的参数类型    
    public event Click_Handle Click;    // 定义当前控件的点击事件名称，在属性中为 On此处名称=“Button_Click”
    public delegate void Change_Handle(object sender, EventArgs e);  // 自定义事件的参数类型    
    public event Change_Handle Dropdownlist_changed;    // 定义当前控件的点击事件名称，在属性中为 On此处名称=“Button_Click”
    protected void DropDownList_学期_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Dropdownlist_changed != null) Dropdownlist_changed(this, new EventArgs());
    }
    protected void DropDownList_学年_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Dropdownlist_changed != null) Dropdownlist_changed(this, new EventArgs());
    }
}