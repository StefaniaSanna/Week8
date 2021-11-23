using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week8.Master.Core.Entities
{
    public abstract class Persona //il public si mette perchè lo dobbiamo condividerlo con gli altri progetti
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }
        public string Email { get; set; }

        public Persona()
        {

        }
        public Persona(int id, string nome, string cognome, string email)
        {
            Id = id;
            Nome = nome;
            Cognome = cognome;
            Email = email;

        }
    }
}
