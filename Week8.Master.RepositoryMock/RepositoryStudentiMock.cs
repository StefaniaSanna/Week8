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

        public static List<Studente> Studenti = new List<Studente>();
        public Studente Add(Studente item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Studente item)
        {
            throw new NotImplementedException();
        }

        public List<Studente> GetAll()
        {
            throw new NotImplementedException();
        }

        public Studente GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Studente Update(Studente item)
        {
            throw new NotImplementedException();
        }
    }
}
