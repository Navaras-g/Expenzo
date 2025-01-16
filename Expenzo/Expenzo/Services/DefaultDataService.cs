using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Expenzo.Model;
using Expenzo.Services.Interface;
using Expenzo.Services;

namespace Expenzo.Services
{
    public class DefaultDataService
    {

        private readonly ICurrencyService _currencyService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;

        public DefaultDataService(CurrencyService currencyService, CategoryService categoryService, TagService tagService)
        {
            _currencyService = currencyService;
            _categoryService = categoryService;
            _tagService = tagService;
        }

        public async Task InsertDefaultDataAsync()
        {
            var existingCurrencies = await _currencyService.GetAllCurrenciesAsync();
            if (existingCurrencies != null)
            {
                // Default values have already been seeded.
                return;
            }

            var currencies = new List<Currency>
            {
                new Currency(1, "USD", "$"),
                new Currency(2, "Nepalese Rupees", "Rs"),
                new Currency(3, "Euros", "€"),
                new Currency(3, "GBP", "£"),
            };

            var transactionCategories = new List<Category>
            {
                new Category(1,"Credit"),
                new Category(2,"Debit"),
                new Category(3,"Debt"),
            };

            var defaultTags = new List<Tag>
            {
                new Tag(1, "Monthly"),
                new Tag(2, "Yearly"),
                new Tag(3, "Food"),
                new Tag(4, "Drinks"),
                new Tag(5, "Clothing"),
                new Tag(6, "Health"),
                new Tag(7, "Fuel"),
                new Tag(8, "Rent"),
            };

            foreach (var currency in currencies)
            {
                await _currencyService.SaveCurrencyAsync(currency);
            }

            foreach (var category in transactionCategories)
            {
                await _categoryService.SaveCategoryAsync(category);
            }

            foreach (var tag in defaultTags)
            {
                await _tagService.SaveTagAsync(tag);
            }

        }
    }
}
