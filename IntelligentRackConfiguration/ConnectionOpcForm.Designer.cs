namespace IntelligentRackConfiguration
{
    partial class ConnectionOpcForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConnectionOpcForm));
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.STEP_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CATEGORY = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MATERIALSHELF_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GUN_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PROGRAME_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MATERIAL_NUMBER = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FEATURE_CODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REWORK_TIMES = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SLEEVE_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PHOTO_NO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRODUCTION_NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(598, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "关闭OPC";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(12, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(661, 270);
            this.panel1.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STEP_NO,
            this.CATEGORY,
            this.NAME,
            this.MATERIALSHELF_NO,
            this.GUN_NO,
            this.PROGRAME_NO,
            this.MATERIAL_NUMBER,
            this.FEATURE_CODE,
            this.REWORK_TIMES,
            this.SLEEVE_NO,
            this.PHOTO_NO,
            this.PRODUCTION_NAME});
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(661, 267);
            this.dataGridView1.TabIndex = 0;
            // 
            // STEP_NO
            // 
            this.STEP_NO.DataPropertyName = "STEP_NO";
            this.STEP_NO.HeaderText = "步序";
            this.STEP_NO.Name = "STEP_NO";
            // 
            // CATEGORY
            // 
            this.CATEGORY.DataPropertyName = "CATEGORY";
            this.CATEGORY.HeaderText = "类别";
            this.CATEGORY.Name = "CATEGORY";
            // 
            // NAME
            // 
            this.NAME.DataPropertyName = "NAME";
            this.NAME.HeaderText = "名称";
            this.NAME.Name = "NAME";
            // 
            // MATERIALSHELF_NO
            // 
            this.MATERIALSHELF_NO.DataPropertyName = "MATERIALSHELF_NO";
            this.MATERIALSHELF_NO.HeaderText = "料格号";
            this.MATERIALSHELF_NO.Name = "MATERIALSHELF_NO";
            // 
            // GUN_NO
            // 
            this.GUN_NO.DataPropertyName = "GUN_NO";
            this.GUN_NO.HeaderText = "枪号";
            this.GUN_NO.Name = "GUN_NO";
            // 
            // PROGRAME_NO
            // 
            this.PROGRAME_NO.DataPropertyName = "PROGRAME_NO";
            this.PROGRAME_NO.HeaderText = "程序号";
            this.PROGRAME_NO.Name = "PROGRAME_NO";
            // 
            // MATERIAL_NUMBER
            // 
            this.MATERIAL_NUMBER.DataPropertyName = "MATERIAL_NUMBER";
            this.MATERIAL_NUMBER.HeaderText = "数量";
            this.MATERIAL_NUMBER.Name = "MATERIAL_NUMBER";
            // 
            // FEATURE_CODE
            // 
            this.FEATURE_CODE.DataPropertyName = "FEATURE_CODE";
            this.FEATURE_CODE.HeaderText = "特征码";
            this.FEATURE_CODE.Name = "FEATURE_CODE";
            // 
            // REWORK_TIMES
            // 
            this.REWORK_TIMES.DataPropertyName = "REWORK_TIMES";
            this.REWORK_TIMES.HeaderText = "返工次数";
            this.REWORK_TIMES.Name = "REWORK_TIMES";
            // 
            // SLEEVE_NO
            // 
            this.SLEEVE_NO.DataPropertyName = "SLEEVE_NO";
            this.SLEEVE_NO.HeaderText = "套筒号";
            this.SLEEVE_NO.Name = "SLEEVE_NO";
            // 
            // PHOTO_NO
            // 
            this.PHOTO_NO.DataPropertyName = "PHOTO_NO";
            this.PHOTO_NO.HeaderText = "相机号";
            this.PHOTO_NO.Name = "PHOTO_NO";
            // 
            // PRODUCTION_NAME
            // 
            this.PRODUCTION_NAME.DataPropertyName = "PRODUCTION_NAME";
            this.PRODUCTION_NAME.HeaderText = "产品名称";
            this.PRODUCTION_NAME.Name = "PRODUCTION_NAME";
            // 
            // ConnectionOpcForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 335);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConnectionOpcForm";
            this.Text = "监听OPC";
            this.Load += new System.EventHandler(this.ConnectionOpcForm_Load);
            this.Shown += new System.EventHandler(this.ConnectionOpcForm_Shown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.DataGridViewTextBoxColumn STEP_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CATEGORY;
        private System.Windows.Forms.DataGridViewTextBoxColumn NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn MATERIALSHELF_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn GUN_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PROGRAME_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn MATERIAL_NUMBER;
        private System.Windows.Forms.DataGridViewTextBoxColumn FEATURE_CODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn REWORK_TIMES;
        private System.Windows.Forms.DataGridViewTextBoxColumn SLEEVE_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PHOTO_NO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRODUCTION_NAME;
    }
}