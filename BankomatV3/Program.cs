using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankomatV3
{
    internal class Program
    {
        //static List<Banche> banche; 
        static SqlConnection sqlConnection;
        static void Main(string[] args)
        {
            //var ctx = new BankomatEntities1();
            // Inizializza();
            UserInterface UI = new UserInterface();
            UI.Esegui();
        }
    }
}
