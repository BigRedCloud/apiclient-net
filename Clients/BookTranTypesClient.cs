using BigRedCloud.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BigRedCloud.Api.Clients
{
    public class BookTranTypesClient : BaseStaticDictionaryApiClient<BookTranTypeDto>
    {
        private const string EntitiesName = "BookTranTypes";

        // Thread-safe lazy initialization.
        private readonly Lazy<Dictionary<string, BookTranTypeDto>> _bookTranTypesByDescription;

        internal BookTranTypesClient(string apiKey) : base(apiKey, EntitiesName)
        {
            _bookTranTypesByDescription = new Lazy<Dictionary<string, BookTranTypeDto>>(GetAllGroupedByDescription);
        }

        public BookTranTypeDto GetByDescription(string description)
        {
            return _bookTranTypesByDescription.Value[description];
        }

        private Dictionary<string, BookTranTypeDto> GetAllGroupedByDescription()
        {
            return GetAll().ToDictionary(bookTranType => bookTranType.description);
        }
    }
}
