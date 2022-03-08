// Copyright 2006    SIMORGH (Co)     Tel : 44669942
// AUTHOR : Ali Azimzadeh - <alipass_2000@yahoo.com>  TEL : 09329070319
// LAST UPDATE : 2006/10/08 = 1385/07/16   TIME : 09:00 am   
// CONTROL NAME : VerticalMenu        VERSION : 1.0.5    NAMESPACE = Vpp.Windows.Forms
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
//using Vpp.Windows.Forms;

namespace Azx.Windows.Forms
{
    public partial class VerticalMenu :System.Windows.Forms.Panel // Vpp.Windows.Forms.Panel
    {
        #region Define Variables
        public Azx.Windows.Forms.ListView lvwMenu = new Azx.Windows.Forms.ListView();
        private Azx.Windows.Forms.Spliter splMenu = new Spliter();
        #endregion
        # region Constructor
        public VerticalMenu()
        {
            InitializeComponent();
            this.BorderStyle = BorderStyle.Fixed3D;
            lvwMenu.BackColor = Color.Gainsboro;
            this.Size = new Size(112, 273);
          //  splMenu.Size = new Size(3, 223);
        //    splMenu.Location = new Point(0, 0);
          //  splMenu.Dock = DockStyle.Right;
          //  splMenu.BackColor = Color.Red;
            this.Controls.Add(lvwMenu);
            this.Dock = DockStyle.Right;
            this.RightToLeft = RightToLeft.Yes;
            this.lvwMenu.RightToLeftLayout = true;
           // this.lvwMenu.Scrollable = false;
            lvwMenu.HotTracking = false;
            this.ViewItems = ViewItem.LargeIcon;
            lvwMenu.SelectedIndexChanged += new EventHandler(lvwMenu_SelectedIndexChanged);
            lvwMenu.Click += new EventHandler(lvwMenu_Click);
        }

