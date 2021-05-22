using Kouch.App.Entities;
using Kouch.App.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kouch.App.Services
{
    public class ApLocationService
    {
        private static ApLocationService _instance;
        public static ApLocationService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ApLocationService();
                }

                return _instance;
            }
        }
        private ApLocationService()
        {

        }
        public async Task<ApiResnonse<PaginationModel<Country>>> GetCountries()
        {
            //    return new ApiResnonse<PaginationModel<Country>>
            //    {
            //        IsSuccsess = true,
            //        Result = new PaginationModel<Country>
            //        {
            //            Data = new List<Country>
            //            {
            //                new Country
            //                {
            //                    Id = 1,
            //                    Name = "Россия"
            //                },
            //                new Country
            //                {
            //                    Id = 2,
            //                    Name = "Казахстан"
            //                }
            //            }
            //        }
            //    };
            return await HttpBaseService.Instance.Get<PaginationModel<Country>>("locations/", false);
        }
        public async Task<ApiResnonse<Country>> GetCountry(int id)
        {
            return await HttpBaseService.Instance.Get<Country>($"locations/{id}//", false);
        }
        public async Task<ApiResnonse<List<City>>> GetCities(int id)
        {
            //return new ApiResnonse<PaginationModel<City>>
            //{
            //    IsSuccsess = true,
            //    Result = new PaginationModel<City>
            //    {
            //        Data = new List<City>
            //        {
            //            new City
            //            {
            //                Id = 1,
            //                Name = "Россия"
            //            },
            //            new City
            //            {
            //                Id = 2,
            //                Name = "Казахстан"
            //            }
            //        }
            //    }
            //};
            return await HttpBaseService.Instance.Get<List<City>>($"locations/{id}/load-cities/", false);
        }
    }
}
