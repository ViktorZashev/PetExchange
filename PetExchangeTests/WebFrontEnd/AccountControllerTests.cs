using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http.Features;
using WebPresentationLayer.Controllers;
using WebPresentationLayer.Services;
using BusinessLayer;
using DataLayer;
using WebPresentationLayer.Utility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace PetExchangeTests.WebFrontEnd
{
    internal class AccountControllerTests : WebFrontEndControllerTestsManagement
    {

        [Test]
        public async Task Details_ReturnsIActionResult()
        {
            var result = await _accountController.Details();
            Assert.IsInstanceOf<IActionResult>(result);
        }

        [Test]
        public void ChangePassword_ReturnsIActionResult()
        {
            var result = _accountController.ChangePassword();
            Assert.IsInstanceOf<IActionResult>(result);
        }

        [Test]
        public async Task Pets_ReturnsIActionResult()
        {
            var result = await _accountController.Pets();
            Assert.IsInstanceOf<IActionResult>(result);
        }

        [Test]
        public async Task RequestInbox_ReturnsIActionResult()
        {
            var result = await _accountController.RequestInbox();
            Assert.IsInstanceOf<IActionResult>(result);
        }

        [Test]
        public async Task RequestOutbox_ReturnsIActionResult()
        {
            var result = await _accountController.RequestOutbox();
            Assert.IsInstanceOf<IActionResult>(result);
        }

        [Test]
        public async Task DenyRequest_ReturnsIActionResult()
        {
            var requestAction = new UserRequestAction()
            {
                PetId = pet.Id,
                RequestId = userRequest.Id,
                Message = "Example message!"
            };
            var result = await _accountController.DenyRequest(userRequest.Id, requestAction);
            Assert.IsInstanceOf<IActionResult>(result);
        }

        [Test]
        public async Task AcceptRequest_ReturnsIActionResult()
        {
            var requestAction = new UserRequestAction()
            {
                PetId = pet.Id,
                RequestId = userRequest.Id,
                Message = "Example message!"
            };
            var result = await _accountController.AcceptRequest(userRequest.Id, requestAction);
            Assert.IsInstanceOf<IActionResult>(result);
        }

        [Test]
        public async Task CancelRequest_ReturnsIActionResult()
        {
            var result = await _accountController.CancelRequest(userRequest.Id);
            Assert.IsInstanceOf<IActionResult>(result);
        }
    }
}
