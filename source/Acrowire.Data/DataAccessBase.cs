using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acrowire.Data
{
    public partial class DataAccessBase
    {
        #region [ Fields ] 
        private DataAccess m_access;
        #endregion

        #region [ Properies ]
        public String ConnectionStringName { get; private set; }

        public DataAccess Access { get; set; }
        #endregion

        #region [ Constructors ]
        public DataAccessBase()
        {
            this.ConnectionStringName = "default";
            this.Access = new DataAccess(this.ConnectionStringName);
        }

        public DataAccessBase(String ConnectionStringName)
        {
            this.ConnectionStringName = ConnectionStringName;
            this.Access = new DataAccess(this.ConnectionStringName);
        }
        #endregion
    }
}
