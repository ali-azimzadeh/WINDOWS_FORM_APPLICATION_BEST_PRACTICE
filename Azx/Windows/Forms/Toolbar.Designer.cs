
namespace Azx.Windows.Forms
{
    partial class Toolbar
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Toolbar));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.insertButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.updateButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.deleteButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.serachButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.printButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.chartButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.exitButton = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.actionProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertButton,
            this.updateButton,
            this.deleteButton,
            this.toolStripStatusLabel1,
            this.serachButton,
            this.printButton,
            this.chartButton,
            this.toolStripStatusLabel2,
            this.exitButton,
            this.toolStripStatusLabel3,
            this.actionProgressBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 0);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip1.ShowItemToolTips = true;
            this.statusStrip1.Size = new System.Drawing.Size(620, 43);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // insertButton
            // 
            this.insertButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.insertButton.Image = ((System.Drawing.Image)(resources.GetObject("insertButton.Image")));
            this.insertButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.insertButton.Name = "insertButton";
            this.insertButton.ShowDropDownArrow = false;
            this.insertButton.Size = new System.Drawing.Size(36, 41);
            this.insertButton.Text = "toolStripDropDownButton1";
            this.insertButton.Click += new System.EventHandler(this.insertButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.updateButton.Image = global::Azx.Properties.Resources.update;
            this.updateButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.updateButton.Name = "updateButton";
            this.updateButton.ShowDropDownArrow = false;
            this.updateButton.Size = new System.Drawing.Size(36, 41);
            this.updateButton.Text = "toolStripDropDownButton2";
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.deleteButton.Image = global::Azx.Properties.Resources.delete;
            this.deleteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.ShowDropDownArrow = false;
            this.deleteButton.Size = new System.Drawing.Size(36, 41);
            this.deleteButton.Text = "toolStripDropDownButton1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(10, 38);
            this.toolStripStatusLabel1.Text = "|";
            // 
            // serachButton
            // 
            this.serachButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.serachButton.Image = global::Azx.Properties.Resources.search;
            this.serachButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.serachButton.Name = "serachButton";
            this.serachButton.ShowDropDownArrow = false;
            this.serachButton.Size = new System.Drawing.Size(36, 41);
            // 
            // printButton
            // 
            this.printButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printButton.Image = global::Azx.Properties.Resources.print;
            this.printButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printButton.Name = "printButton";
            this.printButton.ShowDropDownArrow = false;
            this.printButton.Size = new System.Drawing.Size(36, 41);
            this.printButton.Text = "toolStripDropDownButton1";
            // 
            // chartButton
            // 
            this.chartButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.chartButton.Image = global::Azx.Properties.Resources.chaet;
            this.chartButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.chartButton.Name = "chartButton";
            this.chartButton.ShowDropDownArrow = false;
            this.chartButton.Size = new System.Drawing.Size(36, 41);
            this.chartButton.Text = "toolStripDropDownButton1";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(10, 38);
            this.toolStripStatusLabel2.Text = "|";
            // 
            // exitButton
            // 
            this.exitButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.exitButton.Image = global::Azx.Properties.Resources.close;
            this.exitButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exitButton.Name = "exitButton";
            this.exitButton.ShowDropDownArrow = false;
            this.exitButton.Size = new System.Drawing.Size(36, 41);
            this.exitButton.Text = "toolStripDropDownButton1";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(10, 38);
            this.toolStripStatusLabel3.Text = "|";
            // 
            // actionProgressBar
            // 
            this.actionProgressBar.Name = "actionProgressBar";
            this.actionProgressBar.Size = new System.Drawing.Size(200, 37);
            // 
            // Toolbar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Name = "Toolbar";
            this.Size = new System.Drawing.Size(620, 43);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripDropDownButton insertButton;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripDropDownButton updateButton;
        private System.Windows.Forms.ToolStripDropDownButton deleteButton;
        private System.Windows.Forms.ToolStripDropDownButton serachButton;
        private System.Windows.Forms.ToolStripDropDownButton printButton;
        private System.Windows.Forms.ToolStripDropDownButton chartButton;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripDropDownButton exitButton;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripProgressBar actionProgressBar;
    }
}