        #endregion
        #region Define Enums
        public enum ViewItem
        {
            LargeIcon,
            Tile
        }
        #endregion
        #region VerticalMenu Properties
        //private  ListViewGroup[] _itemsGroups;
        //public ListViewGroup[] ItemsGroups
        //{
        //    get
        //    {
        //        return _itemsGroups;
        //    }
        //    set
        //    {
        //        _itemsGroups = value;
        //        lvwMenu.Groups.AddRange(_itemsGroups);
        //    }
        //}
        /// <summary>
        /// این ویژگی جهت ایجاد دکمه های گرافیکی در منو می باشد
        /// </summary>
        private GraphicalBar[] _addGraphicButton;
        [Description("این ویژگی جهت ایجاد دکمه های گرافیکی در منو می باشد")]
        [Category("VerticalMenu")]
        public GraphicalBar[] AddGraphicButton //: GraphicalBar //.GraphicalBarButtons
        {
            get
            {
                return _addGraphicButton;
            }
            set
            {
                if (_addButton == null)
                {
                    _addGraphicButton = value;
                    if (_addGraphicButton != null)
                    {
                        this.Controls.AddRange(_addGraphicButton);
                    }
                    foreach (Control ctlButton in this.Controls)
                        if (ctlButton is GraphicalBar)
                            ctlButton.Dock = DockStyle.Top;
                }
            }

        }
        /// <summary>
        /// این ویژگی جهت ایجاد دکمه های معمولی در منو می باشد
        /// </summary>
        private Azx.Windows.Forms.Button[] _addButton;
        [Description("این ویژگی جهت ایجاد دکمه های معمولی در منو می باشد")]
        [Category("VerticalMenu")]
        public Azx.Windows.Forms.Button[] AddButton
        {
            get
            {
                return _addButton;
            }
            set
            {
                if (_addGraphicButton == null)
                {
                    _addButton = value;
                    if (_addButton != null)
                        this.Controls.AddRange(_addButton);
                    foreach (Control ctlButton in this.Controls)
                        if (ctlButton is Button)
                            ctlButton.Dock = DockStyle.Top;
                }
            }
        }
        /// <summary>
        /// مربوط به تعریف آیتم های هر دکمه منو می باشد
        /// </summary>
        [Browsable(false)]
        public Azx.Windows.Forms.ListView.ListViewItemCollection Items
        {
            get
            {
                return lvwMenu.Items;
            }
        }
        /// <summary>
        /// آیکونها یا تصاویر مورد نظر که می خواهید در آیتمهای منو بکار رود را دراین قسمت قرار می گیرد
        /// </summary>
        [Category("VerticalMenu")]
        [Description("آیکونها یا تصاویر مورد نظر که می خواهید در آیتمهای منو بکار رود را دراین قسمت قرار می گیرد")]
        public System.Windows.Forms.ImageList ImageList
        {
            get
            {
                return lvwMenu.LargeImageList;
            }
            set
            {
                lvwMenu.LargeImageList = value;
            }
        }
        [Category ("VerticalMenu")]
        public System.Windows.Forms.ImageList StateImageList
        {
            get
            {
                return lvwMenu.StateImageList;
            }
            set
            {
                lvwMenu.StateImageList = value;
            }
        }
        [DefaultValue (DockStyle.Right)]
        [Category("VerticalMenu")]
        public override DockStyle Dock
        {
            get
            {
                return base.Dock;
            }
            set
            {
                base.Dock = value;
            }
        }
        /// <summary>
        ///باعث می شود هنگامی که موس بروی آیتمهای منو قرار می گیرد آیتمها بصورت لینک زیر خط دار نمایان شود 
        /// </summary>
        private bool _hotTracking = false;
        [Category ("VerticalMenu")]
        [DefaultValue (false)]
        [Description(" باعث می شود هنگامی که موس بروی آیتمهای منو قرار می گیرد آیتمها بصورت لینک زیر خط دار نمایان شود ")]
        public bool HotTrackingItems
        {
            get
            {
                return _hotTracking;
            }
            set
            {
                _hotTracking = value;
                lvwMenu.HotTracking = _hotTracking;
                if (_hotTracking)
                    HoverSelectionItems = true;
            }
        }
        /// <summary>
        ///باعث می شود هنگامی که موس بروی آیتمهای منو قرار می گیرد آن آیتم بصورت خودکار انتخاب می شود و به رنگ آبی نمایان می شود 
        /// </summary>
        private bool _hoverSelection = false;
        [Category("VerticalMenu")]
        [DefaultValue(false)]
        [Description("باعث می شود هنگامی که موس بروی آیتمهای منو قرار می گیرد آن آیتم بصورت خودکار انتخاب می شود و به رنگ آبی نمایان می شود")]
        public bool HoverSelectionItems
        {
            get
            {
                return _hoverSelection;
            }
            set
            {
                _hoverSelection = value;
                lvwMenu.HoverSelection = _hoverSelection;
            }
        }
        /// <summary>
        /// این ویژگی نوع نمایش آیتمها و آیکون را در منو تعیین می کند 
        /// باشد عنوان منو زیر آیکون مربوطه قرار می گیرد LargeIcon اگر بصورت  
        ///باشد نوشته و تصویر در کنار هم قرار می گیرند Tile واگر بصورت 
        /// </summary>
        private ViewItem _viewItems;
        [Category("VerticalMenu")]
        [DefaultValue (VerticalMenu.ViewItem.LargeIcon)]
        [Description("این ویژگی نوع نمایش آیتمها و آیکون را در منو تعیین می کند " + "باشد عنوان منو زیر آیکون مربوطه قرار می گیرد LargeIcon اگر بصورت" + "باشد نوشته و تصویر در کنار هم قرار می گیرند Tile واگر بصورت")]
        public ViewItem ViewItems
        {
            get
            {
                return _viewItems;
            }
            set
            {
                _viewItems = value;
                //lvwMenu.View =  (View) _viewItems;
                if (_viewItems == ViewItem.LargeIcon)
                {
                    lvwMenu.View = View.LargeIcon;
                    lvwMenu.LabelWrap = false;
                }
                else
                {
                    lvwMenu.View = View.Tile;
                    lvwMenu.LabelWrap = true;
                }
            }
        }
        [Category ("VerticalMenu")]
        [DefaultValue(RightToLeft.Yes)]
        public override RightToLeft RightToLeft
        {
            get
            {
                return base.RightToLeft;
            }
            set
            {
                base.RightToLeft = value;
            }
        }
        /// <summary>
        /// جهت تغییر رنگ زمینه منو بکار میرود
        /// </summary>
        [Category ("VerticalMenu")]
        [DefaultValue(typeof(Color), "Gainsboro")]
        public override Color BackColor
        {
            get
            {
                return this.lvwMenu.BackColor; // base.BackColor;
            }
            set
            {
                this.lvwMenu.BackColor = value;
            }
        }

