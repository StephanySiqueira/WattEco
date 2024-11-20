using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WattEco.Models;

namespace WattEco.Models
{
    [Table("RECOMPENSA_WAITT")]
    public class Recompensa
    {
        [Key]
        [Column("ID_RECOMPENSA")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "A descrição da recompensa é obrigatória.")]
        [Column("DESCRICAO_RECOMPENSA")]
        public string Descricao { get; set; }

        [Column("PONTOS_NECESSARIOS")]
        public int PontosNecessarios { get; set; } // Pontos necessários para resgatar a recompensa

        // Relacionamento com o usuário que resgatou a recompensa
        [ForeignKey("Usuario")]
        [Column("ID_USUARIO")]
        public int UsuarioId { get; set; } 
        public Usuario Usuario { get; set; }
    }
}
