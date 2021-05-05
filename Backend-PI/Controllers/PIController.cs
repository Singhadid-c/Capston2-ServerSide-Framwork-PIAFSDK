using OSIsoft.AF.PI;
using OSIsoft.AF.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Backend_PI.Controllers
{
    public class PIController : ApiController
    {
        private PIServer _piserver;
        private string _piserverIP = "202.44.12.146";
        private readonly NetworkCredential cred = new NetworkCredential("group1", "inc.382");
        // http://localhost:xxx/api/pi/value
        [HttpGet]
        [ActionName("value")]
        public IHttpActionResult GetValueFromPITag()
        {
            // connecto PI Server 
            var cn = piConnect();
            if(cn.ConnectionInfo.IsConnected)
            {
                var point = PIPoint.FindPIPoint(cn, "G00X-4023-O1-ST002");
                var timeRange = new AFTimeRange("*-1h", "*");
                var value = point.RecordedValues(timeRange, 0, "", true, 0);
                List<(double, string)> valueAll = new List<(double, string)>();
                foreach (var item in value)
                {
                    valueAll.Add((Convert.ToDouble(item.Value), Convert.ToString(item.Timestamp)));
                }
                return Ok(new { result = valueAll, message = "success" });
            }
            else
            {
                return Ok(new {message = "can not connect to pi server" });
            }
        }

        [HttpGet]
        [ActionName("value")]
        public IHttpActionResult GetValueFromPITagByTagname(string id)
        {
            // connecto PI Server 
            var cn = piConnect();
            if (cn.ConnectionInfo.IsConnected)
            {
                var point = PIPoint.FindPIPoint(cn, id);
                var timeRange = new AFTimeRange("*-1h", "*");
                var value = point.RecordedValues(timeRange, 0, "", true, 0);
                List<(double, string)> valueAll = new List<(double, string)>();
                foreach (var item in value)
                {
                    valueAll.Add((Convert.ToDouble(item.Value), Convert.ToString(item.Timestamp)));
                }
                return Ok(new { result = valueAll, message = "success" });
            }
            else
            {
                return Ok(new { message = "can not connect to pi server" });
            }
        }

        private PIServer piConnect()
        {
            // connecto PI Server 
            _piserver = new PIServers()[_piserverIP];
            _piserver.Connect(cred);

            return _piserver;
        }

    }
}
