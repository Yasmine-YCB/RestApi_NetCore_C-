 
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestApiTest1.Models;
using System.Data;
using System.Data.SqlClient;

namespace RestApiTest1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        public readonly IConfiguration? _configuration;
        [HttpGet]
        [Route("GetAllEmployees")]
        public string GetEmployees()
        {
            SqlConnection con = new(_configuration.GetConnectionString("EmplyeeAppCon").ToString());
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM client", con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            List<config> ConfigList = new List<config>();
            Response response = new Response();
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    config newConf = new config
                    {
                        st = Convert.ToInt32(dt.Rows[i]["st"]),
                        sav = Convert.ToString(dt.Rows[i]["sav"]),
                        sc = Convert.ToString(dt.Rows[i]["sc"]),
                        prefix = Convert.ToString(dt.Rows[i]["prefix"])
                    };
                    ConfigList.Add(newConf);
                }
            }
            if (ConfigList.Count > 0)
                return JsonConvert.SerializeObject(ConfigList);
            else
            {
                response.StatusCode = 100;
                response.ErrorMessage = "No data found";
                return JsonConvert.SerializeObject(response);
            }




        }
    }
}