namespace StudentInternshipJobPortal.Shared.Constants;

public static class ApplicationStatuses
{
    public const string Pending = "Pending";
    public const string Accepted = "Accepted";
    public const string Rejected = "Rejected";

    public static readonly string[] All = [Pending, Accepted, Rejected];
}
