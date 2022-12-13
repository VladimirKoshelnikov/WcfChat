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
using WpfChat.ServiceChat;

namespace WpfChat
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window, IServiceChatCallback
    {

        bool isConnected = false;
        ServiceChat.ServiceChatClient client;
        int ID = 0;


        public MainWindow()
        {
            InitializeComponent();
        }


        void ConnectUser()
        {
            if (!isConnected)
            {
                client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
                ID = client.Connect(tbUserName.Text);
                tbUserName.IsEnabled = false;
                btnConnectDisconnect.Content = "Disconnect";
                isConnected = true;

            }
        }

        void DisconnectUser()
        {
            if (isConnected)
            {
                client.Disconnect(ID);
                client = null;
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

        public void MsgCallback(string msg)
        {
            lbChat.Items.Add(msg);
            lbChat.ScrollIntoView(lbChat.Items.Count- 1);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();
        }

        private void tbMsgText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key== Key.Enter) {
                client.SendMsg(tbMsgText.Text, ID);
                tbMsgText.Text = "";
            }
        }
    }
}
