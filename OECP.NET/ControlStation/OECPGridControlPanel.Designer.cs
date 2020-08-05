namespace OECP.NET.ControlStation
{
    partial class OECPGridControlPanel
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.nudGridNum = new System.Windows.Forms.NumericUpDown();
            this.lblGridNum = new System.Windows.Forms.Label();
            this.btnM2 = new System.Windows.Forms.Button();
            this.btnD2 = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudGridNum)).BeginInit();
            this.SuspendLayout();
            // 
            // nudGridNum
            // 
            this.nudGridNum.AutoSize = true;
            this.nudGridNum.Location = new System.Drawing.Point(93, 23);
            this.nudGridNum.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.nudGridNum.Name = "nudGridNum";
            this.nudGridNum.Size = new System.Drawing.Size(135, 21);
            this.nudGridNum.TabIndex = 0;
            this.nudGridNum.ValueChanged += new System.EventHandler(this.nudGridNum_ValueChanged);
            // 
            // lblGridNum
            // 
            this.lblGridNum.AutoSize = true;
            this.lblGridNum.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblGridNum.Location = new System.Drawing.Point(18, 26);
            this.lblGridNum.Name = "lblGridNum";
            this.lblGridNum.Size = new System.Drawing.Size(65, 12);
            this.lblGridNum.TabIndex = 1;
            this.lblGridNum.Text = "输入等分数";
            // 
            // btnM2
            // 
            this.btnM2.AutoSize = true;
            this.btnM2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnM2.Location = new System.Drawing.Point(17, 64);
            this.btnM2.Name = "btnM2";
            this.btnM2.Size = new System.Drawing.Size(67, 23);
            this.btnM2.TabIndex = 2;
            this.btnM2.Text = "X2";
            this.btnM2.UseVisualStyleBackColor = true;
            this.btnM2.Click += new System.EventHandler(this.btnM2_Click);
            // 
            // btnD2
            // 
            this.btnD2.AutoSize = true;
            this.btnD2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnD2.Location = new System.Drawing.Point(90, 64);
            this.btnD2.Name = "btnD2";
            this.btnD2.Size = new System.Drawing.Size(62, 23);
            this.btnD2.TabIndex = 3;
            this.btnD2.Text = "X1/2";
            this.btnD2.UseVisualStyleBackColor = true;
            this.btnD2.Click += new System.EventHandler(this.btnD2_Click);
            // 
            // btnReset
            // 
            this.btnReset.AutoSize = true;
            this.btnReset.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnReset.Location = new System.Drawing.Point(158, 64);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(71, 23);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "重置";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // OECPGridControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnD2);
            this.Controls.Add(this.btnM2);
            this.Controls.Add(this.lblGridNum);
            this.Controls.Add(this.nudGridNum);
            this.Name = "OECPGridControlPanel";
            this.Size = new System.Drawing.Size(253, 103);
            this.Resize += new System.EventHandler(this.OECPGridControlPanel_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.nudGridNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudGridNum;
        private System.Windows.Forms.Label lblGridNum;
        private System.Windows.Forms.Button btnM2;
        private System.Windows.Forms.Button btnD2;
        private System.Windows.Forms.Button btnReset;
    }
}
