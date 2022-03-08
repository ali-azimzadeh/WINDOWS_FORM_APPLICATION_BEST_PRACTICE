#region Using Directives
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;
using System.Threading;
#endregion

namespace Azx.Windows.Forms
{
    public delegate void DelegateCloseSplash();

    public class SplashForm : Azx.Windows.Forms.Form
    {	
        #region Constructor
        private SplashForm(String imageFile, Color color)
        {
            Debug.Assert(imageFile != null && imageFile.Length > 0, 
                "A valid file path has to be given");
            // ====================================================================================
            // Setup the form
            // ==================================================================================== 
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ShowInTaskbar = false;
            this.TopMost = true;

            // make form transparent
            this.TransparencyKey = this.BackColor;

            // tie up the events
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SplashForm_KeyUp);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SplashForm_Paint);
            this.MouseDown += new MouseEventHandler(SplashForm_MouseClick);

            // load and make the bitmap transparent
            _bitmap = new Bitmap(imageFile);

            if(_bitmap == null)
                throw new Exception("Failed to load the bitmap file " + imageFile);
            _bitmap.MakeTransparent(color);

            // resize the form to the size of the iamge
            this.Width = _bitmap.Width;
            this.Height = _bitmap.Height;

            // center the form
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            // thread handling
            _delegateClose = new DelegateCloseSplash(InternalCloseSplash);
        }
        #endregion // Constructor

        #region Public Methods
        // this can be used for About dialogs
        public static void ShowModal(String imageFile, Color color)
        {
            _imageFile = imageFile;
            _transparentColor = color;
            MySplashThreadFunction();
        }
        // Call this method with the image file path and the color 
        // in the image to be rendered transparent
        public static void StartSplash(String imageFile, Color color)
        {
            _imageFile = imageFile;
            _transparentColor = color;
            // Create and Start the splash thread
            Thread InstanceCaller = new Thread(new ThreadStart(MySplashThreadFunction));
            InstanceCaller.Start();
        }

        // Call this at the end of your apps initialization to close the splash screen
        public static void CloseSplash()
        {
            if(_instance != null)
                _instance.Invoke(_instance._delegateClose);

        }
        #endregion // Public methods

        #region Dispose
        protected override void Dispose( bool disposing )
        {
            if(_bitmap != null)
              _bitmap.Dispose();
            base.Dispose( disposing );
            _instance = null;
        }
        #endregion // Dispose

        #region Threading code
        // ultimately this is called for closing the splash window
        void InternalCloseSplash()
        {
            this.Close();
            this.Dispose();
        }
        // this is called by the new thread to show the splash screen
        private static void MySplashThreadFunction()
        {
            _instance = new SplashForm(_imageFile, _transparentColor);
            _instance.TopMost = false;
            _instance.ShowDialog();
        }
        #endregion // Multithreading code

        #region Event Handlers

        void SplashForm_MouseClick(object sender, MouseEventArgs e)
        {
            this.InternalCloseSplash();
        }

        private void SplashForm_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.DrawImage(_bitmap, 0,0);
        }

        private void SplashForm_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
                this.InternalCloseSplash();
        }
        #endregion // Event Handlers

        #region Private variables
        private static SplashForm _instance;
        private static String _imageFile;
        private static Color _transparentColor;
        private Bitmap _bitmap;
        private DelegateCloseSplash _delegateClose;
        #endregion

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SplashForm
            // 
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Name = "SplashForm";
            this.ResumeLayout(false);

        }

    }
}
