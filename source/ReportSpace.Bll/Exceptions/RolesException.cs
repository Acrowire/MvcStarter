using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportSpace.Bll.Exceptions
{
    public class RolesException : Exception 
    {
        public RolesException(String Message, Exception inner) : base(Message, inner) { }
    }
}
