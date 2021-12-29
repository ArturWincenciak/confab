using System;

namespace Confab.Modules.Attendances.Application.Clients.Agendas.DTO
{
    //todo: change to record
    public abstract class AgendaSlotDto
    {
        public Guid Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public string Type { get; set; }
    }
}