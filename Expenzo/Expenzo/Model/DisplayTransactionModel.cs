using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenzo.Model
{
    public class DisplayTransactionModel
    {
        public DisplayTransactionModel()
        {
        }

        public int TransactionId { get; set; }

        public string Title { get; set; }

        public decimal Amount { get; set; }

        public string CategoryId { get; set; }

        public string TagId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DisplayTransactionModel(int transactionId, string title, decimal amount, string categoryId, DateTime createdDate, string tagId)
        {
            TransactionId = transactionId;
            Title = title;
            Amount = amount;
            CategoryId = categoryId;
            CreatedDate = createdDate;
            TagId = tagId;
        }
    }
}
