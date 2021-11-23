using System;
using System.Collections.Generic;
using Week8.Master.Core.BusinessLayer;
using Week8.Master.Core.Entities;
using Week8.Master.RepositoryADO;
using Week8.Master.RepositoryMock;

namespace Week8.Master.ConsoleApp
{
    class Program
    {
        //questo è quello che prima era il dbManager
        private static readonly IBusinessLayer bl = new MainBusinessLayer(new RepositoryCorsiMock(), new RepositoryStudentiMock());
        //private static readonly IBusinessLayer bl = new MainBusinessLayer(new RepositoryCorsiADO());
        static void Main(string[] args)
        {
            Console.WriteLine("Benvenuto al Master!");
            bool continua = true;

            while (continua)
            {
                int scelta = SchermoMenu();
                continua = AnalizzaScelta(scelta);
            }
        }

        private static bool AnalizzaScelta(int scelta)
        {
            //ci metto lo switch
            switch (scelta)
            {
                case 1:
                    VisualizzaCorsi();
                    break;
                case 2:
                    InserisciCorso();
                    break;
                case 3:
                    ModificaCorso();
                    break;
                case 4:
                    EliminaCorso();
                    break;
                case 5:
                    VisualizzaStudenti();
                    break;
                case 6:
                    InserisciStudente();
                    break;
                case 7:
                    ModificaStudente();
                    break;
                case 8:
                    EliminaStudente();
                    break;
                case 9:
                    VisualizzaStudentiPerCorso();
                    break;
                case 0:
                    return false;
            }
            return true;
        }


        #region Metodi Studenti
        private static void VisualizzaStudentiPerCorso()
        {
            Console.WriteLine("I corsi disponibili sono:");
            VisualizzaCorsi();
            Console.WriteLine("Inserire il codice del corso");
            string codice = Console.ReadLine();
            List<Studente> studentiDiUnCorso = bl.GestStudentsByCourse(codice);
            if (studentiDiUnCorso != null)
            {
                if (studentiDiUnCorso.Count == 0)
                {
                    Console.WriteLine("In questo corso non ci sono studenti");
                }
                else
                {
                    foreach (var item in studentiDiUnCorso)
                    {
                        Console.WriteLine(item.ToString());
                    }
                }
            }           
        }

        private static void EliminaStudente()
        {
            Console.WriteLine("Ecco l'elenco degli studenti");
            VisualizzaStudenti();
            Console.WriteLine("Inserisci l'id dello studente da eliminare");
            int id = int.Parse(Console.ReadLine());
            //dialogo con il core con il business layer
            Esito e = bl.EliminaStudente(id);
            Console.WriteLine(e.Messaggio);
        }

        private static void ModificaStudente()
        {
            Console.WriteLine("Ecco l'elenco degli studenti disponibili");
            VisualizzaStudenti();
            Console.WriteLine("Inserisci l'ID dello studente da modificare");
            int id = int.Parse(Console.ReadLine());
            Studente studenteDaModificare = bl.GetStudenteById(id);
            if(studenteDaModificare!= null)
            {
                Console.WriteLine("Inserisci la nuova email");
                string nuovaEmail = Console.ReadLine();
                Esito esito = bl.ModificaStudente(studenteDaModificare, nuovaEmail);
                Console.WriteLine(esito.Messaggio);
            }
            else
            {
                Console.WriteLine("Non esiste uno studente corrispondendo a questo id");
            }
        }


        private static void InserisciStudente()
        {
            Console.WriteLine("Inserisci il codice del corso che frequenta il nuovo studente");
            string cod = Console.ReadLine();
            Esito codiceEsito = bl.VerificaCorso(cod);
            if (codiceEsito.IsOk == true)
            {
                int id = bl.CreateIdStudente();
                Console.WriteLine("Inserisci il nome e del nuovo studente");
                string nome = Console.ReadLine();
                Console.WriteLine("Inserisci il cognome del nuovo studente");
                string cognome = Console.ReadLine();
                DateTime dataNascita = InserisciData();
                Console.WriteLine("Inserisci l'ultimo titolo di studio");
                string titolo = Console.ReadLine();
                Console.WriteLine("Inserisci l'email");
                string email = Console.ReadLine();
                Studente nuovoStudente = new Studente(id, nome, cognome, email, titolo, dataNascita, cod);
                Esito esito = bl.AggiungiStudente(nuovoStudente);
                Console.WriteLine(esito.Messaggio);
            }
            else
            {
                Console.WriteLine(codiceEsito.Messaggio);
            }
        }

