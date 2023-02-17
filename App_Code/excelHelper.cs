using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Data;
using System.Data.OleDb;


/// <summary>
///excelHelper 的摘要说明
/// </summary>
public class excelHelper
{
	public excelHelper()
	{
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    public  System.Data.DataSet ExcelSqlConnection(string filepath, string tableName)
    {
        //string strCon = "Provider=Microsoft.ACE.OLEDB.12.0;Extended Properties=Excel 12.0;Data Source=" + filepath ;

        string strCon = "Provider=Microsoft.Ace.OleDb.12.0;Data Source=" + filepath + ";Extended properties=\"Excel 12.0; HDR=Yes;\"";
        OleDbConnection ExcelConn = new OleDbConnection(strCon);
        try
        {
            string strCom = string.Format("SELECT * FROM [Sheet1$]");
            ExcelConn.Open();

            OleDbCommand com = ExcelConn.CreateCommand();
            com.CommandText = "select * from [sheet1$]";
            com.CommandType = CommandType.Text;
            OleDbDataAdapter adapter = new OleDbDataAdapter(com);
            DataSet ds = new DataSet();

            ExcelConn.Close();
            adapter.Fill(ds);
            return ds;


        }
        catch
        {
            ExcelConn.Close();
            return null;
        }
    }
    protected void downLoad(string fileName, string path)
    {
        FileInfo fi = new FileInfo(path);
        if (fi.Exists)
        {
            System.Web.HttpContext.Current.Response.Clear();
            System.Web.HttpContext.Current.Response.ClearContent();
            System.Web.HttpContext.Current.Response.ClearHeaders();
            System.Web.HttpContext.Current.Response.Buffer = true;

            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            System.Web.HttpContext.Current.Response.AddHeader("Content-Length", fi.Length.ToString());
            System.Web.HttpContext.Current.Response.AddHeader("Content-Transfer-Encoding", "binary");
            System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");

            System.Web.HttpContext.Current.Response.WriteFile(path);
            System.Web.HttpContext.Current.Response.Flush();
            System.Web.HttpContext.Current.Response.End();
        }
    }
    public bool DataTable2Excel(DataTable dataTable, string fileName, int rowsCount)
            {
                bool rt = false;//用于返回值
                if (dataTable == null && rowsCount < 1)
                {
                    return false;
                }
                int rowNum = dataTable.Rows.Count;//获取行数
                int colNum = dataTable.Columns.Count;//获取列数
                int SheetNum = (rowNum - 1) / rowsCount + 1; //获取工作表数
                string sqlText = "";//带类型的列名
                string sqlValues = "";//值
                string colCaption = "";//列名
                string savePath = "";
                savePath = System.Web.HttpContext.Current.Server.MapPath(("download\\") + fileName);
                for (int i = 0; i < colNum; i++)
                {
                    if (i != 0)
                    {
                        sqlText += " , ";
                        colCaption += " , ";
                    }
                    sqlText += "[" + dataTable.Columns[i].Caption.ToString() + "] VarChar";//生成带VarChar列的标题
                    colCaption += "[" + dataTable.Columns[i].Caption.ToString() + "]";//生成列的标题
                }
                String sConnectionString = "Provider=Microsoft.Ace.OleDb.12.0;Data Source=" + savePath + ";Extended properties=\"Excel 12.0 XML;HDR=Yes;\"";
                //"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};" + "Extended Properties='Excel 12.0 XML'"
                OleDbConnection cn = new OleDbConnection(sConnectionString);
                try
                {
                    //判断文件是否存在,存在则先删除
                    //savePath = System.Web.HttpContext.Current.Server.MapPath(("download\\") + fileName);
                    if (File.Exists(savePath))
                    {
                        File.Delete(savePath);
                    }
                    int sheet = 1;//表数
                    int dbRow = 0;//数据的行数
                    //打开连接
                    cn.Open();
                    while (sheet <= SheetNum)
                    {
                        string sqlCreate = "CREATE TABLE [Sheet" + sheet.ToString() + "] (" + sqlText + ")";
                        OleDbCommand cmd = new OleDbCommand(sqlCreate, cn);
                        //创建Excel文件
                        cmd.ExecuteNonQuery();
                        for (int srow = 0; srow < rowsCount; srow++)
                        {
                            sqlValues = "";
                            for (int col = 0; col < colNum; col++)
                            {
                                if (col != 0)
                                {
                                    sqlValues += " , ";
                                }
                                sqlValues += "'" + dataTable.Rows[dbRow][col].ToString() + "'";//拼接Value语句
                            }
                            String queryString = "INSERT INTO [Sheet" + sheet.ToString() + "] (" + colCaption + ") VALUES (" + sqlValues + ")";
                            cmd.CommandText = queryString;
                            cmd.ExecuteNonQuery();//插入数据
                            dbRow++;//目前数据的行数自增
                            if (dbRow >= rowNum)
                            {
                                //目前数据的行数等于rowNum时退出循环
                                break;
                            }
                        }
                        sheet++;
                    }
                    rt = true;
                }
                catch
                {
                }
                finally
                {
                    cn.Close();
                }

                this.downLoad("hello.xlsx", savePath);
                return rt;


            }  
}