using System;
using Confab.Shared.Abstractions.Commands;

namespace Confab.Modules.Agendas.Application.Submissions.Commands
{
    internal record ApproveSubmission(Guid Id) : ICommand;
}