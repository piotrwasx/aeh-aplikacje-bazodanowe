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
    public class UtilitiesRentingController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public UtilitiesRentingController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpGet]
        public JsonResult Get()
        {
            string query = @"SELECT *
                            FROM
                            dbo.Utility_Renting
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

        [HttpGet("byUtility")]
        public JsonResult Get(int id)
        {
            string query = @"SELECT *
                            FROM
                            dbo.Utility_Renting
                            WHERE
                            utility_id = @utility_id
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
        public JsonResult Post([FromBody] UtilityRenting utilityRenting)
        {
            string query = @"INSERT INTO dbo.Utility_Renting
                             (client_id, utility_id, rent_start,
                             rent_end, rent_insurance)
                            VALUES
                             (@client_id, @utility_id, @rent_start,
                             @rent_end, @rent_insurance);";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@client_id", utilityRenting.client_id);
                    myCommand.Parameters.AddWithValue("@utility_id", utilityRenting.utility_id);
                    myCommand.Parameters.AddWithValue("@rent_start", utilityRenting.rent_start);
                    myCommand.Parameters.AddWithValue("@rent_end", utilityRenting.rent_end);
                    myCommand.Parameters.AddWithValue("@rent_insurance", utilityRenting.rent_insurance);
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
                           DELETE FROM dbo.Utility_Renting
                           WHERE id = @utilityRent_id
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@utilityRent_id", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }

        [HttpPut]
        public JsonResult Put(UtilityRenting utilityRenting)
        {
            string query = @"
                           UPDATE dbo.Utility_Renting
                           SET
                            client_id = @client_id,
                            utility_id = @utility_id,
                            rent_start = @rent_start,
                            rent_end = @rent_end,
                            rent_insurance = @rent_insurance
                           WHERE
                            id = @utilityRent_id
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@utilityRent_id", utilityRenting.id);
                    myCommand.Parameters.AddWithValue("@client_id", utilityRenting.client_id);
                    myCommand.Parameters.AddWithValue("@utility_id", utilityRenting.utility_id);
                    myCommand.Parameters.AddWithValue("@rent_start", utilityRenting.rent_start);
                    myCommand.Parameters.AddWithValue("@rent_end", utilityRenting.rent_end);
                    myCommand.Parameters.AddWithValue("@rent_insurance", utilityRenting.rent_insurance);

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