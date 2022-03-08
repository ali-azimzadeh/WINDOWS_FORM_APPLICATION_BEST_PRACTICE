using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Azx.Windows.Forms
{
	/// <summary>
	/// Summary description for Iconits.
	/// </summary>
	public class GrowIcon : Azx.Windows.Forms.UserControl
	{
        private System.ComponentModel.IContainer components;
		//private Vpp.Windows.Forms.Timer timer;
        System.Windows.Forms.Timer timer;
		private Azx.Windows.Forms.ToolTip _toolTip = new ToolTip();
		Bitmap[] _bitmap;	
		int _flag;
		bool blnEnter;
		Graphics _graphic,_graphic2;
		int imWidth,imHeight;
		double curWidth,curHeight;
		double addX,addY;
        Bitmap dblBufferBitmap;
        bool blnBlur = true;

		public GrowIcon()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
            
			// TODO: Add any initialization after the InitializeComponent call			
            _bitmap = new Bitmap[4];
            for (int i = 0; i < 4; i++)
                _bitmap[i] = new Bitmap(Width, Height);
            dblBufferBitmap = new Bitmap(Width, Height);
            IconSize = new Size(Width / 2, Height / 2);
            _graphic = this.CreateGraphics();
            TextColor = Brushes.Black;
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Interval = 10;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // GrowIcon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "GrowIcon";
            this.Size = new System.Drawing.Size(160, 128);
            this.MouseEnter += new System.EventHandler(this.GrowIcon_MouseEnter);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GrowIcon_Paint);
            this.MouseLeave += new System.EventHandler(this.GrowIcon_MouseLeave);
            this.ResumeLayout(false);

		}
		#endregion

		public Bitmap Icon
		{
			get 
            {
                return (_bitmap[0]); 
            }
			set 
            {
                for (int i = 0; i < 4; i++)
                    _bitmap[i] = new Bitmap(Width, Height);
                dblBufferBitmap = new Bitmap(Width, Height);

                _bitmap[0] = value;
                _bitmap[1] = Alpha.ReturnAlpha(_bitmap[0], 60);
                _bitmap[2] = Alpha.ReturnAlpha(_bitmap[0], 120);
                _bitmap[3] = Alpha.ReturnAlpha(_bitmap[0], 180); ;
				draw(0);				
			}
		}

		public new Size Size
		{
			get 
            {
                return (new Size(Width,Height));
            }
			set 
			{
				Width = ((Size)value).Width;
				Height= ((Size)value).Height;				
				if (Width>160) 
				{
					//MessageBox.Show("Width cannot exceed 160 :p");
                    Width = 160;
				}
				if (Height>128) 
				{
					//MessageBox.Show("Height cannot exceed 128 :p");
                    Height = 128;						
				}
				Calculate();
			}
		}	

		public Size IconSize
		{
			get 
            {
                return (new Size(imWidth,imHeight));
            }
			set
			{
                imWidth = ((Size)value).Width;
                imHeight = ((Size)value).Height;
                if (imWidth > Width) imWidth = Width;
                if (imHeight > Height) imHeight = Height;
				Calculate();
			}
		}

		public bool Blur
		{
			get 
            {
                return (blnBlur);
            }
			set
			{
                blnBlur = value;
				if (!blnBlur)
				{
					_bitmap[1].Dispose();
					_bitmap[2].Dispose();
					_bitmap[3].Dispose();
				}
				else
				{
                    _bitmap[1] = Alpha.ReturnAlpha(_bitmap[0], 60);
                    _bitmap[2] = Alpha.ReturnAlpha(_bitmap[0], 120);
                    _bitmap[3] = Alpha.ReturnAlpha(_bitmap[0], 180);
				}
			}
		}

		public string About
		{
			get 
            {
                return ("GrowIcons 0.1 - For Menus and the other use in applications, author: Ali Azimzadeh");
            }
		}

		public string TooltipText
		{
			get 
            {
                return (_toolTip.GetToolTip(this));
            }
			set 
            {
                _toolTip.SetToolTip(this,value); 				
			}
		}

        private string _text = "";
        public new string Text
        {
            get
            {
                return (_text);
            }
            set
            {
                _text = value;
            }
        }
        
        private Brush _textColor;
        [DefaultValue(typeof(Brushes),"Black")]
        public Brush TextColor
        {
            get
            {
                return (_textColor);
            }
            set
            {
                _textColor = value;
                if (_textColor == value)
                    _textColor = Brushes.Black;
            }
        }

        private void Calculate()
        {
            curWidth = imWidth;
            curHeight = imHeight;

            addX = (double)(Width - imWidth) / 10;
            addY = (double)(Height - imHeight) / 10;
        }

        private void timer_Tick(object sender, System.EventArgs e)
        {
            if (blnEnter)
            {
                if (curWidth < Width)
                {
                    curWidth += addX;
                }

                if (curHeight < Height)
                {
                    curHeight += addY;
                }

                if (curWidth >= Width && curHeight >= Height)
                    timer.Stop();
                _flag++;
            }
            else
            {
                if (curWidth > imWidth)
                {
                    curWidth -= addX;
                }

                if (curHeight > imHeight)
                {
                    curHeight -= addY;
                }

                if (curWidth <= imWidth && curHeight <= imHeight)
                    timer.Stop();

                _flag--;
            }

            if (_flag > 9)
                draw(0);
            else if (_flag > 6)
                draw(1);
            else if (_flag > 3)
                draw(2);
            else draw(3);
        }

        private void GrowIcon_MouseEnter(object sender, System.EventArgs e)
        {
            Cursor = Cursors.Hand;
            blnEnter = true;
            timer.Start();
        }

        private void GrowIcon_MouseLeave(object sender, System.EventArgs e)
        {
            Cursor = Cursors.Default;
            blnEnter = false;
            timer.Start();
        }

        private void GrowIcon_Paint(object sender, PaintEventArgs e)
        {
            draw(3);
        }

        private void draw(int state)
        {
            int st;

            if (blnBlur)
                st = state;
            else
                st = 0;

            _graphic2 = Graphics.FromImage(dblBufferBitmap);
            _graphic2.Clear(this.BackColor);
            _graphic2.DrawImage(_bitmap[st], (int)((double)Width - curWidth) / 2, (int)((double)Height - curHeight) / 2, (int)curWidth, (int)curHeight);

            _graphic.DrawImageUnscaled(dblBufferBitmap, 0, 0);
            _graphic.DrawString(Text, this.Font, TextColor, (this.Width/3)-this.Text.Length, this.Height - 16);
//            _graphic.DrawString(Text, this.Font, TextColor, (Math.Abs(this.Width - this.Text.Length)) / 2, this.Height - 16);
        }
    }
    internal class Alpha
    {
        public Alpha()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private static int min(int val)
        {
            return (val < 0 ? 0 : val);
        }

        public static void SetAlpha(Bitmap bmp, int alpha)
        {
            Color _color;

            for (int i = 0; i < bmp.Width; i++)
                for (int j = 0; j < bmp.Height; j++)
                {
                    _color = bmp.GetPixel(i, j);
                    if (_color.A > 0)
                        bmp.SetPixel(i, j, Color.FromArgb(min(_color.A - alpha), _color.R, _color.G, _color.B));
                }
        }

        public static Bitmap ReturnAlpha(Bitmap bmp, int alpha)
        {
            Color _color;
            Bitmap bitmap2 = new Bitmap(bmp);

            for (int i = 0; i < bmp.Width; i++)
                for (int j = 0; j < bmp.Height; j++)
                {
                    _color = bmp.GetPixel(i, j);
                    if (_color.A > 0)
                        bitmap2.SetPixel(i, j, Color.FromArgb(min(_color.A - alpha), _color.R, _color.G, _color.B));
                }
            return bitmap2;
        }
    }
}
