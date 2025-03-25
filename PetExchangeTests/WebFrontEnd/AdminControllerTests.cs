using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPresentationLayer.Models;

namespace PetExchangeTests.WebFrontEnd
{
    internal class AdminControllerTests : WebFrontEndControllerTestsManagement
    {

        [Test]
        public async Task UserManage_Get_ReturnsIActionResult()
        {
            var result = await _adminController.UserManage(user.Id, "returnUrl");
            Assert.IsInstanceOf<IActionResult>(result);
        }

        [Test]
        public async Task UserManage_Post_ReturnsIActionResult()
        {
            var userManage = new UserManage { TownId = user.TownId};
            var result = await _adminController.UserManage(user.Id, "returnUrl", userManage);
            Assert.IsInstanceOf<IActionResult>(result);
        }

        [Test]
        public async Task PetManage_Get_ReturnsIActionResult()
        {
            var result = await _adminController.PetManage(pet.Id, "returnUrl");
            Assert.IsInstanceOf<IActionResult>(result);
        }

        [Test]
        public async Task PetManage_Post_ReturnsIActionResult()
        {
            var petManage = new PetManage { };
            var result = await _adminController.PetManage(pet.Id, "returnUrl", petManage);
            Assert.IsInstanceOf<IActionResult>(result);
        }
    }
}
