using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Gerador_Mensagem_FpML
{
    /// <summary>
    /// Interaction logic for Swap_Form.xaml
    /// </summary>
    public partial class Swap_Form : UserControl
    {
        public Swap_Form()
        {
            InitializeComponent();
            u.DesabilitarTudo(gridz);
            cbEvento.IsEnabled = true;
        }

        #region Atributos

        MontadorSeções ms = new MontadorSeções();
        public ObservableCollection<Imposto> tiposDeImpostos = new ObservableCollection<Imposto>();
        public ObservableCollection<Indexador> indexadores = new ObservableCollection<Indexador>();
        private string tpImposto = string.Empty;
        private string vlrImposto = string.Empty;
        private string periodoIndexador = string.Empty;
        private string familia = string.Empty;
        private string serie = string.Empty;
        private string atributo = string.Empty;
        private string tipoAtributo = string.Empty;
        private string quantidadeFixingContratacao = string.Empty;
        private string quantidadeFixingVencimento = string.Empty;
        private string quantidadeFixingTermo = string.Empty;
        private string dataFixingContratacao = string.Empty;
        private string dataFixingVencimento = string.Empty;
        private string dataFixingTermo = string.Empty;
        private string percentual = string.Empty;
        private string baseTaxa = string.Empty;
        private string valorCotacaoInicial = string.Empty;
        private string taxaPre = string.Empty;
        private string tipoCotacaoLiquidacao = string.Empty;
        private string quantidadeFixingCotacaoLiquidacao = string.Empty;
        private string referenciaCotacao = string.Empty;
        private string parteOperacao = string.Empty;
        Mascaras m = new Mascaras();
        Utilitarios u = new Utilitarios();

        #endregion

        #region Propriedades

        public string TpImposto
        {
            get
            {
                return tpImposto;
            }
            set
            {
                tpImposto = value;
            }
        }

        public string VlrImposto
        {
            get
            {
                return vlrImposto;
            }
            set
            {
                vlrImposto = value;
            }
        }

        #endregion

        #region Eventos

        private void btnGerarFpML_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string fpmlSaida = ms.MontaFpMLSwap(montarObjetoFpML());
            if (fpmlSaida.Contains("EVENTO") || fpmlSaida.Contains("TIPO SWAP"))
            {
                MessageBox.Show(fpmlSaida, "Erro!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    taFpML.Text = XDocument.Parse(fpmlSaida).ToString();
                    btnExportarFpML.IsEnabled = true;
                }
                catch(Exception ex)
                {
                    taFpML.Text = ex.ToString();
                }
            }
        }

        private void btnExportarFpML_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            XDocument doc = new XDocument();
            doc = XDocument.Parse(taFpML.Text);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML-File | *.xml";
            if (saveFileDialog.ShowDialog() == true)
            {
                doc.Save(saveFileDialog.FileName);
            }
        }

        private void tbPrecoUnitario_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            tbPrecoUnitario = m.AplicarMascaraNumericaSimples(e, tbPrecoUnitario);
        }

        private void ValorNumericoInteiro_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            sender = m.AplicarMascaraNumericaSimples(e, (TextBox)sender);
        }

        private void ValorAlfanumerico_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            sender = m.AplicarMascaraAlfanumericaSimples(e, (TextBox)sender);
        }

        private void cbTipoContaLiquidacaoParte_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbTipoContaLiquidacaoParte.SelectedIndex.Equals(2))
            {
                tbNumeroBancoContaLiquidacaoParte.IsEnabled = false;
                tbNumeroAgenciaLiquidacaoParte.IsEnabled = false;
                tbNumeroDigitoLiquidacaoParte.IsEnabled = false;
            }
            else
            {
                tbNumeroBancoContaLiquidacaoParte.IsEnabled = true;
                tbNumeroAgenciaLiquidacaoParte.IsEnabled = true;
                tbNumeroDigitoLiquidacaoParte.IsEnabled = true;
            }
        }

        private void cbTipoContaLiquidacaoContraparte_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbTipoContaLiquidacaoContraparte.SelectedIndex.Equals(2))
            {
                tbNumeroBancoContaLiquidacaoContraparte.IsEnabled = false;
                tbNumeroAgenciaLiquidacaoContraparte.IsEnabled = false;
                tbNumeroDigitoLiquidacaoContraparte.IsEnabled = false;
            }
            else
            {
                tbNumeroBancoContaLiquidacaoContraparte.IsEnabled = true;
                tbNumeroAgenciaLiquidacaoContraparte.IsEnabled = true;
                tbNumeroDigitoLiquidacaoContraparte.IsEnabled = true;
            }
        }

        private void tbNumeroBancoContaLiquidacaoContraparte_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (tbNumeroBancoContaLiquidacaoContraparte.Text.Equals("341"))
            {
                tbCodEmpresaConglomeradoContraparte.IsEnabled = true;
            }
            else
            {
                tbCodEmpresaConglomeradoContraparte.IsEnabled = false;
            }
        }

        private void tbNumeroBancoContaLiquidacaoContraparte_KeyUp(object sender, KeyEventArgs e)
        {
            if (tbNumeroBancoContaLiquidacaoContraparte.Text.Equals("341"))
            {
                tbCodEmpresaConglomeradoContraparte.IsEnabled = true;
            }
            else
            {
                tbCodEmpresaConglomeradoContraparte.IsEnabled = false;
            }
        }

        private void tbNumeroBancoContaLiquidacaoParte_KeyUp(object sender, KeyEventArgs e)
        {
            if (tbNumeroBancoContaLiquidacaoParte.Text.Equals("341"))
            {
                tbCodEmpresaConglomeradoParte.IsEnabled = true;
            }
            else
            {
                tbCodEmpresaConglomeradoParte.IsEnabled = false;
            }
        }

        private void btnLimpar_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            u.LimparFormGrid(gridz);
            tiposDeImpostos.Clear();
            u.DesabilitarTudo(gridz);
            cbEvento.IsEnabled = true;
        }

        private void cbEvento_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //LIQUIDAÇÃO
            if (cbEvento.SelectedIndex.Equals(1))
            {
                u.HabilitarTudo(gridz);
                //dpDataEfetivaVencimento.IsEnabled = true;
                //cbTipoImposto.IsEnabled = true;
                //tbValorImposto.IsEnabled = true;
                AddRow.IsEnabled = true;
                tbEKMCancelamento.IsEnabled = false;
                tbMotivoCancelamento.IsEnabled = false;
            }
            //CANCELAMENTO
            else if(cbEvento.SelectedIndex.Equals(2))
            {
                u.DesabilitarTudo(gridz);
                cbEvento.IsEnabled = true;
                tbEKMCancelamento.IsEnabled = true;
                tbMotivoCancelamento.IsEnabled = true;
                tbFuncionalOperador.IsEnabled = true;
            }
            else
            {
                u.HabilitarTudo(gridz);
                dpDataEfetivaVencimento.IsEnabled = false;
                cbTipoImposto.IsEnabled = false;
                tbValorImposto.IsEnabled = false;
                AddRow.IsEnabled = false;
                tbEKMCancelamento.IsEnabled = false;
                tbMotivoCancelamento.IsEnabled = false;
            }
        }

        private void cbTipoSwap_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                if (cbTipoSwap.SelectedIndex >= 0 && cbTipoSwap.SelectedItem.ToString().Contains("Barreira"))
                {
                    tbBarreiraContraparteQuantDiasFixingRebate.IsEnabled = true;
                    tbBarreiraContraparteValor.IsEnabled = true;
                    tbBarreiraParteQuantDiasFixingRebate.IsEnabled = true;
                    tbBarreiraParteValor.IsEnabled = true;
                    cbBarreiraContraparteFrequenciaVerificacao.IsEnabled = true;
                    cbBarreiraContraparteTipoCotacao.IsEnabled = true;
                    cbBarreiraContraparteTipoDirecao.IsEnabled = true;
                    cbBarreiraContraparteTipoPagamentoRebate.IsEnabled = true;
                    cbBarreiraParteFrequenciaVerificacao.IsEnabled = true;
                    cbBarreiraParteTipoCotacao.IsEnabled = true;
                    cbBarreiraParteTipoDirecao.IsEnabled = true;
                    cbBarreiraParteTipoPagamentoRebate.IsEnabled = true;
                    dpBarreiraContraparteDataFixingRebate.IsEnabled = true;
                    dpBarreiraContraparteDataVerificacao.IsEnabled = true;
                    dpBarreiraParteDataFixingRebate.IsEnabled = true;
                    dpBarreiraParteDataVerificacao.IsEnabled = true;
                }
                else
                {
                    tbBarreiraContraparteQuantDiasFixingRebate.IsEnabled = false;
                    tbBarreiraContraparteValor.IsEnabled = false;
                    tbBarreiraParteQuantDiasFixingRebate.IsEnabled = false;
                    tbBarreiraParteValor.IsEnabled = false;
                    cbBarreiraContraparteFrequenciaVerificacao.IsEnabled = false;
                    cbBarreiraContraparteTipoCotacao.IsEnabled = false;
                    cbBarreiraContraparteTipoDirecao.IsEnabled = false;
                    cbBarreiraContraparteTipoPagamentoRebate.IsEnabled = false;
                    cbBarreiraParteFrequenciaVerificacao.IsEnabled = false;
                    cbBarreiraParteTipoCotacao.IsEnabled = false;
                    cbBarreiraParteTipoDirecao.IsEnabled = false;
                    cbBarreiraParteTipoPagamentoRebate.IsEnabled = false;
                    dpBarreiraContraparteDataFixingRebate.IsEnabled = false;
                    dpBarreiraContraparteDataVerificacao.IsEnabled = false;
                    dpBarreiraParteDataFixingRebate.IsEnabled = false;
                    dpBarreiraParteDataVerificacao.IsEnabled = false;
            }
        }

        private void AddRow_Click(object sender, RoutedEventArgs e)
        {
            if (cbTipoImposto.SelectedIndex >= 0 && !string.IsNullOrEmpty(tbValorImposto.Text))
            {
                TpImposto = cbTipoImposto.SelectedItem.ToString().Substring(38);
                VlrImposto = tbValorImposto.Text;
                
                IEnumerable<Imposto> dadoExistente = from p in tiposDeImpostos where p.TipoImposto == this.TpImposto && p.ValorImposto == this.VlrImposto select p;
                if (dadoExistente.Count().Equals(0))
                {
                    tiposDeImpostos.Add(new Imposto() { TipoImposto = TpImposto, ValorImposto = VlrImposto });
                    dgImpostos.DataContext = tiposDeImpostos;

                    cbTipoImposto.SelectedIndex = -1;
                    tbValorImposto.Clear();
                }
                else
                {
                    MessageBox.Show("Tributo já inserido.", "Erro", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        #endregion

        #region Carga Objeto FpML

        private FPMLObject montarObjetoFpML()
        {
            FPMLObject fpml = new FPMLObject();
            fpml.EVENTO = cbEvento.SelectedIndex.ToString();
            fpml.CHAVE_EKM = tbEKM.Text;
            fpml.ALTERACAO = (bool)cbAlteracao.IsChecked;
            fpml.CHAVE_NATURAL = tbChaveNatural.Text;
            fpml.CHAVE_PROCESSO_FUNCIONAL = tbProcessoFunc.Text;
            fpml.CODIGO_SEGMENTO_CLIENTE = tbSegmentoCliente.Text;
            fpml.DATA_CARENCIA = dpDataCarencia.SelectedDate.ToString();
            fpml.DATA_INICIO_VALORIZACAO = dpDataInicioValoriza.SelectedDate.ToString();
            fpml.DATA_INICIO_VIGENCIA = dpDataInicioVigencia.SelectedDate.ToString();
            fpml.DATA_NEGOCIACAO = dpDataNegociacao.SelectedDate.ToString();
            fpml.DATA_PREVISTA_VENCIMENTO = dpDataPrevistaVcto.SelectedDate.ToString();
            fpml.FORMA_LIQUIDACAO = (cbFormaLiq.SelectedIndex + 1).ToString();
            fpml.ID_CLIENTE_CONTRAPARTE = tbIdClienteContraparte.Text;
            fpml.ID_CLIENTE_PARTE = tbIdClienteParte.Text;
            fpml.ID_PORTFOLIO = tbPortfolio.Text;
            fpml.FUNCIONAL_OPERADOR = tbFuncionalOperador.Text;
            fpml.CODIGO_FIGURA = tbCodigoFigura.Text;
            fpml.CAMARA_REGISTRO = cbNomeCamaraRegistro.SelectedItem == null ? string.Empty : cbNomeCamaraRegistro.SelectedItem.ToString().Substring(38);
            fpml.TIPO_FORMA_LIQUIDACAO = cbTipoFormaLiquidacao.SelectedItem == null ? string.Empty : cbTipoFormaLiquidacao.SelectedItem.ToString().Substring(38);
            fpml.CODIGO_IF = tbCodIF.Text;
            fpml.CODIGO_TIPO_CONDICAO_MERCADO_LIQUIDACAO = (cbTipoCondicaoMercado.SelectedIndex + 1).ToString();
            fpml.MODALIDADE = (cbModalidade.SelectedIndex + 1).ToString();
            fpml.ID_OFERTA = tbIdOferta.Text;
            fpml.INDICADOR_OFERTA_PUBLICA = cbIdOfertaPublica.IsChecked.ToString();
            fpml.PERCENTUAL_CAPITAL_GARANTIDO_CETIP = tbPercentualCapitalGarantidoCetip.Text;
            fpml.PERCENTUAL_BASE_APLICACAO_CAIXA = tbPercentualBaseAplicacaoCaixa.Text;
            fpml.PROTECAO_PROVENTOS = (cbProtecaoContraProventos.SelectedIndex + 1).ToString();
            fpml.INDICADOR_QUANTO = cbVariacaoQuanto.IsChecked.ToString();
            fpml.VALOR_PARIDADE_QUANTO = tbValorParidadeQuanto.Text;
            fpml.MODALIDADE_LIQUIDACAO = cbTipoModalidadeLiquidacao.SelectedIndex.ToString();
            fpml.GRUPO_ECONOMICO_PARTE = tbGrupoEconomicoParte.Text;
            fpml.GRUPO_ECONOMICO_CONTRAPARTE = tbGrupoEconomicoContraparte.Text;
            fpml.TIPO_CONTA_LIQUIDACAO_PARTE = u.PreencherTipoConta(cbTipoContaLiquidacaoParte);
            fpml.NUMERO_BANCO_LIQUIDACAO_PARTE = tbNumeroBancoContaLiquidacaoParte.Text;
            fpml.CODIGO_EMPRESA_CONGLOMERADO_LIQUIDACAO_PARTE = tbCodEmpresaConglomeradoParte.Text;
            fpml.NUMERO_AGENCIA_CONTA_LIQUIDACAO_PARTE = tbNumeroAgenciaLiquidacaoParte.Text;
            fpml.NUMERO_CONTA_LIQUIDACAO_PARTE = tbNumeroContaLiquidacaoParte.Text;
            fpml.NUMERO_DIGITO_VERIFICADOR_CONTA_LIQUIDACAO_PARTE = tbNumeroDigitoLiquidacaoParte.Text;
            fpml.TIPO_CONTA_LIQUIDACAO_CONTRAPARTE = u.PreencherTipoConta(cbTipoContaLiquidacaoContraparte);
            fpml.NUMERO_BANCO_LIQUIDACAO_CONTRAPARTE = tbNumeroBancoContaLiquidacaoContraparte.Text;
            fpml.CODIGO_EMPRESA_CONGLOMERADO_LIQUIDACAO_CONTRAPARTE = tbCodEmpresaConglomeradoContraparte.Text;
            fpml.NUMERO_AGENCIA_CONTA_LIQUIDACAO_CONTRAPARTE = tbNumeroAgenciaLiquidacaoContraparte.Text;
            fpml.NUMERO_CONTA_LIQUIDACAO_CONTRAPARTE = tbNumeroContaLiquidacaoContraparte.Text;
            fpml.NUMERO_DIGITO_VERIFICADOR_CONTA_LIQUIDACAO_CONTRAPARTE = tbNumeroDigitoLiquidacaoContraparte.Text;
            fpml.DATA_EFETIVA_VENCIMENTO = dpDataEfetivaVencimento.SelectedDate.ToString();
            fpml.COMPRADOR = cbComprador.SelectedIndex >= 0 ? cbComprador.SelectedItem.ToString().Substring(38) : "Contraparte";
            fpml.TIPO_SWAP = cbTipoSwap.SelectedIndex.ToString();
            fpml.FATOR_LIMITADOR_SUPERIOR = tbFatorLimitadorSuperior.Text;
            fpml.FATOR_LIMITADOR_INFERIOR = tbFatorLimitadorInferior.Text;
            fpml.CHAVE_EKM_OPERACAO_VINCULO = tbChaveEKMOperacaoVinculo.Text;

            fpml.INDEXADOR_PARTE_CODIGO_ATRIBUTO = tbIndexParteAtrib.Text;
            fpml.INDEXADOR_PARTE_CODIGO_FAMILIA = tbIndexParteFamilia.Text;
            fpml.INDEXADOR_PARTE_CODIGO_SERIE = tbIndexParteSerie.Text;
            fpml.INDEXADOR_PARTE_CODIGO_TIPO_ATRIBUTO = tbIndexParteTipoAtrib.Text;
            fpml.INDEXADOR_PARTE_PERCENTUAL_INDEXADOR = tbIndexPartePercentualIndex.Text;
            fpml.INDEXADOR_PARTE_TAXA_PRE = tbIndexParteTaxaPre.Text;
            fpml.INDEXADOR_PARTE_VALOR_COTACAO_INICIAL = tbIndexParteValorCotInicial.Text;
            fpml.INDEXADOR_PARTE_BASE_TAXA = tbIndexParteBaseTaxa.Text;
            fpml.INDEXADOR_PARTE_TIPO_COTACAO_LIQUIDACAO = u.PreencherTipoCotacao(cbIndexParteTipoCotacaoLiquidacao);
            fpml.INDEXADOR_PARTE_QUANTIDADE_FIXING_COTACAO_LIQUIDACAO = tbIndexParteQuantCotLiq.Text;
            fpml.INDEXADOR_PARTE_REFERENCIA_COTACAO = cbIndexParteRefCotacao.SelectedItem == null ? string.Empty : cbIndexParteRefCotacao.SelectedItem.ToString().Substring(38);

            fpml.INDEXADOR_TERMO_DATA_FIXING = dpIndexTermoDataFixing.Text;
            fpml.INDEXADOR_TERMO_QUANTIDADE_FIXING = tbIndexTermoQuantFixing.Text;
            fpml.INDEXADOR_PARTE_DATA_FIXING_CONTRATACAO = dpIndexParteDataFixingContrat.SelectedDate.ToString();
            fpml.INDEXADOR_PARTE_DATA_FIXING_VENCIMENTO = dpIndexParteDataFixingVcto.SelectedDate.ToString();
            fpml.INDEXADOR_PARTE_QUANTIDADE_FIXING_CONTRATACAO = tbIndexParteQuantFixingContrat.Text;
            fpml.INDEXADOR_PARTE_QUANTIDADE_FIXING_VENCIMENTO = tbIndexParteQuantFixingVcto.Text;
            fpml.INDEXADOR_CONTRAPARTE_CODIGO_ATRIBUTO = tbIndexContraparteAtrib.Text;
            fpml.INDEXADOR_CONTRAPARTE_CODIGO_FAMILIA = tbIndexContraparteFamilia.Text;
            fpml.INDEXADOR_CONTRAPARTE_CODIGO_SERIE = tbIndexContraparteSerie.Text;
            fpml.INDEXADOR_CONTRAPARTE_CODIGO_TIPO_ATRIBUTO = tbIndexContraparteTipoAtrib.Text;
            fpml.INDEXADOR_CONTRAPARTE_PERCENTUAL_INDEXADOR = tbIndexContrapartePercentualIndex.Text;
            fpml.INDEXADOR_CONTRAPARTE_TAXA_PRE = tbIndexContraparteTaxaPre.Text;
            fpml.INDEXADOR_CONTRAPARTE_VALOR_COTACAO_INICIAL = tbIndexContraparteValorCotInicial.Text;
            fpml.INDEXADOR_CONTRAPARTE_BASE_TAXA = tbIndexContraparteBaseTaxa.Text;
            fpml.INDEXADOR_CONTRAPARTE_TIPO_COTACAO_LIQUIDACAO = u.PreencherTipoCotacao(cbIndexContraparteTipoCotacaoLiquidacao);
            fpml.INDEXADOR_CONTRAPARTE_QUANTIDADE_FIXING_COTACAO_LIQUIDACAO = tbIndexContraparteQuantCotLiq.Text;
            fpml.INDEXADOR_CONTRAPARTE_REFERENCIA_COTACAO = cbIndexContraparteRefCotacao.SelectedItem == null ? string.Empty : cbIndexContraparteRefCotacao.SelectedItem.ToString().Substring(38);

            fpml.INDEXADOR_CONTRAPARTE_DATA_FIXING_CONTRATACAO = dpIndexContraparteDataFixingContrat.SelectedDate.ToString();
            fpml.INDEXADOR_CONTRAPARTE_DATA_FIXING_VENCIMENTO = dpIndexContraparteDataFixingVcto.SelectedDate.ToString();
            fpml.INDEXADOR_CONTRAPARTE_QUANTIDADE_FIXING_CONTRATACAO = tbIndexContraparteQuantFixingContrat.Text;
            fpml.INDEXADOR_CONTRAPARTE_QUANTIDADE_FIXING_VENCIMENTO = tbIndexContraparteQuantFixingVcto.Text;

            if (cbTipoSwap.SelectedIndex >= 0)
            {
                if (cbTipoSwap.SelectedItem.ToString().Contains("Barreira"))
                {
                    fpml.PRODUTO_BARREIRA = true;

                    fpml.BARREIRA_PARTE_DATA_VERIFICACAO = dpBarreiraParteDataVerificacao.SelectedDate.ToString();
                    fpml.BARREIRA_PARTE_VALOR = tbBarreiraParteValor.Text;
                    fpml.BARREIRA_PARTE_DATA_FIXING_REBATE = dpBarreiraParteDataFixingRebate.SelectedDate.ToString();
                    fpml.BARREIRA_PARTE_TIPO_DATA_PAGAMENTO_REBATE = cbBarreiraParteTipoPagamentoRebate.SelectedIndex >= 0 ? cbBarreiraParteTipoPagamentoRebate.SelectedIndex.ToString() : string.Empty;
                    fpml.BARREIRA_PARTE_QUANTIDADE_DIAS_FIXING_REBATE = tbBarreiraParteQuantDiasFixingRebate.Text;
                    fpml.BARREIRA_PARTE_TIPO_VERIFICACAO = cbBarreiraParteFrequenciaVerificacao.SelectedIndex >= 0 ? cbBarreiraParteFrequenciaVerificacao.SelectedIndex.ToString() : string.Empty;
                    fpml.BARREIRA_PARTE_TIPO = cbBarreiraParteTipoDirecao.SelectedIndex >= 0 ? cbBarreiraParteTipoDirecao.SelectedItem.ToString().Substring(38) : string.Empty;
                    //fpml.REGISTRO_CAMARA_TIPO_COTACAO_LIQUIDACAO_BARREIRA_PARTE = u.PreencherTipoCotacao(cbBarreiraParteTipoCotacao);

                    fpml.BARREIRA_CONTRAPARTE_DATA_VERIFICACAO = dpBarreiraContraparteDataVerificacao.SelectedDate.ToString();
                    fpml.BARREIRA_CONTRAPARTE_VALOR = tbBarreiraContraparteValor.Text;
                    fpml.BARREIRA_CONTRAPARTE_DATA_FIXING_REBATE = dpBarreiraContraparteDataFixingRebate.SelectedDate.ToString();
                    fpml.BARREIRA_CONTRAPARTE_TIPO_DATA_PAGAMENTO_REBATE = cbBarreiraContraparteTipoPagamentoRebate.SelectedIndex >= 0 ? cbBarreiraContraparteTipoPagamentoRebate.SelectedIndex.ToString() : string.Empty;
                    fpml.BARREIRA_CONTRAPARTE_QUANTIDADE_DIAS_FIXING_REBATE = tbBarreiraContraparteQuantDiasFixingRebate.Text;
                    fpml.BARREIRA_CONTRAPARTE_TIPO_VERIFICACAO = cbBarreiraContraparteFrequenciaVerificacao.SelectedIndex >= 0 ? cbBarreiraContraparteFrequenciaVerificacao.SelectedIndex.ToString() : string.Empty;
                    fpml.BARREIRA_CONTRAPARTE_TIPO = cbBarreiraContraparteTipoDirecao.SelectedIndex >= 0 ? cbBarreiraContraparteTipoDirecao.SelectedItem.ToString().Substring(38) : string.Empty;
                    //fpml.REGISTRO_CAMARA_TIPO_COTACAO_LIQUIDACAO_BARREIRA_CONTRAPARTE = u.PreencherTipoCotacao(cbBarreiraContraparteTipoCotacao);
                }
                else
                {
                    fpml.PRODUTO_BARREIRA = false;
                }

                fpml.TIPO_IMPOSTO = new ObservableCollection<Imposto>();
                if (tiposDeImpostos.Count > 0)
                {
                    foreach (Imposto i in tiposDeImpostos)
                    {
                        fpml.TIPO_IMPOSTO.Add(i);
                    }
                }

                fpml.INDEXADORES = new ObservableCollection<Indexador>();
                if (indexadores.Count > 0)
                {
                    foreach (Indexador i in indexadores)
                    {
                        fpml.INDEXADORES.Add(i);
                    }
                }
            }

            return fpml;
        }

        #endregion
    }
}
