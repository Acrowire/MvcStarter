
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Acrowire.Api.Controllers.ContentTypes
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System.IO;
    using System.Net;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;

    public class JsonContent : HttpContent
    {
        #region [ Fields ]
        private readonly JToken _value;
        #endregion

        #region [ Constructors ]
        public JsonContent(JToken value)
        {
            _value = value;
            Headers.ContentType = new MediaTypeHeaderValue("application/json");
        }
        #endregion

        #region [ Protected Methods ]
        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            var jw = new JsonTextWriter(new StreamWriter(stream))
            {
                Formatting = Formatting.Indented
            };
            _value.WriteTo(jw);
            jw.Flush();
            return Task.FromResult<object>(null);
        }

        protected override bool TryComputeLength(out long length)
        {
            length = -1;
            return false;
        }
        #endregion
    }
}