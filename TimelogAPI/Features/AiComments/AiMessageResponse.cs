namespace TimelogAPI.Features.AiComments;

/// <summary>
/// Svar från AI-tjänsten.
/// </summary>
public record AiMessageResponse(
    /// <summary>
    /// Det genererade meddelandet eller feedbacken.
    /// </summary>
    string GeneratedMessage
);