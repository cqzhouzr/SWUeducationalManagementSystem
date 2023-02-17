using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.SessionState;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using System.Text.RegularExpressions;
/// <summary>
///Course 的摘要说明
/// </summary>
public class Course
{
	public Course()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}    
    private DataTable _考核DataTable = null;    
    public DataTable 考核DataTable
    {
        get
        {
            return _考核DataTable;
        }
        set
        {
            _考核DataTable = value;
        }
        
    }
    private Dictionary<string, ArrayList> _考核字典 = new Dictionary<string, ArrayList>();
    public Dictionary<string, ArrayList> 考核字典
    {
        get
        {
            return _考核字典;
        }
        set
        {
            _考核字典 = value;
        }

    }
    private int _ID = 0;
    public int ID
    {
        get
        {
            return _ID;
        }
        set
        {
            _ID = value;
        }
    }
    private string _coursecode = "";
    public string coursecode
    {
        get
        {
            return _coursecode;
        }
        set
        {
            _coursecode = value;
        }
    }
    private string _coursename = "";
    public string coursename
    {
        get
        {
            return _coursename;
        }
        set
        {
            _coursename = value;
        }
    }
    private string _学年 = "";
    public string 学年
    {
        get
        {
            return _学年;
        }
        set
        {
            _学年 = value;
        }
    }
    private string _学期 = "";
    public string 学期
    {
        get
        {
            return _学期;
        }
        set
        {
            _学期 = value;
        }
    }
    private string _状态 = "";
    public string 状态
    {
        get
        {
            return _状态;
        }
        set
        {
            _状态 = value;
        }
    }
    private string _str分制 = "";
    public string str分制
    {
        get
        {
            return _str分制;
        }
        set
        {
            _str分制 = value;
        }
    }
    private string _教学班 = "";
    public string 教学班
    {
        get
        {
            return _教学班;
        }
        set
        {
            _教学班 = value;
        }
    }
}

