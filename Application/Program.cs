using Ninject;
using ServiceStation;

namespace ApplicationLayer
{
    public static class Program
    {
        private static int clientId;
        private static int serviceId;
        private static int contractId;
        public static void Main()
        {
            clientId = (int)SqlExecuter.Execute("SELECT MAX(Id) AS Id FROM Clients", "Clients").Rows[0]["Id"];
            serviceId = (int)SqlExecuter.Execute("SELECT MAX(Id) AS Id FROM Services", "Services").Rows[0]["Id"];
            contractId = (int)SqlExecuter.Execute("SELECT MAX(Id) AS Id FROM Contract", "Contract").Rows[0]["Id"];
        }
        public static int GetClientId()
        {
            return ++clientId;
        }
        public static int GetServiceId()
        {
            return ++serviceId;
        }
        public static int  GetContractId()
        {
            return ++contractId;
        }
    }
    
}
