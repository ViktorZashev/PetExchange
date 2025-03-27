using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetExchangeTests.WebFrontEnd
{
    internal class SiteControllerTests : WebFrontEndControllerTestsManagement
    {
        [Test]
        public void AboutUs_ReturnsIActionResult()
        {
            var result = _siteController.AboutUs();
            Assert.IsInstanceOf<IActionResult>(result);
        }

        [Test]
        public void Contacts_ReturnsIActionResult()
        {
            var result = _siteController.Contacts();
            Assert.IsInstanceOf<IActionResult>(result);
        }

        [Test]
        public void Privacy_ReturnsIActionResult()
        {
            var result = _siteController.Privacy();
            Assert.IsInstanceOf<IActionResult>(result);
        }

        [Test]
        public void Terms_ReturnsIActionResult()
        {
            var result = _siteController.Terms();
            Assert.IsInstanceOf<IActionResult>(result);
        }
    }
}
