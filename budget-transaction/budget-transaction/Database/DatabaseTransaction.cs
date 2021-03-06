﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using budget_transaction.Models;

namespace budget_transaction
{
    public static partial class Database
    {
        public static Transaction SelectTransaction(int id, bool getName)
        {
            OpenConnection();

            Transaction transaction = new Transaction();

            SqlCommand command = new SqlCommand("SELECT * FROM [Transaction] WHERE Id = @Id", connection);
            command.Parameters.Add(CreateParam("@Id", id, SqlDbType.Int));

            try
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        transaction.Id = id;
                        transaction.Value = Decimal.Parse(reader["Value"].ToString());
                        transaction.Text = reader["Text"].ToString();
                        transaction.Date = DateTime.Parse(reader["Date"].ToString());
                        transaction.Active = Boolean.Parse(reader["Active"].ToString());
                        transaction.FK_Category = Int32.Parse(reader["FK_Category"].ToString());    
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            CloseConnection();
            
            if (getName)
            {
                transaction.GetCategoryName();
            }

            return transaction;
        }

        public static List<Transaction> SelectAllTransactions(bool getName)
        {
            OpenConnection();

            List<Transaction> transactions = new List<Transaction>();

            SqlCommand command = new SqlCommand("SELECT * FROM [Transaction]", connection);

            try
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Transaction transaction = new Transaction();

                        transaction.Id = Int32.Parse(reader["Id"].ToString());
                        transaction.Value = Decimal.Parse(reader["Value"].ToString());
                        transaction.Text = reader["Text"].ToString();
                        transaction.Date = DateTime.Parse(reader["Date"].ToString());
                        transaction.Active = Convert.ToBoolean(reader["Active"].ToString());
                        transaction.FK_Category = Int32.Parse(reader["FK_Category"].ToString());

                        transactions.Add(transaction);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            CloseConnection();

            if (getName)
            {
                foreach (Transaction transaction in transactions)
                {
                    transaction.GetCategoryName();
                }
            }

            return transactions;
        }

        public static List<Transaction> SelectAllTransactions(bool getName, DateTime StartDate, DateTime EndDate)
        {
            OpenConnection();

            List<Transaction> transactions = new List<Transaction>();

            SqlCommand command = new SqlCommand("SELECT * FROM [Transaction] WHERE Date BETWEEN @start and @end", connection);
            command.Parameters.Add(CreateParam("@start", StartDate, SqlDbType.DateTime));
            command.Parameters.Add(CreateParam("@end", EndDate, SqlDbType.DateTime));

            try
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Transaction transaction = new Transaction();

                        transaction.Id = Int32.Parse(reader["Id"].ToString());
                        transaction.Value = Decimal.Parse(reader["Value"].ToString());
                        transaction.Text = reader["Text"].ToString();
                        transaction.Date = DateTime.Parse(reader["Date"].ToString());
                        transaction.Active = Convert.ToBoolean(reader["Active"].ToString());
                        transaction.FK_Category = Int32.Parse(reader["FK_Category"].ToString());

                        transactions.Add(transaction);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            CloseConnection();

            if (getName)
            {
                foreach (Transaction transaction in transactions)
                {
                    transaction.GetCategoryName();
                }
            }

            return transactions;
        }

        public static void InsertTransaction(Transaction transaction)
        {
            OpenConnection();

            SqlCommand command = new SqlCommand("INSERT INTO [Transaction] (Value, Text, FK_Category, Active) VALUES (@Value, @Text, @FK_Category, @Active)", connection);
            command.Parameters.Add(CreateParam("@Value", transaction.Value, SqlDbType.Decimal));
            command.Parameters.Add(CreateParam("@Text", transaction.Text, SqlDbType.NVarChar));
            command.Parameters.Add(CreateParam("@FK_Category", transaction.FK_Category, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@Active", transaction.Active, SqlDbType.Bit));

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

        public static void UpdateTransaction(Transaction transaction)
        {
            OpenConnection();

            SqlCommand command = new SqlCommand("UPDATE [Transaction] SET Value = @Value, Text = @Text, FK_Category = @FK_Category, Active = @Active WHERE Id = @Id", connection);
            command.Parameters.Add(CreateParam("@Id", transaction.Id, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@Value", transaction.Value, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@Text", transaction.Text, SqlDbType.NVarChar));
            command.Parameters.Add(CreateParam("@FK_Category", transaction.FK_Category, SqlDbType.Int));
            command.Parameters.Add(CreateParam("@Active", transaction.Active, SqlDbType.Bit));

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

        public static void DeleteTransaction(int id)
        {
            OpenConnection();

            SqlCommand command = new SqlCommand("DELETE FROM [Transaction] WHERE Id = @Id", connection);
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