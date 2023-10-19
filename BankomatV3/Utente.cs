using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankomatV3
{
    class Utente
    {
        private string _nomeUtente;
        private string _password;
        private int _tentativiDiAccessoErrati = 0;
        private bool _bloccato = false;
        private const int _tentativiDiAccessoPermessi = 3;
        


        public int TentativiDiAccessoResidui
        {
            get
            {
                return _tentativiDiAccessoPermessi - _tentativiDiAccessoErrati;
            }
        }
        public int TentativiDiAccessoErrati
        {
            get => _tentativiDiAccessoErrati;
            set
            {
                _tentativiDiAccessoErrati = value;
                if (_tentativiDiAccessoErrati >= _tentativiDiAccessoPermessi)
                {
                    _bloccato = true;
                }
            }
        }
    }
}
