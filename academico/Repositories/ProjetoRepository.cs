using academico.Models;

namespace academico.Repositories
{
    public class ProjetoRepository : IProjetoRepository
    {
        private readonly List<Projeto> _projetos = new List<Projeto>();
        private int _nextId = 1;
        private readonly object _lock = new object();

        public ProjetoRepository()
        {
            _projetos.Add(new Projeto { ProjetoId = _nextId++, Nome = "Sistema de Gestão Acadêmica", Sigla = "SGA", Ano = 2026, Status = StatusProjeto.EmDesenvolvimento });
            _projetos.Add(new Projeto { ProjetoId = _nextId++, Nome = "Módulo de Avaliação", Sigla = "MOD_AV", Ano = 2026, Status = StatusProjeto.Implantado });
        }

        public Task Create(Projeto projeto, CancellationToken cancellationToken = default)
        {
            lock (_lock)
            {
                projeto.ProjetoId = _nextId++;
                _projetos.Add(projeto);
            }
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Projeto>> GetAll(CancellationToken cancellationToken = default)
        {
            lock (_lock) { return Task.FromResult(_projetos.AsEnumerable()); }
        }

        public Task<Projeto> GetId(int id, CancellationToken cancellationToken = default)
        {
            lock (_lock) { return Task.FromResult(_projetos.FirstOrDefault(p => p.ProjetoId == id)); }
        }

        public Task Edit(Projeto projeto, CancellationToken cancellationToken = default)
        {
            lock (_lock)
            {
                var existing = _projetos.FirstOrDefault(p => p.ProjetoId == projeto.ProjetoId);
                if (existing != null)
                {
                    existing.Nome = projeto.Nome;
                    existing.Sigla = projeto.Sigla;
                    existing.Ano = projeto.Ano;
                    existing.Status = projeto.Status;
                }
            }
            return Task.CompletedTask;
        }

        public Task Delete(int id, CancellationToken cancellationToken = default)
        {
            lock (_lock)
            {
                var existing = _projetos.FirstOrDefault(p => p.ProjetoId == id);
                if (existing != null) _projetos.Remove(existing);
            }
            return Task.CompletedTask;
        }

        public Task<bool> Exists(int id, CancellationToken cancellationToken = default)
        {
            lock (_lock) { return Task.FromResult(_projetos.Any(p => p.ProjetoId == id)); }
        }
    }
}