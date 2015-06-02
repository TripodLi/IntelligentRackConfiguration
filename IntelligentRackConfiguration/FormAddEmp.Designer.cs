namespace IntelligentRackConfiguration
{
    partial class FormAddEmp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAddEmp));
            this.LB_EmpNo = new System.Windows.Forms.Label();
            this.TB_EmpNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.LB_EmpName = new System.Windows.Forms.Label();
            this.TB_EmpName = new System.Windows.Forms.TextBox();
            this.LB_EmpStation = new System.Windows.Forms.Label();
            this.CB_EmpStation = new System.Windows.Forms.ComboBox();
            this.BT_AddEmp = new System.Windows.Forms.Button();
            this.BT_Canel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LB_EmpNo
            // 
            this.LB_EmpNo.AutoSize = true;
            this.LB_EmpNo.Location = new System.Drawing.Point(12, 27);
            this.LB_EmpNo.Name = "LB_EmpNo";
            this.LB_EmpNo.Size = new System.Drawing.Size(65, 12);
            this.LB_EmpNo.TabIndex = 0;
            this.LB_EmpNo.Text = "员工编号：";
            // 
            // TB_EmpNo
            // 
            this.TB_EmpNo.Location = new System.Drawing.Point(72, 24);
            this.TB_EmpNo.Name = "TB_EmpNo";
            this.TB_EmpNo.Size = new System.Drawing.Size(177, 21);
            this.TB_EmpNo.TabIndex = 1;
            this.TB_EmpNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_EmpNo_KeyDown);
            this.TB_EmpNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TB_EmpNo_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(255, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 10);
            this.label1.TabIndex = 2;
            this.label1.Text = "（请填整数）";
            // 
            // LB_EmpName
            // 
            this.LB_EmpName.AutoSize = true;
            this.LB_EmpName.Location = new System.Drawing.Point(12, 73);
            this.LB_EmpName.Name = "LB_EmpName";
            this.LB_EmpName.Size = new System.Drawing.Size(65, 12);
            this.LB_EmpName.TabIndex = 3;
            this.LB_EmpName.Text = "员工姓名：";
            // 
            // TB_EmpName
            // 
            this.TB_EmpName.Location = new System.Drawing.Point(72, 70);
            this.TB_EmpName.Name = "TB_EmpName";
            this.TB_EmpName.Size = new System.Drawing.Size(177, 21);
            this.TB_EmpName.TabIndex = 4;
            // 
            // LB_EmpStation
            // 
            this.LB_EmpStation.AutoSize = true;
            this.LB_EmpStation.Location = new System.Drawing.Point(12, 121);
            this.LB_EmpStation.Name = "LB_EmpStation";
            this.LB_EmpStation.Size = new System.Drawing.Size(65, 12);
            this.LB_EmpStation.TabIndex = 5;
            this.LB_EmpStation.Text = "所属料架：";
            // 
            // CB_EmpStation
            // 
            this.CB_EmpStation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_EmpStation.FormattingEnabled = true;
            this.CB_EmpStation.Location = new System.Drawing.Point(72, 118);
            this.CB_EmpStation.Name = "CB_EmpStation";
            this.CB_EmpStation.Size = new System.Drawing.Size(177, 20);
            this.CB_EmpStation.TabIndex = 6;
            // 
            // BT_AddEmp
            // 
            this.BT_AddEmp.Location = new System.Drawing.Point(72, 167);
            this.BT_AddEmp.Name = "BT_AddEmp";
            this.BT_AddEmp.Size = new System.Drawing.Size(75, 23);
            this.BT_AddEmp.TabIndex = 7;
            this.BT_AddEmp.Text = "确定";
            this.BT_AddEmp.UseVisualStyleBackColor = true;
            this.BT_AddEmp.Click += new System.EventHandler(this.BT_AddEmp_Click);
            // 
            // BT_Canel
            // 
            this.BT_Canel.Location = new System.Drawing.Point(174, 167);
            this.BT_Canel.Name = "BT_Canel";
            this.BT_Canel.Size = new System.Drawing.Size(75, 23);
            this.BT_Canel.TabIndex = 8;
            this.BT_Canel.Text = "取消";
            this.BT_Canel.UseVisualStyleBackColor = true;
            this.BT_Canel.Click += new System.EventHandler(this.BT_Canel_Click);
            // 
            // FormAddEmp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(336, 202);
            this.Controls.Add(this.BT_Canel);
            this.Controls.Add(this.BT_AddEmp);
            this.Controls.Add(this.CB_EmpStation);
            this.Controls.Add(this.LB_EmpStation);
            this.Controls.Add(this.TB_EmpName);
            this.Controls.Add(this.LB_EmpName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TB_EmpNo);
            this.Controls.Add(this.LB_EmpNo);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormAddEmp";
            this.Text = "增加员工";
            this.Load += new System.EventHandler(this.FormAddEmp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LB_EmpNo;
        private System.Windows.Forms.TextBox TB_EmpNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label LB_EmpName;
        private System.Windows.Forms.TextBox TB_EmpName;
        private System.Windows.Forms.Label LB_EmpStation;
        private System.Windows.Forms.ComboBox CB_EmpStation;
        private System.Windows.Forms.Button BT_AddEmp;
        private System.Windows.Forms.Button BT_Canel;
    }
}