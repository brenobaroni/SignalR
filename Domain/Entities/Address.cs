using System;
using System.Collections.Generic;
using System.Text;

namespace LinkGamer.Domain.Entities
{
    public class Address : BaseEntitie
    {
        public int IdClient { get; set; }
        public string Street { get; set; }
        public string State { get; set; }
        public int Country { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }

        public override void Validate()
        {

        }
    }
}
