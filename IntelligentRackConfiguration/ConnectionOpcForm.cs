using MyOPC;
using MySql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Xml;

namespace IntelligentRackConfiguration
{
    public partial class ConnectionOpcForm : Form
    {
        public int beatcount = 0;
        public int renectioncount = 0;
        public static int feerackId;
        Boolean myselfBeat;
       // public static int productionType;
        /// <summary>
        /// 写日志时加锁
        /// </summary>
        private static object m_Lock = new object();
        /// <summary>
        /// 程序启动路径
        /// </summary>
        public static string BasePath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName);
        //实例化Timer类，设置间隔时间为500毫秒；
        public static OPCCreate OPC = null;
      //  DbUtility db = new DbUtility("Data Source=.;Initial Catalog=" + GetXml("DataSource", "value") + ";User ID=sa;pwd=" + GetXml("Password", "value"), DbProviderType.SqlServer);
        DbUtility db = new DbUtility("Data Source=" +GetXml("DataSource", "value") + ";Initial Catalog=" + GetXml("InitialCatalog", "value") + ";User ID=" + GetXml("UserId", "value") + ";pwd=" + GetXml("Password", "value"), DbProviderType.SqlServer);
        Form1 F = null;
        int[] canRead = new int[2] { 0, 0 };
        int count = 0;
        int reqStepLog; //记录上一步请求步
        DataTable log = new DataTable();
        
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
            //feerackId = Convert.ToInt32(F.CB_Station.SelectedValue);
            //productionType = F.CB_MaterialNo.SelectedIndex + 1;
            //String productionName = F.TB_ProductionName.Text;
            //String sql = "SELECT IDT.STEP_NO,IDT.CATEGORY,IDT.NAME,IDT.MATERIALSHELF_NO,IDT.GUN_NO,IDT.MATERIAL_NUMBER,IDT.PROGRAME_NO,IDT.SLEEVE_NO,IDT.FEATURE_CODE,IDT.PHOTO_NO,IDT.REWORK_TIMES,PT.PRODUCTION_NAME " +
            //              "FROM XH_FEERACK_T FT,XH_INTELLIGENTRACK_DETAIL_T IDT,XH_INTELLIGENTRACK_T IT,XH_PRODUCTION_T PT" +
            //               " WHERE FT.FEERACK_ID=PT.FEERACK_ID AND IT.PRODUCTION_ID=PT.PRODUCTION_ID AND IDT.INTELLIGENTRACK_ID=IT.INTELLIGENTRACK_ID " +
            //               "AND FT.FEERACK_ID=" + feerackId + " AND PT.PRODUCTION_TYPE=" + productionType + "AND IDT.DELETE_FLAG='0'" + ";";// + " AND PT.PRODUCTION_NAME= '" + productionName + "'";
            //DataTable dt = new DataTable();
            //dt = db.ExecuteDataTable(sql);
            //if (dt.Rows.Count > 0)
            //{
            //    dataGridView1.DataSource = dt;
            //}
            //else
            //{
            //    WriteLog("没有为该产品配置步序或保存，请配置且保存后重试！");
            //    MessageBox.Show("没有为该产品配置步序或保存，请配置且保存后重试！");
            //    F.Show();
            //    this.Dispose();
            //    this.Close();

