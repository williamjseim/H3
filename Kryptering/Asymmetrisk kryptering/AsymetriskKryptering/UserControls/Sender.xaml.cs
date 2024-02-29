using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace AsymetriskKryptering.UserControls
{
    /// <summary>
    /// Interaction logic for Sender.xaml
    /// </summary>
    public partial class Sender : UserControl
    {
        SignalrConnector connector;
        RSA rsa;
        public Sender()
        {
            InitializeComponent();
            rsa = RSA.Create();
            ConnectButton.Click += Connect;
            this.connector = new();
            SendButton.Click += connector.SendMessage;
        }

        private void Connect(object sender, EventArgs args)
        {
        }
    }
}
