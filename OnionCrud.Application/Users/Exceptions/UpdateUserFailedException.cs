using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionCrud.Application.Users.Exceptions
{
    public class UpdateUserFailedException : Exception
    {
        public override string Message { get; }

        public UpdateUserFailedException() : base()
        {
            Message = "Failed to update user";
        }

    }
    
}
