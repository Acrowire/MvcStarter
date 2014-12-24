using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acrowire.Bll.Exceptions
{
    public class UsersException : Exception
    {
        public Guid PublicId { get; set; }  

        public UsersException(String Message)
            : base(Message)
        {
            this.PublicId = Guid.Empty;
        }

        public UsersException(String Message, Exception inner)
            :base(Message,inner)
        {
            this.PublicId = Guid.Empty;
        }

        public UsersException(String Message, Exception inner, Guid PublicId)
            : base(Message, inner)
        {
            this.PublicId = PublicId;
        }

    }
}
