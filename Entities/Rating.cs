using System;
using System.Collections.Generic;

namespace Entities
{
    public partial class Rating
    {
        public int RatingId { get; set; }
        public string Host { get; set; }
        public string Method { get; set; }
        public string Path { get; set; }
        public string Referer { get; set; }
        public string UserAgent { get; set; }
        public DateTime? RecordDate { get; set; }
        public Rating(string host,string method, string path, string referer, string userAgent, DateTime recordDate)
        {
            Host = host;
            Method = method;
            Path = path;
            Referer = referer;
            UserAgent = userAgent;
            RecordDate = recordDate;
        }
    }
}
