using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;

public partial class WebUserControl_查询排课信息 : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public string 获得dropdownlist学期()
    {
        return DropDownList_学年.SelectedValue + "-" + DropDownList_学期.SelectedValue;
    }
    public DataTable _排课计划 = null;
    public DataTable 排课计划
    {
        get { return (DataTable)ViewState["viewState_排课计划"]; }
        set { _排课计划 = value; }
    }
    public string 课程计划ID  // 控件的自定义属性值
    {
        get { return (string)ViewState["课程计划ID"]; }
        set { ViewState["课程计划ID"] = value; }
    }
    public string 学年  // 控件的自定义属性值
    {
        get { return (string)ViewState["学年"]; }
        set { ViewState["学年"] = value; }
    }
    public string 学期  // 控件的自定义属性值
    {
        get { return (string)ViewState["学期"]; }
        set { ViewState["学期"] = value; }
    }
    protected void GridView_OnRowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Select")
        {
            DataSourceSelectArguments args = new DataSourceSelectArguments();
            DataView view = (DataView)SqlDataSource_排课信息.Select(args);
            if (view == null)
                return;
            DataTable dta = view.ToTable();
            ViewState["viewState_排课计划"] = dta; 
            
            
            int index = Convert.ToInt32(e.CommandArgument);
            Label label_课程计划ID = (Label)GridView_排课信息.Rows[index].FindControl("Label_课程计划ID");
            课程计划ID = label_课程计划ID.Text.Trim();
            学期 = DropDownList_学期.SelectedValue;
            学年 = DropDownList_学年.SelectedValue;
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