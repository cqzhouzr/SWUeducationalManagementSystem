using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Configuration; 


/// <summary>
///dbHelper 的摘要说明
/// </summary>
public class dbHelper
{
	public dbHelper()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    //private String connectionString="Data Source=swu-PC\\SQLEXPRESS;Initial Catalog=教务系统;Integrated Security=True";
    private String connectionString = ConfigurationManager.ConnectionStrings["教务系统ConnectionString"].ConnectionString;
    public string selectToVar(String sql)
    {
        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();
        SqlCommand command = new SqlCommand(sql, connection);
        SqlDataReader dr = command.ExecuteReader();
        dr.Read();
        string result= dr[0].ToString();
        connection.Close();
        return result;
    }
    public SqlDataReader ExecuteReader(String sql)
    {
        SqlDataReader result = null;
        SqlConnection connection = new SqlConnection(connectionString);
        try
        {
            connection.Open();

            SqlCommand command = new SqlCommand(sql, connection);

            result = command.ExecuteReader();
            //connection.Close();
        }
        catch (Exception e)
        {
            throw e;
        }
        return result;
    }
    public bool ExecuteCommand(String sql)
    {
        bool result = false;

        try
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand(sql,connection);
            //command.Connection = connection;
            //command.CommandText = sql;
            command.ExecuteNonQuery();


            connection.Close();

            result = true;
        }
        catch (Exception e)
        {
            throw e;
        }

        return result;
    }    
}