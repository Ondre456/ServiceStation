using ServiceStation;
using System.Windows;
using ApplicationLayer;
using System.Data;

namespace UserInterface
{
    /// <summary>
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        private Client client;
        public ClientWindow(Client client)
        {
            InitializeComponent();
            name.Text = client.Имя;
            desc.Text = client.Описание;
            email.Text = client.email;
            phone.Text = client.Телефон;
            this.client = client;
            var query = "SELECT * FROM Cars t WHERE t.Владелец = " + client.Id;
            grid.ItemsSource = SqlExecuter.Execute(query, "Cars").DefaultView;
            carWindowopen.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var cw = new AddCarWindow(client.Id);
            cw.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var query = "SELECT * FROM Cars t WHERE t.Владелец = " + client.Id;
            grid.ItemsSource = SqlExecuter.Execute(query, "Cars").DefaultView;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DataRowView rowView = (DataRowView)grid.SelectedItem;
            DataRow row = rowView.Row;
            var str = (string)row["Год_Выпуска"];
            var parts = str.Split('.', ' ');
            var mark = (string)row["Марка"];
            var model = (string)row["Модель"];
            var win = (string)row["Win"];
            var owner = (int)row["Владелец"];
            var num = (int)row["Номер_Двигателя"];
            var status = (string)row["Статус"];
            var car = new Car(mark, model, new System.DateTime(int.Parse(parts[2]), int.Parse(parts[1]), int.Parse(parts[0])), win, owner, num, status);
            var cw = new CarWindow(car);
            cw.Show();
        }

        private void grid_Selected(object sender, RoutedEventArgs e)
        {
            carWindowopen.Visibility = Visibility.Visible;
        }

        private void grid_Unselected(object sender, RoutedEventArgs e)
        {
            carWindowopen.Visibility = Visibility.Hidden;
        }
    }
}
