using System.Windows.Forms;
using MySQLhelp;
using MySql.Data.MySqlClient;
using System.Data;
using System;
using System.Net;
using oracelDatabase;
using Oracle.ManagedDataAccess.Client;

namespace MYSQL
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        MySqlOperate sqlOperate = new MySqlOperate();

        oracleHelp oracleOperate = new oracleHelp();
        

        //窗体加载时，执行程序
        private void FrmMain_Load(object sender, EventArgs e)
        {
            timer1.Start();//从中控室向阿里云数据库写入数据
            timer2.Start();//1s执行一次
        }



        //窗口关闭时，程序后台运行
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.WindowState = FormWindowState.Minimized;
        }
        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("你确定要退出程序吗？", "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                notifyIcon1.Visible = false;
                this.Close();
                this.Dispose();
                Application.Exit();
            }

        }

        private void hideMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void showMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.Activate();

        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)//判断鼠标的按键
            {
                if (this.WindowState == FormWindowState.Normal)
                {
                    this.WindowState = FormWindowState.Minimized;

                    this.Hide();
                }
                else if (this.WindowState == FormWindowState.Minimized)
                {
                    this.Show();
                    this.WindowState = FormWindowState.Normal;
                    this.Activate();
                }
            }

        }

        //窗体关闭时，最小化运行
        private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.WindowState = FormWindowState.Minimized;
        }



        //oracle数据库获取数据插入阿里云中
        public void ActionFLineAll()
        {
            for (int iNum = 1; iNum <= 4; iNum++)
            {
                if (iNum == 1)
                {
                    //删除阿里云中数据
                    sqlOperate.MySqlCom("delete  from fline1;");

                    //获取oracle数据库中数据
                    string SQL = "select * from YM_FINISH_INFO";

                    OracleDataReader DReader =oracleOperate.ReturnDataReader(SQL);
                    if (DReader.HasRows)//判断SqlDataReader对象中是否有数据
                    {
                        while (DReader.Read())//循环读取SqlDataReader对象中的数据
                        {                                          
                            string mysql = "insert into fline1 values('" + DReader["DT"] + "','" + DReader["OK"] + "','"
                                + DReader["O1"] + "','" + DReader["O2"] + "','" + DReader["TOTAL"] + "')";
                            sqlOperate.MySqlCom(mysql);
                        }
                    }

                    oracleOperate.CloseConn();//关闭oracle连接

                    



                }
                if (iNum == 2)
                {
                    //删除阿里云中数据
                    sqlOperate.MySqlCom("delete  from fline2;");

                    //获取oracle数据库中数据
                    string SQL = "select * from YM_FINISH2_INFO";

                    OracleDataReader DReader = oracleOperate.ReturnDataReader(SQL);
                    if (DReader.HasRows)//判断SqlDataReader对象中是否有数据
                    {
                        while (DReader.Read())//循环读取SqlDataReader对象中的数据
                        {
                            string mysql = "insert into fline2 values('" + DReader["DT"] + "','" + DReader["OK"] + "','"
                                + DReader["O1"] + "','" + DReader["O2"] + "','" + DReader["TOTAL"] + "')";
                            sqlOperate.MySqlCom(mysql);
                        }
                    }
                    oracleOperate.CloseConn();//关闭oracle连接
                }
                if (iNum == 3)
                {
                    //删除阿里云中数据
                    sqlOperate.MySqlCom("delete  from fline3;");

                    //获取oracle数据库中数据
                    string SQL = "select * from YM_FINISH3_INFO";


                    OracleDataReader DReader = oracleOperate.ReturnDataReader(SQL);
                    if (DReader.HasRows)//判断SqlDataReader对象中是否有数据
                    {
                        while (DReader.Read())//循环读取SqlDataReader对象中的数据
                        {
                            string mysql = "insert into fline3 values('" + DReader["DT"] + "','" + DReader["OK"] + "','"
                                + DReader["O1"] + "','" + DReader["O2"] + "','" + DReader["TOTAL"] + "')";
                            sqlOperate.MySqlCom(mysql);
                        }
                    }
                    oracleOperate.CloseConn();//关闭oracle连接
                }
                if (iNum == 4)
                {
                    //删除阿里云中数据
                    sqlOperate.MySqlCom("delete  from fline4;");

                    //获取oracle数据库中数据
                    string SQL = "select * from YM_FINISH4_INFO";


                    OracleDataReader DReader = oracleOperate.ReturnDataReader(SQL);
                    if (DReader.HasRows)//判断SqlDataReader对象中是否有数据
                    {
                        while (DReader.Read())//循环读取SqlDataReader对象中的数据
                        {
                            string mysql = "insert into fline4 values('" + DReader["DT"] + "','" + DReader["OK"] + "','"
                                + DReader["O1"] + "','" + DReader["O2"] + "','" + DReader["TOTAL"] + "')";
                            sqlOperate.MySqlCom(mysql);
                        }
                    }
                    oracleOperate.CloseConn();//关闭oracle连接
                }
            }
        }

      
        //定时器-1
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = 240000;//每4分钟写入一次数据 单位ms


            //检测网络连接状态，网络连接成功后，写入数据

            System.Net.NetworkInformation.Ping ping = new System.Net.NetworkInformation.Ping();
            System.Net.NetworkInformation.PingReply pingStatus = ping.Send(IPAddress.Parse("202.108.22.5"), 1000);//ping 百度的IP地址

                if (pingStatus.Status == System.Net.NetworkInformation.IPStatus.Success)
                {
                    ActionFLineAll();
                listInfo.Items.Add(DateTime.Now.ToString() + "数据写入成功");
                }
                else
                {
                    listInfo.Items.Add(DateTime.Now.ToString() + "外网连接失败");
                }
     
        }

        //定时器1s执行一次
        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Interval = 1000;
            toolStripStatusLabel1.Text = DateTime.Now.ToLocalTime().ToString();
        }
    }
}
