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
    public class HasilController : ApiController
    {
        string connStr = ConfigurationManager.ConnectionStrings["mySql"].ConnectionString;

        public IEnumerable<tb_hasil> GetAllHasil()
        {
            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                _connection.Open();
                MySqlCommand _command = new MySqlCommand("SELECT *FROM tb_hasil", _connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(_command);
                DataTable results = new DataTable();

                adapter.Fill(results);
                _connection.Close();

                if (results == null)
                {
                    NotFound();
                }

                var srlJson = JsonConvert.SerializeObject(results);

                return JsonConvert.DeserializeObject<List<tb_hasil>>(srlJson);
            }

        }

        [HttpPost]
        public void SaveHasil(tb_hasil hsl)
        {
            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "INSERT INTO tb_hasil (nama, kriteria, nilai, hasil, ket) Values (@nama, @kriteria, @nilai, @hasil, @ket)";
                sqlCmd.Connection = _connection;

                sqlCmd.Parameters.AddWithValue("@nama", hsl.nama);
                sqlCmd.Parameters.AddWithValue("@kriteria", hsl.kriteria);
                sqlCmd.Parameters.AddWithValue("@nillai", hsl.nilai);
                sqlCmd.Parameters.AddWithValue("@hasil", hsl.hasil);
                sqlCmd.Parameters.AddWithValue("@ket", hsl.ket);

                _connection.Open();
                int rowInserted = sqlCmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        [HttpPut]
        public void UpdateKriteria(tb_hasil hsl)
        {
            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "UPDATE tb_hasil SET nama = @nama, kriteria=@kriteria, nilai = @nilai, hasil=@hasil, ket=@ket WHERE Id= @id";
                sqlCmd.Connection = _connection;

                sqlCmd.Parameters.AddWithValue("@id", hsl.Id);
                sqlCmd.Parameters.AddWithValue("@nama", hsl.nama);
                sqlCmd.Parameters.AddWithValue("@kriteria", hsl.kriteria);
                sqlCmd.Parameters.AddWithValue("@nillai", hsl.nilai);
                sqlCmd.Parameters.AddWithValue("@hasil", hsl.hasil);
                sqlCmd.Parameters.AddWithValue("@ket", hsl.ket);

                _connection.Open();
                int rowUpdatd = sqlCmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        [HttpPut]
        public void UpdateKet(tb_hasil hsl)
        {
            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "UPDATE tb_hasil SET nama = ket=@ket WHERE Id= @id";
                sqlCmd.Connection = _connection;

                sqlCmd.Parameters.AddWithValue("@id", hsl.Id);
                sqlCmd.Parameters.AddWithValue("@ket", hsl.ket);

                _connection.Open();
                int rowUpdatd = sqlCmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public string CheckAlternatif(string nama, string kriteria)
        {
            string user = "";
            string krit = "";
            string msg = "";

            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                _connection.Open();
                MySqlCommand _command = new MySqlCommand("SELECT *FROM tb_hasil WHERE nama = '" + nama + "' AND kriteria = '"+kriteria+"'", _connection);

                MySqlDataReader reader = _command.ExecuteReader();

                if (reader.Read())
                {
                    user = reader.GetString("nama");
                    krit = reader.GetString("kriteria");

                }

                if (nama == user && kriteria == krit)
                {
                    msg = "Data exist";
                }
                else
                {
                    msg = "Data doesnt exist";
                }

                _connection.Close();

            }

            return msg;
        }

        [HttpPost]
        public void SaveKeputusan(tb_keputusan kep)
        {
            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                MySqlCommand sqlCmd = new MySqlCommand();
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "INSERT INTO tb_keputusan (idkeputusan, nama, hasil_akhir, ket, tanggal) Values (@idkeputusan, @nama, @hasil, @ket, @tanggal)";
                sqlCmd.Connection = _connection;

                sqlCmd.Parameters.AddWithValue("@idkeputusan", kep.idkeputusan);
                sqlCmd.Parameters.AddWithValue("@nama", kep.nama);
                sqlCmd.Parameters.AddWithValue("@hasil", kep.hasil_akhir);
                sqlCmd.Parameters.AddWithValue("@ket", kep.ket);
                sqlCmd.Parameters.AddWithValue("@tanggal", kep.tanggal);

                _connection.Open();
                int rowInserted = sqlCmd.ExecuteNonQuery();
                _connection.Close();
            }
        }

        public IEnumerable<tb_keputusan> GetAllKeputusan()
        {
            using (MySqlConnection _connection = new MySqlConnection(connStr))
            {
                _connection.Open();
                MySqlCommand _command = new MySqlCommand("SELECT *FROM tb_keputusan", _connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(_command);
                DataTable results = new DataTable();

                adapter.Fill(results);
                _connection.Close();

                if (results == null)
                {
                    NotFound();
                }

                var srlJson = JsonConvert.SerializeObject(results);

                return JsonConvert.DeserializeObject<List<tb_keputusan>>(srlJson);
            }

        }
    }
}
