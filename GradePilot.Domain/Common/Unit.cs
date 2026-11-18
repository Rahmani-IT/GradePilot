namespace GradePilot.Domain.Common;

// Represents "no value" for operations that succeed or fail without returning data.
public sealed record Unit
{
    public static readonly Unit Value = new();
}