            //}
        }
        public void LogShown(object source, System.Timers.ElapsedEventArgs e)
        {
            //OverrideTimer t = (OverrideTimer)source;
            //t.Stop();
            //try
            //{
            //    String sql1 = "delete from dbo.XH_CONFIG_LOG where DT BETWEEN '" + System.DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd") + " 00:00:00'" + " AND '" + System.DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59';";
            //    db.ExecuteNonQuery(sql1);
            //    String sql = "select  * from dbo.XH_CONFIG_LOG where DT BETWEEN '" + System.DateTime.Today.ToString("yyyy-MM-dd") + " 00:00:00'" + " AND '" + System.DateTime.Today.ToString("yyyy-MM-dd") + " 23:59:59'"+" order by  CONFIG_ID desc"+";";
            //    log = db.ExecuteDataTable(sql);
            //    BeginInvoke((MethodInvoker)delegate() { dataGridView1.DataSource = log; });
            //    BeginInvoke((MethodInvoker)delegate() { dataGridView1.Refresh(); });
            //}
            //catch
            //{

            //}
            //finally
            //{
            //    t.Start();
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            F.Show();
            this.Dispose();
            this.Close();
            OPC.EventDataChanged -= OnEventDataChanged;
            OPC = null;
          //  OPC.DisConnect();
            // Application.Exit();
        }

        private void ConnectionOpcForm_Load(object sender, EventArgs e)
        {
            try
            {
                //监听地址并发送消息
                OPC = new OPCCreate();
                OPC.EventDataChanged += new EventDataChanged(OnEventDataChanged);
                OPC.Run();
            }
            catch
            {
                MessageBox.Show("OPC连接失败！");
                WriteLog("OPC连接失败");
            }
        
        }
        /// <summary>
        /// 监听OPC地址块
        /// </summary>
        public void Opc(object source, System.Timers.ElapsedEventArgs e)
        {
            
            OverrideTimer t = (OverrideTimer)source;
            try
            {
               
                //t.AutoReset = false;
                t.Stop();
                string feerack = t.Feerackno;
                int feerackId = t.FeerackId;
                int[] canread = t.CanRead;
                int opcProductionType;
                String plcEmpName;
                int feerackControl = Convert.ToInt32(GetOpcConfigXml(feerack, "1"));
                int control = Convert.ToInt32((OPC.ReadItem(feerackControl).ToString()));
                canread[0] = control;
                if (canread[0] != canread[1])
                {
                    switch (control)
                    {
                        case 1: //请求对比员工号，但是没有发员工的名字，直接写错误9：错误
                            plcEmpName = OPC.ReadItem(Convert.ToInt32(GetOpcConfigXml(feerack, "2"))).ToString();
                            //  plcEmpName = "张三";
                            if (String.IsNullOrEmpty(plcEmpName))
                            {

                                plcEmpName = OPC.ReadItem(Convert.ToInt32(GetOpcConfigXml(feerack, "2"))).ToString();
                                if (String.IsNullOrEmpty(plcEmpName))
                                {
                                    plcEmpName = OPC.ReadItem(Convert.ToInt32(GetOpcConfigXml(feerack, "2"))).ToString();
                                    if (String.IsNullOrEmpty(plcEmpName))
                                    {
                                        OPC.WriteItem(feerackControl, 9);
                                        String sql = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerack + "#料架','对比员工号',1,9,'错误')";
                                        db.ExecuteNonQuery(sql);
                                    }
                                    else
                                    {
                                        String sql = "SELECT ET.EMP_ID  FROM XH_EMP_T ET WHERE ET.EMP_NAME= '" + plcEmpName + "'  AND ET.FEERACK_ID= " + feerackId + " AND ET.DELETE_FLAG='0'" + ";";
                                        DataTable dt = new DataTable();
                                        dt = db.ExecuteDataTable(sql);
                                        if (dt.Rows.Count > 0)
                                        {
                                            //员工号通过
                                            OPC.WriteItem(feerackControl, 2);
                                            String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerack + "#料架','对比员工号',1,2,'对比员工号通过')";
                                            db.ExecuteNonQuery(sql1);
                                            // WriteLog("员工号对比-请求信号： "+control + "写入结果： "+ 2+" 表示员工号通过");
                                        }
                                        else
                                        {
                                            //员工号不通过
                                            OPC.WriteItem(feerackControl, 3);
                                            String sql2 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerack + "#料架','对比员工号',1,3,'对比员工号不通过')";
                                            db.ExecuteNonQuery(sql2);
                                            //  WriteLog("员工号对比-请求信号： " + control + " 写入结果： " + 3 + " 表示员工号不通过");
                                        }
                                    }
                                }
                                else
                                {
                                    String sql = "SELECT ET.EMP_ID  FROM XH_EMP_T ET WHERE ET.EMP_NAME= '" + plcEmpName + "'  AND ET.FEERACK_ID= " + feerackId + " AND ET.DELETE_FLAG='0'" + ";";
                                    DataTable dt = new DataTable();
                                    dt = db.ExecuteDataTable(sql);
                                    if (dt.Rows.Count > 0)
                                    {
                                        //员工号通过
                                        OPC.WriteItem(feerackControl, 2);
                                        String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerack + "#料架','对比员工号',1,2,'对比员工号通过')";
                                        db.ExecuteNonQuery(sql1);
                                        // WriteLog("员工号对比-请求信号： "+control + "写入结果： "+ 2+" 表示员工号通过");
                                    }
                                    else
                                    {
                                        //员工号不通过
                                        OPC.WriteItem(feerackControl, 3);
                                        String sql2 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerack + "#料架','对比员工号',1,3,'对比员工号不通过')";
                                        db.ExecuteNonQuery(sql2);
                                        //  WriteLog("员工号对比-请求信号： " + control + " 写入结果： " + 3 + " 表示员工号不通过");
                                    }
                                }
                              
                               //  WriteLog("员工号对比-请求信号： "+control + "写入结果： "+ 9 +" 表示地址块中员工号为空");
                            }
                            else
                            {
                                String sql = "SELECT ET.EMP_ID  FROM XH_EMP_T ET WHERE ET.EMP_NAME= '" + plcEmpName + "'  AND ET.FEERACK_ID= " + feerackId + " AND ET.DELETE_FLAG='0'" + ";";
                                DataTable dt = new DataTable();
                                dt = db.ExecuteDataTable(sql);
                                if (dt.Rows.Count > 0)
                                {
                                    //员工号通过
                                    OPC.WriteItem(feerackControl, 2);
                                    String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerack + "#料架','对比员工号',1,2,'对比员工号通过')";
                                    db.ExecuteNonQuery(sql1);
                                    // WriteLog("员工号对比-请求信号： "+control + "写入结果： "+ 2+" 表示员工号通过");
                                }
                                else
                                {
                                    //员工号不通过
                                    OPC.WriteItem(feerackControl, 3);
                                    String sql2 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerack + "#料架','对比员工号',1,3,'对比员工号不通过')";
                                    db.ExecuteNonQuery(sql2);
                                  //  WriteLog("员工号对比-请求信号： " + control + " 写入结果： " + 3 + " 表示员工号不通过");
                                }
                            }
                            break;
                        case 11: //请求产品的名称
                            opcProductionType = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetOpcConfigXml(feerack, "3"))).ToString()));
                            if (opcProductionType <= 0 && opcProductionType > 20)
                            {
                                opcProductionType = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetOpcConfigXml(feerack, "3"))).ToString()));
                                if (opcProductionType <= 0 && opcProductionType > 20)
                                {
                                    opcProductionType = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetOpcConfigXml(feerack, "3"))).ToString()));
                                    if (opcProductionType <= 0 && opcProductionType > 20)
                                    {
                                        //不是产品类型
                                        OPC.WriteItem(feerackControl, 19);
                                        String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerack + "#料架','请求产品名称和总步数',11,19,'请求产品名称错误')";
                                        db.ExecuteNonQuery(sql1);
                                    }
                                    else
                                    {
                                        String sql10 = "SELECT COUNT(IDT.INTELLIGENTRACK_DETAIL_ID)AS STEPNUM,PT.PRODUCTION_NAME FROM XH_PRODUCTION_T PT,XH_INTELLIGENTRACK_T IT,XH_INTELLIGENTRACK_DETAIL_T IDT WHERE IT.PRODUCTION_ID=PT.PRODUCTION_ID AND IDT.INTELLIGENTRACK_ID=IT.INTELLIGENTRACK_ID AND PT.PRODUCTION_TYPE=" + opcProductionType + " AND PT.FEERACK_ID=" + feerackId + " AND IDT.DELETE_FLAG='0'" + "  GROUP BY PT.PRODUCTION_NAME";
                                        //    String sql1 = "SELECT PT.PRODUCTION_NAME FROM XH_PRODUCTION_T PT WHERE PT.PRODUCTION_TYPE=" + opcProductionType + " AND PT.FEERACK_ID=" + Convert.ToInt32(F.CB_Station.SelectedValue) + ";";
                                        DataTable dt2 = new DataTable();
                                        dt2 = db.ExecuteDataTable(sql10);
                                        if (dt2.Rows.Count == 1)
                                        {
                                            //写入名称
                                            OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "4")), dt2.Rows[0]["PRODUCTION_NAME"].ToString());
                                            //写入总步数
                                            OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "5")), Convert.ToInt32(dt2.Rows[0]["STEPNUM"].ToString()));
                                            //完成
                                            OPC.WriteItem(feerackControl, 12);
                                            String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,PRODUCTION_NAME,STEP_TOAL,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerack + "#料架','请求产品名称和总步数',11,'" + dt2.Rows[0]["PRODUCTION_NAME"].ToString() + "','" + dt2.Rows[0]["STEPNUM"].ToString() + "',12,'请求产品名称和总步数成功');";
                                            db.ExecuteNonQuery(sql1);
                                            //  WriteLog("请求产品名称-请求信号： " + control + " 写入产品名称： " + dt2.Rows[0]["PRODUCTION_NAME"].ToString() + "写入总步数： " + Convert.ToInt32(dt2.Rows[0]["STEPNUM"].ToString())+" 完成信号： "+12);
                                        }
                                        else
                                        {
                                            //错误
                                            OPC.WriteItem(feerackControl, 19);
                                            String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerack + "#料架','请求产品名称和总步数',11,19,'请求产品名称错误')";
                                            db.ExecuteNonQuery(sql1);
                                            //  WriteLog("请求产品名称-请求信号： " + control + " 写入结果： " + 19 + " 表示不是产品类型");
                                        }
                                    }
                                }
                                else
                                {
                                    String sql10 = "SELECT COUNT(IDT.INTELLIGENTRACK_DETAIL_ID)AS STEPNUM,PT.PRODUCTION_NAME FROM XH_PRODUCTION_T PT,XH_INTELLIGENTRACK_T IT,XH_INTELLIGENTRACK_DETAIL_T IDT WHERE IT.PRODUCTION_ID=PT.PRODUCTION_ID AND IDT.INTELLIGENTRACK_ID=IT.INTELLIGENTRACK_ID AND PT.PRODUCTION_TYPE=" + opcProductionType + " AND PT.FEERACK_ID=" + feerackId + " AND IDT.DELETE_FLAG='0'" + "  GROUP BY PT.PRODUCTION_NAME";
                                    //    String sql1 = "SELECT PT.PRODUCTION_NAME FROM XH_PRODUCTION_T PT WHERE PT.PRODUCTION_TYPE=" + opcProductionType + " AND PT.FEERACK_ID=" + Convert.ToInt32(F.CB_Station.SelectedValue) + ";";
                                    DataTable dt2 = new DataTable();
                                    dt2 = db.ExecuteDataTable(sql10);
                                    if (dt2.Rows.Count == 1)
                                    {
                                        //写入名称
                                        OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "4")), dt2.Rows[0]["PRODUCTION_NAME"].ToString());
                                        //写入总步数
                                        OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "5")), Convert.ToInt32(dt2.Rows[0]["STEPNUM"].ToString()));
                                        //完成
                                        OPC.WriteItem(feerackControl, 12);
                                        String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,PRODUCTION_NAME,STEP_TOAL,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerack + "#料架','请求产品名称和总步数',11,'" + dt2.Rows[0]["PRODUCTION_NAME"].ToString() + "','" + dt2.Rows[0]["STEPNUM"].ToString() + "',12,'请求产品名称和总步数成功');";
                                        db.ExecuteNonQuery(sql1);
                                        //  WriteLog("请求产品名称-请求信号： " + control + " 写入产品名称： " + dt2.Rows[0]["PRODUCTION_NAME"].ToString() + "写入总步数： " + Convert.ToInt32(dt2.Rows[0]["STEPNUM"].ToString())+" 完成信号： "+12);
                                    }
                                    else
                                    {
                                        //错误
                                        OPC.WriteItem(feerackControl, 19);
                                        String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerack + "#料架','请求产品名称和总步数',11,19,'请求产品名称错误')";
                                        db.ExecuteNonQuery(sql1);
                                        //  WriteLog("请求产品名称-请求信号： " + control + " 写入结果： " + 19 + " 表示不是产品类型");
                                    }
                                }
                                 
                                //   WriteLog("请求产品名称-请求信号： " + control + " 写入结果： " + 19 + " 表示不是产品类型");
                            }
                            else
                            {
                                String sql10 = "SELECT COUNT(IDT.INTELLIGENTRACK_DETAIL_ID)AS STEPNUM,PT.PRODUCTION_NAME FROM XH_PRODUCTION_T PT,XH_INTELLIGENTRACK_T IT,XH_INTELLIGENTRACK_DETAIL_T IDT WHERE IT.PRODUCTION_ID=PT.PRODUCTION_ID AND IDT.INTELLIGENTRACK_ID=IT.INTELLIGENTRACK_ID AND PT.PRODUCTION_TYPE=" + opcProductionType + " AND PT.FEERACK_ID=" + feerackId + " AND IDT.DELETE_FLAG='0'" + "  GROUP BY PT.PRODUCTION_NAME";
                                //    String sql1 = "SELECT PT.PRODUCTION_NAME FROM XH_PRODUCTION_T PT WHERE PT.PRODUCTION_TYPE=" + opcProductionType + " AND PT.FEERACK_ID=" + Convert.ToInt32(F.CB_Station.SelectedValue) + ";";
                                DataTable dt2 = new DataTable();
                                dt2 = db.ExecuteDataTable(sql10);
                                if (dt2.Rows.Count == 1)
                                {
                                    //写入名称
                                    OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "4")), dt2.Rows[0]["PRODUCTION_NAME"].ToString());
                                    //写入总步数
                                    OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "5")), Convert.ToInt32(dt2.Rows[0]["STEPNUM"].ToString()));
                                    //完成
                                    OPC.WriteItem(feerackControl, 12);
                                    String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,PRODUCTION_NAME,STEP_TOAL,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerack + "#料架','请求产品名称和总步数',11,'" + dt2.Rows[0]["PRODUCTION_NAME"].ToString() + "','" + dt2.Rows[0]["STEPNUM"].ToString() + "',12,'请求产品名称和总步数成功');";
                                    db.ExecuteNonQuery(sql1);
                                    //  WriteLog("请求产品名称-请求信号： " + control + " 写入产品名称： " + dt2.Rows[0]["PRODUCTION_NAME"].ToString() + "写入总步数： " + Convert.ToInt32(dt2.Rows[0]["STEPNUM"].ToString())+" 完成信号： "+12);
                                }
                                else
                                {
                                    //错误
                                    OPC.WriteItem(feerackControl, 19);
                                    String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerack + "#料架','请求产品名称和总步数',11,19,'请求产品名称错误')";
                                    db.ExecuteNonQuery(sql1);
                                    //  WriteLog("请求产品名称-请求信号： " + control + " 写入结果： " + 19 + " 表示不是产品类型");
                                }
                            }
                            break;
                        case 21:  //请求步
                            opcProductionType = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetOpcConfigXml(feerack, "3"))).ToString()));
                            int reqStep = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetOpcConfigXml(feerack, "6"))).ToString()));
                            if (reqStep <= 0)
                            {
                                reqStep = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetOpcConfigXml(feerack, "6"))).ToString()));
                                if (reqStep <= 0)
                                {
                                    reqStep = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetOpcConfigXml(feerack, "6"))).ToString()));
                                    if (reqStep <= 0)
                                    {
                                        OPC.WriteItem(feerackControl, 29);
                                        String sql = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,STEP_NO,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerack + "#料架','请求步',21,'" + reqStep.ToString() + "',29,'请求步错误');";
                                        db.ExecuteNonQuery(sql);
                                    }
                                    else
                                    {
                                        opcProductionType = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetOpcConfigXml(feerack, "3"))).ToString()));
                                        String sql2 = "SELECT IDT.* FROM XH_PRODUCTION_T PT,XH_INTELLIGENTRACK_T IT,XH_INTELLIGENTRACK_DETAIL_T IDT WHERE IT.PRODUCTION_ID=PT.PRODUCTION_ID AND IDT.INTELLIGENTRACK_ID=IT.INTELLIGENTRACK_ID AND PT.PRODUCTION_TYPE=" + opcProductionType + " AND PT.FEERACK_ID=" + feerackId + "AND IDT.STEP_NO=" + reqStep + " AND IDT.DELETE_FLAG='0'";
                                        DataTable dt3 = new DataTable();
                                        dt3 = db.ExecuteDataTable(sql2);
                                        //写入类别
                                        //int aaa = Convert.ToInt32(GetOpcConfigXml(feerack, "7"));
                                        //string bbb = dt3.Rows[0]["CATEGORY"].ToString();
                                        OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "7")), Convert.ToInt32(dt3.Rows[0]["CATEGORY"].ToString()));
                                        switch (Convert.ToInt32(dt3.Rows[0]["CATEGORY"].ToString()))
                                        {
                                            case 1: //扫描
                                                //写入名字
                                                OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "8")), dt3.Rows[0]["NAME"].ToString());
                                                //写入料格号
                                                OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "9")), Convert.ToInt32(dt3.Rows[0]["MATERIALSHELF_NO"].ToString()));
                                                //写入数量
                                                OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "10")), Convert.ToInt32(dt3.Rows[0]["MATERIAL_NUMBER"].ToString()));
                                                // 完成
                                                OPC.WriteItem(feerackControl, 22);
                                                String sql = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,STEP_NO,CATEGORY,NAME,MATERIALSHELF_NO,MATERIAL_NUMBER,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerack + "#料架','请求步',21,'" + reqStep.ToString() + "','" + dt3.Rows[0]["CATEGORY"].ToString() + "','" + dt3.Rows[0]["NAME"].ToString() + "','" + dt3.Rows[0]["MATERIALSHELF_NO"].ToString() + "号料格','" + dt3.Rows[0]["MATERIAL_NUMBER"].ToString() + "',22,'请求步-扫描：成功');";
                                                db.ExecuteNonQuery(sql);
                                                //  WriteLog("请求步-请求信号： " + control + " 类别: " + Convert.ToInt32(dt3.Rows[0]["CATEGORY"].ToString()) + " 名字：" + dt3.Rows[0]["NAME"].ToString() + "料格号： " + Convert.ToInt32(dt3.Rows[0]["MATERIALSHELF_NO"].ToString()) + " 数量：" + Convert.ToInt32(dt3.Rows[0]["MATERIAL_NUMBER"].ToString())+" 完成信号："+22);
                                                break;
                                            case 2: //拧紧
                                                //写入名字
                                                OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "8")), dt3.Rows[0]["NAME"].ToString());
                                                //写入工具号
                                                OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "9")), Convert.ToInt32(dt3.Rows[0]["GUN_NO"].ToString()));
                                                //请求步套筒号
                                                OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "11")), Convert.ToInt32(dt3.Rows[0]["SLEEVE_NO"].ToString()));
                                                //请求步程序号
                                                OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "12")), Convert.ToInt32(dt3.Rows[0]["PROGRAME_NO"].ToString()));
                                                //写入返工次数
                                                OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "13")), Convert.ToInt32(dt3.Rows[0]["REWORK_TIMES"].ToString()));
                                                //写入数量
                                                OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "10")), Convert.ToInt32(dt3.Rows[0]["MATERIAL_NUMBER"].ToString()));
                                                // 完成
                                                OPC.WriteItem(feerackControl, 22);
                                                String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,STEP_NO,CATEGORY,NAME,GUN_NO,SLEEVE_NO,PROGRAME_NO,REWORK_TIMES,MATERIAL_NUMBER,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerack + "#料架','请求步',21,'" + reqStep.ToString() + "','" + dt3.Rows[0]["CATEGORY"].ToString() + "','" + dt3.Rows[0]["NAME"].ToString() + "','" + dt3.Rows[0]["GUN_NO"].ToString() + "号枪','" + dt3.Rows[0]["SLEEVE_NO"].ToString() + "号套筒','" + dt3.Rows[0]["PROGRAME_NO"].ToString() + "','" + dt3.Rows[0]["REWORK_TIMES"].ToString() + "次','" + dt3.Rows[0]["MATERIAL_NUMBER"].ToString() + "'22,'请求步-拧紧：成功');";
                                                db.ExecuteNonQuery(sql1);
                                                //  WriteLog("请求步-请求信号： " + control + " 类别: " + Convert.ToInt32(dt3.Rows[0]["CATEGORY"].ToString()) + " 名字：" + dt3.Rows[0]["NAME"].ToString() + "工具号： " + Convert.ToInt32(dt3.Rows[0]["GUN_NO"].ToString()) + " 套筒号：" + Convert.ToInt32(dt3.Rows[0]["SLEEVE_NO"].ToString()) + "程序号： " + Convert.ToInt32(dt3.Rows[0]["PROGRAME_NO"].ToString()) + "返工次数：" + Convert.ToInt32(dt3.Rows[0]["REWORK_TIMES"].ToString()) + "数量： " + Convert.ToInt32(dt3.Rows[0]["MATERIAL_NUMBER"].ToString()) + "完成信号：" + 22);
                                                break;
                                            case 3: //照相
                                                //写入名字
                                                OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "8")), dt3.Rows[0]["NAME"].ToString());
                                                //写入工具号
                                                OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "9")), Convert.ToInt32(dt3.Rows[0]["PHOTO_NO"].ToString()));
                                                // 完成
                                                OPC.WriteItem(feerackControl, 22);
                                                String sql3 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,STEP_NO,CATEGORY,NAME,PHOTO_NO,RESULT,SIGNIFICANCE)VALUES(GETDATE(),'" + feerack + "#料架','请求步',21,'" + reqStep.ToString() + "','" + dt3.Rows[0]["CATEGORY"].ToString() + "','" + dt3.Rows[0]["NAME"].ToString() + "','" + dt3.Rows[0]["PHOTO_NO"].ToString() + "号相机,22,'请求步-照相：成功');";
                                                db.ExecuteNonQuery(sql3);
                                                //  WriteLog("请求步-请求信号： " + control + " 类别: " + Convert.ToInt32(dt3.Rows[0]["CATEGORY"].ToString()) + " 名字：" + dt3.Rows[0]["NAME"].ToString() + "相机号： " + Convert.ToInt32(dt3.Rows[0]["PHOTO_NO"].ToString()) + "完成信号：" + 22);
                                                break;
                                            case 4:
                                                OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "7")), "10");
                                                OPC.WriteItem(feerackControl, 22);
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    opcProductionType = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetOpcConfigXml(feerack, "3"))).ToString()));
                                    String sql2 = "SELECT IDT.* FROM XH_PRODUCTION_T PT,XH_INTELLIGENTRACK_T IT,XH_INTELLIGENTRACK_DETAIL_T IDT WHERE IT.PRODUCTION_ID=PT.PRODUCTION_ID AND IDT.INTELLIGENTRACK_ID=IT.INTELLIGENTRACK_ID AND PT.PRODUCTION_TYPE=" + opcProductionType + " AND PT.FEERACK_ID=" + feerackId + "AND IDT.STEP_NO=" + reqStep + " AND IDT.DELETE_FLAG='0'";
                                    DataTable dt3 = new DataTable();
                                    dt3 = db.ExecuteDataTable(sql2);
                                    //写入类别
                                    //int aaa = Convert.ToInt32(GetOpcConfigXml(feerack, "7"));
                                    //string bbb = dt3.Rows[0]["CATEGORY"].ToString();
                                    OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "7")), Convert.ToInt32(dt3.Rows[0]["CATEGORY"].ToString()));
                                    switch (Convert.ToInt32(dt3.Rows[0]["CATEGORY"].ToString()))
                                    {
                                        case 1: //扫描
                                            //写入名字
                                            OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "8")), dt3.Rows[0]["NAME"].ToString());
                                            //写入料格号
                                            OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "9")), Convert.ToInt32(dt3.Rows[0]["MATERIALSHELF_NO"].ToString()));
                                            //写入数量
                                            OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "10")), Convert.ToInt32(dt3.Rows[0]["MATERIAL_NUMBER"].ToString()));
                                            // 完成
                                            OPC.WriteItem(feerackControl, 22);
                                            String sql = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,STEP_NO,CATEGORY,NAME,MATERIALSHELF_NO,MATERIAL_NUMBER,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerack + "#料架','请求步',21,'" + reqStep.ToString() + "','" + dt3.Rows[0]["CATEGORY"].ToString() + "','" + dt3.Rows[0]["NAME"].ToString() + "','" + dt3.Rows[0]["MATERIALSHELF_NO"].ToString() + "号料格','" + dt3.Rows[0]["MATERIAL_NUMBER"].ToString() + "',22,'请求步-扫描：成功');";
                                            db.ExecuteNonQuery(sql);
                                            //  WriteLog("请求步-请求信号： " + control + " 类别: " + Convert.ToInt32(dt3.Rows[0]["CATEGORY"].ToString()) + " 名字：" + dt3.Rows[0]["NAME"].ToString() + "料格号： " + Convert.ToInt32(dt3.Rows[0]["MATERIALSHELF_NO"].ToString()) + " 数量：" + Convert.ToInt32(dt3.Rows[0]["MATERIAL_NUMBER"].ToString())+" 完成信号："+22);
                                            break;
                                        case 2: //拧紧
                                            //写入名字
                                            OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "8")), dt3.Rows[0]["NAME"].ToString());
                                            //写入工具号
                                            OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "9")), Convert.ToInt32(dt3.Rows[0]["GUN_NO"].ToString()));
                                            //请求步套筒号
                                            OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "11")), Convert.ToInt32(dt3.Rows[0]["SLEEVE_NO"].ToString()));
                                            //请求步程序号
                                            OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "12")), Convert.ToInt32(dt3.Rows[0]["PROGRAME_NO"].ToString()));
                                            //写入返工次数
                                            OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "13")), Convert.ToInt32(dt3.Rows[0]["REWORK_TIMES"].ToString()));
                                            //写入数量
                                            OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "10")), Convert.ToInt32(dt3.Rows[0]["MATERIAL_NUMBER"].ToString()));
                                            // 完成
                                            OPC.WriteItem(feerackControl, 22);
                                            String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,STEP_NO,CATEGORY,NAME,GUN_NO,SLEEVE_NO,PROGRAME_NO,REWORK_TIMES,MATERIAL_NUMBER,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerack + "#料架','请求步',21,'" + reqStep.ToString() + "','" + dt3.Rows[0]["CATEGORY"].ToString() + "','" + dt3.Rows[0]["NAME"].ToString() + "','" + dt3.Rows[0]["GUN_NO"].ToString() + "号枪','" + dt3.Rows[0]["SLEEVE_NO"].ToString() + "号套筒','" + dt3.Rows[0]["PROGRAME_NO"].ToString() + "','" + dt3.Rows[0]["REWORK_TIMES"].ToString() + "次','" + dt3.Rows[0]["MATERIAL_NUMBER"].ToString() + "'22,'请求步-拧紧：成功');";
                                            db.ExecuteNonQuery(sql1);
                                            //  WriteLog("请求步-请求信号： " + control + " 类别: " + Convert.ToInt32(dt3.Rows[0]["CATEGORY"].ToString()) + " 名字：" + dt3.Rows[0]["NAME"].ToString() + "工具号： " + Convert.ToInt32(dt3.Rows[0]["GUN_NO"].ToString()) + " 套筒号：" + Convert.ToInt32(dt3.Rows[0]["SLEEVE_NO"].ToString()) + "程序号： " + Convert.ToInt32(dt3.Rows[0]["PROGRAME_NO"].ToString()) + "返工次数：" + Convert.ToInt32(dt3.Rows[0]["REWORK_TIMES"].ToString()) + "数量： " + Convert.ToInt32(dt3.Rows[0]["MATERIAL_NUMBER"].ToString()) + "完成信号：" + 22);
                                            break;
                                        case 3: //照相
                                            //写入名字
                                            OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "8")), dt3.Rows[0]["NAME"].ToString());
                                            //写入工具号
                                            OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "9")), Convert.ToInt32(dt3.Rows[0]["PHOTO_NO"].ToString()));
                                            // 完成
                                            OPC.WriteItem(feerackControl, 22);
                                            String sql3 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,STEP_NO,CATEGORY,NAME,PHOTO_NO,RESULT,SIGNIFICANCE)VALUES(GETDATE(),'" + feerack + "#料架','请求步',21,'" + reqStep.ToString() + "','" + dt3.Rows[0]["CATEGORY"].ToString() + "','" + dt3.Rows[0]["NAME"].ToString() + "','" + dt3.Rows[0]["PHOTO_NO"].ToString() + "号相机,22,'请求步-照相：成功');";
                                            db.ExecuteNonQuery(sql3);
                                            //  WriteLog("请求步-请求信号： " + control + " 类别: " + Convert.ToInt32(dt3.Rows[0]["CATEGORY"].ToString()) + " 名字：" + dt3.Rows[0]["NAME"].ToString() + "相机号： " + Convert.ToInt32(dt3.Rows[0]["PHOTO_NO"].ToString()) + "完成信号：" + 22);
                                            break;
                                        case 4:
                                            OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "7")), "10");
                                            OPC.WriteItem(feerackControl, 22);
                                            break;
                                    }
                                }
                               
                               // WriteLog("请求步-请求信号： " + control + " 写入结果： " + 29 + " 表示不是产品类型");
                            }
                            else
                            {
                                opcProductionType = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetOpcConfigXml(feerack, "3"))).ToString()));
                                String sql2 = "SELECT IDT.* FROM XH_PRODUCTION_T PT,XH_INTELLIGENTRACK_T IT,XH_INTELLIGENTRACK_DETAIL_T IDT WHERE IT.PRODUCTION_ID=PT.PRODUCTION_ID AND IDT.INTELLIGENTRACK_ID=IT.INTELLIGENTRACK_ID AND PT.PRODUCTION_TYPE=" + opcProductionType + " AND PT.FEERACK_ID=" + feerackId + "AND IDT.STEP_NO=" + reqStep + " AND IDT.DELETE_FLAG='0'";
                                DataTable dt3 = new DataTable();
                                dt3 = db.ExecuteDataTable(sql2);
                                //写入类别
                                //int aaa = Convert.ToInt32(GetOpcConfigXml(feerack, "7"));
                                //string bbb = dt3.Rows[0]["CATEGORY"].ToString();
                                OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "7")), Convert.ToInt32(dt3.Rows[0]["CATEGORY"].ToString()));
                                switch (Convert.ToInt32(dt3.Rows[0]["CATEGORY"].ToString()))
                                {
                                    case 1: //扫描
                                        //写入名字
                                        OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "8")), dt3.Rows[0]["NAME"].ToString());
                                        //写入料格号
                                        OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "9")), Convert.ToInt32(dt3.Rows[0]["MATERIALSHELF_NO"].ToString()));
                                        //写入数量
                                        OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "10")), Convert.ToInt32(dt3.Rows[0]["MATERIAL_NUMBER"].ToString()));
                                        // 完成
                                        OPC.WriteItem(feerackControl, 22);
                                        String sql = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,STEP_NO,CATEGORY,NAME,MATERIALSHELF_NO,MATERIAL_NUMBER,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerack + "#料架','请求步',21,'"+reqStep.ToString()+"','" + dt3.Rows[0]["CATEGORY"].ToString() + "','" + dt3.Rows[0]["NAME"].ToString() + "','" + dt3.Rows[0]["MATERIALSHELF_NO"].ToString() + "号料格','" + dt3.Rows[0]["MATERIAL_NUMBER"].ToString() + "',22,'请求步-扫描：成功');";
                                        db.ExecuteNonQuery(sql);
                                      //  WriteLog("请求步-请求信号： " + control + " 类别: " + Convert.ToInt32(dt3.Rows[0]["CATEGORY"].ToString()) + " 名字：" + dt3.Rows[0]["NAME"].ToString() + "料格号： " + Convert.ToInt32(dt3.Rows[0]["MATERIALSHELF_NO"].ToString()) + " 数量：" + Convert.ToInt32(dt3.Rows[0]["MATERIAL_NUMBER"].ToString())+" 完成信号："+22);
                                        break;
                                    case 2: //拧紧
                                        //写入名字
                                        OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "8")), dt3.Rows[0]["NAME"].ToString());
                                        //写入工具号
                                        OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "9")), Convert.ToInt32(dt3.Rows[0]["GUN_NO"].ToString()));
                                        //请求步套筒号
                                        OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "11")), Convert.ToInt32(dt3.Rows[0]["SLEEVE_NO"].ToString()));
                                        //请求步程序号
                                        OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "12")), Convert.ToInt32(dt3.Rows[0]["PROGRAME_NO"].ToString()));
                                        //写入返工次数
                                        OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "13")), Convert.ToInt32(dt3.Rows[0]["REWORK_TIMES"].ToString()));
                                        //写入数量
                                        OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "10")), Convert.ToInt32(dt3.Rows[0]["MATERIAL_NUMBER"].ToString()));
                                        // 完成
                                        OPC.WriteItem(feerackControl, 22);
                                        String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,STEP_NO,CATEGORY,NAME,GUN_NO,SLEEVE_NO,PROGRAME_NO,REWORK_TIMES,MATERIAL_NUMBER,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerack + "#料架','请求步',21,'" + reqStep.ToString() + "','" + dt3.Rows[0]["CATEGORY"].ToString() + "','" + dt3.Rows[0]["NAME"].ToString() + "','" + dt3.Rows[0]["GUN_NO"].ToString() + "号枪','" + dt3.Rows[0]["SLEEVE_NO"].ToString() + "号套筒','" + dt3.Rows[0]["PROGRAME_NO"].ToString() + "','" + dt3.Rows[0]["REWORK_TIMES"].ToString() + "次','" + dt3.Rows[0]["MATERIAL_NUMBER"].ToString() + "'22,'请求步-拧紧：成功');";
                                        db.ExecuteNonQuery(sql1);
                                      //  WriteLog("请求步-请求信号： " + control + " 类别: " + Convert.ToInt32(dt3.Rows[0]["CATEGORY"].ToString()) + " 名字：" + dt3.Rows[0]["NAME"].ToString() + "工具号： " + Convert.ToInt32(dt3.Rows[0]["GUN_NO"].ToString()) + " 套筒号：" + Convert.ToInt32(dt3.Rows[0]["SLEEVE_NO"].ToString()) + "程序号： " + Convert.ToInt32(dt3.Rows[0]["PROGRAME_NO"].ToString()) + "返工次数：" + Convert.ToInt32(dt3.Rows[0]["REWORK_TIMES"].ToString()) + "数量： " + Convert.ToInt32(dt3.Rows[0]["MATERIAL_NUMBER"].ToString()) + "完成信号：" + 22);
                                        break;
                                    case 3: //照相
                                        //写入名字
                                        OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "8")), dt3.Rows[0]["NAME"].ToString());
                                        //写入工具号
                                        OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "9")), Convert.ToInt32(dt3.Rows[0]["PHOTO_NO"].ToString()));
                                        // 完成
                                        OPC.WriteItem(feerackControl, 22);
                                        String sql3 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,STEP_NO,CATEGORY,NAME,PHOTO_NO,RESULT,SIGNIFICANCE)VALUES(GETDATE(),'" + feerack + "#料架','请求步',21,'" + reqStep.ToString() + "','" + dt3.Rows[0]["CATEGORY"].ToString() + "','" + dt3.Rows[0]["NAME"].ToString() + "','" + dt3.Rows[0]["PHOTO_NO"].ToString() + "号相机,22,'请求步-照相：成功');";
                                        db.ExecuteNonQuery(sql3);
                                      //  WriteLog("请求步-请求信号： " + control + " 类别: " + Convert.ToInt32(dt3.Rows[0]["CATEGORY"].ToString()) + " 名字：" + dt3.Rows[0]["NAME"].ToString() + "相机号： " + Convert.ToInt32(dt3.Rows[0]["PHOTO_NO"].ToString()) + "完成信号：" + 22);
                                        break;
                                    case 4:
                                        OPC.WriteItem(Convert.ToInt32(GetOpcConfigXml(feerack, "7")), "10");
                                        OPC.WriteItem(feerackControl, 22);
                                        break;
                                }

                            }
                            break;
                        case 31: //对比特征码                        

                            String pn = OPC.ReadItem(Convert.ToInt32(GetOpcConfigXml(feerack, "14"))).ToString();
                            if (String.IsNullOrEmpty(pn))  //  && Convert.ToInt32(OPC.ReadItem(feerackControl)) == 31)
                            {
                                pn = OPC.ReadItem(Convert.ToInt32(GetOpcConfigXml(feerack, "14"))).ToString();
                                if (String.IsNullOrEmpty(pn))// && Convert.ToInt32(OPC.ReadItem(feerackControl)) == 31)
                                {
                                    pn = OPC.ReadItem(Convert.ToInt32(GetOpcConfigXml(feerack, "14"))).ToString();
                                    if (String.IsNullOrEmpty(pn))// && Convert.ToInt32(OPC.ReadItem(feerackControl)) == 31)
                                    {
                                        //写入为空或错误
                                        OPC.WriteItem(feerackControl, 39);
                                        String sql = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,PN,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerack + "#料架','对比特征码',31,'" + pn + "',39,'请求对比特征码错误');";
                                        db.ExecuteNonQuery(sql);
                                    }
                                    else
                                    {
                                        int type = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetOpcConfigXml(feerack, "3"))).ToString()));
                                        String sql3 = "SELECT IDT.FEATURE_CODE FROM XH_FEERACK_T FT,XH_INTELLIGENTRACK_DETAIL_T IDT,XH_PRODUCTION_T PT,XH_INTELLIGENTRACK_T IT WHERE FT.FEERACK_ID=PT.FEERACK_ID AND IT.PRODUCTION_ID=PT.PRODUCTION_ID AND IDT.INTELLIGENTRACK_ID=IT.INTELLIGENTRACK_ID AND PT.PRODUCTION_TYPE=" + type + " AND IDT.STEP_NO=" + Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetOpcConfigXml(feerack, "6"))).ToString())) + " AND FT.FEERACK_ID=" + feerackId + " AND IDT.DELETE_FLAG='0'" + ";";
                                        DataTable dt4 = new DataTable();
                                        dt4 = db.ExecuteDataTable(sql3);
                                        if (String.Equals(pn, dt4.Rows[0]["FEATURE_CODE"].ToString()))
                                        {
                                            //object a = OPC.ReadItem(feerackControl);
                                            //特征码通过
                                            if (Convert.ToInt32(OPC.ReadItem(feerackControl)) == 31) OPC.WriteItem(feerackControl, 32);
                                            String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,PN,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerack + "#料架','对比特征码',31,'" + pn + "',32,'对比特征码通过');";
                                            db.ExecuteNonQuery(sql1);
                                            //  WriteLog("请求对比特征码-请求信号： " + control + "获取物料PN: " + pn + " 写入结果： " + 32 + " 表示特征码对比通过");
                                        }
                                        else
                                        {
                                            //特征码不通过
                                            if (Convert.ToInt32(OPC.ReadItem(feerackControl)) == 31) OPC.WriteItem(feerackControl, 33);
                                            String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,PN,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerack + "#料架','对比特征码',31,'" + pn + "',33,'对比特征码不通过');";
                                            db.ExecuteNonQuery(sql1);
                                            // WriteLog("请求对比特征码-请求信号： " + control + "获取物料PN: " + pn + " 写入结果： " + 33 + " 表示特征码对比不通过");
                                        }
                                    }
                                }
                                else
                                {
                                    int type = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetOpcConfigXml(feerack, "3"))).ToString()));
                                    String sql3 = "SELECT IDT.FEATURE_CODE FROM XH_FEERACK_T FT,XH_INTELLIGENTRACK_DETAIL_T IDT,XH_PRODUCTION_T PT,XH_INTELLIGENTRACK_T IT WHERE FT.FEERACK_ID=PT.FEERACK_ID AND IT.PRODUCTION_ID=PT.PRODUCTION_ID AND IDT.INTELLIGENTRACK_ID=IT.INTELLIGENTRACK_ID AND PT.PRODUCTION_TYPE=" + type + " AND IDT.STEP_NO=" + Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetOpcConfigXml(feerack, "6"))).ToString())) + " AND FT.FEERACK_ID=" + feerackId + " AND IDT.DELETE_FLAG='0'" + ";";
                                    DataTable dt4 = new DataTable();
                                    dt4 = db.ExecuteDataTable(sql3);
                                    if (String.Equals(pn, dt4.Rows[0]["FEATURE_CODE"].ToString()))
                                    {
                                        //object a = OPC.ReadItem(feerackControl);
                                        //特征码通过
                                        if (Convert.ToInt32(OPC.ReadItem(feerackControl)) == 31) OPC.WriteItem(feerackControl, 32);
                                        String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,PN,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerack + "#料架','对比特征码',31,'" + pn + "',32,'对比特征码通过');";
                                        db.ExecuteNonQuery(sql1);
                                        //  WriteLog("请求对比特征码-请求信号： " + control + "获取物料PN: " + pn + " 写入结果： " + 32 + " 表示特征码对比通过");
                                    }
                                    else
                                    {
                                        //特征码不通过
                                        if (Convert.ToInt32(OPC.ReadItem(feerackControl)) == 31) OPC.WriteItem(feerackControl, 33);
                                        String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,PN,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerack + "#料架','对比特征码',31,'" + pn + "',33,'对比特征码不通过');";
                                        db.ExecuteNonQuery(sql1);
                                        // WriteLog("请求对比特征码-请求信号： " + control + "获取物料PN: " + pn + " 写入结果： " + 33 + " 表示特征码对比不通过");
                                    }
                                }
                               
                              //  WriteLog("请求对比特征码-请求信号： " + control +"获取物料PN: "+pn+ " 写入结果： " + 39 + " 表示物料pn为空");
                            }
                            else
                            {
                                int type = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetOpcConfigXml(feerack, "3"))).ToString()));
                                String sql3 = "SELECT IDT.FEATURE_CODE FROM XH_FEERACK_T FT,XH_INTELLIGENTRACK_DETAIL_T IDT,XH_PRODUCTION_T PT,XH_INTELLIGENTRACK_T IT WHERE FT.FEERACK_ID=PT.FEERACK_ID AND IT.PRODUCTION_ID=PT.PRODUCTION_ID AND IDT.INTELLIGENTRACK_ID=IT.INTELLIGENTRACK_ID AND PT.PRODUCTION_TYPE=" + type + " AND IDT.STEP_NO=" + Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetOpcConfigXml(feerack, "6"))).ToString())) + " AND FT.FEERACK_ID=" + feerackId + " AND IDT.DELETE_FLAG='0'" + ";";
                                DataTable dt4 = new DataTable();
                                dt4 = db.ExecuteDataTable(sql3);
                                if (String.Equals(pn, dt4.Rows[0]["FEATURE_CODE"].ToString()))
                                {
                                    //object a = OPC.ReadItem(feerackControl);
                                    //特征码通过
                                    if (Convert.ToInt32(OPC.ReadItem(feerackControl)) == 31) OPC.WriteItem(feerackControl, 32);
                                    String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,PN,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerack + "#料架','对比特征码',31,'" + pn + "',32,'对比特征码通过');";
                                    db.ExecuteNonQuery(sql1);
                                  //  WriteLog("请求对比特征码-请求信号： " + control + "获取物料PN: " + pn + " 写入结果： " + 32 + " 表示特征码对比通过");
                                }
                                else
                                {
                                    //特征码不通过
                                    if (Convert.ToInt32(OPC.ReadItem(feerackControl)) == 31) OPC.WriteItem(feerackControl, 33);
                                    String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,PN,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerack + "#料架','对比特征码',31,'" + pn + "',33,'对比特征码不通过');";
                                    db.ExecuteNonQuery(sql1);
                                   // WriteLog("请求对比特征码-请求信号： " + control + "获取物料PN: " + pn + " 写入结果： " + 33 + " 表示特征码对比不通过");
                                }
                            }
                            break;
                    }
                }
                canread[1] = canread[0];

            }
            catch
            {
             //   MessageBox.Show("读写出错！");
                WriteLog("读写出错......");
            }
            finally
            {
                t.Start();
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

        /// <summary>
        /// 读取opc配置文件 根据料架号和order 得到client
        /// </summary>
        /// <param name="node"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static string GetOpcConfigXml(string feerack, string order)
        {
            string result = null;
            XmlDocument xmlDoc = new XmlDocument();
            string addr = "opc.xml";
            xmlDoc.Load(addr);
            XmlNode nd;
            nd = xmlDoc.SelectSingleNode("OPC");
            XmlNodeList xnl = nd.ChildNodes;
            foreach (XmlNode xn in xnl)
            {
                XmlElement xe = (XmlElement)xn;
                if (xe.GetAttribute("feerack") == feerack && xe.GetAttribute("order") == order)
                {
                    result = xe.GetAttribute("client");
                }
            }
            return result;
        }
        /// <summary>
        /// 根据句柄获取料架号
        /// </summary>
        /// <param name="feerack"></param>
        /// <returns></returns>
        public static string GetFeerackByItem(string item)
        {
            string result = null;
            XmlDocument xmlDoc = new XmlDocument();
            string addr = "opc.xml";
            xmlDoc.Load(addr);
            XmlNode nd;
            nd = xmlDoc.SelectSingleNode("OPC");
            XmlNodeList xnl = nd.ChildNodes;
            foreach (XmlNode xn in xnl)
            {
                XmlElement xe = (XmlElement)xn;
                if (xe.GetAttribute("client") == item)
                {
                    result = xe.GetAttribute("feerack");
                }
            }
            return result;
        }
        public static string GetOrderByItem(string item)
        {
            string result = null;
            XmlDocument xmlDoc = new XmlDocument();
            string addr = "opc.xml";
            xmlDoc.Load(addr);
            XmlNode nd;
            nd = xmlDoc.SelectSingleNode("OPC");
            XmlNodeList xnl = nd.ChildNodes;
            foreach (XmlNode xn in xnl)
            {
                XmlElement xe = (XmlElement)xn;
                if (xe.GetAttribute("client") == item)
                {
                    result = xe.GetAttribute("order");
                }
            }
            return result;
        }
        public static string GetClientByFeerackAndOrder(string feerackNo,string order)
        {
            string result = null;
            XmlDocument xmlDoc = new XmlDocument();
            string addr = "opc.xml";
            xmlDoc.Load(addr);
            XmlNode nd;
            nd = xmlDoc.SelectSingleNode("OPC");
            XmlNodeList xnl = nd.ChildNodes;
            foreach (XmlNode xn in xnl)
            {
                XmlElement xe = (XmlElement)xn;
                if (xe.GetAttribute("feerack") == feerackNo && xe.GetAttribute("order") == order)
                {
                    result = xe.GetAttribute("client");
                }
            }
            return result;
        }
        public void ConnectionOpcForm_Shown(object sender, EventArgs e)
        {
          //  BindData();
            OverrideTimer beat = new OverrideTimer();
            beat.Interval = 500;
            beat.Elapsed += new System.Timers.ElapsedEventHandler(LineBeateChange);
            beat.Enabled = true;
            beat.AutoReset = false;
            //DataTable dt = new DataTable();
            //String sql = "select count(pt.production_id)as productioncont,ft.feerack_id as feerackcount,ft.FEERACK_NAME from xh_production_t pt,xh_feerack_t ft where pt.feerack_id=ft.feerack_id and pt.DELETE_FLAG='0' and ft.delete_flag='0' group by ft.feerack_id,ft.FEERACK_NAME ";
            //dt = db.ExecuteDataTable(sql);
            //for (int i = 0; i <dt.Rows.Count; i++) //循环查找产品，有多少产品配置了信息，就开启多少线程
            //{
            //    // 如果是数字，则转换为decimal类型 
            //      //  String feerack = Regex.Replace(dt.Rows[i][2].ToString(), @"[^/d./d]", "");               
            //    String feerack = (dt.Rows[i][2].ToString()).Substring(0, 2); //得到料架编号
            //    OverrideTimer t = new OverrideTimer();
            //    t.FeerackId = Convert.ToInt32((dt.Rows[i][1].ToString()));
            //    t.CanRead = canRead;
            //    t.Feerackno = feerack;
            //    t.Interval = 800;
            //    t.Elapsed += new System.Timers.ElapsedEventHandler(Opc);  //传入料架ID和料架编号，以便监听时查询数据库和监听OPC的值 
            //    t.Enabled = true;
            //    t.AutoReset = false;
            //}
            //OverrideTimer log = new OverrideTimer();
            //log.Interval = 20000;
            //log.Elapsed += new System.Timers.ElapsedEventHandler(LogShown);
            //log.Enabled = true;
            //log.AutoReset = false;
        }
        /// <summary>
        /// 监控心跳
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void LineBeateChange(object source, System.Timers.ElapsedEventArgs e)
        {
            OverrideTimer tt = (OverrideTimer)source;
            int sklineclient = Convert.ToInt32(GetOpcConfigXml("100", "2"));
        //    OPC.WriteItem(sklineclient, true);
            tt.Stop();
            try
            {
                if (myselfBeat)
                {
                    OPC.WriteItem(sklineclient, false);
                    myselfBeat = false;
                }
                else
                {
                    OPC.WriteItem(sklineclient, true);
                    myselfBeat = true;
                }
                int lineclient = Convert.ToInt32(GetOpcConfigXml("100", "1"));
                
                Boolean beat = Convert.ToBoolean(OPC.ReadItem(lineclient).ToString());
                if (beat)
                {
                  //  OPC.WriteItem(sklineclient, beat);
                    //BeginInvoke((MethodInvoker)delegate() { panel2.BackColor = System.Drawing.Color.Olive; });
                    //BeginInvoke((MethodInvoker)delegate() { panel2.Text = ""; });
                    //beatcount = 0;
                    panel2.BackColor = System.Drawing.Color.Olive;
                }
                else
                {
                  //  OPC.WriteItem(sklineclient, beat);
                    //BeginInvoke((MethodInvoker)delegate() { panel2.BackColor = System.Drawing.Color.Yellow; });
                    //BeginInvoke((MethodInvoker)delegate() { panel2.Text = ""; });
                    //beatcount = 0;
                    panel2.BackColor = System.Drawing.Color.Yellow;
                }

            }
            catch
            {
               //// OPC.DisConnect();
               // OPC = null;
               // tt.Stop();
               // tt.Dispose();
                
             //   beatcount = beatcount + 1;
              //  LB_Beat.Text = "产线OPC连接中断，连接倒计时 " + beatcount;
                WriteLog("OPC连接中断......");
            }
            finally
            {
                if (count == 20)
                {
                    //重连
                    renectioncount = renectioncount + 1;
                    OPC.ReConnect();
                    beatcount = 0;

                    if (renectioncount == 2)
                    {
                        MessageBox.Show("OPC服务器连接中断，软件重启！");
                        WriteLog("OPC服务器连接中断，软件重启！");
                        Application.Restart();
                    }
                }
             //   OPC.WriteItem(sklineclient, false);
                tt.Start();
            }
        }
        /// <summary>
        /// 写一条日志到日志文件中
        /// </summary>
        /// <param name="data"></param>
        public static void WriteLog(string data)
        {
            if (!Directory.Exists(BasePath + "\\log\\"))
            {
                Directory.CreateDirectory(BasePath + "\\log\\");
            }
            var logFiles = Directory.GetFiles(BasePath + "\\log\\");
            if (logFiles.Length >= 5 && !logFiles.Contains<string>(DateTime.Now.ToString("yyyyMMdd") + ".log"))
            {
                var deleteFile = BasePath + "\\log\\" + DateTime.Now.AddDays(-logFiles.Length).ToString("yyyyMMdd") + ".log";
                if (File.Exists(deleteFile))
                {
                    File.Delete(deleteFile);
                }
            }
            var fileName = BasePath + "\\log\\" + DateTime.Now.ToString("yyyyMMdd") + ".log";
            lock (m_Lock)
            {
                try
                {
                    FileInfo file = new FileInfo(fileName);
                    if (!file.Exists)
                    {
                        file.Create();
                    }
                    //定位到文件尾
                    StreamWriter stream = file.AppendText();
                    //写当前的时间
                    stream.Write(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss fff]"));
                    //写用户传过来的字符串
                    stream.WriteLine(data);
                    //最后记着要关了它
                    stream.Close();
                }
                catch
                {
                }
            }
        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //if (e == null || e.Value == null || !(sender is DataGridView))
            //    return;

            //DataGridView view = (DataGridView)sender;
            //object originalValue = e.Value;
            ////更改类别显示
            //if (view.Columns[e.ColumnIndex].DataPropertyName == "CATEGORY")
            //    switch ((int)originalValue)
            //    {
            //        case 1:
            //            e.Value = (int)originalValue + ".扫描";
            //            break;
            //        case 2:
            //            e.Value = (int)originalValue + ".拧紧";
            //            break;
            //        case 3:
            //            e.Value = (int)originalValue + ".拍照";
            //            break;
            //        case 4:
            //            e.Value = "END";
            //            break;
            //    }
            ////转换料格号
            //if (view.Columns[e.ColumnIndex].DataPropertyName == "MATERIALSHELF_NO")
            //{
            //    if ((int)originalValue > 0)
            //    {
            //        e.Value = (int)originalValue + "号料格";
            //    }
            //}
            ////转换枪号
            //if (view.Columns[e.ColumnIndex].DataPropertyName == "GUN_NO")
            //{
            //    if ((int)originalValue > 0)
            //    {
            //        e.Value = (int)originalValue + "号枪";
            //    }
            //}
            ////转换套筒号
            //if (view.Columns[e.ColumnIndex].DataPropertyName == "SLEEVE_NO")
            //{
            //    if ((int)originalValue > 0)
            //    {
            //        e.Value = (int)originalValue + "号套筒";
            //    }
            //}
            ////转换相机号
            //if (view.Columns[e.ColumnIndex].DataPropertyName == "PHOTO_NO")
            //{
            //    if ((int)originalValue > 0)
            //    {
            //        e.Value = (int)originalValue + "号相机";
            //    }
            //}

        }

        private void ConnectionOpcForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x02, 0);
            }
        }
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Show();                                //窗体显示
            this.WindowState = FormWindowState.Normal;  //窗体状态默认大小
            this.Activate();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //点击"是(YES)"退出程序
            if (MessageBox.Show("确定要退出程序?", "安全提示",
                        System.Windows.Forms.MessageBoxButtons.YesNo,
                        System.Windows.Forms.MessageBoxIcon.Warning)
                == System.Windows.Forms.DialogResult.Yes)
            {
                notifyIcon1.Visible = false;   //设置图标不可见
                this.Close();                  //关闭窗体
                this.Dispose();                //释放资源
                Application.Exit();            //关闭应用程序窗体
            }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            //点击鼠标"左键"发生
            if (e.Button == MouseButtons.Left)
            {
                this.Visible = true;                        //窗体可见
                this.WindowState = FormWindowState.Normal;  //窗体默认大小
                this.notifyIcon1.Visible = true;            //设置图标可见
            }
        }

        private void OnEventDataChanged(object objItem, object objValue)
        {
            int control = (Int32)objItem;
            String value = objValue.ToString();
            DoOPCEvent(control,value);
        }
        /// <summary>
        /// 获取OPC改变的值
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="itemValue"></param>
        private void DoOPCEvent(int itemCode, string itemValue)
        {
            String plcEmpName;
            int opcProductionType;
            String pn;
            String feerackNo = GetFeerackByItem(itemCode.ToString());
            String order=GetOrderByItem(itemCode.ToString());
            if(itemValue=="1"&& order=="1")//校验员工号
            {
                plcEmpName = OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "2"))).ToString();
                //  plcEmpName = "张三";
                if (String.IsNullOrEmpty(plcEmpName))
                {

                    plcEmpName = OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "2"))).ToString();
                    if (String.IsNullOrEmpty(plcEmpName))
                    {
                        plcEmpName = OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "2"))).ToString();
                        if (String.IsNullOrEmpty(plcEmpName))
                        {
                            OPC.WriteItem(itemCode, 9);
                            //String sql = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerackNo + "#料架','对比员工号',1,9,'错误')";
                            //db.ExecuteNonQuery(sql);
                            TB_ShowLog.AppendText(DateTime.Now.ToString()+" 员工号对比" + feerackNo + "#料架 " + "请求信号： " + itemValue + "写入结果： " + 9 + " 表示员工号不通过" + Environment.NewLine);
                        }
                        else
                        {
                            String sql = "SELECT ET.EMP_ID  FROM XH_EMP_T ET,XH_FEERACK_T FT WHERE ET.FEERACK_ID=FT.FEERACK_ID AND  ET.EMP_NAME= '" + plcEmpName + "'  AND FT.FEERACK_NAME LIKE '" + feerackNo + "%' AND ET.DELETE_FLAG='0'" + ";";
                            DataTable dt = new DataTable();
                            dt = db.ExecuteDataTable(sql);
                            if (dt.Rows.Count > 0)
                            {
                                //员工号通过
                                OPC.WriteItem(itemCode, 2);
                                //String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerackNo + "#料架','对比员工号',1,2,'对比员工号通过')";
                                //db.ExecuteNonQuery(sql1);
                                TB_ShowLog.AppendText(DateTime.Now.ToString()+" 员工号对比" + feerackNo + "#料架 " + "请求信号： " + itemValue + "写入结果： " + 2 + " 表示员工号通过" + Environment.NewLine);
                            }
                            else
                            {
                                //员工号不通过
                                OPC.WriteItem(itemCode, 3);
                                //String sql2 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerackNo + "#料架','对比员工号',1,3,'对比员工号不通过')";
                                //db.ExecuteNonQuery(sql2);
                                TB_ShowLog.AppendText(DateTime.Now.ToString() + " 员工号对比" + feerackNo + "#料架 " + "请求信号： " + itemValue + " 写入结果： " + 3 + " 表示员工号不通过" + Environment.NewLine);
                            }
                        }
                    }
                    else
                    {
                        String sql = "SELECT ET.EMP_ID  FROM XH_EMP_T ET,XH_FEERACK_T FT WHERE ET.FEERACK_ID=FT.FEERACK_ID AND  ET.EMP_NAME= '" + plcEmpName + "'  AND FT.FEERACK_NAME LIKE '" + feerackNo + "%' AND ET.DELETE_FLAG='0'" + ";";
                        DataTable dt = new DataTable();
                        dt = db.ExecuteDataTable(sql);
                        if (dt.Rows.Count > 0)
                        {
                            //员工号通过
                            OPC.WriteItem(itemCode, 2);
                            //String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerackNo + "#料架','对比员工号',1,2,'对比员工号通过')";
                            //db.ExecuteNonQuery(sql1);
                            TB_ShowLog.AppendText(DateTime.Now.ToString() + " 员工号对比" + feerackNo + "#料架" + " 请求信号： " + itemValue + "写入结果： " + 2 + " 表示员工号通过" + Environment.NewLine);
                        }
                        else
                        {
                            //员工号不通过
                            OPC.WriteItem(itemCode, 3);
                            //String sql2 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerackNo + "#料架','对比员工号',1,3,'对比员工号不通过')";
                            //db.ExecuteNonQuery(sql2);
                            TB_ShowLog.AppendText(DateTime.Now.ToString() + " 员工号对比" + feerackNo + "#料架" + " 请求信号： " + itemValue + " 写入结果： " + 3 + " 表示员工号不通过" + Environment.NewLine);
                        }
                    }

                    //  WriteLog("员工号对比-请求信号： "+control + "写入结果： "+ 9 +" 表示地址块中员工号为空");
                }
                else
                {
                    String sql = "SELECT ET.EMP_ID  FROM XH_EMP_T ET,XH_FEERACK_T FT WHERE ET.FEERACK_ID=FT.FEERACK_ID AND  ET.EMP_NAME= '" + plcEmpName + "'  AND FT.FEERACK_NAME LIKE '" + feerackNo + "%' AND ET.DELETE_FLAG='0'" + ";";
                    DataTable dt = new DataTable();
                    dt = db.ExecuteDataTable(sql);
                    if (dt.Rows.Count > 0)
                    {
                        //员工号通过
                        OPC.WriteItem(itemCode, 2);
                        //String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerackNo + "#料架','对比员工号',1,2,'对比员工号通过')";
                        //db.ExecuteNonQuery(sql1);
                        TB_ShowLog.AppendText(DateTime.Now.ToString() + " 员工号对比" + feerackNo + "#料架" + " 请求信号： " + itemValue + "写入结果： " + 2 + " 表示员工号通过" + Environment.NewLine);
                    }
                    else
                    {
                        //员工号不通过
                        OPC.WriteItem(itemCode, 3);
                        //String sql2 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerackNo + "#料架','对比员工号',1,3,'对比员工号不通过')";
                        //db.ExecuteNonQuery(sql2);
                        TB_ShowLog.AppendText(DateTime.Now.ToString() + " 员工号对比" + feerackNo + "#料架" + " 请求信号： " + itemValue + " 写入结果： " + 3 + " 表示员工号不通过" + Environment.NewLine);
                    }
                }
            }
            if (itemValue == "11" && order == "1") //请求产品名称和总步序
            {
                 opcProductionType = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "3"))).ToString()));
                if (opcProductionType <= 0 && opcProductionType > 20)
                {
                    System.Threading.Thread.Sleep(800);
                    opcProductionType = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "3"))).ToString()));
                    if (opcProductionType <= 0 && opcProductionType > 20)
                    {
                        System.Threading.Thread.Sleep(800);
                        opcProductionType = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "3"))).ToString()));
                        if (opcProductionType <= 0 && opcProductionType > 20)
                        {
                            //不是产品类型
                            OPC.WriteItem(itemCode, 19);
                            //String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerackNo + "#料架','请求产品名称和总步数',11,19,'请求产品名称错误')";
                            //db.ExecuteNonQuery(sql1);
                            TB_ShowLog.AppendText(DateTime.Now.ToString() + " 请求产品名称" + feerackNo + "#料架" + " 请求信号： " + itemValue + "产品类型： " + opcProductionType + " 完成信号： " + 19 + Environment.NewLine);
                        }
                        else
                        {
                            String sql10 = "SELECT COUNT(IDT.INTELLIGENTRACK_DETAIL_ID)AS STEPNUM,PT.PRODUCTION_NAME FROM XH_PRODUCTION_T PT,XH_INTELLIGENTRACK_T IT,XH_INTELLIGENTRACK_DETAIL_T IDT,XH_FEERACK_T FT WHERE IT.PRODUCTION_ID=PT.PRODUCTION_ID AND IDT.INTELLIGENTRACK_ID=IT.INTELLIGENTRACK_ID AND FT.FEERACK_ID=PT.FEERACK_ID AND PT.PRODUCTION_TYPE=" + opcProductionType + " AND FT.FEERACK_NAME LIKE '" + feerackNo + "%' AND IDT.DELETE_FLAG='0'" + "  GROUP BY PT.PRODUCTION_NAME";
                            //    String sql1 = "SELECT PT.PRODUCTION_NAME FROM XH_PRODUCTION_T PT WHERE PT.PRODUCTION_TYPE=" + opcProductionType + " AND PT.FEERACK_ID=" + Convert.ToInt32(F.CB_Station.SelectedValue) + ";";
                            DataTable dt2 = new DataTable();
                            dt2 = db.ExecuteDataTable(sql10);
                            if (dt2.Rows.Count == 1)
                            {
                                //写入名称
                                OPC.WriteItem(Convert.ToInt32(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "4"))), dt2.Rows[0]["PRODUCTION_NAME"].ToString());
                                //写入总步数
                                OPC.WriteItem(Convert.ToInt32(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "5"))), Convert.ToInt32(dt2.Rows[0]["STEPNUM"].ToString()));
                                //完成
                                OPC.WriteItem(itemCode, 12);
                                //String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,PRODUCTION_NAME,STEP_TOAL,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerackNo + "#料架','请求产品名称和总步数',11,'" + dt2.Rows[0]["PRODUCTION_NAME"].ToString() + "','" + dt2.Rows[0]["STEPNUM"].ToString() + "',12,'请求产品名称和总步数成功');";
                                //db.ExecuteNonQuery(sql1);
                                TB_ShowLog.AppendText(DateTime.Now.ToString() + " 请求产品名称" + feerackNo + "#料架" + " 请求信号： " + itemValue + " 写入产品名称： " + dt2.Rows[0]["PRODUCTION_NAME"].ToString() + "写入总步数： " + Convert.ToInt32(dt2.Rows[0]["STEPNUM"].ToString()) + " 完成信号： " + 12 + Environment.NewLine);
                            }
                            else
                            {
                                //错误
                                OPC.WriteItem(itemCode, 19);
                                //String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerackNo + "#料架','请求产品名称和总步数',11,19,'请求产品名称错误')";
                                //db.ExecuteNonQuery(sql1);
                                TB_ShowLog.AppendText(DateTime.Now.ToString() + " 请求产品名称" + feerackNo + "#料架" + " 请求信号： " + itemValue + " 写入结果： " + 19 + " 表示不是产品类型" + Environment.NewLine);
                            }
                        }
                    }
                    else
                    {
                        String sql10 = "SELECT COUNT(IDT.INTELLIGENTRACK_DETAIL_ID)AS STEPNUM,PT.PRODUCTION_NAME FROM XH_PRODUCTION_T PT,XH_INTELLIGENTRACK_T IT,XH_INTELLIGENTRACK_DETAIL_T IDT,XH_FEERACK_T FT WHERE IT.PRODUCTION_ID=PT.PRODUCTION_ID AND IDT.INTELLIGENTRACK_ID=IT.INTELLIGENTRACK_ID AND FT.FEERACK_ID=PT.FEERACK_ID AND PT.PRODUCTION_TYPE=" + opcProductionType + " AND FT.FEERACK_NAME LIKE '" + feerackNo + "%' AND IDT.DELETE_FLAG='0'" + "  GROUP BY PT.PRODUCTION_NAME";
                        //    String sql1 = "SELECT PT.PRODUCTION_NAME FROM XH_PRODUCTION_T PT WHERE PT.PRODUCTION_TYPE=" + opcProductionType + " AND PT.FEERACK_ID=" + Convert.ToInt32(F.CB_Station.SelectedValue) + ";";
                        DataTable dt2 = new DataTable();
                        dt2 = db.ExecuteDataTable(sql10);
                        if (dt2.Rows.Count == 1)
                        {
                            //写入名称
                            OPC.WriteItem(Convert.ToInt32(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "4"))), dt2.Rows[0]["PRODUCTION_NAME"].ToString());
                            //写入总步数
                            OPC.WriteItem(Convert.ToInt32(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "5"))), Convert.ToInt32(dt2.Rows[0]["STEPNUM"].ToString()));
                            //完成
                            OPC.WriteItem(itemCode, 12);
                            //String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,PRODUCTION_NAME,STEP_TOAL,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerackNo + "#料架','请求产品名称和总步数',11,'" + dt2.Rows[0]["PRODUCTION_NAME"].ToString() + "','" + dt2.Rows[0]["STEPNUM"].ToString() + "',12,'请求产品名称和总步数成功');";
                            //db.ExecuteNonQuery(sql1);
                            TB_ShowLog.AppendText(DateTime.Now.ToString() + " 请求产品名称" + feerackNo + "#料架" + " 请求信号： " + itemValue + " 写入产品名称： " + dt2.Rows[0]["PRODUCTION_NAME"].ToString() + "写入总步数： " + Convert.ToInt32(dt2.Rows[0]["STEPNUM"].ToString()) + " 完成信号： " + 12 + Environment.NewLine);
                        }
                        else
                        {
                            //错误
                            OPC.WriteItem(itemCode, 19);
                            //String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerackNo + "#料架','请求产品名称和总步数',11,19,'请求产品名称错误')";
                            //db.ExecuteNonQuery(sql1);
                            TB_ShowLog.AppendText(DateTime.Now.ToString() + " 请求产品名称" + feerackNo + "#料架" + " 请求信号： " + itemValue + " 写入结果： " + 19 + " 表示不是产品类型" + Environment.NewLine);
                        }
                    }

                    //   WriteLog("请求产品名称-请求信号： " + control + " 写入结果： " + 19 + " 表示不是产品类型");
                }
                else
                {
                    String sql10 = "SELECT COUNT(IDT.INTELLIGENTRACK_DETAIL_ID)AS STEPNUM,PT.PRODUCTION_NAME FROM XH_PRODUCTION_T PT,XH_INTELLIGENTRACK_T IT,XH_INTELLIGENTRACK_DETAIL_T IDT,XH_FEERACK_T FT WHERE IT.PRODUCTION_ID=PT.PRODUCTION_ID AND IDT.INTELLIGENTRACK_ID=IT.INTELLIGENTRACK_ID AND FT.FEERACK_ID=PT.FEERACK_ID AND PT.PRODUCTION_TYPE=" + opcProductionType + " AND FT.FEERACK_NAME LIKE '" + feerackNo + "%' AND IDT.DELETE_FLAG='0'" + "  GROUP BY PT.PRODUCTION_NAME";
                    //    String sql1 = "SELECT PT.PRODUCTION_NAME FROM XH_PRODUCTION_T PT WHERE PT.PRODUCTION_TYPE=" + opcProductionType + " AND PT.FEERACK_ID=" + Convert.ToInt32(F.CB_Station.SelectedValue) + ";";
                    DataTable dt2 = new DataTable();
                    dt2 = db.ExecuteDataTable(sql10);
                    if (dt2.Rows.Count == 1)
                    {
                        //写入名称
                        OPC.WriteItem(Convert.ToInt32(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "4"))), dt2.Rows[0]["PRODUCTION_NAME"].ToString());
                        //写入总步数
                        OPC.WriteItem(Convert.ToInt32(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "5"))), Convert.ToInt32(dt2.Rows[0]["STEPNUM"].ToString()));
                        //完成
                        OPC.WriteItem(itemCode, 12);
                        //String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,PRODUCTION_NAME,STEP_TOAL,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerackNo + "#料架','请求产品名称和总步数',11,'" + dt2.Rows[0]["PRODUCTION_NAME"].ToString() + "','" + dt2.Rows[0]["STEPNUM"].ToString() + "',12,'请求产品名称和总步数成功');";
                        //db.ExecuteNonQuery(sql1);
                        TB_ShowLog.AppendText(DateTime.Now.ToString() + " 请求产品名称" + feerackNo + "#料架" + " 请求信号： " + itemValue + " 写入产品名称： " + dt2.Rows[0]["PRODUCTION_NAME"].ToString() + "写入总步数： " + Convert.ToInt32(dt2.Rows[0]["STEPNUM"].ToString()) + " 完成信号： " + 12 + Environment.NewLine);
                    }
                    else
                    {
                        //错误
                        OPC.WriteItem(itemCode, 19);
                        //String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerackNo + "#料架','请求产品名称和总步数',11,19,'请求产品名称错误')";
                        //db.ExecuteNonQuery(sql1);
                        TB_ShowLog.AppendText(DateTime.Now.ToString() + " 请求产品名称" + feerackNo + "#料架" + " 请求信号： " + itemValue + " 写入结果： " + 19 + " 表示不是产品类型" + Environment.NewLine);
                    }
                }
            }
            if(itemValue == "21" && order == "1") //请求步 
            {
                opcProductionType = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "3"))).ToString()));
                int reqStep = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "6"))).ToString()));
                if (reqStep <= 0)
                {
                    reqStep = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "6"))).ToString()));
                    if (reqStep <= 0)
                    {
                        reqStep = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "6"))).ToString()));
                        if (reqStep <= 0)
                        {
                            OPC.WriteItem(itemCode, 29);
                            //String sql = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,STEP_NO,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerackNo + "#料架','请求步',21,'" + reqStep.ToString() + "',29,'请求步错误');";
                            //db.ExecuteNonQuery(sql);
                        }
                        else
                        {
                            if (reqStepLog == reqStep)
                            {
                                System.Threading.Thread.Sleep(500);
                                reqStep = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "6"))).ToString()));
                                if (reqStepLog == reqStep)
                                {
                                    System.Threading.Thread.Sleep(500);
                                    reqStep = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "6"))).ToString()));
                                }
                            }
                            opcProductionType = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "3"))).ToString()));
                            String sql2 = "SELECT IDT.* FROM XH_PRODUCTION_T PT,XH_INTELLIGENTRACK_T IT,XH_INTELLIGENTRACK_DETAIL_T IDT,XH_FEERACK_T FT WHERE  FT.FEERACK_ID=PT.FEERACK_ID AND IT.PRODUCTION_ID=PT.PRODUCTION_ID AND IDT.INTELLIGENTRACK_ID=IT.INTELLIGENTRACK_ID AND PT.PRODUCTION_TYPE=" + opcProductionType + " AND FT.FEERACK_NAME LIKE '" + feerackNo + "%' AND IDT.STEP_NO=" + reqStep + " AND IDT.DELETE_FLAG='0'";
                            DataTable dt3 = new DataTable();
                            dt3 = db.ExecuteDataTable(sql2);
                            //写入类别
                            //int aaa = Convert.ToInt32(GetOpcConfigXml(feerack, "7"));
                            //string bbb = dt3.Rows[0]["CATEGORY"].ToString();
                            OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "7")), Convert.ToInt32(dt3.Rows[0]["CATEGORY"].ToString()));
                            switch (Convert.ToInt32(dt3.Rows[0]["CATEGORY"].ToString()))
                            {
                                case 1: //扫描
                                    //写入名字
                                    OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "8")), dt3.Rows[0]["NAME"].ToString());
                                    //写入料格号
                                    OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "9")), Convert.ToInt32(dt3.Rows[0]["MATERIALSHELF_NO"].ToString()));
                                    //写入数量
                                    OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "10")), Convert.ToInt32(dt3.Rows[0]["MATERIAL_NUMBER"].ToString()));
                                    //向PLC写入请求的步数
                                    OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "6")), reqStep.ToString());
                                    // 完成
                                    OPC.WriteItem(itemCode, 22);
                                    //String sql = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,STEP_NO,CATEGORY,NAME,MATERIALSHELF_NO,MATERIAL_NUMBER,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerackNo+ "#料架','请求步',21,'" + reqStep.ToString() + "','" + dt3.Rows[0]["CATEGORY"].ToString() + "','" + dt3.Rows[0]["NAME"].ToString() + "','" + dt3.Rows[0]["MATERIALSHELF_NO"].ToString() + "号料格','" + dt3.Rows[0]["MATERIAL_NUMBER"].ToString() + "',22,'请求步-扫描：成功');";
                                    //db.ExecuteNonQuery(sql);
                                    TB_ShowLog.AppendText(DateTime.Now.ToString() + " 请求步" + feerackNo + "#料架" + " 请求信号： " + itemValue + " 请求步序：" + reqStep + " 类别: " + Convert.ToInt32(dt3.Rows[0]["CATEGORY"].ToString()) + " 名字：" + dt3.Rows[0]["NAME"].ToString() + "料格号： " + Convert.ToInt32(dt3.Rows[0]["MATERIALSHELF_NO"].ToString()) + " 数量：" + Convert.ToInt32(dt3.Rows[0]["MATERIAL_NUMBER"].ToString()) + " 完成信号：" + 22 + Environment.NewLine);
                                    break;
                                case 2: //拧紧
                                    //写入名字
                                    OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "8")), dt3.Rows[0]["NAME"].ToString());
                                    //写入工具号
                                    OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "9")), Convert.ToInt32(dt3.Rows[0]["GUN_NO"].ToString()));
                                    //请求步套筒号
                                    OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "11")), Convert.ToInt32(dt3.Rows[0]["SLEEVE_NO"].ToString()));
                                    //请求步程序号
                                    OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "12")), Convert.ToInt32(dt3.Rows[0]["PROGRAME_NO"].ToString()));
                                    //写入返工次数
                                    OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "13")), Convert.ToInt32(dt3.Rows[0]["REWORK_TIMES"].ToString()));
                                    //写入数量
                                    OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "10")), Convert.ToInt32(dt3.Rows[0]["MATERIAL_NUMBER"].ToString()));
                                    //向PLC写入请求的步数
                                    OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "6")), reqStep.ToString());
                                    // 完成
                                    OPC.WriteItem(itemCode, 22);
                                    //String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,STEP_NO,CATEGORY,NAME,GUN_NO,SLEEVE_NO,PROGRAME_NO,REWORK_TIMES,MATERIAL_NUMBER,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerackNo + "#料架','请求步',21,'" + reqStep.ToString() + "','" + dt3.Rows[0]["CATEGORY"].ToString() + "','" + dt3.Rows[0]["NAME"].ToString() + "','" + dt3.Rows[0]["GUN_NO"].ToString() + "号枪','" + dt3.Rows[0]["SLEEVE_NO"].ToString() + "号套筒','" + dt3.Rows[0]["PROGRAME_NO"].ToString() + "','" + dt3.Rows[0]["REWORK_TIMES"].ToString() + "次','" + dt3.Rows[0]["MATERIAL_NUMBER"].ToString() + "'22,'请求步-拧紧：成功');";
                                    //db.ExecuteNonQuery(sql1);
                                    TB_ShowLog.AppendText(DateTime.Now.ToString() + " 请求步" + feerackNo + "#料架" + " 请求信号： " + itemValue + " 请求步序：" + reqStep + " 类别: " + Convert.ToInt32(dt3.Rows[0]["CATEGORY"].ToString()) + " 名字：" + dt3.Rows[0]["NAME"].ToString() + "工具号： " + Convert.ToInt32(dt3.Rows[0]["GUN_NO"].ToString()) + " 套筒号：" + Convert.ToInt32(dt3.Rows[0]["SLEEVE_NO"].ToString()) + "程序号： " + Convert.ToInt32(dt3.Rows[0]["PROGRAME_NO"].ToString()) + "返工次数：" + Convert.ToInt32(dt3.Rows[0]["REWORK_TIMES"].ToString()) + "数量： " + Convert.ToInt32(dt3.Rows[0]["MATERIAL_NUMBER"].ToString()) + "完成信号：" + 22 + Environment.NewLine);
                                    break;
                                case 3: //照相
                                    //写入名字
                                    OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "8")), dt3.Rows[0]["NAME"].ToString());
                                    //写入工具号
                                    OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "9")), Convert.ToInt32(dt3.Rows[0]["PHOTO_NO"].ToString()));
                                    //向PLC写入请求的步数
                                    OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "6")), reqStep.ToString());
                                    // 完成
                                    OPC.WriteItem(itemCode, 22);
                                    //String sql3 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,STEP_NO,CATEGORY,NAME,PHOTO_NO,RESULT,SIGNIFICANCE)VALUES(GETDATE(),'" + feerack + "#料架','请求步',21,'" + reqStep.ToString() + "','" + dt3.Rows[0]["CATEGORY"].ToString() + "','" + dt3.Rows[0]["NAME"].ToString() + "','" + dt3.Rows[0]["PHOTO_NO"].ToString() + "号相机,22,'请求步-照相：成功');";
                                    //db.ExecuteNonQuery(sql3);
                                    TB_ShowLog.AppendText(DateTime.Now.ToString() + " 请求步" + feerackNo + "#料架" + " 请求信号： " + itemValue + " 请求步序：" + reqStep + " 类别: " + Convert.ToInt32(dt3.Rows[0]["CATEGORY"].ToString()) + " 名字：" + dt3.Rows[0]["NAME"].ToString() + "相机号： " + Convert.ToInt32(dt3.Rows[0]["PHOTO_NO"].ToString()) + "完成信号：" + 22 + Environment.NewLine);
                                    break;
                                case 4:
                                    OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "7")), "10");
                                    //向PLC写入请求的步数
                                    OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "6")), reqStep.ToString());
                                    OPC.WriteItem(itemCode, 22);
                                    break;
                            }
                            reqStepLog = reqStep; //记录步信息
                        }
                    }
                    else
                    {
                        if (reqStepLog == reqStep)
                        {
                            System.Threading.Thread.Sleep(500);
                            reqStep = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "6"))).ToString()));
                            if (reqStepLog == reqStep)
                            {
                                System.Threading.Thread.Sleep(300);
                                reqStep = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "6"))).ToString()));
                            }
                        }
                        opcProductionType = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "3"))).ToString()));
                        String sql2 = "SELECT IDT.* FROM XH_PRODUCTION_T PT,XH_INTELLIGENTRACK_T IT,XH_INTELLIGENTRACK_DETAIL_T IDT,XH_FEERACK_T FT WHERE  FT.FEERACK_ID=PT.FEERACK_ID AND IT.PRODUCTION_ID=PT.PRODUCTION_ID AND IDT.INTELLIGENTRACK_ID=IT.INTELLIGENTRACK_ID AND PT.PRODUCTION_TYPE=" + opcProductionType + " AND FT.FEERACK_NAME LIKE '" + feerackNo + "%' AND IDT.STEP_NO=" + reqStep + " AND IDT.DELETE_FLAG='0'";
                        DataTable dt3 = new DataTable();
                        dt3 = db.ExecuteDataTable(sql2);
                        //写入类别
                        //int aaa = Convert.ToInt32(GetOpcConfigXml(feerack, "7"));
                        //string bbb = dt3.Rows[0]["CATEGORY"].ToString();
                        OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "7")), Convert.ToInt32(dt3.Rows[0]["CATEGORY"].ToString()));
                        switch (Convert.ToInt32(dt3.Rows[0]["CATEGORY"].ToString()))
                        {
                            case 1: //扫描
                                //写入名字
                                OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "8")), dt3.Rows[0]["NAME"].ToString());
                                //写入料格号
                                OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "9")), Convert.ToInt32(dt3.Rows[0]["MATERIALSHELF_NO"].ToString()));
                                //写入数量
                                OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "10")), Convert.ToInt32(dt3.Rows[0]["MATERIAL_NUMBER"].ToString()));
                                //向PLC写入请求的步数
                                OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "6")), reqStep.ToString());
                                // 完成
                                OPC.WriteItem(itemCode, 22);
                                //String sql = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,STEP_NO,CATEGORY,NAME,MATERIALSHELF_NO,MATERIAL_NUMBER,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerackNo+ "#料架','请求步',21,'" + reqStep.ToString() + "','" + dt3.Rows[0]["CATEGORY"].ToString() + "','" + dt3.Rows[0]["NAME"].ToString() + "','" + dt3.Rows[0]["MATERIALSHELF_NO"].ToString() + "号料格','" + dt3.Rows[0]["MATERIAL_NUMBER"].ToString() + "',22,'请求步-扫描：成功');";
                                //db.ExecuteNonQuery(sql);
                                TB_ShowLog.AppendText(DateTime.Now.ToString() + " 请求步" + feerackNo + "#料架" + " 请求信号： " + itemValue + " 请求步序：" + reqStep + " 类别: " + Convert.ToInt32(dt3.Rows[0]["CATEGORY"].ToString()) + " 名字：" + dt3.Rows[0]["NAME"].ToString() + "料格号： " + Convert.ToInt32(dt3.Rows[0]["MATERIALSHELF_NO"].ToString()) + " 数量：" + Convert.ToInt32(dt3.Rows[0]["MATERIAL_NUMBER"].ToString()) + " 完成信号：" + 22 + Environment.NewLine);
                                break;
                            case 2: //拧紧
                                //写入名字
                                OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "8")), dt3.Rows[0]["NAME"].ToString());
                                //写入工具号
                                OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "9")), Convert.ToInt32(dt3.Rows[0]["GUN_NO"].ToString()));
                                //请求步套筒号
                                OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "11")), Convert.ToInt32(dt3.Rows[0]["SLEEVE_NO"].ToString()));
                                //请求步程序号
                                OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "12")), Convert.ToInt32(dt3.Rows[0]["PROGRAME_NO"].ToString()));
                                //写入返工次数
                                OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "13")), Convert.ToInt32(dt3.Rows[0]["REWORK_TIMES"].ToString()));
                                //写入数量
                                OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "10")), Convert.ToInt32(dt3.Rows[0]["MATERIAL_NUMBER"].ToString()));
                                //向PLC写入请求的步数
                                OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "6")), reqStep.ToString());
                                // 完成
                                OPC.WriteItem(itemCode, 22);
                                //String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,STEP_NO,CATEGORY,NAME,GUN_NO,SLEEVE_NO,PROGRAME_NO,REWORK_TIMES,MATERIAL_NUMBER,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerackNo + "#料架','请求步',21,'" + reqStep.ToString() + "','" + dt3.Rows[0]["CATEGORY"].ToString() + "','" + dt3.Rows[0]["NAME"].ToString() + "','" + dt3.Rows[0]["GUN_NO"].ToString() + "号枪','" + dt3.Rows[0]["SLEEVE_NO"].ToString() + "号套筒','" + dt3.Rows[0]["PROGRAME_NO"].ToString() + "','" + dt3.Rows[0]["REWORK_TIMES"].ToString() + "次','" + dt3.Rows[0]["MATERIAL_NUMBER"].ToString() + "'22,'请求步-拧紧：成功');";
                                //db.ExecuteNonQuery(sql1);
                                TB_ShowLog.AppendText(DateTime.Now.ToString() + " 请求步" + feerackNo + "#料架" + " 请求信号： " + itemValue + " 请求步序：" + reqStep + " 类别: " + Convert.ToInt32(dt3.Rows[0]["CATEGORY"].ToString()) + " 名字：" + dt3.Rows[0]["NAME"].ToString() + "工具号： " + Convert.ToInt32(dt3.Rows[0]["GUN_NO"].ToString()) + " 套筒号：" + Convert.ToInt32(dt3.Rows[0]["SLEEVE_NO"].ToString()) + "程序号： " + Convert.ToInt32(dt3.Rows[0]["PROGRAME_NO"].ToString()) + "返工次数：" + Convert.ToInt32(dt3.Rows[0]["REWORK_TIMES"].ToString()) + "数量： " + Convert.ToInt32(dt3.Rows[0]["MATERIAL_NUMBER"].ToString()) + "完成信号：" + 22 + Environment.NewLine);
                                break;
                            case 3: //照相
                                //写入名字
                                OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "8")), dt3.Rows[0]["NAME"].ToString());
                                //写入工具号
                                OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "9")), Convert.ToInt32(dt3.Rows[0]["PHOTO_NO"].ToString()));
                                //向PLC写入请求的步数
                                OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "6")), reqStep.ToString());
                                // 完成
                                OPC.WriteItem(itemCode, 22);
                                //String sql3 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,STEP_NO,CATEGORY,NAME,PHOTO_NO,RESULT,SIGNIFICANCE)VALUES(GETDATE(),'" + feerack + "#料架','请求步',21,'" + reqStep.ToString() + "','" + dt3.Rows[0]["CATEGORY"].ToString() + "','" + dt3.Rows[0]["NAME"].ToString() + "','" + dt3.Rows[0]["PHOTO_NO"].ToString() + "号相机,22,'请求步-照相：成功');";
                                //db.ExecuteNonQuery(sql3);
                                TB_ShowLog.AppendText(DateTime.Now.ToString() + " 请求步" + feerackNo + "#料架" + " 请求信号： " + itemValue + " 请求步序：" + reqStep + " 类别: " + Convert.ToInt32(dt3.Rows[0]["CATEGORY"].ToString()) + " 名字：" + dt3.Rows[0]["NAME"].ToString() + "相机号： " + Convert.ToInt32(dt3.Rows[0]["PHOTO_NO"].ToString()) + "完成信号：" + 22 + Environment.NewLine);
                                break;
                            case 4:
                                OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "7")), "10");
                                //向PLC写入请求的步数
                                OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "6")), reqStep.ToString());
                                OPC.WriteItem(itemCode, 22);
                                break;
                        }
                        reqStepLog = reqStep; //记录步信息
                    }

                    // WriteLog("请求步-请求信号： " + control + " 写入结果： " + 29 + " 表示不是产品类型");
                }
                else
                {
                    if (reqStepLog == reqStep)
                    {
                        System.Threading.Thread.Sleep(500);
                        reqStep = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "6"))).ToString()));
                        if (reqStepLog == reqStep)
                        {
                            System.Threading.Thread.Sleep(300);
                            reqStep = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "6"))).ToString()));
                        }
                    }
                    opcProductionType = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "3"))).ToString()));
                    String sql2 = "SELECT IDT.* FROM XH_PRODUCTION_T PT,XH_INTELLIGENTRACK_T IT,XH_INTELLIGENTRACK_DETAIL_T IDT,XH_FEERACK_T FT WHERE  FT.FEERACK_ID=PT.FEERACK_ID AND IT.PRODUCTION_ID=PT.PRODUCTION_ID AND IDT.INTELLIGENTRACK_ID=IT.INTELLIGENTRACK_ID AND PT.PRODUCTION_TYPE=" + opcProductionType + " AND FT.FEERACK_NAME LIKE '" + feerackNo + "%' AND IDT.STEP_NO=" + reqStep + " AND IDT.DELETE_FLAG='0'";
                    DataTable dt3 = new DataTable();
                    dt3 = db.ExecuteDataTable(sql2);
                    //写入类别
                    //int aaa = Convert.ToInt32(GetOpcConfigXml(feerack, "7"));
                    //string bbb = dt3.Rows[0]["CATEGORY"].ToString();
                    OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "7")), Convert.ToInt32(dt3.Rows[0]["CATEGORY"].ToString()));
                    switch (Convert.ToInt32(dt3.Rows[0]["CATEGORY"].ToString()))
                    {
                        case 1: //扫描
                            //写入名字
                            OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "8")), dt3.Rows[0]["NAME"].ToString());
                            //写入料格号
                            OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "9")), Convert.ToInt32(dt3.Rows[0]["MATERIALSHELF_NO"].ToString()));
                            //写入数量
                            OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "10")), Convert.ToInt32(dt3.Rows[0]["MATERIAL_NUMBER"].ToString()));
                            //向PLC写入请求的步数
                            OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "6")), reqStep.ToString());
                            // 完成
                            OPC.WriteItem(itemCode, 22);
                            //String sql = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,STEP_NO,CATEGORY,NAME,MATERIALSHELF_NO,MATERIAL_NUMBER,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerackNo+ "#料架','请求步',21,'" + reqStep.ToString() + "','" + dt3.Rows[0]["CATEGORY"].ToString() + "','" + dt3.Rows[0]["NAME"].ToString() + "','" + dt3.Rows[0]["MATERIALSHELF_NO"].ToString() + "号料格','" + dt3.Rows[0]["MATERIAL_NUMBER"].ToString() + "',22,'请求步-扫描：成功');";
                            //db.ExecuteNonQuery(sql);
                            TB_ShowLog.AppendText(DateTime.Now.ToString() + " 请求步" + feerackNo + "#料架" + " 请求信号： " + itemValue + " 请求步序：" + reqStep + " 类别: " + Convert.ToInt32(dt3.Rows[0]["CATEGORY"].ToString()) + " 名字：" + dt3.Rows[0]["NAME"].ToString() + "料格号： " + Convert.ToInt32(dt3.Rows[0]["MATERIALSHELF_NO"].ToString()) + " 数量：" + Convert.ToInt32(dt3.Rows[0]["MATERIAL_NUMBER"].ToString()) + " 完成信号：" + 22 + Environment.NewLine);
                            break;
                        case 2: //拧紧
                            //写入名字
                            OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "8")), dt3.Rows[0]["NAME"].ToString());
                            //写入工具号
                            OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "9")), Convert.ToInt32(dt3.Rows[0]["GUN_NO"].ToString()));
                            //请求步套筒号
                            OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "11")), Convert.ToInt32(dt3.Rows[0]["SLEEVE_NO"].ToString()));
                            //请求步程序号
                            OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "12")), Convert.ToInt32(dt3.Rows[0]["PROGRAME_NO"].ToString()));
                            //写入返工次数
                            OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "13")), Convert.ToInt32(dt3.Rows[0]["REWORK_TIMES"].ToString()));
                            //写入数量
                            OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "10")), Convert.ToInt32(dt3.Rows[0]["MATERIAL_NUMBER"].ToString()));
                            //向PLC写入请求的步数
                            OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "6")), reqStep.ToString());
                            // 完成
                            OPC.WriteItem(itemCode, 22);
                            //String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,STEP_NO,CATEGORY,NAME,GUN_NO,SLEEVE_NO,PROGRAME_NO,REWORK_TIMES,MATERIAL_NUMBER,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerackNo + "#料架','请求步',21,'" + reqStep.ToString() + "','" + dt3.Rows[0]["CATEGORY"].ToString() + "','" + dt3.Rows[0]["NAME"].ToString() + "','" + dt3.Rows[0]["GUN_NO"].ToString() + "号枪','" + dt3.Rows[0]["SLEEVE_NO"].ToString() + "号套筒','" + dt3.Rows[0]["PROGRAME_NO"].ToString() + "','" + dt3.Rows[0]["REWORK_TIMES"].ToString() + "次','" + dt3.Rows[0]["MATERIAL_NUMBER"].ToString() + "'22,'请求步-拧紧：成功');";
                            //db.ExecuteNonQuery(sql1);
                            TB_ShowLog.AppendText(DateTime.Now.ToString() + " 请求步" + feerackNo + "#料架" + " 请求信号： " + itemValue + " 请求步序：" + reqStep + " 类别: " + Convert.ToInt32(dt3.Rows[0]["CATEGORY"].ToString()) + " 名字：" + dt3.Rows[0]["NAME"].ToString() + "工具号： " + Convert.ToInt32(dt3.Rows[0]["GUN_NO"].ToString()) + " 套筒号：" + Convert.ToInt32(dt3.Rows[0]["SLEEVE_NO"].ToString()) + "程序号： " + Convert.ToInt32(dt3.Rows[0]["PROGRAME_NO"].ToString()) + "返工次数：" + Convert.ToInt32(dt3.Rows[0]["REWORK_TIMES"].ToString()) + "数量： " + Convert.ToInt32(dt3.Rows[0]["MATERIAL_NUMBER"].ToString()) + "完成信号：" + 22 + Environment.NewLine);
                            break;
                        case 3: //照相
                            //写入名字
                            OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "8")), dt3.Rows[0]["NAME"].ToString());
                            //写入工具号
                            OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "9")), Convert.ToInt32(dt3.Rows[0]["PHOTO_NO"].ToString()));
                            //向PLC写入请求的步数
                            OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "6")), reqStep.ToString());
                            // 完成
                            OPC.WriteItem(itemCode, 22);
                            //String sql3 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,STEP_NO,CATEGORY,NAME,PHOTO_NO,RESULT,SIGNIFICANCE)VALUES(GETDATE(),'" + feerack + "#料架','请求步',21,'" + reqStep.ToString() + "','" + dt3.Rows[0]["CATEGORY"].ToString() + "','" + dt3.Rows[0]["NAME"].ToString() + "','" + dt3.Rows[0]["PHOTO_NO"].ToString() + "号相机,22,'请求步-照相：成功');";
                            //db.ExecuteNonQuery(sql3);
                            TB_ShowLog.AppendText(DateTime.Now.ToString() + " 请求步" + feerackNo + "#料架" + " 请求信号： " + itemValue + " 请求步序：" + reqStep + " 类别: " + Convert.ToInt32(dt3.Rows[0]["CATEGORY"].ToString()) + " 名字：" + dt3.Rows[0]["NAME"].ToString() + "相机号： " + Convert.ToInt32(dt3.Rows[0]["PHOTO_NO"].ToString()) + "完成信号：" + 22 + Environment.NewLine);
                            break;
                        case 4:
                            OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "7")), "10");
                            //向PLC写入请求的步数
                            OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "6")), reqStep.ToString());
                            OPC.WriteItem(itemCode, 22);
                            break;
                    }
                    reqStepLog = reqStep; //记录步信息

                }
            }
            if (itemValue == "31" && order == "1") //请求对比特征码
            {
                pn = OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "14"))).ToString();
                if (String.IsNullOrEmpty(pn))  //  && Convert.ToInt32(OPC.ReadItem(feerackControl)) == 31)
                {
                    pn = OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "14"))).ToString();
                    if (String.IsNullOrEmpty(pn))// && Convert.ToInt32(OPC.ReadItem(feerackControl)) == 31)
                    {
                        pn = OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "14"))).ToString();
                        if (String.IsNullOrEmpty(pn))// && Convert.ToInt32(OPC.ReadItem(feerackControl)) == 31)
                        {
                            //写入为空或错误
                            OPC.WriteItem(itemCode, 39);
                            //String sql = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,PN,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerackNo + "#料架','对比特征码',31,'" + pn + "',39,'请求对比特征码错误');";
                            //db.ExecuteNonQuery(sql);
                            TB_ShowLog.AppendText(DateTime.Now.ToString() + " 请求对比特征码" + feerackNo + "#料架" + " 请求信号： " + itemValue + " " + feerackNo + "#料架 " + "获取物料PN: " + pn + " 写入结果： " + 39 + " PN号为空" + Environment.NewLine);
                        }
                        else
                        {
                            int type = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "3"))).ToString()));
                            String sql3 = "SELECT IDT.FEATURE_CODE FROM XH_FEERACK_T FT,XH_INTELLIGENTRACK_DETAIL_T IDT,XH_PRODUCTION_T PT,XH_INTELLIGENTRACK_T IT WHERE FT.FEERACK_ID=PT.FEERACK_ID AND IT.PRODUCTION_ID=PT.PRODUCTION_ID AND IDT.INTELLIGENTRACK_ID=IT.INTELLIGENTRACK_ID AND PT.PRODUCTION_TYPE=" + type + " AND IDT.STEP_NO=" + Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "18"))).ToString())) + " AND FT.FEERACK_NAME LIKE '" + feerackNo + "%' AND IDT.DELETE_FLAG='0'" + ";";
                            DataTable dt4 = new DataTable();
                            dt4 = db.ExecuteDataTable(sql3);
                            if (String.Equals(pn, dt4.Rows[0]["FEATURE_CODE"].ToString()))
                            {
                                //object a = OPC.ReadItem(feerackControl);
                                //特征码通过
                                if (Convert.ToInt32(OPC.ReadItem(itemCode)) == 31) OPC.WriteItem(itemCode, 32);
                                //String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,PN,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerackNo + "#料架','对比特征码',31,'" + pn + "',32,'对比特征码通过');";
                                //db.ExecuteNonQuery(sql1);
                                TB_ShowLog.AppendText(DateTime.Now.ToString() + " 请求对比特征码" + feerackNo + "#料架" + " 请求信号： " + itemValue + "获取物料PN: " + pn + " 当前步数：" + Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "18"))).ToString())) + " 写入结果： " + 32 + " 表示特征码对比通过" + Environment.NewLine);
                            }
                            else
                            {
                                //特征码不通过
                                if (Convert.ToInt32(OPC.ReadItem(itemCode)) == 31) OPC.WriteItem(itemCode, 33);
                                //String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,PN,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerackNo + "#料架','对比特征码',31,'" + pn + "',33,'对比特征码不通过');";
                                //db.ExecuteNonQuery(sql1);
                                TB_ShowLog.AppendText(DateTime.Now.ToString() + " 请求对比特征码" + feerackNo + "#料架" + " 请求信号： " + itemValue + "获取物料PN: " + pn + " 写入结果： " + 33 + " 表示特征码对比不通过" + Environment.NewLine);
                            }
                        }
                    }
                    else
                    {
                        int type = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "3"))).ToString()));
                        String sql3 = "SELECT IDT.FEATURE_CODE FROM XH_FEERACK_T FT,XH_INTELLIGENTRACK_DETAIL_T IDT,XH_PRODUCTION_T PT,XH_INTELLIGENTRACK_T IT WHERE FT.FEERACK_ID=PT.FEERACK_ID AND IT.PRODUCTION_ID=PT.PRODUCTION_ID AND IDT.INTELLIGENTRACK_ID=IT.INTELLIGENTRACK_ID AND PT.PRODUCTION_TYPE=" + type + " AND IDT.STEP_NO=" + Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "18"))).ToString())) + " AND FT.FEERACK_NAME LIKE '" + feerackNo + "%' AND IDT.DELETE_FLAG='0'" + ";";
                        DataTable dt4 = new DataTable();
                        dt4 = db.ExecuteDataTable(sql3);
                        if (String.Equals(pn, dt4.Rows[0]["FEATURE_CODE"].ToString()))
                        {
                            //object a = OPC.ReadItem(feerackControl);
                            //特征码通过
                            if (Convert.ToInt32(OPC.ReadItem(itemCode)) == 31) OPC.WriteItem(itemCode, 32);
                            //String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,PN,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerackNo + "#料架','对比特征码',31,'" + pn + "',32,'对比特征码通过');";
                            //db.ExecuteNonQuery(sql1);
                            TB_ShowLog.AppendText(DateTime.Now.ToString() + " 请求对比特征码" + feerackNo + "#料架" + " 请求信号： " + itemValue + "获取物料PN: " + pn + " 当前步数：" + Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "18"))).ToString())) + " 写入结果： " + 32 + " 表示特征码对比通过" + Environment.NewLine);
                        }
                        else
                        {
                            //特征码不通过
                            if (Convert.ToInt32(OPC.ReadItem(itemCode)) == 31) OPC.WriteItem(itemCode, 33);
                            //String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,PN,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerackNo + "#料架','对比特征码',31,'" + pn + "',33,'对比特征码不通过');";
                            //db.ExecuteNonQuery(sql1);
                            TB_ShowLog.AppendText(DateTime.Now.ToString() + " 请求对比特征码" + feerackNo + "#料架" + " 请求信号： " + itemValue + "获取物料PN: " + pn + " 写入结果： " + 33 + " 表示特征码对比不通过" + Environment.NewLine);
                        }
                    }

                    //  WriteLog("请求对比特征码-请求信号： " + control +"获取物料PN: "+pn+ " 写入结果： " + 39 + " 表示物料pn为空");
                }
                else
                {
                    int type = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "3"))).ToString()));
                    String sql3 = "SELECT IDT.FEATURE_CODE FROM XH_FEERACK_T FT,XH_INTELLIGENTRACK_DETAIL_T IDT,XH_PRODUCTION_T PT,XH_INTELLIGENTRACK_T IT WHERE FT.FEERACK_ID=PT.FEERACK_ID AND IT.PRODUCTION_ID=PT.PRODUCTION_ID AND IDT.INTELLIGENTRACK_ID=IT.INTELLIGENTRACK_ID AND PT.PRODUCTION_TYPE=" + type + " AND IDT.STEP_NO=" + Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "18"))).ToString())) + " AND FT.FEERACK_NAME LIKE '" + feerackNo + "%' AND IDT.DELETE_FLAG='0'" + ";";
                    DataTable dt4 = new DataTable();
                    dt4 = db.ExecuteDataTable(sql3);
                    if (String.Equals(pn, dt4.Rows[0]["FEATURE_CODE"].ToString()))
                    {
                        //object a = OPC.ReadItem(feerackControl);
                        //特征码通过
                        if (Convert.ToInt32(OPC.ReadItem(itemCode)) == 31) OPC.WriteItem(itemCode, 32);
                        //String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,PN,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerackNo + "#料架','对比特征码',31,'" + pn + "',32,'对比特征码通过');";
                        //db.ExecuteNonQuery(sql1);
                        TB_ShowLog.AppendText(DateTime.Now.ToString() + " 请求对比特征码" + feerackNo + "#料架" + " 请求信号： " + itemValue + "获取物料PN: " + pn + " 当前步数：" + Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "18"))).ToString())) + " 写入结果： " + 32 + " 表示特征码对比通过" + Environment.NewLine);
                    }
                    else
                    {
                        //特征码不通过
                        if (Convert.ToInt32(OPC.ReadItem(itemCode)) == 31) OPC.WriteItem(itemCode, 33);
                        //String sql1 = "INSERT dbo.XH_CONFIG_LOG(DT,FEERACK,BUSSINESS,CONTROL,PN,RESULT,SIGNIFICANCE)values(GETDATE(),'" + feerackNo + "#料架','对比特征码',31,'" + pn + "',33,'对比特征码不通过');";
                        //db.ExecuteNonQuery(sql1);
                        TB_ShowLog.AppendText(DateTime.Now.ToString() + " 请求对比特征码" + feerackNo + "#料架" + " 请求信号： " + itemValue + "获取物料PN: " + pn + " 写入结果： " + 33 + " 表示特征码对比不通过" + Environment.NewLine);
                    }
                }
            }
            if (itemValue == "41" && order == "1")//预校验
            {
                //读取批次号
                String kp=OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "16"))).ToString();
                if (String.IsNullOrEmpty(kp))  //当批次号为空的时候，再读取KP
                {
                    System.Threading.Thread.Sleep(800);
                     kp = OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "16"))).ToString();
                     if (String.IsNullOrEmpty(kp)) //kp号为空的时候再读取KP
                     {
                         kp = OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "16"))).ToString();
                         if (String.IsNullOrEmpty(kp)) //kp为空，返回错误
                         {
                             //错误信息写入1
                             OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "15")), 1);
                             //交互完成信号
                             OPC.WriteItem(itemCode, 49);
                             //界面显示扫描结果
                             TB_ShowLog.AppendText(DateTime.Now.ToString() + " 业务：预校验 料架号：" + feerackNo + "#料架" + "  触发信号： " + itemValue + "  批次号：" + kp + "  错误信息：1" + "  完成信号： " + "49" + Environment.NewLine);
                         }
                         else
                         {
                             String sql = "SELECT MATERIAL_PN FROM dbo.XH_MATERIAL_CHECK_T WHERE MATERIAL_SN='"+kp+"';";
                             DataTable dt = new DataTable();
                             dt = db.ExecuteDataTable(sql);
                             if (dt.Rows.Count != 1)
                             {
                                 //错误信息写入1
                                 OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "15")), 3);
                                 //交互完成信号
                                 OPC.WriteItem(itemCode, 49);
                                 //界面显示扫描结果
                                 TB_ShowLog.AppendText(DateTime.Now.ToString() + " 业务：预校验 料架号：" + feerackNo + "#料架" + "  触发信号： " + itemValue + "  批次号：" + kp + "  错误信息：3" + "  完成信号： " + "49" + Environment.NewLine);
                             }
                             else
                             {
                                 //本地PN
                                 String materialPn = dt.Rows[0]["MATERIAL_PN"].ToString();
                                 //获取产品类别和配方配置的PN
                                 opcProductionType = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "3"))).ToString()));
                                 if (opcProductionType <= 0 || opcProductionType > 20)
                                 {
                                     //产品类型超过了20
                                     OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "15")), 5);
                                     //交互完成信号
                                     OPC.WriteItem(itemCode, 49);
                                     TB_ShowLog.AppendText(DateTime.Now.ToString() + " 业务：预校验 料架号：" + feerackNo + "#料架" + "  触发信号： " + itemValue + "  批次号：" + kp + "  产品类型：" + opcProductionType + "  错误信息：5" + "  完成信号： " + "49" + Environment.NewLine);
                                 }
                                 else
                                 {
                                     int reqStep = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "18"))).ToString()));
                                     String sql3 = "SELECT IDT.FEATURE_CODE FROM XH_FEERACK_T FT,XH_INTELLIGENTRACK_DETAIL_T IDT,XH_PRODUCTION_T PT,XH_INTELLIGENTRACK_T IT WHERE FT.FEERACK_ID=PT.FEERACK_ID AND IT.PRODUCTION_ID=PT.PRODUCTION_ID AND IDT.INTELLIGENTRACK_ID=IT.INTELLIGENTRACK_ID AND PT.PRODUCTION_TYPE=" + opcProductionType + " AND IDT.STEP_NO=" + reqStep + " AND FT.FEERACK_NAME LIKE '" + feerackNo + "%' AND IDT.DELETE_FLAG='0'" + ";";
                                     DataTable dt1 = new DataTable();
                                     dt1 = db.ExecuteDataTable(sql3);
                                     if (dt1.Rows.Count == 1)
                                     {
                                         if (materialPn == dt1.Rows[0]["FEATURE_CODE"].ToString())
                                         {
                                             //写入PN
                                             OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "17")), materialPn);
                                             //交互完成信号
                                             OPC.WriteItem(itemCode, 42);
                                             TB_ShowLog.AppendText(DateTime.Now.ToString() + " 业务：预校验 料架号：" + feerackNo + "#料架" + "  触发信号： " + itemValue + "  批次号：" + kp + "  产品类型：" + opcProductionType + "请求步： " + reqStep + "  配置PN号：" + dt1.Rows[0]["FEATURE_CODE"].ToString() + "  完成信号： " + "42" + Environment.NewLine);
                                         }
                                         else
                                         {
                                             //交互完成信号
                                             OPC.WriteItem(itemCode, 43);
                                             TB_ShowLog.AppendText(DateTime.Now.ToString() + " 业务：预校验 料架号：" + feerackNo + "#料架" + "  触发信号： " + itemValue + "  批次号：" + kp + "  产品类型：" + opcProductionType + "请求步： " + reqStep + "  配置PN号：" + dt1.Rows[0]["FEATURE_CODE"].ToString() + "  完成信号： " + "43" + Environment.NewLine);
                                         }
                                     }
                                 }
                                

                             }
                         }
                     }
                     else   //KP不为空的时候 处理业务
                     {
                         String sql = "SELECT MATERIAL_PN FROM dbo.XH_MATERIAL_CHECK_T WHERE MATERIAL_SN='" + kp + "';";
                         DataTable dt = new DataTable();
                         dt = db.ExecuteDataTable(sql);
                         if (dt.Rows.Count != 1)
                         {
                             //错误信息写入1
                             OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "15")), 3);
                             //交互完成信号
                             OPC.WriteItem(itemCode, 49);
                             //界面显示扫描结果
                             TB_ShowLog.AppendText(DateTime.Now.ToString() + " 业务：预校验 料架号：" + feerackNo + "#料架" + "  触发信号： " + itemValue + "  批次号：" + kp + "  错误信息：3" + "  完成信号： " + "49" + Environment.NewLine);
                         }
                         else
                         {
                             //本地PN
                             String materialPn = dt.Rows[0]["MATERIAL_PN"].ToString();
                             //获取产品类别和配方配置的PN
                             opcProductionType = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "3"))).ToString()));
                             if (opcProductionType <= 0 || opcProductionType > 20)
                             {
                                 //产品类型超过了20
                                 OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "15")), 5);
                                 //交互完成信号
                                 OPC.WriteItem(itemCode, 49);
                                 TB_ShowLog.AppendText(DateTime.Now.ToString() + " 业务：预校验 料架号：" + feerackNo + "#料架" + "  触发信号： " + itemValue + "  批次号：" + kp + "  产品类型：" + opcProductionType + "  错误信息：5" + "  完成信号： " + "49" + Environment.NewLine);
                             }
                             else
                             {
                                 int reqStep = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "18"))).ToString()));
                                 String sql3 = "SELECT IDT.FEATURE_CODE FROM XH_FEERACK_T FT,XH_INTELLIGENTRACK_DETAIL_T IDT,XH_PRODUCTION_T PT,XH_INTELLIGENTRACK_T IT WHERE FT.FEERACK_ID=PT.FEERACK_ID AND IT.PRODUCTION_ID=PT.PRODUCTION_ID AND IDT.INTELLIGENTRACK_ID=IT.INTELLIGENTRACK_ID AND PT.PRODUCTION_TYPE=" + opcProductionType + " AND IDT.STEP_NO=" + reqStep + " AND FT.FEERACK_NAME LIKE '" + feerackNo + "%' AND IDT.DELETE_FLAG='0'" + ";";
                                 DataTable dt1 = new DataTable();
                                 dt1 = db.ExecuteDataTable(sql3);
                                 if (dt1.Rows.Count == 1)
                                 {
                                     if (materialPn == dt1.Rows[0]["FEATURE_CODE"].ToString())
                                     {
                                         //写入PN
                                         OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "17")), materialPn);
                                         //交互完成信号
                                         OPC.WriteItem(itemCode, 42);
                                         TB_ShowLog.AppendText(DateTime.Now.ToString() + " 业务：预校验 料架号：" + feerackNo + "#料架" + "  触发信号： " + itemValue + "  批次号：" + kp + "  产品类型：" + opcProductionType + "请求步： " + reqStep + "  配置PN号：" + dt1.Rows[0]["FEATURE_CODE"].ToString() + "  完成信号： " + "42" + Environment.NewLine);
                                     }
                                     else
                                     {
                                         //交互完成信号
                                         OPC.WriteItem(itemCode, 43);
                                         TB_ShowLog.AppendText(DateTime.Now.ToString() + " 业务：预校验 料架号：" + feerackNo + "#料架" + "  触发信号： " + itemValue + "  批次号：" + kp + "  产品类型：" + opcProductionType + "请求步： " + reqStep + "  配置PN号：" + dt1.Rows[0]["FEATURE_CODE"].ToString() + "  完成信号： " + "43" + Environment.NewLine);
                                     }
                                 }
                             }


                         }
                     }
                }
                else  //kp不为空
                {
                    String sql = "SELECT MATERIAL_PN FROM dbo.XH_MATERIAL_CHECK_T WHERE MATERIAL_SN='" + kp + "';";
                    DataTable dt = new DataTable();
                    dt = db.ExecuteDataTable(sql);
                    if (dt.Rows.Count != 1)
                    {
                        //错误信息写入1
                        OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "15")), 3);
                        //交互完成信号
                        OPC.WriteItem(itemCode, 49);
                        //界面显示扫描结果
                        TB_ShowLog.AppendText(DateTime.Now.ToString() + " 业务：预校验 料架号：" + feerackNo + "#料架" + "  触发信号： " + itemValue + "  批次号：" + kp + "  错误信息：3" + "  完成信号： " + "49" + Environment.NewLine);
                    }
                    else
                    {
                        //本地PN
                        String materialPn = dt.Rows[0]["MATERIAL_PN"].ToString();
                        //获取产品类别和配方配置的PN
                        opcProductionType = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "3"))).ToString()));
                        if (opcProductionType <= 0 || opcProductionType > 20)
                        {
                            //产品类型超过了20
                            OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "15")), 5);
                            //交互完成信号
                            OPC.WriteItem(itemCode, 49);
                            TB_ShowLog.AppendText(DateTime.Now.ToString() + " 业务：预校验 料架号：" + feerackNo + "#料架" + "  触发信号： " + itemValue + "  批次号：" + kp + "  产品类型：" + opcProductionType + "  错误信息：5" + "  完成信号： " + "49" + Environment.NewLine);
                        }
                        else
                        {
                            int reqStep = Convert.ToInt32((OPC.ReadItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "18"))).ToString()));
                            String sql3 = "SELECT IDT.FEATURE_CODE FROM XH_FEERACK_T FT,XH_INTELLIGENTRACK_DETAIL_T IDT,XH_PRODUCTION_T PT,XH_INTELLIGENTRACK_T IT WHERE FT.FEERACK_ID=PT.FEERACK_ID AND IT.PRODUCTION_ID=PT.PRODUCTION_ID AND IDT.INTELLIGENTRACK_ID=IT.INTELLIGENTRACK_ID AND PT.PRODUCTION_TYPE=" + opcProductionType + " AND IDT.STEP_NO=" + reqStep + " AND FT.FEERACK_NAME LIKE '" + feerackNo + "%' AND IDT.DELETE_FLAG='0'" + ";";
                            DataTable dt1 = new DataTable();
                            dt1 = db.ExecuteDataTable(sql3);
                            if (dt1.Rows.Count == 1)
                            {
                                if (materialPn == dt1.Rows[0]["FEATURE_CODE"].ToString())
                                {
                                    //写入PN
                                    OPC.WriteItem(Convert.ToInt32(GetClientByFeerackAndOrder(feerackNo, "17")), materialPn);
                                    //交互完成信号
                                    OPC.WriteItem(itemCode, 42);
                                    TB_ShowLog.AppendText(DateTime.Now.ToString() + " 业务：预校验 料架号：" + feerackNo + "#料架" + "  触发信号： " + itemValue + "  批次号：" + kp + "  产品类型：" + opcProductionType + "请求步： " + reqStep + "  配置PN号：" + dt1.Rows[0]["FEATURE_CODE"].ToString() + "  完成信号： " + "42" + Environment.NewLine);
                                }
                                else
                                {
                                    //交互完成信号
                                    OPC.WriteItem(itemCode, 43);
                                    TB_ShowLog.AppendText(DateTime.Now.ToString() + " 业务：预校验 料架号：" + feerackNo + "#料架" + "  触发信号： " + itemValue + "  批次号：" + kp + "  产品类型：" + opcProductionType + "请求步： " + reqStep + "  配置PN号：" + dt1.Rows[0]["FEATURE_CODE"].ToString() + "  完成信号： " + "43" + Environment.NewLine);
                                }
                            }
                        }


                    }
                }
            }
        }
    }
}
