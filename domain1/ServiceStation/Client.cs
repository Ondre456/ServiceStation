using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStation
{
    public class Client : IItem
    {

        public Client(int id, string name, string desc, string em, string tel)
        {
            Id = id;
            Имя = name;
            Описание = desc;
            email = em;
            Телефон = tel;
        }

        public readonly int Id;
        public readonly string Имя;
        public readonly string Описание;
        public readonly string email;
        public readonly string Телефон;

        public string tableName
        {
            get
            {
                return "Clients";
            }
        }
    }
}
