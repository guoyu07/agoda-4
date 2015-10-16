using System.Collections.Generic;
using System.Threading.Tasks;
using API.Business.Interfaces;
using API.Domain;
using Api.Infrastructure;
using API.Repository.Interfaces;


namespace API.Business.Implementation
{
    /// <summary>
    /// Hotel service implementation
    /// </summary>
    public class HotelService : IHotelService
    {
        private readonly IHotelRepository _repository;
        public HotelService(IHotelRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Room>> Search(string cityId, SortDirection priceSortDirection = SortDirection.None)
        {
            return await _repository.GetByCityId(cityId, priceSortDirection);
        }
    }
}