        [Browsable(false)]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                //this.lvwMenu.Font = base.Font;
            }
        }

        private Font _itemsFont;
        public Font ItemsFont
        {
            get
            {
                return (_itemsFont);
            }
            set
            {
                _itemsFont = value;
                this.lvwMenu.Font = _itemsFont;
            }
        }

        
        #endregion
        #region VerticalMenu Events
        void lvwMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            //            throw new Exception("The method or operation is not implemented.");
            //if (lvwMenu.SelectedIndices.Count > 0)
            //    ItemClicked(lvwMenu.SelectedItems[0].Text.ToString(), e); //.Invoke();

        }
        void lvwMenu_Click(object sender, EventArgs e)
        {
            //throw new Exception("The method or operation is not implemented.");
            try
            {
                if (lvwMenu.SelectedIndices.Count > 0)
                    ItemClicked(lvwMenu.SelectedItems[0].Text.ToString(), e); //.Invoke();
            }
            catch (System.Exception ex)
            {
                PersianMessageBox.MessageFont = new Font("Tahoma", 8, FontStyle.Bold);
                PersianMessageBox.Show("کاربر گرامی لطفا رخداد مربوط به کلیک آیتمها را ایجاد کنید","هشدار", PersianMessageBox.PersianMessageBoxButton.قبول, PersianMessageBox.aPersianDefaultButoon.دكمه1, PersianMessageBox.PersianMessageBoxIcon.اخطار);
            }
        }
        protected override void OnRightToLeftChanged(EventArgs e)
        {
            base.OnRightToLeftChanged(e);
            if (this.Dock == DockStyle.Right)
                this.RightToLeft = RightToLeft.Yes;
        }

        /// <summary>
        /// با استفاده از دستورات زیر و تابع مربوطه رخدادی تعیین شده که هنگامی که کاربر بروی آیتمهای منو کلیک کرد اتفاق می افتد
        /// </summary>
        /// <param name="sender">عنوان آیتم منو را تعیین می کند</param>
        /// <param name="e"></param>
        public delegate void ItemClickEventHandler(object sender, EventArgs e);
        public event ItemClickEventHandler ItemClicked;

        private void VerticalMenu_ItemClicked(object sender, EventArgs e)
        {
            if (ItemClicked != null)
                ItemClicked.Invoke(sender, e);
        }
        #endregion
        #region VerticalMenu Metods
        /// <summary>
        /// دکمه ها کار میکند Tag این تابع وظیفه لغزاندن دکمه های گرافیکی را بر عهده دارد که بر اساس  
        /// </summary>
        /// <param name="sender">ورودی تابع دکمه های گرافیکی منو می باشد</param>
        public void FalterButton(GraphicalBar sender)
        {
            GraphicalBar clickedButton = (GraphicalBar)sender;
            int clickedButtonTag = System.Convert.ToInt32(clickedButton.Tag);
            lvwMenu.Items.Clear();
            // Send each button to top or bottom as appropriate
            foreach (Control ctl in this.Controls)
            {
                if (ctl is GraphicalBar)
                {
                    GraphicalBar btn = (GraphicalBar)ctl;
                    if (System.Convert.ToInt32(btn.Tag) > clickedButtonTag)
                    {
                        if (btn.Dock != DockStyle.Bottom)
                        {
                            btn.Dock = DockStyle.Bottom;
                            // This is vital to preserve the correct order
                            btn.BringToFront();
                        }
                    }
                    else
                    {
                        if (btn.Dock != DockStyle.Top)
                        {
                            btn.Dock = DockStyle.Top;
                            // This is vital to preserve the correct order
                            btn.BringToFront();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// دکمه ها کار میکند Tag این تابع وظیفه لغزاندن دکمه های معمولی را بر عهده دارد که بر اساس  
        /// </summary>
        /// <param name="sender">ورودی تابع دکمه های منو می باشد</param>
        public void FalterButton(Button sender)
        {
            // Get the clicked button...
            Button clickedButton = (Button)sender;
            int clickedButtonTag = System.Convert.ToInt32(clickedButton.Tag);
            lvwMenu.Items.Clear();

            // Send each button to top or bottom as appropriate
            foreach (Control ctl in this.Controls)
            {
                if (ctl is Button)
                {
                    Button btn = (Button)ctl;
                    if (System.Convert.ToInt32(btn.Tag) > clickedButtonTag)
                    {
                        if (btn.Dock != DockStyle.Bottom)
                        {
                            btn.Dock = DockStyle.Bottom;
                            // This is vital to preserve the correct order
                            btn.BringToFront();
                        }
                    }
                    else
                    {
                        if (btn.Dock != DockStyle.Top)
                        {
                            btn.Dock = DockStyle.Top;
                            // This is vital to preserve the correct order
                            btn.BringToFront();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// این تابع آیتمهای منو را ایجاد کرده و بر اساس شماره آیکونی که تعیین می شود آیتم را به همراه تصویر مربوطه نشان می دهد
        /// </summary>
        /// <param name="_Items">آرایه ای است که ستون اول را عنوان آیتم و ستون دوم را شماره تصویر یا آیکون مشخص می نماید</param>
        public void AddItems(string[,]  _Items)
        {
     //       lvwMenu.Columns.Clear();
     //       lvwMenu.Columns.Add("", 110, HorizontalAlignment.Right);
     //       lvwMenu.HeaderStyle = ColumnHeaderStyle.None;
            
            lvwMenu.Items.Clear();
//            lvwMenu.BackColor = Color.Red;
            for (int i = 0; i < _Items.Length / 2; i++)
            {
                if (_Items[i, 1] == " " || _Items[i, 1] == "")
                    _Items[i, 1] = "-1";
                lvwMenu.Items.Add(_Items[i, 0],Convert.ToInt32(_Items[i, 1]));
            }
            lvwMenu.BringToFront();
        }
        #endregion
        #region Comments
        //public class ItemClickEventArgs : MouseEventArgs
        //{
        //    public ItemClickEventArgs(ListView.SelectedListViewItemCollection item, MouseEventArgs evt)
        //        : base(evt.Button, evt.Clicks, evt.X, evt.Y, evt.Delta)
        //    {
        //        selecteditem = item;
        //    }
        //    public readonly ListView.SelectedListViewItemCollection selecteditem;
        //}
        //public ListView.SelectedListViewItemCollection _selected = null;
        //public delegate void  ItemClickEventHandler(object sender, ItemClickEventArgs e);
        //public new event Vpp.Windows.Forms.VerticalMenu.ItemClickEventHandler Click;

        //private void VerticalMenu_Click(object sender,EventArgs e)
        //{
        //               MouseEventArgs mea = (MouseEventArgs) e;
        //    ItemClickEventArgs ex = new ItemClickEventArgs(_selected,mea);
        //    if(Click != null)
        //       Click.Invoke(this, ex);
        //}
        #endregion
    }
}
