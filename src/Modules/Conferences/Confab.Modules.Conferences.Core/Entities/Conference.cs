using System;

namespace Confab.Modules.Conferences.Core.Entities
{
    internal class Conference
    {
        public Guid Id { get; set; }
        public Guid HostId { get; set; }
        public Host Host { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Localization { get; set; }
        public string LogoUrl { get; set; }
        public int? ParticipantsLimit { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}