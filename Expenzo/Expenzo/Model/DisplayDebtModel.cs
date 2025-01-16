using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenzo.Model
{
    public class DisplayDebtModel
    {

        public int TransactionId { get; set; }
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Note { get; set; }
        public string TagId { get; set; }
        public int UserId { get; set; }
        public string CategoryId { get; set; }
        public int DebtId { get; set; }
        public string Source { get; set; }
        public DateTime DueDate { get; set; }
        public string Status { get; set; }

        public DisplayDebtModel(int transactionId, string title, decimal amount, DateTime createdDate, string? note, string tagId, int userId, string categoryId)
        {
            TransactionId = transactionId;
            Title = title;
            Amount = amount;
            CreatedDate = createdDate;
            Note = note;
            TagId = tagId;
            UserId = userId;
            CategoryId = categoryId;
        }

        public DisplayDebtModel()
        {
        }
    }
}
