using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using aeh_aplikacje_bazodanowe.Models;

namespace aeh_aplikacje_bazodanowe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public ClientsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            SELECT id, client_name, client_surname
                            FROM
                            dbo.Clients
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            string query = @"
                            SELECT *
                            FROM
                            dbo.Clients
                            WHERE
                            id = @client_id
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@client_id", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Client client)
        {
            string query = @"INSERT INTO dbo.Clients
                             (client_name, client_surname, client_address,
                             client_city, client_phone_nr, client_email,
                             client_driving_license_since)
                            VALUES
                             (@client_name, @client_surname, @client_address,
                             @client_city, @client_phone_nr, @client_email,
                             @client_driving_license_since);";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@client_name", client.client_name);
                    myCommand.Parameters.AddWithValue("@client_surname", client.client_surname);
                    myCommand.Parameters.AddWithValue("@client_address", client.client_address);
                    myCommand.Parameters.AddWithValue("@client_city", client.client_city);
                    myCommand.Parameters.AddWithValue("@client_phone_nr", client.client_phone_nr);
                    myCommand.Parameters.AddWithValue("@client_email", client.client_email);
                    myCommand.Parameters.AddWithValue("@client_driving_license_since", client.client_driving_license_since);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                           DELETE FROM dbo.Clients
                           WHERE id = @client_id
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@client_id", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }

        [HttpPut]
        public JsonResult Put(Client client)
        {
            string query = @"
                           UPDATE dbo.Clients
                           SET
                            clinet_name = @clinet_name,
                            client_surname = @client_surname,
                            client_address = @client_address,
                            client_city = @client_city,
                            client_phone_nr = @client_phone_nr,
                            client_email = @client_email,
                            client_driving_license_since = @client_driving_license_since
                           WHERE
                            id = @client_id
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@client_id", client.id);
                    myCommand.Parameters.AddWithValue("@clinet_name", client.client_name);
                    myCommand.Parameters.AddWithValue("@client_surname", client.client_surname);
                    myCommand.Parameters.AddWithValue("@client_address", client.client_address);
                    myCommand.Parameters.AddWithValue("@client_city", client.client_city);
                    myCommand.Parameters.AddWithValue("@client_phone_nr", client.client_phone_nr);
                    myCommand.Parameters.AddWithValue("@client_email", client.client_email);
                    myCommand.Parameters.AddWithValue("@client_driving_license_since", client.client_driving_license_since);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

    }
}