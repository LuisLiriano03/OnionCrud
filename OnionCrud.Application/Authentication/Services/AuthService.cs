using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OnionCrud.Application.Authentication.DTOs;
using OnionCrud.Application.Authentication.Exceptions;
using OnionCrud.Application.Authentication.Interfaces;
using OnionCrud.Application.Authentication.Validators;
using OnionCrud.Application.Users.DTOs;
using OnionCrud.Application.Users.Exceptions;
using OnionCrud.Application.Users.Validators;
using OnionCrud.Domain.Entities;
using OnionCrud.Domain.Enums;
using OnionCrud.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OnionCrud.Application.Authentication.Services
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(IGenericRepository<User> userRepository, IConfiguration configuration, IMapper mapper)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        private string GenerateToken(string UserId)
        {
            var key = _configuration.GetValue<string>("JwtSettings:key");
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, UserId));

            var TokenCredentials = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature
                );

            var DecryptionToken = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddHours(15),
                SigningCredentials = TokenCredentials
            };

            var TokenHandler = new JwtSecurityTokenHandler();
            var TokenConfig = TokenHandler.CreateToken(DecryptionToken);

            string TokenCreated = TokenHandler.WriteToken(TokenConfig);

            return TokenCreated;

        }

        public async Task<LoginResponse> Login(string email, string password)
        {
            try
            {
                var loginRequest = new LoginRequest { Email = email, PasswordHash = password };
                var loginRequestValidator = new LoginRequestValidator();
                var validationResult = loginRequestValidator.Validate(loginRequest);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                    throw new TaskCanceledException($"{string.Join(", ", errors)}");
                }

                var userQuery = await _userRepository.VerifyDataExistenceAsync(u => u.Email == email && u.PasswordHash == password);
                var user = userQuery.FirstOrDefault() ?? throw new UserNotFoundException();

                string token = GenerateToken(user.Id.ToString());

                var loginResponse = _mapper.Map<LoginResponse>(user);
                loginResponse.Token = token;

                return loginResponse;

            }
            catch
            {
                throw;
            }

        }

        public async Task<GetUser> Register(CreateUser model)
        {
            try
            {
                var createUserValidator = new CreateUserValidator();
                var validationResult = createUserValidator.Validate(model);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                    throw new TaskCanceledException($"{string.Join(", ", errors)}");
                }

                // Crear el usuario
                var userCreated = await _userRepository.CreateAsync(_mapper.Map<User>(model));

                // Verificar si el usuario fue creado correctamente usando el operador ternario
                userCreated = userCreated.Id == (int)UserCreationOption.DoNotCreate
                    ? throw new UserNotCreatedException()
                    : userCreated;

                // Obtener el usuario creado desde la base de datos, sin incluir rol
                var query = await _userRepository.VerifyDataExistenceAsync(u => u.Id == userCreated.Id);
                userCreated = query.First();  // Eliminada la parte que incluía el Rol

                // Retornar el usuario mapeado
                return _mapper.Map<GetUser>(userCreated);
            }
            catch
            {
                throw;
            }
        }


    }
}
