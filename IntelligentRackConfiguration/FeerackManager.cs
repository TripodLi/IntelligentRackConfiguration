using MySql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace IntelligentRackConfiguration
{
    public partial class FeerackManager : Form
    {
        DbUtility db = new DbUtility("server=" + GetXml("DataSource", "value") + "; " + "database=" + GetXml("InitialCatalog", "value") + ";User Id=" + GetXml("UserId", "value") + ";pwd=" + GetXml("Password", "value"), DbProviderType.SqlServer);
        public FeerackManager()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void FeerackManager_Load(object sender, EventArgs e)
        {
            DataBind();
        }
        /// <summary>
        /// 读取配置文件方法
        /// </summary>
        /// <param name="node"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>

        public static string GetXml(string node, string attribute)
        {
            string result = null;
            XmlDocument xmlDoc = new XmlDocument();
            string addr = "Database.xml";
            xmlDoc.Load(addr);
            XmlNode nd;
            nd = xmlDoc.SelectSingleNode("Config");
            XmlNodeList xnl = nd.ChildNodes;
            foreach (XmlNode xn in xnl)
            {
                XmlElement xe = (XmlElement)xn;
                if (xe.Name == node)
                {
                    result = xe.GetAttribute(attribute);
                }
            }
            return result;
        }
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        private void FeerackManager_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x02, 0);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(feerack_Name.Text) && !String.IsNullOrEmpty(anothername.Text))
            {
                string sql = "select FEERACK_ID from dbo.XH_FEERACK_T where FEERACK_NAME='" + feerack_Name.Text + "' and ANOTHERNAME='" + anothername.Text + "' and DELETE_FLAG='0';";
                DataTable dt = new DataTable();
                dt = db.ExecuteDataTable(sql);
                if(dt.Rows.Count>0)
                {
                    MessageBox.Show("添加的料架号已存在！");
                }else
                {
                    string sql1 = "insert into dbo.XH_FEERACK_T (FEERACK_NAME,ANOTHERNAME,CREATE_TIME,DELETE_FLAG,PRINTEDBOOKS_ID)values('" + feerack_Name.Text + "','" + anothername.Text + "',GETDATE(),'0',2)";
                    db.ExecuteNonQuery(sql1);
                    DataBind();
                }
            }else
            {
                MessageBox.Show("料架名称为空或别名为空！");
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }

        public void DataBind()
        {
            string sql = "select FEERACK_ID,FEERACK_NAME,ANOTHERNAME  from dbo.XH_FEERACK_T WHERE DELETE_FLAG='0'";
            DataTable dt = new DataTable();
            dt = db.ExecuteDataTable(sql);
            dataGridView1.DataSource = dt;
        }
    }
}
