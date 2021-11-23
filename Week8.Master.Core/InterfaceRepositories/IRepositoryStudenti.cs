using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week8.Master.Core.Entities;

namespace Week8.Master.Core.InterfaceRepositories
{
    public interface IRepositoryStudenti: IRepository<Studente>
    {
        public Studente GetById(int id); //è in aggiunta perchè questo metodo non andrebbe bene per i corsi 
    }
}
