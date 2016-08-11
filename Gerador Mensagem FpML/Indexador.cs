using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerador_Mensagem_FpML
{
    public class Indexador
    {
        #region Atributos

        private string periodoIndexador;
        private string familia;
        private string serie;
        private string atributo;
        private string tipoAtributo;
        private string quantidadeFixingContratacao;
        private string quantidadeFixingVencimento;
        private string quantidadeFixingTermo;
        private string dataFixingContratacao;
        private string dataFixingVencimento;
        private string dataFixingTermo;
        private string percentual;
        private string baseTaxa;
        private string valorCotacaoInicial;
        private string taxaPre;
        private string tipoCotacaoLiquidacao;
        private string quantidadeFixingCotacaoLiquidacao;
        private string referenciaCotacao;
        private string parteOperacao;

       

        #endregion

        #region Propriedades

        public string PeriodoIndexador
        {
            get { return periodoIndexador; }
            set { periodoIndexador = value; }
        }

        public string Familia
        {
            get { return familia; }
            set { familia = value; }
        }

        public string Serie
        {
            get { return serie; }
            set { serie = value; }
        }

        public string Atributo
        {
            get { return atributo; }
            set { atributo = value; }
        }

        public string TipoAtributo
        {
            get { return tipoAtributo; }
            set { tipoAtributo = value; }
        }

        public string QuantidadeFixingContratacao
        {
            get { return quantidadeFixingContratacao; }
            set { quantidadeFixingContratacao = value; }
        }

        public string QuantidadeFixingVencimento
        {
            get { return quantidadeFixingVencimento; }
            set { quantidadeFixingVencimento = value; }
        }

        public string QuantidadeFixingTermo
        {
            get { return quantidadeFixingTermo; }
            set { quantidadeFixingTermo = value; }
        }

        public string DataFixingContratacao
        {
            get { return dataFixingContratacao; }
            set { dataFixingContratacao = value; }
        }

        public string DataFixingVencimento
        {
            get { return dataFixingVencimento; }
            set { dataFixingVencimento = value; }
        }

        public string DataFixingTermo
        {
            get { return dataFixingTermo; }
            set { dataFixingTermo = value; }
        }

        public string Percentual
        {
            get { return percentual; }
            set { percentual = value; }
        }

        public string BaseTaxa
        {
            get { return baseTaxa; }
            set { baseTaxa = value; }
        }

        public string ValorCotacaoInicial
        {
            get { return valorCotacaoInicial; }
            set { valorCotacaoInicial = value; }
        }

        public string TaxaPre
        {
            get { return taxaPre; }
            set { taxaPre = value; }
        }

        public string TipoCotacaoLiquidacao
        {
            get { return tipoCotacaoLiquidacao; }
            set { tipoCotacaoLiquidacao = value; }
        }

        public string QuantidadeFixingCotacaoLiquidacao
        {
            get { return quantidadeFixingCotacaoLiquidacao; }
            set { quantidadeFixingCotacaoLiquidacao = value; }
        }

        public string ReferenciaCotacao
        {
            get { return referenciaCotacao; }
            set { referenciaCotacao = value; }
        }

        public string ParteOperacao
        {
            get { return parteOperacao; }
            set { parteOperacao = value; }
        }

        #endregion
    }
}
