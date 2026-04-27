namespace ProxyAPI.DTOs;

/// <summary>
/// En lättviktig databärare för tidrapportsinformation.
/// </summary>
public record TimeLogDto(
    int Id,
    DateTime StartTime,
    DateTime? EndTime,
    int CategoryId
);