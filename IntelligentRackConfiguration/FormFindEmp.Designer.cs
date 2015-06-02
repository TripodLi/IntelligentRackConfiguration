namespace IntelligentRackConfiguration
{
    partial class FormFindEmp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormFindEmp));
            this.LB_EmpStation = new System.Windows.Forms.Label();
            this.CB_EmpStation = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.BT_Sure = new System.Windows.Forms.Button();
            this.BT_Canel = new System.Windows.Forms.Button();
            this.EmpNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EmpName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Station = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FeeRack = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EMP_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // LB_EmpStation
            // 
            this.LB_EmpStation.AutoSize = true;
            this.LB_EmpStation.Location = new System.Drawing.Point(12, 27);
            this.LB_EmpStation.Name = "LB_EmpStation";
            this.LB_EmpStation.Size = new System.Drawing.Size(65, 12);
            this.LB_EmpStation.TabIndex = 0;
            this.LB_EmpStation.Text = "所属料架：";
            // 
            // CB_EmpStation
            // 
            this.CB_EmpStation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_EmpStation.FormattingEnabled = true;
            this.CB_EmpStation.Location = new System.Drawing.Point(73, 24);
            this.CB_EmpStation.Name = "CB_EmpStation";
            this.CB_EmpStation.Size = new System.Drawing.Size(113, 20);
            this.CB_EmpStation.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(14, 53);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(334, 153);
            this.panel1.TabIndex = 2;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EmpNo,
            this.EmpName,
            this.Station,
            this.FeeRack,
            this.EMP_ID});
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(334, 150);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.dataGridView1_UserDeletedRow);
            this.dataGridView1.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler(this.dataGridView1_UserDeletingRow);
            // 
            // BT_Sure
            // 
            this.BT_Sure.Location = new System.Drawing.Point(192, 24);
            this.BT_Sure.Name = "BT_Sure";
            this.BT_Sure.Size = new System.Drawing.Size(75, 23);
            this.BT_Sure.TabIndex = 3;
            this.BT_Sure.Text = "确定";
            this.BT_Sure.UseVisualStyleBackColor = true;
            this.BT_Sure.Click += new System.EventHandler(this.BT_Sure_Click);
            // 
            // BT_Canel
            // 
            this.BT_Canel.Location = new System.Drawing.Point(273, 22);
            this.BT_Canel.Name = "BT_Canel";
            this.BT_Canel.Size = new System.Drawing.Size(75, 23);
            this.BT_Canel.TabIndex = 4;
            this.BT_Canel.Text = "删除";
            this.BT_Canel.UseVisualStyleBackColor = true;
            this.BT_Canel.Click += new System.EventHandler(this.BT_Canel_Click);
            // 
            // EmpNo
            // 
            this.EmpNo.DataPropertyName = "EMP_NO";
            this.EmpNo.HeaderText = "员工编号";
            this.EmpNo.Name = "EmpNo";
            // 
            // EmpName
            // 
            this.EmpName.DataPropertyName = "EMP_NAME";
            this.EmpName.HeaderText = "员工姓名";
            this.EmpName.Name = "EmpName";
            // 
            // Station
            // 
            this.Station.DataPropertyName = "FEERACK_NAME";
            this.Station.HeaderText = "所属料架";
            this.Station.Name = "Station";
            // 
            // FeeRack
            // 
            this.FeeRack.DataPropertyName = "FEERACK_ID";
            this.FeeRack.HeaderText = "料架ID";
            this.FeeRack.Name = "FeeRack";
            this.FeeRack.Visible = false;
            // 
            // EMP_ID
            // 
            this.EMP_ID.DataPropertyName = "EMP_ID";
            this.EMP_ID.HeaderText = "员工ID";
            this.EMP_ID.Name = "EMP_ID";
            this.EMP_ID.Visible = false;
            // 
            // FormFindEmp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(349, 218);
            this.Controls.Add(this.BT_Canel);
            this.Controls.Add(this.BT_Sure);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.CB_EmpStation);
            this.Controls.Add(this.LB_EmpStation);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormFindEmp";
            this.Text = "查找及删除员工";
            this.Load += new System.EventHandler(this.FormFindEmp_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LB_EmpStation;
        private System.Windows.Forms.ComboBox CB_EmpStation;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BT_Sure;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button BT_Canel;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn EmpName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Station;
        private System.Windows.Forms.DataGridViewTextBoxColumn FeeRack;
        private System.Windows.Forms.DataGridViewTextBoxColumn EMP_ID;
    }
}