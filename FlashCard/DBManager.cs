///A. Seun Ajayi
///AjayiP8
///8/7/2022
///aajayi@cnm.edu

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace FlashCard
{
   
    public class DBManager
    {
        private string _connectionString;

        #region 
        public DBManager(string configKey)
        {
            _connectionString = ConfigurationManager.ConnectionStrings[configKey].ConnectionString;
        }

        public void GetCards(BindingList<Card> cards)
        {
            var sqlQuery = "SELECT * FROM Card";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var id = int.Parse(reader[0].ToString());
                        var numRight = int.Parse(reader[4].ToString());
                        var numWrong = int.Parse(reader[5].ToString());
                        var card = new Card(id, reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), numRight, numWrong);
                        cards.Add(card);
                    }

                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }

        }

        public void AddCard(Card card)
        {
            var sqlQuery = "INSERT INTO Card (Title, Question, Answer, NumRight, NumWrong) VALUES(@Title, @Question, @Answer, @NumRight, @NumWrong);";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                    cmd.Parameters.AddWithValue("@Title", card.Title);
                    cmd.Parameters.AddWithValue("@Question", card.Question);
                    cmd.Parameters.AddWithValue("@Answer", card.Answer);
                    cmd.Parameters.AddWithValue("@NumRight", card.NumRight);
                    cmd.Parameters.AddWithValue("@NumWrong", card.NumWrong);
                    int rowsReturned = cmd.ExecuteNonQuery();
                    Console.WriteLine("{0} rows returned.", rowsReturned);
                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public void RemoveCard(Card card)
        {
            var sqlQuery = "DELETE FROM Card WHERE CardID = @CardID";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                    cmd.Parameters.AddWithValue("@CardID", card.CardID);
                    int rowsReturned = cmd.ExecuteNonQuery();
                    Console.WriteLine("{0} rows returned.", rowsReturned);
                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public void UpdateCard(Card card)
        {
            var sqlQuery = "UPDATE Card SET Title = @Title, Question = @Question, Answer = @Answer, NumRight = @NumRight, NumWrong = @NumWrong WHERE CardID = @CardID;";

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                    cmd.Parameters.AddWithValue("@CardID", card.CardID);
                    cmd.Parameters.AddWithValue("@Title", card.Title);
                    cmd.Parameters.AddWithValue("@Question", card.Question);
                    cmd.Parameters.AddWithValue("@Answer", card.Answer);
                    cmd.Parameters.AddWithValue("@NumRight", card.NumRight);
                    cmd.Parameters.AddWithValue("@NumWrong", card.NumWrong);
                    int rowsReturned = cmd.ExecuteNonQuery();
                    Console.WriteLine("{0} rows returned.", rowsReturned);
                    conn.Close();
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        #endregion
    }
}
