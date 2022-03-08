namespace Azx.Windows.Forms
{
	partial class LookupData
	{
		private System.ComponentModel.IContainer components = null;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
				components.Dispose();

			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.btnLookup = new System.Windows.Forms.Button();
            this.txtText = new System.Windows.Forms.TextBox();
            this.txtValue = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnLookup
            // 
            this.btnLookup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLookup.Location = new System.Drawing.Point(0, 0);
            this.btnLookup.Name = "btnLookup";
            this.btnLookup.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnLookup.Size = new System.Drawing.Size(30, 20);
            this.btnLookup.TabIndex = 2;
            this.btnLookup.Text = "...";
            this.btnLookup.UseVisualStyleBackColor = true;
            this.btnLookup.Click += new System.EventHandler(this.btnLookup_Click);
            // 
            // txtText
            // 
            this.txtText.Location = new System.Drawing.Point(30, 0);
            this.txtText.Name = "txtText";
            this.txtText.ReadOnly = true;
            this.txtText.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtText.Size = new System.Drawing.Size(58, 21);
            this.txtText.TabIndex = 1;
            this.txtText.TabStop = false;
            this.txtText.TextChanged += new System.EventHandler(this.txtText_TextChanged);
            // 
            // txtValue
            // 
            this.txtValue.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.txtValue.Location = new System.Drawing.Point(88, 0);
            this.txtValue.Name = "txtValue";
            this.txtValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtValue.Size = new System.Drawing.Size(92, 21);
            this.txtValue.TabIndex = 0;
            this.txtValue.Enter += new System.EventHandler(this.txtValue_Enter);
            this.txtValue.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtValue_KeyDown);
            // 
            // LookupData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtValue);
            this.Controls.Add(this.txtText);
            this.Controls.Add(this.btnLookup);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Name = "LookupData";
            this.Size = new System.Drawing.Size(205, 23);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtValue_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnLookup;
		internal System.Windows.Forms.TextBox txtText;
		internal System.Windows.Forms.TextBox txtValue;
	}
}
