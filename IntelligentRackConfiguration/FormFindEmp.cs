using MySql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace IntelligentRackConfiguration
{
    public partial class FormFindEmp : Form
    {
        DbUtility db = new DbUtility("Data Source=.;Initial Catalog=" + GetXml("DataSource", "value") + ";User ID=sa;pwd=" + GetXml("Password", "value"), DbProviderType.SqlServer);
        public FormFindEmp()
        {
            InitializeComponent();
        }

        private void FormFindEmp_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = db.ExecuteDataTable("select FEERACK_ID,FEERACK_NAME from XH_FEERACK_T where DELETE_FLAG='0'");
            CB_EmpStation.DataSource = dt;
            CB_EmpStation.DisplayMember = "FEERACK_NAME";
            CB_EmpStation.ValueMember = "FEERACK_ID";
            FindEmp();
        }
        /// <summary>
        /// 读取配置文件
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

        private void BT_Sure_Click(object sender, EventArgs e)
        {
            try
            {
                FindEmp();
            }
            catch
            {
                MessageBox.Show("查找失败！");
            }
           
        }
        public void FindEmp()
        {
            DataTable dt1 = new DataTable();
            String str = "SELECT XH_EMP_T.EMP_NO,XH_EMP_T.EMP_NAME,XH_FEERACK_T.FEERACK_NAME,XH_FEERACK_T.FEERACK_ID,XH_EMP_T.EMP_ID FROM XH_EMP_T,XH_FEERACK_T WHERE XH_EMP_T.DELETE_FLAG='0' AND XH_EMP_T.FEERACK_ID=" + CB_EmpStation.SelectedValue + "AND XH_FEERACK_T.FEERACK_ID=XH_EMP_T.FEERACK_ID";
            dt1 = db.ExecuteDataTable(str);
            dataGridView1.DataSource = dt1;
        }
        private void dataGridView1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {

        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            //String str = "UPDATE XH_EMP_T SET XH_EMP_T.DELETE_FLAG='1' WHERE XH_EMP_T.EMP_ID=" + this.dataGridView1.CurrentRow.Cells["EMP_ID"].Value;
            //db.ExecuteNonQuery(str);
        }
        /// <summary>
        /// 在DATAGRIDVIEW和数据库中同时删除记录，但是在数据库中是逻辑删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_Canel_Click(object sender, EventArgs e)
        {
            try
            {
                int r = this.dataGridView1.CurrentRow.Index;
                String empId = this.dataGridView1.Rows[r].Cells[4].Value.ToString();
                //删除DATAGRIDVIEW1的选中行
                this.dataGridView1.Rows.Remove(this.dataGridView1.Rows[r]);
                String str = "UPDATE XH_EMP_T SET XH_EMP_T.DELETE_FLAG='1' WHERE XH_EMP_T.EMP_ID=" + Convert.ToInt32(empId);
                db.ExecuteNonQuery(str);
            }
            catch (Exception ex)
            {
                MessageBox.Show("删除员工失败！");
            }
        }
    }
}
