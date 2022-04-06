using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserPanel.API.Controllers;
using UserPanel.Core.Dtos;
using UserPanel.Core.Services;
using UserPanel.Shared.Dtos;
using Xunit;

namespace UserPanel.Test.UnitTest
{
    public class AuthControllerTest
    {
        private readonly Mock<IAuthenticationService> _authServiceMock;
        private readonly AuthController _authController;

        private readonly Response<TokenDto> _tokenDto;
        private readonly Response<TokenDto> _errorNotActiveTokenDto;

        private readonly Response<AppUserDto> _registerResponse;
        private readonly Response<AppUserDto> _errorAlreadyUserRegisterResponse;

        public AuthControllerTest()
        {
            _authServiceMock = new Mock<IAuthenticationService>();
            _authController = new AuthController(_authServiceMock.Object);
            
            _tokenDto = new Response<TokenDto>() {
                Data = new TokenDto() { 
                    AccessToken = "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjgyMmFhNTNhLTYzZGItNGJiOC1hNjI5LTUzNGY2NDliNDg1YyIsIlVzZXJJZCI6IjgyMmFhNTNhLTYzZGItNGJiOC1hNjI5LTUzNGY2NDliNDg1YyIsIklkZCI6IjgyMmFhNTNhLTYzZGItNGJiOC1hNjI5LTUzNGY2NDliNDg1YyIsImVtYWlsIjoic2FsaWgxQHRleWVrLmNvbSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJzYWxpaHRleWVrMSIsImp0aSI6ImJhMWE4ODU3LTdhZWItNGY4ZC04N2ZhLTg2MDYyOGZlMjRiZCIsImF1ZCI6Ind3dy5hdXRoc2VydmVyLmNvbSIsIm5iZiI6MTY0OTI2ODc0MywiZXhwIjoxNjQ5NzAwNzQzLCJpc3MiOiJ3d3cuYXV0aHNlcnZlci5jb20ifQ.CWX8dVD-9_rDpB3EjsbvBgk7uNaJkn1wEPNanezwLbI", 
                    AccessTokenExpiration = Convert.ToDateTime("2022-04-11T21:12:23.9477361+03:00") },
                Error = null,
                IsSuccessful = true,
                StatusCode = 200
            };
            List<string> loginErrors = new List<string>();
            loginErrors.Add("Your account is not active");
            _errorNotActiveTokenDto = new Response<TokenDto>()
            {
                Data = null,
                Error = new ErrorDto(loginErrors, true),
                StatusCode = 400
            };

            _registerResponse = new Response<AppUserDto>() { 
                Data = new AppUserDto()
                {
                    Id = "d1c927d7-ebb9-4b7e-968a-68458329aae2",
                    FullName = "Salih Teyek",
                    UserName = "salihteyek",
                    Email = "salih@teyek.com",
                    Active = false,
                    UserStatus = 0,
                },
                Error = null,
                IsSuccessful = true,
                StatusCode = 200
            };
            List<string> registerErrors = new();
            registerErrors.Add("Username 'salihteyek' is already taken.");
            registerErrors.Add("Email 'salih@teyek.com' is already taken.");
            _errorAlreadyUserRegisterResponse = new Response<AppUserDto>()
            {
                Data = null,
                StatusCode = 400,
                Error = new ErrorDto(registerErrors, true)
            };

        }


        [Theory]
        [InlineData("salih1@teyek.com", "St.123")]
        public async Task Login_Successfull_RetrunOkStatus(string email, string password) 
        {
            LoginDto loginDto = new LoginDto() { Email = email, Password = password };
            _authServiceMock.Setup(x => x.LoginAsync(loginDto)).ReturnsAsync(_tokenDto);

            var result = await _authController.Login(loginDto);
            var okResult = Assert.IsType<ObjectResult>(result);

            var returnResult = Assert.IsAssignableFrom<Response<TokenDto>>(okResult.Value);
            Assert.Equal<int>(200, returnResult.StatusCode);
        }

        /// <summary>
        /// pasive user test
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [Theory]
        [InlineData("salih2@teyek.com", "St.123")]
        public async Task Login_ErrorNotActive_RetrunErrorStatus(string email, string password)
        {
            LoginDto loginDto = new LoginDto() { Email = email, Password = password };
            _authServiceMock.Setup(x => x.LoginAsync(loginDto)).ReturnsAsync(_errorNotActiveTokenDto);

            var result = await _authController.Login(loginDto);
            var errorResult = Assert.IsType<ObjectResult>(result);

            var returnResult = Assert.IsAssignableFrom<Response<TokenDto>>(errorResult.Value);
            Assert.Equal<int>(400, returnResult.StatusCode);
        }

        [Theory]
        [InlineData("Salih Teyek", "salihteyek", "salih@teyek.com", "St..123")]
        public async Task Register_Successfull_ReturnOkStatus(string fullName, string userName, string email, string password)
        {
            RegisterDto registerDto = new RegisterDto() { FullName = fullName, UserName = userName, Email = email, Password = password };
            _authServiceMock.Setup(x => x.RegisterAsync(registerDto)).ReturnsAsync(_registerResponse);

            var result  = await _authController.Register(registerDto);
            var okResult = Assert.IsType<ObjectResult>(result);

            var returnResult = Assert.IsAssignableFrom<Response<AppUserDto>>(okResult.Value);
            Assert.Equal<int>(200, returnResult.StatusCode);
            Assert.Equal<bool>(true, returnResult.IsSuccessful);
        }

        
        [Theory]
        [InlineData("Salih Teyek", "salihteyek", "salih@teyek.com", "St..123")]
        public async Task Register_ErrorAlreadyExist_ReturnErrorStatus(string fullName, string userName, string email, string password)
        {
            RegisterDto registerDto = new RegisterDto() { FullName = fullName, UserName = userName, Email = email, Password = password };
            _authServiceMock.Setup(x => x.RegisterAsync(registerDto)).ReturnsAsync(_errorAlreadyUserRegisterResponse);

            var result = await _authController.Register(registerDto);
            var errorResult = Assert.IsType<ObjectResult>(result);

            var returnResult = Assert.IsAssignableFrom<Response<AppUserDto>>(errorResult.Value);
            Assert.Equal<int>(400, returnResult.StatusCode);
        }
    }
}
