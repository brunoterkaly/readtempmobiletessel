using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ExposeTemperatures.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values

        public IEnumerable<TimeTemp> Get()
        {

            DataTable table = new DataTable();
            {
                // write the sql statement to execute
                string sql = "SELECT top 15 * from TemperatureAverage order by TemperatureTime desc";
                // instantiate the command object to fire
                using (SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        // get the adapter object and attach the command object to it
                        using (SqlDataAdapter ad = new SqlDataAdapter(cmd))
                        {
                            // fire Fill method to fetch the data and fill into DataTable
                            ad.Fill(table);
                        }
                    }
                }
            }
            // Loop through and extract firstname as a list of strings

            List<TimeTemp> results = new List<TimeTemp>();
            foreach (DataRow dr in table.Rows)
            {
                string dt = dr[1].ToString();
                DateTime dt2 = DateTime.Parse(dt);

                results.Add(new TimeTemp { time = dt2.ToString("mm:ss"), temperature = dr[2].ToString() });
            }
            return results;
        }
        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
    public class TimeTemp
    {
        public string time { get; set; }
        public string temperature { get; set; }
    }
}