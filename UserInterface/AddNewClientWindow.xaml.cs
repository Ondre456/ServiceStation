using ApplicationLayer;
using ServiceStation;
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
using System.Windows.Shapes;

namespace UserInterface
{
    /// <summary>
    /// Логика взаимодействия для AddNewClientWindow.xaml
    /// </summary>
    public partial class AddNewClientWindow : Window
    {
        public AddNewClientWindow(/*string cased*/)
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var rnd = new Random();
            var client = new Client(Program.GetClientId(), name.Text, desc.Text, mail.Text, phone.Text);
            SqlExecuter.Insert(client);
            var addCar = new AddCarWindow(client.Id);
            addCar.Show();
            this.Close();
        }
    }
}
