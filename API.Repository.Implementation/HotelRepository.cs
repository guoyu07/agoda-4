
using System;
using System.Collections.Generic;
using Api.Infrastructure;
using API.Domain;
using API.Repository.Interfaces;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

namespace API.Repository.CSV
{
    public class HotelRepository : IHotelRepository
    {
        public HotelRepository(string path)
        {
            _path = path;
        }
        public async Task<IEnumerable<Room>> GetByCityId(string cityId, SortDirection priceSortDirection = SortDirection.None)
        {
            if (_cache == null)
                await ReadData();

            var results = _cache[cityId.ToLower()];

            if (priceSortDirection == SortDirection.Ascending)
                return results.OrderBy(h => h.Price);
            if (priceSortDirection == SortDirection.Descending)
                return results.OrderByDescending(h => h.Price);

            return results;
        }

        #region Private Implementation
        private async Task ReadData()
        {
            var list = new List<Room>();
            var content = await ReadAllLinesAsync(_path);

            for (var i = 1; i < content.Length; i++)
            {
                string[] lineParts = content[i].Split(new[] { Def_Separator }, 
                    StringSplitOptions.RemoveEmptyEntries);

                var hotel = MapHotel(lineParts);
                if(hotel != null)
                    list.Add(hotel);
            }

            _cache = list.ToLookup(h => h.City.ToLower());

        }

        private async Task<string[]> ReadAllLinesAsync(string path)
        {
            using (var reader = File.OpenText(path))
            {
                var s = await reader.ReadToEndAsync();
                return s.Split(new[] { "\r" }, StringSplitOptions.None);
            }
        }

        private Room MapHotel(string[] lineParts)
        {
            string cityId, room;
            int id;
            double price;

            if (lineParts.Length == 4)
            {
                cityId = lineParts[0];
                room = lineParts[1];

                if (int.TryParse(lineParts[1], out id) && 
                    double.TryParse(lineParts[3], out price))
                    return new Room
                    {
                        City = cityId,
                        Id = id,
                        Name = room,
                        Price = price
                    };
            }

            return null;
        }

        private const char Def_Separator = ',';
        private ILookup<string, Room> _cache;
        private string _path;
        #endregion
    }
}
