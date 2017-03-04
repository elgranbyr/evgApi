using evgCoreApi;
using evgCoreApi.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using evgEvents;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace evgApi.Controllers
{
    [RoutePrefix("api/Session")]
    public class SessionController : ApiController
    {
        [HttpGet]
        [Route( Name = "GetInfoSession")]
        public IHttpActionResult getInfoSession()
        {
            return Ok( new { obj= "Este es el primero" });
        }

        [HttpPost, Route(Name = "SetInfoSession")]

        public IHttpActionResult SetInfoSession()
        {
            var t = new dataLake();

            
            var tr = new model_session() { sessionid = Guid.NewGuid().ToString(), dtUTC= DateTime.UtcNow };
            JsonSerializer serializer = new JsonSerializer();
            
            var ret = JsonConvert.SerializeObject(tr); 
                    t.UploadSession(ret, tr.dtUTC);
            return Ok(tr);
        }

        [HttpPut, Route(Name = "SendInfo")]
        public async Task<IHttpActionResult> SendHubInfoSession()
        {
            var t = new evgEvents.evgEvents();


            var tr = new model_session() { sessionid = Guid.NewGuid().ToString(), dtUTC = DateTime.UtcNow };
            await Task.Run(() => evgEvents.evgEvents.MainAsync(null).GetAwaiter().GetResult()); 
            return Ok(tr);
        }
    }
}
