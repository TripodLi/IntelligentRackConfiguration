using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql;
using System.Xml;
using System.Text.RegularExpressions;
using MyOPC;

namespace IntelligentRackConfiguration
{
    public partial class Form1 : Form
    {
        public static int feerack;
        public static int productiontype;
        /// <summary>
        /// 连接数据库
        /// </summary>
      //  DbUtility db = new DbUtility("server=.; "+"database=" + GetXml("DataSource", "value") + ";User Id=sa" + ";pwd=" + GetXml("Password", "value"), DbProviderType.SqlServer);
        DbUtility db = new DbUtility("Data Source=.;Initial Catalog=" + GetXml("DataSource", "value") + ";User ID=sa;pwd=" + GetXml("Password", "value"), DbProviderType.SqlServer);
      /// <summary>
      /// OPC连接
      /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 加载时连接数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                String str = "SELECT FEERACK_ID,FEERACK_NAME,PRINTEDBOOKS_ID FROM XH_FEERACK_T WHERE DELETE_FLAG='0'"; 
                //绑定料架
                DataTable dt = new DataTable();
                dt=db.ExecuteDataTable(str);
                CB_Station.DataSource = dt;
                CB_Station.ValueMember = "FEERACK_ID";
                CB_Station.DisplayMember = "FEERACK_NAME";

              
                dataGridView1.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接错误！");
            }
        }
        /// <summary>
        /// 弹出增加员工子窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 增加员工ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAddEmp fa = new FormAddEmp();
            fa.ShowDialog();

        }
        
        /// <summary>
        /// 弹出查询员工子窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 查询员工ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormFindEmp ffe = new FormFindEmp();
            ffe.ShowDialog();
        }
        /// <summary>
        /// 类别选择事件触发相应的选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CB_Category_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            switch (CB_Category.SelectedIndex)
            { 
                   
                case 0:
                    MaterialShelfpanel.Visible = true;
                    MaterialNumpanel.Visible = true;
                    FeatureCodepanel.Visible = true;

                    Sleevepanel.Visible=false;
                    Gunpanel.Visible = false;
                    Programpanel.Visible = false;
                    PhotoNopanel.Visible = false;
                    Reworkpanel.Visible = false;
                 //   Buttonpanel.Visible = true;
                     TB_MaterialName.Visible = true;
                    LB_MaterailName.Visible = true;
                    label3.Visible = true;
                    break;
                case 1:
                    MaterialShelfpanel.Visible = false;
                    MaterialNumpanel.Visible = true;
                    FeatureCodepanel.Visible = false;
                     Reworkpanel.Visible = true;
                    //Buttonpanel.Visible = true;
                    Sleevepanel.Visible=true;
                    Gunpanel.Visible = true;
                    Programpanel.Visible = true;
                    PhotoNopanel.Visible = false;
                     TB_MaterialName.Visible = true;
                    LB_MaterailName.Visible = true;
                    label3.Visible = true;
                    break;
                case 2:
                     MaterialShelfpanel.Visible = false;
                    MaterialNumpanel.Visible = false;
                    FeatureCodepanel.Visible = false;
                     Reworkpanel.Visible = false;
                 //   Buttonpanel.Visible = true;

                    Sleevepanel.Visible=false;
                    Gunpanel.Visible = false;
                    Programpanel.Visible = false;
                    PhotoNopanel.Visible = true;
                     TB_MaterialName.Visible = true;
                    LB_MaterailName.Visible = true;
                    label3.Visible = true;
                    break;
                default :
                     MaterialShelfpanel.Visible = false;
                    MaterialNumpanel.Visible = false;
                    FeatureCodepanel.Visible = false;
                     Reworkpanel.Visible = false;
                  //  Buttonpanel.Visible = true;

                    Sleevepanel.Visible=false;
                    Gunpanel.Visible = false;
                    Programpanel.Visible = false;
                    PhotoNopanel.Visible = false;
                    TB_MaterialName.Visible = false;
                    LB_MaterailName.Visible = false;
                    label3.Visible = false;
                    break;

            }
        }

        private void LB_FeatureCode_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 连接OPC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_Connect_Click(object sender, EventArgs e)
        {
            feerack = Convert.ToInt32(CB_Station.SelectedValue);
            productiontype = CB_MaterialNo.SelectedIndex + 1;
            ConnectionOpcForm cof = new ConnectionOpcForm(this);
           
            this.Hide();
            cof.ShowDialog();
        }
        /// <summary>
        /// 将配好的步序保存到右边网格中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        DataTable dt = new DataTable();
        public  void BT_StepSubmit_Click(object sender, EventArgs e)
        {
            this.dataGridView2.Rows.Add();
            ////步序
            this.dataGridView2.Rows[this.dataGridView2.Rows.Count - 1].Cells[0].Value = CB_StepNo.SelectedItem.ToString();
           // rw["步序"] = CB_StepNo.SelectedItem.ToString();
            ////类别
            this.dataGridView2.Rows[this.dataGridView2.Rows.Count - 1].Cells[1].Value = CB_Category.SelectedItem.ToString();
            //rw["Category"] = CB_Category.SelectedItem.ToString();
            
            ////名称
            if (!String.IsNullOrEmpty(TB_MaterialName.Text))
            {
                this.dataGridView2.Rows[this.dataGridView2.Rows.Count - 1].Cells[2].Value = TB_MaterialName.Text;
               // rw["Desc"] = TB_MaterialName.Text; 
            }
            else
            {
                this.dataGridView2.Rows[this.dataGridView2.Rows.Count - 1].Cells[2].Value = "--";
                //rw["Desc"] = "--"; 
            }
            ////返工次数
            if (!String.IsNullOrEmpty(TB_ReworkTimes.Text))
            {
                this.dataGridView2.Rows[this.dataGridView2.Rows.Count - 1].Cells[10].Value = TB_ReworkTimes.Text;
              //  rw["Rework_Time"] = TB_ReworkTimes.Text;
            }
            else
            {
                this.dataGridView2.Rows[this.dataGridView2.Rows.Count - 1].Cells[10].Value = "--";
               // rw["Rework_Time"] = "--";
            }
            if (CB_MaterialShelfNo.SelectedIndex>=0)
            {
                this.dataGridView2.Rows[this.dataGridView2.Rows.Count - 1].Cells[3].Value = CB_MaterialShelfNo.SelectedItem.ToString();
              //  rw["MaterialShelfNo"] = CB_MaterialShelfNo.SelectedItem.ToString();
            }
            else
            {
                this.dataGridView2.Rows[this.dataGridView2.Rows.Count - 1].Cells[3].Value = "--";
               // rw["MaterialShelfNo"] = "--";
            }
            if (CB_GunNo.SelectedIndex >= 0)
            {
                this.dataGridView2.Rows[this.dataGridView2.Rows.Count - 1].Cells[5].Value = CB_GunNo.SelectedItem.ToString();
               // rw["GunNo"] = CB_GunNo.SelectedItem.ToString();
            }
            else
            {
                this.dataGridView2.Rows[this.dataGridView2.Rows.Count - 1].Cells[5].Value = "--";
              //  rw["GunNo"] = "--";
            }
            if (!String.IsNullOrEmpty(TB_ProgramNo.Text))
            {
                this.dataGridView2.Rows[this.dataGridView2.Rows.Count - 1].Cells[6].Value = TB_ProgramNo.Text;
              //  rw["ProgrameNo"] = TB_ProgramNo.Text;
            }
            else
            {
                this.dataGridView2.Rows[this.dataGridView2.Rows.Count - 1].Cells[6].Value = "--";
              //  rw["ProgrameNo"] = "--";
            }
            if (!String.IsNullOrEmpty(TB_MaterialNum.Text))
            {
                this.dataGridView2.Rows[this.dataGridView2.Rows.Count - 1].Cells[4].Value = TB_MaterialNum.Text;
               // rw["Number"] = TB_MaterialNum.Text;
            }
            else
            {
                this.dataGridView2.Rows[this.dataGridView2.Rows.Count - 1].Cells[4].Value = "--";
               // rw["Number"] = "--";

            }
            if (!String.IsNullOrEmpty(TB_FeatureCode.Text))
            {
                this.dataGridView2.Rows[this.dataGridView2.Rows.Count - 1].Cells[7].Value = TB_FeatureCode.Text;
              //  rw["FeatureCode"] = TB_FeatureCode.Text;
            }
            else
            {
                this.dataGridView2.Rows[this.dataGridView2.Rows.Count - 1].Cells[7].Value = "--";
               // rw["FeatureCode"] = "--";
            }
            if (TB_SleeveNo.SelectedIndex >= 0)
            {
                this.dataGridView2.Rows[this.dataGridView2.Rows.Count - 1].Cells[9].Value = TB_SleeveNo.SelectedItem.ToString();
              //  rw["SleeveNo"] = TB_SleeveNo.SelectedItem.ToString();
            }
            else
            {
                this.dataGridView2.Rows[this.dataGridView2.Rows.Count - 1].Cells[9].Value = "--";
              //  rw["SleeveNo"] = "--";
            }
            if (CB_PhotoNo.SelectedIndex >= 0)
            {
                this.dataGridView2.Rows[this.dataGridView2.Rows.Count - 1].Cells[8].Value = CB_PhotoNo.SelectedItem.ToString();
               // rw["PhotoNo"] = CB_PhotoNo.SelectedItem.ToString();
            }
            else
            {
                this.dataGridView2.Rows[this.dataGridView2.Rows.Count - 1].Cells[8].Value = "--";
              //  rw["PhotoNo"] = "--";
            }
          //  dt.Rows.Add(rw);
         //   dataGridView1.DataSource = dt;
            dataGridView2.Refresh();
            CB_StepNo.SelectedIndex =-1;
            CB_Category.SelectedIndex = -1;
            TB_MaterialName.Text = "";
            CB_MaterialShelfNo.SelectedIndex = -1;
            CB_GunNo.SelectedIndex = -1;
            TB_ProgramNo.Text = "";
            TB_MaterialNum.Text = "";
            TB_FeatureCode.Text = "";
            TB_SleeveNo.SelectedIndex = -1;
            CB_PhotoNo.SelectedIndex = -1;
            TB_ReworkTimes.Text = "";
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
        /// 限制只能输入数字
        /// </summary>
        private bool nonNumberEntered = false;
        private void TB_MaterialNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (nonNumberEntered)
            {
                e.Handled = true;
            }
        }

        private void TB_MaterialNum_KeyDown(object sender, KeyEventArgs e)
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

        private void TB_ProgramNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (nonNumberEntered)
            {
                e.Handled = true;
            }
        }

        private void TB_ProgramNo_KeyDown(object sender, KeyEventArgs e)
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

        private void TB_FeatureCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (nonNumberEntered)
            //{
            //    e.Handled = true;
            //}
            this.TB_FeatureCode.ImeMode = ImeMode.Off;
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar >= 'A') && (e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z'))
            {
                e.Handled = false;
            }
            else
            {
                MessageBox.Show("只能输入字母或数字");
                e.Handled = true;

            }
        }

        private void TB_FeatureCode_KeyDown(object sender, KeyEventArgs e)
        {
            //nonNumberEntered = false;
            //if ((e.KeyCode < Keys.D0) || (e.KeyCode > Keys.D9 && e.KeyCode < Keys.NumPad0) || (e.KeyCode > Keys.NumPad9))
            //{
            //    if (e.KeyCode != Keys.Back)
            //    {
            //        nonNumberEntered = true;
            //    }
            //}
        }
        private void TB_ReworkTimes_KeyDown(object sender, KeyEventArgs e)
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

        private void TB_ReworkTimes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (nonNumberEntered)
            {
                e.Handled = true;
            }
        }
        /// <summary>
        /// 限制名字只能是字母和数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TB_MaterialName_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.TB_MaterialName.ImeMode = ImeMode.Off;
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar >= 'A') && (e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z'))
            {
                e.Handled =false;
            }else
            {
                MessageBox.Show("只能输入字母或数字");
                e.Handled = true;

            }
        }

        private void TB_MaterialName_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void TB_ProductionName_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.TB_MaterialName.ImeMode = ImeMode.Off;
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar >= 'A') && (e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z'))
            {
                e.Handled = false;
            }
            else
            {
                MessageBox.Show("只能输入字母或数字");
                e.Handled = true;

            }
        }
        /// <summary>
        /// 当产品编号发生变化的时候查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CB_MaterialNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataBind();
            
        }
        public void dataBind()
        {
            DataTable dt1 = new DataTable();
            dt1 = null;
            String str1 = "SELECT IDT.INTELLIGENTRACK_DETAIL_ID,IDT.INTELLIGENTRACK_ID,IDT.STEP_NO,IDT.CATEGORY," +
                          " IDT.NAME,IDT.MATERIALSHELF_NO,IDT.GUN_NO,IDT.PROGRAME_NO,IDT.MATERIAL_NUMBER,IDT.FEATURE_CODE," +
                          "IDT.SLEEVE_NO,IDT.PHOTO_NO,IDT.REWORK_TIMES,FT.FEERACK_ID,PT.PRODUCTION_ID " +
                          "FROM XH_INTELLIGENTRACK_DETAIL_T IDT,XH_FEERACK_T FT,XH_INTELLIGENTRACK_T IT,XH_PRODUCTION_T PT " +
                          "WHERE FT.FEERACK_ID=PT.FEERACK_ID  " +
                          "AND IT.PRODUCTION_ID=PT.PRODUCTION_ID AND IT.INTELLIGENTRACK_ID=IDT.INTELLIGENTRACK_ID " +
                          "AND IDT.DELETE_FLAG='0' AND PT.DELETE_FLAG='0'AND IT.DELETE_FLAG='0' AND FT.DELETE_FLAG='0'";
            String a;
            String b;
            if (CB_MaterialNo.SelectedIndex >= 0)
            {
                a = "AND PT.PRODUCTION_TYPE=" + (CB_MaterialNo.SelectedIndex + 1);
            }
            else
            {
                a = " AND PT.PRODUCTION_TYPE=0";
            }
            if (CB_Station.SelectedIndex >= 0)
            {
                b = "  AND FT.FEERACK_ID=" + CB_Station.SelectedValue;
            }
            else
            {
                b = "  AND FT.FEERACK_ID=0 ;";
            }
            String sql = str1 + a + b;
            
            dt1 = db.ExecuteDataTable(sql);

            if (dt1.Rows.Count > 0)
            {
            //    String sq = "SELECT  FROM XH_PRODUCTION_T PT,XH_FEERACK_T FT WHERE PT.FEERACK_ID PT.PRODUCTION_TYPE="+;
                dataGridView1.Visible = true;
                dataGridView2.Visible = false;
                dataGridView1.DataSource = dt1;
                dataGridView1.Rows[0].Selected = false;

            }
            else
            {
                dataGridView1.Visible = false;
                dataGridView2.Visible = true;
                dataGridView2.Rows.Clear();
                    

            }
        }
        /// <summary>
        /// 保存事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_MaterialSubmit_Click(object sender, EventArgs e)
        {
            Save();
            dataBind();

        }
        private SqlConnection GetConn() 
        {
            SqlConnection myConn = new SqlConnection("Data Source=.;Initial Catalog=" + GetXml("DataSource", "value") + ";User ID=sa;pwd=" + GetXml("Password", "value"));
            return myConn;
        }
        /// <summary>
        /// 保存方法,且要写在事物中，要么同时成功要么同时失败
        /// </summary>
        public void Save()
        {   
            SqlConnection myConnection = new SqlConnection("Data Source=.;Initial Catalog=" + GetXml("DataSource", "value") + ";User ID=sa;pwd=" + GetXml("Password", "value"));
            try
            {
                
                if (myConnection.State != ConnectionState.Open)
                {
                    myConnection.Open();
                }

                for (int i = 0; i <= this.dataGridView2.Rows.Count - 1; i++)
                {
                   
                    SqlCommand myCommand = new SqlCommand("XH_INTELLIGENTRACK_P", myConnection);
                    myCommand.CommandType = CommandType.StoredProcedure;
                    //存入步序
                    myCommand.Parameters.Add("@STEP_NO", SqlDbType.Int);
                    myCommand.Parameters["@STEP_NO"].Value = Convert.ToInt32(this.dataGridView2.Rows[i].Cells[0].Value);
                    //存入料架号
                    myCommand.Parameters.Add("@FEERACK_ID", SqlDbType.Int);
                    myCommand.Parameters["@FEERACK_ID"].Value = CB_Station.SelectedValue;
                    //存入产品类型
                    myCommand.Parameters.Add("@PRODUCTION_TYPE", SqlDbType.Int);
                    myCommand.Parameters["@PRODUCTION_TYPE"].Value = CB_MaterialNo.SelectedIndex+1;
                    //存入产品名称
                    myCommand.Parameters.Add("@PRODUCTION_NAME", SqlDbType.VarChar);
                    myCommand.Parameters["@PRODUCTION_NAME"].Value = TB_ProductionName.Text;
                    
                    //存入类别
                    myCommand.Parameters.Add("@CATEGORY_NO", SqlDbType.Int);
                    String a = this.dataGridView2.Rows[i].Cells[1].Value.ToString();
                    String[] category = a.Split('.');
                    myCommand.Parameters["@CATEGORY_NO"].Value = Convert.ToInt32(category[0]);
                    //存入名称
                    myCommand.Parameters.Add("@NAME", SqlDbType.VarChar);
                    myCommand.Parameters["@NAME"].Value = this.dataGridView2.Rows[i].Cells[2].Value;
                    //存入料格号
                    if (String.Equals(this.dataGridView2.Rows[i].Cells[3].Value, "--"))
                    {
                        myCommand.Parameters.Add("@MATERIALSHELF_NO", SqlDbType.Int);
                        myCommand.Parameters["@MATERIALSHELF_NO"].Value = 0;  //貌似这里存的好像有点问题，有待验证
                    }
                    else
                    {

                        myCommand.Parameters.Add("@MATERIALSHELF_NO", SqlDbType.Int);
                        myCommand.Parameters["@MATERIALSHELF_NO"].Value = Convert.ToInt32(this.dataGridView2.Rows[i].Cells[3].Value.ToString().Substring(0,1));
                    }
                    //存入枪号
                    if (String.Equals(this.dataGridView2.Rows[i].Cells[5].Value, "--"))
                    {
                        myCommand.Parameters.Add("@GUN_NO", SqlDbType.Int);
                        myCommand.Parameters["@GUN_NO"].Value = 0;  //貌似这里存的好像有点问题，有待验证
                    }
                    else
                    {
                        myCommand.Parameters.Add("@GUN_NO", SqlDbType.Int);
                        myCommand.Parameters["@GUN_NO"].Value = Convert.ToInt32(this.dataGridView2.Rows[i].Cells[5].Value.ToString().Substring(0,1));
                    }
                    //存入程序号
                    if (String.Equals(this.dataGridView2.Rows[i].Cells[6].Value, "--"))
                    {
                        myCommand.Parameters.Add("@PROGRAME_NO", SqlDbType.Int);
                        myCommand.Parameters["@PROGRAME_NO"].Value = 0;  //貌似这里存的好像有点问题，有待验证
                    }
                    else
                    {
                        myCommand.Parameters.Add("@PROGRAME_NO", SqlDbType.Int);
                        myCommand.Parameters["@PROGRAME_NO"].Value = Convert.ToInt32(this.dataGridView2.Rows[i].Cells[6].Value);
                    }
                    //存入数量
                    if (String.Equals(this.dataGridView2.Rows[i].Cells[4].Value, "--"))
                    {
                        myCommand.Parameters.Add("@MATERIAL_NUMBER", SqlDbType.Int);
                        myCommand.Parameters["@MATERIAL_NUMBER"].Value = 0;  //貌似这里存的好像有点问题，有待验证
                    }
                    else
                    {
                        myCommand.Parameters.Add("@MATERIAL_NUMBER", SqlDbType.Int);
                        myCommand.Parameters["@MATERIAL_NUMBER"].Value = Convert.ToInt32(this.dataGridView2.Rows[i].Cells[4].Value);
                    }
                    //存入返工次数
                    if (String.Equals(this.dataGridView2.Rows[i].Cells[10].Value, "--"))
                    {
                        myCommand.Parameters.Add("@REWORK_TIMES", SqlDbType.Int);
                        myCommand.Parameters["@REWORK_TIMES"].Value = 0;  //貌似这里存的好像有点问题，有待验证
                    }
                    else
                    {
                        myCommand.Parameters.Add("@REWORK_TIMES", SqlDbType.Int);
                        myCommand.Parameters["@REWORK_TIMES"].Value = Convert.ToInt32(this.dataGridView2.Rows[i].Cells[10].Value);
                    }
                    //存入特征码
                    if (String.Equals(this.dataGridView2.Rows[i].Cells[7].Value, "--"))
                    {
                        myCommand.Parameters.Add("@FEATURE_CODE", SqlDbType.VarChar);
                        myCommand.Parameters["@FEATURE_CODE"].Value ="--";  //貌似这里存的好像有点问题，有待验证
                    }
                    else
                    {
                        myCommand.Parameters.Add("@FEATURE_CODE", SqlDbType.VarChar);
                        myCommand.Parameters["@FEATURE_CODE"].Value = this.dataGridView2.Rows[i].Cells[7].Value;
                    }
                    //存入套筒号
                    if (String.Equals(this.dataGridView2.Rows[i].Cells[9].Value, "--"))
                    {
                        myCommand.Parameters.Add("@SLEEVE_NO", SqlDbType.Int);
                        myCommand.Parameters["@SLEEVE_NO"].Value = 0;  //貌似这里存的好像有点问题，有待验证
                    }
                    else
                    {
                        myCommand.Parameters.Add("@SLEEVE_NO", SqlDbType.Int);
                        myCommand.Parameters["@SLEEVE_NO"].Value = Convert.ToInt32(this.dataGridView2.Rows[i].Cells[9].Value.ToString().Substring(0,1));
                    }
                    //存入相机号
                    if (String.Equals(this.dataGridView2.Rows[i].Cells[8].Value, "--"))
                    {
                        myCommand.Parameters.Add("@PHOTO_NO", SqlDbType.Int);
                        myCommand.Parameters["@PHOTO_NO"].Value = 0;  //貌似这里存的好像有点问题，有待验证
                    }
                    else
                    {
                        myCommand.Parameters.Add("@PHOTO_NO", SqlDbType.Int);
                        String c = this.dataGridView2.Rows[i].Cells[8].Value.ToString();
                        String photo = c.Substring(0, 1);
                        myCommand.Parameters["@PHOTO_NO"].Value = Convert.ToInt32(photo);
                    }
                    ////传入配置明细ID

                    //if (String.IsNullOrEmpty(Convert.ToString(this.dataGridView2.Rows[i].Cells[11].Value)))
                    //{
                        myCommand.Parameters.Add("@INTELLIGENTRACK_DETAIL_ID", SqlDbType.Int);
                        myCommand.Parameters["@INTELLIGENTRACK_DETAIL_ID"].Value = 0;  //貌似这里存的好像有点问题，有待验证
                    //}
                    //else
                    //{
                    //    myCommand.Parameters.Add("@INTELLIGENTRACK_DETAIL_ID", SqlDbType.Int);
                    //    myCommand.Parameters["@INTELLIGENTRACK_DETAIL_ID"].Value = Convert.ToInt32(this.dataGridView1.Rows[i].Cells[11].Value);
                    //}
                    myCommand.ExecuteNonQuery();
                   
                }
                MessageBox.Show("保存成功！");
                dataBind();
            }
            catch (Exception ex)
            {
                myConnection.Close();
                MessageBox.Show("存入数据出错！");
                
            }
           //关闭连接
                if (myConnection.State == ConnectionState.Open)
                {
                    myConnection.Close();
                }
            

        }
        /// <summary>
        /// 删除配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_Cancel_Click(object sender, EventArgs e)
        {
            int row = Convert.ToInt32(dataGridView1.CurrentCell.RowIndex);
         //   String aaa = dataGridView1.SelectedRows[0].Cells["INTELLIGENTRACK_DATAIL_ID"].Value.ToString();
          
            if (dataGridView1.DataSource == null)
            {
                this.dataGridView1.Rows.RemoveAt(row);
            }
            else
            {
                String sql = "UPDATE XH_INTELLIGENTRACK_DETAIL_T SET DELETE_FLAG='1'WHERE INTELLIGENTRACK_DETAIL_ID=" + Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["INTELLIGENTRACK_DATAIL_ID"].Value.ToString());
                db.ExecuteNonQuery(sql);
                this.dataGridView1.Rows.RemoveAt(row);
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e == null || e.Value == null || !(sender is DataGridView))
                return;

            DataGridView view = (DataGridView)sender;
            object originalValue = e.Value;
            //更改类别显示
            if (view.Columns[e.ColumnIndex].DataPropertyName == "CATEGORY")
                switch ((int)originalValue)
                {
                    case 1:
                        e.Value = (int)originalValue + ".扫描";
                        break;
                    case 2:
                        e.Value = (int)originalValue + ".拧紧";
                        break;
                    case 3:
                        e.Value = (int)originalValue + ".拍照";
                        break;
                    case 4:
                        e.Value = "END";
                        break;
                }
            //转换料格号
            if (view.Columns[e.ColumnIndex].DataPropertyName == "MATERIALSHELF_NO")
            {
                if ((int)originalValue > 0)
                {
                    e.Value = (int)originalValue + "号料格";
                }
             }
        //转换枪号
         if (view.Columns[e.ColumnIndex].DataPropertyName == "GUN_NO")
         {
             if ((int)originalValue>0)
             {
                 e.Value = (int)originalValue + "号枪";
             }
         }
         //转换套筒号
         if (view.Columns[e.ColumnIndex].DataPropertyName == "SLEEVE_NO")
         {
             if ((int)originalValue > 0)
             {
                 e.Value = (int)originalValue + "号套筒";
             }
         }
         //转换相机号
         if (view.Columns[e.ColumnIndex].DataPropertyName == "PHOTO_NO")
         {
             if ((int)originalValue > 0)
             {
                 e.Value = (int)originalValue + "号相机";
             }
         }
                
        }

        private void CB_Station_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void BT_MaterialCancel_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    int a = Convert.ToInt32(this.dataGridView1.Rows[i].Cells["Category"].Value.ToString());
                    String sql = "UPDATE XH_INTELLIGENTRACK_DETAIL_T SET STEP_NO=" + Convert.ToInt32(this.dataGridView1.Rows[i].Cells["StepNo"].Value.ToString()) + " ,CATEGORY=" + Convert.ToInt32(this.dataGridView1.Rows[i].Cells["Category"].Value.ToString()) + ",NAME='" + this.dataGridView1.Rows[i].Cells["Desc"].Value.ToString() + "',MATERIALSHELF_NO=" + Convert.ToInt32(this.dataGridView1.Rows[i].Cells["MaterialShelfNo"].Value.ToString()) + ",GUN_NO=" + Convert.ToInt32(this.dataGridView1.Rows[i].Cells["GunNo"].Value.ToString()) +
                   " ,PROGRAME_NO=" + Convert.ToInt32(this.dataGridView1.Rows[i].Cells["ProgrameNo"].Value.ToString()) + ",MATERIAL_NUMBER=" + Convert.ToInt32(this.dataGridView1.Rows[i].Cells["Number"].Value.ToString()) + ",FEATURE_CODE='" + this.dataGridView1.Rows[i].Cells["FeatureCode"].Value.ToString() + "',SLEEVE_NO=" + Convert.ToInt32(this.dataGridView1.Rows[i].Cells["SleeveNo"].Value.ToString()) + ",PHOTO_NO=" + Convert.ToInt32(this.dataGridView1.Rows[i].Cells["PhotoNo"].Value.ToString()) + ",REWORK_TIMES=" + Convert.ToInt32(this.dataGridView1.Rows[i].Cells["Rework_Time"].Value.ToString()) +
                   " WHERE INTELLIGENTRACK_DETAIL_ID=" + Convert.ToInt32(this.dataGridView1.Rows[i].Cells["INTELLIGENTRACK_DATAIL_ID"].Value.ToString());
                    db.ExecuteNonQuery(sql);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("更新失败！");
            }
        }

        //private void dataGridView1_Click(object sender, EventArgs e)
        //{
        //    dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Selected = true;
        //}

    
    }
}
