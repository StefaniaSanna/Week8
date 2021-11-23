using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week8.Master.Core.Entities
{
    public class Lezione
    {
        public int LezioneId { get; set; }
        public DateTime DataOraInizio { get; set; }
        public int Durata { get; set; }
        public string Aula { get; set; }
        //mettoFK
        public int DocenteId { get; set; }
        public Docente Docente { get; set; }
        public string CorsoCodice { get; set; }
        public Corso Corso { get; set; }

        public override string ToString()
        {
            return $"Lezione: {LezioneId}\tData: {DataOraInizio}\tAula: {Aula}\tDurata: {Durata}";
        }
    }
}
