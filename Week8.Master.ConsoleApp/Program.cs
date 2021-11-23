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
                case 0:
                    return false;                    
            }
            return true;
        }

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

            Esito esito= bl.AggiungiCorso(nuovoCorso);
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

        private static int SchermoMenu()
        {
            Console.WriteLine("-----------Menù----------");
            //funzionalità corsi
            Console.WriteLine("\nFunzionalità Corsi");
            Console.WriteLine("[1] Visualizza corsi");
            Console.WriteLine("[2] Inserisci un corso");
            Console.WriteLine("[3] Modificare un corso");
            Console.WriteLine("[4] Elimina un corso");

            //aggiungi

            Console.WriteLine("\n[0] Exit");
            Console.WriteLine("-------------------------");

            int scelta;
            Console.WriteLine("Inserisci la tua scelta");
            while(!(int.TryParse(Console.ReadLine(), out scelta) && scelta >= 0 && scelta <= 4))
            {
                Console.WriteLine("Scelta errata. Inserisci scelta corretta");
            };
            return scelta;
        }
    }
}
