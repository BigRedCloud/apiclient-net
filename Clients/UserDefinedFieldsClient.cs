using BigRedCloud.Api.Model;
using BigRedCloud.Api.Model.Querying;
using System;
using System.Threading.Tasks;
using BigRedCloud.Api.Extensions;

namespace BigRedCloud.Api.Clients
{
    public class UserDefinedFieldsClient : BaseQueryableApiClient<UserDefinedFieldDto>
    {
        private const string EntitiesName = "UserDefinedFields";

        internal UserDefinedFieldsClient(string apiKey) : base(apiKey, EntitiesName) { }

        public UserDefinedFieldDto[] GetAllByCategoryType(long categoryTypeId)
        {
            UserDefinedFieldDto[] result = GetAllByCategoryTypeAsync(categoryTypeId).ResultAndUnwrapException();
            return result;
        }

        public async Task<UserDefinedFieldDto[]> GetAllByCategoryTypeAsync(long categoryTypeId)
        {
            string odataParameters = String.Format("$filter=categoryTypeId eq {0}&$orderby=orderIndex", categoryTypeId);
            return await GetAllAsync(odataParameters).ConfigureAwait(false);
        }

        public ODataResult<UserDefinedFieldDto> GetPageByCategoryType(long categoryTypeId)
        {
            ODataResult<UserDefinedFieldDto> result = GetPageByCategoryTypeAsync(categoryTypeId).ResultAndUnwrapException();
            return result;
        }

        public async Task<ODataResult<UserDefinedFieldDto>> GetPageByCategoryTypeAsync(long categoryTypeId)
        {
            string odataParameters = String.Format("$inlinecount=allpages&$filter=categoryTypeId eq {0}&$orderby=orderIndex", categoryTypeId);
            return await GetPageAsync(odataParameters).ConfigureAwait(false);
        }
    }
}
