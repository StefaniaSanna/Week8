using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week8.Master.Core.Entities;
using Week8.Master.Core.InterfaceRepositories;

namespace Week8.Master.RepositoryMock
{
    public class RepositoryStudentiMock : IRepositoryStudenti
    {

        public static List<Studente> Studenti = new List<Studente>()
        {
            new Studente(1, "Mario", "Rossi","marioRossi@gmail.com","Laurea magistrale in Economia",new DateTime(2021,12,10), "C-01")

        };
        public Studente Add(Studente item)
        {
            Studenti.Add(item);
            return item;
        }

        public bool Delete(Studente item)
        {
            Studenti.Remove(item);
            return true;
        }

        public List<Studente> GetAll()
        {
            return Studenti;
        }

        public Studente GetById(int id)
        {
            foreach (var item in Studenti)
            {
                if(item.Id == id)
                {
                    return item;
                }
            }
            return null;
        }

        public Studente Update(Studente item)
        {
            foreach (var e in Studenti)
            {
                if(item.Id == e.Id)
                {
                    e.Email = item.Email;
                    return e;
                }
            }
            return null;
        }
    }
}
