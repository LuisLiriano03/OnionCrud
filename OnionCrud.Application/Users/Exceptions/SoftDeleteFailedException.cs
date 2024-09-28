using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionCrud.Application.Users.Exceptions
{
    public class SoftDeleteFailedException : Exception
    {
        public override string Message { get; }
        public SoftDeleteFailedException() : base() 
        {
            Message = "not delete";
        }

    }

}
