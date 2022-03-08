using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Azx.Windows.Forms
{
    public partial class PersianDateBox : UserControl
    {
        private bool dateRequired;
        private Color dateRequiredColor = Color.LightGoldenrodYellow;
        private string dateErrorMessage;
        private ErrorIconAlignment dateErrorIconAlignment;
        private string txtDate;
        private MaskFormat mskText;
        public PersianDateBox()
        {
            InitializeComponent();
        }

        public MaskFormat MaskTextFormat
        {
            get
            {
                return mskText;
            }
            set
            {
                mskText = value;
                this.mskTextBox.TextMaskFormat  = mskText;
            }
        }
        
        public string FormattedText
        {
            get
            {
                return txtDate;
            }
            set
            {
                txtDate = value;
                this.mskTextBox.Text = txtDate;
            }
        }

        public bool Required
        {
            get
            {
                return dateRequired;
            }
            set
            {
                dateRequired = value;
                if (!dateRequired)
                    RequiredColor = SystemColors.Window;
                else
                    RequiredColor = Color.LightGoldenrodYellow;
            }
        }
        public Color RequiredColor
        {
            get
            {
                return dateRequiredColor;
            }
            set
            {
                dateRequiredColor = value;
                this.mskTextBox.BackColor = dateRequiredColor;
            }
        }
        public string ErrorMessage
        {
            get
            {
                return dateErrorMessage;
            }
            set
            {
                dateErrorMessage = value;
            }
        }
        public ErrorIconAlignment ErrorIconAlignment
        {
            get
            {
                return dateErrorIconAlignment;
            }
            set
            {
                dateErrorIconAlignment = value;
            }
        }

        private void DateBox_RightToLeftChanged(object sender, EventArgs e)
        {
            this.RightToLeft = RightToLeft.No;
        }

        private void DateBox_Load(object sender, EventArgs e)
        {
            errorProvider1.SetError(this, string.Empty);
        }

        private void DateBox_Resize(object sender, EventArgs e)
        {
            this.Height = this.mskTextBox.Height + 2;
        }

        private void mskTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (dateRequired) //
            {
                if (!this.mskTextBox.MaskFull)
                {
                    //          dateErrorMessage = "ﬂ«—»— ê—«„Ì  «—ÌŒ Ê«—œ ‘œÂ ‰«ﬁ’ „Ì »«‘œ ·ÿ›« «’·«Õ ‰„«ÌÌœ";
                    this.mskTextBox.SelectionStart = 2;
                    this.mskTextBox.SelectionLength = 8;
                    e.Cancel = true;
                    this.errorProvider1.SetIconAlignment(this, dateErrorIconAlignment);
                    this.errorProvider1.SetError(this, dateErrorMessage);
                }
                else
                {
                    e.Cancel = !IsValidDate(this.mskTextBox.Text);
                    if (e.Cancel)
                    {
                        this.errorProvider1.SetIconAlignment(this, dateErrorIconAlignment);
                        this.errorProvider1.SetError(this, dateErrorMessage);
                    }
                    else
                        this.errorProvider1.Dispose();
                }
            }
            else
            {
                if (!this.mskTextBox.MaskFull)
                {
                    //          dateErrorMessage = "ﬂ«—»— ê—«„Ì  «—ÌŒ Ê«—œ ‘œÂ ‰«ﬁ’ „Ì »«‘œ ·ÿ›« «’·«Õ ‰„«ÌÌœ";
                    if ((mskText == MaskFormat.IncludeLiterals && this.mskTextBox.Text == "14  /  /") || (mskText == MaskFormat.ExcludePromptAndLiterals && this.mskTextBox.Text == "") || (mskText == MaskFormat.IncludePrompt && this.mskTextBox.Text == "______") || (mskText == MaskFormat.IncludePromptAndLiterals && this.mskTextBox.Text == "14__/__/__")) //"13__/__/__") //)
//                        if ((mskText == MaskFormat.IncludeLiterals && this.mskTextBox.Text == "13  /  /") || (mskText == MaskFormat.ExcludePromptAndLiterals && this.mskTextBox.Text == "") || (mskText == MaskFormat.IncludePrompt && this.mskTextBox.Text == "______") || (mskText == MaskFormat.IncludePromptAndLiterals && this.mskTextBox.Text == "13__/__/__")) //"13__/__/__") //)
                    {
                        e.Cancel = false;
                        this.errorProvider1.Dispose();
                    }
                    else
                    {
                        this.mskTextBox.SelectionStart = 2;
                        this.mskTextBox.SelectionLength = 8;
                        e.Cancel = true;
                        this.errorProvider1.SetIconAlignment(this, dateErrorIconAlignment);
                        this.errorProvider1.SetError(this, dateErrorMessage);
                    }
                }
                else
                {
                    e.Cancel = !IsValidDate(this.mskTextBox.Text);
                    if (e.Cancel)
                    {
                        this.errorProvider1.SetIconAlignment(this, dateErrorIconAlignment);
                        this.errorProvider1.SetError(this, dateErrorMessage);
                    }
                    else
                        this.errorProvider1.Dispose();
                }

            }
        }
        public  bool IsValidDate(object sender)
        {
            if (Convert.ToInt32(this.mskTextBox.Text.Substring(5, 2)) > 12 || Convert.ToInt32(this.mskTextBox.Text.Substring(5, 2)) == 0)
                {
                    //    dateErrorMessage = "ﬂ«—»— ê—«„Ì ⁄œœ Ê«—œ ‘œÂ œ— ﬁ”„  „«Â »“—ê — «“ 12 „Ì »«‘œ ·ÿ›« «’·«Õ ‰„«ÌÌœ";
                    this.mskTextBox.SelectionStart = 5;
                    this.mskTextBox.SelectionLength = 2;
                    return false;
                }
            else
                {
                    if ((Convert.ToInt32(this.mskTextBox.Text.Substring(5, 2)) < 7 && Convert.ToInt32(this.mskTextBox.Text.Substring(8, 2)) > 31 || Convert.ToInt32(this.mskTextBox.Text.Substring(8, 2)) == 0))
                    {
                        //    dateErrorMessage = "ﬂ«—»— ê—«„Ì ⁄œœ Ê«—œ ‘œÂ œ— ﬁ”„  „«Â »“—ê — «“ 12 „Ì »«‘œ ·ÿ›« «’·«Õ ‰„«ÌÌœ";
                        this.mskTextBox.SelectionStart = 8;
                        this.mskTextBox.SelectionLength = 2;
                        return false;
                    }
                    else
                    {
                        if ((Convert.ToInt32(this.mskTextBox.Text.Substring(5, 2)) > 6 && Convert.ToInt32(this.mskTextBox.Text.Substring(8, 2)) > 30 || Convert.ToInt32(this.mskTextBox.Text.Substring(5, 2)) == 12 && Convert.ToInt32(this.mskTextBox.Text.Substring(8, 2)) == 30) || Convert.ToInt32(this.mskTextBox.Text.Substring(8, 2)) == 0)
                        {
                            //    dateErrorMessage = "ﬂ«—»— ê—«„Ì ⁄œœ Ê«—œ ‘œÂ œ— ﬁ”„  „«Â »“—ê — «“ 12 „Ì »«‘œ ·ÿ›« «’·«Õ ‰„«ÌÌœ";
                            this.mskTextBox.SelectionStart = 8;
                            this.mskTextBox.SelectionLength = 2;
                            return false;
                        }
                    }
                }
            txtDate = this.mskTextBox.Text;
            return true;
        }


        private void DateBox_Enter(object sender, EventArgs e)
        {
            if (!this.mskTextBox.MaskFull && dateRequired)
            {
                this.mskTextBox.Focus();
                this.mskTextBox.SelectionStart = 2;
                this.mskTextBox.SelectionLength = 8;
            }
        }

        private void mskTextBox_Enter(object sender, EventArgs e)
        {
          //  this.mskTextBox.SelectionStart = 2;
        //    this.mskTextBox.SelectionLength = 8;
        }

        private void mskTextBox_Leave(object sender, EventArgs e)
        {
            this.mskTextBox.DeselectAll();
        }

    }
}
