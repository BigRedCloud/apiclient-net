using BigRedCloud.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BigRedCloud.Api.Extensions;

namespace BigRedCloud.Api.Clients
{
    public class BookTranTypesClient : BaseStaticDictionaryApiClient<BookTranTypeDto>
    {
        private const string EntitiesName = "BookTranTypes";

        // Thread-safe lazy initialization.
        private readonly Lazy<Task<Dictionary<string, BookTranTypeDto>>> _bookTranTypesByDescription;

        internal BookTranTypesClient(string apiKey) : base(apiKey, EntitiesName)
        {
            _bookTranTypesByDescription = new Lazy<Task<Dictionary<string, BookTranTypeDto>>>(GetAllGroupedByDescriptionAsync);
        }

        public BookTranTypeDto GetByDescription(string description)
        {
            BookTranTypeDto result = GetByDescriptionAsync(description).ResultAndUnwrapException();
            return result;
        }

        public async Task<BookTranTypeDto> GetByDescriptionAsync(string description)
        {
            Dictionary<string, BookTranTypeDto> bookTranTypesByDescription = await _bookTranTypesByDescription.Value.ConfigureAwait(false);
            return bookTranTypesByDescription[description];
        }

        private async Task<Dictionary<string, BookTranTypeDto>> GetAllGroupedByDescriptionAsync()
        {
            BookTranTypeDto[] bookTranTypes = await GetAllAsync().ConfigureAwait(false);
            return bookTranTypes.ToDictionary(bookTranType => bookTranType.description);
        }
    }
}
