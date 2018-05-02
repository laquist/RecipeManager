using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Services
{
    public class ApiAccess
    {
        //Fields
        private string endPoint;

        //Properties
        public string EndPoint
        {
            get { return endPoint; }
            set { endPoint = value; }
        }


        public ApiAccess(string endPoint)
        {
            EndPoint = endPoint;
        }

        public string GetApiResponse(string queryString)
        {
            string url = String.Format(EndPoint, queryString);

            using (WebClient client = new WebClient())
            {
                string content = client.DownloadString(url);

                var res = JObject.Parse(content);

                var entities = (JObject)res["query"]["pages"];

                var entity = entities.Properties().First();

                var pageValues = (JObject)entity.Value;

                var pageValue = pageValues["extract"];

                return pageValue.ToString();
            }
        }
    }
}
