using MyOPC;
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
    public partial class ConnectionOpcForm : Form
    {
        public static int feerackId;
        public static int productionType;
        System.Timers.Timer t = new System.Timers.Timer(500);   //实例化Timer类，设置间隔时间为500毫秒；
        public static OPCCreate OPC = null;
        DbUtility db = new DbUtility("Data Source=.;Initial Catalog=" + GetXml("DataSource", "value") + ";User ID=sa;pwd=" + GetXml("Password", "value"), DbProviderType.SqlServer);
        Form1 F=null;
        public ConnectionOpcForm(Form1 f)
        {
            F = f;
            InitializeComponent();
        }
        /// <summary>
        /// 绑定数据源
        /// </summary>
        public void BindData()
        {
            feerackId = Convert.ToInt32(F.CB_Station.SelectedValue);
             productionType = F.CB_MaterialNo.SelectedIndex + 1;
         //   String productionName = F.TB_ProductionName.Text;
            String sql = "SELECT IDT.STEP_NO,IDT.CATEGORY,IDT.NAME,IDT.MATERIALSHELF_NO,IDT.GUN_NO,IDT.MATERIAL_NUMBER,IDT.PROGRAME_NO,IDT.SLEEVE_NO,IDT.FEATURE_CODE,IDT.PHOTO_NO,IDT.REWORK_TIMES,PT.PRODUCTION_NAME " +
                          "FROM XH_FEERACK_T FT,XH_INTELLIGENTRACK_DETAIL_T IDT,XH_INTELLIGENTRACK_T IT,XH_PRODUCTION_T PT" +
                           " WHERE FT.FEERACK_ID=PT.FEERACK_ID AND IT.PRODUCTION_ID=PT.PRODUCTION_ID AND IDT.INTELLIGENTRACK_ID=IT.INTELLIGENTRACK_ID " +
                           "AND FT.FEERACK_ID=" + feerackId + " AND PT.PRODUCTION_TYPE=" + productionType +"AND IDT.DELETE_FLAG='0'"+ ";";// + " AND PT.PRODUCTION_NAME= '" + productionName + "'";
            DataTable dt = new DataTable();
            dt = db.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
            }
            else
            {
                MessageBox.Show("没有为该产品配置步序或保存，请配置且保存后重试！");
                F.Show();
                this.Dispose();
                this.Close();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            F.Show();
            this.Close();
           // Application.Exit();
        }

        private void ConnectionOpcForm_Load(object sender, EventArgs e)
        {
            try
            {
                //监听地址并发送消息
                OPC = new OPCCreate();
            }
            catch
            {
                MessageBox.Show("OPC连接失败！");
            }
        }
        /// <summary>
        /// 监听OPC地址块
        /// </summary>
        public void Opc(object source, System.Timers.ElapsedEventArgs e)
        {
            t.Enabled = false;
            try
            {
                int opcProductionType;
                String plcEmpName;
                int control = Convert.ToInt32((OPC.ReadItem(0).ToString()));
                switch (control)
                {
                    case 1: //请求对比员工号，但是没有发员工的名字，直接写错误9：错误
                        plcEmpName = OPC.ReadItem(1).ToString();
                      //  plcEmpName = "张三";
                        if (String.IsNullOrEmpty(plcEmpName))
                        {

                            OPC.WriteItem(0, 9);
                        }
                        else
                        {
                            String sql = "SELECT ET.EMP_ID  FROM XH_EMP_T ET WHERE ET.EMP_NAME= '" + plcEmpName + "'  AND ET.FEERACK_ID= " + feerackId +" AND ET.DELETE_FLAG='0'"+ ";";
                            DataTable dt = new DataTable();
                            dt = db.ExecuteDataTable(sql);
                            if (dt.Rows.Count > 0)
                            {
                                //员工号通过
                                OPC.WriteItem(0, 2);
                            }
                            else
                            {
                                //员工号不通过
                                OPC.WriteItem(0, 3);
                            }
                        }
                        break;
                    case 11: //请求产品的名称
                          opcProductionType = Convert.ToInt32((OPC.ReadItem(2).ToString()));
                        if (opcProductionType <= 0 && opcProductionType > 10)
                        {
                            //不是产品类型
                            OPC.WriteItem(0, 19);
                        }
                        String sql1 = "SELECT COUNT(IDT.INTELLIGENTRACK_DETAIL_ID)AS STEPNUM,PT.PRODUCTION_NAME FROM XH_PRODUCTION_T PT,XH_INTELLIGENTRACK_T IT,XH_INTELLIGENTRACK_DETAIL_T IDT WHERE IT.PRODUCTION_ID=PT.PRODUCTION_ID AND IDT.INTELLIGENTRACK_ID=IT.INTELLIGENTRACK_ID AND PT.PRODUCTION_TYPE=" + opcProductionType + " AND PT.FEERACK_ID=" + feerackId +" AND IDT.DELETE_FLAG='0'"+ "  GROUP BY PT.PRODUCTION_NAME";
                        //    String sql1 = "SELECT PT.PRODUCTION_NAME FROM XH_PRODUCTION_T PT WHERE PT.PRODUCTION_TYPE=" + opcProductionType + " AND PT.FEERACK_ID=" + Convert.ToInt32(F.CB_Station.SelectedValue) + ";";
                        DataTable dt2 = new DataTable();
                        dt2 = db.ExecuteDataTable(sql1);
                        if (dt2.Rows.Count == 1)
                        {
                            //写入名称
                            OPC.WriteItem(3, dt2.Rows[0]["PRODUCTION_NAME"].ToString());
                            //写入总步数
                            OPC.WriteItem(4, Convert.ToInt32(dt2.Rows[0]["STEPNUM"].ToString()));
                            //完成
                            OPC.WriteItem(0, 12);
                        }
                        else
                        {
                            //错误
                            OPC.WriteItem(0, 19);
                        }

                        break;
                    case 21:  //请求步
                        int reqStep = Convert.ToInt32((OPC.ReadItem(5).ToString()));
                        if (reqStep <= 0)
                        {
                            OPC.WriteItem(0, 29);
                        }
                        else
                        {
                            String sql2 = "SELECT IDT.* FROM XH_PRODUCTION_T PT,XH_INTELLIGENTRACK_T IT,XH_INTELLIGENTRACK_DETAIL_T IDT WHERE IT.PRODUCTION_ID=PT.PRODUCTION_ID AND IDT.INTELLIGENTRACK_ID=IT.INTELLIGENTRACK_ID AND PT.PRODUCTION_TYPE=" + productionType + " AND PT.FEERACK_ID=" + feerackId + "AND IDT.STEP_NO=" + reqStep + " AND IDT.DELETE_FLAG='0'";
                            DataTable dt3 = new DataTable();
                            dt3 = db.ExecuteDataTable(sql2);
                            //写入类别
                            OPC.WriteItem(6, Convert.ToInt32(dt3.Rows[0]["CATEGORY"].ToString()));
                            switch (Convert.ToInt32(dt3.Rows[0]["CATEGORY"].ToString()))
                            {
                                case 1: //扫描
                                    //写入名字
                                    OPC.WriteItem(7, dt3.Rows[0]["NAME"].ToString());
                                    //写入料格号
                                    OPC.WriteItem(8, Convert.ToInt32(dt3.Rows[0]["MATERIALSHELF_NO"].ToString()));
                                    //写入数量
                                    OPC.WriteItem(9, Convert.ToInt32(dt3.Rows[0]["MATERIAL_NUMBER"].ToString()));
                                    // 完成
                                    OPC.WriteItem(0,22);
                                    break;
                                case 2: //拧紧
                                    //写入名字
                                    OPC.WriteItem(7, dt3.Rows[0]["NAME"].ToString());
                                    //写入工具号
                                    OPC.WriteItem(8, Convert.ToInt32(dt3.Rows[0]["GUN_NO"].ToString()));
                                    //请求步套筒号
                                    OPC.WriteItem(10, Convert.ToInt32(dt3.Rows[0]["SLEEVE_NO"].ToString()));
                                    //请求步程序号
                                    OPC.WriteItem(11, Convert.ToInt32(dt3.Rows[0]["PROGRAME_NO"].ToString()));
                                    //写入返工次数
                                    OPC.WriteItem(12, Convert.ToInt32(dt3.Rows[0]["REWORK_TIMES"].ToString()));
                                    //写入数量
                                    OPC.WriteItem(9, Convert.ToInt32(dt3.Rows[0]["MATERIAL_NUMBER"].ToString()));
                                    // 完成
                                    OPC.WriteItem(0, 22);
                                    break;
                                case 3: //照相
                                    //写入名字
                                    OPC.WriteItem(7, dt3.Rows[0]["NAME"].ToString());
                                    //写入工具号
                                    OPC.WriteItem(8, Convert.ToInt32(dt3.Rows[0]["PHOTO_NO"].ToString()));
                                    // 完成
                                    OPC.WriteItem(0, 22);
                                    break;
                            }

                        }
                        break;
                    case 31:
                        String pn = OPC.ReadItem(13).ToString();
                        if (String.IsNullOrEmpty(pn))
                        {
                            //写入为空或错误
                            OPC.WriteItem(0, 39);
                        }
                        else
                        {
                            int type = Convert.ToInt32((OPC.ReadItem(2).ToString()));

                            String sql3 = "SELECT IDT.FEATURE_CODE FROM XH_FEERACK_T FT,XH_INTELLIGENTRACK_DETAIL_T IDT,XH_PRODUCTION_T PT,XH_INTELLIGENTRACK_T IT WHERE FT.FEERACK_ID=PT.FEERACK_ID AND IT.PRODUCTION_ID=PT.PRODUCTION_ID AND IDT.INTELLIGENTRACK_ID=IT.INTELLIGENTRACK_ID AND PT.PRODUCTION_TYPE=" + type + " AND IDT.STEP_NO=" + Convert.ToInt32((OPC.ReadItem(5).ToString())) + " AND FT.FEERACK_ID=" + feerackId + " AND IDT.DELETE_FLAG='0'" + ";";
                            DataTable dt4 = new DataTable();
                            dt4 = db.ExecuteDataTable(sql3);
                            if (String.Equals(pn,dt4.Rows[0]["FEATURE_CODE"].ToString()))
                            {
                                //特征码通过
                                OPC.WriteItem(0, 32);
                            }
                            else
                            {
                                //特征码不通过
                                OPC.WriteItem(0, 33);
                            }
                        }

                        break;
                } 
               t.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("读写出错！");
            }
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

        public void ConnectionOpcForm_Shown(object sender, EventArgs e)
        {
            BindData();
       
            t.Elapsed += new System.Timers.ElapsedEventHandler(Opc); //到达时间的时候执行事件；   
            t.AutoReset = true;   //设置是执行一次（false）还是一直执行(true)；   
            t.Enabled = true;  
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
                if ((int)originalValue > 0)
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
    }
}
