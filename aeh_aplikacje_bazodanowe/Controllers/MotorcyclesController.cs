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
    public class MotorcyclesController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public MotorcyclesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            SELECT id, motorcycle_brand, motorcycle_model
                            FROM
                            dbo.Motorcycles
                            WHERE motorcycle_availability = 1
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

        [HttpGet("available")]
        public JsonResult GetAvailable()
        {
            string query = @"
                            SELECT DISTINCT dbo.Motorcycles.id, dbo.Motorcycles.motorcycle_brand, dbo.Motorcycles.motorcycle_model, dbo.Motorcycles.motorcycle_year, dbo.Motorcycles.motorcycle_mileage_km, dbo.Motorcycles.motorcycle_motor, dbo.Motorcycles.motorcycle_body_type, dbo.Motorcycles.motorcycle_rent_price_pln
                            FROM dbo.Motorcycles 
                            LEFT JOIN dbo.Motorcycle_Renting ON dbo.Motorcycle_Renting.motorcycle_id = dbo.Motorcycles.id
                            AND dbo.Motorcycles.motorcycle_availability = 1
                            WHERE dbo.Motorcycle_Renting.motorcycle_id IS NULL OR DATEDIFF(day, SYSDATETIME(), dbo.Motorcycle_Renting.rent_end) < 0;
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
            string query = @"SELECT *
                            FROM
                            dbo.Motorcycles
                            WHERE
                            id = @motorcycle_id
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
        public JsonResult Post([FromBody] Motorcycle motorcycle)
        {
            string query = @"INSERT INTO dbo.Motorcycles
                             (motorcycle_brand, motorcycle_model,
                             motorcycle_year, motorcycle_mileage_km, motorcycle_motor,
                             motorcycle_body_type, motorcycle_rent_price_pln, motorcycle_availability)
                            VALUES
                             (@motorcycle_brand, @motorcycle_model,
                             @motorcycle_year, @motorcycle_mileage_km, @motorcycle_motor,
                             @motorcycle_body_type, @motorcycle_rent_price_pln, @motorcycle_availability);";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@motorcycle_brand", motorcycle.motorcycle_brand);
                    myCommand.Parameters.AddWithValue("@motorcycle_model", motorcycle.motorcycle_model);
                    myCommand.Parameters.AddWithValue("@motorcycle_year", motorcycle.motorcycle_year);
                    myCommand.Parameters.AddWithValue("@motorcycle_mileage_km", motorcycle.motorcycle_mileage_km);
                    myCommand.Parameters.AddWithValue("@motorcycle_motor", motorcycle.motorcycle_motor);
                    myCommand.Parameters.AddWithValue("@motorcycle_body_type", motorcycle.motorcycle_body_type);
                    myCommand.Parameters.AddWithValue("@motorcycle_rent_price_pln", motorcycle.motorcycle_rent_price_pln);
                    myCommand.Parameters.AddWithValue("@motorcycle_availability", motorcycle.motorcycle_availability);
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
                           DELETE FROM dbo.Motorcycles
                           WHERE id = @motorcycle_id
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

            return new JsonResult("Deleted Successfully");
        }

        [HttpPut]
        public JsonResult Put(Motorcycle motorcycle)
        {
            string query = @"
                           UPDATE dbo.Motorcycles
                           SET
                            motorcycle_brand = @motorcycle_brand,
                            motorcycle_model = @motorcycle_model,
                            motorcycle_year = @motorcycle_year,
                            motorcycle_mileage_km = @motorcycle_mileage_km,
                            motorcycle_motor = @motorcycle_motor,
                            motorcycle_body_type = @motorcycle_body_type,
                            motorcycle_rent_price_pln = @motorcycle_rent_price_pln
                           WHERE
                            id = @motorcycle_id
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@motorcycle_id", motorcycle.id);
                    myCommand.Parameters.AddWithValue("@motorcycle_brand", motorcycle.motorcycle_brand);
                    myCommand.Parameters.AddWithValue("@motorcycle_model", motorcycle.motorcycle_model);
                    myCommand.Parameters.AddWithValue("@motorcycle_year", motorcycle.motorcycle_year);
                    myCommand.Parameters.AddWithValue("@motorcycle_mileage_km", motorcycle.motorcycle_mileage_km);
                    myCommand.Parameters.AddWithValue("@motorcycle_motor", motorcycle.motorcycle_motor);
                    myCommand.Parameters.AddWithValue("@motorcycle_body_type", motorcycle.motorcycle_body_type);
                    myCommand.Parameters.AddWithValue("@motorcycle_rent_price_pln", motorcycle.motorcycle_rent_price_pln);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpPut("{id}")]
        public JsonResult ChangeAvailabilityToZero(int id)
        {
            string query = @"
                           UPDATE dbo.Motorcycles
                           SET
                            motorcycle_availability = 0
                           WHERE
                            id = @motorcycle_id
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

            return new JsonResult("Updated Successfully");
        }

    }
}