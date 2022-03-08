using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Animations;

namespace Azx.Windows.Forms
{
	/// <summary>
	/// Control representing one group within a <see cref="GroupMenuBar"/>.
	/// It renders borders, a expand/collapse button, a text and an image and
	/// positions on inner control within the inner area of the group.
	/// The properties needed for this are fetched from the associated <see cref="GroupMenuBar"/>.
	/// Cannot be used as a standalone control.
	/// </summary>
	public partial class SlipperyMenuItems : UserControl
	{

        #region Constructor

        private System.Windows.Forms.ToolStripMenuItem _menuExpandCollapse;
        private System.Windows.Forms.ToolStripMenuItem _menuCollapseAll;
        private System.Windows.Forms.ToolStripMenuItem _menuExpandAll;
        private const string _menuCollapseallText = "Collapse All";
        private const string _menuExpandAllText = "Expand All";
        private const string _menuCollapseText = "Collapse";
        private ControlBoundsAnimator _heightAnimator;
        private SlipperyMenuItemsBar _parent;
        /// <summary>
        /// Creates a new instance.
        /// Users should use the <see cref="GroupMenuBar.CreateGroupMenu"/> method instead.
        /// </summary>
        /// <param name="parent">Parent control.</param>
        public SlipperyMenuItems(SlipperyMenuItemsBar parent)
        {
            if (parent == null)
                throw new ArgumentNullException("parent");

            _parent = parent;

            base.SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw | ControlStyles.UserPaint |
                ControlStyles.DoubleBuffer, true);

            _heightAnimator = new ControlBoundsAnimator();
            _heightAnimator.AnimateX = false;
            _heightAnimator.AnimateY = false;
            _heightAnimator.AnimateWidth = false;
            _heightAnimator.Control = this;
            _heightAnimator.AnimationFinished += new EventHandler(OnHeightAnimatorAnimationFinished);

            AdjustDockPadding();

            this.Height = 160;

            _menuExpandCollapse = 
				new   System.Windows.Forms.ToolStripMenuItem(_menuCollapseText,Image, new EventHandler(OnContextMenuClick));
            _menuCollapseAll = 
				new System.Windows.Forms.ToolStripMenuItem(_menuCollapseallText,Image, new EventHandler(OnContextMenuClick));
            _menuExpandAll = 
				new System.Windows.Forms.ToolStripMenuItem(_menuExpandAllText,Image, new EventHandler(OnContextMenuClick));

            this.ContextMenuStrip = new ContextMenuStrip();

            this.ContextMenuStrip.Items.Add(_menuExpandCollapse);
            this.ContextMenuStrip.Items.Add(_menuCollapseAll);
            this.ContextMenuStrip.Items.Add(_menuExpandAll);
        }

        public SlipperyMenuItems()
        {
            SlipperyMenuItemsBar sm = new SlipperyMenuItemsBar();
            SlipperyMenuItems smm = new SlipperyMenuItems(sm);
        }

        #endregion

        #region Properties
        /// <summary>
        /// <see cref="GroupMenuBar"/> which is associated with this instance.
        /// </summary>
        public SlipperyMenuItemsBar ParentBar
        {
            get
            {
                return (_parent);
            }
            set
            {
                _parent = value;
            }
        }

        private const string _defaultText = "";
        private string _text = _defaultText;
        /// <summary>
        /// Gets/sets the text which should be displayed in the header.
        /// </summary>
        public new string Text
        {
            get
            {
                return (_text);
            }
            set
            {
                _text = value;
                Invalidate();
            }
        }

        private Image _image;
        /// <summary>
        /// Gets/sets the image which should be displayed in the header.
        /// </summary>
        public Image Image
        {
            get
            {
                return (_image);
            }
            set
            {
                _image = value;
                Invalidate();
            }
        }

        private bool _expanded = true;
        /// <summary>
        /// Gets/sets whether the group should be expanded.
        /// If the associated <see cref="GroupMenuBar"/> has animation enabled
        /// then setting this property will also result in an animation effect.
        /// </summary>
        public bool Expanded
        {
            get
            {
                return (_expanded);
            }
            set
            {
                if (_expanded == value)
                    return;

                if (_expanded)
                    Collapse();
                else
                    Expand();
            }
        }

