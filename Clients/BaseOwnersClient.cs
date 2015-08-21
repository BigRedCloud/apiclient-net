using BigRedCloud.Api.Model;
using System;
using System.Threading.Tasks;
using BigRedCloud.Api.Extensions;

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
            TApiDto result = GetWithBalanceAsync(ownerId).ResultAndUnwrapException();
            return result;
        }

        public virtual async Task<TApiDto> GetWithBalanceAsync(long ownerId)
        {
            string requestUri = String.Format("{0}/{1}?needBalance={2}", _entitiesName, ownerId, true);
            return await GetByApiAsync<TApiDto>(requestUri).ConfigureAwait(false);
        }

        public virtual OwnerOpeningBalanceInPeriodsDto GetOpeningBalance(long ownerId)
        {
            OwnerOpeningBalanceInPeriodsDto result = GetOpeningBalanceAsync(ownerId).ResultAndUnwrapException();
            return result;
        }

        public virtual async Task<OwnerOpeningBalanceInPeriodsDto> GetOpeningBalanceAsync(long ownerId)
        {
            string requestUri = String.Format("{0}/{1}/openingBalance", _entitiesName, ownerId);
            return await GetByApiAsync<OwnerOpeningBalanceInPeriodsDto>(requestUri).ConfigureAwait(false);
        }

        public virtual OwnerOpeningBalanceDto[] GetOpeningBalanceList(long ownerId)
        {
            OwnerOpeningBalanceDto[] result = GetOpeningBalanceListAsync(ownerId).ResultAndUnwrapException();
            return result;
        }

        public virtual async Task<OwnerOpeningBalanceDto[]> GetOpeningBalanceListAsync(long ownerId)
        {
            string requestUri = String.Format("{0}/{1}/openingBalanceList", _entitiesName, ownerId);
            return await GetByApiAsync<OwnerOpeningBalanceDto[]>(requestUri).ConfigureAwait(false);
        }

        public virtual AccountTranDto[] GetAccountTransactions(long ownerId)
        {
            AccountTranDto[] result = GetAccountTransactionsAsync(ownerId).ResultAndUnwrapException();
            return result;
        }

        public virtual async Task<AccountTranDto[]> GetAccountTransactionsAsync(long ownerId)
        {
            string requestUri = String.Format("{0}/{1}/accountTrans", _entitiesName, ownerId);
            return await GetByApiAsync<AccountTranDto[]>(requestUri).ConfigureAwait(false);
        }
    }
}
