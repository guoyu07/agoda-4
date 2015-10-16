using System.Collections.Generic;
using System.Threading.Tasks;
using API.Domain;
using Api.Infrastructure;

namespace API.Business.Interfaces
{
    /// <summary>
    /// Hotel App Service Abstraction
    /// </summary>
    public interface IHotelService
    {
        Task<IEnumerable<Room>> Search(string cityId, SortDirection priceSortDirection = SortDirection.None);
    }
}
