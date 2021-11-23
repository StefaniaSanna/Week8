using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week8.Master.Core.Entities
{
    public class Studente:Persona
    {
        public string TitoloStudio { get; set; }
        public DateTime DataNascita { get; set; }

        //dobbiamo mettere la FK di corso 

        public string CorsoCodice { get; set; }
        public Corso Corso { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}\t{Nome}\t{Cognome}\t nato il: {DataNascita.ToShortDateString()}\t Altre info: {Email} - {TitoloStudio} ";
        }
    }

}
