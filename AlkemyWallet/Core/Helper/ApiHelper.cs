using System.Net;

namespace AlkemyWallet.Core.Helper
{
    public class ApiHelper
    {
        public static string GetIpAddress()
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.Equals(System.Net.Sockets.AddressFamily.InterNetwork))
                {
                    return ip.ToString();
                }
            }
            return String.Empty;
        }
    }
}
