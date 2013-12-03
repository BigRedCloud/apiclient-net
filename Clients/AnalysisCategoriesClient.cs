using BigRedCloud.Api.Model;
using BigRedCloud.Api.Model.Querying;
using System;

namespace BigRedCloud.Api.Clients
{
    public class AnalysisCategoriesClient : BaseQueryableApiClient<AnalysisCategoryDto>
    {
        private const string EntitiesName = "AnalysisCategories";

        internal AnalysisCategoriesClient(string apiKey) : base(apiKey, EntitiesName) { }

        public ODataResult<AnalysisCategoryDto> GetPageByCategoryType(long categoryTypeId)
        {
            string odataParameters = String.Format("$inlinecount=allpages&$filter=categoryTypeId eq {0}&$orderby=orderIndex", categoryTypeId);
            ODataResult<AnalysisCategoryDto> analysisCategories = GetPageByApi<AnalysisCategoryDto>(odataParameters);
            return analysisCategories;
        }
    }
}
