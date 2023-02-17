using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.SessionState;

/// <summary>
///User 的摘要说明
/// </summary>
public class User
{
    private String _account = "";
    

    public String account
        {
            get
            {
                return _account;
            }
            set
            {
                _account = value;
            }
        }
    private String _password = "";
    public String password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }
    private String _role = "";
    public String role
    {
        get
        {
            return _role;
        }
        set
        {
            _role = value;
        }
    }
    private String _name = "";
    public String name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = value;
        }
    }
	
}


public class UserService
{
    public User GetUserByLogin(User user)
    {
        String sql = "";
        sql = "select * from [教务系统].[dbo].[View_user] where account='" + user.account + "'";
        dbHelper helper = new dbHelper();
        SqlDataReader reader = helper.ExecuteReader(sql);
        User result = new User();
        if (reader.Read())
        {
            result.account = reader.GetString(0).Trim();
            result.password = reader.GetString(1).Trim();
            result.name = reader.GetString(2).Trim();
            result.role = reader.GetString(3).Trim();

        }
        else
        {
            return null;
        }
        return result;
    }

}

public class UserManager
{

    public User Login(User user)
    {
        

        UserService service = new UserService();

        User temp = service.GetUserByLogin(user);
        if (temp ==null)
            return null;
        if (user.password.Equals(temp.password))
        {
            
            return temp;            
        }
        else
            return null;
    }
   

}
