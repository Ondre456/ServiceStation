using ApplicationLayer;
using ServiceStation;
using System;
using System.Windows;


namespace UserInterface
{
    /// <summary>
    /// Логика взаимодействия для AddService.xaml
    /// </summary>
    public partial class AddService : Window
    {
        public AddService()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var rnd = new Random();
            SqlExecuter.Insert(new Service(Program.GetServiceId(), name.Text, int.Parse(price.Text)));
            this.Close();
        }
    }
}
