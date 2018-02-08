using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace ContactApp.Extensions
{
    public static class ExtensionMethods
    {
        public static string GetLocalIPAddress()
        {
            //string  output = "";
            //foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            //{
            //    if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
            //    {
            //        foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
            //        {

            //                output  += $"{ip.Address} {ip.Address.AddressFamily} \n";

            //        }
            //    }
            //}
            //return output;

            var host = Dns.GetHostEntry(Dns.GetHostName());
            string Address = "";
            foreach (var ip in host.AddressList)
            {

                Address += $"{host.AddressList.Length} {ip} {ip.AddressFamily} /n";

            }
            return Address;
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
