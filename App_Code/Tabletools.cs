using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
///Tabletools 的摘要说明
/// </summary>
public class Tabletools
{
	public Tabletools()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    public DataTable createDataTable(List<string> 字段列表)
    {
        DataTable dt1 = new DataTable("Table1");
        foreach (string item in 字段列表)
        {
            dt1.Columns.Add(item);
        }
        return dt1;
    }
    public DataTable 生成考核空表(int ID)
    {
        Tabletools tabletools = new Tabletools();
        List<string> 考核项目list = new List<string> { "平时", "实验", "半期", "期末" };
        List<string> 字段list = new List<string> { "ID", "课程计划ID", "考核项目", "比例", "分制" };
        DataTable dt1 = tabletools.createDataTable(字段list);
        for (int i = 0; i < 4; i++)  //第一次设置考核参数
        {
            DataRow newRow = dt1.NewRow();
            newRow[0] = "";
            newRow[1] = ID;
            newRow[2] = 考核项目list[i];
            newRow[3] = "";
            newRow[4] = "百分制";
            dt1.Rows.Add(newRow);
        }
        return dt1;
    }
}