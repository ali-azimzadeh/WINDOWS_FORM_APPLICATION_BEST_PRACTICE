using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
//using Vpp.Windows.Forms;
namespace Azx.Windows.Forms
{
    partial class VerticalMenu
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
            components = new System.ComponentModel.Container();
            lvwMenu.BorderStyle = BorderStyle.None;
            lvwMenu.Dock = DockStyle.Fill;
            lvwMenu.BackColor = Color.Beige;// SystemColors.ControlLight;
            lvwMenu.MultiSelect = false;
            lvwMenu.LargeImageList = this.ImageList;
            lvwMenu.HotTracking = false;
            //  this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        }

        #endregion
    }
}
