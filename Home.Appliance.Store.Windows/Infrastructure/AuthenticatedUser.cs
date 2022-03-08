using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class AuthenticatedUser
    {
        public static int RunID { get; set; }

        private static AuthenticatedUser _instance;
        public static AuthenticatedUser Instance
        {
            get
            {
                return (_instance);
            }
            set
            {
                _instance = value;
            }
        }

        //private string _userSkin = string.Empty;

        //public string UserSkin
        //{
        //    get
        //    {
        //        var settings = Properties.Settings.Default;

        //        if (string.IsNullOrWhiteSpace(settings.SkinName) == false)
        //        {
        //            _userSkin =
        //                settings.SkinName;
        //        }

        //        return _userSkin;
        //    }
        //}

        //public string OrganTitle { get; set; } =
        //    Resourcesx.Captions.OrganTitle;

        private System.Collections.ArrayList _menuList =
            new System.Collections.ArrayList();//null;
        public System.Collections.ArrayList MenuList
        {
            get
            {
                //if (_menuList == null)
                //{
                //    _menuList = new System.Collections.ArrayList();
                //}
                return (_menuList);
            }
            //protected set
            // {
            //     _menuList = value;
            // }
        }

        private MenuStatus _menuStatus = null;//=new MenuStatus();//null;
        public MenuStatus MenuStatus
        {
            get
            {
                if (_menuStatus == null)
                {
                    _menuStatus = new MenuStatus();
                }
                return (_menuStatus);
            }
            protected set
            {
                _menuStatus = value;
            }
        }

        public bool IsInternetAccess { get; set; }

        #region [Propery][public virtual int UserID][DefaultValue(0)]
        private int _userID;
        [System.ComponentModel.DefaultValue(0)]
        public virtual int UserID
        {
            get
            {
                return (_userID);
            }
            protected set
            {
                _userID = value;
            }
        }
        #endregion /[Propery][public virtual int UserID][DefaultValue(0)]


        //این کد برای ذخیره در 
        //settings
        //استفاده می شود
        //    var settings = Properties.Settings.Default;
        //    settings.SkinName = UserLookAndFeel.Default.SkinName;
        //settings.Palette = UserLookAndFeel.Default.ActiveSvgPaletteName;
        //settings.Save();


        //private string[] _port = System.Configuration.ConfigurationManager.AppSettings.GetValues("PortName");
        //public string Port
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(_port[0]) == false)
        //        {
        //            return (_port[0]);
        //        }
        //        return (string.Empty);
        //    }
        //    set
        //    {
        //        _port[0] = value;
        //    }
        //}




        //public AuthenticatedUser(System.Guid userID, System.Guid roleID, string userName, string fullName, System.Guid groupID, string userSkin)
        //{
        //    UserID = userID;
        //    RoleID = roleID;
        //    GroupID = groupID;
        //    UserName = userName;
        //    FullName = fullName;
        //    //SecurityId = securityId;
        //    //OrganID = organId;
        //    UserSkin = userSkin;
        //    //if (userPicture == null)
        //    //{
        //    //    ImageConverter converter = new ImageConverter();
        //    //    UserPicture =
        //    //        (byte[])converter.ConvertTo(global::HospitalAccessControl.Properties.Resources.NoPicture, typeof(byte[]));
        //    //}
        //    //else
        //    //{
        //    //    UserPicture = userPicture;
        //    //}
        //    //if (userIp == null)
        //    //{
        //    //    string ipAddress = "127.0.0.1";
        //    //    System.Net.IPAddress address = System.Net.IPAddress.Parse(ipAddress);
        //    //    UserIP = address.GetAddressBytes();
        //    //}
        //    //else
        //    //{
        //    //    UserIP = userIp;
        //    //}
        //}

        //public AuthenticatedUser(Models.User usersRow)
        //    : this(usersRow.UserId, usersRow.RoleId, usersRow.UserName, usersRow.FullName, usersRow.GroupId, "")
        //{
        //}

    }
}
