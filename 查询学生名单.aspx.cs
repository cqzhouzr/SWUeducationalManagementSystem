using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;

public partial class 查询授课任务 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label Label_operation = Page.Master.FindControl("Label_operation") as Label;
        Label_operation.Text = "> 查询学生名单";
        
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        excelHelper eclHelper = new excelHelper();
        string realpath = "C:\\test.xlsx";
        DataSourceSelectArguments args = new DataSourceSelectArguments();
        DataView view = (DataView)SqlDataSource_学生信息.Select(args);
        if (view == null)
            return;
        DataTable dta = view.ToTable();
        eclHelper.DataTable2Excel(dta, realpath, 500);
      
    }
    protected void Button_Click(object sender, EventArgs e)
    {

        
        string str_课程计划ID = WebUserControl_查询排课信息1.课程计划ID.Trim();
        HiddenField_课程计划ID.Value = str_课程计划ID;
        SqlDataSource_学生信息.DataBind();
        GridView_学生名单.DataBind();
        if (GridView_学生名单.Rows.Count!=0)
            div_成绩模板.Visible = true;

    }//web自定义控件的事件相应
    protected void Dropdownlist_changed(object sender, EventArgs e)
    {
        
        HiddenField_课程计划ID.Value = "";
        SqlDataSource_学生信息.DataBind();        
        div_成绩模板.Visible = false;
        
    }//web自定义控件的事件相应
 
}