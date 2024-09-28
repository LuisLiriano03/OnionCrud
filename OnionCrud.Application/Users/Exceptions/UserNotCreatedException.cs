using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionCrud.Application.Users.Exceptions
{
    public class UserNotCreatedException : Exception
    {
        public override string Message { get; }

        public UserNotCreatedException() : base()
        {
            Message = "User could not be created";
        }

    }
}
