using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acrowire.Application.Security
{
    /// <summary>
    /// Application Role
    /// </summary>
    public class ApplicationRole : Microsoft.AspNet.Identity.IRole
    {
        #region [ Fields ] 
        private Guid m_public_id = Guid.Empty;
        #endregion

        #region [ Contructors ]
        public ApplicationRole()
            : base()
        {
            
        }

        public ApplicationRole(Guid PublicId)
        {
            this.m_public_id = PublicId;
        }
        #endregion

        #region [ Properties ]

        public string Id
        {
            get { return this.m_public_id.ToString(); }
        }

        public string Name { get; set; } 
        #endregion
    }
}
