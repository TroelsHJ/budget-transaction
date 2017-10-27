using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace budget_transaction.Models
{
    public class Category
    {
        [DisplayName("ID")]
        public int Id { get; set; }
        [DisplayName("Name")]
        public string Name { get; set; }
        [DisplayName("Parent Category")]
        public int FK_Category { get; set; }
        [DisplayName("Parent Category")]
        public string ParentName { get; set; }

        /*
        public static List<SelectListItem> ToList(bool includeNull)
        {
            // Fetch all categories and store them in a list
            List<Category> categories = Database.SelectAllCategories(true);

            // Fill a list of SelectListItem's with all categories
            List<SelectListItem> selectList = new List<SelectListItem>();

            if (includeNull)
            {
                selectList.Add(new SelectListItem() { Text = "No parent", Value = "0" });
            }

            foreach (Category category in categories)
            {
                selectList.Add(new SelectListItem() { Text = category.Name, Value = category.Id.ToString() });
            }

            return selectList;
        }
        */
        public void GetParentName()
        {
            ParentName = Database.SelectCategory(FK_Category, false).Name;

            if (ParentName == null)
            {
                ParentName = "";
            }
        }
    }
}