using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week8.Master.Core.Entities;
using Week8.Master.Core.InterfaceRepositories;

namespace Week8.Master.Core.BusinessLayer
{
    public class MainBusinessLayer : IBusinessLayer
    {

        //mi dichiaro i repository che ho a disposizione
        private readonly IRepositoryCorsi corsiRepo; //ho dichiarato il mio repository tramite interfaccia
        private readonly IRepositoryStudenti studentiRepo; //ho dichiarato il mio repository tramite interfaccia

        public MainBusinessLayer(IRepositoryCorsi corsi, IRepositoryStudenti studenti) //questo è un costruttore
        {
            corsiRepo = corsi;
            studentiRepo = studenti;
        }

        #region Funzionalita corsi
        public Esito AggiungiCorso(Corso c)
        {
            //ho già il repoCorsi
            //non devi far inserire corsi con codici uguali
            Corso corsoEsistente = corsiRepo.GetByCode(c.CorsoCodice);
            //se lo trova mi restituisce un corso, se no no

            if(corsoEsistente == null)
            {
                corsiRepo.Add(c);
                return  new Esito { Messaggio = "Corso aggiunto correttamente", IsOk = true };
            }
            return new Esito { Messaggio = "Impossibile aggiungere il corso, perchè esiste già un corso con quel codice", IsOk = false };           
        }

        public Esito EliminaCorso(string codice)
        {
            var corsoEsistente = corsiRepo.GetByCode(codice); 
            if (corsoEsistente == null)
            {               
                return new Esito { Messaggio = "Nessun corso corrispondere al codice inserito", IsOk = false };
            }
            corsiRepo.Delete(corsoEsistente);
            return new Esito { Messaggio = "Corso eliminato correttamente", IsOk = true };
        }

        public List<Corso> GetAllCorsi()
        {
            return corsiRepo.GetAll();
            
        }

        public Esito ModificaCorso(string codice, string nuovoNome, string nuovaDescrizione)
        {
            //controllo degli input
            var corsoEsistente = corsiRepo.GetByCode(codice); //se lo trova ci restituisce un corso
            if (corsoEsistente == null)
            {
                //questo corso non esiste
                return new Esito { Messaggio = "Nessun corso corrispondere al codice inserito", IsOk = false };
            }

            Corso corsoDaAggiornare = new Corso();
            corsoDaAggiornare.CorsoCodice = codice;
            corsoDaAggiornare.Nome = nuovoNome;
            corsoDaAggiornare.Descrizione = nuovaDescrizione;
            //così il croso è aggiornato
            //ora dobbiamo aggiornare corsiRepo
            corsiRepo.Update(corsoDaAggiornare);


            return new Esito { Messaggio = "Corso aggiornato", IsOk = true };
        }

        public Esito VerificaCorso(string codice)
        {
            Corso corsoEsistente = corsiRepo.GetByCode(codice);

            if (corsoEsistente == null)
            {
                return new Esito { Messaggio = "Corso inesistente", IsOk = false };
            }
            return new Esito { Messaggio = "Corso esistente", IsOk = true };
        }

        #endregion Funzionalita corsi

        #region Funzionalita Studenti
        public List<Studente> GetAllStudenti()
        {
            return studentiRepo.GetAll();
        }

        public Esito AggiungiStudente(Studente studente)
        {          
            studentiRepo.Add(studente);
            return new Esito { Messaggio = "Studente aggiunto correttamente", IsOk = true };
        }
      
        public Studente GetStudenteById(int id)
        {
            Studente studenteEsistente = studentiRepo.GetById(id);
            if (studenteEsistente != null)
            {
                return studenteEsistente;
            }
            return null;
        }

        public Esito ModificaStudente(Studente studenteDaModificare, string nuovaEmail)
        {
            studenteDaModificare.Email = nuovaEmail;
            studentiRepo.Update(studenteDaModificare);
            return new Esito { Messaggio = "Studente aggiornato", IsOk = true };
        }

        public Esito EliminaStudente(int id)
        {
            Studente studenteDaEliminare = studentiRepo.GetById(id);
            if(studentiRepo == null)
            {
                return new Esito { Messaggio = "Nessuno studente corrispondere al codice inserito", IsOk = false };
            }
            studentiRepo.Delete(studenteDaEliminare);
            return new Esito { Messaggio = "Studente eliminato correttamente", IsOk = true };
        }

        public List<Studente> GestStudentsByCourse(string codice)
        {
            Esito esitoCorso = VerificaCorso(codice);
            if (esitoCorso.IsOk == true)
            {
                List<Studente> tuttiStudenti = GetAllStudenti();
                List<Studente> StudentiFiltrati = new List<Studente>();
                foreach (var item in tuttiStudenti)
                {
                    if (item.CorsoCodice == codice)
                    {
                        StudentiFiltrati.Add(item);
                    }
                }
                return StudentiFiltrati;
            }
            else
            {
                Console.WriteLine(esitoCorso.Messaggio);
            }
            return null;
        }


        #endregion Funzionalita Studenti
    }
}
