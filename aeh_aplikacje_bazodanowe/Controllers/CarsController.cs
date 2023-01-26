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
                            SELECT id,
                            car_brand,
                            car_model,
                            car_year,
                            car_mileage_km,
                            car_transmisson,
                            car_motor,
                            car_body_type,
                            car_rent_price_pln
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
                             car_mileage_km, car_transmisson, car_motor,
                             car_body_type, car_rent_price_pln)
                            VALUES
                             (@car_brand, @car_model, @car_year,
                             @car_mileage_km, @car_transmisson, @car_motor,
                             @car_body_type, @car_rent_price_pln);";

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
                    myCommand.Parameters.AddWithValue("@car_transmisson", car.car_transmisson);
                    myCommand.Parameters.AddWithValue("@car_motor", car.car_motor);
                    myCommand.Parameters.AddWithValue("@car_body_type", car.car_body_type);
                    myCommand.Parameters.AddWithValue("@car_rent_price_pln", car.car_rent_price_pln);
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
                            car_transmisson = @car_transmisson,
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
                    myCommand.Parameters.AddWithValue("@car_transmisson", car.car_transmisson);
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

    }
}