using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week8.Master.Core.Entities;

namespace Week8.Master.Core.InterfaceRepositories
{
    public interface IRepositoryCorsi: IRepository<Corso>
    {
        //sto ereditando tutti i metodi dell'interfaccia padre

        public Corso GetByCode(string codice);
    }
}
