using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using SeniaACSAPI.Controllers;
using System;
using Xunit;

namespace SeniaACSAPITest.Test
{
    public class UserControllerTest
    {
        private readonly UserController _controller;

        public UserControllerTest(UserController controller)
        {
            _controller = controller;
        }

        [Fact]
        public void GetUserByID()
        {
            var okResult = _controller.GetById("31e58e3f-756b-4246-af5f-23dabde14ae8");

            Assert.NotNull(okResult.Result);
        }
    }
}