using System.Collections.Generic;

namespace BigRedCloud.Api.Model
{
    public class CustomerDto : BaseOwnerDto
    {
        public IEnumerable<NoteDto> delivery { get; set; }
    }
}
