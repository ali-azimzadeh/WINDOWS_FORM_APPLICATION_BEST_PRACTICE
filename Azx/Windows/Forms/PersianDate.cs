using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azx.Windows.Forms
{
    public enum CompareMode
    {
        Equal = 0,
        Greater = 1,
        Less = -1,
        None
    }
    public class PersianDate : object
    {
        public PersianDate() : base()
        {
        }

        private static System.Globalization.PersianCalendar _persianCalender =
            new System.Globalization.PersianCalendar();

        private static string strResult = null;
        private static int intResult = 0;

        /// <summary>
        /// تاريخ ميلادی را به شمسی تبديل می کند
        /// </summary>
        /// <param name="_dateTime">تاريخ ميلادی</param>
        /// <returns></returns>
        public static string ConvertToShamsiDate(string miladi_date)
        {
            DateTime _dateTime = Convert.ToDateTime(miladi_date);//).ToShortDateString());
            return (ConvertYear(_dateTime) + "/" + ConvertMonth(_dateTime) + "/" + ConvertDay(_dateTime));
        }

        /// <summary>
        /// تاريخ ميلادی را به شمسی تبديل می کند
        /// </summary>
        /// <param name="_dateTime">تاريخ سيستم</param>
        /// <returns></returns>
        public static string ConvertToShamsiDate(DateTime _dateTime)
        {
            return (ConvertYear(_dateTime) + "/" + ConvertMonth(_dateTime) + "/" + ConvertDay(_dateTime));
        }

        /// <summary>
        /// اين تابع عدد مورد نظر را به روز اضافه می کند
        /// </summary>
        /// <param name="time">تاريخ مورد نظر</param>
        /// <param name="days">عددی که می خواهید به روز اضافه شود</param>
        /// <returns>نتيجه را بطور کامل بصورت رشته برمی گرداند</returns>
        public static string AddDays(DateTime date, int days)
        {
            string strDays = ConvertToShamsiDate(date);
            return (AddDays(strDays, days));
        }

        /// <summary>
        /// اين تابع عدد مورد نظر را به روز اضافه می کند
        /// </summary>
        /// <param name="time">تاريخ فارسی</param>
        /// <param name="days">عددی که می خواهيد به روز اضافه شود</param>
        /// <returns>نتيجه را بطور کامل بصورت رشته برمی گرداند</returns>
        public static string AddDays(string date, int days)
        {
            //            DateTime dtDays = _persianCalender.AddDays(System.Convert.ToDateTime(date), days);
            int intYear = int.Parse(date.Substring(0, 4));
            int intMonth = int.Parse(date.Substring(5, 2));
            int intDays = int.Parse(date.Substring(8, 2));
            DateTime newDate = _persianCalender.AddDays(_persianCalender.ToDateTime(intYear, intMonth, intDays, 0, 0, 0, 0), days);
            //return (dtDays.ToShortDateString());
            return (ConvertToShamsiDate(newDate));
        }

        /// <summary>
        /// اين تابع عدد مورد نظر را به ماه اضافه می کند
        /// </summary>
        /// <param name="date">تاريخ مورد نظر</param>
        /// <param name="months">عددی که می خواهيد به ماه اضافه شود</param>
        /// <returns>نتيجه را بطور کامل بصورت رشته برمی گرداند</returns>
        public static string AddMonths(DateTime date, int months)
        {
            string strMonths = ConvertToShamsiDate(date);
            return (AddMonths(strMonths, months));
        }

        /// <summary>
        /// اين تابع عدد مورد نظر را به ماه اضافه می کند
        /// </summary>
        /// <param name="date">تاريخ فارسی</param>
        /// <param name="months">عددی که می خواهيد به ماه اضافه شود</param>
        /// <returns>نتيجه را بطور کامل بصورت رشته برمی گرداند</returns>
        public static string AddMonths(string date, int months)
        {
            DateTime dtMonths = _persianCalender.AddMonths(System.Convert.ToDateTime(date), months);
            return (dtMonths.ToShortDateString());
        }

        /// <summary>
        /// اين تابع عدد مورد نظر را به سال اضافه می کند
        /// </summary>
        /// <param name="date">تاريخ مورد نظر</param>
        /// <param name="years">عددی که می خواهيد به سال اضافه شود</param>
        /// <returns>نتيجه را بطور کامل بصورت رشته برمی گرداند</returns>
        public static string AddYears(DateTime date, int years)
        {
            string strYears = ConvertToShamsiDate(date);
            return (AddYears(strYears, years));
        }

        /// <summary>
        /// اين تابع عدد مورد نظر را به سال اضافه می کند
        /// </summary>
        /// <param name="date">تاريخ فارسی</param>
        /// <param name="years">عددی که می خواهيد به سال اضافه شود</param>
        /// <returns>نتيجه را بطور کامل بصورت رشته برمی گرداند</returns>
        public static string AddYears(string date, int years)
        {
            DateTime dtYears = _persianCalender.AddYears(System.Convert.ToDateTime(date), years);
            return (dtYears.ToShortDateString());
        }

        /// <summary>
        /// اين تابع نام ماه مورد نظر را به فارسی بر می گرداند
        /// </summary>
        /// <param name="month">عدد ماه مورد نظر</param>
        /// <returns></returns>
        public static string GetPersianMonthName(int month)
        {
            string[] arMonths = new string[12] { "فروردين", "ارديبهشت", "خرداد", "تير", "مرداد", "شهريور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
            return (arMonths[month - 1]);
        }

        public static string GetPersianDayNameOfWeek(DateTime date)
        {
            DayOfWeek strResult = _persianCalender.GetDayOfWeek(date);
            string strDayName = string.Empty;
            switch (strResult)
            {
                case DayOfWeek.Saturday:
                    {
                        strDayName = "شنبه";
                        break;
                    }
                case DayOfWeek.Sunday:
                    {
                        strDayName = "یکشنبه";
                        break;
                    }
                case DayOfWeek.Monday:
                    {
                        strDayName = "دوشنبه";
                        break;
                    }
                case DayOfWeek.Tuesday:
                    {
                        strDayName = "سه شنبه";
                        break;
                    }
                case DayOfWeek.Wednesday:
                    {
                        strDayName = "چهار شنبه";
                        break;
                    }
                case DayOfWeek.Thursday:
                    {
                        strDayName = "پنج شنبه";
                        break;
                    }
                case DayOfWeek.Friday:
                    {
                        strDayName = "جمعه";
                        break;
                    }
            }
            return (strDayName);
        }

        /// <summary>
        /// با استفاده از اين تابع می توان تشخيص داد که تاريخ مورد نظر چه روزی از هفته می باشد
        /// </summary>
        /// <param name="date">تاريخ مورد نظر</param>
        /// <returns>چه روزی از هفته رابه فارسی برمی گرداند</returns>
        public static string GetPersianDayOfWeek(DateTime date)
        {
            strResult = ConvertToShamsiDate(date.ToShortDateString());
            return (GetPersianDayOfWeek(strResult));
        }

        /// <summary>
        /// با استفاده از اين تابع می توان تشخيص داد که تاريخ مورد نظر چه روزی از هفته می باشد
        /// </summary>
        /// <param name="date">تاريخ فارسی</param>
        /// <returns>چه روزی از هفته ) رابه فارسی برمی گرداند)</returns>
        public static string GetPersianDayOfWeek(string date)
        {
            //  string[] arPersianWeekDaysName = new string[8] { "", "يکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنج شنبه", "جمعه", "شنبه" };
            string[] arPersianWeekDaysName = new string[8] { "", "دوشنبه", "سه شنبه", "چهارشنبه", "پنج شنبه", "جمعه", "شنبه", "يکشنبه" };

            //  MessageBox.Show(Convert.ToDateTime("1390/05/01").ToString());
            //            MessageBox.Show(GetPersianDayOfWeekIndex(Convert.ToDateTime(AddDays(date, -1)).ToShortDateString()).ToString());
            int intIndex = 0;
            switch (date.Substring(5, 5))
            {
                case "02/30":
                case "02/31":
                case "03/01":
                case "03/02":
                case "05/01":
                case "05/02":
                case "07/01":
                case "07/02":
                    {
                        intIndex = 0;
                        break;
                    }
                default:
                    {
                        intIndex = GetPersianDayOfWeekIndex(Convert.ToDateTime(AddDays(date, -1)).ToShortDateString());
                        break;
                    }
            }
            strResult = arPersianWeekDaysName[intIndex];
            return (strResult);
            //            string[] arPersianWeekDaysName = new string[8] { "", "يکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنج شنبه", "جمعه", "شنبه" };
            ////            MessageBox.Show(GetPersianDayOfWeekIndex(Convert.ToDateTime(AddDays(date, -1)).ToShortDateString()).ToString());
            //            strResult = arPersianWeekDaysName[GetPersianDayOfWeekIndex(Convert.ToDateTime(AddDays(date, -1)).ToShortDateString())];
            //            return (strResult);
        }


        /// <summary>
        /// با استفاده از اين تابع می توان تشخيص داد که تاريخ مورد نظر چه روزی از هفته می باشد
        /// </summary>
        /// <param name="date">تاريخ فارسی</param>
        /// <returns>چه روزی از هفته ) رابه فارسی برمی گرداند)</returns>
        public static string GetPersianDayNameOfWeek(string date)
        {
            System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
            _persianCalender = new System.Globalization.PersianCalendar();
            DayOfWeek strDayName = pc.GetDayOfWeek(Azx.Windows.Forms.PersianDate.ConvertToMiladyDate(date));
            switch (strDayName)
            {
                case DayOfWeek.Saturday:
                    {
                        strResult = "شنبه";
                        break;
                    }
                case DayOfWeek.Sunday:
                    {
                        strResult = "یکشنبه";
                        break;
                    }
                case DayOfWeek.Monday:
                    {
                        strResult = "دوشنبه";
                        break;
                    }
                case DayOfWeek.Tuesday:
                    {
                        strResult = "سه شنبه";
                        break;
                    }
                case DayOfWeek.Wednesday:
                    {
                        strResult = "چهارشنبه";
                        break;
                    }
                case DayOfWeek.Thursday:
                    {
                        strResult = "پنج شنبه";
                        break;
                    }
                case DayOfWeek.Friday:
                    {
                        strResult = "جمعه";
                        break;
                    }
            }
            return (strResult);
        }

        /// <summary>
        /// اين تابع مشخص می کند سال شمسی مورد نظر کبيسه است يا نه
        /// </summary>
        /// <param name="year">ورودی تابع سال شمسی است</param>
        /// <returns>
        /// اگر تابع مقدار درست را بر گرداند سال مورد نظر کبيسه است 
        /// </returns>
        public static bool IsPersianLeapYear(int year)
        {
            if (year.ToString().Substring(0, 2) != "13")
            {
                year += 1300;
            }
            int yearmod33 = year % 33;
            if ((yearmod33 == 1) || (yearmod33 == 5) || (yearmod33 == 9) || (yearmod33 == 13) || (yearmod33 == 17) || (yearmod33 == 22) || (yearmod33 == 26) || (yearmod33 == 30))
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }

        /// <summary>
        /// اين تابع مشخص ميکند که آيا سال ورودی ميلادی کبيسه است يا خير
        /// </summary>
        /// <param name="year">
        /// اين پارامتر که سال ميلادی مورد نظر است
        /// </param>
        /// <returns>
        /// اگر تابع مقدار درست را بر گرداند سال ميلادی مورد نظر کبيسه است 
        /// نحوه تشخيص آن بدين صورت است که اگر ماه فوريه 29 روز باشد يعنی آن سال کبيسه خواهد بود زيرا در حالت عادی 28 روز است
        /// </returns>
        public static bool IsMiladiLeapYear(int yearMiladi)
        {
            if (((yearMiladi % 4 == 0) && ((yearMiladi) % 100 != 0)) || (((yearMiladi) % 400 == 0) && ((yearMiladi) % 100 == 0)))
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }

        /// <summary>
        /// با استفاده از اين تابع می توان تشخيص داد که تاريخ مورد نظر چندمين روز از هفته می باشد
        /// </summary>
        /// <param name="date">تاريخ مورد نظر</param>
        /// <returns>چندمين روز از هفته رابه عدد برمی گرداند</returns>
        public static int GetPersianDayOfWeekIndex(DateTime date)
        {
            int intIndex = 0;
            DayOfWeek dayOfWeek =
                _persianCalender.GetDayOfWeek(date);
            switch (dayOfWeek)
            {
                case DayOfWeek.Saturday:
                    {
                        intIndex = 1;
                        break;
                    }
                case DayOfWeek.Sunday:
                    {
                        intIndex = 2;
                        break;
                    }
                case DayOfWeek.Monday:
                    {
                        intIndex = 3;
                        break;
                    }
                case DayOfWeek.Tuesday:
                    {
                        intIndex = 4;
                        break;
                    }
                case DayOfWeek.Wednesday:
                    {
                        intIndex = 5;
                        break;
                    }
                case DayOfWeek.Thursday:
                    {
                        intIndex = 6;
                        break;
                    }
                case DayOfWeek.Friday:
                    {
                        intIndex = 7;
                        break;
                    }
            }
            return (intIndex);
            //  return (GetPersianDayOfWeekIndex(AddDays(date, 0).ToString()));
            //strResult = _persianCalender.GetDayOfWeek(date.ToShortDateString()).ToString();
        }

        /// <summary>
        /// با استفاده از اين تابع می توان تشخيص داد که تاريخ مورد نظر چندمين روز از هفته می باشد
        /// </summary>
        /// <param name="date">تاريخ فارسی</param>
        /// <returns>چندمين روز از هفته رابه عدد برمی گرداند</returns>
        public static int GetPersianDayOfWeekIndex(string date)
        {
            //string[] arEnglishWeekDaysName = new string[7] { "Saturday", "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday" };
            //strResult = _persianCalender.GetDayOfWeek(Convert.ToDateTime(AddDays(date, -1))).ToString();
            //for (int count = 0; count <= arEnglishWeekDaysName.Length; count++)
            //{
            //    if (strResult.Equals(arEnglishWeekDaysName[count]))
            //    {
            //        intResult = ++count;
            //        break;
            //    }
            //}
            intResult =
                GetPersianDayOfWeekIndex(Azx.Windows.Forms.PersianDate.ConvertToMiladyDate(date));
            return (intResult);
        }

        /// <summary>
        /// اين تابع صحت فرمت تاريخ فارسی که بصورت رشته باشد را تعيين می کند
        /// و قادر است سال کبيسه را نيز کنترل نمايد
        /// </summary>
        /// <param name="persian_date">رشته با فرمت تاريخ فارسی</param>
        /// <returns>
        ///باشد فرمت تاريخ صحيح است true اگر 
        /// </returns>
        public static bool IsPersianValidDate(string persian_date)
        {
            //			try
            //			{
            bool DateTrue = true;
            string Day, Month, Year;
            if (persian_date == "0")
            {
                return (false);
            }
            //if(persian_date.Length 
            if (persian_date.Substring(0, 4).Contains("/"))
            {
                if (persian_date.Length == 8)
                {
                    Day = persian_date.Substring(6, 2);
                    Month = persian_date.Substring(3, 2);
                    Year = persian_date.Substring(0, 2);
                }
                else
                {
                    return (false);
                }
            }
            else
            {
                if (persian_date.Length == 10)
                {
                    Day = persian_date.Substring(8, 2);
                    Month = persian_date.Substring(5, 2);
                    Year = persian_date.Substring(0, 4);
                }
                else
                {
                    return (false);
                }
            }
            if ((Convert.ToInt32(Month) < 1) || (Convert.ToInt32(Month) > 12))
            {
                DateTrue = false;
            }
            if (Convert.ToInt32(Month) < 7) //شش ماهه اول سال
            {
                if ((Convert.ToInt32(Day) < 1) || (Convert.ToInt32(Day) > 31))
                {
                    DateTrue = false;
                }
            }
            if (Convert.ToInt32(Month) < 12 && Convert.ToInt32(Month) >= 7)
            {
                if ((Convert.ToInt32(Day) < 1) || (Convert.ToInt32(Day) > 30))
                {
                    DateTrue = false;
                }
            }
            if (IsPersianLeapYear(Convert.ToInt32(Year)) && (Convert.ToInt32(Month) == 12))
            {
                if ((Convert.ToInt32(Day) < 1) || (Convert.ToInt32(Day) > 30))
                {
                    DateTrue = false;
                }
            }
            else
            {
                if (!IsPersianLeapYear(Convert.ToInt32(Year)) && (Convert.ToInt32(Month) == 12))
                {
                    if ((Convert.ToInt32(Day) < 1) || (Convert.ToInt32(Day) > 29))
                    {
                        DateTrue = false;
                    }
                }
            }
            return DateTrue;
            //			}
            //			catch (System.Exception ex)
            //			{
            //				string strError = "کاربر گرامی خطای " + ex.Message + "رخ داده است";
            //				PersianMessageBox.Show(strError, "خطا", PersianMessageBox.PersianMessageBoxButton.قبول, PersianMessageBox.aPersianDefaultButoon.دكمه1, PersianMessageBox.PersianMessageBoxIcon.خطا);
            //			}
        }

        /// <summary>
        /// اين تابع تاريخ شمسی را به حروف فارسی تبديل می نمايد
        /// </summary>
        /// <param name="date">تاريخ فارسی</param>
        /// <returns>متن فارسی تاريخ شمسی را برمی گرداند</returns>
        public static string ConvertToFarsiTextDate(string date)
        {

            string strFarsiText, strConvertDate, strDay, strMonth, strYear, strYear1, strYear2, strYear3;
            strConvertDate = date;// ConvertToShamsiDate(date);
            string[] arDay = new string[31] { "يکم", "دوم", "سوم", "چهارم", "پنجم", "ششم", "هفتم", "هشتم", "نهم", "دهم", "يازدهم", "دوازدهم", "سيزدهم", "چهاردهم", "پانزدهم", "شانزدهم", "هفدهم", "هجدهم", "نوزدهم", "بيستم", "بيست ويکم", "بيست ودوم", "بيست وسوم", "بيست وچهارم", "بيست وپنجم", "بيست وششم", "بيست وهفتم", "بيست وهشتم", "بيست ونهم", "سی ام", "سی و يکم" };
            strDay = arDay[Convert.ToInt32(strConvertDate.Substring(8, 2)) - 1];
            string[] arMonth = new string[12] { "فروردين", "ارديبهشت", "خرداد", "تير", "مرداد", "شهريور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
            strMonth = arMonth[Convert.ToInt32(strConvertDate.Substring(5, 2)) - 1];
            string[] arYear1 = new string[9] { "يکصد", "دويست", "سيصد", "چهارصد", "پانصد", "ششصد", "هفتصد", "هشتصد", "نهصد" };
            int Sadgan = Convert.ToInt32(strConvertDate.Substring(1, 3)) / 100;
            strYear1 = arYear1[Sadgan - 1];
            string[] arYear2 = new string[9] { "ده", "بيست", "ُسي", "چهل", "پنجاه", "شصت", "هفتاد", "هشتاد", "نود" };
            int Dahgan = (Convert.ToInt32(strConvertDate.Substring(1, 3)) % 100) / 10;
            strYear2 = arYear2[Dahgan - 1];
            string[] arYear3 = new string[9] { "يک", "دو", "سه", "چهار", "پنج", "شش", "هفت", "هشت", "نه" };
            int Yekan = (Convert.ToInt32(strConvertDate.Substring(1, 3)) % 100) % 10;
            if (Yekan > 0)
            {
                strYear3 = arYear3[Yekan - 1];
                strYear = strYear1 + " و " + strYear2 + " و " + strYear3;
            }
            else
            {
                strYear = strYear1 + " و " + strYear2;// +" و "; //+ strYear3;
            }
            strFarsiText = strDay + " " + strMonth + " " + " يکهزارو " + strYear;
            return (strFarsiText);
        }

        /// <summary>
        /// اين تابع تاريخ شمسی را به حروف فارسی تبديل می نمايد
        /// </summary>
        /// <param name="date">تاريخ مورد نظر</param>
        /// <returns>متن فارسی تاريخ شمسی را برمی گرداند</returns>
        public static string ConvertToFarsiTextDate(DateTime date)
        {
            return (ConvertToFarsiTextDate(ConvertToShamsiDate(date).ToString()));
        }

        /// <summary>
        /// تعداد روز سال شمسی مورد نظر را بر می گرداند
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public static int GetPersianDaysInYear(int year)
        {
            return (_persianCalender.GetDaysInYear(year));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="persianDate"></param>
        /// <returns></returns>
        public static bool IsFriday(string persianDate)
        {
            if (GetPersianDayOfWeekIndex(persianDate) == 7)
            {
                return (true);
            }
            else
            {
                return (false);
            }
        }

        public static int GetDaysOfBetweenDates(DateTime date1, DateTime date2)
        {
            int days = 0;

            days = _persianCalender.GetDayOfYear(date2) - _persianCalender.GetDayOfYear(date1);
            return (days);
        }

        public static int GetDaysOfBetweenDates(string startPersianDate, string finishPersianDate)
        {
            int days = 0;
            int intResult1 = 0;
            int intFinishDays = _persianCalender.GetDayOfYear(ConvertToMiladyDate(finishPersianDate));
            int intStartDays = _persianCalender.GetDayOfYear(ConvertToMiladyDate(startPersianDate));
            if (startPersianDate.Substring(0, 4) == finishPersianDate.Substring(0, 4))
            {

                //DateTime dt = Convert.ToDateTime(date2);
                //DateTime dt1 = Convert.ToDateTime(date1);
                //TimeSpan dat = dt.Subtract(dt1);
                //MessageBox.Show(dat.ToString());
                days = intFinishDays - intStartDays;// _persianCalender.GetDayOfYear(Convert.ToDateTime(date2)) - _persianCalender.GetDayOfYear(Convert.ToDateTime(date1));
                //    return (System.Math.Abs(days)+1);
            }
            else
            {

                if (IsPersianLeapYear(int.Parse(startPersianDate.Substring(0, 4))) == true) //سال کبیسه است
                {
                    intResult1 = 366 - intStartDays;
                }
                else //سال کبیسه نیست
                {
                    intResult1 = 365 - intStartDays;
                }
                if (int.Parse(finishPersianDate.Substring(0, 4)) - int.Parse(startPersianDate.Substring(0, 4)) > 1)
                {
                    for (int index = int.Parse(startPersianDate.Substring(0, 4)); index < int.Parse(finishPersianDate.Substring(0, 4)) - 1; index++)
                    {
                        if (IsPersianLeapYear(index) == true)
                        {
                            intResult1 += 366;
                        }
                        else
                        {
                            intResult1 += 365;
                        }
                    }
                }
                days = intResult1 + intFinishDays;
            }
            return (days);
        }

        public static CompareMode CompareDates(string firstDate, string lastDate)
        {
            CompareMode compareMode = CompareMode.None;
            //switch (string.Compare(firstDate, lastDate,
            //System.Globalization.CultureInfo.CurrentCulture,
            //System.Globalization.CompareOptions.StringSort))
            if (string.IsNullOrWhiteSpace(firstDate) == true || string.IsNullOrWhiteSpace(lastDate) == true)
            {
                return CompareMode.None;
            }
            if (firstDate.Substring(0, 2).Contains("0") == true && lastDate.Substring(0, 2).Contains("0") == false)
            {
                firstDate = "4" + firstDate;
                lastDate = "3" + lastDate;
            }
            switch (String.Compare
                (firstDate, lastDate, StringComparison.CurrentCulture))
            {
                case 0:
                    {
                        compareMode = CompareMode.Equal;
                        break;
                    }
                case 1:
                    {
                        compareMode = CompareMode.Greater;
                        break;
                    }
                case -1:
                    {
                        compareMode = CompareMode.Less;
                        break;
                    }
                default:
                    {
                        compareMode = CompareMode.None;
                        break;
                    }
            }
            return (compareMode);
        }

        //public static string GetDaysBetweenDates(string date1, string date2)
        //{
        //    DateTime date1, date2;
        //    int year1, month1, day1, year2, month2, day2, month, day, day11, day12;
        //    year1 = 0; year2 = 0; month1 = 0; month2 = 0; day2 = 0; day1 = 0; day = 0;
        //    if ((axMaskEdBox3.FormattedText.Substring(3, 5) == "06/31" || axMaskEdBox3.FormattedText.Substring(3, 5) == "04/31" || axMaskEdBox3.FormattedText.Substring(3, 5) == "02/31" || axMaskEdBox3.FormattedText.Substring(3, 5) == "02/30") && (axMaskEdBox4.FormattedText.Substring(3, 5) == "02/30" || axMaskEdBox4.FormattedText.Substring(3, 5) == "02/31" || axMaskEdBox4.FormattedText.Substring(3, 5) == "04/31" || axMaskEdBox4.FormattedText.Substring(3, 5) == "06/31"))
        //    {
        //        if (axMaskEdBox3.FormattedText.Substring(3, 5) == "06/31")
        //        {
        //            string s = yy.ToString() + "/05/31";
        //            date1 = System.Convert.ToDateTime(s);
        //            year1 = System.Convert.ToInt16(date1.Year.ToString().Substring(2, 2));
        //            month1 = 6;
        //            day1 = date1.Day;
        //        }
        //        if (axMaskEdBox3.FormattedText.Substring(3, 5) == "04/31")
        //        {
        //            string s = yy.ToString() + "/05/31";
        //            date1 = System.Convert.ToDateTime(s);
        //            year1 = System.Convert.ToInt16(date1.Year.ToString().Substring(2, 2));
        //            month1 = 4;
        //            day1 = date1.Day;
        //        }
        //        if (axMaskEdBox3.FormattedText.Substring(3, 5) == "02/31")
        //        {
        //            string s = yy.ToString() + "/05/31";
        //            date1 = System.Convert.ToDateTime(s);
        //            year1 = System.Convert.ToInt16(date1.Year.ToString().Substring(2, 2));
        //            month1 = 2;
        //            day1 = date1.Day;
        //        }
        //        if (axMaskEdBox3.FormattedText.Substring(3, 5) == "02/30")
        //        {
        //            string s = yy.ToString() + "/05/30";
        //            date1 = System.Convert.ToDateTime(s);
        //            year1 = System.Convert.ToInt16(date1.Year.ToString().Substring(2, 2));
        //            month1 = 2;
        //            day1 = date1.Day;
        //        }
        //        if (axMaskEdBox4.FormattedText.Substring(3, 5) == "06/31")
        //        {
        //            string s = yy.ToString() + "/05/31";
        //            date2 = System.Convert.ToDateTime(s);
        //            year2 = System.Convert.ToInt16(date2.Year.ToString().Substring(2, 2));
        //            month2 = 6;
        //            day2 = date2.Day;
        //        }
        //        if (axMaskEdBox4.FormattedText.Substring(3, 5) == "04/31")
        //        {
        //            string s = yy.ToString() + "/05/31";
        //            date2 = System.Convert.ToDateTime(s);
        //            year2 = System.Convert.ToInt16(date2.Year.ToString().Substring(2, 2));
        //            month2 = 4;
        //            day2 = date2.Day;
        //        }
        //        if (axMaskEdBox4.FormattedText.Substring(3, 5) == "02/31")
        //        {
        //            string s = yy.ToString() + "/05/31";
        //            date2 = System.Convert.ToDateTime(s);
        //            year2 = System.Convert.ToInt16(date2.Year.ToString().Substring(2, 2));
        //            month2 = 2;
        //            day2 = date2.Day;
        //        }
        //        if (axMaskEdBox4.FormattedText.Substring(3, 5) == "02/30")
        //        {
        //            string s = yy.ToString() + "/05/30";
        //            date2 = System.Convert.ToDateTime(s);
        //            year2 = System.Convert.ToInt16(date2.Year.ToString().Substring(2, 2));
        //            month2 = 2;
        //            day2 = date2.Day;
        //        }
        //    }
        //    else if (axMaskEdBox3.FormattedText.Substring(3, 5) == "06/31" || axMaskEdBox3.FormattedText.Substring(3, 5) == "04/31" || axMaskEdBox3.FormattedText.Substring(3, 5) == "02/31" || axMaskEdBox3.FormattedText.Substring(3, 5) == "02/30")
        //    {
        //        if (axMaskEdBox3.FormattedText.Substring(3, 5) == "06/31")
        //        {
        //            string s = yy.ToString() + "/05/31";
        //            date1 = System.Convert.ToDateTime(s);
        //            year1 = System.Convert.ToInt16(date1.Year.ToString().Substring(2, 2));
        //            month1 = 6;
        //            day1 = date1.Day;
        //        }
        //        if (axMaskEdBox3.FormattedText.Substring(3, 5) == "04/31")
        //        {
        //            string s = yy.ToString() + "/05/31";
        //            date1 = System.Convert.ToDateTime(s);
        //            year1 = System.Convert.ToInt16(date1.Year.ToString().Substring(2, 2));
        //            month1 = 4;
        //            day1 = date1.Day;
        //        }
        //        if (axMaskEdBox3.FormattedText.Substring(3, 5) == "02/31")
        //        {
        //            string s = yy.ToString() + "/05/31";
        //            date1 = System.Convert.ToDateTime(s);
        //            year1 = System.Convert.ToInt16(date1.Year.ToString().Substring(2, 2));
        //            month1 = 2;
        //            day1 = date1.Day;
        //        }
        //        if (axMaskEdBox3.FormattedText.Substring(3, 5) == "02/30")
        //        {
        //            string s = yy.ToString() + "/05/30";
        //            date1 = System.Convert.ToDateTime(s);
        //            year1 = System.Convert.ToInt16(date1.Year.ToString().Substring(2, 2));
        //            month1 = 2;
        //            day1 = date1.Day;
        //        }
        //        date2 = System.Convert.ToDateTime(axMaskEdBox4.FormattedText);
        //        year2 = System.Convert.ToInt16(date2.Year.ToString().Substring(2, 2));
        //        month2 = date2.Month;
        //        day2 = date2.Day;
        //    }
        //    else if (axMaskEdBox4.FormattedText.Substring(3, 5) == "02/30" || axMaskEdBox4.FormattedText.Substring(3, 5) == "02/31" || axMaskEdBox4.FormattedText.Substring(3, 5) == "04/31" || axMaskEdBox4.FormattedText.Substring(3, 5) == "06/31")
        //    {
        //        if (axMaskEdBox4.FormattedText.Substring(3, 5) == "06/31")
        //        {
        //            string s = yy.ToString() + "/05/31";
        //            date2 = System.Convert.ToDateTime(s);
        //            year2 = System.Convert.ToInt16(date2.Year.ToString().Substring(2, 2));
        //            month2 = 6;
        //            day2 = date2.Day;
        //        }
        //        if (axMaskEdBox4.FormattedText.Substring(3, 5) == "04/31")
        //        {
        //            string s = yy.ToString() + "/05/31";
        //            date2 = System.Convert.ToDateTime(s);
        //            year2 = System.Convert.ToInt16(date2.Year.ToString().Substring(2, 2));
        //            month2 = 4;
        //            day2 = date2.Day;
        //        }
        //        if (axMaskEdBox4.FormattedText.Substring(3, 5) == "02/31")
        //        {
        //            string s = yy.ToString() + "/05/31";
        //            date2 = System.Convert.ToDateTime(s);
        //            year2 = System.Convert.ToInt16(date2.Year.ToString().Substring(2, 2));
        //            month2 = 2;
        //            day2 = date2.Day;
        //        }
        //        if (axMaskEdBox4.FormattedText.Substring(3, 5) == "02/30")
        //        {
        //            string s = yy.ToString() + "/05/30";
        //            date2 = System.Convert.ToDateTime(s);
        //            year2 = System.Convert.ToInt16(date2.Year.ToString().Substring(2, 2));
        //            month2 = 2;
        //            day2 = date2.Day;
        //        }
        //        date1 = System.Convert.ToDateTime(axMaskEdBox3.FormattedText);
        //        year1 = System.Convert.ToInt16(date1.Year.ToString().Substring(2, 2));
        //        month1 = date1.Month;
        //        day1 = date1.Day;
        //    }
        //    else
        //    {
        //        date1 = System.Convert.ToDateTime(axMaskEdBox3.FormattedText);
        //        date2 = System.Convert.ToDateTime(axMaskEdBox4.FormattedText);
        //        year1 = System.Convert.ToInt16(date1.Year.ToString().Substring(2, 2));
        //        month1 = date1.Month;
        //        day1 = date1.Day;
        //        year2 = System.Convert.ToInt16(date2.Year.ToString().Substring(2, 2));
        //        month2 = date2.Month;
        //        day2 = date2.Day;
        //    }
        //    //day =0;
        //    if (year1 == year2 && month1 == month2)
        //    {
        //        day = day2 - day1;
        //        day++;
        //    }
        //    else if (year1 == year2 && month1 < month2)
        //    {
        //        if (month1 > 6 && month2 > 6)
        //        {
        //            month = (month2 - month1) * 30;
        //            day = day2 - day1;
        //            day += month;
        //            day++;
        //        }
        //        else if (month1 <= 6 && month2 <= 6)
        //        {
        //            month = (month2 - month1) * 31;
        //            day = day2 - day1;
        //            day += month;
        //            day++;
        //        }
        //        else if (month1 <= 6 && month2 > 6)
        //        {
        //            month = (6 - month1) * 31;
        //            day11 = 31 - day1;
        //            day11 += month;
        //            day11++;

        //            month = (month2 - 7) * 30;
        //            day12 = day2 - 1;
        //            day12 += month;
        //            day12++;
        //            day = day11 + day12;
        //        }
        //    }
        //    else if (year1 < year2)
        //    {
        //    }
        //    dayy = day.ToString();
        //}
        //**************************************************************
        //تابع های داخلی که فقط داخل همين برنامه مورد استفاده قرار می گيرند و از دنيای بيرون قابل دسترس نيست
        //**************************************************************
        /// <summary>
        /// سال ميلادی به سال شمسی تبديل می شود
        /// </summary>
        /// <param name="_dateTime"></param>
        /// <returns></returns>
        private static string ConvertYear(DateTime _dateTime)
        {
            int Year = _persianCalender.GetYear(_dateTime);
            return (Year.ToString());
        }

        /// <summary>
        /// ماه ميلادی  به ماه شمسی تبديل می شود
        /// </summary>
        /// <param name="_dateTime"></param>
        /// <returns></returns>
        private static string ConvertMonth(DateTime _dateTime)
        {
            string ResultMonth;
            int Month = _persianCalender.GetMonth(_dateTime);
            if (Month <= 9)
            {
                ResultMonth = "0" + Month.ToString();
            }
            else
            {
                ResultMonth = Month.ToString();
            }
            return ResultMonth;
        }

        /// <summary>
        /// روز ميلادی به روز شمسی تبديل می شود
        /// </summary>
        /// <param name="_dateTime"></param>
        /// <returns></returns>
        private static string ConvertDay(DateTime _dateTime)
        {
            string ResultDay;
            int Day = _persianCalender.GetDayOfMonth(_dateTime);
            if (Day <= 9)
            {
                ResultDay = "0" + Day.ToString();
            }
            else
            {
                ResultDay = Day.ToString();
            }
            return (ResultDay);
        }

        public static DateTime ConvertToMiladyDate(string persianDate)
        {
            int intYear = int.Parse(persianDate.Substring(0, 4));
            int intMonth = int.Parse(persianDate.Substring(5, 2));
            int intDay = int.Parse(persianDate.Substring(8, 2));
            return (_persianCalender.ToDateTime(intYear, intMonth, intDay, 0, 0, 0, 0));
        }

        public static string GetFirstDateOfMonth(string date)
        {
            if (date.Length == 10)
            {
                return (date.Substring(0, 8) + "01");
            }
            else if (date.Length == 8)
            {
                return (date.Substring(0, 6) + "01");
            }
            return (string.Empty);
        }

        public static string GetLastDateOfMonth(string date)
        {
            string strMonth = date.Substring(5, 2);
            string strNewDate = "";
            switch (strMonth)
            {
                case "01":
                case "02":
                case "03":
                case "04":
                case "05":
                case "06":
                    {
                        strNewDate = date.Substring(0, 8) + "31";
                        break;
                    }
                case "07":
                case "08":
                case "09":
                case "10":
                case "11":
                    {
                        strNewDate = date.Substring(0, 8) + "30";
                        break;

                    }
                case "12":
                    {
                        if (IsPersianLeapYear(int.Parse(date.Substring(0, 4))) == true)
                        {
                            strNewDate = date.Substring(0, 8) + "30";
                        }
                        else
                        {
                            strNewDate = date.Substring(0, 8) + "29";
                        }
                        break;
                    }
            }
            return (strNewDate);
        }

        public static int GetCurrentPersianYearNo()
        {
            return (int.Parse(ConvertToShamsiDate(DateTime.Now).Substring(2, 2)));
        }

        public static int GetCurrentPersianMonthNo()
        {
            return (int.Parse(ConvertToShamsiDate(DateTime.Now).Substring(5, 2)));
        }

        public static int GetPersianDayIndexOfWeek(DateTime date)
        {
            int intDayNo = 0;
            DayOfWeek day = _persianCalender.GetDayOfWeek(date);// DateTime.Now);
            switch (day)
            {
                case DayOfWeek.Friday:
                    {
                        intDayNo = 7;
                        break;
                    }
                case DayOfWeek.Monday:
                    {
                        intDayNo = 3;
                        break;
                    }
                case DayOfWeek.Saturday:
                    {
                        intDayNo = 1;
                        break;
                    }
                case DayOfWeek.Sunday:
                    {
                        intDayNo = 2;
                        break;
                    }
                case DayOfWeek.Thursday:
                    {
                        intDayNo = 6;
                        break;
                    }
                case DayOfWeek.Tuesday:
                    {
                        intDayNo = 4;
                        break;
                    }
                case DayOfWeek.Wednesday:
                    {
                        intDayNo = 5;
                        break;
                    }
            }
            return (intDayNo);
        }

        public static List<string> GetPersianDatesOfWeek(string currentPersianDate)
        {
            string strCurrentDate = currentPersianDate;
            string strResultDate = string.Empty;
            List<string> CurrentWeekDates = new List<string>();
            int CurrentDayIndex =
                GetPersianDayIndexOfWeek(ConvertToMiladyDate(currentPersianDate));
            for (int intIndex = CurrentDayIndex - 1; intIndex >= 1; intIndex--)
            {
                strResultDate = AddDays(strCurrentDate, intIndex * (-1));
                CurrentWeekDates.Add(strResultDate);
            }
            CurrentWeekDates.Add(strCurrentDate);
            for (int intIndex = 1; intIndex <= 7 - CurrentDayIndex; intIndex++)
            {
                strResultDate = AddDays(strCurrentDate, intIndex);
                CurrentWeekDates.Add(strResultDate);
            }
            return (CurrentWeekDates);
        }

        public static bool IsEvenDay(DateTime date)
        {
            bool blnResult = false;
            switch (GetPersianDayIndexOfWeek(date))
            {
                case 1:
                case 2:
                case 5:
                    {
                        blnResult = (true);
                        break;
                    }
            }
            return (blnResult);
        }

        public static bool IsOddDay(DateTime date)
        {
            bool blnResult = false;
            switch (GetPersianDayIndexOfWeek(date))
            {
                case 2:
                case 4:
                case 6:
                    {
                        blnResult = (true);
                        break;
                    }
            }
            return (blnResult);
        }

        //public static DateTime ToEn(string fadate)
        //{
        //    if (fadate.Trim() == "") return DateTime.MinValue;
        //    int[] farsiPartArray = SplitRoozMahSalNew(fadate);

        //    return new PersianCalendar().ToDateTime(farsiPartArray[0], farsiPartArray[1], farsiPartArray[2], 0, 0, 0, 0);

        //}

        //private static int[] SplitRoozMahSalNew(string farsiDate)
        //{
        //    int pYear = 0;
        //    int pMonth = 0;
        //    int pDay = 0;


        //    //normalize with one character
        //    farsiDate = farsiDate.Trim().Replace(@"\", "/").Replace(@"-", "/").Replace(@"_", "/").
        //        Replace(@",", "/").Replace(@".", "/").Replace(@" ", "/");


        //    string[] rawValues = farsiDate.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);


        //    if (!farsiDate.Contains("/"))
        //    {
        //        if (rawValues.Length != 2)
        //            throw new Exception("usually there should be 2 seperator for a complete date");
        //    }
        //    else //mostly given in all numeric format like 13930316
        //    {
        //        // detect year side and add slashes in right places and continue
        //    }
        //    //new simple method which emcompass below methods too
        //    try
        //    {
        //        pYear = int.Parse(rawValues[0].TrimStart(new[] { '0' }).Trim());
        //        pMonth = int.Parse(rawValues[1].TrimStart(new[] { '0' }).Trim());
        //        pDay = int.Parse(rawValues[2].TrimStart(new[] { '0' }).Trim());

        //        // the year usually must be larger than 90
        //        //or for historic values rarely lower than 33 if 2 digit is given
        //        if (pYear < 33 && pYear > 0)
        //        {
        //            //swap year and day
        //            pYear = pDay;
        //            pDay = int.Parse(rawValues[0]); //convert again
        //        }
        //        //fix 2 digits of persian strings
        //        if (pYear.ToString(CultureInfo.InvariantCulture).Length == 2)
        //            pYear = pYear + 1300;
        //        //
        //        if (pMonth <= 0 || pMonth >= 13)
        //            throw new Exception("mahe shamsi must be under 12 ");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(
        //            "invalid Persian date format: maybe all 3 numric Sal, Mah,rooz parts are not present. \r\n" + ex);
        //    }

        //    return new[] { pYear, pMonth, pDay };
        //}
    }
}
