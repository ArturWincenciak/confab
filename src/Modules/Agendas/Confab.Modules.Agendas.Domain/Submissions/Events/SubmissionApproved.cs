﻿using System;
using Confab.Shared.Abstractions.Kernel;

namespace Confab.Modules.Agendas.Domain.Submissions.Events
{
    public record SubmissionApproved(Guid Id) : IDomainEvent;
}