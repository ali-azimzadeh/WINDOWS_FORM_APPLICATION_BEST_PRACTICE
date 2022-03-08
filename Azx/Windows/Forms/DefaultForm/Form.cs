using System;
using System.Collections.Generic;
using System.Text;

namespace Azx.Windows.Forms
{
    public partial class Form : System.Windows.Forms.Form
    {
    
        public Form()
        {
            InitializeComponent();
                    
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Form
            // 
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "Form";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }
    }
}
