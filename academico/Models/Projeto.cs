using System.ComponentModel.DataAnnotations;

namespace academico.Models
{
    public enum StatusProjeto
    {
        [Display(Name = "Em desenvolvimento")]
        EmDesenvolvimento,
        [Display(Name = "Em condições de Defesa")]
        EmDefesa,
        [Display(Name = "Completo sem Implantar")]
        CompletoSemImplantar,
        [Display(Name = "Implantado")]
        Implantado
    }

    public class Projeto
    {
        [Key]
        public int ProjetoId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome não deve ultrapassar 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A sigla é obrigatória.")]
        [RegularExpression(@"^[A-Z0-9]+$", ErrorMessage = "A sigla deve conter apenas letras maiúsculas e números, sem espaços.")]
        public string Sigla { get; set; } = string.Empty;

        [Required(ErrorMessage = "O ano é obrigatório.")]
        [CustomValidation(typeof(Projeto), nameof(ValidarAnoAtual))]
        public int Ano { get; set; }

        [Required(ErrorMessage = "O status é obrigatório.")]
        public StatusProjeto Status { get; set; }

        public static ValidationResult? ValidarAnoAtual(int ano, ValidationContext context)
        {
            if (ano != DateTime.Now.Year)
            {
                return new ValidationResult($"Não é permitido cadastrar projetos com ano diferente de {DateTime.Now.Year}.");
            }
            return ValidationResult.Success;
        }
    }
}