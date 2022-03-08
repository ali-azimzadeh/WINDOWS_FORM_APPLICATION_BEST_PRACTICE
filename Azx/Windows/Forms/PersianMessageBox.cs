// Copyright 2006    SIMORGH (Co)     Tel : 44669942
// AUTHOR : Ali Azimzadeh - <alipass_2000@yahoo.com>  TEL : 09329070319
// LAST UPDATE : 2009/11/24  = 1388/09/04   TIME : 13:00    
// CONTROL NAME : Persian MessageBox        VERSION : 2.0.0    NAMESPACE = AH.Windows.Forms
//============================================================================
// This Source File(s) May be Redistributed, Altered and Custimized
// By Any Means PROVIDING the Authors Name and all Copyright
// notices remain intact.
// THIS SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED.
// USE IT AT YOUR OWN RISK. THE AUTHOR ACCEPTS NO
// LIABILITY FOR ANY DATA DAMAGE/LOSS THAT THIS PRODUCT MAY CAUSE.
//-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Azx.Windows.Forms
{
    public sealed class PersianMessageBox 
    {
        #region Enums
        /// <summary>
        /// جهت تعیین دکمه یا دکمه های مورد نظر بکار می رود
        /// </summary>
        public enum PersianMessageBoxButton
        {
            قبول = 0,
            تاييدانصراف = 1,
            خيربلي = 2,
            بلىخيرانصراف = 3,
            سعىمجددانصراف = 4,
        }
        /// <summary>
        /// جهت تعیین آیکون مورد نظر بکار می رود
        /// </summary>
        public enum PersianMessageBoxIcon
        {
            خالي = 0,
            خطا = 1,
            اخطار = 2,
            اطلاع = 3,
            علامتسوال = 4,
            ايست = 5,
            ورودممنوع = 6,

        }
        /// <summary>
        /// تعیین نتیجه دکمه زده شده بکار می رود
        /// </summary>
        public enum aPersianDialogResult
        {
            قبول = 0,
            بلي = 1,
            خير = 2,
            تاييد = 3,
            سعي = 4,
            انصراف = 5,
                
        }
        /// <summary>
        /// جهت تعیین دکمه پیش فرض  بکار می رود
        /// </summary>
        public enum aPersianDefaultButoon
        {
            دكمه1 = 0,
            دكمه2 = 1,
            دكمه3 = 2,
        }
        #endregion
        #region PersianMessageBox Variables
        private static Button btnOk = new Button();
        private static Button btnCancel = new Button();
        private static Button btnRetry = new Button();
        private static Button btnIgnore = new Button();
        private static Button btnAbort = new Button();
        private static Form frmMessageBox = new Form();
//        protected static GradientForm frmMessageBox = new GradientForm();
        private static Label lblMessage = new Label();
      //  protected static Panel pnlMessage = new Panel();
        private static Timer timCurrent = new Timer();
        private static PictureBox picMessageBoxIcon = new PictureBox();
        private static aPersianDialogResult dr;
        private static Font fntPersianMessageBox;
        private static Color colMessageForeColor, colMesssageBoxBackColor, colMessageBoxButtonForeColor, colMessageBoxButtonBackColor, colMessageBackColor = System.Drawing.Color.Transparent;
        private static Image imgMessageBoxBackGround, imgMessageBoxButtonBackGround;

        private static Panel pnlMessage = new Panel();
        private static Panel pnlIcon = new Panel();
        private static Panel pnlButtons = new Panel();
        #endregion
        #region PersianMessageBox Constractor
        private PersianMessageBox()
        {
            MessageFont = new Font("Tahoma",8);
            picMessageBoxIcon.BackColor = Color.Transparent;
            MessageBackColor = Color.Transparent;
//            InitializeComponent();
        }

        private static void InitializeComponent()
        {
/*           components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PersianMessageBox));
            this.imgList = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();*/
            // 
            // imgList
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PersianMessageBox));
            // 

            pnlButtons.BorderStyle = BorderStyle.None;
            pnlIcon.BorderStyle = BorderStyle.None;
            pnlMessage.BorderStyle = BorderStyle.None;
            btnOk.Font = btnAbort.Font = btnCancel.Font = btnIgnore.Font = btnRetry.Font =
                new Font("Tahoma", 9f, FontStyle.Regular);
            frmMessageBox.Opacity = 0;
            /*           imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
                        imgList.TransparentColor = System.Drawing.Color.Transparent;
                        imgList.Images.SetKeyName(0, "Error.ICO");
                        imgList.Images.SetKeyName(1, "Forbidden.ICO");
                        imgList.Images.SetKeyName(2, "Information.ICO");
                        imgList.Images.SetKeyName(3, "Question.ICO");
                        imgList.Images.SetKeyName(4, "Stop.ICO");
                        imgList.Images.SetKeyName(5, "Warning.ICO");*/
        }
        #endregion
        #region PersianMessageBox Properties
        /// <summary>
        /// این ویژگی جهت تعیین نوع فونت  متن پیام می باشد
        /// </summary>
        [DefaultValue(typeof(Font),"Arial,10")]
        public static Font MessageFont
        {
            get 
            {
                return fntPersianMessageBox;
            }
            set
            {
                fntPersianMessageBox = value;
            }
        }
        /// <summary>
        /// این ویژگی جهت تعیین رنگ قلم پیام می باشد
        /// </summary>
        public static Color MessageForeColor
        {
            get
            {
                return colMessageForeColor;
            }
            set
            {
                colMessageForeColor = value;
            }
        }
        /// <summary>
        /// این ویژگی جهت تعیین رنگ زمینه پیام می باشد
        /// </summary>
        [DefaultValue(typeof(Color),"Transparent")]
        public static Color MessageBackColor
        {
            get
            {
                return colMessageBackColor;
            }
            set
            {
                colMessageBackColor = value;
            }
        }
        /// <summary>
        /// این ویژگی جهت تعیین رنگ زمینه فرم پیام می باشد
        /// </summary>
        public static Color MessageBoxBackColor
        {
            get
            {
                return colMesssageBoxBackColor;
            }
            set
            {
                colMesssageBoxBackColor = value;
            }
        }
        /// <summary>
        /// این ویژگی جهت تعیین تصویر مورد نظر بروی زمینه فرم پیام می باشد
        /// </summary>
        public static Image MessageBoxBackGround
        {
            get
            {
                return imgMessageBoxBackGround;
            }
            set
            {
                imgMessageBoxBackGround = value;
                imgMessageBoxButtonBackGround = imgMessageBoxBackGround;
            }
        }
        /// <summary>
        /// این ویژگی جهت تعیین تصویر بروی دکمه های فرم می باشد
        /// </summary>
        public static Image MessageBoxButtonBackGround
        {
            get
            {
                return imgMessageBoxButtonBackGround;
            }
            set
            {
                imgMessageBoxButtonBackGround = value;
            }
        }
        /// <summary>
        /// این ویژگی جهت تعیین رنگ زمینه دکمه های روی فرم می باشد
        /// </summary>
        public static Color MessageBoxButtonBackColor
        {
            get
            {
                return colMessageBoxButtonBackColor;
            }
            set
            {
                colMessageBoxButtonBackColor = value;
             //   foreach (Button btnTest in frmMessageBox.Controls)
               //     MessageBox.Show(btnTest.ToString());
            }
        }
        /// <summary>
        /// این ویژگی جهت تعیین رنگ قلم دکمه های روی فرم می باشد
        /// </summary>
        public static Color MessageBoxButtonForeColor
        {
            get
            {
                return colMessageBoxButtonForeColor;
            }
            set
            {
                colMessageBoxButtonForeColor = value;
            }
        }
        #endregion
        #region PersionMessageBox Metods
        /// <summary>
        /// این تابع مربوط به ساخت دکمه قبول و قراردادن آن بروی فرم پیام می باشد
        /// </summary>
        private static void InitializeOkButton()
        {
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Location = new Point(Math.Abs((frmMessageBox.Width -btnOk.Width)/2 ), (pnlButtons.Height-btnOk.Height) / 2);
          //  btnOk.Anchor = AnchorStyles.Bottom;
            btnOk.Cursor = Cursors.Hand;
            btnOk.Text = "قبول";
            btnOk.BackColor = colMessageBoxButtonBackColor;
            btnOk.ForeColor = colMessageBoxButtonForeColor;
            btnOk.BackgroundImage = imgMessageBoxButtonBackGround;
            
        }
        /// <summary>
        /// این تابع مربوط به ساخت دکمه تایید و انصراف و قراردادن آنها بروی فرم می باشد
        /// </summary>
        private static void InitializeOkCancelButton()
        {
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(Math.Abs((frmMessageBox.Width - (btnOk.Width+btnCancel.Width+6)) / 2), (pnlButtons.Height - btnOk.Height) / 2);
           // btnCancel.Anchor = AnchorStyles.Bottom;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.BackColor = colMessageBoxButtonBackColor;
            btnCancel.ForeColor = colMessageBoxButtonForeColor;
            btnCancel.BackgroundImage = imgMessageBoxButtonBackGround;
            btnCancel.Text = "انصراف";
            btnOk.DialogResult = DialogResult.OK;
            btnOk.Location = new Point(btnCancel.Location.X + btnCancel.Width + 6, btnCancel.Location.Y);
            //btnOk.Anchor = AnchorStyles.Bottom;
            btnOk.Cursor = Cursors.Hand;
            btnOk.BackColor = colMessageBoxButtonBackColor;
            btnOk.ForeColor = colMessageBoxButtonForeColor;
            btnOk.BackgroundImage = imgMessageBoxButtonBackGround;
            btnOk.Text = "تاييد";
        }
        /// <summary>
        /// این تابع مربوط به ساخت دکمه بلی و خیر  و قراردادن آنها بروی فرم می باشد
        /// </summary>
        private static void InitializeYesNoButton()
        {
            btnCancel.DialogResult = DialogResult.No;
            btnCancel.Location = new Point(Math.Abs((frmMessageBox.Width - (btnOk.Width + btnCancel.Width + 6)) / 2), (pnlButtons.Height - btnOk.Height) / 2);
            btnCancel.Anchor = AnchorStyles.Bottom;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.BackColor = colMessageBoxButtonBackColor;
            btnCancel.ForeColor = colMessageBoxButtonForeColor;
            btnCancel.BackgroundImage = imgMessageBoxButtonBackGround;
            btnCancel.Text = "خير";
            btnOk.DialogResult = DialogResult.Yes;
            btnOk.Location = new Point(btnCancel.Location.X + btnCancel.Width + 6, btnCancel.Location.Y);
            btnOk.Anchor = AnchorStyles.Bottom;
            btnOk.Cursor = Cursors.Hand;
            btnOk.BackColor = colMessageBoxButtonBackColor;
            btnOk.ForeColor = colMessageBoxButtonForeColor;
            btnOk.BackgroundImage = imgMessageBoxButtonBackGround;
            btnOk.Text = "بلي";
        }
        /// <summary>
        /// این تابع مربوط به ساخت دکمه بلی ، خیر و انصراف و قراردادن آنها بروی فرم می باشد
        /// </summary>
        private static void InitializeYesNoCancelButton()
        {
            btnRetry.DialogResult = DialogResult.Cancel;
            btnRetry.Location = new Point(Math.Abs((frmMessageBox.Width - (btnOk.Width + btnCancel.Width +btnRetry.Width+ 12)) / 2), (pnlButtons.Height - btnOk.Height) / 2);
            //btnRetry.Location = new Point(btnCancel.Location.X + btnOk.Width + 3, AH.Windows.Forms.PersianMessageBox.frmMessageBox.Height / 2);
            btnRetry.Anchor = AnchorStyles.Bottom;
            btnRetry.Cursor = Cursors.Hand;
            btnRetry.BackColor = colMessageBoxButtonBackColor;
            btnRetry.ForeColor = colMessageBoxButtonForeColor;
            btnRetry.BackgroundImage = imgMessageBoxButtonBackGround;
            btnRetry.Text = "انصراف";
            //                             MessageBox.Show(Math.Abs(this.Width-860).ToString(),"",MessageBoxButtons.AbortRetryIgnor,MessageBoxIcon.Hand );

            btnCancel.DialogResult = DialogResult.No;
            btnCancel.Location = new Point(btnRetry.Location.X + btnRetry.Width + 6, btnRetry.Location.Y);
            //btnCancel.Location = new Point(Math.Abs(AH.Windows.Forms.PersianMessageBox.frmMessageBox.Width / 2 - 70), AH.Windows.Forms.PersianMessageBox.frmMessageBox.Height / 2);
            btnCancel.Anchor = AnchorStyles.Bottom;
            btnCancel.Cursor = Cursors.Hand;
            btnCancel.BackColor = colMessageBoxButtonBackColor;
            btnCancel.ForeColor = colMessageBoxButtonForeColor;
            btnCancel.BackgroundImage = imgMessageBoxButtonBackGround;
            btnCancel.Text = "خير";

            btnOk.DialogResult = DialogResult.Yes;
            btnOk.Location = new Point(btnCancel.Location.X + btnCancel.Width + 6, btnCancel.Location.Y);
//            btnOk.Location = new Point(btnCancel.Location.X + btnCancel.Width + 3, AH.Windows.Forms.PersianMessageBox.frmMessageBox.Height / 2);
            btnOk.Anchor = AnchorStyles.Bottom;
            btnOk.Cursor = Cursors.Hand;
            btnOk.BackColor = colMessageBoxButtonBackColor;
            btnOk.ForeColor = colMessageBoxButtonForeColor;
            btnOk.BackgroundImage = imgMessageBoxButtonBackGround;
            btnOk.Text = "بلي";

        }
        /// <summary>
        /// این تابع مربوط به ساخت دکمه رد ، سعی مجدد و بدون نتیجه و قراردادن آنها بروی فرم می باشد
        /// </summary>
        private static void InitializeAbortRetryIgnoreButton()
        {
            btnIgnore.DialogResult = DialogResult.Ignore;
            btnIgnore.Location = new Point(Math.Abs((frmMessageBox.Width - (btnIgnore.Width + btnRetry.Width + btnAbort.Width + 12)) / 2), (pnlButtons.Height - btnIgnore.Height) / 2);
            //btnRetry.Location = new Point(btnCancel.Location.X + btnOk.Width + 3, AH.Windows.Forms.PersianMessageBox.frmMessageBox.Height / 2);
//            btnIgnore.Anchor = AnchorStyles.Bottom;
            btnIgnore.Cursor = Cursors.Hand;
            btnIgnore.BackColor = colMessageBoxButtonBackColor;
            btnIgnore.ForeColor = colMessageBoxButtonForeColor;
            btnIgnore.BackgroundImage = imgMessageBoxButtonBackGround;
            btnIgnore.Text = "رد کردن";
            //                             MessageBox.Show(Math.Abs(this.Width-860).ToString(),"",MessageBoxButtons.AbortRetryIgnor,MessageBoxIcon.Hand );

            btnRetry.DialogResult = DialogResult.Retry;
            btnRetry.Location = new Point(btnIgnore.Location.X + btnIgnore.Width + 6, btnIgnore.Location.Y);
            //btnCancel.Location = new Point(Math.Abs(AH.Windows.Forms.PersianMessageBox.frmMessageBox.Width / 2 - 70), AH.Windows.Forms.PersianMessageBox.frmMessageBox.Height / 2);
  //          btnRetry.Anchor = AnchorStyles.Bottom;
            btnRetry.Cursor = Cursors.Hand;
            btnRetry.BackColor = colMessageBoxButtonBackColor;
            btnRetry.ForeColor = colMessageBoxButtonForeColor;
            btnRetry.BackgroundImage = imgMessageBoxButtonBackGround;
            btnRetry.Text = "سعي مجدد";

            btnAbort.DialogResult = DialogResult.Abort;
            btnAbort.Location = new Point(btnRetry.Location.X + btnRetry.Width + 6, btnRetry.Location.Y);
            //            btnOk.Location = new Point(btnCancel.Location.X + btnCancel.Width + 3, AH.Windows.Forms.PersianMessageBox.frmMessageBox.Height / 2);
//            btnAbort.Anchor = AnchorStyles.Bottom;
            btnAbort.Cursor = Cursors.Hand;
            btnAbort.BackColor = colMessageBoxButtonBackColor;
            btnAbort.ForeColor = colMessageBoxButtonForeColor;
            btnAbort.BackgroundImage = imgMessageBoxButtonBackGround;
            btnAbort.Text = "بدون نتیجه";
        }
        /// <summary>
        /// این تابع متن مورد نظر را نمایش داده که با پنج پارامتر ورودی تعریف شده است که مربوط به متن پیام،هدر آن و دکمه مورد نظر ، تعیین دکمه پیش فرض و نوع آیکونی که می خواهید روی آن می باشد
        /// این تابع کاملترین دریافت پارامترهای ورودی می باشد
        /// </summary>
        /// <param name="strMessageText">متن اصلی که می خواهید درون فرم پیام نمایش داده شود</param>
        /// <param name="strMessageHeader">متنی که می خواهید در بالای فرم پیام نوشته شود</param>
        /// <param name="btn">دکمه یا دکمه هایی که می خواهید بروی فرم دیده شود</param>
        /// <param name="btnDefault">دکمه ای را که می خواهید بصورت پیش فرض  قرار گیرد تعیین می کند</param>
        /// <param name="icoPersianMessageBoxIcon">آیکون مورد نظر که می خواهید بروی فرم ظاهر شود را مشخص می کند</param>
        /// <returns></returns>
        public static aPersianDialogResult Show(string strMessageText, string strMessageHeader, PersianMessageBoxButton btn, aPersianDefaultButoon btnDefault, PersianMessageBoxIcon icoPersianMessageBoxIcon)
        {
      //      frmMessageBox.Controls.Add(lblMessage);
            //frmMessageBox.Controls.Add(picMessageBoIcon);
            frmMessageBox.Controls.Clear();
            InitializeComponent();
            picMessageBoxIcon.BackColor = Color.Transparent;
            switch (icoPersianMessageBoxIcon)
            {
                case PersianMessageBoxIcon.اخطار:
                    {
                        picMessageBoxIcon.Image =
                             global::Azx.Properties.Resources.warning;// global::AH_PersianMessageBox.Properties.Resources.Error; // imgList.Images[5]; //.FromFile(@"E:\MyProject\TextBox\TextBox\Icons\Warning.ICO");
                        break;
                    }
                case PersianMessageBoxIcon.اطلاع :
                    {
                        picMessageBoxIcon.Image =
                             global::Azx.Properties.Resources.info;
                        break;
                    }
                case PersianMessageBoxIcon.ايست :
                    {
                        picMessageBoxIcon.Image =
                            global::Azx.Properties.Resources.stop;
                        break;
                    }
                case PersianMessageBoxIcon.خالي :
                    {
                        //picMessageBoxIcon.Image = Image.FromFile(@"E:\MyProject\TextBox\TextBox\Icons\Information.ICO");
                        break;
                    }
                case PersianMessageBoxIcon.خطا :
                    {
                        picMessageBoxIcon.Image = System.Drawing.SystemIcons.Error.ToBitmap(); 
                        break;
                    }
                case PersianMessageBoxIcon.علامتسوال :
                    {
                        picMessageBoxIcon.Image = System.Drawing.SystemIcons.Question.ToBitmap(); 
                        break;
                    }
                case PersianMessageBoxIcon.ورودممنوع :
                    {
                        picMessageBoxIcon.Image =
                            global::Azx.Properties.Resources.forbiden;
                        break;
                        
                    }
            }
            lblMessage.AutoSize = false;
            frmMessageBox.MinimizeBox = false;
            frmMessageBox.ShowIcon = false;
            frmMessageBox.ShowInTaskbar = false;
            pnlMessage.Controls.Add(lblMessage);
            pnlIcon.Controls.Add(picMessageBoxIcon);


            pnlMessage.Dock = DockStyle.Fill;
            pnlMessage.Size = new Size(350, 65);
            pnlIcon.Dock = DockStyle.Left;
            pnlIcon.Size = new Size(45, 65);
            pnlButtons.Dock = DockStyle.Bottom;
            pnlButtons.Size = new Size(450, 45);

            frmMessageBox.Controls.Add(pnlMessage);
            frmMessageBox.Controls.Add(pnlIcon);
            frmMessageBox.Controls.Add(pnlButtons);
            switch (btn)
            {
                case PersianMessageBoxButton.قبول :
                    {
/*                        lblMessage.Parent = frmMessageBox;
                        lblMessage.Text = strMessageText;
                        lblMessage.AutoSize = true;
                        lblMessage.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                        lblMessage.Location = new Point(0, 0);
                        lblMessage.TextAlign = ContentAlignment.MiddleCenter;
                        lblMessage.BackColor = Color.Azure;
                        lblMessage.RightToLeft = RightToLeft.Yes;
                        //  lblMessage.Size = new Size(lblMessage.Text.Length , 15);
                        // lblMessage.Dock = DockStyle.Fill;

                        //pnlMessage.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
                        //pnlMessage.AutoSize = true; //
                        //pnlMessage.Size = new Size(frmMessageBox.Size.Width, lblMessage.Size.Height);
                        //pnlMessage.Location = new Point(AH.Windows.Forms.PersianMessageBox.frmMessageBox.Location.X, AH.Windows.Forms.PersianMessageBox.frmMessageBox.Location.Y + 10);
                        //pnlMessage.BackColor = Color.AliceBlue;
                        frmMessageBox.MaximizeBox = false;
                        frmMessageBox.MinimizeBox = false;
                        frmMessageBox.RightToLeft = RightToLeft.Yes;
//                        frmMessageBox.AutoSize = true;
//                        MessageBox.Show(lblMessage.Size.Width.ToString());
                        frmMessageBox.Size = new Size(lblMessage.Size.Width, 100);
                        frmMessageBox.StartPosition = FormStartPosition.CenterScreen;
                        frmMessageBox.FormBorderStyle = FormBorderStyle.FixedDialog;
                        frmMessageBox.Text = strMessageHeader;*/

/*                                                lblMessage.Text = strMessageText;
                                                frmMessageBox.Text = strMessageHeader;
                                                AddControls();
                                                frmMessageBox.Controls.Add(lblMessage);
                                           //     pnlMessage.Controls.Add(lblMessage);
                                                InitializeOkButton();
                                                frmMessageBox.Controls.Add(btnOk);
                                    //            MessageBox.Show(frmMessageBox.Size.Width.ToString());*/

                        //frmMessageBox.Controls.Add(lblMessage);
                        //frmMessageBox.Controls.Add(picMessageBoxIcon);
                        //frmMessageBox.Controls.Add(btnOk);
                        pnlButtons.Controls.Add(btnOk);

                        AddControls(strMessageText, strMessageHeader);
                        InitializeOkButton();

                        frmMessageBox.ShowDialog();
                        dr = aPersianDialogResult.قبول; //.OK;
                        break;
                    }
                case PersianMessageBoxButton.تاييدانصراف :
                    {
/*                        AddControls();
                        lblMessage.Text = strMessageText;
                        frmMessageBox.Text = strMessageHeader;
                        frmMessageBox.Controls.Add(pnlMessage);
                        pnlMessage.Controls.Add(lblMessage);
                        InitializeOkCancelButton();
                        frmMessageBox.Controls.Add(btnOk);
                        frmMessageBox.Controls.Add(btnCancel);
                        frmMessageBox.ShowDialog();
                        frmMessageBox.ShowDialog();*/

                        if (btnDefault == aPersianDefaultButoon.دكمه1)
                        {
                            btnCancel.TabIndex = 1;
                            btnOk.TabIndex = 0;
                        }
                        else
                        {
                            btnCancel.TabIndex = 0;
                            btnOk.TabIndex = 1;
                        }
//                        frmMessageBox.Controls.Add(lblMessage);
//                        frmMessageBox.Controls.Add(btnOk);
  //                      frmMessageBox.Controls.Add(btnCancel);
                        pnlButtons.Controls.Add(btnOk);
                        pnlButtons.Controls.Add(btnCancel);

                        AddControls(strMessageText, strMessageHeader);
                        InitializeOkCancelButton();
                        if (frmMessageBox.ShowDialog() == DialogResult.OK)
                            dr = aPersianDialogResult.تاييد;
                        else
                            dr = aPersianDialogResult.انصراف;
                        break;
                    }
                case PersianMessageBoxButton.خيربلي :
                    {

                        if (btnDefault == aPersianDefaultButoon.دكمه1)
                        {
                            btnCancel.TabIndex = 1;
                            btnOk.TabIndex = 0;
                        }
                        else
                        {
                            btnCancel.TabIndex = 0;
                            btnOk.TabIndex = 1;
                        }
  //                      frmMessageBox.Controls.Add(lblMessage);
//                        frmMessageBox.Controls.Add(btnOk);
  //                      frmMessageBox.Controls.Add(btnCancel);
                        pnlButtons.Controls.Add(btnOk);
                        pnlButtons.Controls.Add(btnCancel);
                        AddControls(strMessageText, strMessageHeader);
                        InitializeYesNoButton();
                        if (frmMessageBox.ShowDialog() == DialogResult.Yes)
                            dr = aPersianDialogResult.بلي ;
                        else
                            dr = aPersianDialogResult.خير ;
                        break;
                    }
                case PersianMessageBoxButton.بلىخيرانصراف :
                    {

                        if (btnDefault == aPersianDefaultButoon.دكمه1)
                        {
                            btnCancel.TabIndex = 1;
                            btnOk.TabIndex = 0;
                            btnRetry.TabIndex = 2;
                        }
                        else
                        {
                            if (btnDefault == aPersianDefaultButoon.دكمه2)
                            {
                                btnCancel.TabIndex = 0;
                                btnOk.TabIndex = 1;
                                btnRetry.TabIndex = 2;
                            }
                            else
                            {
                                btnCancel.TabIndex = 2;
                                btnOk.TabIndex = 1;
                                btnRetry.TabIndex = 0;
                            }
                        }
    //                    frmMessageBox.Controls.Add(lblMessage);
                        //frmMessageBox.Controls.Add(btnOk);
                        //frmMessageBox.Controls.Add(btnCancel);
                        //frmMessageBox.Controls.Add(btnRetry);
                        pnlButtons.Controls.Add(btnOk);
                        pnlButtons.Controls.Add(btnCancel);
                        pnlButtons.Controls.Add(btnRetry);
                        AddControls(strMessageText, strMessageHeader);
                        InitializeYesNoCancelButton();
                        switch (frmMessageBox.ShowDialog())
                        {
                            case DialogResult.Yes:
                                {
                                    dr = aPersianDialogResult.بلي;
                                    break;
                                }
                            case DialogResult.No:
                                {
                                    dr = aPersianDialogResult.خير;
                                    break;
                                }
                            case DialogResult.Cancel:
                                {
                                    dr = aPersianDialogResult.انصراف;
                                    break;
                                }
                        }
//                            for (int i = 0; i <= frmMessageBox.Controls.Count - 1; i++)
  //                              MessageBox.Show((frmMessageBox.Controls[i]).ToString());
                        break;
                    }
                case PersianMessageBoxButton.سعىمجددانصراف :
                    {

                        if (btnDefault == aPersianDefaultButoon.دكمه1)
                        {
                            btnRetry.TabIndex = 1;
                            btnAbort.TabIndex = 0;
                            btnIgnore.TabIndex = 2;
                        }
                        else
                        {
                            if (btnDefault == aPersianDefaultButoon.دكمه2)
                            {
                                btnRetry.TabIndex = 0;
                                btnAbort.TabIndex = 1;
                                btnIgnore.TabIndex = 2;
                            }
                            else
                            {
                                btnRetry.TabIndex = 2;
                                btnAbort.TabIndex = 1;
                                btnIgnore.TabIndex = 0;
                            }
                        }
      //                  frmMessageBox.Controls.Add(lblMessage);
                        //frmMessageBox.Controls.Add(btnAbort);
                        //frmMessageBox.Controls.Add(btnRetry);
                        //frmMessageBox.Controls.Add(btnIgnore);
                        pnlButtons.Controls.Add(btnAbort);
                        pnlButtons.Controls.Add(btnRetry);
                        pnlButtons.Controls.Add(btnIgnore);
                        AddControls(strMessageText, strMessageHeader);
                        InitializeAbortRetryIgnoreButton();
                        switch (frmMessageBox.ShowDialog())
                        {
                            case DialogResult.Retry :
                                {
                                    dr = aPersianDialogResult.سعي;
                                    break;
                                }
/*                            case DialogResult.No:
                                {
                                    dr = aPersianDialogResult.خير;
                                    break;
                                }
                            case DialogResult.Cancel:
                                {
                                    dr = aPersianDialogResult.انصراف;
                                    break;
                                }*/
                        }
                        break;
                    }
            }
            return dr;
        }
        /// <summary>
        /// این تابع با چهار پارامتر ورودی تعریف شده است که مربوط به متن پیام،هدر آن و دکمه مورد نظر و دکمه پیش فرض  می باشد
        /// </summary>
        /// <param name="strMessageText">متن اصلی که می خواهید درون فرم پیام نمایش داده شود</param>
        /// <param name="strMessageHeader">متنی که می خواهید در بالای فرم پیام نوشته شود</param>
        /// <param name="btn">دکمه یا دکمه هایی که می خواهید بروی فرم دیده شود</param>
        /// <param name="Defaultbtn">دکمه ای را که می خواهید بصورت پیش فرض  قرار گیرد تعیین می کند</param>
        /// <returns></returns>
        public static aPersianDialogResult Show(string strMessageText, string strMessageHeader, PersianMessageBoxButton btn,PersianMessageBox.aPersianDefaultButoon Defaultbtn)
        {
            dr = Show(strMessageText, strMessageHeader, btn, Defaultbtn, PersianMessageBoxIcon.خالي);
            return dr;
        }
        /// <summary>
        /// این تابع با سه پارامتر ورودی تعریف شده است که مربوط به متن پیام،هدر آن و دکمه مورد نظر روی آن می باشد
        /// </summary>
        /// <param name="strMessageText">متن اصلی که می خواهید درون فرم پیام نمایش داده شود</param>
        /// <param name="strMessageHeader">متنی که می خواهید در بالای فرم پیام نوشته شود</param>
        /// <param name="btn">دکمه یا دکمه هایی که می خواهید بروی فرم دیده شود</param>
        /// <returns></returns>
        public static aPersianDialogResult Show(string strMessageText, string strMessageHeader, PersianMessageBoxButton btn)
        {
            dr = Show(strMessageText, strMessageHeader, btn, aPersianDefaultButoon.دكمه1, PersianMessageBoxIcon.خالي);
            return dr;
        }
        /// <summary>
        /// این تابع با دو پارامتر ورودی تعریف شده است که مربوط به متن پیام،هدر آن می باشد
        /// </summary>
        /// <param name="strMessageText">متن اصلی که می خواهید درون فرم پیام نمایش داده شود</param>
        /// <param name="strMessageHeader">متنی که می خواهید در بالای فرم پیام نوشته شود</param>
        /// <returns></returns>
        public static aPersianDialogResult Show(string strMessageText, string strMessageHeader)
        {
            dr = Show(strMessageText, strMessageHeader, PersianMessageBoxButton.قبول, aPersianDefaultButoon.دكمه1, PersianMessageBoxIcon.خالي);
//            frmMessageBox.Dispose();
            return dr;
        }
        /// <summary>
        /// این تابع با یک پارامتر ورودی تعریف شده است که مربوط به متن پیام می باشد
        /// </summary>
        /// <param name="strMessageText">متن اصلی که می خواهید درون فرم پیام نمایش داده شود</param>
        /// <returns></returns>
        public static aPersianDialogResult Show(string strMessageText)
        {
            dr = Show(strMessageText, "", PersianMessageBoxButton.قبول, aPersianDefaultButoon.دكمه1, PersianMessageBoxIcon.خالي);
            return dr;
        }
        /// <summary>
        /// این تابع مربوط به ایجاد کنترلهای مورد نظر  روی فرم و تنظیم بعضی از ویژگیهای آنها می باشد
        /// </summary>
        /// <param name="strMessageText">متن اصلی که می خواهید درون فرم پیام نمایش داده شود</param>
        /// <param name="strMessageHeader">متنی که می خواهید در بالای فرم پیام نوشته شود</param>
        private static void AddControls(string strMessageText, string strMessageHeader)
        {

            picMessageBoxIcon.Parent = pnlIcon;// frmMessageBox;
            picMessageBoxIcon.SizeMode = PictureBoxSizeMode.StretchImage;
            picMessageBoxIcon.Size = new Size(32, 32);
            picMessageBoxIcon.BackgroundImageLayout = ImageLayout.Stretch;
            //picMessageBoxIcon.Dock = DockStyle.Fill;
            picMessageBoxIcon.Left = 5;
            picMessageBoxIcon.Top = 8;
//            picMessageBoxIcon.BackColor = Color.AliceBlue;

            lblMessage.Parent = pnlMessage;// frmMessageBox;
            lblMessage.Text = strMessageText;// + MessageBoxIcon.Asterisk;
            lblMessage.AutoSize = false;
            lblMessage.Dock = DockStyle.Fill;
            //lblMessage.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
            lblMessage.TextAlign = ContentAlignment.MiddleLeft;
//            lblMessage.BackColor = Color.Azure;
            lblMessage.RightToLeft = RightToLeft.Yes;
            lblMessage.Font = fntPersianMessageBox;
            lblMessage.ForeColor = MessageForeColor; // colMessageForeColor;
            lblMessage.BackColor = MessageBackColor; // colMessageBackColor; 
            
            //  lblMessage.Size = new Size(lblMessage.Text.Length , 15);
            // lblMessage.Dock = DockStyle.Fill;

            //pnlMessage.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
            //pnlMessage.AutoSize = true; //
            //pnlMessage.Size = new Size(frmMessageBox.Size.Width, lblMessage.Size.Height);
            //pnlMessage.Location = new Point(AH.Windows.Forms.PersianMessageBox.frmMessageBox.Location.X, AH.Windows.Forms.PersianMessageBox.frmMessageBox.Location.Y + 10);
            //pnlMessage.BackColor = Color.AliceBlue;
            frmMessageBox.MaximizeBox = false;
            frmMessageBox.MinimizeBox = false;
            frmMessageBox.RightToLeft = RightToLeft.Yes;
         //   frmMessageBox.Opacity = 100;
            //frmMessageBox.RightToLeftLayout = true;
            //                        frmMessageBox.AutoSize = true;
         //   MessageBox.Show("سلام علی","asgdjs", MessageBoxButtons.OK , MessageBoxIcon.Error);
            //System.Messaging.Design.MessageDesigner md = new System.Messaging.Design.MessageDesigner();
            
            switch (pnlButtons.Controls.Count) //frmMessageBox.Controls.Count)
            {
                case 1:
                    {
                        if (lblMessage.Text.Length > 5 && lblMessage.Text.Length > strMessageHeader.Length)
                            frmMessageBox.Size = new Size(((lblMessage.Text.Length - 5) * 5) + 120, 125);
                        else if (strMessageHeader.Length > 5 && strMessageHeader.Length > lblMessage.Text.Length)
                            frmMessageBox.Size = new Size(((strMessageHeader.Length - 5) * 5) + 120, 125);
                        else
                            frmMessageBox.Size = new Size(120, 125);
                        if (frmMessageBox.Width > 600)
                            frmMessageBox.Width = 600;
                        break;
                    }
                case 2:
                    {
                        if (lblMessage.Text.Length > 15 && lblMessage.Text.Length > strMessageHeader.Length)
                            frmMessageBox.Size = new Size(((lblMessage.Text.Length - 15) * 5) + 190, 125);
                        else if (strMessageHeader.Length > 5 && strMessageHeader.Length > lblMessage.Text.Length)
                            frmMessageBox.Size = new Size(((strMessageHeader.Length - 15) * 5) + 190, 125);
                        else
                            frmMessageBox.Size = new Size(190, 125);
                        if (frmMessageBox.Width > 600)
                            frmMessageBox.Width = 600;
                        break;
                    }
                case 3:
                    {
                        if (lblMessage.Text.Length > 30 && lblMessage.Text.Length > strMessageHeader.Length)
                            frmMessageBox.Size = new Size(((lblMessage.Text.Length - 30) * 5) + 270, 125);
                        else if (strMessageHeader.Length > 5 && strMessageHeader.Length > lblMessage.Text.Length)
                            frmMessageBox.Size = new Size(((strMessageHeader.Length - 30) * 5) + 270, 125);
                        else
                            frmMessageBox.Size = new Size(270, 125);
                        if (frmMessageBox.Width > 600)
                            frmMessageBox.Width = 600;
                        break;
                    }
            }

       //     MessageBox.Show(lblMessage.Location.ToString());
            frmMessageBox.StartPosition = FormStartPosition.CenterScreen;
            frmMessageBox.FormBorderStyle = FormBorderStyle.FixedDialog;
            frmMessageBox.Font = fntPersianMessageBox;
            frmMessageBox.Text = strMessageHeader;
            frmMessageBox.BackColor = colMesssageBoxBackColor;
            frmMessageBox.BackgroundImage = imgMessageBoxBackGround;
            frmMessageBox.FormClosing += new  FormClosingEventHandler(Azx.Windows.Forms.PersianMessageBox.frmMessageBox_FormClosing);
            frmMessageBox.Load += new EventHandler(frmMessageBox_Load);
            timCurrent.Tick += new EventHandler(timCurrent_Tick);

        }
        #endregion 
        #region PersianMessageBox Events
        private static void frmMessageBox_Load(object sender, EventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");
//            InitializeComponent();
            timCurrent.Enabled = true;
            timCurrent.Interval = 100;
            
        }

        private static void timCurrent_Tick(object sender, EventArgs e)
        {
            if (frmMessageBox.Opacity < 1)
            {
                for (int i = 1; i < 10000000; i++) ;
                frmMessageBox.Opacity += .1;
            }
            else
                timCurrent.Enabled = false;
            ////throw new Exception("The method or operation is not implemented.");
            //Timer timCurrent = (Timer)sender;
            //if (timCurrent.Interval == 15)
            //{
            //    AH.Windows.Forms.PersianMessageBox.frmMessageBox.Opacity += .01;
            //    if (AH.Windows.Forms.PersianMessageBox.frmMessageBox.Opacity == 1)
            //        timCurrent.Stop();
            //}
        }

        private static void frmMessageBox_FormClosing(object sender, FormClosingEventArgs e)
        {
//            throw new Exception("The method or operation is not implemented.");
            //frmMessageBox.Close();
            while (frmMessageBox.Opacity != 0)
            {
                e.Cancel = true;
                for (int i = 0; i < 6000000; i++) ;
                frmMessageBox.Opacity -= .01;
            }
            e.Cancel = false;
       //     frmMessageBox.Close();

        }
        #endregion
    }
}
