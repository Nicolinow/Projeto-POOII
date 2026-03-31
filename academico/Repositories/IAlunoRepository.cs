using academico.Models;

namespace academico.Repositories
{
    public interface IAlunoRepository
    {
        Task<IEnumerable<Aluno>> GetAll(CancellationToken cancelationToken = default);
        Task<Aluno> GetId(int Id,CancellationToken cancelationToken = default);
        Task Create(Aluno aluno, CancellationToken cancellationToken=default);
        Task Edit(Aluno aluno, CancellationToken cancellationToken = default);
        Task Delete(int  id, CancellationToken cancelationToken = default);
        Task <bool> Exists(int id, CancellationToken cancelationToken = default);
    }
}
