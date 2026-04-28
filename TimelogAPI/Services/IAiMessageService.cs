using TimelogAPI.Features.TimeLogs;

namespace TimelogAPI.Services
{
    public interface IAiMessageService
    {
        Task<string> GenerateMessageAsync(string prompt);
        Task<string> GenerateTimelogFeedbackMessageAsync(List<TimeLog> timeLogs);
        Task<string> GetSimpleAiResponseAsync(string prompt);

    }
}