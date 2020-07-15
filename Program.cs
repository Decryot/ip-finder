using System;

using System.Net;
using System.Net.NetworkInformation;

using System.Threading;
using System.Threading.Tasks;

namespace IP {
    class MainClass {
        
        public static void Main(string[] args) {

            // your address
            IPAddress[] addresses = GetIP();

            Thread.Sleep(500);
            for (int i = 0; i < addresses.Length; i++) {
                string ip = addresses[i].ToString();
                Console.WriteLine("IP Address {0}: {1}",(i+1),ip);
                Thread.Sleep(50);
            }
            Console.WriteLine(" ");

            Task task = new Task(GetWebIP);
            task.Start();
            task.Wait();

            Console.ReadKey();
        }

        static IPAddress[] GetIP() {
            string hostName = Dns.GetHostName();
            IPAddress[] addresses = Dns.GetHostEntry(hostName).AddressList;

            return addresses;
        }

        static async void GetWebIP()
        {
            // track website addresses
            string websiteURL = "web url here";

            Ping ping = new Ping();
            try {
                PingReply pingReply = await ping.SendPingAsync(websiteURL);
                Console.WriteLine("The address for: {0} is {1}", websiteURL, pingReply.Address);
                Console.WriteLine("Runtime: {0}s", (float)pingReply.RoundtripTime / 1000);
            }catch (Exception e) {
                Console.WriteLine("the website: '{0}' is null or formatted differently", websiteURL);
            }
        }
    }
}