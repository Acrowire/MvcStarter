using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acrowire.Application.Resource
{
    public interface IResourceManager
    {
        bool Authorized(String Path, ApplicationIdentity identity);

        List<JObject> Get(String PathExpression,ApplicationIdentity identity = null);

        ResourceResult Create(String Path, JObject inputModel, ApplicationIdentity identity = null);

        ResourceResult Update(String Path, JObject inputModel,ApplicationIdentity identity = null);

        ResourceResult Delete(String Path, ApplicationIdentity identity);
    }
}
