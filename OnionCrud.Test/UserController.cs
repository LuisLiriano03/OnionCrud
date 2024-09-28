using Microsoft.AspNetCore.Mvc;
using Moq;
using OnionCrud.API.Controllers;
using OnionCrud.API.Utility;
using OnionCrud.Application.Authentication.Exceptions;
using OnionCrud.Application.Users.DTOs;
using OnionCrud.Application.Users.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionCrud.Test
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _controller = new UserController(_userServiceMock.Object);
        }

        [Fact]
        public async Task GetUsers_ReturnsOkResponse_WithUserList()
        {
            // Arrange
            var users = new List<GetUser>
        {
            new GetUser { Id = 1, Name = "Test User", Email = "test@example.com" }
        };

            _userServiceMock.Setup(service => service.GetAllUserAsync()).ReturnsAsync(users);

            // Act
            var result = await _controller.GetUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<Response<List<GetUser>>>(okResult.Value);
            Assert.True(response.status);
            Assert.Equal(users, response.value);
            Assert.Equal("Successful data", response.message);
        }

        [Fact]
        public async Task EditUser_ReturnsOkResponse_WhenUpdateIsSuccessful()
        {
            // Arrange
            var updateUser = new UpdateUser { Id = 1, Name = "Updated User", Email = "updated@example.com" };
            _userServiceMock.Setup(service => service.UpdateAsync(updateUser)).ReturnsAsync(true);

            // Act
            var result = await _controller.EditUser(updateUser);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<Response<bool>>(okResult.Value);
            Assert.True(response.status);
            Assert.True(response.value);
            Assert.Equal("User information updated successfully", response.message);
        }

        [Fact]
        public async Task SoftDeleteUser_ReturnsOkResponse_WhenDeletionIsSuccessful()
        {
            // Arrange
            int userId = 1;
            _userServiceMock.Setup(service => service.SoftDeleteAsync(userId)).ReturnsAsync(true);

            // Act
            var result = await _controller.SoftDeleteUser(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<Response<bool>>(okResult.Value);
            Assert.True(response.status);
            Assert.True(response.value);
            Assert.Equal("User soft deleted successfully", response.message);
        }

        [Fact]
        public async Task GetUsers_ReturnsOkResponse_WithException()
        {
            // Arrange
            _userServiceMock.Setup(service => service.GetAllUserAsync()).ThrowsAsync(new Exception("Service failure"));

            // Act
            var result = await _controller.GetUsers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<Response<List<GetUser>>>(okResult.Value);
            Assert.False(response.status);
            Assert.Equal("Service failure", response.message);
        }

        [Fact]
        public async Task SoftDeleteUser_ReturnsOkResponse_WithException()
        {
            // Arrange
            int userId = 1;
            _userServiceMock.Setup(service => service.SoftDeleteAsync(userId)).ThrowsAsync(new Exception("Service failure"));

            // Act
            var result = await _controller.SoftDeleteUser(userId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<Response<bool>>(okResult.Value);
            Assert.False(response.status);
            Assert.Equal("Service failure", response.message);
        }
    }
}
