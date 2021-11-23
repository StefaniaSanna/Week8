using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week8.Master.Core.InterfaceRepositories
{
    public interface IRepository<T>
    {
        //contiene le crud
        public List<T> GetAll();
        public T Add(T item); //si può se no mettere un bool
        public T Update(T item);
        public bool Delete(T item);


    }
}
