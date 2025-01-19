using SystemPanel.Models;

namespace SystemPanel.Services;

public interface IDuyuruService
{
    Task<IEnumerable<Duyuru>> GetAllAsync();
    Task<Duyuru?> GetByIdAsync(int id);
    Task AddAsync(Duyuru duyuru);
    Task UpdateAsync(int id, Duyuru duyuru);
    Task DeleteAsync(int id);
}
