using System;

namespace ServiceStation
{
    public class Car : IItem
    {
        public Car(string marc, string model, DateTime date, string win, int owner, int number, string status)
        {
            Марка = marc;
            Модель = model;
            Год_Выпуска = date;
            WIN = win;
            Владелец = owner;
            Номер_Двигателя = number;
            Статус = status;
        }

        public readonly string Марка;
        public readonly string Модель;
        public readonly DateTime Год_Выпуска;
        public readonly string WIN;
        public readonly int Владелец;
        public readonly int Номер_Двигателя;
        public readonly string Статус;

        public string tableName
        {
            get
            {
                return "Cars";
            }
        }
    }
}