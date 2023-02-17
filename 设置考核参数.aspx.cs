using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
public partial class 测试导入excel : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        string 课程计划ID = Request.QueryString["ID"];
        if (课程计划ID != null)
        {
            Course course = (Course)Session["course"];
            HiddenField_课程计划ID.Value = 课程计划ID;
            //WebUserControl_查询教学任务1.Visible = false;
            配置考核表(course);
        }
        else
            HiddenField_课程计划ID.Value = "";

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        Label Label_operation = Page.Master.FindControl("Label_operation") as Label;
        Label_operation.Text = ">设置考核参数";
        string 课程计划ID = Request.QueryString["ID"];
        if (课程计划ID==null)
            div_设置考核参数框.Visible = false;
        LinkButton1.ToolTip = "五分制转百分制\n优-95，良-85，中-75，合格-65，不合格-55\n百分制转五分制\n90-100:优\n80-89：良\n70-79：中\n60-69：合格\n<60：不合格";
        
    }
    protected void Button_Click(object sender, EventArgs e)    
    {
        string str_课程计划ID = WebUserControl_查询教学任务1.课程计划ID.Trim();
        CourseService courseService = new CourseService();
        Course course = new Course();
        course.ID = Convert.ToInt32(str_课程计划ID);
        courseService.GetCourse(course);//获取course数据保存在session中
        courseService.Get课程考核(course);
        Session["course"] = course;
        配置考核表(course);        
    }//web自定义控件的事件响应，建立课程考核表
    protected void Dropdownlist_changed(object sender, EventArgs e)
    {
        GridView_课程考核参数.DataSource = "";
        GridView_课程考核参数.DataBind();        
        div_设置考核参数框.Visible = false;
        
    }//web自定义控件的事件响应
    protected DataTable GetDataFromGridview()
    {
        CourseService courseService = new CourseService();
        Course course = new Course();
        Tabletools tabletools = new Tabletools();        
        List<string> 字段list = new List<string> { "ID", "课程计划ID", "考核项目", "比例", "分制" };
        DataTable dt1 = tabletools.createDataTable(字段list);
        if (HiddenField_课程计划ID.Value=="")
            course.ID = Convert.ToInt32(WebUserControl_查询教学任务1.课程计划ID);
        else
            course.ID = Convert.ToInt32(HiddenField_课程计划ID.Value);
        courseService.Get课程考核(course); //在couse对象中建立课程考核字典
        for (int i = 0; i < GridView_课程考核参数.Rows.Count; i++)
        {
            GridViewRow gRow = GridView_课程考核参数.Rows[i];
            DataRow newRow = dt1.NewRow();
            newRow[0] = ((Label)gRow.FindControl("Label_ID")).Text.Trim();
            newRow[1] = ((Label)gRow.FindControl("Label_课程计划ID")).Text.Trim(); 
            newRow[2] = ((Label)gRow.FindControl("Label_考核项目")).Text.Trim();
            newRow[3] = ((TextBox)gRow.FindControl("TextBox_比例")).Text.Trim();
            newRow[4] = ((DropDownList)gRow.FindControl("DropDownList_分制")).SelectedValue.Trim();
            dt1.Rows.Add(newRow);
        }
        dt1.AcceptChanges();
        return dt1;
    }
    protected void Button_保存考核参数_Click(object sender, EventArgs e)
    {
        Course course = (Course)Session["course"];        
        DataTable dt = GetDataFromGridview();
        CourseService courseservice = new CourseService();        
        courseservice.save_para_datatable_to_database(dt);
        courseservice.Get课程考核(course);
        Session["course"] = course;
        courseservice.UpdateScore(course);        
        Response.Redirect("录入成绩.aspx?ID=" + course.ID);
    } 
    protected void 配置考核表(Course course)
    {
        DataTable dt1;
        Tabletools tabletools = new Tabletools();
        CourseService courseService = new CourseService();
        if (course.考核字典.Count == 0)
        {
            Label_提示信息.Text = "第一次设置考核参数";
            dt1 = tabletools.生成考核空表(course.ID);
            HiddenField_分制.Value = "";
        }
        else
        {
            dt1 = course.考核DataTable;
            HiddenField_分制.Value = course.str分制;  //记录原有分制，JS检测用户是否修改分制
        }
        GridView_课程考核参数.DataSource = dt1 ;
        GridView_课程考核参数.DataBind();
        if (course.状态 == "保存")
            Label_提示信息.Text = "已录入成绩，修改考核分制可能引起成绩转换";
        if (course.状态 == "提交")
        {
            Label_提示信息.Text = "成绩已提交，不能再修改考核参数！ ";
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "设置考核表只读();", true);
        }
        if (course.状态 == "")
        {
            Label_提示信息.Text = "成绩还未提交，可以修改考核参数！ ";
        }
        div_设置考核参数框.Style.Add("width", "400");
        div_设置考核参数框.Visible = true;
    }//web自定义控件的事件响应，建立课程考核表
}