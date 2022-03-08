namespace Azx.Windows.Forms
{
    partial class PersianDateBox
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
            this.components = new System.ComponentModel.Container();
            this.mskTextBox = new System.Windows.Forms.MaskedTextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // mskTextBox
            // 
            this.mskTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mskTextBox.HideSelection = false;
            this.mskTextBox.Location = new System.Drawing.Point(0, 0);
            this.mskTextBox.Mask = "13##/##/##";
            this.mskTextBox.MaximumSize = new System.Drawing.Size(70, 20);
            this.mskTextBox.Name = "mskTextBox";
            this.mskTextBox.RejectInputOnFirstFailure = true;
            this.mskTextBox.Size = new System.Drawing.Size(68, 20);
            this.mskTextBox.TabIndex = 0;
            this.mskTextBox.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludePromptAndLiterals;
            this.mskTextBox.Enter += new System.EventHandler(this.mskTextBox_Enter);
            this.mskTextBox.Leave += new System.EventHandler(this.mskTextBox_Leave);
            this.mskTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.mskTextBox_Validating);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // PersianDateBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.mskTextBox);
            this.MaximumSize = new System.Drawing.Size(70, 20);
            this.Name = "PersianDateBox";
            this.Size = new System.Drawing.Size(68, 18);
            this.Load += new System.EventHandler(this.DateBox_Load);
            this.RightToLeftChanged += new System.EventHandler(this.DateBox_RightToLeftChanged);
            this.Enter += new System.EventHandler(this.DateBox_Enter);
            this.Resize += new System.EventHandler(this.DateBox_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.MaskedTextBox mskTextBox;
    }
}
