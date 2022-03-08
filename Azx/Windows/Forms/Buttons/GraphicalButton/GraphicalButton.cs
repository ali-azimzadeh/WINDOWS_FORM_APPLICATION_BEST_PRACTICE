// Copyright 2006 Herre Kuijpers - <herre@xs4all.nl>
//
// This source file(s) may be redistributed, altered and custimized
// by any means PROVIDING the authors name and all copyright
// notices remain intact.
// THIS SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED. USE IT AT YOUR OWN RISK. THE AUTHOR ACCEPTS NO
// LIABILITY FOR ANY DATA DAMAGE/LOSS THAT THIS PRODUCT MAY CAUSE.
//-----------------------------------------------------------------------
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Azx.Windows.Forms;

namespace Azx.Windows.Forms //GraphicalStyleControls
{
    public partial class GraphicalBar : UserControl
    {
        /// <summary>
        /// این کلاس یک دکمه  را به شکل گرافیکی نمایش می دهد
        /// این کلاس یک کلاس داخلی می باشد و یک کنترل نیست
        /// </summary>
        #region GraphicalBarButton Properties
        public class GraphicalBarButton // : IComponent
        {
            private bool _enabled = true;

            [Description("Indicates wether the button is enabled"), Category("GraphicalButton")]
          //  [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
            public bool Enabled
            {
                get 
                {
                    return _enabled; 
                }
                set 
                { 
                    _enabled = value; 
                }
            }

            protected Image _image = null;
            [Description("تصویری که می خواهید بروی دکمه قرار گیرد"), Category("GraphicalButton")]
            [DisplayName ("تصویر روی دکمه")]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
            public Image Image
            {
                get 
                {
                    return _image; 
                }
                set { 
                    _image = value; 
                    _parent.Invalidate(); 
                }
            }

            protected object _tag = null;
            [Description("فیلد در اختیار کاربر که در خصوص دکمه می تواند بکار ببرد"), Category("GraphicalButton")]
            [DisplayName("فیلد آزاد")]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
            public object Tag
            {
                get 
                {
                    return _tag; 
                }
                set
                {
                    _tag = value; 
                }
            }

            protected GraphicalBar _parent;

            internal GraphicalBar Parent
            {
                get
                {
                    return _parent;
                }
                set
                {
                    _parent = value;
                }
            }

            protected string _text;
            [Description("متنی که می خواهید بروی دکمه نوشته شود"), Category("GraphicalButton")]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
            [DefaultValue("Graphical Button")]
            public string Text
            {
                get
                {
                    return _text; 
                }
                set
                {
                    _text = value;
                    if (_text == null)
                        _text = "Graphical Button";
                    _parent.Invalidate();
                }
            }

            protected int _height;
            [Description("مشخص کننده ارتفاع دکمه است"), Category("GraphicalButton")]
            [DisplayName("ارتفاع دکمه")]
            public int Height
            {
                get 
                {
                    return _parent == null ? 23 : _parent._buttonHeight; 
                }

            }

            [Description("مشخص کننده عرض دکمه است"), Category("GraphicalButton")]
            [DisplayName("عرض دکمه")]
            public int Width
            {
                get 
                {
                    return _parent == null ? 60 : _parent.Width - 2; 
                }
            }

            private ContentAlignment _textAlignment;
            [Description("تعیین کننده جای ظاهر شدن متن بروی دکمه"), Category("GraphicalButton")]
            [DisplayName("جهت متن روی دکمه")]
            [DefaultValue(ContentAlignment.MiddleCenter)]
            public ContentAlignment TextAlign
            {
                get
                {
                    return _textAlignment;
                }
                set
                {
                    _textAlignment = value;
                }
            }

            private ImageAlign _imageAlignment;
            [Description("تعیین کننده جای ظاهر شدن تصویر بروی دکمه"), Category("GraphicalButton")]
            [DisplayName("جهت تصویر روی دکمه")]
            [DefaultValue(ImageAlign.Left)]
            public ImageAlign ImageAlign
            {
                get
                {
                    return _imageAlignment;
                }
                set
                {
                    _imageAlignment = value;
                }
            }

