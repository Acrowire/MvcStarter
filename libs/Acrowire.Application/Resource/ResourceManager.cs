using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acrowire.Application.Resource
{
    public class ResourceManager : IResourceManager
    {
        #region [ Cache ]
        private static Dictionary<String, JObject> cache = new Dictionary<string, JObject>();
        #endregion


        public bool Authorized(string Path, ApplicationIdentity identity)
        {
            throw new NotImplementedException();
        }

        public List<JObject> Get(string PathExpression, ApplicationIdentity identity = null)
        {
            var results = cache.Where(item => item.Key.ToLower().StartsWith(PathExpression.ToLower()))
                               .OrderBy(item => item.Key)
                               .Select(item => item.Value)
                               .ToList();
            return results;
        }

        public ResourceResult Create(string Path, JObject inputModel, ApplicationIdentity identity = null)
        {
            ResourceResult result = new ResourceResult();

            if (cache.ContainsKey(Path))
            {
                cache[Path] = inputModel;
                result.Status = ResourceResultStatus.UPDATED;
            }
            else
            {
                cache.Add(Path, inputModel);
                result.Status = ResourceResultStatus.CREATED;
            }

            return result;
        }

        public ResourceResult Update(string Path, JObject inputModel, ApplicationIdentity identity = null)
        {
            ResourceResult result = new ResourceResult();

            if (cache.ContainsKey(Path))
            {
                cache[Path] = inputModel;
                result.Status = ResourceResultStatus.UPDATED;
            }
            else
            {
                cache.Add(Path, inputModel);
                result.Status = ResourceResultStatus.CREATED;
            }

            return result;
        }

        public ResourceResult Delete(string Path, ApplicationIdentity identity)
        {
            ResourceResult result = new ResourceResult();

            if (cache.ContainsKey(Path))
            {
                cache.Remove(Path);
                result.Status = ResourceResultStatus.DELTETED;
            }
            else
            {
                result.Status = ResourceResultStatus.NOT_FOUND;
            }

            return result;
        }
    }
}
