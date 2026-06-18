using cts_twister_api.model;
using Newtonsoft.Json;
using System.Net;

namespace cts_twister_api.common
{
    public class ResHost
    {
        public static MDHostInfo GetHostInfo(HttpContext httpContext)
        {
            var remoteIpAddress = httpContext.Connection.RemoteIpAddress?.ToString();
            var hostName = "";
            if (remoteIpAddress == "::1")
            {
                remoteIpAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0].ToString();
                hostName = Dns.GetHostEntry(Dns.GetHostName()).HostName;
            }


            var host = new MDHostInfo
            {
                HostIp = remoteIpAddress,
                HostName = hostName,
                HostUser = "",
            };

            return host;
        }
    }
}
