using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Azx.Windows.Forms
{
    public partial class GradientForm : Azx.Windows.Forms.Form
    {
        System.Drawing.Drawing2D.LinearGradientBrush gradientBrush = null;
        public GradientForm()
        {
            InitializeComponent();
            this.BodyStyle = GradientMode.ForwardDiagonal;
            this.BodyFirstColor = Color.Gold;
            this.BodyLastColor = Color.MediumBlue;
        }
       
        public enum GradientMode
        {
            BackwardDiagonal,
            ForwardDiagonal,
            Horizontal,
            Vertical
        }

        private GradientMode _bodyStyle;
        [DefaultValue(Azx.Windows.Forms.GradientForm.GradientMode.ForwardDiagonal)]
        public GradientMode BodyStyle
        {
            get
            {
                return (_bodyStyle);
            }
            set
            {
                _bodyStyle = value;
            }
        }

        private Color _bodyFirstColor;
        [DefaultValue(typeof(Color),"Gold")]
        public Color BodyFirstColor
        {
            get
            {
                return (_bodyFirstColor);
            }
            set
            {
                _bodyFirstColor = value;
                this.Invalidate();
            }
        }

        private Color _bodyLastColor;
        [DefaultValue(typeof(Color), "MediumBlue")]
        public Color BodyLastColor
        {
            get
            {
                return (_bodyLastColor);
            }
            set
            {
                _bodyLastColor = value;
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Rectangle rectangle = new Rectangle(0, 0, this.Width, this.Height);
            switch (BodyStyle)
            {
                case GradientMode.BackwardDiagonal:
                    {
                        gradientBrush = new System.Drawing.Drawing2D.LinearGradientBrush(rectangle, BodyFirstColor, BodyLastColor,System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal);
                        break;
                    }
                case GradientMode.ForwardDiagonal:
                    {
                        gradientBrush = new System.Drawing.Drawing2D.LinearGradientBrush(rectangle, BodyFirstColor, BodyLastColor, System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal);
                        break;
                    }
                case GradientMode.Horizontal :
                    {
                        gradientBrush = new System.Drawing.Drawing2D.LinearGradientBrush(rectangle, BodyFirstColor, BodyLastColor, System.Drawing.Drawing2D.LinearGradientMode.Horizontal);
                        break;
                    }
                case GradientMode.Vertical:
                    {
                        gradientBrush = new System.Drawing.Drawing2D.LinearGradientBrush(rectangle, BodyFirstColor, BodyLastColor, System.Drawing.Drawing2D.LinearGradientMode.Vertical);
                        break;
                    }
            }
            e.Graphics.FillRectangle(gradientBrush, rectangle);

        }
    }
}