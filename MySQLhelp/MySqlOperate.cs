using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
namespace MySQLhelp
{
    public class MySqlOperate
    {

        
        //建立连接 2020-08-18
        public MySqlConnection MySqlGetCon()
        {       
                String connetStr = "server=rm-hp3q8vgfzl4du8493zo.mysql.huhehaote.rds.aliyuncs.com;port=3306;user=ccr_123;password=FAW_ccr123!; database=xiushi;";
                MySqlConnection myCon = new MySqlConnection(connetStr);
                return myCon;
    
        }

        //执行sql语句 2020-08-18
        public void MySqlCom(string sqlstr)
        {
                MySqlConnection mysqlcon = this.MySqlGetCon();
                mysqlcon.Open();
                MySqlCommand mysqlcom = new MySqlCommand(sqlstr, mysqlcon);
                mysqlcom.ExecuteNonQuery();
                mysqlcon.Close();
             
        }


        //查询内容 20200903

        public MySqlDataReader mysqlSearch(string sqlstr)
        {
                MySqlConnection conn = this.MySqlGetCon();                   
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sqlstr, conn);
                MySqlDataReader reader = cmd.ExecuteReader();//执行ExecuteReader()返回一个MySqlDataReader对象
                return reader;            
         }
    }
}
