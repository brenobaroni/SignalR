using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace LinkGamer.Domain.Entities
{
    [Table("User")]
    public class User : BaseEntitie
    {

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public int Role { get; set; }

        public bool Ativo { get; set; }

        public int IdEmpresa { get; set; }

        public override void Validate()
        {
        }
    }
}
