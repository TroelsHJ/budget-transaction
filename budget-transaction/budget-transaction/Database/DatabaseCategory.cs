using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using budget_transaction.Models;

namespace budget_transaction
{
    public static partial class Database
    {
        public static Category SelectCategory(int id, bool getParentName)
        {
            OpenConnection();

            Category category = new Category();

            SqlCommand command = new SqlCommand("SELECT * FROM Category WHERE Id = @Id", connection);
            command.Parameters.Add(CreateParam("@Id", id, SqlDbType.Int));

            try
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        category.Id = Int32.Parse(reader["Id"].ToString());
                        category.Name = reader["Name"].ToString();

                        int temp;

                        if (Int32.TryParse(reader["FK_Category"].ToString(), out temp))
                        {
                            category.FK_Category = temp;
                        }
                        else
                        {
                            category.FK_Category = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            CloseConnection();

            if (getParentName)
            {
            category.GetParentName();
            }

            return category;
        }

        public static List<Category> SelectAllCategories(bool getParentName)
        {
            OpenConnection();

            List<Category> categories = new List<Category>();

            SqlCommand command = new SqlCommand("SELECT * FROM Category", connection);

            try
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Category category = new Category();

                        category.Id = Int32.Parse(reader["Id"].ToString());
                        category.Name = reader["Name"].ToString();

                        int temp;

                        if (Int32.TryParse(reader["FK_Category"].ToString(), out temp))
                        {
                            category.FK_Category = temp;
                        }
                        else
                        {
                            category.FK_Category = 0;
                        }

                        categories.Add(category);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            CloseConnection();

            if (getParentName)
            {
                foreach (Category category in categories)
                {
                    category.GetParentName();
                }
            }

            return categories;
        }

        public static void InsertCategory(Category category)
        {
            OpenConnection();

            SqlCommand command;

            if (category.FK_Category == 0)
            {
                command = new SqlCommand("INSERT INTO Category (Name, FK_Category) VALUES (@Name, NULL)", connection);
            }
            else
            {
                command = new SqlCommand("INSERT INTO Category (Name, FK_Category) VALUES (@Name, @FK_Category)", connection);
            }

            command.Parameters.Add(CreateParam("@Name", category.Name, SqlDbType.NVarChar));
            command.Parameters.Add(CreateParam("@FK_Category", category.FK_Category, SqlDbType.Int));
            try
            {
                command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            CloseConnection();
        }

        public static void UpdateCategory(Category category)
        {
            OpenConnection();

            SqlCommand command;

            if (category.FK_Category == 0)
            {
                command = new SqlCommand("UPDATE Category SET Name = @Name, FK_Category = NULL WHERE Id = @Id", connection);
            }
            else
            {
                command = new SqlCommand("UPDATE Category SET Name = @Name, FK_Category = @FK_Category WHERE Id = @Id", connection);
            }
       
            command.Parameters.Add(CreateParam("@Id", category.Id, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@Name", category.Name, SqlDbType.NVarChar));
            command.Parameters.Add(CreateParam("@FK_Category", category.FK_Category, SqlDbType.Int));

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            CloseConnection();
        }

        public static void DeleteCategory(int id)
        {
            OpenConnection();

            SqlCommand command = new SqlCommand("DELETE FROM Category WHERE Id = @Id", connection);
            command.Parameters.Add(CreateParam("@Id", id, SqlDbType.Int));

            try
            {
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            CloseConnection();
        }
    }
}