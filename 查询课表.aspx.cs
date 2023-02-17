using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;

public partial class 查询课表 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label Label_operation = Page.Master.FindControl("Label_operation") as Label;
        Label_operation.Text = "> 查询课表";
        
    }
    protected void Button_Click(object sender, EventArgs e)
    {   
        string str_课程计划ID = WebUserControl_查询排课信息1.课程计划ID.Trim();
        HiddenField_课程计划ID.Value = str_课程计划ID;
        SqlDataSource_学生信息.DataBind();        
        显示课表();
    }//web自定义控件的事件响应,由选择排课按钮触发
    protected void Dropdownlist_changed(object sender, EventArgs e)
    {
        
        HiddenField_课程计划ID.Value = "";
        SqlDataSource_学生信息.DataBind();
        GridView_课表.Visible = false;
        div_成绩模板.Visible = false;
        
    }//web自定义控件的事件响应
    protected void 显示课表()
    {
        
        DataTable 排课计划 = WebUserControl_查询排课信息1.排课计划;
        Tabletools tabletools = new Tabletools();
        List<string> 字段list = new List<string>{"全天","节次","时间","星期一","星期二","星期三","星期四","星期五","星期六","星期天"};
        List<string> 时间list = new List<string> { "8:00-8:45", "8:55-9:40", "10:00-10:45", "10:55-11:40", 
                                                    "12:10-12:55", "13:05-13:50" ,
                                                    "14:00-14:45", "14:55-15:40", "15:50-16:35", "16:55-17:40", "17:50-18:35", 
                                                    "19:20-20:05", "20:15-21:00", "21:10-21:55" };
        DataTable dt1 = tabletools.createDataTable(字段list);
        string 课程名称 = "";
        string 星期 = "";
        string str_节次 = "";
        string 单双 = "";
        for (int i = 0; i < 14; i++)
        {

            DataRow newRow = dt1.NewRow();
            if(i==4 || i==5)
                newRow[0] = "中午";           
            else if(i>=11)
                newRow[0] = "晚上";           
            else if(i<4)
                newRow[0] = "上午";           
            else
                newRow[0] = "下午";
            newRow[1] = i+1;
            newRow[2] = 时间list[i];
            dt1.Rows.Add(newRow);
        }

        foreach (DataRow row in 排课计划.Rows)
        {
            
            课程名称 = "<b>" + "&#9749 " + row["课程名称"].ToString().Trim() + "</b>" +
                "<br>" + "&#9728 " + row["理论_实验"].ToString().Trim() +
                "<br>" + "&#9997 " + row["教室编号"].ToString().Trim() +
                "<br>" + "&#9875 " + row["起止周"].ToString().Trim();
            星期 = row["星期"].ToString().Trim();
            str_节次 = row["节次"].ToString().Trim();
            string[] arr_节次 = str_节次.Split('-');
            int 节次始 = Convert.ToInt32(arr_节次[0]);
            int 节次终 = Convert.ToInt32(arr_节次[1]);
            单双 = row["单双"].ToString().Trim();
            if (单双 != "")
                课程名称 = 课程名称 + "(" + 单双 + ")";
            for (int i = 节次始-1; i <节次终; i++)
            {
                int c = Convert.ToInt32(星期);                
                dt1.Rows[i][c+2] = 课程名称;
            }
        }
        GridView_课表.DataSource = dt1;
        User u = (User)Session["USER"];
        string 学期=WebUserControl_查询排课信息1.学期;
        string 学年=WebUserControl_查询排课信息1.学年;
        GridView_课表.Caption = u.name+"老师 > "+学年+"-"+学期+"学期 > 课表";
        GridView_课表.DataBind();
        GridView_课表.Visible = true;
        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "ChangeTable();", true);
        div_成绩模板.Visible = true;
    }
}