namespace Confab.Modules.Agendas.Domain.Submissions.Constants;

internal static class SubmissionStatus
{
    internal const string Pending = nameof(Pending);
    internal const string Approved = nameof(Approved);
    internal const string Rejected = nameof(Rejected);

    internal static bool IsValid(string status) =>
        status is Pending or Approved or Rejected;
}