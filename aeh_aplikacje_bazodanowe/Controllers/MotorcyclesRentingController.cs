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
    public class MotorcyclesRentingController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public MotorcyclesRentingController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SELECT *
                            FROM
                            dbo.Motorcycle_Renting
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

        [HttpGet("byMotorcycle")]
        public JsonResult Get(int id)
        {
            string query = @"SELECT *
                            FROM
                            dbo.Motorcycle_Renting
                            WHERE
                            motorcycle_id = @motorcycle_id
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@motorcycle_id", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post([FromBody] MotorcycleRenting motorcycleRenting)
        {
            string query = @"INSERT INTO dbo.Motorcycle_Renting
                             (client_id, motorcycle_id, rent_start,
                             rent_end, rent_insurance)
                            VALUES
                             (@client_id, @motorcycle_id, @rent_start,
                             @rent_end, @rent_insurance);";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@client_id", motorcycleRenting.client_id);
                    myCommand.Parameters.AddWithValue("@motorcycle_id", motorcycleRenting.motorcycle_id);
                    myCommand.Parameters.AddWithValue("@rent_start", motorcycleRenting.rent_start);
                    myCommand.Parameters.AddWithValue("@rent_end", motorcycleRenting.rent_end);
                    myCommand.Parameters.AddWithValue("@rent_insurance", motorcycleRenting.rent_insurance);
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
                           DELETE FROM dbo.Motorcycle_Renting
                           WHERE id = @motorcycleRent_id
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@motorcycleRent_id", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }

        [HttpPut]
        public JsonResult Put(MotorcycleRenting motorcycleRenting)
        {
            string query = @"
                           UPDATE dbo.Motorcycle_Renting
                           SET
                            client_id = @client_id,
                            motorcycle_id = @motorcycle_id,
                            rent_start = @rent_start,
                            rent_end = @rent_end,
                            rent_insurance = @rent_insurance
                           WHERE
                            id = @motorcycleRent_id
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@motorcycleRent_id", motorcycleRenting.id);
                    myCommand.Parameters.AddWithValue("@client_id", motorcycleRenting.client_id);
                    myCommand.Parameters.AddWithValue("@motorcycle_id", motorcycleRenting.motorcycle_id);
                    myCommand.Parameters.AddWithValue("@rent_start", motorcycleRenting.rent_start);
                    myCommand.Parameters.AddWithValue("@rent_end", motorcycleRenting.rent_end);
                    myCommand.Parameters.AddWithValue("@rent_insurance", motorcycleRenting.rent_insurance);

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