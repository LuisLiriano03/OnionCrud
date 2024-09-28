using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnionCrud.Application.Authentication.Exceptions;
using OnionCrud.Application.Users.DTOs;
using OnionCrud.Application.Users.Exceptions;
using OnionCrud.Application.Users.Interfaces;
using OnionCrud.Application.Users.Validators;
using OnionCrud.Domain.Entities;
using OnionCrud.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionCrud.Application.Users.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<GetUser>> GetAllUserAsync()
        {
            try
            {
                var userQuery = await _userRepository.VerifyDataExistenceAsync(
                    u => u.IsDeleted == false || u.IsDeleted == null);
                
                return _mapper.Map<List<GetUser>>(userQuery.ToList());
            }
            catch
            {
                throw new GetUsersFailedException();
            }

        }


        public async Task<bool> UpdateAsync(UpdateUser model)
        {
            try
            {
                var validator = new UpdateUserValidator();
                var validationResult = await validator.ValidateAsync(model);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                    throw new TaskCanceledException(string.Join(", ", errors));
                }

                var userModel = _mapper.Map<User>(model);

                var userFound = await _userRepository.GetEverythingAsync(u => u.Id == userModel.Id);

                var userToUpdate = userFound ?? throw new UserNotFoundException();

                userToUpdate.Name = userModel.Name;
                userToUpdate.Email = userModel.Email;
                userToUpdate.PasswordHash = userModel.PasswordHash;

                bool response = await _userRepository.UpdateAsync(userToUpdate);

                bool isUpdateSuccessful = !response ? throw new UpdateUserFailedException() : response;

                return response;
            }
            catch
            {
                throw;
            }

        }

        public async Task<bool> SoftDeleteAsync(int userId)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(userId);

                _ = user ?? throw new UserNotFoundException();

                bool response = await _userRepository.SoftDeleteAsync(user);

                bool isDeleteSuccessful = !response ? throw new SoftDeleteFailedException() : response;

                return response;
            }
            catch
            {
                throw;
            }

        }



    }

}
