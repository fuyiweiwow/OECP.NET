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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OECPVertexControlPanel));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAddVtx = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDelVtx = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddVtx,
            this.toolStripSeparator2,
            this.tsbDelVtx});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(498, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAddVtx
            // 
            this.tsbAddVtx.AutoSize = false;
            this.tsbAddVtx.CheckOnClick = true;
            this.tsbAddVtx.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tsbAddVtx.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddVtx.Image")));
            this.tsbAddVtx.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbAddVtx.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddVtx.Name = "tsbAddVtx";
            this.tsbAddVtx.Size = new System.Drawing.Size(76, 22);
            this.tsbAddVtx.Text = "添加节点";
            this.tsbAddVtx.CheckStateChanged += new System.EventHandler(this.tsbAddVtx_CheckStateChanged);
            this.tsbAddVtx.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tsbAddVtx_MouseDown);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbDelVtx
            // 
            this.tsbDelVtx.AutoSize = false;
            this.tsbDelVtx.CheckOnClick = true;
            this.tsbDelVtx.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tsbDelVtx.Image = ((System.Drawing.Image)(resources.GetObject("tsbDelVtx.Image")));
            this.tsbDelVtx.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbDelVtx.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelVtx.Name = "tsbDelVtx";
            this.tsbDelVtx.Size = new System.Drawing.Size(76, 22);
            this.tsbDelVtx.Text = "删除节点";
            this.tsbDelVtx.CheckStateChanged += new System.EventHandler(this.tsbDelVtx_CheckStateChanged);
            this.tsbDelVtx.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tsbDelVtx_MouseDown);
            // 
            // OECPVertexControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.toolStrip1);
            this.Name = "OECPVertexControlPanel";
            this.Size = new System.Drawing.Size(498, 177);
            this.Resize += new System.EventHandler(this.OECPVertexControlPanel_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAddVtx;
        private System.Windows.Forms.ToolStripButton tsbDelVtx;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}
