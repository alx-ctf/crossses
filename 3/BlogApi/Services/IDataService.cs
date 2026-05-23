using BlogApi.Dtos;

namespace BlogApi.Services;

public interface IDataService
{
    Task<DataResponseDto> GetDataAsync(CancellationToken cancellationToken = default);
}
