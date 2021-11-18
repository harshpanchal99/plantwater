using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using WaterPlant.Models;

namespace WaterPlant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PlantController(IConfiguration configuration) {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get() {
            Console.Write("Here Here");
            string query = @"select * from plants";
            Console.Write("Here Here");
            DataTable table = new DataTable();
            Console.Write("Here Here");
            string sqlDataSource = _configuration.GetConnectionString("PlantAppConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource)) {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon)) {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult(table);
        }

        [HttpPut]
        public JsonResult Put(Plant plant)
        {
            string query = @"update plants set status = @status,
                            last_watered='" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + "' where id=@id";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PlantAppConnection");
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myCommand.Parameters.AddWithValue("@status", plant.status);
                    myCommand.Parameters.AddWithValue("@id", plant.id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
            
        }


    }
}
