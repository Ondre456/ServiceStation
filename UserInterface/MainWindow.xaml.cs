using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using ApplicationLayer;
using ServiceStation;

namespace UserInterface
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataBaseToGrid("Clients");
            Program.Main();
            TablesChanger.ItemsSource = new[]
            {
                "Clients",
                "Cars",
                "Services"
            };
            TablesChanger.SelectedItem = "Clients";
            clientWindow.Visibility = Visibility.Hidden;
        }

        private void DataBaseToGrid(string tableName)
        {
            MainDataGrid.ItemsSource = SqlExecuter.Select(tableName).DefaultView;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            MainDataGrid.HorizontalAlignment = HorizontalAlignment.Left;
            MainDataGrid.Width = ActualWidth;
            MainDataGrid.Height = ActualHeight;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddNewClientWindow addNewClientWindow = new AddNewClientWindow();
            addNewClientWindow.Show();
        }

        private void addService_Click(object sender, RoutedEventArgs e)
        {
            var addNewClientWindow = new AddService();
            addNewClientWindow.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DataBaseToGrid(TablesChanger.SelectedItem.ToString());
        }

        private void TablesChanger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataBaseToGrid(TablesChanger.SelectedItem.ToString());
            switch (TablesChanger.SelectedItem.ToString())
            { 
                case "Clients":
                    {
                        addButton.Visibility = Visibility.Visible;
                        clientWindow.Visibility = Visibility.Hidden;
                        addButton.Click += Button_Click;
                        addButton.Click -= addService_Click;
                        clientWindow.Content = "Открыть окно пользователя";
                    }
                    break;
                case "Services":
                    {
                        addButton.Visibility = Visibility.Visible;
                        addButton.Click -= Button_Click;
                        addButton.Click += addService_Click;
                    }
                break;
                case "Cars":
                    {
                        addButton.Visibility = Visibility.Hidden;
                        clientWindow.Content = "Открыть окно автомобиля";
                        addButton.Click -= addService_Click;
                        addButton.Click -= Button_Click;
                    }
                break;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DataRowView rowView = (DataRowView)MainDataGrid.SelectedItem;
            DataRow row = rowView.Row;
            switch (TablesChanger.SelectedItem.ToString())
            {
                case "Clients":
                    {
                        var client = new Client((int)row["Id"], (string)row["Имя"], (string)row["Описание"], (string)row["Email"], (string)row["Телефон"]);
                        var wc = new ClientWindow(client);
                        wc.Show();
                    }break;
                case "Cars":
                    {
                        var str = (string)row["Год_Выпуска"];
                        var parts = str.Split('.',' ');
                        var mark = (string)row["Марка"];
                        var model = (string)row["Модель"];
                        var win = (string)row["Win"];
                        var owner = (int)row["Владелец"];
                        var num = (int)row["Номер_Двигателя"];
                        var status = (string)row["Статус"];
                        var car = new Car(mark, model, new System.DateTime(int.Parse(parts[2]),int.Parse(parts[1]),int.Parse(parts[0])), win, owner, num, status);
                        var cw = new CarWindow(car);
                        cw.Show();
                    }break;
            }
        }

        private void MainDataGrid_Selected(object sender, RoutedEventArgs e)
        {
            if (TablesChanger.SelectedItem.ToString() != "Services")
            clientWindow.Visibility = Visibility.Visible;
        }

        private void MainDataGrid_Unselected(object sender, RoutedEventArgs e)
        {
            clientWindow.Visibility = Visibility.Hidden;
        }
    }
}
