using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Expenzo.Model
{
    public class Debt
    {

        public int DebtId { get; set; }

        public string Source { get; set; }

        public DateTime DueDate { get; set; }

        public string Status { get; set; }

        public int TransactionId { get; set; }
    }
}
