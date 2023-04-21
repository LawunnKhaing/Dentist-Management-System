using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment15
{
    internal class Dentist
    {
        public Dentist(int id, string name, string telNum)
        {
            Id = id;
            Name = name;
            TelNum = telNum;
        }

        public Dentist()
        {
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string TelNum { get; set; }


        public List<Dentist> GetAllDentists(SqlConnection myConnection)
        {
            List<Dentist> dentists = new List<Dentist>();
            string query = "SELECT * FROM Dentist";
            SqlCommand cmd = new SqlCommand(query, myConnection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Dentist dentist = new Dentist();
                dentist.Id = (int)reader["Id"];
                dentist.Name = (string)reader["Name"];
                dentist.TelNum = (string)reader["TelNum"];
                dentists.Add(dentist);
            }
            reader.Close();
            return dentists;
        }

        public void InsertDentist(SqlConnection myConnection, Dentist dentist)
        {
            string query = "INSERT INTO Dentist (Name, TelNum) VALUES (@name, @telnum)";
            SqlCommand cmd = new SqlCommand(query, myConnection);
            cmd.Parameters.AddWithValue("@name", dentist.Name);
            cmd.Parameters.AddWithValue("@telnum", dentist.TelNum);
            cmd.ExecuteNonQuery();
        }

        public List<Dentist> FindDentist(SqlConnection myConnection, string name)
        {
            List<Dentist> dentists = new List<Dentist>();
            string query = "SELECT * FROM Dentist WHERE Name LIKE '%' + @name + '%'";
            SqlCommand cmd = new SqlCommand(query, myConnection);
            cmd.Parameters.AddWithValue("@name", name);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Dentist dentist = new Dentist();
                dentist.Id = (int)reader["Id"];
                dentist.Name = (string)reader["Name"];
                dentist.TelNum = (string)reader["TelNum"];
                dentists.Add(dentist);
            }
            reader.Close();
            return dentists;
        }

        public void DeleteDentist(SqlConnection myConnection, Dentist dentist)
        {
            string query = "DELETE FROM Dentist WHERE Id = @id";
            SqlCommand cmd = new SqlCommand(query, myConnection);
            cmd.Parameters.AddWithValue("@id", dentist.Id);
            cmd.ExecuteNonQuery();
        }

        public void UpdateDentist(SqlConnection myConnection, Dentist dentist)
        {
            string query = "UPDATE Dentist SET TelNum = @telnum WHERE Id = @id";
            SqlCommand cmd = new SqlCommand(query, myConnection);
            cmd.Parameters.AddWithValue("@telnum", dentist.TelNum);
            cmd.Parameters.AddWithValue("@id", dentist.Id);
            cmd.ExecuteNonQuery();
        }

    }

}
