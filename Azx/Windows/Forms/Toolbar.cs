using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Azx.Windows.Forms
{
    public partial class Toolbar : UserControl
    {
        public Toolbar()
        {
            InitializeComponent();

            this.Dock = DockStyle.Top;
        }

        //*****************************************************************
        //  برای دکمه ایجاد  که از دنیای بیرون آنرا صدا کنیم Event ایجاد یک 
        //*****************************************************************
        public delegate void InsertButtonHandler(object sender, EventArgs e);
        public event InsertButtonHandler InsertButton_Click;
        virtual protected void OnInsertButton_Click(EventArgs e)
        {
            if (InsertButton_Click != null)
            {
                InsertButton_Click(this, e);
            }
        }
        private void insertButton_Click(object sender, EventArgs e)
        {
            OnInsertButton_Click(new EventArgs());
        }

        //*****************************************************************
        //  برای دکمه آپدیت  که از دنیای بیرون آنرا صدا کنیم Event ایجاد یک 
        //*****************************************************************

        public delegate void UpdateButtonHandler(object sender, EventArgs e);
        public event UpdateButtonHandler UpdateButton_Click;

        virtual protected void OnUpdateButton_Click(EventArgs e)
        {
            if(UpdateButton_Click != null)
            {
                UpdateButton_Click(this,e);
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            OnUpdateButton_Click(new EventArgs());
           
        }

        //*****************************************************************
        //  برای دکمه حذف  که از دنیای بیرون آنرا صدا کنیم Event ایجاد
        //*****************************************************************



        public void ProgressBarRun(System.Windows.Forms.ProgressBar _progressBar)
        {
            _progressBar.Visible = true;
            int intValue = 0;
            _progressBar.Value = intValue;
            for (int i = 0; i < 25; i++)
            {
                intValue = 
                    Convert.ToInt32(_progressBar.Value.ToString());
                intValue += 4;
                _progressBar.Value = intValue;
                _progressBar.Refresh();
                System.Threading.Thread.Sleep(25);
            }
            _progressBar.Value = 0;
            _progressBar.Visible = false;
        }
    }
}
