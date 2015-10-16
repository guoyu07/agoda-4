using Microsoft.VisualStudio.TestTools.UnitTesting;
using API.Controllers;
using API.Business.Implementation;
using API.Repository.CSV;
using API.Domain;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace API.Tests
{
    [TestClass]
    public class RoomSearchTest
    {
        [TestMethod]
        public async Task TestMethod()
        {
            var controller = new HotelsController(new HotelService(
                new HotelRepository(AppDomain.CurrentDomain.BaseDirectory + "\\hoteldb.csv")));
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            var response = await controller.Search("Amsterdam", Api.Infrastructure.SortDirection.Ascending);

            Assert.IsTrue(response.Count() == 6);
        }
    }
}
