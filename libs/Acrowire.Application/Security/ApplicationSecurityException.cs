using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acrowire.Application.Security
{
    using Acrowire;
    using System.Reflection;

    public class ApplicationSecurityException : Exception 
    {
        public MethodBase Context { get; set; }  
        public ApplicationSecurityException(MethodBase context, String Message, Exception inner)
            : base(Message, inner)
        {
            this.Context = context;
        }
    }
}
