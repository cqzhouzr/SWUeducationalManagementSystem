using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.IO;
public partial class 录入成绩 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Label Label_operation = Page.Master.FindControl("Label_operation") as Label;
            Label_operation.Text = ">成绩录入";
            string str_课程计划ID = Request.QueryString["ID"];
            if (str_课程计划ID != null)
            {
                Course course = (Course)Session["course"];
                this.进入录入成绩状态(course);
            }
        }
    }
    protected DataTable GetDataFromGridview()    {
        CourseService courseService = new CourseService();
        Course course = new Course();
        Tabletools tabletools = new Tabletools();
        List<string> 字段list = courseService.get课程考核字段();
        DataTable dt1 = tabletools.createDataTable(字段list);  
        course.ID = Convert.ToInt32( HiddenField_课程计划ID_录入成绩.Value);// WebUserControl_查询教学任务1.课程计划ID);
        courseService.Get课程考核(course); //在couse对象中建立课程考核字典
        for (int i = 0; i < GridView_录入成绩.Rows.Count; i++)
        {
            GridViewRow gRow = GridView_录入成绩.Rows[i];
            DataRow newRow = dt1.NewRow();
            newRow[0] = course.ID;
            newRow[1] = ((Label)gRow.FindControl("Label_学号")).Text;
            for (int j = 2; j < 字段list.Count-1; j++)
            {
                if (course.考核字典[字段list[j]][1].ToString() == "五分制")
                    newRow[j] = ((DropDownList)gRow.FindControl("DropDownList_"+字段list[j])).SelectedValue;
                else
                    newRow[j] = ((TextBox)gRow.FindControl("TextBox_"+字段list[j])).Text;
            }
            string total = courseService.计算总分(course, newRow);
            newRow[6] = total;
            dt1.Rows.Add(newRow);
        }
        dt1.AcceptChanges();
        return dt1;
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
        this.进入录入成绩状态(course);
      
    }//web自定义控件的事件相应
    protected void Dropdownlist_changed(object sender, EventArgs e)
    {
        HiddenField_课程计划ID_录入成绩.Value = "";       
        SqlDataSource_录入成绩.DataBind();        
        div_成绩模板.Visible = false;
    }//web自定义控件的事件相应
    protected void 五分制百分制切换(Course course)
    {
        CourseService courseService = new CourseService();
        List<string> list_课程考核字段 = courseService.get课程考核字段();
        string 考核项目 = "";        
        for (int i = 0; i < GridView_录入成绩.Rows.Count; i++)//根据考核参数，将gridview中的五分制和百分制之间切换
        {
            for (int j = 2; j < list_课程考核字段.Count - 1; j++)
            {
                考核项目 = list_课程考核字段[j];
                TextBox TextBox_score = (TextBox)GridView_录入成绩.Rows[i].FindControl("TextBox_" + 考核项目);
                TextBox_score.Attributes.Add("OnFocus", "if(this.value=='范围错') {this.value='';this.style.backgroundColor='#ffffff';}");
                TextBox_score.Attributes.Add("OnBlur", "if(parseFloat(this.value)>100 || parseFloat(this.value)<0 ||isNaN(this.value)){this.value='范围错';this.style.backgroundColor='#ff0000';}");
                TextBox_score.Attributes.Add("OnKeyPress", "if(((event.keyCode>=48)&&(event.keyCode<=57))||(event.keyCode==46)) {event.returnValue=true;} else{event.returnValue=false;}");
                DropDownList DropDownList_score = (DropDownList)GridView_录入成绩.Rows[i].FindControl("DropDownList_" + 考核项目);
                if (course.状态 == "提交")
                {
                    TextBox_score.Enabled=false;
                    DropDownList_score.Enabled = false;
                }

                if (course.考核字典[考核项目][1].ToString() == "五分制")
                {
                    DropDownList_score.SelectedValue = TextBox_score.Text.Trim();
                    TextBox_score.Visible = false;
                    DropDownList_score.Visible = true;
                    if (course.考核字典[考核项目][0].ToString() == "0")
                        DropDownList_score.Enabled  = false;
                }
                else
                {
                    TextBox_score.Visible = true;
                    DropDownList_score.Visible = false;
                    if (course.考核字典[考核项目][0].ToString() == "0")
                        TextBox_score.Enabled = false;
                }
            }
        }
        gridview列标题加分数比例(list_课程考核字段, course);
        gridview标题加课程和班级(course);


    }//在五分制和百分制之间切换  
    protected void gridview标题加课程和班级(Course course)
    {
        string 教学班 = "";  
        if (course.教学班.Length > 30)
            教学班 = course.教学班.Substring(0, 30) + "....";
        else
            教学班 = course.教学班;
        GridView_录入成绩.Caption = course.coursename + "※" + 教学班 + "※成绩单";

    }//在gridview标题上加入课程和班级
    protected void gridview列标题加分数比例(List<string> list_课程考核字段,Course course)
    {
        string 考核项目 = "";
        string 比例 = "";
        for (int i = 0; i < 4; i++)
        {
            考核项目 = list_课程考核字段[i + 2];
            string fieldname = GridView_录入成绩.HeaderRow.Cells[i + 3].Text;
            比例 = course.考核字典[考核项目][0].ToString();
            GridView_录入成绩.HeaderRow.Cells[i + 3].Text = fieldname + "(" + 比例 + "%)";
        }

    }//在gridview列名上加分数比例
    protected void 进入录入成绩状态(Course course){        
        HiddenField_课程计划ID_录入成绩.Value = course.ID.ToString();
        SqlDataSource_录入成绩.DataBind();
        GridView_录入成绩.DataBind();        
        五分制百分制切换(course);
        if (course.状态 == "提交")
            div_成绩模板.Visible = false;
        else
            div_成绩模板.Visible = true;
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        HiddenField_课程计划ID_录入成绩.Value = "";
        SqlDataSource_录入成绩.DataBind();
        div_成绩模板.Visible = false;
    }
    protected void Button_导出成绩录入excel模板_Click(object sender, EventArgs e)
    {
        SqlDataSource_录入成绩.DataBind();
        excelHelper eclHelper = new excelHelper();
        string realpath = "成绩导入模板.xlsx";
        DataSourceSelectArguments args = new DataSourceSelectArguments();
        DataView view = (DataView)SqlDataSource_录入成绩.Select(args);
        if (view == null)
            return;
        DataTable dta = view.ToTable();
        eclHelper.DataTable2Excel(dta, realpath, 500);
    }
    protected void Button_开始导入成绩_Click(object sender, EventArgs e)
    {
        string 课程计划ID=HiddenField_课程计划ID_录入成绩.Value;
        dbHelper helper = new dbHelper();
        excelHelper exlHelper = new excelHelper();
        if (FileUpload1.HasFile == false)//HasFile用来检查FileUpload是否有指定文件
        {
            Response.Write("<script>alert('请您选择Excel文件')</script> ");
            return;//当无文件时,返回
        }
        string IsXls = System.IO.Path.GetExtension(FileUpload1.FileName).ToString().ToLower();//System.IO.Path.GetExtension获得文件的扩展名
        if (IsXls != ".xlsx")
        {
            Response.Write("<script>alert('只可以选择Excel文件')</script>");
            return;//当选择的不是Excel文件时,返回
        }
        string filename = FileUpload1.FileName;              //获取Execle文件名  DateTime日期函数
        string savePath = Server.MapPath(("upfiles\\") + filename);//Server.MapPath 获得虚拟服务器相对路径
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
        }
        FileUpload1.SaveAs(savePath);                        //SaveAs 将上传的文件内容保存在服务器上
        
        DataSet ds = exlHelper.ExcelSqlConnection(savePath, filename);           //调用自定义方法
        
        DataRow[] dr = ds.Tables[0].Select();            //定义一个DataRow数组
        int rowsnum = ds.Tables[0].Rows.Count;
        if (rowsnum == 0)
        {
            Response.Write("<script>alert('Excel表为空表,无数据!')</script>");   //当Excel表为空时,对用户进行提示
            return;
        }
        CourseService courseService = new CourseService();
        if (courseService.DatarowToSQL(dr, 课程计划ID))
        {
            Course course = (Course)Session["course"];
            Response.Write("<script>alert('Excle表导入成功!," + dr.Length + "条记录');</script>");                        
            this.进入录入成绩状态(course);
        }
        else
            Response.Write("<script>alert('Excle表导入失败，请重试!');</script>");

    }
    protected void Button_保存_Click(object sender, EventArgs e)
    {  
        try
        {   
            保存成绩("保存");
            Response.Write("<script language=javascript>alert('成绩已保存在服务器上，您可以继续录入成绩！');</" + "script>");
        }
        catch
        {
            Response.Write("<script language=javascript>alert('保存失败，请稍后再试');</" + "script>");
        }

        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "设置成绩表读();", true);
    }
    protected void Button_提交_Click(object sender, EventArgs e)
    {
        try
        {  
            保存成绩("提交");
            Response.Write("<script language=javascript>alert('成绩已提交服务器，不能继续录入成绩！');</" + "script>");
        }
        catch
        {
            Response.Write("<script language=javascript>alert('提交失败，请稍后再试');</" + "script>");
            return;
        }
        div_成绩模板.Visible = false;
        
    }
    protected void Button_修改考核参数_Click(object sender, EventArgs e)
    {
        string ID = HiddenField_课程计划ID_录入成绩.Value;
        Response.Redirect("设置考核参数.aspx?ID="+ID);
    }
    protected void 保存成绩(string statue)
    {
        DataTable dt = this.GetDataFromGridview();
        Course course = (Course)Session["course"];
        CourseService courseService = new CourseService();
        courseService.保存成绩(course, dt,statue);
        courseService.updateCourse(course);//更新课程最新状态数据
        this.进入录入成绩状态(course);
        WebUserControl_查询教学任务1.DataBind(); //更新用户控件中的教学任务信息   
    }
   
}