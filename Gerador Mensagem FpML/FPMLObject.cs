using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerador_Mensagem_FpML
{
    public class FPMLObject
    {
        public bool ALTERACAO;

        public string BARREIRA_CONTRAPARTE_DATA_VERIFICACAO;
        public string BARREIRA_CONTRAPARTE_MOEDA_REBATE;
        public string BARREIRA_CONTRAPARTE_DATA_FIXING_REBATE;
        public string BARREIRA_CONTRAPARTE_TIPO_DATA_PAGAMENTO_REBATE;
        public string BARREIRA_CONTRAPARTE_QUANTIDADE_DIAS_FIXING_REBATE;
        public string BARREIRA_CONTRAPARTE_TIPO_VERIFICACAO;
        public string BARREIRA_CONTRAPARTE_TIPO;
        public string BARREIRA_CONTRAPARTE_VALOR;
        public string BARREIRA_PARTE_DATA_VERIFICACAO;
        public string BARREIRA_PARTE_VALOR;
        public string BARREIRA_PARTE_MOEDA_REBATE;
        public string BARREIRA_PARTE_DATA_FIXING_REBATE;
        public string BARREIRA_PARTE_TIPO_DATA_PAGAMENTO_REBATE;
        public string BARREIRA_PARTE_QUANTIDADE_DIAS_FIXING_REBATE;
        public string BARREIRA_PARTE_TIPO_VERIFICACAO;
        public string BARREIRA_PARTE_TIPO;

        public string CAMARA_REGISTRO;
        public string CHAVE_EKM;
        public string CHAVE_EKM_CANCELAMENTO;
        public ObservableCollection<Instrumento> CHAVE_EKM_INSTRUMENTOS;
        public string CHAVE_EKM_OPERACAO_VINCULO;
        public string CHAVE_PROCESSO_FUNCIONAL;
        public string CHAVE_NATURAL;
        public string CODIGO_EMPRESA_CONGLOMERADO_LIQUIDACAO_CONTRAPARTE;
        public string CODIGO_EMPRESA_CONGLOMERADO_LIQUIDACAO_PARTE;
        public string CODIGO_FIGURA;
        public string CODIGO_IF;
        public string CODIGO_MOEDA;
        public string CODIGO_SEGMENTO_CLIENTE;
        public string CODIGO_TIPO_CONDICAO_MERCADO_LIQUIDACAO;
        public string CODIGO_TIPO_CONDICAO_RESGATE_FINANCEIRO;
        public string COMPRADOR;

        public string DATA_CARENCIA;
        public string DATA_EFETIVA_VENCIMENTO;
        public string DATA_INICIO_VALORIZACAO;
        public string DATA_INICIO_VIGENCIA;
        public string DATA_NEGOCIACAO;
        public string DATA_PREVISTA_VENCIMENTO;

        public string EVENTO;

        public string FATOR_LIMITADOR_INFERIOR;
        public string FATOR_LIMITADOR_SUPERIOR;
        public string FORMA_LIQUIDACAO;
        public string FUNCIONAL_OPERADOR;

        public string GRUPO_ECONOMICO_CONTRAPARTE;
        public string GRUPO_ECONOMICO_PARTE;

        public string ID_CLIENTE_CONTRAPARTE;
        public string ID_CLIENTE_PARTE;
        public string ID_OFERTA;
        public string ID_PORTFOLIO;
        public string INDEXADOR_CONTRAPARTE_CODIGO_ATRIBUTO;
        public string INDEXADOR_CONTRAPARTE_CODIGO_FAMILIA;
        public string INDEXADOR_CONTRAPARTE_CODIGO_SERIE;
        public string INDEXADOR_CONTRAPARTE_CODIGO_TIPO_ATRIBUTO;
        public string INDEXADOR_CONTRAPARTE_QUANTIDADE_FIXING_CONTRATACAO;
        public string INDEXADOR_CONTRAPARTE_QUANTIDADE_FIXING_VENCIMENTO;
        public string INDEXADOR_CONTRAPARTE_DATA_FIXING_CONTRATACAO;
        public string INDEXADOR_CONTRAPARTE_DATA_FIXING_VENCIMENTO;
        public string INDEXADOR_CONTRAPARTE_PERCENTUAL_INDEXADOR;
        public string INDEXADOR_CONTRAPARTE_TAXA_PRE;
        public string INDEXADOR_CONTRAPARTE_VALOR_COTACAO_INICIAL;
        public string INDEXADOR_CONTRAPARTE_BASE_TAXA;
        public string INDEXADOR_CONTRAPARTE_TIPO_COTACAO_LIQUIDACAO;
        public string INDEXADOR_CONTRAPARTE_REFERENCIA_COTACAO;
        public string INDEXADOR_CONTRAPARTE_QUANTIDADE_FIXING_COTACAO_LIQUIDACAO;
        public string INDEXADOR_PARTE_CODIGO_ATRIBUTO;
        public string INDEXADOR_PARTE_CODIGO_FAMILIA;
        public string INDEXADOR_PARTE_CODIGO_SERIE;
        public string INDEXADOR_PARTE_CODIGO_TIPO_ATRIBUTO;
        public string INDEXADOR_PARTE_QUANTIDADE_FIXING_CONTRATACAO;
        public string INDEXADOR_PARTE_QUANTIDADE_FIXING_VENCIMENTO;
        public string INDEXADOR_PARTE_QUANTIDADE_FIXING_COTACAO_LIQUIDACAO;
        public string INDEXADOR_PARTE_DATA_FIXING_CONTRATACAO;
        public string INDEXADOR_PARTE_DATA_FIXING_VENCIMENTO;
        public string INDEXADOR_PARTE_PERCENTUAL_INDEXADOR;
        public string INDEXADOR_PARTE_TAXA_PRE;
        public string INDEXADOR_PARTE_VALOR_COTACAO_INICIAL;
        public string INDEXADOR_PARTE_BASE_TAXA;
        public string INDEXADOR_PARTE_TIPO_COTACAO_LIQUIDACAO;
        public string INDEXADOR_PARTE_REFERENCIA_COTACAO;
        public string INDEXADOR_TERMO_CODIGO_ATRIBUTO_PERIODO;
        public string INDEXADOR_TERMO_CODIGO_FAMILIA_PERIODO;
        public string INDEXADOR_TERMO_CODIGO_SERIE_PERIODO;
        public string INDEXADOR_TERMO_CODIGO_TIPO_ATRIBUTO_PERIODO;
        public string INDEXADOR_TERMO_DATA_FIXING;
        public string INDEXADOR_TERMO_QUANTIDADE_FIXING;
        public ObservableCollection<Indexador> INDEXADORES;
        public string INDICADOR_NEM;
        public string INDICADOR_OFERTA_PUBLICA;
        public string INDICADOR_QUANTO;

        public string MODALIDADE;
        public string MODALIDADE_LIQUIDACAO;
        public string MOTIVO_CANCELAMENTO;

        public string NUMERO_AGENCIA_CONTA_LIQUIDACAO_CONTRAPARTE;
        public string NUMERO_AGENCIA_CONTA_LIQUIDACAO_PARTE;
        public string NUMERO_BANCO_LIQUIDACAO_CONTRAPARTE;
        public string NUMERO_BANCO_LIQUIDACAO_PARTE;
        public string NUMERO_CONTA_LIQUIDACAO_CONTRAPARTE;
        public string NUMERO_CONTA_LIQUIDACAO_PARTE;
        public string NUMERO_CONTRATO;
        public string NUMERO_DIGITO_VERIFICADOR_CONTA_LIQUIDACAO_CONTRAPARTE;
        public string NUMERO_DIGITO_VERIFICADOR_CONTA_LIQUIDACAO_PARTE;
        public string NUMERO_VERSAO;

        public string PERCENTUAL_BASE_APLICACAO_CAIXA;
        public string PERCENTUAL_CAPITAL_GARANTIDO_CETIP;
        public bool PRODUTO_BARREIRA;
        public string PROTECAO_PROVENTOS;

        public string QUANTIDADE_ATIVOS_LIQUIDADOS;
        public string QUANTIDADE_ATIVOS_NEGOCIADOS;
        public string QUANTIDADE_DIAS_CORRIDOS;
        public string QUANTIDADE_DIAS_UTEIS;

        public string REGISTRO_CAMARA_TIPO_COTACAO_LIQUIDACAO_BARREIRA_BAIXA;
        public string REGISTRO_CAMARA_TIPO_COTACAO_LIQUIDACAO_BARREIRA_ALTA;
        public string REGISTRO_CAMARA_TIPO_COTACAO_LIQUIDACAO;

        public string SIGLA_SISTEMA;

        public string TIPO_BARREIRA_CONTRAPARTE;
        public string TIPO_BARREIRA_PARTE;
        public string TIPO_CONTA_LIQUIDACAO_CONTRAPARTE;
        public string TIPO_CONTA_LIQUIDACAO_PARTE;
        public string TIPO_FORMA_LIQUIDACAO;
        public ObservableCollection<Imposto> TIPO_IMPOSTO;
        public string TIPO_SWAP;

        public string VALOR_FINANCEIRO_LIQUIDO;
        public string VALOR_OPERACAO;
        public string VALOR_PARIDADE_QUANTO;
        public string VALOR_PRECO_UNITARIO_OPERACAO;
        
    }
}