        private static DateTime InserisciData()
        {
            DateTime data;
            do
            {
                Console.WriteLine("Inserisci la data di nascita");

            }
            while (!(DateTime.TryParse(Console.ReadLine(), out data) && data <= DateTime.Today));

            return data;
        }

        private static void VisualizzaStudenti()
        {
            List<Studente> studenti = bl.GetAllStudenti();
            foreach (var item in studenti)
            {
                Console.WriteLine(item);
            }
        }

        #endregion Metodi Studenti

        #region Metodi Corsi
        private static void EliminaCorso()
        {
            Console.WriteLine("Ecco l'elenco dei corsi disponibili");
            VisualizzaCorsi();
            Console.WriteLine("Inserisci il codice del corso da eliminare");
            string codice = Console.ReadLine();
            //dialogo con il core con il business layer
            Esito e = bl.EliminaCorso(codice);
            Console.WriteLine(e.Messaggio);
        }

        private static void ModificaCorso()
        {
            //stampiamo tutti i corsi
            Console.WriteLine("Ecco l'elenco dei corsi disponibili");
            VisualizzaCorsi();
            Console.WriteLine("Inserisci il codice del corso da modificare");
            string codice = Console.ReadLine();
            Console.WriteLine("Inserisci il nuovo nome");
            string nuovoNome = Console.ReadLine();
            Console.WriteLine("Inserisci la nuova descrizione");
            string nuovaDescrizione = Console.ReadLine();

            Esito esito = bl.ModificaCorso(codice, nuovoNome, nuovaDescrizione);
            Console.WriteLine(esito.Messaggio);
        }
        private static void InserisciCorso()
        {
            //chiedo all'utente i dati per creare il nuovo corso
            Console.WriteLine("Inserisci il codice del nuovo corso");
            string codice = Console.ReadLine();
            Console.WriteLine("Inserisci il nome del nuovo corso");
            string nome = Console.ReadLine();
            Console.WriteLine("Inserisci la descrizione del nuovo corso");
            string descrizione = Console.ReadLine();

            //costruisco il nuovo corso
            Corso nuovoCorso = new Corso();
            nuovoCorso.CorsoCodice = codice;
            nuovoCorso.Nome = nome;
            nuovoCorso.Descrizione = descrizione;

            Esito esito = bl.AggiungiCorso(nuovoCorso);
            Console.WriteLine(esito.Messaggio);
            //così finisce la parte della program
        }
        private static void VisualizzaCorsi()
        {
            List<Corso> corsi = bl.GetAllCorsi();
            foreach (var item in corsi)
            {
                Console.WriteLine(item);
            }
        }

        #endregion Metodi Corsi

        private static int SchermoMenu()
        {
            Console.WriteLine("-----------Menù----------");
            //funzionalità corsi
            Console.WriteLine("\nFunzionalità Corsi");
            Console.WriteLine("[1] Visualizza corsi");
            Console.WriteLine("[2] Inserisci un corso");
            Console.WriteLine("[3] Modificare un corso");
            Console.WriteLine("[4] Elimina un corso");
            Console.WriteLine("\nFunzionalità Studenti");
            Console.WriteLine("[5] Visualizza studenti");
            Console.WriteLine("[6] Inserisci uno studente");
            Console.WriteLine("[7] Modificare uno studente");
            Console.WriteLine("[8] Elimina uno studente");
            Console.WriteLine("[9] Visualizza studenti di un corso");

            //aggiungi

            Console.WriteLine("\n[0] Exit");
            Console.WriteLine("-------------------------");

            int scelta;
            Console.WriteLine("Inserisci la tua scelta");
            while (!(int.TryParse(Console.ReadLine(), out scelta) && scelta >= 0 && scelta <= 9))
            {
                Console.WriteLine("Scelta errata. Inserisci scelta corretta");
            };
            return scelta;
        }
    }
}