        private int _expandedHeight;
        /// <summary>
        /// Gets/sets the height of the group when being expanded.
        /// If the group is currently expanded it is immediatly set to the new height.
        /// </summary>
        public int ExpandedHeight
        {
            get
            {
                return (_expanded ? this.Height : _expandedHeight);
            }
            set
            {
                if (value < _parent.MinimumExpandedHeight)
                    throw new ArgumentException("Value must not be smaller than " + _parent.MinimumExpandedHeight, "ExpandedHeight");

                if (_expandedHeight == value)
                    return;

                _expandedHeight = value;
                if (_expanded)
                {
                    if (_heightAnimator.IsRunning)
                        _heightAnimator.CurrentStep = 100;
                    this.Height = _expandedHeight;
                }
                OnExpandedHeightChanged(EventArgs.Empty);
            }
        }

        private Control _control;
        /// <summary>
        /// Gets/sets the control which should be shown in the inner area of the control.
        /// </summary>
        public Control Control
        {
            get
            {
                return (_control);
            }
            set
            {
                if (_control == value)
                    return;

                if (value as Form != null && (value as Form).TopLevel)
                    (value as Form).TopLevel = false;

                _control = value;

                this.Controls.Clear();

                if (_control == null)
                    return;

                _control.Dock = DockStyle.Fill;
                this.Controls.Add(_control);
            }
        }

        /// <summary>
        /// Gets whether the group is currently being animated.
        /// </summary>
        public bool IsAnimationRunning
        {
            get
            {
                return (_heightAnimator.IsRunning);
            }
        }

        private bool _buttonHighLighted = false;
        private bool ButtonHighLighted
        {
            get 
            { 
                return (_buttonHighLighted); 
            }
            set
            {
                if (value == _buttonHighLighted)
                    return;

                _buttonHighLighted = value;
                Invalidate();
            }
        }

        #endregion
        
        #region Events

        /// <summary>
        /// Event which gets fired when <see cref="Image"/> has changed.
        /// </summary>
        public event EventHandler ImageChanged;
        /// <summary>
        /// Raises the <see cref="ImageChanged"/> event.
        /// </summary>
        /// <param name="eventArgs">Event data.</param>
        protected void OnImageChanged(EventArgs eventArgs)
        {
            if (ImageChanged != null)
                ImageChanged(this, eventArgs);
        }

        /// <summary>
        /// Event which gets fired when <see cref="Text"/> has changed.
        /// </summary>
        public new event EventHandler TextChanged;
        /// <summary>
        /// Raises the <see cref="TextChanged"/> event.
        /// </summary>
        /// <param name="eventArgs">Event data.</param>
        protected new void OnTextChanged(EventArgs eventArgs)
        {
            if (TextChanged != null)
                TextChanged(this, eventArgs);
        }

        /// <summary>
        /// Event which gets fired when <see cref="Control"/> has changed.
        /// </summary>
        public event EventHandler ControlChanged;
        /// <summary>
        /// Raises the <see cref="ControlChanged"/> event.
        /// </summary>
        /// <param name="eventArgs">Event data.</param>
        protected void OnControlChanged(EventArgs eventArgs)
        {
            if (ControlChanged != null)
                ControlChanged(this, eventArgs);
        }

        /// <summary>
        /// Event which gets fired when <see cref="ExpandedHeight"/> has changed.
        /// </summary>
        public event EventHandler ExpandedHeightChanged;
        /// <summary>
        /// Raises the <see cref="ExpandedHeightChanged"/> event.
        /// </summary>
        /// <param name="eventArgs">Event data.</param>
        protected void OnExpandedHeightChanged(EventArgs eventArgs)
        {
            if (ExpandedHeightChanged != null)
                ExpandedHeightChanged(this, eventArgs);
        }

        /// <summary>
        /// Event which gets before the group is expanded.
        /// </summary>
        public event CancelEventHandler MenuExpanding;
        /// <summary>
        /// Raises the <see cref="MenuExpanding"/> event.
        /// </summary>
        /// <param name="eventArgs">Event data.</param>
        protected void OnMenuExpanding(CancelEventArgs eventArgs)
        {
            if (MenuExpanding != null)
                MenuExpanding(this, eventArgs);
        }

