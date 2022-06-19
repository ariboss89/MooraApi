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
    public class AlternatifController : ApiController
    {
        string connStr = ConfigurationManager.ConnectionStrings["mySql"].ConnectionString;

        public IEnumerable<tb_alternatif> GetAllAlternatif()
        {
            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                _connection.Open();
                MySqlCommand _command = new MySqlCommand("SELECT *FROM tb_alternatif", _connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(_command);
                DataTable results = new DataTable();

                adapter.Fill(results);
                _connection.Close();

                if (results == null)
                {
                    NotFound();
                }

                var srlJson = JsonConvert.SerializeObject(results);

                return JsonConvert.DeserializeObject<List<tb_alternatif>>(srlJson);
            }

        }

        [HttpPost]
        public void SaveAlternatif(tb_alternatif atr)
        {
            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "INSERT INTO tb_alternatif (nama, alamat, kontak, jenis_kelamin) Values (@nama, @alamat, @kontak, @jenis)";
                sqlCmd.Connection = _connection;

                sqlCmd.Parameters.AddWithValue("@nama", atr.nama);
                sqlCmd.Parameters.AddWithValue("@alamat", atr.alamat);
                sqlCmd.Parameters.AddWithValue("@kontak", atr.kontak);
                sqlCmd.Parameters.AddWithValue("@jenis", atr.jenis_kelamin);

                _connection.Open();
                int rowInserted = sqlCmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        [HttpPut]
        public void UpdateAlternatif(tb_alternatif atr)
        {
            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "UPDATE tb_alternatif SET nama = @nama, alamat=@alamat, kontak=@kontak, jenis_kelamin = @jenis WHERE Id= @id";
                sqlCmd.Connection = _connection;

                sqlCmd.Parameters.AddWithValue("@id", atr.Id);
                sqlCmd.Parameters.AddWithValue("@nama", atr.nama);
                sqlCmd.Parameters.AddWithValue("@alamat", atr.alamat);
                sqlCmd.Parameters.AddWithValue("@kontak", atr.kontak);
                sqlCmd.Parameters.AddWithValue("@jenis", atr.jenis_kelamin);

                _connection.Open();
                int rowUpdatd = sqlCmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        [HttpDelete]
        public void DeleteAlternatif(int Id)
        {
            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "delete from tb_alternatif where Id = " + Id + "";
                sqlCmd.Connection = _connection;
                _connection.Open();
                int rowDeleted = sqlCmd.ExecuteNonQuery();
                _connection.Close();
            }
        }
    }
}
