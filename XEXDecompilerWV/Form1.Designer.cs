namespace XEXDecompilerWV
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openASMToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportSingleStep1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportSingleStep2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAllStep1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportAllStep2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.decompileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.step1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.step2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.pb1 = new System.Windows.Forms.ToolStripProgressBar();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.rtb1 = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.rtb2 = new System.Windows.Forms.RichTextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.rtb3 = new System.Windows.Forms.RichTextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.midStepToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.decompileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(883, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openASMToolStripMenuItem,
            this.exportSingleStep1ToolStripMenuItem,
            this.exportSingleStep2ToolStripMenuItem,
            this.exportAllStep1ToolStripMenuItem,
            this.exportAllStep2ToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openASMToolStripMenuItem
            // 
            this.openASMToolStripMenuItem.Name = "openASMToolStripMenuItem";
            this.openASMToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.openASMToolStripMenuItem.Text = "Open ASM";
            this.openASMToolStripMenuItem.Click += new System.EventHandler(this.openASMToolStripMenuItem_Click);
            // 
            // exportSingleStep1ToolStripMenuItem
            // 
            this.exportSingleStep1ToolStripMenuItem.Name = "exportSingleStep1ToolStripMenuItem";
            this.exportSingleStep1ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exportSingleStep1ToolStripMenuItem.Text = "Export Single Step1...";
            this.exportSingleStep1ToolStripMenuItem.Click += new System.EventHandler(this.exportSingleStep1ToolStripMenuItem_Click);
            // 
            // exportSingleStep2ToolStripMenuItem
            // 
            this.exportSingleStep2ToolStripMenuItem.Name = "exportSingleStep2ToolStripMenuItem";
            this.exportSingleStep2ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exportSingleStep2ToolStripMenuItem.Text = "Export Single Step2...";
            this.exportSingleStep2ToolStripMenuItem.Click += new System.EventHandler(this.exportSingleStep2ToolStripMenuItem_Click);
            // 
            // exportAllStep1ToolStripMenuItem
            // 
            this.exportAllStep1ToolStripMenuItem.Name = "exportAllStep1ToolStripMenuItem";
            this.exportAllStep1ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exportAllStep1ToolStripMenuItem.Text = "Export All Step1...";
            this.exportAllStep1ToolStripMenuItem.Click += new System.EventHandler(this.exportAllStep1ToolStripMenuItem_Click);
            // 
            // exportAllStep2ToolStripMenuItem
            // 
            this.exportAllStep2ToolStripMenuItem.Name = "exportAllStep2ToolStripMenuItem";
            this.exportAllStep2ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exportAllStep2ToolStripMenuItem.Text = "Export All Step2...";
            this.exportAllStep2ToolStripMenuItem.Click += new System.EventHandler(this.exportAllStep2ToolStripMenuItem_Click);
            // 
            // decompileToolStripMenuItem
            // 
            this.decompileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.step1ToolStripMenuItem,
            this.step2ToolStripMenuItem,
            this.midStepToolStripMenuItem});
            this.decompileToolStripMenuItem.Name = "decompileToolStripMenuItem";
            this.decompileToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.decompileToolStripMenuItem.Text = "Decompile";
            // 
            // step1ToolStripMenuItem
            // 
            this.step1ToolStripMenuItem.Checked = true;
            this.step1ToolStripMenuItem.CheckOnClick = true;
            this.step1ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.step1ToolStripMenuItem.Name = "step1ToolStripMenuItem";
            this.step1ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.step1ToolStripMenuItem.Text = "Step 1";
            this.step1ToolStripMenuItem.Click += new System.EventHandler(this.step1ToolStripMenuItem_Click);
            // 
            // step2ToolStripMenuItem
            // 
            this.step2ToolStripMenuItem.Checked = true;
            this.step2ToolStripMenuItem.CheckOnClick = true;
            this.step2ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.step2ToolStripMenuItem.Name = "step2ToolStripMenuItem";
            this.step2ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.step2ToolStripMenuItem.Text = "Step 2";
            this.step2ToolStripMenuItem.Click += new System.EventHandler(this.step2ToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pb1,
            this.status});
            this.statusStrip1.Location = new System.Drawing.Point(0, 507);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(883, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // pb1
            // 
            this.pb1.Name = "pb1";
            this.pb1.Size = new System.Drawing.Size(100, 16);
            // 
            // status
            // 
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(38, 17);
            this.status.Text = "Ready";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 49);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.listBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(883, 458);
            this.splitContainer1.SplitterDistance = 300;
            this.splitContainer1.TabIndex = 2;
            // 
            // listBox1
            // 
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.IntegralHeight = false;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(300, 458);
            this.listBox1.TabIndex = 0;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(579, 458);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.rtb1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(571, 432);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Assembler";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // rtb1
            // 
            this.rtb1.DetectUrls = false;
            this.rtb1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb1.HideSelection = false;
            this.rtb1.Location = new System.Drawing.Point(3, 3);
            this.rtb1.Name = "rtb1";
            this.rtb1.Size = new System.Drawing.Size(565, 426);
            this.rtb1.TabIndex = 0;
            this.rtb1.Text = "";
            this.rtb1.WordWrap = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.rtb2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(571, 432);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Step1";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // rtb2
            // 
            this.rtb2.DetectUrls = false;
            this.rtb2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb2.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb2.HideSelection = false;
            this.rtb2.Location = new System.Drawing.Point(3, 3);
            this.rtb2.Name = "rtb2";
            this.rtb2.Size = new System.Drawing.Size(565, 426);
            this.rtb2.TabIndex = 1;
            this.rtb2.Text = "";
            this.rtb2.WordWrap = false;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.rtb3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(571, 432);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Step 2";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // rtb3
            // 
            this.rtb3.DetectUrls = false;
            this.rtb3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb3.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb3.HideSelection = false;
            this.rtb3.Location = new System.Drawing.Point(3, 3);
            this.rtb3.Name = "rtb3";
            this.rtb3.Size = new System.Drawing.Size(565, 426);
            this.rtb3.TabIndex = 2;
            this.rtb3.Text = "";
            this.rtb3.WordWrap = false;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.treeView1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(571, 432);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "MidStep";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.Location = new System.Drawing.Point(3, 3);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(565, 426);
            this.treeView1.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(883, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(200, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(44, 22);
            this.toolStripButton1.Tag = "";
            this.toolStripButton1.Text = "Search";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // midStepToolStripMenuItem
            // 
            this.midStepToolStripMenuItem.Checked = true;
            this.midStepToolStripMenuItem.CheckOnClick = true;
            this.midStepToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.midStepToolStripMenuItem.Name = "midStepToolStripMenuItem";
            this.midStepToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.midStepToolStripMenuItem.Text = "MidStep";
            this.midStepToolStripMenuItem.Click += new System.EventHandler(this.midStepToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(883, 529);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "XEX Assembler Decompiler by Warranty Voider";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openASMToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar pb1;
        private System.Windows.Forms.ToolStripStatusLabel status;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox rtb1;
        private System.Windows.Forms.ToolStripMenuItem decompileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem step1ToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.RichTextBox rtb2;
        private System.Windows.Forms.ToolStripMenuItem step2ToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.RichTextBox rtb3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripMenuItem exportSingleStep1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportSingleStep2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportAllStep1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportAllStep2ToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ToolStripMenuItem midStepToolStripMenuItem;
    }
}

