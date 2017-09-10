using System;
using System.Collections.Generic;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using PSB2.Types;

namespace PSB2.Functions
{
    internal class B2Functions
    {
        public static Account AuthorizeAccount(string AccountId, string ApplicationKey)
        {
            RestClient client = new RestClient()
            {
                BaseUrl = new Uri("https://api.backblazeb2.com/b2api/v1/b2_authorize_account"),
                Authenticator = new HttpBasicAuthenticator(AccountId, ApplicationKey)
            };
            RestRequest req = new RestRequest()
            {
                Method = Method.GET
            };

            return client.Execute<Account>(req).Data;
        }

        public static Bucket[] ListBuckets(Account account)
        {
            var client = new RestClient()
            {
                BaseUrl = new Uri(account.ApiUri)
            };
            var req = new RestRequest()
            {
                Method = Method.POST,
                Resource = "/b2api/v1/b2_list_buckets"
            };
            req.AddHeader("Authorization", account.AuthorizationToken);
            req.AddJsonBody(new Dictionary<string, string> { ["accountId"] = account.AccountId });

            var data = JsonConvert.DeserializeObject<Bucket[]>(client.Execute(req).Content);
            return data;
        }
    }
}
