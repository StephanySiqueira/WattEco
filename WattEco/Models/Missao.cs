using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WattEco.Models
{
    [Table("MISSAO_WATT")]
    public class Missao
    {
        [Key]
        [Column("ID_MISSAO")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "A descrição da missão é obrigatória.")]
        [Column("DESCRICAO")]
        public string Descricao { get; set; }

        [Column("PONTUACAO")]
        public int Pontuacao { get; set; } // Pontos atribuídos para a missão


        [ForeignKey("Usuario")]
        [Column("ID_USUARIO")]
        public int UsuarioId { get; set; } // Chave estrangeira para relacionar com o usuário
        public Usuario Usuario { get; set; }
    }
}
