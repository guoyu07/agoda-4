using Api.Infrastructure;
using API.Business.Interfaces;
using API.Domain;
using API.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.Controllers
{
    public class HotelsController : ApiController
    {
        public readonly IHotelService _service;
        public HotelsController(IHotelService service)
        {
            _service = service;
        }

        [HttpGet]
        [RateLimit]
        public async Task<IEnumerable<Room>> Search(string cityId, SortDirection priceSortDirection = SortDirection.None)
        {
            if (string.IsNullOrEmpty(cityId))
                return Enumerable.Empty<Room>();

            return await _service.Search(cityId, priceSortDirection);
        }
    }
}
