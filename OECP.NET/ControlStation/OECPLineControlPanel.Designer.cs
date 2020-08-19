namespace OECP.NET.ControlStation
{
    partial class OECPLineControlPanel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OECPLineControlPanel));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAddLine = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbDelLine = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddLine,
            this.toolStripSeparator2,
            this.tsbDelLine});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(338, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAddLine
            // 
            this.tsbAddLine.AutoSize = false;
            this.tsbAddLine.CheckOnClick = true;
            this.tsbAddLine.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tsbAddLine.Image = ((System.Drawing.Image)(resources.GetObject("tsbAddLine.Image")));
            this.tsbAddLine.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbAddLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAddLine.Name = "tsbAddLine";
            this.tsbAddLine.Size = new System.Drawing.Size(76, 22);
            this.tsbAddLine.Text = "添加线";
            this.tsbAddLine.CheckStateChanged += new System.EventHandler(this.tsbAddLine_CheckStateChanged);
            this.tsbAddLine.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tsbAddLine_MouseDown);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbDelLine
            // 
            this.tsbDelLine.AutoSize = false;
            this.tsbDelLine.CheckOnClick = true;
            this.tsbDelLine.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tsbDelLine.Image = ((System.Drawing.Image)(resources.GetObject("tsbDelLine.Image")));
            this.tsbDelLine.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbDelLine.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelLine.Name = "tsbDelLine";
            this.tsbDelLine.Size = new System.Drawing.Size(76, 22);
            this.tsbDelLine.Text = "删除线";
            this.tsbDelLine.CheckStateChanged += new System.EventHandler(this.tsbDelLine_CheckStateChanged);
            this.tsbDelLine.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tsbDelLine_MouseDown);
            // 
            // OECPLineControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.toolStrip1);
            this.Name = "OECPLineControlPanel";
            this.Size = new System.Drawing.Size(338, 215);
            this.Resize += new System.EventHandler(this.OECPLineControlPanel_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbAddLine;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsbDelLine;
    }
}
