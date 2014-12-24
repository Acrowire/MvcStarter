using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acrowire.Application.Resource
{
    public enum ResourceResultStatus : int
    {
        OK,
        NOT_FOUND,
        ERROR,
        CREATED,
        UPDATED,
        DELTETED,
        UNAUHTORIZED
    }

    public class ResourceResult
    {
        #region [ Properties ] 
        public ResourceResultStatus Status { get; set; }

        public List<String> Messages { get; set; }
        #endregion

        #region [ Constructors ] 
        public ResourceResult()
        {
            this.Status = ResourceResultStatus.NOT_FOUND;
            this.Messages = new List<string>();
        }
        #endregion
    }
}
