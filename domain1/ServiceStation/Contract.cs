using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStation
{
    public class Contract : IItem
    {
        public Contract(int id, string win, int serviceId, string status = "In Process")
        {
            Id = id;
            this.win = win;
            ServiceId = serviceId;
            Статус = status;
        }

        public readonly int Id;
        public readonly string win;
        public readonly int ServiceId;
        public readonly string Статус;

        public string tableName
        {
            get
            {
                return "Contract";
            }
        }
    }
}
