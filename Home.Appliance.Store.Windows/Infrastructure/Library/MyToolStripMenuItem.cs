using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Infrastructure.Library
{
    public class MyToolStripMenuItem : Azx.Windows.Forms.ToolStripMenuItem
    {
        public MyToolStripMenuItem() : base()
        {
        }

        public string Caption { get; set; }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);

            if (DesignMode == false)
            {
                if (Tag != null)
                {
                    int intMenuID = int.Parse(Tag.ToString());

                    foreach (Infrastructure.MenuStatus oMenuStatus in Infrastructure.AuthenticatedUser.Instance.MenuList)
                    {
                        if (oMenuStatus.MenuID == intMenuID)
                        {
                            if (oMenuStatus.MenuIsActive == true)
                            {
                                Visible = true;
                                //if (this.DropDownItems.Count == 0)
                                //{
                                //    if (this.BackgroundImage == null)
                                //        this.BackgroundImage = global::Sport.Properties.Resources.LiquidSky1;// AuthenticatedUser.Instance.MenuBackgroundImage;
                                //}
                            }
                            else
                            {
                                Visible = false;
                            }
                            break;
                        }
                    }
                    // AuthenticatedUser.Instance.MenuList.Contains();
                }
                else
                {
                    Enabled = false;
                }
            }
        }

    }
}