            public GraphicalBarButton()
            {
                this._parent = new GraphicalBar(); // set it to a dummy GraphicalButton control
                _text = "";
                ImageAlign = ImageAlign.Left;
                TextAlign = ContentAlignment.MiddleCenter;
                Text = "Graphical Button";
            }

            public GraphicalBarButton(GraphicalBar parent)
            {
                this._parent = parent;
                _text = "";
            }



            /// <summary>
            /// the Graphical button will paint itself on its container (the GraphicalButton)
            /// </summary>
            public void PaintButton(Graphics graphics, int x, int y, bool IsSelected, bool IsHovering)
            {
                Brush br;
                Rectangle rect = new Rectangle(0, y, this.Width, this.Height);
                if (_enabled)
                {
                    if (IsSelected)
                        if (IsHovering)
                            br = new LinearGradientBrush(rect, _parent._gradientButtonSelectedDark, _parent._gradientButtonSelectedLight, 90f);
                        else
                            br = new LinearGradientBrush(rect, _parent._gradientButtonSelectedLight, _parent._gradientButtonSelectedDark, 90f);
                    else
                        if (IsHovering)
                            br = new LinearGradientBrush(rect, _parent._gradientButtonHoverLight, _parent._gradientButtonHoverDark, 90f);
                        else
                            br = new LinearGradientBrush(rect, _parent._gradientButtonLight, _parent._gradientButtonDark, 90f);
                }
                else
                    br = new LinearGradientBrush(rect, _parent._gradientButtonLight, _parent._gradientButtonDark, 90f);

                graphics.FillRectangle(br, x, y, this.Width, this.Height);
                br.Dispose();

                if (_text.Length > 0)
                {
                    int intAlign=0,intTextAlignment = this.Width;
                    switch(TextAlign)
                    {
                        case ContentAlignment.MiddleLeft:
                            {
                                intAlign = 1;
                                break;
                            }
                        case ContentAlignment.MiddleCenter:
                            {
                              //  MessageBox.Show(this.Text.Length.ToString());
                                intAlign = (intTextAlignment / 2) - (this.Text.Length / 2)-36;
                        //        MessageBox.Show(intAlign.ToString());
                                break;
                            }
                        case ContentAlignment.MiddleRight:
                            {
                                intAlign = (intTextAlignment - this.Text.Length*6)-12;
                                break;
                            }
                        default:
                            {
                                intAlign = 0;
                                break;
                            }
                    }
                    graphics.DrawString(this.Text, _parent.Font, Brushes.Black,intAlign, y + this.Height / 2 - _parent.Font.Height / 2);
                }

                if (_image != null)
                {
                    int intimage = 0;
                    int intImageAlignment = this.Width;
                    switch (ImageAlign)
                    {
                        case ImageAlign.Left:
                            {
                                intimage = 1;
                                break;
                            }
                        case ImageAlign.Center:
                            {
                                intimage = (intImageAlignment/2 - _image.Width);
                                break;
                            }
                        case ImageAlign.Right:
                            {
                                intimage = (intImageAlignment - _image.Width);
                                break;
                            }
                    }
//                    graphics.DrawImage(_image, 36 / 2 - _image.Width / 2, y + this.Height / 2 - _image.Height / 2, _image.Width, _image.Height);
                    graphics.DrawImage(_image, intimage, y + this.Height / 2 - _image.Height / 2, _image.Width, _image.Height);
                }
            }
        }
        #endregion



        /// <summary>
        /// شامل لیستی از دکمه ها که اضافه ،حذف و بروز کردن آنها را به ترتیب  بر عهده دارد GraphicalButton کلاس  
        /// توجه این که این کلاس یک کنترل نیست
        /// </summary>
        #region GraphicalBarButtons list
        public class GraphicalBarButtons : CollectionBase
        {
            //protected ArrayList List;
            protected GraphicalBar _parent;
            public GraphicalBar Parent
            {
                get 
                {
                    return _parent; 
                }
            }

