using System;
using System.Collections.Generic;

namespace BigRedCloud.Api.Model
{
    public abstract class BaseBookTranDto : BaseApiDto
    {
        public long id { get; set; }
        public long bookTranTypeId { get; set; }
        public string note { get; set; }
        public DateTime entryDate { get; set; }
        public DateTime procDate { get; set; }
        public decimal total { get; set; }
        public byte[] timestamp { get; set; }
        public IEnumerable<UserDefinedFieldValueDto> customFields { get; set; }
    }
}
