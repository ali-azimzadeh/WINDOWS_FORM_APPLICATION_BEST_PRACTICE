using Azx.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class UserLog
    {

        private static int _userID;
        public static int UserID
        {
            get
            {
                if (AuthenticatedUser.Instance != null)
                {
                    _userID = AuthenticatedUser.Instance.UserID;
                }
                return (_userID);

            }
        }

        private static string _userRole;
        public static string UserRole
        {
            get
            {
                return (_userRole);
            }
            set
            {
                _userRole = value;
            }
        }

        public static void Log(string formName, ActionLogType actionLog, string operationLog)
        {
            string actionCode = "";
            switch (actionLog)
            {
                case ActionLogType.Add: //اضافه کردن
                    {
                        actionCode = "AD";
                        break;
                    }
                case ActionLogType.Update: //اصلاح کردن
                    {
                        actionCode = "UP";
                        break;
                    }
                case ActionLogType.Delete: //حذف کردن
                    {
                        actionCode = "DE";
                        break;
                    }
                case ActionLogType.Search: // جستجو کردن
                    {
                        actionCode = "SE";
                        break;
                    }
                case ActionLogType.Print: //تهیه گزارش
                    {
                        actionCode = "PR";
                        break;
                    }
                case ActionLogType.ExitForm: // بسته شدن فرم
                    {
                        actionCode = "EF";
                        break;
                    }
                case ActionLogType.OpenForm: // باز شدن فرم
                    {
                        actionCode = "OF";
                        break;
                    }
                case ActionLogType.OpenSystem: // وارد شدن به سامانه
                    {
                        actionCode = "OS";
                        break;
                    }
                case ActionLogType.ExitSystem: //خارج شدن از سامانه
                    {
                        actionCode = "ES";
                        break;
                    }
                case ActionLogType.CardRead: //خواندن اطلاعات از کارت
                    {
                        actionCode = "CR";
                        break;
                    }
                case ActionLogType.CardWrite: // نوشتن اطلاعات بروی کارت
                    {
                        actionCode = "CW";
                        break;
                    }
                case ActionLogType.ThrowError: //رخ دادن خطا
                    {
                        actionCode = "ER";
                        break;
                    }
                case ActionLogType.Refresh:
                    {
                        actionCode = "RF";
                        break;
                    }
                case ActionLogType.LockNotFound: //قفل یافت نشد
                    {
                        actionCode = "LF";
                        break;
                    }
                case ActionLogType.Play: //پخش فایل صوتی
                    {
                        actionCode = "PL";
                        break;
                    }
                default:
                    {
                        actionCode = string.Empty;// actionLog.Substring(0, 2);
                        break;
                    }
            }
            try
            {
                string date = Azx.Windows.Forms.PersianDate.ConvertToShamsiDate(DateTime.Now).Substring(2, 8);
             //   BLL.UserLogBLL.InsertUserLog(UserID, date, DateTime.Now.ToLongTimeString(), formName, actionCode, operationLog);
            }
            catch (System.Data.SqlClient.SqlException sex)
            {
                UserLog.Log("UserLogClass", ActionLogType.ThrowError, "ErrNumber:" + sex.Number + "ErrLineNumber:" + sex.LineNumber + "ErrMessage:" + sex.Message);

                //PersianMessageBox.Show(sex.Message,
                //    global::Sport._Messages.Default.ErrorMessage007, UserSkin.SkinName);
            }
            catch (Exception ex)
            {
                UserLog.Log("UserLogClass", ActionLogType.ThrowError, "ErrMessage:" + ex.Message);

                //VPP.Extra.Windows.Forms_15.ExtraPersianMessageBox.Show(ex.Message,
                //    global::Sport._Messages.Default.ErrorMessage008, UserSkin.SkinName);
            }
        }
    }
}
