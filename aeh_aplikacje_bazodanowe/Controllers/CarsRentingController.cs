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
    public class CarsRentingController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public CarsRentingController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SELECT *
                            FROM
                            dbo.Car_Renting
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

        [HttpGet("byCar")]
        public JsonResult Get(int id)
        {
            string query = @"SELECT *
                            FROM
                            dbo.Car_Renting
                            WHERE
                            car_id = @car_id
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@car_id", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post([FromBody] CarRenting carRenting)
        {
            string query = @"INSERT INTO dbo.Car_Renting
                             (client_id, car_id, rent_start,
                             rent_end, rent_insurance)
                            VALUES
                             (@client_id, @car_id, @rent_start,
                             @rent_end, @rent_insurance);";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@client_id", carRenting.client_id);
                    myCommand.Parameters.AddWithValue("@car_id", carRenting.car_id);
                    myCommand.Parameters.AddWithValue("@rent_start", carRenting.rent_start);
                    myCommand.Parameters.AddWithValue("@rent_end", carRenting.rent_end);
                    myCommand.Parameters.AddWithValue("@rent_insurance", carRenting.rent_insurance);
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
                           DELETE FROM dbo.Car_Renting
                           WHERE id = @carRent_id
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@carRent_id", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }

        [HttpPut]
        public JsonResult Put(CarRenting carRenting)
        {
            string query = @"
                           UPDATE dbo.Car_Renting
                           SET
                            client_id = @client_id,
                            car_id = @car_id,
                            rent_start = @rent_start,
                            rent_end = @rent_end,
                            rent_insurance = @rent_insurance
                           WHERE
                            id = @carRent_id
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@carRent_id", carRenting.id);
                    myCommand.Parameters.AddWithValue("@client_id", carRenting.client_id);
                    myCommand.Parameters.AddWithValue("@car_id", carRenting.car_id);
                    myCommand.Parameters.AddWithValue("@rent_start", carRenting.rent_start);
                    myCommand.Parameters.AddWithValue("@rent_end", carRenting.rent_end);
                    myCommand.Parameters.AddWithValue("@rent_insurance", carRenting.rent_insurance);

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