        /// <summary>
        /// Event which gets after the group has been expanded.
        /// </summary>
        public event EventHandler MenuExpanded;
        /// <summary>
        /// Raises the <see cref="MenuExpanded"/> event.
        /// </summary>
        /// <param name="eventArgs">Event data.</param>
        protected void OnMenuExpanded(EventArgs eventArgs)
        {
            if (MenuExpanded != null)
                MenuExpanded(this, eventArgs);
        }

        /// <summary>
        /// Event which gets before the group is collapsed.
        /// </summary>
        public event CancelEventHandler MenuCollapsing;
        /// <summary>
        /// Raises the <see cref="MenuCollapsing"/> event.
        /// </summary>
        /// <param name="eventArgs">Event data.</param>
        protected void OnMenuCollapsing(CancelEventArgs eventArgs)
        {
            if (MenuCollapsing != null)
                MenuCollapsing(this, eventArgs);
        }

        /// <summary>
        /// Event which gets after the group has been collapsed.
        /// </summary>
        public event EventHandler MenuCollapsed;
        /// <summary>
        /// Raises the <see cref="MenuCollapsed"/> event.
        /// </summary>
        /// <param name="eventArgs">Event data.</param>
        protected void OnMenuCollapsed(EventArgs eventArgs)
        {
            if (MenuCollapsed != null)
                MenuCollapsed(this, eventArgs);
        }

		#endregion

        #region Methods
        /// <summary>
		/// Expands the group.
		/// If the group is already expanded nothing happens.
		/// If the associated <see cref="GroupMenuBar"/> has animation enabled
		/// then setting this property will also result in an animation effect.
		/// </summary>
		public void Expand()
		{
			Expand(_parent.AnimationEnabled);
		}

		/// <summary>
		/// Expands the group.
		/// If the group is already expanded nothing happens.
		/// </summary>
		/// <param name="animate">Indicates whether the expand should be animated or not.</param>
		public void Expand(bool animate)
		{
			if (_expanded)
				return;

			if (_heightAnimator.IsRunning)
				throw new InvalidOperationException("Cannot expand while animation is running.");

			CancelEventArgs expandingEventArgs = new CancelEventArgs(false);
			OnMenuExpanding(expandingEventArgs);
			if (expandingEventArgs.Cancel)
				return;

			_expanded = true;
			_menuExpandCollapse.Text = _menuCollapseText;
			if (animate)
				AnimateToHeight(_expandedHeight);
			else
			{
				this.Height = _expandedHeight;
				OnMenuExpanded(EventArgs.Empty);
			}

			Invalidate();
		}

		/// <summary>
		/// Collapes the group.
		/// If the group is already collapsed nothing happens.
		/// If the associated <see cref="GroupMenuBar"/> has animation enabled
		/// then setting this property will also result in an animation effect.
		/// </summary>
		public void Collapse()
		{
			Collapse(_parent.AnimationEnabled);
		}

        private const string _menuExpandText = "Expand";
        /// <summary>
		/// Collapes the group.
		/// If the group is already collapsed nothing happens.
		/// </summary>
		/// <param name="animate">Indicates whether the collapse should be animated or not.</param>
		public void Collapse(bool animate)
		{
			if (!_expanded)
				return;

			if (_heightAnimator.IsRunning)
				throw new InvalidOperationException("Cannot expand while animation is running.");

			CancelEventArgs collapsingEventArgs = new CancelEventArgs(false);
			OnMenuCollapsing(collapsingEventArgs);
			if (collapsingEventArgs.Cancel)
				return;

			_expanded = false;
			_menuExpandCollapse.Text = _menuExpandText;
			_expandedHeight = this.Height;
			if (animate)
				AnimateToHeight(_parent.CollapsedHeight);
			else
			{
				this.Height = _parent.CollapsedHeight;
				OnMenuCollapsed(EventArgs.Empty);
			}

			Invalidate();
        }

		internal void AdjustDockPadding()
		{
			this.DockPadding.Left = _parent.BorderWidth;
			this.DockPadding.Right = _parent.BorderWidth;
			this.DockPadding.Bottom = _parent.BorderWidth * 2;
			this.DockPadding.Top = _parent.BorderWidth * 2 + _parent.HeaderHeight;
			Invalidate();
		}

