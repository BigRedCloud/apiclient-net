using BigRedCloud.Api.Model;
using BigRedCloud.Api.Model.Querying;
using System;
using System.Threading.Tasks;
using BigRedCloud.Api.Extensions;

namespace BigRedCloud.Api.Clients
{
    public class AnalysisCategoriesClient : BaseQueryableApiClient<AnalysisCategoryDto>
    {
        private const string EntitiesName = "AnalysisCategories";

        internal AnalysisCategoriesClient(string apiKey) : base(apiKey, EntitiesName) { }

        public AnalysisCategoryDto[] GetAllByCategoryType(long categoryTypeId)
        {
            AnalysisCategoryDto[] result = GetAllByCategoryTypeAsync(categoryTypeId).ResultAndUnwrapException();
            return result;
        }

        public async Task<AnalysisCategoryDto[]> GetAllByCategoryTypeAsync(long categoryTypeId)
        {
            string odataParameters = String.Format("$filter=categoryTypeId eq {0}&$orderby=orderIndex", categoryTypeId);
            return await GetAllAsync(odataParameters).ConfigureAwait(false);
        }

        public ODataResult<AnalysisCategoryDto> GetPageByCategoryType(long categoryTypeId)
        {
            ODataResult<AnalysisCategoryDto> result = GetPageByCategoryTypeAsync(categoryTypeId).ResultAndUnwrapException();
            return result;
        }

        public async Task<ODataResult<AnalysisCategoryDto>> GetPageByCategoryTypeAsync(long categoryTypeId)
        {
            string odataParameters = String.Format("$inlinecount=allpages&$filter=categoryTypeId eq {0}&$orderby=orderIndex", categoryTypeId);
            return await GetPageAsync(odataParameters).ConfigureAwait(false);
        }
    }
}
