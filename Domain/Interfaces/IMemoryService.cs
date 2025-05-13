namespace Domain.Interfaces
{
    public interface IMemoryService
    {
        Task SetAsync(string key, string value, TimeSpan? expiry = null);
        Task<string?> GetAsync(string key);
        Task RemoveAsync(string key);
    }
}
