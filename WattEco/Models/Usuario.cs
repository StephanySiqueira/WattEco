using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WattEco.Models
{
    [Table("USUARIO_WATT")]
    public class Usuario
    {
        [Key]
        [Column("ID_USUARIO")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [Column("NOME_USUARIO")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [Column("EMAIL_USUARIO", TypeName = "varchar(255)")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [Column("SENHA_USUARIO")]
        public string Senha { get; set; }

    }
    }

