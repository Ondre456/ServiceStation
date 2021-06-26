using ApplicationLayer;
using ServiceStation;
using System;
using System.Data;
using System.Diagnostics.Contracts;
using System.Windows;

namespace UserInterface
{
    /// <summary>
    /// Логика взаимодействия для CarWindow.xaml
    /// </summary>
    public partial class CarWindow : Window
    {
        private Car car;
        public CarWindow(Car car)
        {
            InitializeComponent();
            model.Text = car.Модель;
            mark.Text = car.Марка;
            year.Text = car.Год_Выпуска.ToString();
            win.Text = car.WIN;
            this.car = car;
            contractGrid.ItemsSource = SqlExecuter.Execute("SELECT e.Название, e.Цена, t.Статус FROM Contract t LEFT JOIN Services e ON t.ServiceId = e.Id WHERE t.win = '" + car.WIN +"'", "Contracts").DefaultView;
            serviceBase.ItemsSource = SqlExecuter.Execute("SELECT * FROM Services","Services").DefaultView;
            addContract.Visibility = Visibility.Hidden;
            if (car.Статус == "In Service")
                toArchive.Click += toArchive_Click;
            else
            {
                toArchive.Content = "Вернуть в сервис";
                toArchive.Click += toService_Click;
            }
        }

        private void serviceBase_Selected(object sender, RoutedEventArgs e)
        {
            addContract.Visibility = Visibility.Visible;
        }

        private void serviceBase_Unselected(object sender, RoutedEventArgs e)
        {
            addContract.Visibility = Visibility.Hidden;
        }

        private void addContract_Click(object sender, RoutedEventArgs e)
        {
            var rnd = new Random();
            DataRowView rowView = (DataRowView)serviceBase.SelectedItem;
            DataRow row = rowView.Row;
            SqlExecuter.Insert(new ServiceStation.Contract(Program.GetContractId(), car.WIN, (int)row["Id"]));
            contractGrid.ItemsSource = SqlExecuter.Execute("SELECT e.Название, e.Цена, t.Статус FROM Contract t LEFT JOIN Services e ON t.ServiceId = e.Id WHERE t.win = '" + car.WIN + "'", "Contracts").DefaultView;
            serviceBase.ItemsSource = SqlExecuter.Execute("SELECT * FROM Services", "Services").DefaultView;
            if (car.Статус == "In Archive")
            {
                SqlExecuter.Execute("UPDATE Cars SET Статус = 'In Service' WHERE WIN = '" + car.WIN + "'");
                car = new Car(car.Марка, car.Модель, car.Год_Выпуска, car.WIN, car.Владелец, car.Номер_Двигателя, "In Service");
            }
            if (car.Статус == "In Archive")
            {
                SqlExecuter.Execute("UPDATE Cars SET Статус = 'In Service' WHERE WIN = '" + car.WIN + "'");
                contractGrid.ItemsSource = SqlExecuter.Execute("SELECT e.Название, e.Цена, t.Статус FROM Contract t LEFT JOIN Services e ON t.ServiceId = e.Id WHERE t.win = '" + car.WIN + "'", "Contracts").DefaultView;
                car = new Car(car.Марка, car.Модель, car.Год_Выпуска, car.WIN, car.Владелец, car.Номер_Двигателя, "In Service");
                toArchive.Content = "В архив";
            }
        }

        private void toService_Click(object sender, RoutedEventArgs e)
        {
            SqlExecuter.Execute("UPDATE Cars SET Статус = 'In Service' WHERE WIN = '" + car.WIN + "'");
            contractGrid.ItemsSource = SqlExecuter.Execute("SELECT e.Название, e.Цена, t.Статус FROM Contract t LEFT JOIN Services e ON t.ServiceId = e.Id WHERE t.win = '" + car.WIN + "'", "Contracts").DefaultView;
            car = new Car(car.Марка, car.Модель, car.Год_Выпуска, car.WIN, car.Владелец, car.Номер_Двигателя, "In Service");
            toArchive.Content = "В архив";
            toArchive.Click += toArchive_Click;
        }

        private void toArchive_Click(object sender, RoutedEventArgs e)
        {
            SqlExecuter.Execute("UPDATE Contract SET Статус = 'Done' WHERE win = '" + car.WIN + "'");
            SqlExecuter.Execute("UPDATE Cars SET Статус = 'In Archive' WHERE WIN = '" + car.WIN + "'");
            contractGrid.ItemsSource = SqlExecuter.Execute("SELECT e.Название, e.Цена, t.Статус FROM Contract t LEFT JOIN Services e ON t.ServiceId = e.Id WHERE t.win = '" + car.WIN + "'", "Contracts").DefaultView;
            car = new Car(car.Марка,car.Модель,car.Год_Выпуска,car.WIN,car.Владелец,car.Номер_Двигателя,"In Archive");
            contractGrid.ItemsSource = SqlExecuter.Execute("SELECT e.Название, e.Цена, t.Статус FROM Contract t LEFT JOIN Services e ON t.ServiceId = e.Id WHERE t.win = '" + car.WIN + "'", "Contracts").DefaultView;
            var sm = new SendMail();
            sm.Show();
            toArchive.Content = "Вернуть в сервис";
        }
    }
}
