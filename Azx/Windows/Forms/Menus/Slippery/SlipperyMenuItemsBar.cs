using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Azx.Windows.Forms
{
    /// <summary>
    /// Core class of the component.
    /// It holds several <see cref="GroupPane"/>s, manages their positioning
    /// and holds all visual properties for unifying the look of all contained groups.
    /// </summary>
    public class SlipperyMenuItemsBar : ScrollableControl, ISupportInitialize
    {

        #region Fields

        private const int _defaultBorderWidth = 1;
        private const int _defaultHeaderHeight = 24;
        private const byte _defaultButtonHighLightAlpha = 70;
        private const LinearGradientMode
            _defaultHeaderLinearGradientMode = LinearGradientMode.Vertical;
        private const bool _defaultCanResize = true;
        private const bool _defaultCanExpandCollapse = true;
        private const bool _defaultShowExpandCollapseButton = true;
        private const bool _defaultImagesEnabled = true;
        private const bool _defaultAnimationEnabled = true;
        private const int _defaultAnimationIntervall = 10;
        private const double _defaultAnimationStepSize = 5;
        private const ContentAlignment _defaultTextAligment = ContentAlignment.MiddleCenter;

        private int _borderWidth = _defaultBorderWidth;
        private int _headerHeight = _defaultHeaderHeight;
        private Color _headerFirstColor;
        private Color _headerSecondColor;
        private Color _borderColor;
        private Color _buttonHighLightColor;
        private Color _buttonArrowColor;
        private byte _buttonHighLightAlpha = _defaultButtonHighLightAlpha;
        private LinearGradientMode _headerGradientMode = _defaultHeaderLinearGradientMode;
        private bool _canResize = _defaultCanResize;
        private bool _canExpandCollapse = _defaultCanExpandCollapse;
        private bool _showExpandCollapseButton = _defaultShowExpandCollapseButton;
        private bool _imagesEnabled = _defaultAnimationEnabled;
        private bool _animationEnabled = _defaultImagesEnabled;
        private int _animatorIntervall = _defaultAnimationIntervall;
        private double _animatorStepSize = _defaultAnimationStepSize;
        private int _minimumExpandedHeight = -1;

        private StringFormat _stringFormat;
        private ContentAlignment _textAlign = _defaultTextAligment;

        private BorderStyle _borderStyle = BorderStyle.FixedSingle;

        #endregion

        #region Properties

        //private int _menuBarCount = 0;
        //[Browsable(false)]
        //public int MenuBarCount
        //{
        //    get
        //    {
        //        return (_menuBarCount);
        //    }
        //    set
        //    {
        //        _menuBarCount = value;
        //       // EndInit();
        //    }
        //}

        //private SlipperyMenuItems[] _addMenuItems;//= new SlipperyMenuItems(SlipperyMenuItemsBar);
        //[Browsable(false)]
        //public SlipperyMenuItems[] AddMenuItems
        //{
        //    get
        //    {
        //        return (_addMenuItems);
        //    }
        //    set
        //    {
        //        _addMenuItems = value;
        //    }
        //}

        /// <summary>
        /// Gets whether there is currently and expand/collapse animation running.
        /// </summary>
        [Browsable(false)]
        public bool IsAnimationRunning
        {
            get
            {
                foreach (SlipperyMenuItems groupPane in this.Controls)
                    if (groupPane.IsAnimationRunning)
                        return true;

                return false;
            }
        }

        internal int MinimumExpandedHeightInternal
        {
            get 
            {
                return (_headerHeight + 6 * _borderWidth); 
            }
        }

        internal int CollapsedHeight
        {
            get 
            {
                return (_headerHeight + 3 * _borderWidth); 
            }
        }

        /// <summary>
        /// Gets the default value of the <see cref="HeaderColor1"/> property.
        /// </summary>
        protected virtual Color DefaultHeaderFirstColor
        {
            get
            {
                return (Color.BurlyWood);
            }
        }

        /// <summary>
        /// Gets the default value of the <see cref="HeaderColor2"/> property.
        /// </summary>
        protected virtual Color DefaultHeaderSecondColor
        {
            get
            {
                return (Color.OldLace);
            }
        }

        /// <summary>
        /// Gets the default value of the <see cref="BorderColor"/> property.
        /// </summary>
        protected virtual Color DefaultBorderColor
        {
            get
            {
                return (Color.Black);
            }
        }

        /// <summary>
        /// Gets the default value of the <see cref="ButtonHighlightColor"/> property.
        /// </summary>
        protected virtual Color DefaultButtonHighLightColor
        {
            get
            {
                return (Color.Red);
            }
        }

        /// <summary>
        /// Gets the default value of the <see cref="ButtonArrowColor"/> property.
        /// </summary>
        protected virtual Color DefaultButtonArrowColor
        {
            get
            {
                return (Color.Black);
            }
        }


        /// <summary>
        /// Number of contained groups.
        /// </summary>
        [Browsable(false)]
        public int Count
        {
            get
            {
                return (this.Controls.Count);
            }
        }

        /// <summary>
        /// Gets the group at the specified position.
        /// </summary>
        public SlipperyMenuItems this[int index]
        {
            get
            {
                return ((SlipperyMenuItems)this.Controls[Controls.Count - index - 1]);
            }
        }

        /// <summary>
        /// Gets or sets the type of border of the whole control.
        /// </summary>
        [DefaultValue(BorderStyle.FixedSingle), System.Runtime.InteropServices.DispId(-504)]
        [Category("SlipperyMenu")]
        [Description("The type of border of the whole control.")]
        public BorderStyle BorderStyle
        {
            get
            {
                return (_borderStyle);
            }
            set
            {
                if (_borderStyle != value)
                {
                    if (!Enum.IsDefined(typeof(BorderStyle), value))
                        throw new InvalidEnumArgumentException("value", (int)value, typeof(BorderStyle));

                    _borderStyle = value;
                    base.UpdateStyles();

                    OnBorderStyleChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets/sets the color of the arrow on the expand/collapse button.
        /// </summary>
        [Category("SlipperyMenu")]
        [Description("The color of the arrow on the expand/collapse button.")]
        public Color ButtonArrowColor
        {
            get
            {
                return (_buttonArrowColor);
            }
            set
            {
                if (_buttonArrowColor == value)
                    return;

                _buttonArrowColor = value;
                OnButtonArrowColorChanged(EventArgs.Empty);
                Invalidate(true);
            }
        }

        /// <summary>
        /// Gets/sets the alpha value of the color of the color highlighting of the expand/collapse button.
        /// </summary>
        [Category("SlipperyMenu")]
        [Description("The alpha value of the color of the color highlighting of the expand/collapse button.")]
        [DefaultValue(_defaultButtonHighLightAlpha)]
        public byte ButtonHighLightAlpha
        {
            get
            {
                return (_buttonHighLightAlpha);
            }
            set
            {
                if (_buttonHighLightAlpha == value)
                    return;

                _buttonHighLightAlpha = value;
                OnButtonHighlightAlphaChanged(EventArgs.Empty);
                Invalidate(true);
            }
        }

        /// <summary>
        /// Gets/sets the color of the highlighting of the expand/collapse button.
        /// </summary>
        [Category("SlipperyMenu")]
        [Description("The color of the highlighting of the expand/collapse button.")]
        public Color ButtonHighLightColor
        {
            get
            {
                return (_buttonHighLightColor);
            }
            set
            {
                if (_buttonHighLightColor == value)
                    return;

                _buttonHighLightColor = value;
                OnButtonHighlightColorChanged(EventArgs.Empty);
                Invalidate(true);
            }
        }

        /// <summary>
        /// Gets/sets the first color of the background of the header.
        /// </summary>
        [Category("SlipperyMenu")]
        [Description("The first color of the background of the header.")]
        public Color HeaderFirstColor
        {
            get
            {
                return (_headerFirstColor);
            }
            set
            {
                if (_headerFirstColor == value)
                    return;

                _headerFirstColor = value;
                OnHeaderColor1Changed(EventArgs.Empty);
                Invalidate(true);
            }
        }

        /// <summary>
        /// Gets/sets the second color of the background of the header.
        /// </summary>
        [Category("SlipperyMenu")]
        [Description("The second color of the background of the header.")]
        public Color HeaderSecondColor
        {
            get
            {
                return (_headerSecondColor);
            }
            set
            {
                if (_headerSecondColor == value)
                    return;

                _headerSecondColor = value;
                OnHeaderColor2Changed(EventArgs.Empty);
                Invalidate(true);
            }
        }

        /// <summary>
        /// Gets/sets the color of the borders.
        /// </summary>
        [Category("SlipperyMenu")]
        [Description("The color of the borders.")]
        public Color BorderColor
        {
            get
            {
                return (_borderColor);
            }
            set
            {
                if (_borderColor == value)
                    return;

                _borderColor = value;
                OnBorderColorChanged(EventArgs.Empty);
                Invalidate(true);
            }
        }

        /// <summary>
        /// Gets/sets how the gradient between the two header colors is painted.
        /// </summary>
        [Category("SlipperyMenu")]
        [Description("Defines how the gradient between the two header colors is painted.")]
        [DefaultValue(_defaultHeaderLinearGradientMode)]
        public LinearGradientMode HeaderGradientMode
        {
            get
            {
                return (_headerGradientMode);
            }
            set
            {
                if (_headerGradientMode == value)
                    return;

                _headerGradientMode = value;
                OnHeaderGradientModeChanged(EventArgs.Empty);
                Invalidate(true);
            }
        }

        /// <summary>
        /// Gets/sets the height of the header.
        /// </summary>
        [Category("SlipperyMenu")]
        [Description("The height of the header.")]
        [DefaultValue(_defaultHeaderHeight)]
        public int HeaderHeight
        {
            get
            {
                return (_headerHeight);
            }
            set
            {
                if (_headerHeight == value)
                    return;

                _headerHeight = value;
                OnHeaderHeightChanged(EventArgs.Empty);
                AdjustDockPadding();
                AutoCorrectMinimumExpandedHeight();
            }
        }

        /// <summary>
        /// Gets/sets the width of the borders.
        /// </summary>
        [Category("SlipperyMenu")]
        [Description("The width of the borders.")]
        [DefaultValue(_defaultBorderWidth)]
        public int BorderWidth
        {
            get
            {
                return (_borderWidth);
            }
            set
            {
                if (_borderWidth < 1 || _borderWidth > 10)
                    throw new ArgumentException("Value must be between 1 and 10.", "BorderWidth");

                if (_borderWidth == value)
                    return;

                _borderWidth = value;
                OnBorderWidthChanged(EventArgs.Empty);
                AdjustDockPadding();
                AutoCorrectMinimumExpandedHeight();
            }
        }

        /// <summary>
        /// Gets/sets whether users can resize groups or not.
        /// </summary>
        [Category("SlipperyMenu")]
        [Description("Defines whether users can resize groups or not.")]
        [DefaultValue(_defaultCanResize)]
        public bool CanResize
        {
            get
            {
                return (_canResize);
            }
            set
            {
                if (_canResize == value)
                    return;

                _canResize = value;
                OnCanResizeChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets/sets whether user can collpase/expand groups or not.
        /// </summary>
        [Category("SlipperyMenu")]
        [Description("Defines whether users can collpase/expand groups or not.")]
        [DefaultValue(_defaultCanExpandCollapse)]
        public bool CanExpandCollapse
        {
            get
            {
                return (_canExpandCollapse);
            }
            set
            {
                if (_canExpandCollapse == value)
                    return;

                _canExpandCollapse = value;
                OnCanExpandCollapseChanged(EventArgs.Empty);
                Invalidate(true);
            }
        }

        /// <summary>
        /// Gets/sets whether the collpase/expand button should be shown or not.
        /// If this property is set to false but <see cref="CanExpandCollapse"/>
        /// is set to true than the user can expand/collapse the group by
        /// simply clicking into the header.
        /// </summary>
        [Category("SlipperyMenu")]
        [Description("Defines whether the collpase/expand button should be shown or not.")]
        [DefaultValue(_defaultShowExpandCollapseButton)]
        public bool ShowExpandCollapseButton
        {
            get
            {
                return (_showExpandCollapseButton);
            }
            set
            {
                if (_showExpandCollapseButton == value)
                    return;

                _showExpandCollapseButton = value;
                OnShowExpandCollapseButtonChanged(EventArgs.Empty);
                Invalidate(true);
            }
        }

        /// <summary>
        /// Gets/sets whether images are drawn or not.
        /// </summary>
        [Category("SlipperyMenu")]
        [Description("Defines whether images are drawn or not.")]
        [DefaultValue(_defaultImagesEnabled)]
        public bool ImagesEnabled
        {
            get
            {
                return (_imagesEnabled);
            }
            set
            {
                if (_imagesEnabled == value)
                    return;

                _imagesEnabled = value;
                OnImagesEnabledChanged(EventArgs.Empty);
                Invalidate(true);
            }
        }

        /// <summary>
        /// Gets/sets whether expand/collapse animation is enabled or not.
        /// </summary>
        [Category("SlipperyMenu")]
        [Description("Defines whether expand/collapse animation is enabled or not.")]
        [DefaultValue(_defaultAnimationEnabled)]
        public bool AnimationEnabled
        {
            get
            {
                return (_animationEnabled);
            }
            set
            {
                if (_animationEnabled == value)
                    return;

                _animationEnabled = value;
                OnAnimationEnabledChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets/sets the update intervall of expand/collapse animations.
        /// </summary>
        [Category("SlipperyMenu")]
        [Description("The update intervall of expand/collapse animations.")]
        [DefaultValue(_defaultAnimationIntervall)]
        public int AnimationIntervall
        {
            get
            {
                return (_animatorIntervall);
            }
            set
            {
                if (_animatorIntervall == value)
                    return;

                _animatorIntervall = value;
                OnAnimationIntervallChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets/sets the step size of expand/collapse animations.
        /// </summary>
        [Category("Appearance")]
        [Description("The step size of expand/collapse animations.")]
        [DefaultValue(_defaultAnimationStepSize)]
        public double AnimationStepSize
        {
            get { return _animatorStepSize; }
            set
            {
                if (_animatorStepSize == value)
                    return;

                _animatorStepSize = value;
                OnAnimationStepSizeChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the alignment of the text.
        /// </summary>
        [Description("The alignment of the text.")]
        [DefaultValue(_defaultTextAligment), Category("SlipperyMenu")]
        public ContentAlignment TextAlign
        {
            get
            {
                return (_textAlign);
            }
            set
            {
                if (_textAlign == value)
                    return;

                _textAlign = value;
                ClearStringFormat();
                Invalidate(true);
                OnTextAlignChanged(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Gets or sets the minimum height the groups should have
        /// when they are expanded.
        /// Thus the user won't be able to resize it to less than this given value.
        /// </summary>
        [Description("The minimum height the groups should have when they are expanded.")]
        [DefaultValue(_defaultTextAligment), Category("SlipperyMenu")]
        public int MinimumExpandedHeight
        {
            get
            {
                return (_minimumExpandedHeight);
            }
            set
            {
                if (value < MinimumExpandedHeightInternal)
                    throw new ArgumentException("Value must not be smaller than " + MinimumExpandedHeightInternal, "MinimumExpandedHeight");

                if (_minimumExpandedHeight == value)
                    return;

                _minimumExpandedHeight = value;

                foreach (SlipperyMenuItems groupPane in this.Controls)
                {
                    if (groupPane.ExpandedHeight < _minimumExpandedHeight)
                        groupPane.ExpandedHeight = _minimumExpandedHeight;
                    if (!groupPane.Expanded)
                        groupPane.Height = CollapsedHeight;
                }

                OnMinimumExpandedHeightChanged(EventArgs.Empty);
            }
        }
        #endregion


        #region Events

        /// <summary>
        /// Event which gets fired when <see cref="TextAlign"/> has changed.
        /// </summary>
        public event EventHandler TextAlignChanged;

        /// <summary>
        /// Event which gets fired when <see cref="ButtonArrowColor"/> has changed.
        /// </summary>
        public event EventHandler ButtonArrowColorChanged;

        /// <summary>
        /// Event which gets fired when <see cref="ButtonHighlightAlpha"/> has changed.
        /// </summary>
        public event EventHandler ButtonHighLightAlphaChanged;

        /// <summary>
        /// Event which gets fired when <see cref="ButtonHighlightColor"/> has changed.
        /// </summary>
        public event EventHandler ButtonHighLightColorChanged;

        /// <summary>
        /// Event which gets fired when <see cref="HeaderColor1"/> has changed.
        /// </summary>
        public event EventHandler HeaderFirstColorChanged;

        /// <summary>
        /// Event which gets fired when <see cref="HeaderColor2"/> has changed.
        /// </summary>
        public event EventHandler HeaderSecondColorChanged;

        /// <summary>
        /// Event which gets fired when <see cref="BorderColor"/> has changed.
        /// </summary>
        public event EventHandler BorderColorChanged;

        /// <summary>
        /// Event which gets fired when <see cref="HeaderGradientMode"/> has changed.
        /// </summary>
        public event EventHandler HeaderGradientModeChanged;

        /// <summary>
        /// Event which gets fired when <see cref="HeaderHeight"/> has changed.
        /// </summary>
        public event EventHandler HeaderHeightChanged;

        /// <summary>
        /// Event which gets fired when <see cref="BorderWidth"/> has changed.
        /// </summary>
        public event EventHandler BorderWidthChanged;

        /// <summary>
        /// Event which gets fired when <see cref="CanResize"/> has changed.
        /// </summary>
        public event EventHandler CanResizeChanged;

        /// <summary>
        /// Event which gets fired when <see cref="CanExpandCollapse"/> has changed.
        /// </summary>
        public event EventHandler CanExpandCollapseChanged;

        /// <summary>
        /// Event which gets fired when <see cref="ShowExpandCollapseButton"/> has changed.
        /// </summary>
        public event EventHandler ShowExpandCollapseButtonChanged;

        /// <summary>
        /// Event which gets fired when <see cref="ImagesEnabled"/> has changed.
        /// </summary>
        public event EventHandler ImagesEnabledChanged;

        /// <summary>
        /// Event which gets fired when <see cref="AnimationEnabled"/> has changed.
        /// </summary>
        public event EventHandler AnimationEnabledChanged;

        /// <summary>
        /// Event which gets fired when <see cref="AnimationIntervall"/> has changed.
        /// </summary>
        public event EventHandler AnimationIntervallChanged;

        /// <summary>
        /// Event which gets fired when <see cref="AnimationStepSize"/> has changed.
        /// </summary>
        public event EventHandler AnimationStepSizeChanged;

        /// <summary>
        /// Event which gets fired when <see cref="MinimumExpandedHeight"/> has changed.
        /// </summary>
        public event EventHandler MinimumExpandedHeightChanged;

        /// <summary>
        /// Event which gets fired when <see cref="BorderStyle"/> has changed.
        /// </summary>
        public event EventHandler BorderStyleChanged;

        /// <summary>
        /// Event which gets fired when a <see cref="GroupPane"/> has been added.
        /// </summary>
        public event SlipperyMenuItemsEventHandler SlipperyMenuItemsAdded;

        /// <summary>
        /// Event which gets fired when a <see cref="GroupPane"/> has been removed.
        /// </summary>
        public event SlipperyMenuItemsEventHandler SlipperyMenuItemsRemoved;

        /// <summary>
        /// Event which gets fired before a <see cref="GroupPane"/> is collapsed.
        /// </summary>
        public event SlipperyMenuItemsCancelEventHandler SlipperyMenuItemsCollapsing;

        /// <summary>
        /// Event which gets fired after a <see cref="GroupPane"/> has collapsed.
        /// </summary>
        public event SlipperyMenuItemsEventHandler SlipperyMenuItemsCollapsed;

        /// <summary>
        /// Event which gets fired before a <see cref="GroupPane"/> is expanded.
        /// </summary>
        public event SlipperyMenuItemsCancelEventHandler SlipperyMenuItemsExpanding;

        /// <summary>
        /// Event which gets fired after a <see cref="GroupPane"/> has expanded.
        /// </summary>
        public event SlipperyMenuItemsEventHandler SlipperyMenuItemsExpanded;

        #endregion


        #region Constructor

        /// <summary>
        /// Creates a new empty instance.
        /// </summary>
        public SlipperyMenuItemsBar()
        {
            base.DockPadding.All = 1;

            _headerFirstColor = DefaultHeaderFirstColor;
            _headerSecondColor = DefaultHeaderSecondColor;
            _borderColor = DefaultBorderColor;
            _buttonHighLightColor = DefaultButtonHighLightColor;
            _buttonArrowColor = DefaultButtonArrowColor;

            this.AutoScroll = true;
            AutoCorrectMinimumExpandedHeight();
        }

        #endregion


        #region Create/Add/Remove/Get GroupPanes

        /// <summary>
        /// Adds a new <see cref="GroupPane"/> to the end of the list.
        /// </summary>
        /// <param name="control">Element which should initially beend placed in the new group.</param>
        /// <param name="text">Initial text of the new group.</param>
        /// <param name="image">Initial image of the new group.</param>
        /// <returns>The newly created <see cref="GroupPane"/>.</returns>
        public SlipperyMenuItems Add(Control control, string text, Image image)
        {
            return Add(control, text, image, false);
        }

        /// <summary>
        /// Adds a new <see cref="GroupPane"/> to the end of the list.
        /// </summary>
        /// <param name="control">Element which should initially beend placed in the new group.</param>
        /// <param name="text">Initial text of the new group.</param>
        /// <param name="image">Initial image of the new group.</param>
        /// <param name="adjustGroupPaneHeightToControlheight">Sets whether the expanded height of 
        /// the resulting group pane should match the height of the given control.</param>
        /// <returns>The newly created <see cref="GroupPane"/>.</returns>
        public SlipperyMenuItems Add(Control control, string text, Image image, bool adjustGroupPaneHeightToControlheight)
        {
            int controlHeight = control == null ? 0 : control.Height;

            SlipperyMenuItems result = CreateSlipperyMenuItems(control, text, image);
            if (adjustGroupPaneHeightToControlheight)
                result.ExpandedHeight = controlHeight + result.DockPadding.Top + result.DockPadding.Bottom;
            Add(result);
            return result;
        }

        /// <summary>
        /// Adds a <see cref="GroupPane"/> to the end of the list.
        /// Note that this group must have been created via the <see cref="CreateGroupPane"/> function.
        /// </summary>
        /// <param name="groupPane">Group to be added.</param>
        public void Add(SlipperyMenuItems slipperyMenuItems)
        {
            this.InsertAt(Controls.Count, slipperyMenuItems);

            slipperyMenuItems.MenuCollapsing += new CancelEventHandler(OnSlipperyMenuItemsCollapsing);
            slipperyMenuItems.MenuCollapsed += new EventHandler(OnSlipperyMenuItemsCollapsed);
            slipperyMenuItems.MenuExpanding += new CancelEventHandler(OnSlipperyMenuItemsExpanding);
            slipperyMenuItems.MenuExpanded += new EventHandler(OnSlipperyMenuItemsExpanded);
        }

        /// <summary>
        /// Removes a <see cref="GroupPane"/> from the list.
        /// </summary>
        /// <param name="groupPane">Group to be removed.</param>
        public void Remove(SlipperyMenuItems slipperyMenuItems)
        {
            this.Controls.Remove(slipperyMenuItems);

            slipperyMenuItems.MenuCollapsing -= new CancelEventHandler(OnSlipperyMenuItemsCollapsing);
            slipperyMenuItems.MenuCollapsed -= new EventHandler(OnSlipperyMenuItemsCollapsed);
            slipperyMenuItems.MenuExpanding -= new CancelEventHandler(OnSlipperyMenuItemsExpanding);
            slipperyMenuItems.MenuExpanded -= new EventHandler(OnSlipperyMenuItemsExpanded);

            OnSlipperyMenuItemsRemoved(new SlipperyMenuItemsEventArgs(slipperyMenuItems));
        }

        /// <summary>
        /// Removes the <see cref="GroupPane"/> at the specified index. 
        /// </summary>
        /// <param name="index">Index of the group to be removed.</param>
        public void RemoveAt(int index)
        {
            this.Remove(this[index]);
        }


        /// <summary>
        /// Removes all groups.
        /// </summary>
        public void Clear()
        {
            while (this.Controls.Count > 0)
                RemoveAt(0);
        }

        /// <summary>
        /// Inserts a <see cref="GroupPane"/> at a specified position.
        /// Note that this group must have been created via the <see cref="CreateGroupPane"/> function.
        /// </summary>
        /// <param name="index">Insertion index.</param>
        /// <param name="groupPane">Group to be added.</param>
        public void InsertAt(int index, SlipperyMenuItems slipperyMenuItems)
        {
            if (slipperyMenuItems.Parent != null)
                throw new ArgumentException("GroupPane already belongs to another container.", "groupPane");

            if (slipperyMenuItems.ParentBar != this)
                throw new ArgumentException("GroupPane was not created by this instance.", "groupPane");

            index = Controls.Count - index;
            slipperyMenuItems.Dock = DockStyle.Top;
            Controls.Add(slipperyMenuItems);
            Controls.SetChildIndex(slipperyMenuItems, index);

            OnSlipperyMenuItemsAdded(new SlipperyMenuItemsEventArgs(slipperyMenuItems));
        }

        /// <summary>
        /// Creates a new empty <see cref="GroupPane"/> which is valid for being added
        /// to this instance.
        /// </summary>
        /// <returns>Newly created <see cref="GroupPane"/>.</returns>
        public SlipperyMenuItems CreateSlipperyMenuItems()
        {
            return CreateSlipperyMenuItems(null, "", null);
        }

        /// <summary>
        /// Creates a new <see cref="GroupPane"/> which is valid for being added
        /// to this instance.
        /// </summary>
        /// <param name="control">Element which should initially beend placed in the new group.</param>
        /// <param name="text">Initial text of the new group.</param>
        /// <param name="image">Initial image of the new group.</param>
        /// <returns>Newly created <see cref="GroupPane"/>.</returns>
        public SlipperyMenuItems CreateSlipperyMenuItems(Control control, string text, Image image)
        {
            SlipperyMenuItems result = new  SlipperyMenuItems(this);

            result.Control = control;
            result.Text = text;
            result.Image = image;

            return result;
        }

        #endregion

        #region Collapse/Expand

        /// <summary>
        /// Collapses all contained groups immediatly.
        /// </summary>
        public void CollapseAll()
        {
            CollapseAll(false);
        }

        /// <summary>
        /// Collapses all contained groups.
        /// </summary>
        /// <param name="animate">Indicates whether the collapse should be animated.</param>
        public void CollapseAll(bool animate)
        {
            foreach (SlipperyMenuItems groupPane in this.Controls)
                groupPane.Collapse(animate);
        }

        /// <summary>
        /// Expands all contained groups immediatly.
        /// </summary>
        public void ExpandAll()
        {
            ExpandAll(false);
        }

        /// <summary>
        /// Expands all contained groups.
        /// </summary>
        /// <param name="animate">Indicates whether the expand should be animated.</param>
        public void ExpandAll(bool animate)
        {
            foreach (SlipperyMenuItems groupPane in this.Controls)
                groupPane.Expand(animate);
        }

        #endregion


        #region Internal Method

        internal StringFormat GetStringFormat()
        {
            if (_stringFormat == null)
                _stringFormat = CreateStringFormat();
            return _stringFormat;
        }

        #endregion

        #region Protected interface

        #region ShouldSerialize

        /// <summary>
        /// Indicates the designer whether <see cref="ButtonArrowColor"/> needs
        /// to be serialized.
        /// </summary>
        protected virtual bool ShouldSerializeButtonArrowColor()
        {
            return (!_buttonArrowColor.Equals(DefaultButtonArrowColor));
        }

        /// <summary>
        /// Indicates the designer whether <see cref="ButtonHighlightColor"/> needs
        /// to be serialized.
        /// </summary>
        protected virtual bool ShouldSerializeButtonHighlightColor()
        {
            return (!_buttonHighLightColor.Equals(DefaultButtonHighLightColor));
        }

        /// <summary>
        /// Indicates the designer whether <see cref="HeaderColor1"/> needs
        /// to be serialized.
        /// </summary>
        protected virtual bool ShouldSerializeHeaderColor1()
        {
            return (!_headerFirstColor.Equals(DefaultHeaderFirstColor));
        }

        /// <summary>
        /// Indicates the designer whether <see cref="HeaderColor2"/> needs
        /// to be serialized.
        /// </summary>
        protected virtual bool ShouldSerializeHeaderColor2()
        {
            return (!_headerSecondColor.Equals(DefaultHeaderSecondColor));
        }

        /// <summary>
        /// Indicates the designer whether <see cref="BorderColor"/> needs
        /// to be serialized.
        /// </summary>
        protected virtual bool ShouldSerializeBorderColor()
        {
            return (!_borderColor.Equals(DefaultBorderColor));
        }

        #endregion

        #region Eventraisers

        /// <summary>
        /// Raises the <see cref="TextAlignChanged"/> event.
        /// </summary>
        /// <param name="eventArgs">Event data.</param>
        protected void OnTextAlignChanged(EventArgs eventArgs)
        {
            if (TextAlignChanged != null)
                TextAlignChanged(this, eventArgs);
        }

        /// <summary>
        /// Raises the <see cref="CanResizeChanged"/> event.
        /// </summary>
        /// <param name="eventArgs">Event data.</param>
        protected void OnCanResizeChanged(EventArgs eventArgs)
        {
            if (CanResizeChanged != null)
                CanResizeChanged(this, eventArgs);
        }

        /// <summary>
        /// Raises the <see cref="CanExpandCollapseChanged"/> event.
        /// </summary>
        /// <param name="eventArgs">Event data.</param>
        protected void OnCanExpandCollapseChanged(EventArgs eventArgs)
        {
            if (CanExpandCollapseChanged != null)
                CanExpandCollapseChanged(this, eventArgs);
        }

        /// <summary>
        /// Raises the <see cref="ShowExpandCollapseButtonChanged"/> event.
        /// </summary>
        /// <param name="eventArgs">Event data.</param>
        protected void OnShowExpandCollapseButtonChanged(EventArgs eventArgs)
        {
            if (ShowExpandCollapseButtonChanged != null)
                ShowExpandCollapseButtonChanged(this, eventArgs);
        }

        /// <summary>
        /// Raises the <see cref="ImagesEnabledChanged"/> event.
        /// </summary>
        /// <param name="eventArgs">Event data.</param>
        protected void OnImagesEnabledChanged(EventArgs eventArgs)
        {
            if (ImagesEnabledChanged != null)
                ImagesEnabledChanged(this, eventArgs);
        }

        /// <summary>
        /// Raises the <see cref="AnimationEnabledChanged"/> event.
        /// </summary>
        /// <param name="eventArgs">Event data.</param>
        protected void OnAnimationEnabledChanged(EventArgs eventArgs)
        {
            if (AnimationEnabledChanged != null)
                AnimationEnabledChanged(this, eventArgs);
        }

        /// <summary>
        /// Raises the <see cref="AnimationIntervallChanged"/> event.
        /// </summary>
        /// <param name="eventArgs">Event data.</param>
        protected void OnAnimationIntervallChanged(EventArgs eventArgs)
        {
            if (AnimationIntervallChanged != null)
                AnimationIntervallChanged(this, eventArgs);
        }

        /// <summary>
        /// Raises the <see cref="AnimationStepSizeChanged"/> event.
        /// </summary>
        /// <param name="eventArgs">Event data.</param>
        protected void OnAnimationStepSizeChanged(EventArgs eventArgs)
        {
            if (AnimationStepSizeChanged != null)
                AnimationStepSizeChanged(this, eventArgs);
        }

        /// <summary>
        /// Raises the <see cref="BorderColorChanged"/> event.
        /// </summary>
        /// <param name="eventArgs">Event data.</param>
        protected void OnBorderColorChanged(EventArgs eventArgs)
        {
            if (BorderColorChanged != null)
                BorderColorChanged(this, eventArgs);
        }

        /// <summary>
        /// Raises the <see cref="BorderWidthChanged"/> event.
        /// </summary>
        /// <param name="eventArgs">Event data.</param>
        protected void OnBorderWidthChanged(EventArgs eventArgs)
        {
            if (BorderWidthChanged != null)
                BorderWidthChanged(this, eventArgs);
        }

        /// <summary>
        /// Raises the <see cref="ButtonArrowColorChanged"/> event.
        /// </summary>
        /// <param name="eventArgs">Event data.</param>
        protected void OnButtonArrowColorChanged(EventArgs eventArgs)
        {
            if (ButtonArrowColorChanged != null)
                ButtonArrowColorChanged(this, eventArgs);
        }

        /// <summary>
        /// Raises the <see cref="ButtonHighlightAlphaChanged"/> event.
        /// </summary>
        /// <param name="eventArgs">Event data.</param>
        protected void OnButtonHighlightAlphaChanged(EventArgs eventArgs)
        {
            if (ButtonHighLightAlphaChanged != null)
                ButtonHighLightAlphaChanged(this, eventArgs);
        }

        /// <summary>
        /// Raises the <see cref="ButtonHighlightColorChanged"/> event.
        /// </summary>
        /// <param name="eventArgs">Event data.</param>
        protected void OnButtonHighlightColorChanged(EventArgs eventArgs)
        {
            if (ButtonHighLightColorChanged != null)
                ButtonHighLightColorChanged(this, eventArgs);
        }

        /// <summary>
        /// Raises the <see cref="HeaderColor1Changed"/> event.
        /// </summary>
        /// <param name="eventArgs">Event data.</param>
        protected void OnHeaderColor1Changed(EventArgs eventArgs)
        {
            if (HeaderFirstColorChanged != null)
                HeaderFirstColorChanged(this, eventArgs);
        }

        /// <summary>
        /// Raises the <see cref="HeaderColor2Changed"/> event.
        /// </summary>
        /// <param name="eventArgs">Event data.</param>
        protected void OnHeaderColor2Changed(EventArgs eventArgs)
        {
            if (HeaderSecondColorChanged != null)
                HeaderSecondColorChanged(this, eventArgs);
        }

        /// <summary>
        /// Raises the <see cref="HeaderGradientModeChanged"/> event.
        /// </summary>
        /// <param name="eventArgs">Event data.</param>
        protected void OnHeaderGradientModeChanged(EventArgs eventArgs)
        {
            if (HeaderGradientModeChanged != null)
                HeaderGradientModeChanged(this, eventArgs);
        }

        /// <summary>
        /// Raises the <see cref="HeaderHeightChanged"/> event.
        /// </summary>
        /// <param name="eventArgs">Event data.</param>
        protected void OnHeaderHeightChanged(EventArgs eventArgs)
        {
            if (HeaderHeightChanged != null)
                HeaderHeightChanged(this, eventArgs);
        }

        /// <summary>
        /// Raises the <see cref="MinimumExpandedHeightChanged"/> event.
        /// </summary>
        /// <param name="eventArgs">Event data.</param>
        protected void OnMinimumExpandedHeightChanged(EventArgs eventArgs)
        {
            if (MinimumExpandedHeightChanged != null)
                MinimumExpandedHeightChanged(this, eventArgs);
        }

        /// <summary>
        /// Raises the <see cref="BorderStyleChanged"/> event.
        /// </summary>
        /// <param name="eventArgs">Event data.</param>
        protected void OnBorderStyleChanged(EventArgs eventArgs)
        {
            if (BorderStyleChanged != null)
                BorderStyleChanged(this, eventArgs);
        }

        /// <summary>
        /// Raises the <see cref="GroupPaneAdded"/> event.
        /// </summary>
        /// <param name="eventArgs">Event data.</param>
        protected void OnSlipperyMenuItemsAdded(SlipperyMenuItemsEventArgs eventArgs)
        {
            if (SlipperyMenuItemsAdded != null)
                SlipperyMenuItemsAdded(this, eventArgs);
        }

        /// <summary>
        /// Raises the <see cref="GroupPaneRemoved"/> event.
        /// </summary>
        /// <param name="eventArgs">Event data.</param>
        protected void OnSlipperyMenuItemsRemoved(SlipperyMenuItemsEventArgs eventArgs)
        {
            if (SlipperyMenuItemsRemoved != null)
                SlipperyMenuItemsRemoved(this, eventArgs);
        }

        /// <summary>
        /// Raises the <see cref="GroupPaneCollapsing"/> event.
        /// </summary>
        /// <param name="eventArgs">Event data.</param>
        protected virtual void OnSlipperyMenuItemsCollapsing(SlipperyMenuItemsCancelEventArgs eventArgs)
        {
            if (SlipperyMenuItemsCollapsing != null)
                SlipperyMenuItemsCollapsing(this, eventArgs);
        }

        /// <summary>
        /// Raises the <see cref="GroupPaneCollapsed"/> event.
        /// </summary>
        /// <param name="eventArgs">Event data.</param>
        protected virtual void OnSlipperyMenuItemsCollapsed(SlipperyMenuItemsEventArgs eventArgs)
        {
            if (SlipperyMenuItemsCollapsed != null)
                SlipperyMenuItemsCollapsed(this, eventArgs);

            this.PerformLayout(null, "");
        }

        /// <summary>
        /// Raises the <see cref="GroupPaneExpanding"/> event.
        /// </summary>
        /// <param name="eventArgs">Event data.</param>
        protected virtual void OnSlipperyMenuItemsExpanding(SlipperyMenuItemsCancelEventArgs eventArgs)
        {
            if (SlipperyMenuItemsExpanding != null)
                SlipperyMenuItemsExpanding(this, eventArgs);
        }

        /// <summary>
        /// Raises the <see cref="GroupPaneExpanded"/> event.
        /// </summary>
        /// <param name="eventArgs">Event data.</param>
        protected virtual void OnSlipperyMenuItemsExpanded(SlipperyMenuItemsEventArgs eventArgs)
        {
            if (SlipperyMenuItemsExpanded != null)
                SlipperyMenuItemsExpanded(this, eventArgs);

            this.PerformLayout(null, "");
        }

        #endregion

        #endregion

        #region Privates Methods

        private void AutoCorrectMinimumExpandedHeight()
        {
            if (MinimumExpandedHeight < MinimumExpandedHeightInternal)
                MinimumExpandedHeight = MinimumExpandedHeightInternal;
        }

        private void ClearStringFormat()
        {
            if (_stringFormat != null)
            {
                _stringFormat.Dispose();
                _stringFormat = null;
            }
        }

        private StringFormat CreateStringFormat()
        {
            StringFormat stringFormat = new StringFormat();

            if ((this.TextAlign & (ContentAlignment.BottomRight |
                ContentAlignment.MiddleRight | ContentAlignment.TopRight))
                != (ContentAlignment)0)
                stringFormat.Alignment = StringAlignment.Far;
            else if ((this.TextAlign & (ContentAlignment.BottomCenter |
                ContentAlignment.MiddleCenter | ContentAlignment.TopCenter))
                != (ContentAlignment)0)
                stringFormat.Alignment = StringAlignment.Center;
            else
                stringFormat.Alignment = StringAlignment.Near;

            if ((this.TextAlign & (ContentAlignment.BottomRight |
                ContentAlignment.BottomCenter | ContentAlignment.BottomLeft))
                != (ContentAlignment)0)
                stringFormat.LineAlignment = StringAlignment.Far;
            else if ((this.TextAlign & (ContentAlignment.MiddleCenter |
                ContentAlignment.MiddleLeft | ContentAlignment.MiddleRight))
                != (ContentAlignment)0)
                stringFormat.LineAlignment = StringAlignment.Center;
            else
                stringFormat.LineAlignment = StringAlignment.Near;

            if (this.RightToLeft == RightToLeft.Yes)
                stringFormat.FormatFlags |= StringFormatFlags.DirectionRightToLeft;

            return stringFormat;
        }

        private void AdjustDockPadding()
        {
            foreach (Control control in Controls)
            {
                if (control as SlipperyMenuItems != null)
                    (control as SlipperyMenuItems).AdjustDockPadding();
            }
        }
#endregion

        #region EventHandlers

        private void OnSlipperyMenuItemsCollapsing(object sender, CancelEventArgs e)
        {
            OnSlipperyMenuItemsCollapsing(new SlipperyMenuItemsCancelEventArgs((SlipperyMenuItems)sender));
        }

        private void OnSlipperyMenuItemsCollapsed(object sender, EventArgs e)
        {
            OnSlipperyMenuItemsCollapsed(new SlipperyMenuItemsEventArgs((SlipperyMenuItems)sender));
        }

        private void OnSlipperyMenuItemsExpanding(object sender, CancelEventArgs e)
        {
            OnSlipperyMenuItemsExpanding(new SlipperyMenuItemsCancelEventArgs((SlipperyMenuItems)sender));
        }

        private void OnSlipperyMenuItemsExpanded(object sender, EventArgs e)
        {
            OnSlipperyMenuItemsExpanded(new SlipperyMenuItemsEventArgs((SlipperyMenuItems)sender));
        }

        #endregion


        #region Overriden from Panel

        /// <summary>
        /// Reinitializes the internal string format of the text.
        /// </summary>
        /// <param name="e">Event data.</param>
        protected override void OnRightToLeftChanged(EventArgs e)
        {
            base.OnRightToLeftChanged(e);
            ClearStringFormat();
        }

        /// <summary>
        /// Adjusts the scrollbars.
        /// Disabled while an animation is running.
        /// </summary>
        /// <param name="displayScrollbars">Sets wether the scrollbars should be visible or not.</param>
        protected override void AdjustFormScrollbars(bool displayScrollbars)
        {
            if (!IsAnimationRunning)
                base.AdjustFormScrollbars(displayScrollbars);
        }

        /// <summary>
        /// Gets the paramters for the control.
        /// </summary>
        protected override CreateParams CreateParams
        {
            [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.LinkDemand, UnmanagedCode = true)]
            get
            {
                CreateParams parameters = base.CreateParams;

                switch (_borderStyle)
                {
                    case BorderStyle.FixedSingle:
                        {
                            parameters.Style |= 0x800000;
                            break;
                        }
                    case BorderStyle.Fixed3D:
                        {
                            parameters.ExStyle |= 0x200;
                            break;
                        }
                }

                return parameters;
            }
        }

        #endregion

        #region ISupportInitialize Member

        /// <summary>
        /// Signals the object the start of the initialization process.
        /// </summary>
        public void BeginInit() 
        {
        }

        /// <summary>
        /// Signals the object the end of the initialization process.
        /// Adds some dummy groups at design time (just a visual gimmick).
        /// </summary>
        public void EndInit()
        {
//            if (base.DesignMode)
  //          {
          
                //for (int i = 0; i < MenuBarCount; i++)
                //{
                //    Panel panel = new Panel();
                //    panel.BackColor = Color.Transparent;
                //    Add(panel, "MenuItem " + i, null);
                //}
    //        }
        }

        public Panel CreateMenuItems(string[] MenuItemsText,string[] MenuItemsName, Control control,ImageList imageList)
        {
            Panel pnlMenuItems = new Panel();
            LinkLabel ll = new LinkLabel();
            for (int index = MenuItemsText.Length; index >= 1; index--)
            {
                if (control.GetType() == typeof(System.Windows.Forms.Label))
                {
                    System.Windows.Forms.Label lable = new System.Windows.Forms.Label();
                    control = lable;
                }
                else if (control.GetType() == typeof(Azx.Windows.Forms.Label))
                {
                    Azx.Windows.Forms.Label lable = new Azx.Windows.Forms.Label();
                    control = lable;
                }
                else if (control.GetType() == typeof(Azx.Windows.Forms.LinkLable))
                {
                    Azx.Windows.Forms.LinkLable linkLabel = new LinkLable();
                    control = linkLabel;
                }
                else if (control.GetType() == typeof(System.Windows.Forms.LinkLabel))
                {
                    System.Windows.Forms.LinkLabel linkLabel = new  LinkLabel();
                    control = linkLabel;
                }
                else if (control.GetType() == typeof(System.Windows.Forms.Button))
                {
                    System.Windows.Forms.Button button = new System.Windows.Forms.Button();
                    control = button;
                }
                else if (control.GetType() == typeof(Azx.Windows.Forms.Button))
                {
                    Azx.Windows.Forms.Button button = new Button();
                    control = button;
                }
                else if (control.GetType() == typeof(Azx.Windows.Forms.BitmapButton))
                {
                    Azx.Windows.Forms.BitmapButton bitmapButton = new BitmapButton();
                    control = bitmapButton;
                }
                else if (control.GetType() == typeof(Azx.Windows.Forms.GrowIcon))
                {
                    Azx.Windows.Forms.GrowIcon growIcon = new GrowIcon();
                    growIcon.Name = "growIcon" + MenuItemsName[index - 1];
                    growIcon.Text = MenuItemsText[index - 1];
                    growIcon.TooltipText = MenuItemsText[index - 1];
                    growIcon.TextColor = Brushes.Red;
                    growIcon.Icon = (Bitmap)imageList.Images[index-1];
                    growIcon.BackColor = this.BackColor;
                    control = growIcon;
                }
                control.Dock = DockStyle.Top;
                //control.TextAlign = ContentAlignment.MiddleCenter;
                control.Text = MenuItemsText[index-1];
                pnlMenuItems.Controls.Add(control);
                //control.Height += 7;
                control.Click += new EventHandler(control_Click);
//                ll.LinkClicked += new LinkLabelLinkClickedEventHandler(ll_LinkClicked);
            }
            pnlMenuItems.Height = pnlMenuItems.Controls[0].Bottom;
            return pnlMenuItems;
        }

        private void control_Click(object sender, EventArgs e)
        {
            //Vpp.Windows.Forms.GrowIcon cur =
            //MessageBox.Show(((Vpp.Windows.Forms.GrowIcon)sender).Name.ToString());
            OnMenuItemClick(new SlipperyMenuItemsClickEventArgs(((Azx.Windows.Forms.GrowIcon)sender).Name));
        }

        public delegate void SlipperyMenuItemsClickEventHandler(object sender,  SlipperyMenuItemsClickEventArgs  e);
        public class SlipperyMenuItemsClickEventArgs : EventArgs
        {
            private string _menuItemName;
            public string MenuItemName
            {
                get
                {
                    return (_menuItemName);
                }
            }
            public SlipperyMenuItemsClickEventArgs(string menuItemName)
            {
                _menuItemName = menuItemName;
            }
        }

        public event SlipperyMenuItemsClickEventHandler MenuItemClick;
        protected virtual void OnMenuItemClick(SlipperyMenuItemsClickEventArgs e)
        {
            if (MenuItemClick != null)
                MenuItemClick(this, e);
        }

        #endregion

        /// <summary>
        /// Delegate for the events with <see cref="GroupPaneEventArgs"/>:
        /// </summary>
        public delegate void SlipperyMenuItemsEventHandler(object sender, SlipperyMenuItemsEventArgs eventArgs);

        /// <summary>
        /// Class with event data holding a <see cref="GroupPane"/>.
        /// </summary>
        public class SlipperyMenuItemsEventArgs : EventArgs
        {

            private  SlipperyMenuItems __slipperyMenuItems;

            /// <summary>
            /// Creates a new instance.
            /// </summary>
            /// <param name="groupPane"><see cref="	GroupPane"/> associated with this event.</param>
            public SlipperyMenuItemsEventArgs(SlipperyMenuItems slipperyMenuItems)
            {
                __slipperyMenuItems = slipperyMenuItems;
            }



            /// <summary>
            /// <see cref="	GroupPane"/> associated with this event.
            /// </summary>
            public SlipperyMenuItems SlipperyMenuItems
            {
                get
                {
                    return (__slipperyMenuItems);
                }
            }
        }
        /// <summary>
        /// Delegate for the events with <see cref="GroupPaneCancelEventArgs"/>:
        /// </summary>
        public delegate void SlipperyMenuItemsCancelEventHandler(object sender, SlipperyMenuItemsCancelEventArgs eventArgs);

        /// <summary>
        /// Class with event data holding a <see cref="GroupPane"/> which can be cancelled.
        /// </summary>
        public class SlipperyMenuItemsCancelEventArgs : SlipperyMenuItemsEventArgs
        {

            /// <summary>
            /// Creates a new instance.
            /// </summary>
            /// <param name="groupPane"><see cref="	GroupPane"/> associated with this event.</param>
            public SlipperyMenuItemsCancelEventArgs(SlipperyMenuItems slipperyMenuItems)
                : base(slipperyMenuItems)
            {
                _cancel = false;
            }

            private bool _cancel;
            /// <summary>
            /// Gets/sets whether the operation in process should be cancelled.
            /// </summary>
            public bool Cancel
            {
                get 
                {
                    return (_cancel); 
                }
                set 
                {
                    _cancel = value; 
                }
            }
        }
    }
}