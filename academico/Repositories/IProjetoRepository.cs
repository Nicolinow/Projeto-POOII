using academico.Models;

namespace academico.Repositories
{
    public interface IProjetoRepository
    {
        Task<IEnumerable<Projeto>> GetAll(CancellationToken cancellationToken = default);
        Task<Projeto> GetId(int id, CancellationToken cancellationToken = default);
        Task Create(Projeto projeto, CancellationToken cancellationToken = default);
        Task Edit(Projeto projeto, CancellationToken cancellationToken = default);
        Task Delete(int id, CancellationToken cancellationToken = default);
        Task<bool> Exists(int id, CancellationToken cancellationToken = default);
    }
}