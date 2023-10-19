using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankomatV3
{
    internal class UserInterface
    {
        enum Richiesta
        {
            SchermataDiBenvenuto,
            Login,
            MenuPrincipale,
            Versamento,
            ReportSaldo,
            Prelievo,
            Uscita
        };

        
        private Banche _bancaCorrente;
        private BankomatEntities1 ctx = new BankomatEntities1();

        private void StampaIntestazione(string titoloMenu)
        {
            Console.Clear();
            Console.WriteLine("**************************************************");
            Console.WriteLine("*              Bankomat Simulator V3             *");
            Console.WriteLine("**************************************************");
            Console.WriteLine("".PadLeft((50 - titoloMenu.Length) / 2)
                + titoloMenu);
            Console.WriteLine("--------------------------------------------------");
            return;
        }

        private int ScegliVoceMenu(int min, int max)
        {
            string rispostaUtente;


            Console.Write("Scelta: ");
            rispostaUtente = Console.ReadKey().KeyChar.ToString();
            if (!Int32.TryParse(rispostaUtente, out int scelta) ||
                !(min <= scelta && scelta <= max))
            {
                scelta = -1;
                Console.WriteLine("");
                Console.WriteLine($"Scelta non consentita - {rispostaUtente}");
                Console.Write("Premere un tasto per proseguire");
                Console.ReadKey();
            }
            return scelta;
        }

        private int SchermataDiBenvenuto(BankomatEntities1 ctx)
        {
            int scelta = -1;
            while (scelta == -1)
            {
                StampaIntestazione("Selezione Banca");

                foreach (var banca in ctx.Banche)
                {
                    Console.WriteLine($"{banca.Id}- {banca.Nome}");
                }
                Console.WriteLine("0 - Uscita");

                scelta = ScegliVoceMenu(0, ctx.Banche.Count());
            }

            return scelta;

        }

        //private Banca.Funzionalita MenuPrincipale()
        //{
        //    int scelta = -1;

        //    while (scelta == -1)
        //    {
        //        StampaIntestazione($"Menu principale - {_bancaCorrente.Nome}");

        //        //foreach (var funzionalita in _bancaCorrente.Banche_Funzionalita)
        //        //{
        //        //    Console.WriteLine($"{funzionalita.Key.ToString()} - {funzionalita.Value.ToString()}");
        //        //}
        //        Console.WriteLine("0 - Uscita");

        //        scelta = ScegliVoceMenu(0, _bancaCorrente.Banche_Funzionalita.Count);
        //    }

        //    return scelta == 0 ?
        //        Banca.Funzionalita.Uscita:
        //        _bancaCorrente.ElencoFunzionalita[scelta];
        //}

        private bool Login()
        {
            bool autenticato = false;
            int tentativiRimanenti = 3;

            StampaIntestazione($"Login - {_bancaCorrente.Nome}");

            do
            {
                Console.Write("Nome utente: ");
                string nomeUtente = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();
                var utente = ctx.Utenti.FirstOrDefault(u => u.NomeUtente == nomeUtente && u.Password == password && u.IdBanca == _bancaCorrente.Id);

                if (utente == null)
                {
                    tentativiRimanenti--;

                    if (tentativiRimanenti > 0)
                    {
                        Console.WriteLine($"Accesso non riuscito. Tentativi rimanenti: {tentativiRimanenti}");
                        Console.Write("Premere un tasto per proseguire\n");
                        Console.ReadKey();
                    }
                    else
                    {
                        utente.Bloccato = true;
                        ctx.SaveChanges();
                        Console.WriteLine("Accesso non riuscito. Account bloccato.");
                    }
                }
                else if (utente.IdBanca != _bancaCorrente.Id)
                {
                    Console.WriteLine("Accesso non riuscito. L'utente non appartiene a questa banca.");
                }
                else
                {
                    autenticato = true; // Imposta autenticato su true se le credenziali sono corrette
                }
            }
            while (!autenticato && tentativiRimanenti > 0);

            return autenticato;
        }




        public void Esegui()
        {
            int rispostaUtente = 0;
            Richiesta richiesta = Richiesta.SchermataDiBenvenuto;

            while (richiesta != Richiesta.Uscita)
            {
                switch (richiesta)
                {
                    case Richiesta.SchermataDiBenvenuto:
                        rispostaUtente = SchermataDiBenvenuto(ctx);

                        if (rispostaUtente == 0)
                            richiesta = Richiesta.Uscita;
                        else
                        {

                            _bancaCorrente = ctx.Banche.FirstOrDefault(b => b.Id == rispostaUtente);
                            richiesta = Richiesta.Login;
                        }
                        break;

                    case Richiesta.Login:
                        if (Login())
                            richiesta = Richiesta.Uscita;
                        else
                            richiesta = Richiesta.SchermataDiBenvenuto;
                        break;
                    case Richiesta.MenuPrincipale:
                        //switch (MenuPrincipale())
                        //{
                        //    case Banca.Funzionalita.Uscita:
                        //        richiesta = Richiesta.SchermataDiBenvenuto;
                        //        break;
                        //    case Banca.Funzionalita.Versamento:
                        //        richiesta = Richiesta.Versamento;
                        //        break;
                        //    case Banca.Funzionalita.ReportSaldo:
                        //        richiesta = Richiesta.ReportSaldo;
                        //        break;
                        //    case Banca.Funzionalita.Prelievo:
                        //        richiesta = Richiesta.Prelievo;
                        //        break;
                        //}
                        break;
                    //case Richiesta.Versamento:
                    //    bool esito = Versamento();
                    //    if (esito && _bancaCorrente.ElencoFunzionalita
                    //        .ContainsValue(Banca.Funzionalita.ReportSaldo))
                    //        richiesta = Richiesta.ReportSaldo;
                    //    else
                    //        richiesta = Richiesta.MenuPrincipale;
                    //    break;
                    //case Richiesta.ReportSaldo:
                    //    ReportSaldo();
                    //    richiesta = Richiesta.MenuPrincipale;
                    //    break;
                    //case Richiesta.Prelievo:
                    //    Prelievo();
                    //    richiesta = Richiesta.MenuPrincipale;
                    //    break;
                    default:
                        break;
                }
            }
            Console.WriteLine("accesso riuscito");

        }
    }
}


