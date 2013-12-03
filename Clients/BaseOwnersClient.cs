using BigRedCloud.Api.Model;
using System;

namespace BigRedCloud.Api.Clients
{
    public abstract class BaseOwnersClient<TApiDto> : BaseCrudApiClient<TApiDto> where TApiDto : BaseOwnerDto
    {
        private readonly string _entitiesName;

        protected BaseOwnersClient(string apiKey, string entitiesName) : base(apiKey, entitiesName)
        {
            _entitiesName = entitiesName;
        }

        public virtual TApiDto GetWithBalance(long ownerId)
        {
            string requestUri = String.Format("{0}/{1}?needBalance={2}", _entitiesName, ownerId, true);
            return GetByApi<TApiDto>(requestUri);
        }

        public virtual OwnerOpeningBalanceInPeriodsDto GetOpeningBalance(long ownerId)
        {
            string requestUri = String.Format("{0}/{1}/openingBalance", _entitiesName, ownerId);
            return GetByApi<OwnerOpeningBalanceInPeriodsDto>(requestUri);
        }

        public virtual OwnerOpeningBalanceDto[] GetOpeningBalanceList(long ownerId)
        {
            string requestUri = String.Format("{0}/{1}/openingBalanceList", _entitiesName, ownerId);
            return GetByApi<OwnerOpeningBalanceDto[]>(requestUri);
        }

        public virtual AccountTranDto[] GetAccountTransactions(long ownerId)
        {
            string requestUri = String.Format("{0}/{1}/accountTrans", _entitiesName, ownerId);
            return GetByApi<AccountTranDto[]>(requestUri);
        }
    }
}
