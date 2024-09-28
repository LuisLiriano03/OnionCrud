using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionCrud.Application.Authentication.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public override string Message { get; }

        public UserNotFoundException() : base()
        {
            Message = "The user is not found";
        }

    }
}
