using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MCServerInformation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            IPHostEntry hostEntry;

            string hostName = "85.148.126.63";
            string ip = "";
            if (!isValidIP(hostName))
            {
                hostEntry = Dns.GetHostEntry(hostName);
                if (hostEntry.AddressList.Length > 0)
                {
                    ip = hostEntry.AddressList[0].ToString();
                }
            }
            else
            {
                ip = hostName;
            }
            
            MCServerInfo msi = getMCServerInfo(ip);

        }

        private MCServerInfo getMCServerInfo(string ip)
        {
            TcpClient client = new TcpClient(ip, 25565);
            Byte[] data = { 0x01, 0x00 };

            NetworkStream stream = client.GetStream();

            Byte[] init_bytes = {
              0x14, 0x00, 0xf3, 0x05, 0x0d, 0x38, 0x35, 0x2e,
              0x31, 0x34, 0x38, 0x2e, 0x31, 0x32, 0x36, 0x2e,
              0x36, 0x33, 0x63, 0xdd, 0x01
            };

            stream.Write(init_bytes, 0, init_bytes.Length);
            stream.Write(data, 0, data.Length);

            // Buffer to store the response bytes.
            data = new Byte[1024];

            // String to store the response ASCII representation.
            String responseData = String.Empty;
            int bytesToRead;

            while ((bytesToRead = stream.Read(data, 0, data.Length)) != 0)
            {
                responseData += System.Text.Encoding.ASCII.GetString(data, 0, bytesToRead);
            }
            string jsonResponse = responseData.Substring(5);
            Console.WriteLine(jsonResponse);
            MCServerInfo mcServerInfo = JsonConvert.DeserializeObject<MCServerInfo>(jsonResponse);

            // Close everything.
            stream.Close();
            client.Close();

            return mcServerInfo;
        }
        bool isValidIP(string ip)
        {
            IPAddress dummy;
            return IPAddress.TryParse(ip, out dummy);
        }
    }
}
