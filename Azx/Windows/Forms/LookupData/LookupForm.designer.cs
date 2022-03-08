namespace Azx.Windows.Forms
{
	partial class LookupForm
	{
		private System.ComponentModel.IContainer components = null;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
				components.Dispose();

			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.lvwData = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // lvwData
            // 
            this.lvwData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwData.Location = new System.Drawing.Point(0, 0);
            this.lvwData.Name = "lvwData";
            this.lvwData.RightToLeftLayout = true;
            this.lvwData.Size = new System.Drawing.Size(387, 210);
            this.lvwData.TabIndex = 0;
            this.lvwData.UseCompatibleStateImageBehavior = false;
            this.lvwData.DoubleClick += new System.EventHandler(this.lvwData_DoubleClick);
            this.lvwData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lvwData_KeyDown);
            this.lvwData.Click += new System.EventHandler(this.lvwData_Click);
            // 
            // LookupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 210);
            this.ControlBox = false;
            this.Controls.Add(this.lvwData);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LookupForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "گزينه ای را انتخاب نماييد";
            this.Activated += new System.EventHandler(this.LookupForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LookupForm_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LookupForm_KeyDown);
            this.Load += new System.EventHandler(this.LookupForm_Load);
            this.ResumeLayout(false);

		}

		#endregion

        public System.Windows.Forms.ListView lvwData;






    }
}