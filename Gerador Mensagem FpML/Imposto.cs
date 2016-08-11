using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerador_Mensagem_FpML
{
    public class Imposto
    {
        private string tipoImposto;

        public string TipoImposto
        {
            get { return tipoImposto; }
            set { tipoImposto = value; }
        }
        
        private string valorImposto;

        public string ValorImposto
        {
            get { return valorImposto; }
            set { valorImposto = value; }
        }

    }
}
