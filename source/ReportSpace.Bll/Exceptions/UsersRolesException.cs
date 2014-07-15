using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportSpace.Bll.Exceptions
{
    public class UsersRolesException : Exception
    {
        public UsersRolesException(String Message, Exception inner)
            : base(Message, inner)
        {

        }
    }
}