		private void AnimateToHeight(int height)
		{
			_heightAnimator.Intervall = _parent.AnimationIntervall;
			_heightAnimator.StepSize = _parent.AnimationStepSize;
			_heightAnimator.EndBounds = new Rectangle(0, 0, 0, height);
			_heightAnimator.Start(true);
		}

		private bool CheckResizePosition(MouseEventArgs e)
		{
			return (_parent.CanResize && _expanded && e.Y >= this.Height - 
                _parent.BorderWidth - 1);
		}

        private Rectangle _buttonRectangle;
        private bool CheckExpandCollapsePosition(MouseEventArgs e)
		{
			return (_parent.CanExpandCollapse &&
                (_buttonRectangle.Contains(e.X, e.Y) ||
                (!_parent.ShowExpandCollapseButton 
                && GetHeaderRectangle().Contains(e.X, e.Y))));
		}

		private Rectangle GetHeaderRectangle()
		{
			return (new Rectangle(_parent.BorderWidth, 
                _parent.BorderWidth, Width - 2 * _parent.BorderWidth,
                _parent.HeaderHeight));
		}

		private Rectangle GetInnerRectangle()
		{
			return (new Rectangle(_parent.BorderWidth,
                _parent.BorderWidth + _parent.HeaderHeight,
                Width - 2 * _parent.BorderWidth,
                Height - _parent.BorderWidth * 3 - _parent.HeaderHeight));
		}
		
		private void DrawArrow(Graphics graphics, Rectangle rectangle, Color color, bool IsUp)
		{
			int arrowHeight = rectangle.Height - 8;
			if (arrowHeight > 5)
				arrowHeight = 5;

			int halfLeftHeight = (rectangle.Height - arrowHeight) / 2;
			int halfWidth = (rectangle.Width / 2) - 1;
			using (SolidBrush brush = new SolidBrush(color))
			{
				int upwardsOffset = IsUp ? 1 : -1;
				int currentLine = 0;
				for (int i = (upwardsOffset < 0) ? (arrowHeight - 1) : 0;
                    (upwardsOffset < 0) ? (i >= 0) : (i < arrowHeight); i += upwardsOffset)
				{
					graphics.FillRectangle(brush, (rectangle.X + halfWidth) - i,
						(rectangle.Y + halfLeftHeight) + currentLine, (i * 2) + 1, 1);
					currentLine++;
				}
			}
		}

