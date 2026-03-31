using academico.Models;

namespace academico.Repositories
{
    public class InMemoryRepository : IAlunoRepository
    {
        private readonly List<Aluno> _alunos = new List<Aluno>();
        private int _nextId = 1;
        private readonly object _lock = new object();

        public InMemoryRepository() {
            _alunos.Add(new Aluno
            {
                AlunoId = 1,
                Nome = "Aluno Teste",
                Email = "aluno@mail.com",
                Telefone = "(24)99999-9999",
                Endereco = "Rua X, numero 1000",
                Complemento = "apto 101",
                Bairro = "Bairro do aluno",
                Municipio = "Cidade do aluno",
                Uf = "RJ",
                Cep = "271000-000"
            });
        }
        

        public Task Create(Aluno aluno, CancellationToken cancellationToken = default)
        {
            lock (_lock)
            {
                aluno.AlunoId = _nextId++;
                _alunos.Add(aluno);
            }
            return Task.CompletedTask;
        }

        public Task Delete(int id, CancellationToken cancelationToken = default)
        {
            lock (_lock)
            {
                var existing = _alunos.FirstOrDefault(a => a.AlunoId == id);
                if (existing != null)
                {
                    _alunos.Remove(existing);
                }
                return Task.CompletedTask;
            }
        }

        public Task Edit(Aluno aluno, CancellationToken cancellationToken = default)
        {
            lock (_lock)
            {
                var existing = _alunos.FirstOrDefault(a => a.AlunoId == aluno.AlunoId);
                if (existing != null)
                {
                    existing.Nome = aluno.Nome;
                    existing.Email = aluno.Email;
                    existing.Cep = aluno.Cep;
                    existing.Bairro = aluno.Bairro;
                    existing.Endereco = aluno.Endereco;
                    existing.Municipio = aluno.Municipio;
                    existing.Complemento = aluno.Complemento;
                    existing.Uf = aluno.Uf;
                    existing.Telefone = aluno.Telefone;
                }
            }
            return Task.CompletedTask;
        }

        public Task<bool> Exists(int id, CancellationToken cancelationToken = default)
        {
            bool exists;
            lock (_lock)
            {
                exists = _alunos.Any(a => a.AlunoId.Equals(id));
            }
            return Task.FromResult(exists);
        }

        public Task<IEnumerable<Aluno>> GetAll(CancellationToken cancelationToken = default)
        {
            IEnumerable<Aluno> result;
            lock (_lock)
            {
                result = _alunos.Select(a => a).ToList();
            }
            return Task.FromResult(result);
        }

        public Task<Aluno> GetId(int Id, CancellationToken cancelationToken = default)
        {
            Aluno? aluno;
            lock (_lock)
            {
                aluno = _alunos.FirstOrDefault(a => a.AlunoId == Id);
            }
            return Task.FromResult(aluno);
        }
    }
}
