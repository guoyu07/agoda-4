using System.Collections.Generic;
using System.Threading.Tasks;
using API.Domain;
using Api.Infrastructure;

namespace API.Repository.Interfaces
{
    /// <summary>
    /// Hotel repository abstraction
    /// </summary>
    public interface IHotelRepository
    {
        Task<IEnumerable<Room>> GetByCityId(string cityId, SortDirection priceSortDirection = SortDirection.None);
    }
}