            internal GraphicalBarButtons(GraphicalBar _parent)   : base()
            {
                this._parent = _parent;
            }

            public GraphicalBarButton this[int index]
            {
                get 
                {
                    return (GraphicalBarButton)List[index]; 
                }
            }

            public void  Add(GraphicalBarButton item)
            {
                if (List.Count == 0)
                {
                    Parent.SelectedButton = item;
                    List.Add(item);
                    item.Parent = this.Parent;
                    Parent.ButtonlistChanged();
                }
            }

            public GraphicalBarButton Add(string text, Image image)
            {
                GraphicalBarButton b = new GraphicalBarButton(this._parent);
                b.Text = text;
                b.Image = image;
                this.Add(b);
                return b;
            }

            public GraphicalBarButton Add(string text)
            {
                return this.Add(text, null);
            }

            public GraphicalBarButton Add()
            {
                return this.Add();
            }

            

            public void Remove(GraphicalBarButton button)
            {
                List.Remove(button);
                Parent.ButtonlistChanged();
            }

            public int IndexOf(object value)
            {
                return List.IndexOf(value);
            }

            public GraphicalBarButtons()
            {
            }
            #region handle CollectionBase events
            protected override void OnInsertComplete(int index, object value)
            {
                GraphicalBarButton b = (GraphicalBarButton)value;
                b.Parent = this._parent;
                Parent.ButtonlistChanged();
                base.OnInsertComplete(index, value);
            }

            protected override void OnSetComplete(int index, object oldValue, object newValue)
            {
                GraphicalBarButton b = (GraphicalBarButton)newValue;
                b.Parent = this._parent;
                Parent.ButtonlistChanged();
                base.OnSetComplete(index, oldValue, newValue);
            }

            protected override void OnClearComplete()
            {
                Parent.ButtonlistChanged();
                base.OnClearComplete();
            }
            #endregion handle CollectionBase events
        }
        #endregion GraphicalBarButtons list

        #region GraphicalBar property definitions


        /// <summary>
        /// تعریف متغیری که مشخص کند کدام دکمه کلیک شده است
        /// </summary>
        protected GraphicalBarButton _selectedButton = null;

        // this variable remembers the button index over which the mouse is moving
        /// <summary>
        /// تعریف متغیری که اندیس دکمه ای که با ماوس بروی آن رفته شده را نگه می دارد
        /// </summary>
        protected int hoveringButtonIndex = -1;

        // property to set the buttonHeigt
        // default is 30
        /// <summary>
        /// این ویژگی جهت تنظیم ارتفاع دکمه می باشد که مقدار اولیه آن 23 است 
        /// </summary>
        protected int _buttonHeight;
        [Description("مشخص کننده ارتفاع هر دکمه ایجاد شده می باشد"), Category("GraphicalButton")]
        [DisplayName ("ارتفاع دکمه")]
        [DefaultValue(23)]
        public int ButtonHeight
        {
            get 
            {
                return _buttonHeight; 
            }
            set 
            {
                if (value > 18)
                    _buttonHeight = value;
                else
                    _buttonHeight = 18;
            }
        }
        /// <summary>
        /// در این قسمت رنگ افکت تیره دکمه ایجاد شده مشخص می گردد
        /// </summary>
        protected Color _gradientButtonDark = Color.FromArgb(178, 193, 140);
        [Description("رنگ افکت تیره دکمه را مشخص می کند"), Category("GraphicalButton")]
        [DisplayName("رنگ افکت تیره")]
        [DefaultValue(typeof(Color),"178,193,140")]
        public Color GradientButtonNormalDark
        {
            get 
            {
                return _gradientButtonDark; 
            }
            set 
            {
                _gradientButtonDark = value; 
            }
        }

