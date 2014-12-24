using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acrowire.Bll.Exceptions
{
    public class UserReportsException : Exception
    {
        public UserReportsException(String Message, Exception inner)
            : base(Message, inner)
        {

        }
    }
}