		private GraphicsPath CreateRoundedRectPath(Rectangle rectangle, int radius)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			graphicsPath.AddLine(rectangle.Left + radius, rectangle.Top, (rectangle.Left + rectangle.Width) - (radius * 2), rectangle.Top);
			graphicsPath.AddArc((rectangle.Left + rectangle.Width) - (radius * 2), rectangle.Top, radius * 2, radius * 2, 270f, 90f);
			graphicsPath.AddLine((int) (rectangle.Left + rectangle.Width), (int) (rectangle.Top + radius), (int) (rectangle.Left + rectangle.Width),
				(int) ((rectangle.Top + rectangle.Height) - (radius * 2)));
			graphicsPath.AddArc((int) ((rectangle.Left + rectangle.Width) - (radius * 2)), (int) ((rectangle.Top + rectangle.Height) - (radius * 2)),
				(int) (radius * 2), (int) (radius * 2), (float) 0f, (float) 90f);
			graphicsPath.AddLine((int) ((rectangle.Left + rectangle.Width) - (radius * 2)), (int) (rectangle.Top + rectangle.Height),
				(int) (rectangle.Left + radius), (int) (rectangle.Top + rectangle.Height));
			graphicsPath.AddArc(rectangle.Left, (rectangle.Top + rectangle.Height) - (radius * 2), radius * 2, radius * 2, 90f, 90f);
			graphicsPath.AddLine(rectangle.Left, (rectangle.Top + rectangle.Height) - (radius * 2), rectangle.Left, rectangle.Top + radius);
			graphicsPath.AddArc(rectangle.Left, rectangle.Top, radius * 2, radius * 2, 180f, 90f);
			graphicsPath.CloseFigure();
			return graphicsPath;
		}

		private Color GetColor(Color color)
		{
			return (GetColor(color, base.Enabled));
		}

		private Color GetColor(Color color, bool enabled)
		{
			if (enabled)
				return (color);
			return (ControlPaint.LightLight(color));
		}

		private void OnHeightAnimatorAnimationFinished(object sender, EventArgs e)
		{
			if (_expanded)
				OnMenuExpanded(EventArgs.Empty);
			else
				OnMenuCollapsed(EventArgs.Empty);
		}

		private void OnContextMenuClick(object sender, EventArgs e)
		{
			if (sender == _menuExpandCollapse && !_heightAnimator.IsRunning)
				this.Expanded = !this.Expanded;
			else if (sender == _menuCollapseAll)
				_parent.CollapseAll(_parent.AnimationEnabled);
			else if (sender == _menuExpandAll)
				_parent.ExpandAll(_parent.AnimationEnabled);
		}

		#endregion

		#region Overridden from UserControl

        private bool _inResize = false;
        /// <summary>
		/// Raises the <see cref="System.Windows.Forms.Control.MouseMove"/> event.
		/// It also changes the highlighting of the button, when the mouse is moved over it
		/// and shows the appropriate resize cursor.
		/// </summary>
		/// <param name="e">Event data.</param>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			ButtonHighLighted = _buttonRectangle.Contains(e.X, e.Y) && 
                _parent.CanExpandCollapse;

			if (_inResize)
				this.Height = Math.Max(_parent.MinimumExpandedHeight, e.Y);

			if (CheckResizePosition(e) || _inResize)
				this.Cursor = Cursors.SizeNS;
			else
				this.Cursor = Cursors.Default;
		}

		/// <summary>
		/// Raises the <see cref="System.Windows.Forms.Control.MouseLeave"/> event.
		/// It also resets the button in case it is showing a resize cursor.
		/// </summary>
		/// <param name="e">Event data.</param>
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this.Cursor = Cursors.Default;
			ButtonHighLighted = false;
		}

		/// <summary>
		/// Raises the <see cref="System.Windows.Forms.Control.MouseUp"/> event.
		/// It also stops the resize operation.
		/// </summary>
		/// <param name="e">Event data.</param>
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);

			_inResize = false;
		}

		/// <summary>
		/// Raises the <see cref="System.Windows.Forms.Control.MouseDown"/> event.
		/// It also toggles the expansion when the button is clicked
		/// or starts the resize operation.
		/// </summary>
		/// <param name="e">Event data.</param>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);

			if (e.Button != MouseButtons.Left || _heightAnimator.IsRunning)
				return;

			if (CheckExpandCollapsePosition(e))
			{
				this.Expanded = !this.Expanded;
				return;
			}

			if (CheckResizePosition(e))
			{
				_inResize = true;
				this.Cursor = Cursors.SizeNS;
			}
		}

		/// <summary>
		/// Paints the control.
		/// </summary>
		/// <param name="e">Event data.</param>
		protected override void OnPaint(PaintEventArgs e)
		{			
			e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

			e.Graphics.Clear(this.BackColor);

			Rectangle headerRectangle = GetHeaderRectangle();

			if (headerRectangle.Width == 0 || headerRectangle.Height == 0)
				return;

			//fill gradient backcolor of header
			using (LinearGradientBrush brush = new LinearGradientBrush(headerRectangle, 
					   GetColor(_parent.HeaderFirstColor), GetColor(_parent.HeaderSecondColor), 
					   _parent.HeaderGradientMode))
			{
				e.Graphics.FillRectangle(brush, headerRectangle);
			}

			//draw surrounding borders
			using (Pen pen = new Pen(GetColor(_parent.BorderColor), _parent.BorderWidth))
			{
				//top
				e.Graphics.DrawLine(pen, _parent.BorderWidth * 2, (_parent.BorderWidth) / 2, 
					Width - _parent.BorderWidth * 2.5f, (_parent.BorderWidth) / 2);

				//topleft
				e.Graphics.DrawArc(pen, _parent.BorderWidth / 2f, _parent.BorderWidth / 2f, 
					_parent.BorderWidth * 4, _parent.BorderWidth * 4, 180, 90);
	
				//topright
				e.Graphics.DrawArc(pen, Width - 1 - _parent.BorderWidth * 4.5f, 
					_parent.BorderWidth / 2f, _parent.BorderWidth * 4,
					_parent.BorderWidth * 4, 270, 90);

				//bottom
				e.Graphics.DrawLine(pen, 1, Height -  2 * _parent.BorderWidth, Width - 2, 
					Height - 2 * _parent.BorderWidth);

				//left
				e.Graphics.DrawLine(pen, (_parent.BorderWidth) / 2, _parent.BorderWidth * 2f, 
					(_parent.BorderWidth) / 2, Height - 1.5f * _parent.BorderWidth - 1);
		
				//right
				e.Graphics.DrawLine(pen, Width - 1 - _parent.BorderWidth / 2, _parent.BorderWidth * 2, 
					Width - 1 - _parent.BorderWidth / 2, Height - 1.5f * _parent.BorderWidth - 1);

				//under header
				e.Graphics.DrawLine(pen, _parent.BorderWidth / 2f,
					_parent.BorderWidth + _parent.HeaderHeight, Width - _parent.BorderWidth,
					_parent.BorderWidth  + _parent.HeaderHeight);
			}

			//draw expand/collapse button
			int buttonSize = (int)(headerRectangle.Height - _parent.BorderWidth - 5);
			_buttonRectangle = new Rectangle(this.Width - _parent.BorderWidth - 5 - buttonSize, 
				_parent.BorderWidth + 2, buttonSize, buttonSize);
			if (_parent.ShowExpandCollapseButton)
			{
				using (Pen pen = new Pen(GetColor(_parent.BorderColor, Enabled && _parent.CanExpandCollapse), 1))
				{
					e.Graphics.DrawPath(pen, 
                        CreateRoundedRectPath(_buttonRectangle, 1));
				}

				if (_buttonHighLighted && Enabled)
				{
					//draw button highlighting
					using (Pen pen = new Pen(Color.FromArgb(_parent.ButtonHighLightAlpha, 
							   _parent.ButtonHighLightColor), 4))
					{
						Rectangle highLightRectangle = _buttonRectangle;
						highLightRectangle.Inflate(-2, -2);
						e.Graphics.DrawPath(pen,
                            CreateRoundedRectPath(highLightRectangle, 3));
					}
				}
	
				//draw expand/collapse arrow
				Rectangle shapeRectangle = 
                    new Rectangle(_buttonRectangle.X + 1, _buttonRectangle.Y + 1, 
					_buttonRectangle.Width - 1, _buttonRectangle.Height - 1);

				if (_heightAnimator.IsRunning || _expanded)
					DrawArrow(e.Graphics, shapeRectangle, GetColor(_parent.ButtonArrowColor, 
						Enabled && _parent.CanExpandCollapse), true);

				if (_heightAnimator.IsRunning || !_expanded)
					DrawArrow(e.Graphics, shapeRectangle, GetColor(_parent.ButtonArrowColor, 
						Enabled && _parent.CanExpandCollapse), false);			
			}

			if (_image != null && _parent.ImagesEnabled) //draw image
			{
				int x = _parent.BorderWidth * 3;
				int y = (int)(_parent.BorderWidth + _parent.HeaderHeight / 2f - _image.Height / 2f);
				if (Enabled)
					e.Graphics.DrawImageUnscaled(_image, x, y);
				else
					ControlPaint.DrawImageDisabled(e.Graphics, _image, x, y,
						GetColor(_parent.HeaderFirstColor));
			}

			if (_text != null)
			{
				//draw text
				int textX = _parent.BorderWidth * 3 + (_image == null ? 0 : _image.Width);
				Rectangle textRectangle = new Rectangle(textX, _parent.BorderWidth, 
					_buttonRectangle.Left - textX - _parent.BorderWidth,
					_parent.HeaderHeight - _parent.BorderWidth);
				if (Enabled)
				{
					using (SolidBrush solidBrush = new SolidBrush(base.ForeColor))
					{
						e.Graphics.DrawString(_text, base.Font, solidBrush,
							textRectangle, _parent.GetStringFormat());
					}
				} 
				else
				{
					ControlPaint.DrawStringDisabled(e.Graphics, _text, base.Font, 
						GetColor(base.ForeColor), textRectangle, _parent.GetStringFormat());
				}
			}
		}

		#endregion
	}
}
