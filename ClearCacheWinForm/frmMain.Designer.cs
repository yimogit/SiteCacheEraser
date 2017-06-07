namespace ClearCacheWinForm
{
    partial class frmMain
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
            this.chkKeyList = new System.Windows.Forms.CheckedListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.btnSelectSingle = new System.Windows.Forms.Button();
            this.txtSingleKey = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbGroup = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbSelectEnv = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnIISResert = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelSearch.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkKeyList
            // 
            this.chkKeyList.FormattingEnabled = true;
            this.chkKeyList.Location = new System.Drawing.Point(12, 30);
            this.chkKeyList.Name = "chkKeyList";
            this.chkKeyList.Size = new System.Drawing.Size(656, 212);
            this.chkKeyList.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(680, 421);
            this.panel1.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.txtLog);
            this.panel3.Controls.Add(this.chkKeyList);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 74);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(680, 347);
            this.panel3.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "Redis缓存键值：";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(13, 248);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(655, 96);
            this.txtLog.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panelSearch);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.cmbGroup);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.cmbSelectEnv);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Controls.Add(this.btnClear);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(680, 74);
            this.panel2.TabIndex = 3;
            // 
            // panelSearch
            // 
            this.panelSearch.Controls.Add(this.btnIISResert);
            this.panelSearch.Controls.Add(this.btnSelectSingle);
            this.panelSearch.Controls.Add(this.txtSingleKey);
            this.panelSearch.Controls.Add(this.label4);
            this.panelSearch.Location = new System.Drawing.Point(12, 44);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(379, 39);
            this.panelSearch.TabIndex = 3;
            this.panelSearch.Visible = false;
            // 
            // btnSelectSingle
            // 
            this.btnSelectSingle.Location = new System.Drawing.Point(189, 5);
            this.btnSelectSingle.Name = "btnSelectSingle";
            this.btnSelectSingle.Size = new System.Drawing.Size(75, 23);
            this.btnSelectSingle.TabIndex = 12;
            this.btnSelectSingle.Text = "查询";
            this.btnSelectSingle.UseVisualStyleBackColor = true;
            this.btnSelectSingle.Click += new System.EventHandler(this.btnSelectSingle_Click);
            // 
            // txtSingleKey
            // 
            this.txtSingleKey.Location = new System.Drawing.Point(46, 7);
            this.txtSingleKey.Name = "txtSingleKey";
            this.txtSingleKey.Size = new System.Drawing.Size(121, 21);
            this.txtSingleKey.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "Key：";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(199, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "相关模块：";
            // 
            // cmbGroup
            // 
            this.cmbGroup.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmbGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGroup.FormattingEnabled = true;
            this.cmbGroup.Location = new System.Drawing.Point(270, 19);
            this.cmbGroup.Name = "cmbGroup";
            this.cmbGroup.Size = new System.Drawing.Size(121, 20);
            this.cmbGroup.TabIndex = 5;
            this.cmbGroup.SelectionChangeCommitted += new System.EventHandler(this.cmbGroup_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "环境：";
            // 
            // cmbSelectEnv
            // 
            this.cmbSelectEnv.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.cmbSelectEnv.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSelectEnv.FormattingEnabled = true;
            this.cmbSelectEnv.Location = new System.Drawing.Point(58, 18);
            this.cmbSelectEnv.Name = "cmbSelectEnv";
            this.cmbSelectEnv.Size = new System.Drawing.Size(121, 20);
            this.cmbSelectEnv.TabIndex = 2;
            this.cmbSelectEnv.SelectionChangeCommitted += new System.EventHandler(this.cmbSelectEnv_SelectionChangeCommitted);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.button1.Location = new System.Drawing.Point(533, 18);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "加载数据";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.cmbGroup_SelectionChangeCommitted);
            // 
            // btnClear
            // 
            this.btnClear.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClear.Location = new System.Drawing.Point(427, 17);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "清除选中值";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnIISResert
            // 
            this.btnIISResert.Location = new System.Drawing.Point(282, 6);
            this.btnIISResert.Name = "btnIISResert";
            this.btnIISResert.Size = new System.Drawing.Size(75, 23);
            this.btnIISResert.TabIndex = 13;
            this.btnIISResert.Text = "重启IIS";
            this.btnIISResert.UseVisualStyleBackColor = true;
            this.btnIISResert.Click += new System.EventHandler(this.btnIISResert_Click);
            // 
            // frmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(680, 421);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "redis缓存清理";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckedListBox chkKeyList;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbSelectEnv;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.ComboBox cmbGroup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Button btnSelectSingle;
        private System.Windows.Forms.TextBox txtSingleKey;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnIISResert;
    }
}

