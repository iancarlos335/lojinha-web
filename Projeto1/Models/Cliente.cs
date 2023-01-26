using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto1.Models
{
    public class Cliente
    {
        [Key]
        public long ClienteID { get; set; }

        [Required]
        public string Nome { get; set; }

        [Display (Name="Endereço")]
        public string Endereco { get; set; }

        [EmailAddress (ErrorMessage ="Email inválido.")]
        [Display (Name ="E-mail")]
        public string Email { get; set; }

        public string Telefone { get; set; }
        
        public long CidadeID { get; set; }

        [ForeignKey("CidadeID")]
        public Cidade Cidades { get; set; }
    }
}
