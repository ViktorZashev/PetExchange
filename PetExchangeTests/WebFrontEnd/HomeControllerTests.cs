using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetExchangeTests.WebFrontEnd
{
    internal class HomeControllerTests : WebFrontEndControllerTestsManagement
    {
        [Test]
        public void NotFound_ReturnsIActionResult()
        {
            var result = _homeController.NotFound();
            Assert.IsInstanceOf<IActionResult>(result);
        }

        [Test]
        public void System_ReturnsIActionResult()
        {
            var result = _homeController.System();
            Assert.IsInstanceOf<IActionResult>(result);
        }
    }
}
