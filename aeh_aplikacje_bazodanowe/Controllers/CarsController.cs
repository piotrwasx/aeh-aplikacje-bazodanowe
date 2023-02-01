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
    public class CarsController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public CarsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            SELECT id,
                            car_brand,
                            car_model
                            FROM
                            dbo.Cars
                            WHERE car_availability = 1
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
                            SELECT DISTINCT dbo.Cars.id, dbo.Cars.car_brand, dbo.Cars.car_model, dbo.Cars.car_year, dbo.Cars.car_mileage_km, dbo.Cars.car_transmission, dbo.Cars.car_motor, dbo.Cars.car_body_type, dbo.Cars.car_rent_price_pln
                            FROM dbo.Cars 
                            LEFT JOIN dbo.Car_Renting ON dbo.Car_Renting.car_id = dbo.Cars.id
                            AND dbo.Cars.car_availability = 1
                            WHERE dbo.Car_Renting.car_id IS NULL OR DATEDIFF(day, SYSDATETIME(), dbo.Car_Renting.rent_end) < 0;
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
                            dbo.Cars
                            WHERE
                            id = @car_id
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
        public JsonResult Post([FromBody]Car car)
        {
            string query = @"INSERT INTO dbo.Cars
                             (car_brand, car_model, car_year,
                             car_mileage_km, car_transmission, car_motor,
                             car_body_type, car_rent_price_pln, car_availability)
                            VALUES
                             (@car_brand, @car_model, @car_year,
                             @car_mileage_km, @car_transmission, @car_motor,
                             @car_body_type, @car_rent_price_pln, @car_availability);";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@car_brand", car.car_brand);
                    myCommand.Parameters.AddWithValue("@car_model", car.car_model);
                    myCommand.Parameters.AddWithValue("@car_year", car.car_year);
                    myCommand.Parameters.AddWithValue("@car_mileage_km", car.car_mileage_km);
                    myCommand.Parameters.AddWithValue("@car_transmission", car.car_transmission);
                    myCommand.Parameters.AddWithValue("@car_motor", car.car_motor);
                    myCommand.Parameters.AddWithValue("@car_body_type", car.car_body_type);
                    myCommand.Parameters.AddWithValue("@car_rent_price_pln", car.car_rent_price_pln);
                    myCommand.Parameters.AddWithValue("@car_availability", car.car_availability);
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
                           DELETE FROM dbo.Cars
                           WHERE id = @car_id
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

            return new JsonResult("Deleted Successfully");
        }

        [HttpPut]
        public JsonResult Put(Car car)
        {
            string query = @"
                           UPDATE dbo.Cars
                           SET
                            car_brand = @car_brand,
                            car_model = @car_model,
                            car_year = @car_year,
                            car_mileage_km = @car_mileage_km,
                            car_transmission = @car_transmission,
                            car_motor = @car_motor,
                            car_body_type = @car_body_type,
                            car_rent_price_pln = @car_rent_price_pln
                           WHERE
                            id = @car_id
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@car_id", car.id);
                    myCommand.Parameters.AddWithValue("@car_brand", car.car_brand);
                    myCommand.Parameters.AddWithValue("@car_model", car.car_model);
                    myCommand.Parameters.AddWithValue("@car_year", car.car_year);
                    myCommand.Parameters.AddWithValue("@car_mileage_km", car.car_mileage_km);
                    myCommand.Parameters.AddWithValue("@car_transmisson", car.car_transmission);
                    myCommand.Parameters.AddWithValue("@car_motor", car.car_motor);
                    myCommand.Parameters.AddWithValue("@car_body_type", car.car_body_type);
                    myCommand.Parameters.AddWithValue("@car_rent_price_pln", car.car_rent_price_pln);

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
                           UPDATE dbo.Cars
                           SET
                            car_availability = 0
                           WHERE
                            id = @car_id
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

            return new JsonResult("Updated Successfully");
        }
    }
}