public class CourseService
{
    public void updateCourse(Course course)
    {
        GetCourse(course);
        Get课程考核(course);
    }
    public void GetCourse(Course course)
    {
        String sql = "";
        sql = "select 课程代码,课程名称,学年,学期,状态,教学班组成 from [教务系统].[dbo].[View_教学任务] where ID='" + course.ID + "'";
        dbHelper helper = new dbHelper();
        SqlDataReader reader = helper.ExecuteReader(sql);
        reader.Read();
        course.coursecode = reader["课程代码"].ToString().Trim();
        course.coursename = reader["课程名称"].ToString().Trim();
        course.学年 = reader["学年"].ToString().Trim();
        course.学期 = reader["学期"].ToString().Trim();
        course.状态 = reader["状态"].ToString().Trim();
        course.教学班 = reader["教学班组成"].ToString().Trim();
    }
    public void Get课程考核(Course course)
    {
        String sql = "";
        sql = "select * from [教务系统].[dbo].[考核] where 课程计划ID='" + course.ID + "'";
        dbHelper helper = new dbHelper();
        SqlDataReader reader = helper.ExecuteReader(sql);
        course.考核DataTable = new DataTable();
        course.考核DataTable.Load(reader);     //将sqldatareader装入datatable  
        reader.Close();
        
        Dictionary<string, ArrayList> _考核字典 = new Dictionary<string, ArrayList>();
        foreach (DataRow dataRow in course.考核DataTable.Rows)
        {
            ArrayList List = new ArrayList();
            List.Add(dataRow["比例"].ToString().Trim());
            List.Add(dataRow["分制"].ToString().Trim());
            _考核字典.Add(dataRow["考核项目"].ToString().Trim(),List);
        }
        course.考核字典 = _考核字典;

        string str分制 = "";        
        for (int i = 0; i < 4; i++)
            str分制 = str分制 + course.考核DataTable.Rows[i][4].ToString().Trim();
        course.str分制 = str分制;
        
    }//取得课程考核参数，保存为datatable和字典两种形式
    public string Get课程状态(Course course)
    {
        
        String sql = "";
        sql = "select 状态 from 课程计划 where ID='" + course.ID + "'";
        dbHelper helper = new dbHelper();
        string result=helper.selectToVar(sql).Trim();
        return result;
    }//取得课程当前状态;
    public void 改变课程状态(Course course,string statue)
    {
        String sql = "";
        sql = "update 课程计划 set 状态='"+statue+"' where ID='" + course.ID + "'";        
        dbHelper helper = new dbHelper();
        helper.ExecuteCommand(sql);

    }
    public string 计算总分(Course course, DataRow dataRow)
    {
        Dictionary<string, string> dic = new Dictionary<string, string>() { 
                {"A", "95"},
                {"B", "85"},
                {"C", "75"},
                {"D", "65"},
                {"E", "55"},
            };
        string[] 考核项目 = { "平时", "实验", "半期", "期末" };
        double 总分 = 0;
        string t = "";
        for (int i = 0; i < 4; i++)
        {
            try
            {
                string 比例 = course.考核字典[考核项目[i]][0].ToString();
                if (course.考核字典[考核项目[i]][1].ToString() == "五分制")
                    t = dic[dataRow[i + 2].ToString()];
                else
                    t = dataRow[i + 2].ToString();
                总分 = 总分 + Convert.ToDouble(t) * Convert.ToInt32(比例) / 100;
            }
            catch (Exception e)
            {
                
            }
        }
        return Convert.ToString(总分);
    }
    public List<string>  get课程考核字段()
    {
        List<string> 字段list = new List<string> { "课程计划ID", "学号", "平时", "实验", "半期", "期末", "总评" };
        return 字段list;
    }
    public void save_score_datatable_to_database(DataTable dt)
    {
        bool result = false;
        String sql = "";
        dbHelper helper = new dbHelper();        
        foreach (DataRow row in dt.Rows)
        {
            if (row["课程计划ID"] != null)
            {
                sql = "update 选课 set 平时= '" + row["平时"] + "' ,实验= '" + row["实验"] + "' ,半期= '" + row["半期"] + "' ,期末= '" + row["期末"] + "' ,总评= '" + row["总评"] + "'  where 课程计划ID='" + row["课程计划ID"] + "' and 学号='" + row["学号"] + " '";
                result = helper.ExecuteCommand(sql);
                // 更新该行记录到数据库 
            }
        }
    }//保存成绩表
    public void save_para_datatable_to_database(DataTable dt)//保存考核参数表
    {
        bool result = false;
        String sql = "";
        dbHelper helper = new dbHelper();
        foreach (DataRow row in dt.Rows)
        {
            if (row["ID"] != "")
            {
                sql = "update 考核 set 比例= '" + row["比例"] + "' ,分制= '" + row["分制"] + "'  where  ID='" + row["ID"] + " '";
                result = helper.ExecuteCommand(sql);
                // 更新该行记录到数据库 
            }
            else
            {
                sql = "insert into 考核 (课程计划ID,考核项目,比例,分制) values ('" + row["课程计划ID"] + "' ,'" + row["考核项目"] + "' ,'" + row["比例"] + "' ,'" + row["分制"] + "')";
                result = helper.ExecuteCommand(sql);
            }
        }
    }
    public DataTable ScoreReader(Course course)
    {
        String sql = "";
        sql = "select * from 选课 where 课程计划ID='" + course.ID + "'";
        dbHelper helper = new dbHelper();
        SqlDataReader sdr = helper.ExecuteReader(sql);
        DataTable dt = new DataTable();
        dt.Load(sdr);
        return dt;
    }//读入成绩表
    public List<string> MarkSystemReader(Course course)//读入分制
    {
        String sql = "";
        List<string> mark=new List<string>();
        sql = "select * from 考核 where 课程计划ID='" + course.ID + "'";
        dbHelper helper = new dbHelper();
        SqlDataReader sdr = helper.ExecuteReader(sql);      
        DataTable dt = new DataTable();
        dt.Load(sdr);
        foreach (DataRow row in dt.Rows)
        {
            mark.Add(row["分制"].ToString().Trim());
        }
        return mark;
        
    }
    public static bool IsNumeric(string value)
    {
        return Regex.IsMatch(value, @"^[0-9]*$");
    }//判断数字串
    public DataTable convertscore(DataTable dt,List<string> mark)
    {
        string[] 等级={"E","E","E","E","E","E","D","C","B","A","A"};
        string[] 考核项目={"平时","实验","半期","期末"};
        Dictionary<string, string> 百分 = new Dictionary<string, string>() 
            { { "A", "95" }, { "B", "85" }, { "C", "75" }, { "D", "65" }, { "E", "55" } };
        int idx=0;
        foreach (DataRow row in dt.Rows)
        {
            for (int i = 0; i < 4; i++)
            {
                if (IsNumeric(row[考核项目[i]].ToString()) && mark[i] == "五分制")
                {
                    try
                    {
                        idx = Convert.ToInt32(row[考核项目[i]].ToString()) / 10;
                        row[考核项目[i]] = 等级[idx];
                    }
                    catch 
                    {
                       
                    }
                }
                bool r=IsNumeric(row[考核项目[i]].ToString());
                if (!r && mark[i] == "百分制")
                {
                    try
                    {
                        string 百分数 = row[考核项目[i]].ToString();
                        row[考核项目[i]] = 百分[百分数];
                    }
                    catch 
                    {
                       
                    }
                }
            }
        }
        return dt;
    }//根据各成绩字段的分制转换成绩
    public DataTable 计算datatable总分(DataTable dt, Course course)
    {
        
        foreach (DataRow row in dt.Rows)
        {
            row["总评"] = 计算总分(course, row);
        }
        return dt;
    }
    public void UpdateScore(Course course)//当一门课的分制发生变化，转换分数
    {
        List<string> mark=MarkSystemReader(course);
        DataTable score=ScoreReader(course);
        score = convertscore(score, mark);
        score = 计算datatable总分(score,course);
        save_score_datatable_to_database(score);
    }
    public void 保存成绩(Course course,DataTable 成绩表,string statue)//当一门课的分制发生变化，转换分数
    {  
       save_score_datatable_to_database(成绩表);
       改变课程状态(course, statue);
    }
    public bool DatarowToSQL(DataRow[] dr,String 课程计划ID)
    {
        string sql = "";
        dbHelper helper = new dbHelper();
        bool result = false;
        for (int i = 0; i < dr.Length; i++)
        {
            string 学号 = dr[i]["学号"].ToString();
            string 平时 = dr[i]["平时"].ToString();
            string 实验 = dr[i]["实验"].ToString();
            string 半期 = dr[i]["半期"].ToString();
            string 期末 = dr[i]["期末"].ToString();
            sql = "update 选课 set" + " 平时= '" + 平时 + "'" + ",半期= '" + 半期 + "'" + ",实验= '" + 实验 + "'" + ",期末= '" + 期末 + "'" + "  where 课程计划ID='" + 课程计划ID + " ' and 学号='" + 学号 + "'";
            result = helper.ExecuteCommand(sql);
        }
        return result;
    }
}

