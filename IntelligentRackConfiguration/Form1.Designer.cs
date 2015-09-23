using System.Diagnostics;
using System.Windows.Forms;
namespace IntelligentRackConfiguration
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.LB_Station = new System.Windows.Forms.Label();
            this.CB_Station = new System.Windows.Forms.ComboBox();
            this.BT_Connect = new System.Windows.Forms.Button();
            this.LB_MaterialNo = new System.Windows.Forms.Label();
            this.CB_MaterialNo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Step_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Cargory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Desc1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaterialShelf_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Material_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Gun_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Program_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FeatureCode1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Photo_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Seelve_No = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rework_Times = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IDT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.StepNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaterialShelfNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GunNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProgrameNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FeatureCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SleeveNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PhotoNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rework_Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INTELLIGENTRACK_DATAIL_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INTELLIGENTRACK_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRODUCTION_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FEERACK_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.production_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PORDUCTION_TYPE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ANOTHERNAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.TB_ProductionName = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.料架管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.员工管理ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.增加员工ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查询员工ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.BT_Eidit = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.LB_StepNo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CB_StepNo = new System.Windows.Forms.ComboBox();
            this.LB_Category = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CB_Category = new System.Windows.Forms.ComboBox();
            this.LB_MaterailName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TB_MaterialName = new System.Windows.Forms.TextBox();
            this.BT_StepSubmit = new System.Windows.Forms.Button();
            this.Reworkpanel = new System.Windows.Forms.Panel();
            this.TB_ReworkTimes = new System.Windows.Forms.TextBox();
            this.LB_ReworkTimes = new System.Windows.Forms.Label();
            this.PhotoNopanel = new System.Windows.Forms.Panel();
            this.CB_PhotoNo = new System.Windows.Forms.ComboBox();
            this.LB_PhotoNo = new System.Windows.Forms.Label();
            this.FeatureCodepanel = new System.Windows.Forms.Panel();
            this.LB_FeatureCode = new System.Windows.Forms.Label();
            this.TB_FeatureCode = new System.Windows.Forms.TextBox();
            this.Programpanel = new System.Windows.Forms.Panel();
            this.TB_ProgramNo = new System.Windows.Forms.TextBox();
            this.LB_ProgramNo = new System.Windows.Forms.Label();
            this.Gunpanel = new System.Windows.Forms.Panel();
            this.LB_GunNo = new System.Windows.Forms.Label();
            this.CB_GunNo = new System.Windows.Forms.ComboBox();
            this.MaterialNumpanel = new System.Windows.Forms.Panel();
            this.LB_MaterialNum = new System.Windows.Forms.Label();
            this.TB_MaterialNum = new System.Windows.Forms.TextBox();
            this.MaterialShelfpanel = new System.Windows.Forms.Panel();
            this.LB_MaterialShelfNo = new System.Windows.Forms.Label();
            this.CB_MaterialShelfNo = new System.Windows.Forms.ComboBox();
            this.Sleevepanel = new System.Windows.Forms.Panel();
            this.TB_SleeveNo = new System.Windows.Forms.ComboBox();
            this.LB_SleeveNo = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.BT_MaterialSubmit = new System.Windows.Forms.Button();
            this.BT_MaterialCancel = new System.Windows.Forms.Button();
            this.BT_Cancel = new System.Windows.Forms.Button();
            this.TB_VIEW1_CANEL = new System.Windows.Forms.Button();
            this.anthername = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.Reworkpanel.SuspendLayout();
            this.PhotoNopanel.SuspendLayout();
            this.FeatureCodepanel.SuspendLayout();
            this.Programpanel.SuspendLayout();
            this.Gunpanel.SuspendLayout();
            this.MaterialNumpanel.SuspendLayout();
            this.MaterialShelfpanel.SuspendLayout();
            this.Sleevepanel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // LB_Station
            // 
            this.LB_Station.AutoSize = true;
            this.LB_Station.Location = new System.Drawing.Point(13, 96);
            this.LB_Station.Name = "LB_Station";
            this.LB_Station.Size = new System.Drawing.Size(53, 12);
            this.LB_Station.TabIndex = 1;
            this.LB_Station.Text = "料架号：";
            // 
            // CB_Station
            // 
            this.CB_Station.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Station.FormattingEnabled = true;
            this.CB_Station.Location = new System.Drawing.Point(71, 93);
            this.CB_Station.Name = "CB_Station";
            this.CB_Station.Size = new System.Drawing.Size(121, 20);
            this.CB_Station.TabIndex = 2;
            this.CB_Station.SelectedIndexChanged += new System.EventHandler(this.CB_Station_SelectedIndexChanged);
            this.CB_Station.SelectionChangeCommitted += new System.EventHandler(this.CB_Station_SelectionChangeCommitted);
            this.CB_Station.SelectedValueChanged += new System.EventHandler(this.CB_Station_SelectedValueChanged);
            // 
            // BT_Connect
            // 
            this.BT_Connect.Location = new System.Drawing.Point(841, 62);
            this.BT_Connect.Name = "BT_Connect";
            this.BT_Connect.Size = new System.Drawing.Size(78, 23);
            this.BT_Connect.TabIndex = 5;
            this.BT_Connect.Text = "连接OPC";
            this.BT_Connect.UseVisualStyleBackColor = true;
            this.BT_Connect.Click += new System.EventHandler(this.BT_Connect_Click);
            // 
            // LB_MaterialNo
            // 
            this.LB_MaterialNo.AutoSize = true;
            this.LB_MaterialNo.Location = new System.Drawing.Point(13, 156);
            this.LB_MaterialNo.Name = "LB_MaterialNo";
            this.LB_MaterialNo.Size = new System.Drawing.Size(65, 12);
            this.LB_MaterialNo.TabIndex = 7;
            this.LB_MaterialNo.Text = "产品编号：";
            // 
            // CB_MaterialNo
            // 
            this.CB_MaterialNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_MaterialNo.FormattingEnabled = true;
            this.CB_MaterialNo.Items.AddRange(new object[] {
            "产品1",
            "产品2",
            "产品3",
            "产品4",
            "产品5",
            "产品6",
            "产品7",
            "产品8",
            "产品9",
            "产品10",
            "产品11",
            "产品12",
            "产品13",
            "产品14",
            "产品15",
            "产品16",
            "产品17",
            "产品18",
            "产品19",
            "产品20"});
            this.CB_MaterialNo.Location = new System.Drawing.Point(71, 153);
            this.CB_MaterialNo.Name = "CB_MaterialNo";
            this.CB_MaterialNo.Size = new System.Drawing.Size(121, 20);
            this.CB_MaterialNo.TabIndex = 8;
            this.CB_MaterialNo.SelectedIndexChanged += new System.EventHandler(this.CB_MaterialNo_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(38, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 10);
            this.label4.TabIndex = 19;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.dataGridView2);
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Location = new System.Drawing.Point(215, 93);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(707, 550);
            this.panel1.TabIndex = 32;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Step_NO,
            this.Cargory,
            this.Desc1,
            this.MaterialShelf_No,
            this.Material_No,
            this.Gun_No,
            this.Program_No,
            this.FeatureCode1,
            this.Photo_No,
            this.Seelve_No,
            this.Rework_Times,
            this.IDT});
            this.dataGridView2.Location = new System.Drawing.Point(-3, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(682, 536);
            this.dataGridView2.TabIndex = 21;
            this.dataGridView2.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellValidated);
            // 
            // Step_NO
            // 
            this.Step_NO.HeaderText = "步序";
            this.Step_NO.Name = "Step_NO";
            this.Step_NO.Width = 55;
            // 
            // Cargory
            // 
            this.Cargory.HeaderText = "类别";
            this.Cargory.Name = "Cargory";
            this.Cargory.Width = 54;
            // 
            // Desc1
            // 
            this.Desc1.HeaderText = "名称";
            this.Desc1.Name = "Desc1";
            this.Desc1.Width = 54;
            // 
            // MaterialShelf_No
            // 
            this.MaterialShelf_No.HeaderText = "料格号";
            this.MaterialShelf_No.Name = "MaterialShelf_No";
            this.MaterialShelf_No.Width = 66;
            // 
            // Material_No
            // 
            this.Material_No.HeaderText = "数量";
            this.Material_No.Name = "Material_No";
            this.Material_No.Width = 54;
            // 
            // Gun_No
            // 
            this.Gun_No.HeaderText = "枪号";
            this.Gun_No.Name = "Gun_No";
            this.Gun_No.Width = 54;
            // 
            // Program_No
            // 
            this.Program_No.HeaderText = "程序号";
            this.Program_No.Name = "Program_No";
            this.Program_No.Width = 66;
            // 
            // FeatureCode1
            // 
            this.FeatureCode1.HeaderText = "特征码";
            this.FeatureCode1.Name = "FeatureCode1";
            this.FeatureCode1.Width = 66;
            // 
            // Photo_No
            // 
            this.Photo_No.HeaderText = "相机号";
            this.Photo_No.Name = "Photo_No";
            this.Photo_No.Width = 66;
            // 
            // Seelve_No
            // 
            this.Seelve_No.HeaderText = "套筒号";
            this.Seelve_No.Name = "Seelve_No";
            this.Seelve_No.Width = 66;
            // 
            // Rework_Times
            // 
            this.Rework_Times.HeaderText = "返工次数";
            this.Rework_Times.Name = "Rework_Times";
            this.Rework_Times.Width = 78;
            // 
            // IDT
            // 
            this.IDT.HeaderText = "配置明细ID";
            this.IDT.Name = "IDT";
            this.IDT.Visible = false;
            this.IDT.Width = 90;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StepNo,
            this.Category,
            this.Desc,
            this.MaterialShelfNo,
            this.GunNo,
            this.ProgrameNo,
            this.Number,
            this.FeatureCode,
            this.SleeveNo,
            this.PhotoNo,
            this.Rework_Time,
            this.INTELLIGENTRACK_DATAIL_ID,
            this.INTELLIGENTRACK_ID,
            this.PRODUCTION_ID,
            this.FEERACK_ID,
            this.production_name,
            this.PORDUCTION_TYPE,
            this.ANOTHERNAME});
            this.dataGridView1.Location = new System.Drawing.Point(-3, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(698, 550);
            this.dataGridView1.TabIndex = 20;
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            // 
            // StepNo
            // 
            this.StepNo.DataPropertyName = "STEP_NO";
            this.StepNo.HeaderText = "步序";
            this.StepNo.Name = "StepNo";
            this.StepNo.ReadOnly = true;
            // 
            // Category
            // 
            this.Category.DataPropertyName = "CATEGORY";
            this.Category.HeaderText = "类别";
            this.Category.Name = "Category";
            this.Category.ReadOnly = true;
            // 
            // Desc
            // 
            this.Desc.DataPropertyName = "NAME";
            this.Desc.HeaderText = "名称";
            this.Desc.Name = "Desc";
            this.Desc.ReadOnly = true;
            // 
            // MaterialShelfNo
            // 
            this.MaterialShelfNo.DataPropertyName = "MATERIALSHELF_NO";
            this.MaterialShelfNo.HeaderText = "料格号";
            this.MaterialShelfNo.Name = "MaterialShelfNo";
            this.MaterialShelfNo.ReadOnly = true;
            // 
            // GunNo
            // 
            this.GunNo.DataPropertyName = "GUN_NO";
            this.GunNo.HeaderText = "枪号";
            this.GunNo.Name = "GunNo";
            this.GunNo.ReadOnly = true;
            // 
            // ProgrameNo
            // 
            this.ProgrameNo.DataPropertyName = "PROGRAME_NO";
            this.ProgrameNo.HeaderText = "程序号";
            this.ProgrameNo.Name = "ProgrameNo";
            this.ProgrameNo.ReadOnly = true;
            // 
            // Number
            // 
            this.Number.DataPropertyName = "MATERIAL_NUMBER";
            this.Number.HeaderText = "数量";
            this.Number.Name = "Number";
            this.Number.ReadOnly = true;
            // 
            // FeatureCode
            // 
            this.FeatureCode.DataPropertyName = "FEATURE_CODE";
            this.FeatureCode.HeaderText = "特征码";
            this.FeatureCode.Name = "FeatureCode";
            this.FeatureCode.ReadOnly = true;
            // 
            // SleeveNo
            // 
            this.SleeveNo.DataPropertyName = "SLEEVE_NO";
            this.SleeveNo.HeaderText = "套筒号";
            this.SleeveNo.Name = "SleeveNo";
            this.SleeveNo.ReadOnly = true;
            // 
            // PhotoNo
            // 
            this.PhotoNo.DataPropertyName = "PHOTO_NO";
            this.PhotoNo.HeaderText = "相机号";
            this.PhotoNo.Name = "PhotoNo";
            this.PhotoNo.ReadOnly = true;
            // 
            // Rework_Time
            // 
            this.Rework_Time.DataPropertyName = "REWORK_TIMES";
            this.Rework_Time.HeaderText = "返工次数";
            this.Rework_Time.Name = "Rework_Time";
            this.Rework_Time.ReadOnly = true;
            // 
            // INTELLIGENTRACK_DATAIL_ID
            // 
            this.INTELLIGENTRACK_DATAIL_ID.DataPropertyName = "INTELLIGENTRACK_DETAIL_ID";
            this.INTELLIGENTRACK_DATAIL_ID.HeaderText = "配置明细ID";
            this.INTELLIGENTRACK_DATAIL_ID.Name = "INTELLIGENTRACK_DATAIL_ID";
            this.INTELLIGENTRACK_DATAIL_ID.ReadOnly = true;
            // 
            // INTELLIGENTRACK_ID
            // 
            this.INTELLIGENTRACK_ID.DataPropertyName = "INTELLIGENTRACK_ID";
            this.INTELLIGENTRACK_ID.HeaderText = "配置ID";
            this.INTELLIGENTRACK_ID.Name = "INTELLIGENTRACK_ID";
            this.INTELLIGENTRACK_ID.ReadOnly = true;
            this.INTELLIGENTRACK_ID.Visible = false;
            // 
            // PRODUCTION_ID
            // 
            this.PRODUCTION_ID.DataPropertyName = "PRODUCTION_ID";
            this.PRODUCTION_ID.HeaderText = "产品ID";
            this.PRODUCTION_ID.Name = "PRODUCTION_ID";
            this.PRODUCTION_ID.ReadOnly = true;
            this.PRODUCTION_ID.Visible = false;
            // 
            // FEERACK_ID
            // 
            this.FEERACK_ID.DataPropertyName = "FEERACK_ID";
            this.FEERACK_ID.HeaderText = "料架ID";
            this.FEERACK_ID.Name = "FEERACK_ID";
            this.FEERACK_ID.ReadOnly = true;
            this.FEERACK_ID.Visible = false;
            // 
            // production_name
            // 
            this.production_name.DataPropertyName = "PRODUCTION_NAME";
            this.production_name.HeaderText = "产品名称";
            this.production_name.Name = "production_name";
            this.production_name.ReadOnly = true;
            // 
            // PORDUCTION_TYPE
            // 
            this.PORDUCTION_TYPE.DataPropertyName = "PRODUCTION_TYPE";
            this.PORDUCTION_TYPE.HeaderText = "产品类型";
            this.PORDUCTION_TYPE.Name = "PORDUCTION_TYPE";
            this.PORDUCTION_TYPE.ReadOnly = true;
            this.PORDUCTION_TYPE.Visible = false;
            // 
            // ANOTHERNAME
            // 
            this.ANOTHERNAME.DataPropertyName = "ANOTHERNAME";
            this.ANOTHERNAME.HeaderText = "别名";
            this.ANOTHERNAME.Name = "ANOTHERNAME";
            this.ANOTHERNAME.ReadOnly = true;
            this.ANOTHERNAME.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 186);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 44;
            this.label5.Text = "产品名称：";
            // 
            // TB_ProductionName
            // 
            this.TB_ProductionName.Location = new System.Drawing.Point(71, 183);
            this.TB_ProductionName.MaxLength = 36;
            this.TB_ProductionName.Name = "TB_ProductionName";
            this.TB_ProductionName.Size = new System.Drawing.Size(121, 21);
            this.TB_ProductionName.TabIndex = 45;
            this.TB_ProductionName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_ProductionName_KeyPress);
            // 
            // pictureBox1
            // 
            this.pictureBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(820, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(37, 25);
            this.pictureBox1.TabIndex = 46;
            this.pictureBox1.TabStop = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.料架管理ToolStripMenuItem,
            this.员工管理ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 48);
            // 
            // 料架管理ToolStripMenuItem
            // 
            this.料架管理ToolStripMenuItem.Name = "料架管理ToolStripMenuItem";
            this.料架管理ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.料架管理ToolStripMenuItem.Text = "料架管理";
            this.料架管理ToolStripMenuItem.Click += new System.EventHandler(this.料架管理ToolStripMenuItem_Click);
            // 
            // 员工管理ToolStripMenuItem
            // 
            this.员工管理ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.增加员工ToolStripMenuItem,
            this.查询员工ToolStripMenuItem});
            this.员工管理ToolStripMenuItem.Name = "员工管理ToolStripMenuItem";
            this.员工管理ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.员工管理ToolStripMenuItem.Text = "员工管理";
            // 
            // 增加员工ToolStripMenuItem
            // 
            this.增加员工ToolStripMenuItem.Name = "增加员工ToolStripMenuItem";
            this.增加员工ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.增加员工ToolStripMenuItem.Text = "增加员工";
            this.增加员工ToolStripMenuItem.Click += new System.EventHandler(this.增加员工ToolStripMenuItem_Click);
            // 
            // 查询员工ToolStripMenuItem
            // 
            this.查询员工ToolStripMenuItem.Name = "查询员工ToolStripMenuItem";
            this.查询员工ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.查询员工ToolStripMenuItem.Text = "查询员工";
            this.查询员工ToolStripMenuItem.Click += new System.EventHandler(this.查询员工ToolStripMenuItem_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(900, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(34, 25);
            this.pictureBox2.TabIndex = 47;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(863, 0);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(31, 25);
            this.pictureBox3.TabIndex = 48;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Click += new System.EventHandler(this.pictureBox3_Click);
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(1, 0);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(55, 49);
            this.pictureBox4.TabIndex = 49;
            this.pictureBox4.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(312, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(273, 29);
            this.label6.TabIndex = 50;
            this.label6.Text = "智 能 防 错 料 架";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(356, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(179, 12);
            this.label7.TabIndex = 51;
            this.label7.Text = "上 海 先 惠 机 械 有 限 公 司";
            // 
            // BT_Eidit
            // 
            this.BT_Eidit.Location = new System.Drawing.Point(317, 659);
            this.BT_Eidit.Name = "BT_Eidit";
            this.BT_Eidit.Size = new System.Drawing.Size(75, 23);
            this.BT_Eidit.TabIndex = 53;
            this.BT_Eidit.Text = "编 辑";
            this.BT_Eidit.UseVisualStyleBackColor = true;
            this.BT_Eidit.Click += new System.EventHandler(this.BT_Eidit_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 12);
            this.label8.TabIndex = 54;
            this.label8.Text = "别 名:";
            // 
            // LB_StepNo
            // 
            this.LB_StepNo.AutoSize = true;
            this.LB_StepNo.Location = new System.Drawing.Point(13, 216);
            this.LB_StepNo.Name = "LB_StepNo";
            this.LB_StepNo.Size = new System.Drawing.Size(41, 12);
            this.LB_StepNo.TabIndex = 9;
            this.LB_StepNo.Text = "步序：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(47, 216);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 10);
            this.label1.TabIndex = 10;
            this.label1.Text = "（当前步在总流程中的顺序）";
            // 
            // CB_StepNo
            // 
            this.CB_StepNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_StepNo.FormattingEnabled = true;
            this.CB_StepNo.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50"});
            this.CB_StepNo.Location = new System.Drawing.Point(13, 236);
            this.CB_StepNo.Name = "CB_StepNo";
            this.CB_StepNo.Size = new System.Drawing.Size(176, 20);
            this.CB_StepNo.TabIndex = 11;
            // 
            // LB_Category
            // 
            this.LB_Category.AutoSize = true;
            this.LB_Category.Location = new System.Drawing.Point(13, 267);
            this.LB_Category.Name = "LB_Category";
            this.LB_Category.Size = new System.Drawing.Size(41, 12);
            this.LB_Category.TabIndex = 12;
            this.LB_Category.Text = "类别：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(47, 268);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 10);
            this.label2.TabIndex = 13;
            this.label2.Text = "(当前步的类别)";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // CB_Category
            // 
            this.CB_Category.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_Category.FormattingEnabled = true;
            this.CB_Category.Items.AddRange(new object[] {
            "1.扫描",
            "2.拧紧",
            "3.照相",
            "4.压装",
            "5.备用",
            "6.备用",
            "7.备用",
            "8.备用",
            "9.备用",
            "10.END"});
            this.CB_Category.Location = new System.Drawing.Point(13, 287);
            this.CB_Category.Name = "CB_Category";
            this.CB_Category.Size = new System.Drawing.Size(176, 20);
            this.CB_Category.TabIndex = 14;
            this.CB_Category.SelectedIndexChanged += new System.EventHandler(this.CB_Category_SelectedIndexChanged);
            // 
            // LB_MaterailName
            // 
            this.LB_MaterailName.AutoSize = true;
            this.LB_MaterailName.Location = new System.Drawing.Point(13, 318);
            this.LB_MaterailName.Name = "LB_MaterailName";
            this.LB_MaterailName.Size = new System.Drawing.Size(41, 12);
            this.LB_MaterailName.TabIndex = 15;
            this.LB_MaterailName.Text = "名称：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(47, 318);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 10);
            this.label3.TabIndex = 16;
            this.label3.Text = "（当前步描述）";
            // 
            // TB_MaterialName
            // 
            this.TB_MaterialName.Location = new System.Drawing.Point(13, 339);
            this.TB_MaterialName.MaxLength = 98;
            this.TB_MaterialName.Name = "TB_MaterialName";
            this.TB_MaterialName.Size = new System.Drawing.Size(176, 21);
            this.TB_MaterialName.TabIndex = 17;
            this.TB_MaterialName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_MaterialName_KeyDown);
            this.TB_MaterialName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_MaterialName_KeyPress);
            // 
            // BT_StepSubmit
            // 
            this.BT_StepSubmit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.BT_StepSubmit.Location = new System.Drawing.Point(3, 427);
            this.BT_StepSubmit.Name = "BT_StepSubmit";
            this.BT_StepSubmit.Size = new System.Drawing.Size(75, 23);
            this.BT_StepSubmit.TabIndex = 30;
            this.BT_StepSubmit.Text = "提交工作步";
            this.BT_StepSubmit.UseVisualStyleBackColor = true;
            this.BT_StepSubmit.Click += new System.EventHandler(this.BT_StepSubmit_Click);
            // 
            // Reworkpanel
            // 
            this.Reworkpanel.Controls.Add(this.TB_ReworkTimes);
            this.Reworkpanel.Controls.Add(this.LB_ReworkTimes);
            this.Reworkpanel.Location = new System.Drawing.Point(2, 374);
            this.Reworkpanel.Margin = new System.Windows.Forms.Padding(2, 3, 3, 3);
            this.Reworkpanel.Name = "Reworkpanel";
            this.Reworkpanel.Size = new System.Drawing.Size(176, 47);
            this.Reworkpanel.TabIndex = 46;
            // 
            // TB_ReworkTimes
            // 
            this.TB_ReworkTimes.Location = new System.Drawing.Point(1, 23);
            this.TB_ReworkTimes.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
            this.TB_ReworkTimes.Name = "TB_ReworkTimes";
            this.TB_ReworkTimes.Size = new System.Drawing.Size(176, 21);
            this.TB_ReworkTimes.TabIndex = 1;
            this.TB_ReworkTimes.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_ReworkTimes_KeyDown);
            this.TB_ReworkTimes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_ReworkTimes_KeyPress);
            // 
            // LB_ReworkTimes
            // 
            this.LB_ReworkTimes.AutoSize = true;
            this.LB_ReworkTimes.Location = new System.Drawing.Point(1, 4);
            this.LB_ReworkTimes.Name = "LB_ReworkTimes";
            this.LB_ReworkTimes.Size = new System.Drawing.Size(65, 12);
            this.LB_ReworkTimes.TabIndex = 0;
            this.LB_ReworkTimes.Text = "返工次数：";
            // 
            // PhotoNopanel
            // 
            this.PhotoNopanel.Controls.Add(this.CB_PhotoNo);
            this.PhotoNopanel.Controls.Add(this.LB_PhotoNo);
            this.PhotoNopanel.Location = new System.Drawing.Point(3, 321);
            this.PhotoNopanel.Name = "PhotoNopanel";
            this.PhotoNopanel.Size = new System.Drawing.Size(176, 47);
            this.PhotoNopanel.TabIndex = 6;
            // 
            // CB_PhotoNo
            // 
            this.CB_PhotoNo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CB_PhotoNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_PhotoNo.FormattingEnabled = true;
            this.CB_PhotoNo.Items.AddRange(new object[] {
            "1号相机",
            "2号相机"});
            this.CB_PhotoNo.Location = new System.Drawing.Point(1, 24);
            this.CB_PhotoNo.Name = "CB_PhotoNo";
            this.CB_PhotoNo.Size = new System.Drawing.Size(176, 20);
            this.CB_PhotoNo.TabIndex = 43;
            // 
            // LB_PhotoNo
            // 
            this.LB_PhotoNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LB_PhotoNo.AutoSize = true;
            this.LB_PhotoNo.Location = new System.Drawing.Point(1, 4);
            this.LB_PhotoNo.Name = "LB_PhotoNo";
            this.LB_PhotoNo.Size = new System.Drawing.Size(53, 12);
            this.LB_PhotoNo.TabIndex = 42;
            this.LB_PhotoNo.Text = "相机号：";
            // 
            // FeatureCodepanel
            // 
            this.FeatureCodepanel.Controls.Add(this.LB_FeatureCode);
            this.FeatureCodepanel.Controls.Add(this.TB_FeatureCode);
            this.FeatureCodepanel.Location = new System.Drawing.Point(3, 268);
            this.FeatureCodepanel.Name = "FeatureCodepanel";
            this.FeatureCodepanel.Size = new System.Drawing.Size(176, 47);
            this.FeatureCodepanel.TabIndex = 4;
            // 
            // LB_FeatureCode
            // 
            this.LB_FeatureCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LB_FeatureCode.AutoSize = true;
            this.LB_FeatureCode.Location = new System.Drawing.Point(1, 5);
            this.LB_FeatureCode.Name = "LB_FeatureCode";
            this.LB_FeatureCode.Size = new System.Drawing.Size(53, 12);
            this.LB_FeatureCode.TabIndex = 24;
            this.LB_FeatureCode.Text = "特征码：";
            this.LB_FeatureCode.Click += new System.EventHandler(this.LB_FeatureCode_Click);
            // 
            // TB_FeatureCode
            // 
            this.TB_FeatureCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_FeatureCode.Location = new System.Drawing.Point(1, 24);
            this.TB_FeatureCode.Name = "TB_FeatureCode";
            this.TB_FeatureCode.Size = new System.Drawing.Size(176, 21);
            this.TB_FeatureCode.TabIndex = 26;
            this.TB_FeatureCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_FeatureCode_KeyDown);
            this.TB_FeatureCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_FeatureCode_KeyPress);
            // 
            // Programpanel
            // 
            this.Programpanel.Controls.Add(this.TB_ProgramNo);
            this.Programpanel.Controls.Add(this.LB_ProgramNo);
            this.Programpanel.Location = new System.Drawing.Point(3, 215);
            this.Programpanel.Name = "Programpanel";
            this.Programpanel.Size = new System.Drawing.Size(176, 47);
            this.Programpanel.TabIndex = 3;
            // 
            // TB_ProgramNo
            // 
            this.TB_ProgramNo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_ProgramNo.Location = new System.Drawing.Point(1, 23);
            this.TB_ProgramNo.Name = "TB_ProgramNo";
            this.TB_ProgramNo.Size = new System.Drawing.Size(176, 21);
            this.TB_ProgramNo.TabIndex = 38;
            this.TB_ProgramNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_ProgramNo_KeyDown);
            this.TB_ProgramNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_ProgramNo_KeyPress);
            // 
            // LB_ProgramNo
            // 
            this.LB_ProgramNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LB_ProgramNo.AutoSize = true;
            this.LB_ProgramNo.Location = new System.Drawing.Point(1, 5);
            this.LB_ProgramNo.Name = "LB_ProgramNo";
            this.LB_ProgramNo.Size = new System.Drawing.Size(53, 12);
            this.LB_ProgramNo.TabIndex = 36;
            this.LB_ProgramNo.Text = "程序号：";
            // 
            // Gunpanel
            // 
            this.Gunpanel.Controls.Add(this.LB_GunNo);
            this.Gunpanel.Controls.Add(this.CB_GunNo);
            this.Gunpanel.Location = new System.Drawing.Point(3, 162);
            this.Gunpanel.Name = "Gunpanel";
            this.Gunpanel.Size = new System.Drawing.Size(176, 47);
            this.Gunpanel.TabIndex = 2;
            // 
            // LB_GunNo
            // 
            this.LB_GunNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LB_GunNo.AutoSize = true;
            this.LB_GunNo.Location = new System.Drawing.Point(1, 4);
            this.LB_GunNo.Name = "LB_GunNo";
            this.LB_GunNo.Size = new System.Drawing.Size(41, 12);
            this.LB_GunNo.TabIndex = 39;
            this.LB_GunNo.Text = "枪号：";
            // 
            // CB_GunNo
            // 
            this.CB_GunNo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CB_GunNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_GunNo.FormattingEnabled = true;
            this.CB_GunNo.Items.AddRange(new object[] {
            "1号枪",
            "2号枪",
            "3号枪",
            "4号枪"});
            this.CB_GunNo.Location = new System.Drawing.Point(1, 24);
            this.CB_GunNo.Name = "CB_GunNo";
            this.CB_GunNo.Size = new System.Drawing.Size(176, 20);
            this.CB_GunNo.TabIndex = 41;
            // 
            // MaterialNumpanel
            // 
            this.MaterialNumpanel.Controls.Add(this.LB_MaterialNum);
            this.MaterialNumpanel.Controls.Add(this.TB_MaterialNum);
            this.MaterialNumpanel.Location = new System.Drawing.Point(3, 109);
            this.MaterialNumpanel.Name = "MaterialNumpanel";
            this.MaterialNumpanel.Size = new System.Drawing.Size(176, 47);
            this.MaterialNumpanel.TabIndex = 1;
            // 
            // LB_MaterialNum
            // 
            this.LB_MaterialNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LB_MaterialNum.AutoSize = true;
            this.LB_MaterialNum.Location = new System.Drawing.Point(2, 6);
            this.LB_MaterialNum.Name = "LB_MaterialNum";
            this.LB_MaterialNum.Size = new System.Drawing.Size(41, 12);
            this.LB_MaterialNum.TabIndex = 21;
            this.LB_MaterialNum.Text = "数量：";
            // 
            // TB_MaterialNum
            // 
            this.TB_MaterialNum.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_MaterialNum.Location = new System.Drawing.Point(1, 24);
            this.TB_MaterialNum.Name = "TB_MaterialNum";
            this.TB_MaterialNum.Size = new System.Drawing.Size(176, 21);
            this.TB_MaterialNum.TabIndex = 23;
            this.TB_MaterialNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_MaterialNum_KeyDown);
            this.TB_MaterialNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_MaterialNum_KeyPress);
            // 
            // MaterialShelfpanel
            // 
            this.MaterialShelfpanel.Controls.Add(this.LB_MaterialShelfNo);
            this.MaterialShelfpanel.Controls.Add(this.CB_MaterialShelfNo);
            this.MaterialShelfpanel.Location = new System.Drawing.Point(2, 56);
            this.MaterialShelfpanel.Margin = new System.Windows.Forms.Padding(2, 3, 3, 3);
            this.MaterialShelfpanel.Name = "MaterialShelfpanel";
            this.MaterialShelfpanel.Size = new System.Drawing.Size(176, 47);
            this.MaterialShelfpanel.TabIndex = 0;
            // 
            // LB_MaterialShelfNo
            // 
            this.LB_MaterialShelfNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LB_MaterialShelfNo.AutoSize = true;
            this.LB_MaterialShelfNo.Location = new System.Drawing.Point(1, 6);
            this.LB_MaterialShelfNo.Name = "LB_MaterialShelfNo";
            this.LB_MaterialShelfNo.Size = new System.Drawing.Size(53, 12);
            this.LB_MaterialShelfNo.TabIndex = 18;
            this.LB_MaterialShelfNo.Text = "料格号：";
            // 
            // CB_MaterialShelfNo
            // 
            this.CB_MaterialShelfNo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CB_MaterialShelfNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_MaterialShelfNo.FormattingEnabled = true;
            this.CB_MaterialShelfNo.Items.AddRange(new object[] {
            "1号料格",
            "2号料格",
            "3号料格",
            "4号料格",
            "5号料格",
            "6号料格",
            "7号料格",
            "8号料格",
            "9号料格",
            "10号料格",
            "11号料格",
            "12号料格",
            "13号料格",
            "14号料格",
            "15号料格",
            "16号料格",
            "17号料格",
            "18号料格",
            "19号料格",
            "20号料格",
            "21号料格",
            "22号料格",
            "23号料格",
            "24号料格",
            "25号料格",
            "26号料格",
            "27号料格",
            "28号料格",
            "29号料格",
            "30号料格",
            "31号料格",
            "32号料格",
            "33号料格",
            "34号料格",
            "35号料格",
            "36号料格",
            "37号料格",
            "38号料格",
            "39号料格",
            "40号料格",
            "41号料格",
            "42号料格",
            "43号料格",
            "44号料格",
            "45号料格",
            "46号料格",
            "47号料格",
            "48号料格"});
            this.CB_MaterialShelfNo.Location = new System.Drawing.Point(1, 25);
            this.CB_MaterialShelfNo.Name = "CB_MaterialShelfNo";
            this.CB_MaterialShelfNo.Size = new System.Drawing.Size(176, 20);
            this.CB_MaterialShelfNo.TabIndex = 20;
            // 
            // Sleevepanel
            // 
            this.Sleevepanel.Controls.Add(this.TB_SleeveNo);
            this.Sleevepanel.Controls.Add(this.LB_SleeveNo);
            this.Sleevepanel.Location = new System.Drawing.Point(2, 3);
            this.Sleevepanel.Margin = new System.Windows.Forms.Padding(2, 3, 3, 3);
            this.Sleevepanel.Name = "Sleevepanel";
            this.Sleevepanel.Size = new System.Drawing.Size(176, 47);
            this.Sleevepanel.TabIndex = 5;
            // 
            // TB_SleeveNo
            // 
            this.TB_SleeveNo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_SleeveNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TB_SleeveNo.FormattingEnabled = true;
            this.TB_SleeveNo.Items.AddRange(new object[] {
            "1号套筒",
            "2号套筒",
            "3号套筒",
            "4号套筒"});
            this.TB_SleeveNo.Location = new System.Drawing.Point(1, 25);
            this.TB_SleeveNo.Name = "TB_SleeveNo";
            this.TB_SleeveNo.Size = new System.Drawing.Size(176, 20);
            this.TB_SleeveNo.TabIndex = 35;
            // 
            // LB_SleeveNo
            // 
            this.LB_SleeveNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LB_SleeveNo.AutoSize = true;
            this.LB_SleeveNo.Location = new System.Drawing.Point(1, 4);
            this.LB_SleeveNo.Name = "LB_SleeveNo";
            this.LB_SleeveNo.Size = new System.Drawing.Size(53, 12);
            this.LB_SleeveNo.TabIndex = 27;
            this.LB_SleeveNo.Text = "套筒号：";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.Sleevepanel);
            this.flowLayoutPanel1.Controls.Add(this.MaterialShelfpanel);
            this.flowLayoutPanel1.Controls.Add(this.MaterialNumpanel);
            this.flowLayoutPanel1.Controls.Add(this.Gunpanel);
            this.flowLayoutPanel1.Controls.Add(this.Programpanel);
            this.flowLayoutPanel1.Controls.Add(this.FeatureCodepanel);
            this.flowLayoutPanel1.Controls.Add(this.PhotoNopanel);
            this.flowLayoutPanel1.Controls.Add(this.Reworkpanel);
            this.flowLayoutPanel1.Controls.Add(this.BT_StepSubmit);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(10, 363);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(184, 454);
            this.flowLayoutPanel1.TabIndex = 43;
            // 
            // BT_MaterialSubmit
            // 
            this.BT_MaterialSubmit.Location = new System.Drawing.Point(495, 659);
            this.BT_MaterialSubmit.Name = "BT_MaterialSubmit";
            this.BT_MaterialSubmit.Size = new System.Drawing.Size(75, 23);
            this.BT_MaterialSubmit.TabIndex = 33;
            this.BT_MaterialSubmit.Text = "保 存";
            this.BT_MaterialSubmit.UseVisualStyleBackColor = true;
            this.BT_MaterialSubmit.Click += new System.EventHandler(this.BT_MaterialSubmit_Click);
            // 
            // BT_MaterialCancel
            // 
            this.BT_MaterialCancel.Location = new System.Drawing.Point(495, 659);
            this.BT_MaterialCancel.Name = "BT_MaterialCancel";
            this.BT_MaterialCancel.Size = new System.Drawing.Size(75, 23);
            this.BT_MaterialCancel.TabIndex = 34;
            this.BT_MaterialCancel.Text = "保 存";
            this.BT_MaterialCancel.UseVisualStyleBackColor = true;
            this.BT_MaterialCancel.Click += new System.EventHandler(this.BT_MaterialCancel_Click);
            // 
            // BT_Cancel
            // 
            this.BT_Cancel.Location = new System.Drawing.Point(665, 659);
            this.BT_Cancel.Name = "BT_Cancel";
            this.BT_Cancel.Size = new System.Drawing.Size(75, 23);
            this.BT_Cancel.TabIndex = 31;
            this.BT_Cancel.Text = "删 除";
            this.BT_Cancel.UseVisualStyleBackColor = true;
            this.BT_Cancel.Click += new System.EventHandler(this.BT_Cancel_Click);
            // 
            // TB_VIEW1_CANEL
            // 
            this.TB_VIEW1_CANEL.Location = new System.Drawing.Point(665, 659);
            this.TB_VIEW1_CANEL.Name = "TB_VIEW1_CANEL";
            this.TB_VIEW1_CANEL.Size = new System.Drawing.Size(75, 23);
            this.TB_VIEW1_CANEL.TabIndex = 52;
            this.TB_VIEW1_CANEL.Text = "删 除";
            this.TB_VIEW1_CANEL.UseVisualStyleBackColor = true;
            this.TB_VIEW1_CANEL.Click += new System.EventHandler(this.TB_VIEW1_CANEL_Click);
            // 
            // anthername
            // 
            this.anthername.AutoSize = true;
            this.anthername.Location = new System.Drawing.Point(73, 126);
            this.anthername.Name = "anthername";
            this.anthername.Size = new System.Drawing.Size(41, 12);
            this.anthername.TabIndex = 55;
            this.anthername.Text = "label9";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(934, 705);
            this.Controls.Add(this.anthername);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.BT_Eidit);
            this.Controls.Add(this.TB_VIEW1_CANEL);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.TB_ProductionName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.BT_MaterialCancel);
            this.Controls.Add(this.BT_MaterialSubmit);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TB_MaterialName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.LB_MaterailName);
            this.Controls.Add(this.BT_Cancel);
            this.Controls.Add(this.CB_Category);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LB_Category);
            this.Controls.Add(this.CB_StepNo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LB_StepNo);
            this.Controls.Add(this.CB_MaterialNo);
            this.Controls.Add(this.LB_MaterialNo);
            this.Controls.Add(this.BT_Connect);
            this.Controls.Add(this.CB_Station);
            this.Controls.Add(this.LB_Station);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "智能防错料架";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown_1);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.Reworkpanel.ResumeLayout(false);
            this.Reworkpanel.PerformLayout();
            this.PhotoNopanel.ResumeLayout(false);
            this.PhotoNopanel.PerformLayout();
            this.FeatureCodepanel.ResumeLayout(false);
            this.FeatureCodepanel.PerformLayout();
            this.Programpanel.ResumeLayout(false);
            this.Programpanel.PerformLayout();
            this.Gunpanel.ResumeLayout(false);
            this.Gunpanel.PerformLayout();
            this.MaterialNumpanel.ResumeLayout(false);
            this.MaterialNumpanel.PerformLayout();
            this.MaterialShelfpanel.ResumeLayout(false);
            this.MaterialShelfpanel.PerformLayout();
            this.Sleevepanel.ResumeLayout(false);
            this.Sleevepanel.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LB_Station;
        private System.Windows.Forms.Button BT_Connect;
        private System.Windows.Forms.Label LB_MaterialNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.ComboBox CB_Station;
        public System.Windows.Forms.ComboBox CB_MaterialNo;
        public System.Windows.Forms.TextBox TB_ProductionName;
        private PictureBox pictureBox1;
        private ContextMenuStrip contextMenuStrip1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
        private PictureBox pictureBox4;
        private Label label6;
        private Label label7;
        private Button BT_Eidit;
        public DataGridView dataGridView1;
        private Label label8;
        private DataGridViewTextBoxColumn StepNo;
        private DataGridViewTextBoxColumn Category;
        private DataGridViewTextBoxColumn Desc;
        private DataGridViewTextBoxColumn MaterialShelfNo;
        private DataGridViewTextBoxColumn GunNo;
        private DataGridViewTextBoxColumn ProgrameNo;
        private DataGridViewTextBoxColumn Number;
        private DataGridViewTextBoxColumn FeatureCode;
        private DataGridViewTextBoxColumn SleeveNo;
        private DataGridViewTextBoxColumn PhotoNo;
        private DataGridViewTextBoxColumn Rework_Time;
        private DataGridViewTextBoxColumn INTELLIGENTRACK_DATAIL_ID;
        private DataGridViewTextBoxColumn INTELLIGENTRACK_ID;
        private DataGridViewTextBoxColumn PRODUCTION_ID;
        private DataGridViewTextBoxColumn FEERACK_ID;
        private DataGridViewTextBoxColumn production_name;
        private DataGridViewTextBoxColumn PORDUCTION_TYPE;
        private DataGridViewTextBoxColumn ANOTHERNAME;
        private DataGridView dataGridView2;
        private DataGridViewTextBoxColumn Step_NO;
        private DataGridViewTextBoxColumn Cargory;
        private DataGridViewTextBoxColumn Desc1;
        private DataGridViewTextBoxColumn MaterialShelf_No;
        private DataGridViewTextBoxColumn Material_No;
        private DataGridViewTextBoxColumn Gun_No;
        private DataGridViewTextBoxColumn Program_No;
        private DataGridViewTextBoxColumn FeatureCode1;
        private DataGridViewTextBoxColumn Photo_No;
        private DataGridViewTextBoxColumn Seelve_No;
        private DataGridViewTextBoxColumn Rework_Times;
        private DataGridViewTextBoxColumn IDT;
        private Label LB_StepNo;
        private Label label1;
        private ComboBox CB_StepNo;
        private Label LB_Category;
        private Label label2;
        private ComboBox CB_Category;
        private Label LB_MaterailName;
        private Label label3;
        private TextBox TB_MaterialName;
        private Button BT_StepSubmit;
        private Panel Reworkpanel;
        private TextBox TB_ReworkTimes;
        private Label LB_ReworkTimes;
        private Panel PhotoNopanel;
        private ComboBox CB_PhotoNo;
        private Label LB_PhotoNo;
        private Panel FeatureCodepanel;
        private Label LB_FeatureCode;
        private TextBox TB_FeatureCode;
        private Panel Programpanel;
        private TextBox TB_ProgramNo;
        private Label LB_ProgramNo;
        private Panel Gunpanel;
        private Label LB_GunNo;
        private ComboBox CB_GunNo;
        private Panel MaterialNumpanel;
        private Label LB_MaterialNum;
        private TextBox TB_MaterialNum;
        private Panel MaterialShelfpanel;
        private Label LB_MaterialShelfNo;
        private ComboBox CB_MaterialShelfNo;
        private Panel Sleevepanel;
        private ComboBox TB_SleeveNo;
        private Label LB_SleeveNo;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button BT_MaterialSubmit;
        private Button BT_MaterialCancel;
        private Button BT_Cancel;
        private Button TB_VIEW1_CANEL;
        private ToolStripMenuItem 料架管理ToolStripMenuItem;
        private ToolStripMenuItem 员工管理ToolStripMenuItem;
        private ToolStripMenuItem 增加员工ToolStripMenuItem;
        private ToolStripMenuItem 查询员工ToolStripMenuItem;
        private Label anthername;
    }
}

