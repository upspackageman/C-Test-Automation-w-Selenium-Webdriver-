using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace WebDriverWrapper
{
    public class SeleniumWrapper
    {
        public void ParseJson(string json)
        {
            var JsonObject = JObject.Parse(json);
                
            if ((bool)JsonObject["LikeThisCourse"])
            {
                var firstName = (string)JsonObject["FirstName"];
                var lastName = (string)JsonObject["LastName"];
                var age = (int)JsonObject["Age"];
            }
        }
    }
}
