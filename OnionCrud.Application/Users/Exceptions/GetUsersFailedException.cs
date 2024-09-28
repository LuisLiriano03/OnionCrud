using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionCrud.Application.Users.Exceptions
{
    public class GetUsersFailedException : Exception
    {
        public override string Message { get; }

        public GetUsersFailedException() : base() 
        {
            Message = "message";
        }

    }
}