        /// <summary>
        /// در این قسمت رنگ افکت روشن دکمه ایجاد شده مشخص می گردد
        /// </summary>
        protected Color _gradientButtonLight = Color.FromArgb(234, 240, 207);
        [Description("رنگ افکت روشن دکمه را مشخص می کند"), Category("GraphicalButton")]
        [DisplayName("رنگ افکت روشن")]
        [DefaultValue(typeof(Color), "234, 240, 207")]
        public Color GradientButtonNormalLight
        {
            get 
            {
                return _gradientButtonLight; 
            }
            set
            {
                _gradientButtonLight = value; 
            }
        }

        /// <summary>
        /// در این قسمت رنگ افکت تیره دکمه ایجاد شده  در هنگامی ماوس بروی آن قرار می گیرد مشخص می گردد
        /// </summary>
        protected Color _gradientButtonHoverDark = Color.FromArgb(247, 192, 91);
        [Description("رنگ افکت تیره به هنگام رفتن موس بروی آن"), Category("GraphicalButton")]
        [DisplayName ("رنگ افکت تیره وقتی موس روی دکمه قراردارد")]
        [DefaultValue(typeof(Color), "247, 192, 91")]
        public Color GradientButtonHoverDark
        {
            get
            {
                return _gradientButtonHoverDark; 
            }
            set
            {
                _gradientButtonHoverDark = value; 
            }
        }

        /// <summary>
        /// در این قسمت رنگ افکت روشن دکمه ایجاد شده  در هنگامی ماوس بروی آن قرار می گیرد مشخص می گردد
        /// </summary>
        protected Color _gradientButtonHoverLight = Color.FromArgb(255, 255, 220);
        [Description("رنگ افکت روشن به هنگام رفتن موس بروی آن"), Category("GraphicalButton")]
        [DisplayName("رنگ افکت روشن وقتی موس روی دکمه قراردارد")]
        [DefaultValue(typeof(Color), "255, 255, 220")]
        public Color GradientButtonHoverLight
        {
            get 
            {
                return _gradientButtonHoverLight; 
            }
            set
            {
                _gradientButtonHoverLight = value; 
            }
        }

        /// <summary>
        /// در این قسمت رنگ افکت تیره دکمه ایجاد شده  در هنگامی  که با ماوس بروی آن کلیک شود مشخص می گردد
        /// </summary>
        protected Color _gradientButtonSelectedDark = Color.FromArgb(239, 150, 21);
        [Description("رنگ افکت تیره به هنگام کلیک کردن با موس  بروی آن"), Category("GraphicalButton")]
        [DisplayName("رنگ افکت تیره وقتی با موس روی دکمه کلیک شود")]
        [DefaultValue(typeof(Color), "239, 150, 21")]
        public Color GradientButtonSelectedDark
        {
            get
            {
                return _gradientButtonSelectedDark; 
            }
            set
            {
                _gradientButtonSelectedDark = value; 
            }
        }

        /// <summary>
        /// در این قسمت رنگ افکت روشن دکمه ایجاد شده  در هنگامی  که با ماوس بروی آن کلیک شود مشخص می گردد
        /// </summary>
        protected Color _gradientButtonSelectedLight = Color.FromArgb(251, 230, 148);
        [Description("رنگ افکت روشن به هنگام کلیک کردن با موس  بروی آن"), Category("GraphicalButton")]
        [DisplayName("رنگ افکت روشن وقتی با موس روی دکمه کلیک شود")]
        [DefaultValue(typeof(Color), "251, 230, 148")]
        public Color GradientButtonSelectedLight
        {
            get
            {
                return _gradientButtonSelectedLight; 
            }
            set
            {
                _gradientButtonSelectedLight = value; 
            }
        }


        /// <summary>
        /// when a button is selected programatically, it must update the control
        /// and repaint the buttons
        /// </summary>
        [Browsable(false)]
        public GraphicalBarButton SelectedButton
        {
            get 
            {
                return _selectedButton; 
            }
            set 
            {                
                // assign new selected button
                PaintSelectedButton(_selectedButton, value);

                // assign new selected button
                _selectedButton = value; 
            }
        }

