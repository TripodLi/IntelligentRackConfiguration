using MySql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace IntelligentRackConfiguration
{
    public partial class ChangeConfigForm : Form
    {
        Form1 F =null;
        int INTELLIGENTRACK_DETAIL_T = 0;
        int selectrow=-1;
        DataTable tb2 = new DataTable();
        DbUtility db = new DbUtility("server=" + GetXml("DataSource", "value") + "; " + "database=" + GetXml("InitialCatalog", "value") + ";User Id=" + GetXml("UserId", "value") + ";pwd=" + GetXml("Password", "value"), DbProviderType.SqlServer);
        public ChangeConfigForm(Form1 f)
        {
            F = f;
            InitializeComponent();
        }
       
        private void ChangeConfigForm_Load(object sender, EventArgs e)
        {
            //数据绑定
            String str = "SELECT FEERACK_ID,FEERACK_NAME,PRINTEDBOOKS_ID FROM XH_FEERACK_T WHERE DELETE_FLAG='0'";
            //绑定料架
            DataTable dt = new DataTable();
            dt = db.ExecuteDataTable(str);
            CB_Station.DataSource = dt;
            CB_Station.ValueMember = "FEERACK_ID";
            CB_Station.DisplayMember = "FEERACK_NAME";
            //绑定表格
            DataTable tb1 = (DataTable)F.dataGridView1.DataSource;
            tb2 = tb1.Copy();
            this.dataGridView1.DataSource = tb2;
            this.dataGridView1.ClearSelection();
            //for (int i = 0; i < tb1.Rows.Count; i++)
            //{
            //    dataGridView1.Rows.Add();//在gridview2中添加一空行
            //    //为空行添加列值
            //    for (int j = 0; j < tb1.Rows[i].; j++)
            //    {
            //        dataGridView1.Rows[i].Cells[j].Value = F.dataGridView1.Rows[i].Cells[j].Value;
            //    }
            //}
        }
        /// <summary>
        /// 删除配置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            int row = Convert.ToInt32(dataGridView1.CurrentCell.RowIndex);
            //   String aaa = dataGridView1.SelectedRows[0].Cells["INTELLIGENTRACK_DATAIL_ID"].Value.ToString();
            //点击"是(YES)"退出程序
            if (Convert.ToInt32(tb2.Rows[row]["INTELLIGENTRACK_DETAIL_ID"].ToString()) != 0)
            {
                if (MessageBox.Show("确定要删除?", "安全提示",
                        System.Windows.Forms.MessageBoxButtons.YesNo,
                        System.Windows.Forms.MessageBoxIcon.Warning)
                == System.Windows.Forms.DialogResult.Yes)
                {
                    String sql = "UPDATE XH_INTELLIGENTRACK_DETAIL_T SET DELETE_FLAG='1'WHERE INTELLIGENTRACK_DETAIL_ID=" + Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["INTELLIGENTRACK_DATAIL_ID"].Value.ToString());
                    db.ExecuteNonQuery(sql);
                    this.dataGridView1.Rows.RemoveAt(row);
                }
            }
            else
            {
                this.dataGridView1.Rows.RemoveAt(row);
            }
            
            
        }
        /// <summary>
        /// 增加行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
                this.Dispose();
                this.Close();
            }
            catch
            {
                MessageBox.Show("存储配置出错！");
            }
           
           
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
        //    if (e == null || e.Value == null || !(sender is DataGridView))
        //        return;

        //    DataGridView view = (DataGridView)sender;
        //    object originalValue = e.Value;
        //    //更改类别显示
        //    if (view.Columns[e.ColumnIndex].DataPropertyName == "CATEGORY")
        //        switch ((int)originalValue)
        //        {
        //            case 1:
        //                e.Value = (int)originalValue + ".扫描";
        //                break;
        //            case 2:
        //                e.Value = (int)originalValue + ".拧紧";
        //                break;
        //            case 3:
        //                e.Value = (int)originalValue + ".拍照";
        //                break;
        //            case 4:
        //                e.Value = "END";
        //                break;
        //        }
        //    //转换料格号
        //    if (view.Columns[e.ColumnIndex].DataPropertyName == "MATERIALSHELF_NO")
        //    {
        //        if ((int)originalValue > 0)
        //        {
        //            e.Value = (int)originalValue + "号料格";
        //        }
        //    }
        //    //转换枪号
        //    if (view.Columns[e.ColumnIndex].DataPropertyName == "GUN_NO")
        //    {
        //        if ((int)originalValue > 0)
        //        {
        //            e.Value = (int)originalValue + "号枪";
        //        }
        //    }
        //    //转换套筒号
        //    if (view.Columns[e.ColumnIndex].DataPropertyName == "SLEEVE_NO")
        //    {
        //        if ((int)originalValue > 0)
        //        {
        //            e.Value = (int)originalValue + "号套筒";
        //        }
        //    }
        //    //转换相机号
        //    if (view.Columns[e.ColumnIndex].DataPropertyName == "PHOTO_NO")
        //    {
        //        if ((int)originalValue > 0)
        //        {
        //            e.Value = (int)originalValue + "号相机";
        //        }
        //    }
        }
        /// <summary>
        /// 整理步序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < tb2.Rows.Count;i++ )
            {
                tb2.Rows[i]["STEP_NO"] = i + 1;
            }
            dataGridView1.Refresh();
        }

        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
           
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
          //  int row = Convert.ToInt32(dataGridView1.CurrentCell.RowIndex);

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
        /// 关闭窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }
        /// <summary>
        /// 最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }
        /// <summary>
        /// 移动窗体
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private void ChangeConfigForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x02, 0);
            }
        }
        /// <summary>
        /// 页面逻辑
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

                    Sleevepanel.Visible = false;
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
                    Sleevepanel.Visible = true;
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

                    Sleevepanel.Visible = false;
                    Gunpanel.Visible = false;
                    Programpanel.Visible = false;
                    PhotoNopanel.Visible = true;
                    TB_MaterialName.Visible = true;
                    LB_MaterailName.Visible = true;
                    label3.Visible = true;
                    break;
                default:
                    MaterialShelfpanel.Visible = false;
                    MaterialNumpanel.Visible = false;
                    FeatureCodepanel.Visible = false;
                    Reworkpanel.Visible = false;
                    //  Buttonpanel.Visible = true;

                    Sleevepanel.Visible = false;
                    Gunpanel.Visible = false;
                    Programpanel.Visible = false;
                    PhotoNopanel.Visible = false;
                    TB_MaterialName.Visible = false;
                    LB_MaterailName.Visible = false;
                    label3.Visible = false;
                    break;

            }
        }
      
        /// <summary>
        /// 填充每个控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = Convert.ToInt32(dataGridView1.CurrentCell.RowIndex);
            selectrow = row;
            INTELLIGENTRACK_DETAIL_T =Convert.ToInt32(tb2.Rows[row]["INTELLIGENTRACK_DETAIL_ID"].ToString());
            CB_Station.SelectedValue = tb2.Rows[row]["FEERACK_ID"];
            anthername.Text = tb2.Rows[row]["ANOTHERNAME"].ToString();
            CB_MaterialNo.SelectedIndex =Convert.ToInt32(tb2.Rows[row]["PRODUCTION_TYPE"].ToString())-1;
            TB_ProductionName.Text = tb2.Rows[row]["PRODUCTION_NAME"].ToString();
            CB_StepNo.SelectedItem = tb2.Rows[row]["STEP_NO"].ToString();
            CB_Category.SelectedIndex = Convert.ToInt32(tb2.Rows[row]["CATEGORY"].ToString()) - 1;
            TB_MaterialName.Text = tb2.Rows[row]["NAME"].ToString();
            TB_SleeveNo.SelectedIndex = Convert.ToInt32(tb2.Rows[row]["SLEEVE_NO"].ToString()) - 1;
            CB_MaterialShelfNo.SelectedIndex = Convert.ToInt32(tb2.Rows[row]["MATERIALSHELF_NO"].ToString()) - 1;
            TB_MaterialNum.Text = tb2.Rows[row]["MATERIAL_NUMBER"].ToString();
            CB_GunNo.SelectedIndex = Convert.ToInt32(tb2.Rows[row]["GUN_NO"].ToString()) - 1;
            TB_ProgramNo.Text = tb2.Rows[row]["PROGRAME_NO"].ToString();
            TB_FeatureCode.Text = tb2.Rows[row]["FEATURE_CODE"].ToString();
            CB_PhotoNo.SelectedIndex = Convert.ToInt32(tb2.Rows[row]["PHOTO_NO"].ToString()) - 1;
            TB_ReworkTimes.Text = tb2.Rows[row]["REWORK_TIMES"].ToString();
        }
        /// <summary>
        /// 保存方法,且要写在事物中，要么同时成功要么同时失败
        /// </summary>
        public void Save()
        {
            SqlConnection myConnection = new SqlConnection("server=" + GetXml("DataSource", "value") + "; " + "database=" + GetXml("InitialCatalog", "value") + ";User Id=" + GetXml("UserId", "value") + ";pwd=" + GetXml("Password", "value"));
            try
            {

                if (myConnection.State != ConnectionState.Open)
                {
                    myConnection.Open();
                }
                
                for (int i = 0; i <= tb2.Rows.Count - 1; i++)
                {
                    SqlCommand myCommand = new SqlCommand("XH_INTELLIGENTRACK_P", myConnection);
                    myCommand.CommandType = CommandType.StoredProcedure;
                    //存入步序
                    myCommand.Parameters.Add("@STEP_NO", SqlDbType.Int);
                    myCommand.Parameters["@STEP_NO"].Value = Convert.ToInt32(tb2.Rows[i]["STEP_NO"].ToString());
                    //存入料架号
                    myCommand.Parameters.Add("@FEERACK_ID", SqlDbType.Int);
                    myCommand.Parameters["@FEERACK_ID"].Value = Convert.ToInt32(tb2.Rows[i]["FEERACK_ID"].ToString());
                    //存入产品类型
                    myCommand.Parameters.Add("@PRODUCTION_TYPE", SqlDbType.Int);
                    myCommand.Parameters["@PRODUCTION_TYPE"].Value = Convert.ToInt32(tb2.Rows[i]["PRODUCTION_TYPE"].ToString());
                    //存入产品名称
                    myCommand.Parameters.Add("@PRODUCTION_NAME", SqlDbType.VarChar);
                    myCommand.Parameters["@PRODUCTION_NAME"].Value = tb2.Rows[i]["PRODUCTION_NAME"].ToString();

                    //存入类别
                    myCommand.Parameters.Add("@CATEGORY_NO", SqlDbType.Int);
                    //String a = this.dataGridView1.Rows[i].Cells[1].Value.ToString();
                    //String[] category = a.Split('.');
                    myCommand.Parameters["@CATEGORY_NO"].Value = Convert.ToInt32(tb2.Rows[i]["CATEGORY"].ToString());
                    //存入名称
                    myCommand.Parameters.Add("@NAME", SqlDbType.VarChar);
                    myCommand.Parameters["@NAME"].Value = tb2.Rows[i]["NAME"].ToString();
                    //存入料格号
                    //if (String.Equals(this.dataGridView1.Rows[i].Cells[3].Value, "--"))
                    //{
                        myCommand.Parameters.Add("@MATERIALSHELF_NO", SqlDbType.Int);
                        myCommand.Parameters["@MATERIALSHELF_NO"].Value = Convert.ToInt32(tb2.Rows[i]["MATERIALSHELF_NO"].ToString()); 
                    //}
                    //else
                    //{

                    //    myCommand.Parameters.Add("@MATERIALSHELF_NO", SqlDbType.Int);
                    //    myCommand.Parameters["@MATERIALSHELF_NO"].Value = Convert.ToInt32(this.dataGridView1.Rows[i].Cells[3].Value.ToString().Substring(0, 1));
                    //}
                    //存入枪号
                    //if (String.Equals(this.dataGridView1.Rows[i].Cells[5].Value, "--"))
                    //{
                        myCommand.Parameters.Add("@GUN_NO", SqlDbType.Int);
                        myCommand.Parameters["@GUN_NO"].Value = Convert.ToInt32(tb2.Rows[i]["GUN_NO"].ToString());  //貌似这里存的好像有点问题，有待验证
                    //}
                    //else
                    //{
                    //    myCommand.Parameters.Add("@GUN_NO", SqlDbType.Int);
                    //    myCommand.Parameters["@GUN_NO"].Value = Convert.ToInt32(this.dataGridView1.Rows[i].Cells[5].Value.ToString().Substring(0, 1));
                    //}
                    //存入程序号
                    //if (String.Equals(this.dataGridView1.Rows[i].Cells[6].Value, "--"))
                    //{
                        myCommand.Parameters.Add("@PROGRAME_NO", SqlDbType.Int);
                        myCommand.Parameters["@PROGRAME_NO"].Value = Convert.ToInt32(tb2.Rows[i]["PROGRAME_NO"].ToString());  
                    //}
                    //else
                    //{
                    //    myCommand.Parameters.Add("@PROGRAME_NO", SqlDbType.Int);
                    //    myCommand.Parameters["@PROGRAME_NO"].Value = Convert.ToInt32(this.dataGridView1.Rows[i].Cells[6].Value);
                    //}
                    //存入数量
                    //if (String.Equals(this.dataGridView1.Rows[i].Cells[4].Value, "--"))
                    //{
                        myCommand.Parameters.Add("@MATERIAL_NUMBER", SqlDbType.Int);
                        myCommand.Parameters["@MATERIAL_NUMBER"].Value = Convert.ToInt32(tb2.Rows[i]["MATERIAL_NUMBER"].ToString());  
                    //}
                    //else
                    //{
                    //    myCommand.Parameters.Add("@MATERIAL_NUMBER", SqlDbType.Int);
                    //    myCommand.Parameters["@MATERIAL_NUMBER"].Value = Convert.ToInt32(this.dataGridView1.Rows[i].Cells[4].Value);
                    //}
                    //存入返工次数
                    //if (String.Equals(this.dataGridView1.Rows[i].Cells[10].Value, "--"))
                    //{
                    //    myCommand.Parameters.Add("@REWORK_TIMES", SqlDbType.Int);
                    //    myCommand.Parameters["@REWORK_TIMES"].Value = 0;  //貌似这里存的好像有点问题，有待验证
                    //}
                    //else
                    //{
                        myCommand.Parameters.Add("@REWORK_TIMES", SqlDbType.Int);
                        myCommand.Parameters["@REWORK_TIMES"].Value = Convert.ToInt32(tb2.Rows[i]["REWORK_TIMES"].ToString());
                    //}
                    //存入特征码
                    //if (String.Equals(this.dataGridView1.Rows[i].Cells[7].Value, "--"))
                    //{
                    //    myCommand.Parameters.Add("@FEATURE_CODE", SqlDbType.VarChar);
                    //    myCommand.Parameters["@FEATURE_CODE"].Value = "--";  //貌似这里存的好像有点问题，有待验证
                    //}
                    //else
                    //{
                        myCommand.Parameters.Add("@FEATURE_CODE", SqlDbType.VarChar);
                        myCommand.Parameters["@FEATURE_CODE"].Value = tb2.Rows[i]["FEATURE_CODE"].ToString();
                    //}
                    //存入套筒号
                    //if (String.Equals(this.dataGridView1.Rows[i].Cells[9].Value, "--"))
                    //{
                    //    myCommand.Parameters.Add("@SLEEVE_NO", SqlDbType.Int);
                    //    myCommand.Parameters["@SLEEVE_NO"].Value = 0;  //貌似这里存的好像有点问题，有待验证
                    //}
                    //else
                    //{
                        myCommand.Parameters.Add("@SLEEVE_NO", SqlDbType.Int);
                        myCommand.Parameters["@SLEEVE_NO"].Value = Convert.ToInt32(tb2.Rows[i]["SLEEVE_NO"].ToString());
                    //}
                    ////存入相机号
                    //if (String.Equals(this.dataGridView1.Rows[i].Cells[8].Value, "--"))
                    //{
                    //    myCommand.Parameters.Add("@PHOTO_NO", SqlDbType.Int);
                    //    myCommand.Parameters["@PHOTO_NO"].Value = 0;  //貌似这里存的好像有点问题，有待验证
                    //}
                    //else
                    //{
                        myCommand.Parameters.Add("@PHOTO_NO", SqlDbType.Int);
                        //String c = this.dataGridView1.Rows[i].Cells[8].Value.ToString();
                        //String photo = c.Substring(0, 1);
                        myCommand.Parameters["@PHOTO_NO"].Value = Convert.ToInt32(tb2.Rows[i]["PHOTO_NO"].ToString());
                    //}
                    ////传入配置明细ID

                    //if (String.IsNullOrEmpty(Convert.ToString(this.dataGridView2.Rows[i].Cells[11].Value)))
                    //{
                        if (Convert.ToInt32(tb2.Rows[i]["INTELLIGENTRACK_DETAIL_ID"].ToString()) > 0)
                        {
                            myCommand.Parameters.Add("@INTELLIGENTRACK_DETAIL_ID", SqlDbType.Int);
                            myCommand.Parameters["@INTELLIGENTRACK_DETAIL_ID"].Value = Convert.ToInt32(tb2.Rows[i]["INTELLIGENTRACK_DETAIL_ID"].ToString());
                        }
                        else
                        {
                            myCommand.Parameters.Add("@INTELLIGENTRACK_DETAIL_ID", SqlDbType.Int);
                            myCommand.Parameters["@INTELLIGENTRACK_DETAIL_ID"].Value = 0;
                        }
                    //}
                    //else
                    //{
                    //    myCommand.Parameters.Add("@INTELLIGENTRACK_DETAIL_ID", SqlDbType.Int);
                    //    myCommand.Parameters["@INTELLIGENTRACK_DETAIL_ID"].Value = Convert.ToInt32(this.dataGridView1.Rows[i].Cells[11].Value);
                    //}
                    myCommand.ExecuteNonQuery();

                }
                MessageBox.Show("保存成功！");
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
        /// 提交工作步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox6_Click(object sender, EventArgs e)
        {
            int row = Convert.ToInt32(dataGridView1.CurrentCell.RowIndex);
            if (selectrow == row)
            {
                //步序赋值
                tb2.Rows[row]["STEP_NO"] = CB_StepNo.SelectedItem.ToString();
                //类别赋值
                tb2.Rows[row]["CATEGORY"] = CB_Category.SelectedIndex + 1;
                //名称
                tb2.Rows[row]["NAME"] = TB_MaterialName.Text;
                ////返工次数
                if (!String.IsNullOrEmpty(TB_ReworkTimes.Text))
                {
                    tb2.Rows[row]["REWORK_TIMES"] = Convert.ToInt32(TB_ReworkTimes.Text);
                }
                else
                {
                    tb2.Rows[row]["REWORK_TIMES"] = 0;
                }
                //料格号
                if (CB_MaterialShelfNo.SelectedIndex >= 0)
                {
                    tb2.Rows[row]["MATERIALSHELF_NO"] = CB_MaterialShelfNo.SelectedIndex + 1;
                }
                else
                {
                    tb2.Rows[row]["MATERIALSHELF_NO"] = 0;
                }
                //枪号
                if (CB_GunNo.SelectedIndex >= 0)
                {
                    tb2.Rows[row]["GUN_NO"] = CB_GunNo.SelectedIndex + 1;
                }
                else
                {
                    tb2.Rows[row]["GUN_NO"] = 0;
                }
                //程序号
                if (!String.IsNullOrEmpty(TB_ProgramNo.Text))
                {
                    tb2.Rows[row]["PROGRAME_NO"] = Convert.ToInt32(TB_ProgramNo.Text);
                }
                else
                {
                    tb2.Rows[row]["PROGRAME_NO"] = 0;
                }
                //数量
                if (!String.IsNullOrEmpty(TB_MaterialNum.Text))
                {
                    tb2.Rows[row]["MATERIAL_NUMBER"] = Convert.ToInt32(TB_MaterialNum.Text);
                }
                else
                {
                    tb2.Rows[row]["MATERIAL_NUMBER"] = 0;

                }
                //特征码
                if (!String.IsNullOrEmpty(TB_FeatureCode.Text))
                {
                    tb2.Rows[row]["FEATURE_CODE"] = TB_FeatureCode.Text;
                }
                else
                {
                    tb2.Rows[row]["FEATURE_CODE"] = "--";
                }
                //套筒号
                if (TB_SleeveNo.SelectedIndex >= 0)
                {
                    tb2.Rows[row]["SLEEVE_NO"] = TB_SleeveNo.SelectedIndex + 1;
                }
                else
                {
                    tb2.Rows[row]["SLEEVE_NO"] = 0;
                }
                //相机号
                if (CB_PhotoNo.SelectedIndex >= 0)
                {
                    tb2.Rows[row]["PHOTO_NO"] = CB_PhotoNo.SelectedIndex + 1;
                }
                else
                {
                    tb2.Rows[row]["PHOTO_NO"] = 0;
                }
                //配置明细ID
                //  tb2.Rows[row]["INTELLIGENTRACK_DETAIL_ID"] = 0;
                //产品名称
                tb2.Rows[row]["PRODUCTION_NAME"] = TB_ProductionName.Text;
                //产品类型
                tb2.Rows[row]["PRODUCTION_TYPE"] = CB_MaterialNo.SelectedIndex + 1;
                //料架号
                tb2.Rows[row]["FEERACK_ID"] = CB_Station.SelectedValue;
                //别名
                tb2.Rows[row]["ANOTHERNAME"] = anthername.Text;
                selectrow = -1;
            }
            else
            {
                //绑定的datatable增加一行
                tb2.Rows.Add();
                //步序赋值
                tb2.Rows[tb2.Rows.Count - 1]["STEP_NO"] = CB_StepNo.SelectedItem.ToString();
                //类别赋值
                tb2.Rows[tb2.Rows.Count - 1]["CATEGORY"] = CB_Category.SelectedIndex + 1;
                //名称
                tb2.Rows[tb2.Rows.Count - 1]["NAME"] = TB_MaterialName.Text;
                ////返工次数
                if (!String.IsNullOrEmpty(TB_ReworkTimes.Text))
                {
                    tb2.Rows[tb2.Rows.Count - 1]["REWORK_TIMES"] = Convert.ToInt32(TB_ReworkTimes.Text);
                }
                else
                {
                    tb2.Rows[tb2.Rows.Count - 1]["REWORK_TIMES"] = 0;
                }
                //料格号
                if (CB_MaterialShelfNo.SelectedIndex >= 0)
                {
                    tb2.Rows[tb2.Rows.Count - 1]["MATERIALSHELF_NO"] = CB_MaterialShelfNo.SelectedIndex + 1;
                }
                else
                {
                    tb2.Rows[tb2.Rows.Count - 1]["MATERIALSHELF_NO"] = 0;
                }
                //枪号
                if (CB_GunNo.SelectedIndex >= 0)
                {
                    tb2.Rows[tb2.Rows.Count - 1]["GUN_NO"] = CB_GunNo.SelectedIndex + 1;
                }
                else
                {
                    tb2.Rows[tb2.Rows.Count - 1]["GUN_NO"] = 0;
                }
                //程序号
                if (!String.IsNullOrEmpty(TB_ProgramNo.Text))
                {
                    tb2.Rows[tb2.Rows.Count - 1]["PROGRAME_NO"] = Convert.ToInt32(TB_ProgramNo.Text);
                }
                else
                {
                    tb2.Rows[tb2.Rows.Count - 1]["PROGRAME_NO"] = 0;
                }
                //数量
                if (!String.IsNullOrEmpty(TB_MaterialNum.Text))
                {
                    tb2.Rows[tb2.Rows.Count - 1]["MATERIAL_NUMBER"] = Convert.ToInt32(TB_MaterialNum.Text);
                }
                else
                {
                    tb2.Rows[tb2.Rows.Count - 1]["MATERIAL_NUMBER"] = 0;

                }
                //特征码
                if (!String.IsNullOrEmpty(TB_FeatureCode.Text))
                {
                    tb2.Rows[tb2.Rows.Count - 1]["FEATURE_CODE"] = TB_FeatureCode.Text;
                }
                else
                {
                    tb2.Rows[tb2.Rows.Count - 1]["FEATURE_CODE"] = "--";
                }
                //套筒号
                if (TB_SleeveNo.SelectedIndex >= 0)
                {
                    tb2.Rows[tb2.Rows.Count - 1]["SLEEVE_NO"] = TB_SleeveNo.SelectedIndex + 1;
                }
                else
                {
                    tb2.Rows[tb2.Rows.Count - 1]["SLEEVE_NO"] = 0;
                }
                //相机号
                if (CB_PhotoNo.SelectedIndex >= 0)
                {
                    tb2.Rows[tb2.Rows.Count - 1]["PHOTO_NO"] = CB_PhotoNo.SelectedIndex + 1;
                }
                else
                {
                    tb2.Rows[tb2.Rows.Count - 1]["PHOTO_NO"] = 0;
                }
                //配置明细ID
                tb2.Rows[tb2.Rows.Count - 1]["INTELLIGENTRACK_DETAIL_ID"] = 0;
                //产品名称
                tb2.Rows[tb2.Rows.Count - 1]["PRODUCTION_NAME"] = TB_ProductionName.Text;
                //产品类型
                tb2.Rows[tb2.Rows.Count - 1]["PRODUCTION_TYPE"] = CB_MaterialNo.SelectedIndex + 1;
                //料架号
                tb2.Rows[tb2.Rows.Count - 1]["FEERACK_ID"] = CB_Station.SelectedValue;
                //别名
                tb2.Rows[tb2.Rows.Count - 1]["ANOTHERNAME"] = anthername.Text;
            }
            //料架号
            dataGridView1.Refresh();
            CB_StepNo.SelectedIndex = -1;
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
        /// 步序上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentRow == null)
            {

                MessageBox.Show("请选择要需要操作的步序所在行");

            }
            else
            {
                if (this.dataGridView1.CurrentRow.Index <= 0)
                {

                    MessageBox.Show("此步序已在顶端，不能再上移！");

                }

                else
                {

                    int nowIndex = this.dataGridView1.CurrentRow.Index;

                    object[] _rowData = (this.dataGridView1.DataSource as DataTable).Rows[nowIndex].ItemArray;

                    (this.dataGridView1.DataSource as DataTable).Rows[nowIndex].ItemArray = (this.dataGridView1.DataSource as DataTable).Rows[nowIndex - 1].ItemArray;

                    (this.dataGridView1.DataSource as DataTable).Rows[nowIndex - 1].ItemArray = _rowData;

                    this.dataGridView1.CurrentCell = this.dataGridView1.Rows[nowIndex - 1].Cells[0];//设定当前行

                }
            }
        }
        /// <summary>
        /// 步序下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentRow == null)
            {

                MessageBox.Show("请选择要需要操作的步序所在行");

            }
            else
            {
                if (this.dataGridView1.CurrentRow.Index >= this.dataGridView1.Rows.Count - 1)
                {

                    MessageBox.Show("此步序已在底端，不能再下移！");

                }

                else
                {

                    int nowIndex = this.dataGridView1.CurrentRow.Index;

                    object[] _rowData = (this.dataGridView1.DataSource as DataTable).Rows[nowIndex].ItemArray;

                    (this.dataGridView1.DataSource as DataTable).Rows[nowIndex].ItemArray = (this.dataGridView1.DataSource as DataTable).Rows[nowIndex + 1].ItemArray;

                    (this.dataGridView1.DataSource as DataTable).Rows[nowIndex + 1].ItemArray = _rowData;

                    this.dataGridView1.CurrentCell = this.dataGridView1.Rows[nowIndex + 1].Cells[0];//设定当前行

                }
            }
        }
    }
}
