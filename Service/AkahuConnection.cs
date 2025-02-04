using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SHSW.Akahu.Model;

namespace SHSW.Akahu.Service 
{
    public class AkahuConnection() : IDisposable
    {
        private HttpClientHandler _handler;
        private string _userToken { get; set; }
        private string _appToken {  get; set; }

        public void InitialiseConnection(string UserToken, string AppToken)
        {
            _userToken = UserToken;
            _appToken = AppToken;
            OpenConnection();
        }
        public List<Transaction> GetTransactions(string AccountId = "")
        {
            List<Transaction> result = new List<Transaction>();
            OpenConnection();
            using var httpClient = new HttpClient(_handler);
            string RequestString = "https://api.akahu.io/v1/";
            // Get Accounts...
            if (!String.IsNullOrEmpty(AccountId))
            {
                RequestString += "accounts/" + AccountId + "/";
            }
            RequestString += "transactions";
            var request = new HttpRequestMessage(HttpMethod.Get, RequestString);
            request.Headers.Add("Authorization", "Bearer " + _userToken);
            request.Headers.Add("X-Akahu-Id", _appToken);

            var response = httpClient.Send(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = response.Content.ReadAsStream();
                try
                {
                    var transactionresult = JsonSerializer.Deserialize<Akahu.Model.TransactionResult>(responseStream);
                    if (transactionresult != null) result = transactionresult.items.ToList();
                }
                catch (Exception e)
                {

                }
            }
            return result;
        }

        public async Task<List<Transaction>> GetTransactionsASync(string AccountId = "", DateTime? StartDate = null, DateTime? EndDate = null)
        {
            List<Transaction> result = new List<Transaction>();
            OpenConnection();
            using var httpClient = new HttpClient(_handler);
            string RequestString = "https://api.akahu.io/v1/";
            // Get Accounts...
            if (!String.IsNullOrEmpty(AccountId))
            {
                RequestString += "accounts/" + AccountId + "/";
            }
            RequestString += "transactions";
            if (StartDate != null)
            {
                RequestString += "?start=" + StartDate.Value.ToString("yyyy-MM-ddTHH:mm:ssK");
                if (EndDate != null)
                {
                    RequestString += "&end=" + EndDate.Value.ToString("yyyy-MM-ddTHH:mm:ssK");
                }
                else
                {
                    RequestString += "&end=" + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssK");
                }

            }
            var request = new HttpRequestMessage(HttpMethod.Get, RequestString);

            request.Headers.Add("Authorization", "Bearer " + _userToken);
            request.Headers.Add("X-Akahu-Id", _appToken);

            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                try
                {
                    var transactionresult = await JsonSerializer.DeserializeAsync<Akahu.Model.TransactionResult>(responseStream);
                    if (transactionresult != null)  result = transactionresult.items.ToList();
                }
                catch (Exception e)
                {

                }
            }
            return result;
        }

        public async Task<List<Account>> GetAccountsAsync()
        {
            OpenConnection();
            List<SHSW.Akahu.Model.Account> accounts = new List<Account>();
            using var httpClient = new HttpClient(_handler);
            // Get Accounts...
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.akahu.io/v1/accounts");
            request.Headers.Add("Authorization", "Bearer " + _userToken);
            request.Headers.Add("X-Akahu-Id", _appToken);

            var response = await httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                try
                {
                    var accountresult = await JsonSerializer.DeserializeAsync<Akahu.Model.AccountResult>(responseStream);
                    if (accountresult != null) accounts = accountresult.items.ToList();
                }
                catch (Exception e)
                {

                }
            }
            return accounts;
        }
        public void OpenConnection()
        {
            _handler = new HttpClientHandler();
            IWebProxy proxy = WebRequest.GetSystemWebProxy();
            proxy.Credentials = CredentialCache.DefaultNetworkCredentials;
            _handler.UseDefaultCredentials = true;
            _handler.Proxy = proxy; 
        }

        public async void RefreshAccounts()
        {
            using var httpClient = new HttpClient(_handler);
            // Get Accounts...
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.akahu.io/v1/refresh");
            request.Headers.Add("Authorization", "Bearer " + _userToken);
            request.Headers.Add("X-Akahu-Id", _appToken);
            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
            }
        }

        public void Dispose()
        {
        }
    }
}