        /// <summary>
        /// (GraphicalBarButtons)  تعریف دکمه هایی که در داخل کلاس  مقابل قابل کلیک باشند
        /// </summary>
        protected GraphicalBarButtons _buttons;
        /// <summary>
        /// readonly list of buttons
        /// </summary>
        //[Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("در این قسمت دکمه ایجاد می گردد"), Category("GraphicalButton")]
        [DisplayName("ایجاد دکمه ها")]
        public GraphicalBarButtons Buttons
        {
            get 
            {
                
                return _buttons;
            }
        }

        #endregion GraphicalBar property definitions

        #region GraphicalBar events


        [Serializable]
        public class ButtonClickEventArgs : MouseEventArgs
        {
            public ButtonClickEventArgs(GraphicalBarButton button, MouseEventArgs evt) : base(evt.Button, evt.Clicks, evt.X, evt.Y, evt.Delta)
            {
                SelectedButton = button;
            }
           
            public readonly GraphicalBarButton SelectedButton;
        }

        public delegate void ButtonClickEventHandler(object sender, ButtonClickEventArgs e);

        public new event ButtonClickEventHandler Click;

        #endregion GraphicalBar events

        #region GraphicalBar functions

        public GraphicalBar()
        {
            InitializeComponent();
            _buttons = new GraphicalBarButtons(this);
            _buttonHeight = 23; // set default to 30
            GradientButtonNormalDark = Color.FromArgb(178, 193, 140);
            GradientButtonNormalLight = Color.FromArgb(234, 240, 207);
            GradientButtonHoverDark = Color.FromArgb(247, 192, 91);
            GradientButtonHoverLight = Color.FromArgb(255, 255, 220);
            GradientButtonSelectedDark = Color.FromArgb(239, 150, 21);
            GradientButtonSelectedLight = Color.FromArgb(251, 230, 148);
        }

        private void PaintSelectedButton(GraphicalBarButton prevButton,GraphicalBarButton newButton)
        {
            if (prevButton == newButton)
                return; // no change so return immediately

            int selIdx = -1;
            int valIdx = -1;
            
            // find the indexes of the previous and new button
            selIdx = _buttons.IndexOf(prevButton);
            valIdx = _buttons.IndexOf(newButton);

            // now reset selected button
            // mouse is leaving control, so unhighlight anythign that is highlighted
            Graphics g = Graphics.FromHwnd(this.Handle);
            if (selIdx >= 0)
                // un-highlight current hovering button
                _buttons[selIdx].PaintButton(g, 1, selIdx * (_buttonHeight + 1) + 1, false, false);

            if (valIdx >= 0)
                // highlight newly selected button
                _buttons[valIdx].PaintButton(g, 1, valIdx * (_buttonHeight + 1) + 1, true, false);
            g.Dispose();
        }

        /// <summary>
        /// returns the button given the coordinates relative to the Graphicalbar control
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public GraphicalBarButton HitTest(int x, int y)
        {
            int index = (y - 1) / (_buttonHeight + 1);
            if (index >= 0 && index < _buttons.Count)
                return _buttons[index];
            else
                return null;
        }

        /// <summary>
        /// this function will setup the control to cope with changes in the buttonlist 
        /// that is, addition and removal of buttons
        /// </summary>
        private void ButtonlistChanged()
        {
            if (!this.DesignMode) // only set sizes automatically at runtime
                this.MaximumSize = new Size(0, _buttons.Count * (_buttonHeight + 1) + 1);

            this.Invalidate();
        }

        #endregion GraphicalBar functions

        #region GraphicalBar control event handlers

        private void GraphicalBar_Load(object sender, EventArgs e)
        {
            // initiate the render style flags of the control
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer |
                ControlStyles.Selectable |
                ControlStyles.UserMouse,
                true
                );
        }

        private void GraphicalBar_Paint(object sender, PaintEventArgs e)
        {
            int top = 1;
            foreach (GraphicalBarButton b in Buttons)
            {
                b.PaintButton(e.Graphics, 1, top, b.Equals(this._selectedButton), false);
                top += b.Height+1;
            }
        }

