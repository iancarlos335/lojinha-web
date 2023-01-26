using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto1.Models
{
    public class Cidade
    {
        [Key]
        public long CidaID { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Sigla")]
        public string Sigla { get; set; }
        

        public virtual ICollection<Cliente> Clientes { get; set; }
    }
}
