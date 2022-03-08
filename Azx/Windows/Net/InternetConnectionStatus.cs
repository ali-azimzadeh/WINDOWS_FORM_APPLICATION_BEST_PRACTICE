using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azx.Windows.Net
{
    public static class InternetConnectionStatus
    {
        public static bool IsConnected()
        {
            InternetConnectionState flags =
                InternetConnectionState.INTERNET_CONNECTION_CONFIGURED;
            
            bool isConnected = 
                Windows.API.API.InternetGetConnectedState(ref flags, 0);

            return (isConnected);
        }

        public static bool IsInternetConnectionAvailable()
        {
            try
            {
                System.Net.IPHostEntry ipHe =
                           System.Net.Dns.GetHostEntry("www.google.com");

                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    public enum InternetConnectionState : int
    {
        INTERNET_CONNECTION_MODEM = 0x18,

        INTERNET_CONNECTION_LAN = 0x2,

        INTERNET_CONNECTION_PROXY = 0x4,

        INTERNET_RAS_INSTALLED = 0x10,

        INTERNET_CONNECTION_OFFLINE = 0x20,

        INTERNET_CONNECTION_CONFIGURED = 0x40
    }
}
