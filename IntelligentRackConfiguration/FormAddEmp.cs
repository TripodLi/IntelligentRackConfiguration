using MySql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace IntelligentRackConfiguration
{
    public partial class FormAddEmp : Form
    {
        /// <summary>
        /// 连接数据库
        /// </summary>
       // DbUtility db = new DbUtility("Data Source=.;Initial Catalog=" + GetXml("DataSource", "value") + ";User ID=sa;pwd=" + GetXml("Password", "value"), DbProviderType.SqlServer);
        DbUtility db = new DbUtility("server=" + GetXml("DataSource", "value") + "; " + "database=" + GetXml("InitialCatalog", "value") + ";User Id=" + GetXml("UserId", "value") + ";pwd=" + GetXml("Password", "value"), DbProviderType.SqlServer);
        public FormAddEmp()
        {
            InitializeComponent();
        }

        private void FormAddEmp_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = db.ExecuteDataTable("select FEERACK_ID,FEERACK_NAME from XH_FEERACK_T where DELETE_FLAG='0'");
                CB_EmpStation.DataSource = dt;
                CB_EmpStation.DisplayMember = "FEERACK_NAME";
                CB_EmpStation.ValueMember = "FEERACK_ID";
            }
            catch (Exception ex) 
            {
                MessageBox.Show("数据库连接失败！");
            }
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
        /// <summary>
        /// 插入员工
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_AddEmp_Click(object sender, EventArgs e)
        {
            try
            {
                String str = "INSERT INTO XH_EMP_T VALUES(" + Convert.ToInt32(TB_EmpNo.Text) + ",'" + TB_EmpName.Text + "'," + Convert.ToInt32(CB_EmpStation.SelectedValue) + ",GETDATE(),'0',NULL)";
                Object objLock = new Object();
                lock (objLock)
                {
                    db.ExecuteNonQuery(str);
                }
                    DialogResult dr = MessageBox.Show("添加成功！是否继续添加？", "提示", MessageBoxButtons.OKCancel);
                    if (dr == DialogResult.OK)
                    {
                        TB_EmpNo.Text = "";
                        TB_EmpName.Text = "";
                        CB_EmpStation.SelectedIndex = -1;
                    }
                    else
                    {
                        this.Close();
                    }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("添加员工失败！");
            }
        
        }

        private void BT_Canel_Click(object sender, EventArgs e)
        {
            TB_EmpNo.Text = "";
            TB_EmpName.Text = "";
            CB_EmpStation.SelectedIndex = -1;
        }
        private bool nonNumberEntered = false;
        private void TB_EmpNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (nonNumberEntered)
            {
                e.Handled = true;
            }
        }

        private void TB_EmpNo_KeyDown(object sender, KeyEventArgs e)
        {
            nonNumberEntered = false;
            if ((e.KeyCode < Keys.D0) || (e.KeyCode > Keys.D9 && e.KeyCode < Keys.NumPad0) || (e.KeyCode > Keys.NumPad9))
            {
                if (e.KeyCode != Keys.Back)
                {
                    nonNumberEntered = true;
                }
            }
        }

        private void LB_EmpStation_Click(object sender, EventArgs e)
        {

        }
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private void FormAddEmp_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x02, 0);
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
    }
}
