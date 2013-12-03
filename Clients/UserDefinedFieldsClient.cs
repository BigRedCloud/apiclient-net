using BigRedCloud.Api.Model;
using BigRedCloud.Api.Model.Querying;
using System;

namespace BigRedCloud.Api.Clients
{
    public class UserDefinedFieldsClient : BaseQueryableApiClient<UserDefinedFieldDto>
    {
        private const string EntitiesName = "UserDefinedFields";

        internal UserDefinedFieldsClient(string apiKey) : base(apiKey, EntitiesName) { }

        public ODataResult<UserDefinedFieldDto> GetPageByCategoryType(long categoryTypeId)
        {
            string odataParameters = String.Format("$inlinecount=allpages&$filter=categoryTypeId eq {0}&$orderby=orderIndex", categoryTypeId);
            ODataResult<UserDefinedFieldDto> userDefinedFields = GetPageByApi<UserDefinedFieldDto>(odataParameters);
            return userDefinedFields;
        }
    }
}
