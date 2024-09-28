using OnionCrud.Application.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionCrud.Application.Users.Interfaces
{
    public interface IUserService
    {
        Task<List<GetUser>> GetAllUserAsync();
        Task<bool> UpdateAsync(UpdateUser user);
        Task<bool> SoftDeleteAsync(int userId);

    }
}
