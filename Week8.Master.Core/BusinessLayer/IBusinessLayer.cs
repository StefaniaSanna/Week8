using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week8.Master.Core.Entities;

namespace Week8.Master.Core.BusinessLayer
{
    public interface IBusinessLayer
    {
        //ci devo mettere l'elenco delle funzionalità della traccia, questo comunica con il program che è sopra
        //visualizza tutti i corsi
        public List<Corso> GetAllCorsi();

        public Esito AggiungiCorso(Corso c);
        public Esito ModificaCorso(string codice, string nuovoNome, string nuovaDescrizione); //implementato dal program
        public Esito EliminaCorso(string codice);
        public List<Studente> GetAllStudenti();
        public int CreateIdStudente();
        public Esito AggiungiStudente(Studente nuovoStudente);
        public Esito VerificaCorso(string codice);
        public Studente GetStudenteById(int id);
        public Esito ModificaStudente(Studente studenteDaModificare, string nuovaEmail);
        public Esito EliminaStudente(int id);
        public List<Studente> GestStudentsByCourse(string codice);
    }
}
