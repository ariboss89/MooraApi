using MooraMethodApi.Models;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MooraMethodApi.Controllers
{
    public class KriteriaController : ApiController
    {
        string connStr = ConfigurationManager.ConnectionStrings["mySql"].ConnectionString;

        public IEnumerable<tb_kriteria> GetAllKriteria()
        {
            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                _connection.Open();
                MySqlCommand _command = new MySqlCommand("SELECT *FROM tb_kriteria", _connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(_command);
                DataTable results = new DataTable();

                adapter.Fill(results);
                _connection.Close();

                if (results == null)
                {
                    NotFound();
                }

                var srlJson = JsonConvert.SerializeObject(results);

                return JsonConvert.DeserializeObject<List<tb_kriteria>>(srlJson);
            }

        }

        [HttpPost]
        public void SaveKriteria(tb_kriteria ktr)
        {
            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "INSERT INTO tb_kriteria (nama, bobot) Values (@nama, @bobot)";
                sqlCmd.Connection = _connection;

                sqlCmd.Parameters.AddWithValue("@nama", ktr.nama);
                sqlCmd.Parameters.AddWithValue("@bobot", ktr.bobot);

                _connection.Open();
                int rowInserted = sqlCmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        [HttpPut]
        public void UpdateKriteria(tb_kriteria ktr)
        {
            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "UPDATE tb_kriteria SET nama = @nama, bobot=@bobot WHERE Id= @id";
                sqlCmd.Connection = _connection;

                sqlCmd.Parameters.AddWithValue("@id", ktr.Id);
                sqlCmd.Parameters.AddWithValue("@nama", ktr.nama);
                sqlCmd.Parameters.AddWithValue("@bobot", ktr.bobot);

                _connection.Open();
                int rowUpdatd = sqlCmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        [HttpDelete]
        public void DeleteKriteria(int Id)
        {
            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "delete from tb_kriteria where Id = " + Id + "";
                sqlCmd.Connection = _connection;
                _connection.Open();
                int rowDeleted = sqlCmd.ExecuteNonQuery();
                _connection.Close();
            }
        }
    }
}
