using System;
using Confab.Modules.Agendas.Domain.CallForPaper.Exceptions;
using Confab.Shared.Kernel.Types;
using Confab.Shared.Kernel.Types.Base;

namespace Confab.Modules.Agendas.Domain.CallForPaper.Entities
{
    public sealed class CallForPapers : AggregateRoot
    {
        public ConferenceId ConferenceId { get; private set; }
        public DateTime From { get; private set; }
        public DateTime To { get; private set; }
        public bool IsOpened { get; private set; }

        public static CallForPapers Create(Guid conferenceId, DateTime from, DateTime to)
        {
            var id = Guid.NewGuid();
            var callForPaper = new CallForPapers
            {
                Id = id,
                ConferenceId = conferenceId,
                IsOpened = false
            };
            callForPaper.ChangeDateRange(from, to);
            callForPaper.ClearEvents();
            return callForPaper;
        }

        public void ChangeDateRange(DateTime from, DateTime to)
        {
            if (from.Date > to.Date)
                throw new InvalidCallForPapersDateException(from, to);

            From = from;
            To = to;
            IncrementVersion();
        }

        public void Open()
        {
            IsOpened = true;
            IncrementVersion();
        }

        public void Close()
        {
            IsOpened = false;
            IncrementVersion();
        }
    }
}