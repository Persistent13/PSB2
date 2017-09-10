using Newtonsoft.Json;
using RestSharp.Deserializers;
using System.Collections;
using System.Collections.Generic;

namespace PSB2.Types
{
    public class Account
    {
        [DeserializeAs(Name = "apiUrl")]
        public string ApiUri { get; set; }
        [DeserializeAs(Name = "downloadUrl")]
        public string DownloadUri { get; set; }
        public string AccountId { get; set; }
        public string AuthorizationToken { get; set; }
        public long MinimumPartSize { get; set; }
        public long RecommendedPartSize { get; set; }
        public long AbsoluteMinimumPartSize { get; set; }

        public Account(string ApiUri,
                       string DownloadUri,
                       string AccountId,
                       string AuthorizationToken,
                       long MinimumPartSize,
                       long AbsoluteMinimumPartSize,
                       long RecommendedPartSize)
        {
            this.ApiUri = ApiUri;
            this.AccountId = AccountId;
            this.DownloadUri = DownloadUri;
            this.MinimumPartSize = MinimumPartSize;
            this.AuthorizationToken = AuthorizationToken;
            this.AbsoluteMinimumPartSize = AbsoluteMinimumPartSize;
            this.RecommendedPartSize = RecommendedPartSize;
        }
        public Account() { }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public enum BucketType { allPublic, allPrivate, snapshot }

    public class Bucket
    {
        public string BucketName { get; set; }
        public string BucketId { get; set; }
        public string BucketInfo { get; set; }
        public string LifecycleRules { get; set; }
        public BucketType BucketType { get; set; }
        public string AccountId { get; set; }
        public long Revision { get; set; }

        public Bucket(string BucketName,
                      string BucketId,
                      BucketType BucketType,
                      string AccountId,
                      string BucketInfo,
                      string LifecycleRules,
                      long Revision)
        {
            this.BucketName = BucketName;
            this.BucketId = BucketId;
            this.BucketType = BucketType;
            this.AccountId = AccountId;
            this.BucketInfo = BucketInfo;
            this.LifecycleRules = LifecycleRules;
            this.Revision = Revision;
        }
        public Bucket() { }

        public override string ToString()
        {
            return BucketName;
        }
    }
    //public class BucketList : IEnumerable<Bucket>
    //{
    //    public List<Bucket> Buckets { get; set; }

    //    public IEnumerator<Bucket> GetEnumerator()
    //    {
    //        return Buckets.GetEnumerator();
    //    }

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return Buckets.GetEnumerator();
    //    }
    //}
}
