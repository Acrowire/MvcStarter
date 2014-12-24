using Acrowire.Application.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Acrowire.Api.Controllers
{
    using Acrowire.Api.Controllers.ContentTypes;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    [RoutePrefix("resource")]
    public class ResourceController : ApiController
    {
        #region [ Manager ]
        private IResourceManager Manager
        {
            get { return new ResourceManager(); }
        }
        #endregion

        #region [ Local Methods ]
        #endregion

        #region [ Controller Methods ] 
        [HttpGet]
        [Route("{*path}")]
        public HttpResponseMessage Index(String path)
        {
            // Json Response Payload
            JToken json = JToken.FromObject(new JObject());

            var result = this.Manager.Get(path);

            json = JToken.FromObject(result);

            return new HttpResponseMessage()
            {
                Content = new JsonContent(json)
            };
        }

        [HttpPost]
        [Route("{*path}")]
        public HttpResponseMessage Post(String path, [FromBody]JToken jsonbody)
        {
            JToken output_model = null;

            try
            {
                var result = this.Manager.Create(path, JObject.Parse(jsonbody.ToString()));
                output_model = JToken.FromObject(result);
            }
            catch (JsonException json_error)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }

            return new HttpResponseMessage()
            {
                Content = new JsonContent(output_model),
                 StatusCode = HttpStatusCode.Created
            };
        }

        [HttpPut]
        [Route("{*path}")]
        public HttpResponseMessage Put(String path, [FromBody]JToken jsonbody)
        {
            JToken output_model = null;

            try
            {
                var result = this.Manager.Update(path, JObject.Parse(jsonbody.ToString()));
                output_model = JToken.FromObject(result);
            }
            catch (JsonException json_error)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }

            return new HttpResponseMessage()
            {
                Content = new JsonContent(output_model),
                StatusCode = HttpStatusCode.Created
            };
        }

        [HttpDelete]
        [Route("{*path}")]
        public HttpResponseMessage Delete(String path, [FromBody]JToken jsonbody)
        {
            JToken output_model = null;

            try
            {
                var result = this.Manager.Delete(path,null);
                output_model = JToken.FromObject(result);
            }
            catch (JsonException json_error)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }

            return new HttpResponseMessage()
            {
                Content = new JsonContent(output_model),
                StatusCode = HttpStatusCode.Created
            };
        }

        #endregion
    }
}
