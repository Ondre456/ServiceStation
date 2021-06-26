using ApplicationLayer;
using System;
using System.Collections.Generic;
using System.Windows;
using ServiceStation;

namespace UserInterface
{
    /// <summary>
    /// Логика взаимодействия для AddCarWindow.xaml
    /// </summary>
    public partial class AddCarWindow : Window
    {
        private int Id;
        public AddCarWindow(int id)
        {
            InitializeComponent();
            Id = id;
            var dates = new List<int>();
            for (var i = 1; i <= 31; i++)
                dates.Add(i);
            date.ItemsSource = dates;
            var months = new List<int>();
            for (var i = 1; i <= 12; i++)
            {
                months.Add(i);
            }
            month.ItemsSource = months;
            var years = new List<int>();
            for (var i = 1960; i < DateTime.Now.Year; i++)
            {
                years.Add(i);
            }
            year.ItemsSource = years;
        }

        private void adopt_Click(object sender, RoutedEventArgs e)
        { 
            var car = new Car(marc.Text, model.Text,new DateTime((int)year.SelectedItem, (int)month.SelectedItem, (int)date.SelectedItem),
                win.Text, Id, int.Parse(number.Text), "In Service");
            SqlExecuter.Insert(car);
            this.Close();
        }
    }
}
