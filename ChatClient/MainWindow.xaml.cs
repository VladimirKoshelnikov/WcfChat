using System;
using System.Collections.Generic;
using System.Linq;
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
using ServiceChat;

namespace ChatClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window , IServiceChatCallback
    {

        bool isConnected = false;
        ServiceChat.ServiceChatClient client;
        int ID = 0;


        public MainWindow()
        {
            InitializeComponent();
        }


        private void windowIsLoaded(object sender, RoutedEventArgs e)
        {
            client = new ServiceChatClient(ServiceChatClientBase.EndpointConfiguration.NetTcpBinding_IServiceChat);
        }

        void ConnectUser() { 
            if (!isConnected)
            {
                client.
                ID = client.Connect(tbUserName.Text);
                tbUserName.IsEnabled= false;
                btnConnectDisconnect.Content = "Disconnect";
                isConnected = true;
                
            }
        }

        void DisconnectUser() {
            if (isConnected)
            {
                tbUserName.IsEnabled = true;
                btnConnectDisconnect.Content = "Connect";
                isConnected = false;
            }
        }

        private void btnConnectDisconnectClick(object sender, RoutedEventArgs e)
        {
            if (isConnected)
            {
                DisconnectUser();
            }
            else
            {
                ConnectUser();
            }
        }

        public void MsgCallback(string msg, int ID)
        {
            lbChat.Items.Add(msg);
        }

    }
}
