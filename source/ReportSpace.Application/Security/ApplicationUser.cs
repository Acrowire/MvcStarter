using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportSpace.Application.Security
{
    public static class ApplicationUserExtensions
    {
    
    }
    /// <summary>
    /// Primary Application User
    /// </summary>
    public class ApplicationUser : IUser
    {
        #region [ Fields ]
        private Guid m_public_id = Guid.Empty;
        #endregion

        #region [ Constructors ] 
        public ApplicationUser() : base()
        {
            this.m_public_id = Guid.Empty;
        }

        public ApplicationUser(Guid PublicId)
        {
            this.m_public_id = PublicId;
        }
        #endregion

        #region [ Properties ]
        public Guid PublicId
        {
            get { return this.m_public_id; }
        }
        public string Id
        {
            get { return this.m_public_id.ToString(); }
        }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }
        #endregion
    }
}
