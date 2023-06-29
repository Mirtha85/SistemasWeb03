using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SistemasWeb01.DataAccess;
using SistemasWeb01.Models;
using System.Collections.Generic;

namespace SistemasWeb01.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly ShoppingDbContext _shoppingDbContext;
        public CombosHelper(ShoppingDbContext shoppingDbContext)
        {
            _shoppingDbContext = shoppingDbContext;
        }

        public async Task<IEnumerable<SelectListItem>> GetAllProductSizeByProductId( int productId)
        {
            List<SelectListItem> list = _shoppingDbContext.ProductSizes
                .Where(ps => ps.ProductId == productId)
                .OrderBy(t => t.Id)
                     .Select(t =>
                      new SelectListItem
                      {
                          Value = $"{t.Id}",
                          Text = t.Talla.ShortName
                      }).ToList();
            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboCitiesAsync(int stateId)
        {
            List<SelectListItem> list = await _shoppingDbContext.Cities
                .Where(x => x.State.Id == stateId)
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = $"{x.Id}"
                })
                .OrderBy(x => x.Text)
                .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione una ciudad...]",
                Value = "0"
            });

            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboCountriesAsync()
        {
            List<SelectListItem> list = await _shoppingDbContext.Countries.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = $"{x.Id}"
            })
                .OrderBy(x => x.Text)
                .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un país...]",
                Value = "0"
            });

            return list;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboStatesAsync(int countryId)
        {
            List<SelectListItem> list = await _shoppingDbContext.States
                .Where(x => x.Country.Id == countryId)
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = $"{x.Id}"
                })
                .OrderBy(x => x.Text)
                .ToListAsync();

            list.Insert(0, new SelectListItem
            {
                Text = "[Seleccione un departamento/estado...]",
                Value = "0"
            });

            return list;
        }


    }
}
