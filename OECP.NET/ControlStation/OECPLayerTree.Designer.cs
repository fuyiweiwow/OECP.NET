using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CCWin.SkinControl;
using HZH_Controls.Controls;
using OECP.NET.ControlStation.BaseControl;

namespace OECP.NET.ControlStation
{
    partial class OECPLayerTree
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OECPLayerTree));
            this.panel1 = new System.Windows.Forms.Panel();
            this.treeLayerImages = new System.Windows.Forms.ImageList(this.components);
            this.tabControlExt1 = new HZH_Controls.Controls.TabControlExt();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.layerTree = new OECP.NET.ControlStation.BaseControl.BaseTreeView();
            this.tabControlExt1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(283, 20);
            this.panel1.TabIndex = 0;
            // 
            // treeLayerImages
            // 
            this.treeLayerImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("treeLayerImages.ImageStream")));
            this.treeLayerImages.TransparentColor = System.Drawing.Color.Transparent;
            this.treeLayerImages.Images.SetKeyName(0, "dot  black.png");
            this.treeLayerImages.Images.SetKeyName(1, "grey line.png");
            this.treeLayerImages.Images.SetKeyName(2, "grid.png");
            this.treeLayerImages.Images.SetKeyName(3, "simple blue line.png");
            this.treeLayerImages.Images.SetKeyName(4, "simple red line.png");
            this.treeLayerImages.Images.SetKeyName(5, "layer.png");
            // 
            // tabControlExt1
            // 
            this.tabControlExt1.CloseBtnColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(85)))), ((int)(((byte)(51)))));
            this.tabControlExt1.Controls.Add(this.tabPage1);
            this.tabControlExt1.Controls.Add(this.tabPage2);
            this.tabControlExt1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlExt1.HeaderBackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabControlExt1.HeadSelectedBackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tabControlExt1.IsShowCloseBtn = false;
            this.tabControlExt1.ItemSize = new System.Drawing.Size(0, 30);
            this.tabControlExt1.Location = new System.Drawing.Point(0, 204);
            this.tabControlExt1.Name = "tabControlExt1";
            this.tabControlExt1.SelectedIndex = 0;
            this.tabControlExt1.Size = new System.Drawing.Size(283, 246);
            this.tabControlExt1.TabIndex = 2;
            this.tabControlExt1.UncloseTabIndexs = null;
            // 
            // tabPage1
            // 
            this.tabPage1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(275, 208);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "图层操作";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(275, 208);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "画布检查";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // layerTree
            // 
            this.layerTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.layerTree.CheckBoxes = true;
            this.layerTree.Dock = System.Windows.Forms.DockStyle.Top;
            this.layerTree.ForeColor = System.Drawing.SystemColors.InfoText;
            this.layerTree.FullRowSelect = true;
            this.layerTree.ImageIndex = 0;
            this.layerTree.ImageList = this.treeLayerImages;
            this.layerTree.Indent = 30;
            this.layerTree.ItemHeight = 25;
            this.layerTree.LineColor = System.Drawing.Color.White;
            this.layerTree.Location = new System.Drawing.Point(0, 20);
            this.layerTree.Name = "layerTree";
            this.layerTree.SelectedImageIndex = 0;
            this.layerTree.ShowPlusMinus = false;
            this.layerTree.ShowRootLines = false;
            this.layerTree.Size = new System.Drawing.Size(283, 184);
            this.layerTree.TabIndex = 1;
            this.layerTree.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.layerTree_BeforeCheck);
            this.layerTree.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.layerTree_AfterCheck);
            this.layerTree.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.layerTree_BeforeSelect);
            this.layerTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.layerTree_AfterSelect);
            // 
            // OECPLayerTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(283, 450);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.tabControlExt1);
            this.Controls.Add(this.layerTree);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.HideOnClose = true;
            this.Name = "OECPLayerTree";
            this.Text = "图层";
            this.tabControlExt1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Panel panel1;
        private BaseTreeView layerTree;
        private TabControlExt tabControlExt1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private ImageList treeLayerImages;
    }
}