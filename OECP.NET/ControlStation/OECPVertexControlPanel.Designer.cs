namespace OECP.NET.ControlStation
{
    partial class OECPVertexControlPanel
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
            this.rdBtnAddVtx = new System.Windows.Forms.RadioButton();
            this.rdBtnDelVtx = new System.Windows.Forms.RadioButton();
            this.rdBtnSpare = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // rdBtnAddVtx
            // 
            this.rdBtnAddVtx.AutoSize = true;
            this.rdBtnAddVtx.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rdBtnAddVtx.Location = new System.Drawing.Point(97, 31);
            this.rdBtnAddVtx.Name = "rdBtnAddVtx";
            this.rdBtnAddVtx.Size = new System.Drawing.Size(71, 16);
            this.rdBtnAddVtx.TabIndex = 0;
            this.rdBtnAddVtx.TabStop = true;
            this.rdBtnAddVtx.Text = "添加节点";
            this.rdBtnAddVtx.UseVisualStyleBackColor = true;
            // 
            // rdBtnDelVtx
            // 
            this.rdBtnDelVtx.AutoSize = true;
            this.rdBtnDelVtx.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rdBtnDelVtx.Location = new System.Drawing.Point(190, 31);
            this.rdBtnDelVtx.Name = "rdBtnDelVtx";
            this.rdBtnDelVtx.Size = new System.Drawing.Size(71, 16);
            this.rdBtnDelVtx.TabIndex = 1;
            this.rdBtnDelVtx.TabStop = true;
            this.rdBtnDelVtx.Text = "删除节点";
            this.rdBtnDelVtx.UseVisualStyleBackColor = true;
            // 
            // rdBtnSpare
            // 
            this.rdBtnSpare.AutoSize = true;
            this.rdBtnSpare.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.rdBtnSpare.Location = new System.Drawing.Point(15, 31);
            this.rdBtnSpare.Name = "rdBtnSpare";
            this.rdBtnSpare.Size = new System.Drawing.Size(47, 16);
            this.rdBtnSpare.TabIndex = 2;
            this.rdBtnSpare.TabStop = true;
            this.rdBtnSpare.Text = "挂起";
            this.rdBtnSpare.UseVisualStyleBackColor = true;
            // 
            // OECPVertexControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.rdBtnSpare);
            this.Controls.Add(this.rdBtnDelVtx);
            this.Controls.Add(this.rdBtnAddVtx);
            this.Name = "OECPVertexControlPanel";
            this.Size = new System.Drawing.Size(287, 70);
            this.Resize += new System.EventHandler(this.OECPVertexControlPanel_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdBtnAddVtx;
        private System.Windows.Forms.RadioButton rdBtnDelVtx;
        private System.Windows.Forms.RadioButton rdBtnSpare;
    }
}
