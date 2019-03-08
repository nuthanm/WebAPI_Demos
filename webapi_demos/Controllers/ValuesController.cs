using System.Collections.Generic;
using System.Web.Http;

namespace WebAPI_Demos.Controllers
{
    public class ValuesController : ApiController
    {
        static List<string> stringValues = new List<string>()
        {
            "value1","value2","value3"
        };

        // GET api/values
        public IEnumerable<string> Get()
        {
            return stringValues;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return stringValues[id];
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
            stringValues.Add(value);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
            stringValues[id] = value;
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            stringValues.RemoveAt(id);
        }
    }
}
