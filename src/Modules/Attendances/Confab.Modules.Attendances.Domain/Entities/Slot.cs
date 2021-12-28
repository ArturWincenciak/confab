using Confab.Modules.Attendances.Domain.Types;

namespace Confab.Modules.Attendances.Domain.Entities
{
    public class Slot
    {
        public Slot(SlotId id, ParticipantId participantId = null)
        {
            Id = id;
            ParticipantId = participantId;
        }

        public SlotId Id { get; }
        public ParticipantId ParticipantId { get; private set; }
        public bool IsFree => ParticipantId is null;

        public void Take(ParticipantId participantId)
        {
            ParticipantId = participantId;
        }
    }
}