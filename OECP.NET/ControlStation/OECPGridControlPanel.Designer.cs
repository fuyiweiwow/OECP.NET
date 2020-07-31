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
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudGridNum)).BeginInit();
            this.SuspendLayout();
            // 
            // nudGridNum
            // 
            this.nudGridNum.Location = new System.Drawing.Point(86, 148);
            this.nudGridNum.Name = "nudGridNum";
            this.nudGridNum.Size = new System.Drawing.Size(159, 21);
            this.nudGridNum.TabIndex = 0;
            // 
            // lblGridNum
            // 
            this.lblGridNum.AutoSize = true;
            this.lblGridNum.Location = new System.Drawing.Point(15, 153);
            this.lblGridNum.Name = "lblGridNum";
            this.lblGridNum.Size = new System.Drawing.Size(65, 12);
            this.lblGridNum.TabIndex = 1;
            this.lblGridNum.Text = "输入网格数";
            // 
            // btnM2
            // 
            this.btnM2.Location = new System.Drawing.Point(17, 64);
            this.btnM2.Name = "btnM2";
            this.btnM2.Size = new System.Drawing.Size(75, 23);
            this.btnM2.TabIndex = 2;
            this.btnM2.Text = "X2";
            this.btnM2.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(138, 64);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "X1/2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // OECPGridControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnM2);
            this.Controls.Add(this.lblGridNum);
            this.Controls.Add(this.nudGridNum);
            this.Name = "OECPGridControlPanel";
            this.Size = new System.Drawing.Size(262, 185);
            ((System.ComponentModel.ISupportInitialize)(this.nudGridNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudGridNum;
        private System.Windows.Forms.Label lblGridNum;
        private System.Windows.Forms.Button btnM2;
        private System.Windows.Forms.Button button2;
    }
}