        private void GraphicalBar_Click(object sender, EventArgs e)
        {
            if (!(e is MouseEventArgs)) return;

            // case to MouseEventArgs so position and mousebutton clicked can be used
            MouseEventArgs mea = (MouseEventArgs) e;

            // only continue if left mouse button was clicked
            if (mea.Button != MouseButtons.Left) return;
            
            int index = (mea.Y - 1) / (_buttonHeight + 1);

            if (index < 0 || index >= _buttons.Count)
                return;

            GraphicalBarButton button = _buttons[index];
            if (button == null) return;
            if (!button.Enabled) return;

            // ok, all checks passed so assign the new selected button
            // and raise the event
            SelectedButton = button;

            ButtonClickEventArgs bce = new ButtonClickEventArgs(_selectedButton, mea);
            if (Click != null) // only invoke on left mouse click
                Click.Invoke(this, bce);
        }

        private void GraphicalBar_DoubleClick(object sender, EventArgs e)
        {
            //TODO: only if you intend to support a doubleclick
            // this can be implemented exactly like the click event
        }


        private void GraphicalBar_MouseLeave(object sender, EventArgs e)
        {
            // mouse is leaving control, so unhighlight anything that is highlighted
            if (hoveringButtonIndex >= 0)
            {
                // so we need to change the hoveringButtonIndex to the new index
                Graphics g = Graphics.FromHwnd(this.Handle);
                GraphicalBarButton b1 = _buttons[hoveringButtonIndex];

                // un-highlight current hovering button
                b1.PaintButton(g, 1, hoveringButtonIndex * (_buttonHeight + 1) + 1, b1.Equals(_selectedButton), false);
                hoveringButtonIndex = -1;
                g.Dispose();
            }
        }

        private void GraphicalBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.None)
            {
                // determine over which button the mouse is moving
                int index = (e.Location.Y - 1) / (_buttonHeight + 1);
                if (index >= 0 && index < _buttons.Count)
                {
                    if (hoveringButtonIndex == index )
                        return; // nothing changed so we're done, current button stays highlighted

                    // so we need to change the hoveringButtonIndex to the new index
                    Graphics g = Graphics.FromHwnd(this.Handle);

                    if (hoveringButtonIndex >= 0)
                    {
                        GraphicalBarButton b1 = _buttons[hoveringButtonIndex];

                        // un-highlight current hovering button
                        b1.PaintButton(g, 1, hoveringButtonIndex * (_buttonHeight + 1) + 1, b1.Equals(_selectedButton), false);
                    }
                    
                    // highlight new hovering button
                    GraphicalBarButton b2 = _buttons[index];
                    b2.PaintButton(g, 1, index * (_buttonHeight + 1) + 1, b2.Equals(_selectedButton), true);
                    hoveringButtonIndex = index; // set to new index
                    g.Dispose();

                }
                else
                {
                    // no hovering button, so un-highlight all.
                    if (hoveringButtonIndex >= 0)
                    {
                        // so we need to change the hoveringButtonIndex to the new index
                        Graphics g = Graphics.FromHwnd(this.Handle);
                        GraphicalBarButton b1 = _buttons[hoveringButtonIndex];

                        // un-highlight current hovering button
                        b1.PaintButton(g, 1, hoveringButtonIndex * (_buttonHeight + 1) + 1, b1.Equals(_selectedButton), false);
                        hoveringButtonIndex = -1;
                        g.Dispose();
                    }
                }
            }
        }

        /// <summary>
        /// isResizing is used as a signal, so this method is not called recusively
        /// this prevents a stack overflow
        /// </summary>
        private bool isResizing = false;
        private void GraphicalBar_Resize(object sender, EventArgs e)
        {
            // only set sizes automatically at runtime
            if (!this.DesignMode)
            {
                if (!isResizing)
                {
                    isResizing = true;
                    if ((this.Height - 1) % (_buttonHeight + 1) > 0)
                        this.Height = ((this.Height - 1) / (_buttonHeight + 1)) * (_buttonHeight + 1) + 1;
                    this.Invalidate();
                    isResizing = false;
                }
            }
        }

        #endregion GraphicalBar control event handlers

    }
}
