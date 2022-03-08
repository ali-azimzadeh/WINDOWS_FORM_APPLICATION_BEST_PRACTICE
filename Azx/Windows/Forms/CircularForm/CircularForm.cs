using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Azx.Windows.Forms
{
	public partial class CircularForm : Form
	{
		private MouseEventHandler MouseMoveHandler;
		private int mouseDownX;
		private int mouseDownY;

		public CircularForm()
		{
			InitializeComponent();
			this.MouseDown += new MouseEventHandler(this.MouseDownHandler);
			this.MouseUp += new MouseEventHandler(this.MouseUpHandler);
			MouseMoveHandler = new MouseEventHandler(this.MoveForm);
		}

		private bool _allowMoveForm = false;
		public bool AllowMoveForm
		{
			get
			{
				return (_allowMoveForm);
			}
			set
			{
				_allowMoveForm = value;
			}
		}

		private Image _skin;
		public Image Skin
		{
			get
			{
				return (_skin);
			}
			set
			{
				_skin = value;
			}
		}

		//private Color _transparencyKey;
		//[System.ComponentModel.Browsable(false)]
		//public new Color TransparencyKey
		//{
		//    get
		//    {
		//        return (_transparencyKey);
		//    }
		//    set
		//    {
		//        _transparencyKey = value;
		//    }
		//}
		// Summary:
		//     Specifies the border styles for a form.

		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
			this.TransparencyKey = this.BackColor;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics g = e.Graphics;

			//set rendering quality to high
			g.SmoothingMode = SmoothingMode.HighQuality;

			//create a new graphics path to define the shape of the form
			GraphicsPath gPath = new GraphicsPath();

			//	this.StartPosition = FormStartPosition.Manual;
			//create a new Rectangle shape primitive to define our circle
			//NOTE: by using an rectangle with equal sides (square) the AddEllipse
			//function will create a circle
			Rectangle boundingBox = new Rectangle(0, 0, this.Size.Width, this.Size.Height);

			//create the circle
			gPath.AddEllipse(boundingBox);

			//set the visible area (clipping region)
			g.SetClip(gPath, CombineMode.Intersect);

			//add skin
			try
			{
				if (this.BackColor != this.TransparencyKey)
					this.TransparencyKey = this.BackColor;
				if(Skin != null)
					g.DrawImage(Skin, boundingBox);
//				g.DrawImage(Image.FromFile("lbluetex.jpg"), boundingBox);
			}
			//if it isn't in the build folder (with the executable) 
			//then alert the user and close the application
			catch (System.IO.FileNotFoundException ex)
			{
				MessageBox.Show("Copy Misc\\lbluetex.jpg to the build folder!");
				Application.Exit();
			}

			//draw a border around the circle
			Pen borderPen = new Pen(Color.Black, 5);
			g.DrawPath(borderPen, gPath);

			//reset the cliping area to infinite
			g.ResetClip();

			//restore the window after drawing
			//	this.WindowState = FormWindowState.Maximized;
		}

		protected override void OnStyleChanged(EventArgs e)
		{
			base.OnStyleChanged(e);
			if (this.FormBorderStyle != System.Windows.Forms.FormBorderStyle.None)
				this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		}

		private void MouseDownHandler(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.MouseMove += MouseMoveHandler;
				mouseDownX = e.X;
				mouseDownY = e.Y;
			}
		}

		private void MouseUpHandler(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this.MouseMove -= MouseMoveHandler;
			}
		}

		private void MoveForm(object sender, MouseEventArgs e)
		{
			if (AllowMoveForm)
			{
				this.Location = new Point(this.Location.X + (e.X - mouseDownX),
											this.Location.Y + (e.Y - mouseDownY));
			}
		}
	}
}