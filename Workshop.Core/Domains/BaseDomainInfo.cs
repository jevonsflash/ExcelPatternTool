using System;

namespace Workshop.Core.Domains
{
    public class BaseDomainInfo
    {
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; }
    }
}