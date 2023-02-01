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
    public class UtilitiesController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public UtilitiesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            SELECT id, utility_brand, utility_model
                            FROM
                            dbo.Utilities WHERE utility_availability = 1
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
                            SELECT DISTINCT dbo.Utilities.id, dbo.Utilities.utility_brand, dbo.Utilities.utility_model, dbo.Utilities.utility_year, dbo.Utilities.utility_mileage_km, dbo.Utilities.utility_transmission, dbo.Utilities.utility_motor, dbo.Utilities.utility_type, dbo.Utilities.utility_rent_price_pln
                            FROM dbo.Utilities 
                            LEFT JOIN dbo.Utility_Renting ON dbo.Utility_Renting.utility_id = dbo.Utilities.id
                            WHERE dbo.Utilities.utility_availability = 1
                            AND dbo.Utility_Renting.utility_id IS NULL OR DATEDIFF(day, SYSDATETIME(), dbo.Utility_Renting.rent_end) < 0
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
                            dbo.Utilities
                            WHERE
                            id = @utility_id
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@utility_id", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post([FromBody] Utility utility)
        {
            string query = @"INSERT INTO dbo.Utilities
                             (utility_brand, utility_model, utility_year,
                             utility_mileage_km, utility_transmission, utility_motor,
                             utility_type, utility_rent_price_pln, utility_availability)
                            VALUES
                             (@utility_brand, @utility_model, @utility_year,
                             @utility_mileage_km, @utility_transmission, @utility_motor,
                             @utility_type, @utility_rent_price_pln, @utility_availability);";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@utility_brand", utility.utility_brand);
                    myCommand.Parameters.AddWithValue("@utility_model", utility.utility_model);
                    myCommand.Parameters.AddWithValue("@utility_year", utility.utility_year);
                    myCommand.Parameters.AddWithValue("@utility_mileage_km", utility.utility_mileage_km);
                    myCommand.Parameters.AddWithValue("@utility_transmission", utility.utility_transmission);
                    myCommand.Parameters.AddWithValue("@utility_motor", utility.utility_motor);
                    myCommand.Parameters.AddWithValue("@utility_type", utility.utility_type);
                    myCommand.Parameters.AddWithValue("@utility_rent_price_pln", utility.utility_rent_price_pln);
                    myCommand.Parameters.AddWithValue("@utility_availability", utility.utility_availability);
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
                           DELETE FROM dbo.Utilities
                           WHERE id = @utility_id
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@utility_id", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }

        [HttpPut]
        public JsonResult Put(Utility utility)
        {
            string query = @"
                           UPDATE dbo.Utilities
                           SET
                            utility_brand = @utility_brand,
                            utility_model = @utility_model,
                            utility_year = @utility_year,
                            utility_mileage_km = @utility_mileage_km,
                            utility_transmission = @utility_transmission,
                            utility_motor = @utility_motor,
                            utility_type = @utility_type,
                            utility_rent_price_pln = @utility_rent_price_pln
                           WHERE
                            id = @utility_id
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@utility_id", utility.id);
                    myCommand.Parameters.AddWithValue("@utility_brand", utility.utility_brand);
                    myCommand.Parameters.AddWithValue("@utility_model", utility.utility_model);
                    myCommand.Parameters.AddWithValue("@utility_year", utility.utility_year);
                    myCommand.Parameters.AddWithValue("@utility_mileage_km", utility.utility_mileage_km);
                    myCommand.Parameters.AddWithValue("@utility_transmission", utility.utility_transmission);
                    myCommand.Parameters.AddWithValue("@utility_motor", utility.utility_motor);
                    myCommand.Parameters.AddWithValue("@utility_type", utility.utility_type);
                    myCommand.Parameters.AddWithValue("@utility_rent_price_pln", utility.utility_rent_price_pln);

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
                           UPDATE dbo.Utilities
                           SET
                            utility_availability = 0
                           WHERE
                            id = @utility_id
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@utility_id", id);

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