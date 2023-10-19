using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankomatV3
{
    class Banca
    {
        public enum Funzionalita
        {
            Versamento,
            Prelievo,
            ReportSaldo,
            Sblocco,
            Uscita
        }


        public enum EsitoLogin
        {
            AccessoConsentito,
            UtentePasswordErrati,
            PasswordErrata,
            AccountBloccato
        }

        private string _nome;
        private List<Utente> _utenti;
        private SortedList<int, Funzionalita> _funzionalita;


        private Utente _utenteCorrente;

        
        internal SortedList<int, Funzionalita> ElencoFunzionalita { get => _funzionalita; set => _funzionalita = value; }

       
    }
}
