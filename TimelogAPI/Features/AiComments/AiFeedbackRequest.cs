namespace TimelogAPI.Features.AiComments;

/// <summary>
/// Begäran om AI-feedback för ett specifikt tidsintervall.
/// </summary>
public record AiFeedbackRequest(
    /// <summary>
    /// Startdatum för analys (t.ex. 2024-01-01).
    /// </summary>
    DateTime FromDate,

    /// <summary>
    /// Slutdatum för analys.
    /// </summary>
    DateTime ToDate
);  