using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace budget_transaction.Models
{
    public class Transaction
    {
        [DisplayName("ID")]
        public int Id { get; set; }
        [DisplayName("Value")]
        public decimal Value { get; set; }
        [DisplayName("Text")]
        public string Text { get; set; }
        [DisplayName("Date")]
        public DateTime Date { get; set; }
        [DisplayName("Category")]
        public int FK_Category { get; set; }
        [DisplayName("Active")]
        public bool Active { get; set; }

        // Extra property to handle property name
        [DisplayName("Category")]
        public string CategoryName { get; set; }

        public void GetCategoryName()
        {
            CategoryName = Database.SelectCategory(FK_Category, false).Name;
        }
    }
}