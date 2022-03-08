using Azx.Windows.Net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Azx.Windows.API
{
    internal sealed class API
    {
        [DllImport("MF_API.dll")]
        public static extern short MF_GetDLL_Ver(ref Byte rVER);
        [DllImport("MF_API.dll")]
        public static extern int MF_InitComm(String portname, int baud);
        //public static extern int MF_ControlBuzzer Lib "MF_API.dll" (ByVal DeviceAddr As short, ByRef BeepTime As short) As Integer
        //public static extern int MF_DeviceReset Lib "MF_API.dll" (ByVal DeviceAddr As short) As Integer
        [DllImport("MF_API.dll")]
        public static extern int MF_ExitComm();
        //public static extern short MF_GetDevice_Ver Lib "MF_API.dll" (ByVal DeviceAddr As short, ByRef ver As Byte) As Integer
        //public static extern short MF_SetDeviceBaud Lib "MF_API.dll" (ByVal DeviceAddr As short, ByVal baud As Integer) As Integer
        //public static extern short MF_SetDeviceAddr Lib "MF_API.dll" (ByVal DeviceAddr As short, ByVal addr As short) As Integer
        //public static extern short MF_ControlLED Lib "MF_API.dll" (ByVal DeviceAddr As short, ByVal LED1 As short, ByVal LED2 As short) As Integer
        //public static extern short MF_GetDeviceAddr Lib "MF_API.dll" (ByVal DeviceAddr As short, ByRef addr As Byte) As Integer
        //public static extern short MF_SetDeviceSNR Lib "MF_API.dll" (ByVal DeviceAddr As short, ByVal snr As String) As Integer
        //public static extern short MF_GetDeviceSNR Lib "MF_API.dll" (ByVal DeviceAddr As short, ByRef snr As Byte) As Integer
        //public static extern short MF_SetRF_ON Lib "MF_API.dll" (ByVal DeviceAddr As short) As Integer
        //public static extern short MF_SetRF_OFF Lib "MF_API.dll" (ByVal DeviceAddr As short) As Integer
        //public static extern short MF_SetWiegandMode Lib "MF_API.dll" (ByVal DeviceAddr As short, ByVal mode As short, ByVal alarm As short) As Integer
        //
        //'''''''''''''''''''''''''''''''''''card reading functions''''''''''''''''''''''''''''''''''''''''''
        [DllImport("MF_API.dll")]
        public static extern int MF_Request(short DeviceAddr, short mode, ref Byte CardType);
        [DllImport("MF_API.dll")]
        public static extern int MF_Anticoll(short DeviceAddr, ref Byte snr);
        //public static extern short MF_Halt Lib "MF_API.dll" (ByVal DeviceAddr As short);
        [DllImport("MF_API.dll")]
        public static extern int MF_Select(short DeviceAddr, ref Byte snr);
        [DllImport("MF_API.dll")]
        public static extern int MF_LoadKey(short DeviceAddr, ref Byte key);
        [DllImport("MF_API.dll")]
        public static extern int MF_LoadKeyFromEE(short DeviceAddr, short KeyType, short KeyNum);
        //public static extern int MF_StoreKeyToEE Lib "MF_API.dll" (ByVal DeviceAddr As short, ByVal KeyAB As short, ByVal KeyAdd As short, ByRef key As Byte);
        [DllImport("MF_API.dll")]
        public static extern int MF_Authentication(short DeviceAddr, short AuthType, short block, ref Byte snr);
        [DllImport("MF_API.dll")]
        public static extern int MF_Read(short DeviceAddr, short block, short numbers, ref Byte databuff);

        [DllImport("MF_API.dll")]
        public static extern int MF_Read(short DeviceAddr, short block, short numbers, ref String databuff);
        [DllImport("MF_API.dll")]
        public static extern int MF_Read(short DeviceAddr, short block, short numbers, ref Char databuff);

        [DllImport("MF_API.dll")]
        public static extern int MF_Write(short DeviceAddr, short block, short numbers, ref Byte databuff);

        [DllImport("MF_API.dll")]
        public static extern int MF_Write(short DeviceAddr, short block, short numbers, ref Char databuff);

        [DllImport("MF_API.dll")]
        public static extern int MF_Value(short DeviceAddr, short valoption, short block, ref Byte value);
        [DllImport("MF_API.dll")]
        public static extern int MF_Transfer(short DeviceAddr, short block);
        [DllImport("MF_API.dll")]
        public static extern int MF_ControlBuzzer(short DeviceAddr, short BeepTime);

        [DllImport("MF_API.dll")]
        public static extern int MF_ControlLED(short DeviceAddr, short RedLED, short GreenLED);

        [DllImport("MF_API.dll")]
        public static extern int MF_DeviceReset(short DeviceAddr);

        [DllImport("MF_API.dll")]
        public static extern int MF_SetRF_ON(short DeviceAddr);

        [DllImport("MF_API.dll")]
        public static extern int MF_SetRF_OFF(short DeviceAddr);

        [DllImport("wininet.dll", CharSet = CharSet.Auto)]
        public static extern bool InternetGetConnectedState(ref InternetConnectionState lpdwFlags, int dwReserved);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int AnimateWindow(IntPtr hwand, int dwTime, int dwFlags);

        //Some Effect are :
        // CollapseInward_Effect = AW_ACTIVATE | AW_CENTER;
        //  FadeEffect = AW_ACTIVATE | AW_BLEND;
        //   BottomToTop = AW_ACTIVATE | AW_VER_NEGATIVE | AW_SLIDE;
        //  TopToBottom = AW_ACTIVATE | AW_VER_POSITIVE | AW_SLIDE;
        //  RightToLeft = AW_ACTIVATE | AW_HOR_NEGATIVE | AW_SLIDE;
        //  LeftToRight = AW_ACTIVATE | AW_HOR_POSITIVE | AW_SLIDE;

        [StructLayout(LayoutKind.Sequential)]
        public struct LASTINPUTINFO
        {
            public static readonly int SizeOf = Marshal.SizeOf(typeof(LASTINPUTINFO));
            [MarshalAs(UnmanagedType.U4)]
            public int cbSize;
            [MarshalAs(UnmanagedType.U4)]
            public UInt32 dwTime;
        }
        //جهت کنترل زمان بیکاری سیستم
        [DllImport("user32.dll")]
        public static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);

        //جهت کنترل زبان کیبورد
        [DllImport("user32.dll")]
        private static extern bool PostMessage(int hhwnd, uint msg, IntPtr wparam, IntPtr lparam);
        [DllImport("user32.dll")]
        private static extern long GetKeyboardLayoutName(System.Text.StringBuilder pwszKLID);
        [DllImport("user32.dll")]
        private static extern long LoadKeyboardLayout(string pwszKLID, uint Flags);

        //************************************************************************
        //    DE-620    جهت کارتخوان جدید     مدل  
        //***************************************************************************

        [DllImport("DualCardDll.dll")]
        public static extern int DE_InitPort(int nPort, int nBaud);

        [DllImport("DualCardDll.dll")]
        public static extern int DE_BuzzerOn(int nPort);

        [DllImport("DualCardDll.dll")]
        public static extern int DE_BuzzerOff(int nPort);

        [DllImport("DualCardDll.dll")]
        public static extern void DE_ClosePort(int nPort);

        [DllImport("DualCardDll.dll")]
        public static extern void DE_GetVersion(int nPort, out int outlen, byte[] lpRes);

        [DllImport("DualCardDll.dll")]
        public static extern int DEA_Idle_Req(int nPort, out int outlen, byte[] lpRes);

        [DllImport("DualCardDll.dll")]
        public static extern int DEA_Anticoll(int nPort, byte level, out int outlen, byte[] lpRes);

        [DllImport("DualCardDll.dll")]
        public static extern int DEA_Select(int nPort, byte[] uid, out int outlen, byte[] lpRes);

        [DllImport("DualCardDll.dll")]
        public static extern int DEA_Authkey(int nPort, byte mode, byte[] keydata, byte blockno);

        [DllImport("DualCardDll.dll")]
        public static extern int DEA_Read(int nPort, byte blockno, out int outlen, byte[] lpRes);

        [DllImport("DualCardDll.dll")]
        public static extern int DEA_Write(int nPort, byte blockno, int datalen, byte[] data);

        [DllImport("DualCardDll.dll")]
        public static extern int GetErrMsg(int errcode, char[] retmsg);

        [DllImport("DualCardDll.dll")]
        public static extern int DE_RFOn(int nPort);

        [DllImport("DualCardDll.dll")]
        public static extern int DE_RFOff(int nPort);

        [DllImport("DualCardDll.dll")]
        public static extern int DEB_Transparent(int nPort, byte datalen, byte[] data, byte TOUT, out byte outlen, byte[] lpRes);

        [DllImport("DualCardDll.dll")]
        public static extern int DEA_Loadkey(int nPort, byte mode, byte keyno, byte[] keydata);

        [DllImport("DualCardDll.dll")]
        public static extern int DEA_Req_AuthkeyRead(int nPort, byte requestmode, byte authmode, byte blockno, byte[] keydata, out int outlen, byte[] lpRes);

        [DllImport("DualCardDll.dll")]
        public static extern int DEA_Req_AuthRead(int nPort, byte requestmode, byte authmode, byte keyno, byte blockno, out int outlen, byte[] lpRes);

        [DllImport("DualCardDll.dll")]
        public static extern int DEA_Req_AuthkeyWrite(int nPort, byte requestmode, byte authmode, byte blockno, byte[] keydata, byte[] data, out int outlen, byte[] lpRes);
    }
}
