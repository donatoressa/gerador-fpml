using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerador_Mensagem_FpML
{
    public class MontadorSeções
    {
        #region Atributos

        Indexador indexadorParteValorizacao = new Indexador();
        Indexador contraparteValorizacao = new Indexador();
        Indexador parteTermo = new Indexador();
        Indexador contraparteTermo = new Indexador();
        TagsFpML tags = new TagsFpML();

        #endregion

        #region MontaFpML COE

        public string MontaFpMLCOE(FPMLObject fpml)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(MontaHeader(fpml).Replace(tags.N1N2N3, "23:218:2464").Replace("N4", "004"));
            sb.Append(tags.tagIsCorrection.Replace(tags.VALOR_VARIAVEL_1, fpml.ALTERACAO.ToString().ToLower()));

            if(fpml.EVENTO.Equals("0"))
            {
                sb.Append(MontaFpMLCOE_Contratacao(fpml));
            }
            else if(fpml.EVENTO.Equals("1"))
            {
                sb.Append(MontaFpMLCOE_Liquidacao(fpml));
            }
            else if (fpml.EVENTO.Equals("2"))
            {
                sb.Append(MontaFpMLCOE_Cancelamento(fpml));
            }
            else
            {
                return "Evento não informado.";
            }

            sb.Append(tags.tagRequestConfirmationEnd);

            return sb.ToString();
        }

        public string MontaFpMLCOE_Contratacao(FPMLObject fpml)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(tags.tagTrade.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_CONTRATO));
            sb.Append(tags.tagVersion.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_VERSAO));
            sb.Append(tags.tagChaveProcessoFuncional.Replace(tags.VALOR_VARIAVEL_1, fpml.CHAVE_PROCESSO_FUNCIONAL));
            sb.Append(tags.tagChaveNatural.Replace(tags.VALOR_VARIAVEL_1, fpml.CHAVE_NATURAL));
            //Incluir Codigo Estrutura
            sb.Append(tags.tagItauTradeId.Replace(tags.VALOR_VARIAVEL_1, string.Empty));
            sb.Append(tags.tagEKMsInstrumentosCOE);
            
            foreach(Instrumento i in fpml.CHAVE_EKM_INSTRUMENTOS)
            {
                sb.Append(tags.tagEKMInstrumento.Replace(tags.VALOR_VARIAVEL_1, i.ChaveEKMInstrumento));
            }

            sb.Append(tags.tagEKMsInstrumentosCOEEnd);
            sb.Append(tags.tagPartyTradeInformation.Replace(tags.VALOR_VARIAVEL_1, fpml.FUNCIONAL_OPERADOR));
            sb.Append(tags.tagTimestamps);
            sb.Append(tags.tagTimestampSystem.Replace(tags.VALOR_VARIAVEL_1, FormatarTimestamp()));
            sb.Append(tags.tagTimestampsEnd);
            sb.Append(tags.tagFigura.Replace(tags.VALOR_VARIAVEL_1, fpml.CODIGO_FIGURA));
            sb.Append(tags.tagPortfolio.Replace(tags.VALOR_VARIAVEL_1, fpml.ID_PORTFOLIO));
            sb.Append(tags.tagTradeDate.Replace(tags.VALOR_VARIAVEL_1, fpml.DATA_NEGOCIACAO));
            sb.Append(tags.tagBondTransaction.Replace(tags.VALOR_VARIAVEL_1, fpml.CODIGO_MOEDA));
            sb.Append(tags.tagValorOperacao.Replace(tags.VALOR_VARIAVEL_1, FormatarValor(fpml.VALOR_OPERACAO)));
            sb.Append(tags.tagBond.Replace(tags.VALOR_VARIAVEL_1, fpml.CAMARA_REGISTRO));
            sb.Append(tags.tagDataPrevistaVencimento.Replace(tags.VALOR_VARIAVEL_1, FormatarData(fpml.DATA_PREVISTA_VENCIMENTO)));
            sb.Append(tags.tagValorPrecoUnitario.Replace(tags.VALOR_VARIAVEL_1, FormatarValor(fpml.VALOR_PRECO_UNITARIO_OPERACAO)));
            sb.Append(tags.tagBaseTaxa.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_PARTE_BASE_TAXA));
            sb.Append(tags.tagFloatingRateParte.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_PARTE_CODIGO_SERIE));
            sb.Append(tags.tagPercentualIndexador.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_PARTE_PERCENTUAL_INDEXADOR));
            sb.Append(tags.tagTaxaPre.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_PARTE_TAXA_PRE));
            sb.Append(tags.tagReferenciaCotacao); //TODO: CRIAR CAMPO NO FORM
            sb.Append(tags.tagCodigoAtributo.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_PARTE_CODIGO_ATRIBUTO));
            sb.Append(tags.tagCodigoTipoAtributo.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_PARTE_CODIGO_TIPO_ATRIBUTO));
            sb.Append(tags.tagCodigoFamilia.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_PARTE_CODIGO_FAMILIA));
            sb.Append(tags.tagCodigoTipoCotacaoLiquidacao.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_PARTE_TIPO_COTACAO_LIQUIDACAO));
            sb.Append(tags.tagValorCotacaoInicial.Replace(tags.VALOR_VARIAVEL_1, FormatarValor(fpml.INDEXADOR_PARTE_VALOR_COTACAO_INICIAL)));
            sb.Append(tags.tagDataFixingContratacao.Replace(tags.VALOR_VARIAVEL_1, FormatarData(fpml.INDEXADOR_PARTE_DATA_FIXING_CONTRATACAO)));
            sb.Append(tags.tagQuantidadeFixingContratacao.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_PARTE_QUANTIDADE_FIXING_CONTRATACAO));
            sb.Append(tags.tagDataFixingVencimento.Replace(tags.VALOR_VARIAVEL_1, FormatarData(fpml.INDEXADOR_PARTE_DATA_FIXING_VENCIMENTO)));
            sb.Append(tags.tagQuantidadeFixingVencimento.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_PARTE_QUANTIDADE_FIXING_VENCIMENTO));
            sb.Append(tags.tagFixingIndexEnd);
            sb.Append(tags.tagFloatingRateCalculationEnd);

            sb.Append(tags.tagTipoFormaLiquidacao.Replace(tags.VALOR_VARIAVEL_1, fpml.TIPO_FORMA_LIQUIDACAO));
            sb.Append(tags.tagCodigoInstrumentoFinanceiro.Replace(tags.VALOR_VARIAVEL_1, fpml.CODIGO_IF));
            sb.Append(tags.tagBondEnd);
            sb.Append(tags.tagDataInicioValorizacao.Replace(tags.VALOR_VARIAVEL_1, FormatarData(fpml.DATA_INICIO_VALORIZACAO)));
            sb.Append(tags.tagDataCarencia.Replace(tags.VALOR_VARIAVEL_1, FormatarData(fpml.DATA_CARENCIA)));
            sb.Append(tags.tagTipoCondicaoMercadoLiquidacao.Replace(tags.VALOR_VARIAVEL_1, fpml.CODIGO_TIPO_CONDICAO_MERCADO_LIQUIDACAO));
            sb.Append(tags.tagModalidadeProduto.Replace(tags.VALOR_VARIAVEL_1, fpml.MODALIDADE));
            sb.Append(tags.tagCondicaoResgateFinanceiro.Replace(tags.VALOR_VARIAVEL_1, fpml.CODIGO_TIPO_CONDICAO_RESGATE_FINANCEIRO));
            sb.Append(tags.tagIdOferta.Replace(tags.VALOR_VARIAVEL_1, fpml.ID_OFERTA));
            sb.Append(tags.tagIndicadorOfertaPublica.Replace(tags.VALOR_VARIAVEL_1, fpml.INDICADOR_OFERTA_PUBLICA));
            sb.Append(tags.tagPrecoReferencia);
            sb.Append(tags.tagDiasUteis.Replace(tags.VALOR_VARIAVEL_1, fpml.QUANTIDADE_DIAS_UTEIS));
            sb.Append(tags.tagPercentualCapitalGarantidoCetip.Replace(tags.VALOR_VARIAVEL_1, fpml.PERCENTUAL_CAPITAL_GARANTIDO_CETIP));
            sb.Append(tags.tagPercentualBaseAplicacaoCaixa.Replace(tags.VALOR_VARIAVEL_1, fpml.PERCENTUAL_BASE_APLICACAO_CAIXA));
            sb.Append(tags.tagTipoProtecaoContraProventos.Replace(tags.VALOR_VARIAVEL_1, fpml.PROTECAO_PROVENTOS));
            sb.Append(tags.tagIndicadorVariacaoQuanto.Replace(tags.VALOR_VARIAVEL_1, fpml.INDICADOR_QUANTO));
            sb.Append(tags.tagValorParidadeQuanto.Replace(tags.VALOR_VARIAVEL_1, FormatarValor(fpml.VALOR_PARIDADE_QUANTO)));
            sb.Append(tags.tagModalidadeLiquidacao.Replace(tags.VALOR_VARIAVEL_1, fpml.MODALIDADE_LIQUIDACAO));
            sb.Append(tags.tagAtributosRegistroCamara);
            sb.Append(tags.tagBarreira);
            sb.Append(tags.tagBarreiraAlta);
            sb.Append(tags.tagLevelPercentage); // PREENCHER COM VALOR A CONFIRMAR (NÃO DEVERIA EXISTIR ESTA TAG)
            sb.Append(tags.tagCodigoTipoCotacaoLiquidacao.Replace(tags.VALOR_VARIAVEL_1, fpml.REGISTRO_CAMARA_TIPO_COTACAO_LIQUIDACAO_BARREIRA_ALTA));
            sb.Append(tags.tagBarreiraAltaEnd);
            sb.Append(tags.tagBarreiraBaixa);
            sb.Append(tags.tagLevelPercentage); // PREENCHER COM VALOR A CONFIRMAR (NÃO DEVERIA EXISTIR ESTA TAG)
            sb.Append(tags.tagCodigoTipoCotacaoLiquidacao.Replace(tags.VALOR_VARIAVEL_1, fpml.REGISTRO_CAMARA_TIPO_COTACAO_LIQUIDACAO_BARREIRA_BAIXA));
            sb.Append(tags.tagBarreiraBaixaEnd);
            sb.Append(tags.tagBarreiraEnd);
            sb.Append(tags.tagAtributosRegistroCamaraEnd);
            sb.Append(tags.tagItauBondTransactionEnd);
            sb.Append(tags.tagTradeEnd);

            sb.Append(tags.tagNumeroClienteParte.Replace(tags.VALOR_VARIAVEL_1, fpml.ID_CLIENTE_PARTE));
            sb.Append(tags.tagGrupoEconomico.Replace(tags.VALOR_VARIAVEL_1, fpml.GRUPO_ECONOMICO_PARTE));
            sb.Append(tags.tagPartyEnd);
            sb.Append(tags.tagNumeroClienteContraparte.Replace(tags.VALOR_VARIAVEL_1, fpml.ID_CLIENTE_CONTRAPARTE));
            sb.Append(tags.tagGrupoEconomico.Replace(tags.VALOR_VARIAVEL_1, fpml.GRUPO_ECONOMICO_CONTRAPARTE));
            sb.Append(tags.tagSegmentoCliente.Replace(tags.VALOR_VARIAVEL_1, fpml.CODIGO_SEGMENTO_CLIENTE));
            sb.Append(tags.tagPartyEnd);

            sb.Append(MontarDadosConta(fpml, "Parte"));
            sb.Append(MontarDadosConta(fpml, "Contraparte"));

            return sb.ToString();
        }

        public string MontaFpMLCOE_Liquidacao(FPMLObject fpml)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(tags.tagTermination);
            sb.Append(tags.tagPartyReference.Replace(tags.VALOR_VARIAVEL_1, "Parte"));
            sb.Append(tags.tagVersionedTradeId.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_CONTRATO));
            sb.Append(tags.tagVersion.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_VERSAO));
            sb.Append(tags.tagOriginatingTradeId);
            sb.Append(tags.tagPartyReference.Replace(tags.VALOR_VARIAVEL_1, "Parte"));
            sb.Append(tags.tagVersionedTradeId.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_CONTRATO));
            sb.Append(tags.tagVersion.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_VERSAO));
            sb.Append(tags.tagOriginatingTradeIdEnd);
            sb.Append(tags.tagTradeIdentifierEnd);
            sb.Append(tags.tagAgreementDate.Replace(tags.VALOR_VARIAVEL_1, FormatarData(DateTime.Now.ToString())));
            sb.Append(tags.tagDataEfetivaVencimento.Replace(tags.VALOR_VARIAVEL_1, FormatarData(fpml.DATA_EFETIVA_VENCIMENTO)));
            sb.Append(tags.tagPayment);
            sb.Append(tags.tagComprador.Replace(tags.VALOR_VARIAVEL_1, fpml.COMPRADOR));
            sb.Append(tags.tagVendedor.Replace(tags.VALOR_VARIAVEL_1, fpml.COMPRADOR.Equals("Parte") ? "Contraparte" : "Parte"));
            sb.Append(tags.tagMoedaLiquidacao.Replace(tags.VALOR_VARIAVEL_1, fpml.CODIGO_MOEDA));
            sb.Append(tags.tagValor.Replace(tags.VALOR_VARIAVEL_1, FormatarValor(fpml.VALOR_FINANCEIRO_LIQUIDO)));
            sb.Append(tags.tagPaymentEnd);

            sb.Append(tags.tagSizeChange);
            sb.Append(tags.tagItauTradeInformation);
            sb.Append(tags.tagPartyReference);
            sb.Append(tags.tagUnit);
            sb.Append(tags.tagTrader);
            sb.Append(tags.tagTimestamps);
            sb.Append(tags.tagTimestampSystem.Replace(tags.VALOR_VARIAVEL_1, FormatarTimestamp()));
            sb.Append(tags.tagTimestampsEnd);
            sb.Append(tags.tagItauTradeInformationEnd);
            sb.Append(tags.tagQuantidadeAtivosNegociado.Replace(tags.VALOR_VARIAVEL_1, fpml.QUANTIDADE_ATIVOS_LIQUIDADOS));

            if (fpml.TIPO_IMPOSTO != null && fpml.TIPO_IMPOSTO.Count > 0)
            {
                for (int i = 0; i < fpml.TIPO_IMPOSTO.Count; i++)
                {
                    sb.Append(tags.tagTaxAmount.Replace(tags.VALOR_VARIAVEL_1, fpml.TIPO_IMPOSTO[i].TipoImposto).Replace(tags.VALOR_VARIAVEL_2, fpml.TIPO_IMPOSTO[i].ValorImposto));
                }
            }


            sb.Append(tags.tagTerminationEnd);
            sb.Append(tags.tagNumeroClienteParte.Replace(tags.VALOR_VARIAVEL_1, fpml.ID_CLIENTE_PARTE));
            sb.Append(tags.tagPartyEnd);
            sb.Append(tags.tagNumeroClienteContraparte.Replace(tags.VALOR_VARIAVEL_1, fpml.ID_CLIENTE_CONTRAPARTE));
            sb.Append(tags.tagPartyEnd);

            sb.Append(MontarDadosConta(fpml,"Parte"));
            sb.Append(MontarDadosConta(fpml, "Contraparte"));

            return sb.ToString();
        }

        public string MontaFpMLCOE_Cancelamento(FPMLObject fpml)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(tags.tagDeclear);
            sb.Append(tags.tagPartyReference.Replace(tags.VALOR_VARIAVEL_1, "Parte"));
            sb.Append(tags.tagVersionedTradeId.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_CONTRATO));
            sb.Append(tags.tagVersion.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_VERSAO));
            sb.Append(tags.tagOriginatingTradeId);
            sb.Append(tags.tagPartyReference.Replace(tags.VALOR_VARIAVEL_1, "Parte"));
            sb.Append(tags.tagVersionedTradeId.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_CONTRATO));
            sb.Append(tags.tagVersion.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_VERSAO));
            sb.Append(tags.tagOriginatingTradeIdEnd);
            sb.Append(tags.tagEKMRelacionada.Replace(tags.VALOR_VARIAVEL_1, fpml.CHAVE_EKM_CANCELAMENTO));
            sb.Append(tags.tagTradeIdentifierEnd);
            sb.Append(tags.tagDataEfetivaVencimento.Replace(tags.VALOR_VARIAVEL_1, string.Concat(DateTime.Today.Year, "-", DateTime.Today.Month, "-", DateTime.Today.Day)));
            sb.Append(tags.tagReason.Replace(tags.VALOR_VARIAVEL_1, fpml.MOTIVO_CANCELAMENTO));
            sb.Append(tags.tagItauTradeInformation);
            sb.Append(tags.tagPartyReference.Replace(tags.VALOR_VARIAVEL_1, "Parte"));
            sb.Append(tags.tagTrader.Replace(tags.VALOR_VARIAVEL_1, fpml.FUNCIONAL_OPERADOR));
            sb.Append(tags.tagTimestamps);
            sb.Append(tags.tagTimestampCreation.Replace(tags.VALOR_VARIAVEL_1, FormatarTimestamp()));
            sb.Append(tags.tagTimestampSystem.Replace(tags.VALOR_VARIAVEL_1, FormatarTimestamp()));
            sb.Append(tags.tagTimestampsEnd);
            sb.Append(tags.tagComments);
            sb.Append(tags.tagItauTradeInformationEnd);
            sb.Append(tags.tagDeclearEnd);
            sb.Append(tags.tagNumeroClienteParte.Replace(tags.VALOR_VARIAVEL_1, fpml.ID_CLIENTE_PARTE));
            sb.Append(tags.tagGrupoEconomico.Replace(tags.VALOR_VARIAVEL_1, fpml.GRUPO_ECONOMICO_PARTE));
            sb.Append(tags.tagPartyEnd);

            return sb.ToString();
        }

        #endregion

        #region MontaFpML Swap

        public string MontaFpMLSwap(FPMLObject fpml)
        {
            StringBuilder sb = new StringBuilder();
            string CODIGO_N3_SWAP = string.Empty;


            if (fpml.TIPO_SWAP.Equals("0"))
                CODIGO_N3_SWAP = "2000493";
            else if (fpml.TIPO_SWAP.Equals("1"))
                CODIGO_N3_SWAP = "2000489";
            else if (fpml.TIPO_SWAP.Equals("2"))
                CODIGO_N3_SWAP = "2000483";
            else if (fpml.TIPO_SWAP.Equals("3"))
                CODIGO_N3_SWAP = "2000494";
            else if (fpml.TIPO_SWAP.Equals("4"))
                CODIGO_N3_SWAP = "2000492";
            else if (fpml.TIPO_SWAP.Equals("5"))
                CODIGO_N3_SWAP = "2000488";
            else if (fpml.TIPO_SWAP.Equals("6"))
                CODIGO_N3_SWAP = "2000487";
            else
                return "Tipo de swap não preenchido.";

            sb.Append(MontaHeader(fpml).Replace(tags.N1N2N3, "17:161:" + CODIGO_N3_SWAP).Replace("N4", "004"));
            sb.Append(tags.tagIsCorrection.Replace(tags.VALOR_VARIAVEL_1, fpml.ALTERACAO.ToString().ToLower()));

            if (fpml.EVENTO.Equals("0"))
            {
                sb.Append(MontaFpMLSwap_Contratacao(fpml, "0"));
            }
            else if (fpml.EVENTO.Equals("1"))
            {
                sb.Append(MontaFpMLSwap_Liquidacao(fpml));
            }
            else if (fpml.EVENTO.Equals("2"))
            {
                sb.Append(MontaFpMLSwap_Cancelamento(fpml));
            }
            else
            {
                return "Evento não informado.";
            }

            sb.Append(tags.tagRequestConfirmationEnd);

            return sb.ToString();
        }

        public string MontaFpMLSwap_Contratacao(FPMLObject fpml, string codProduto)
        {
            StringBuilder sb = new StringBuilder();
            string x = string.Empty;

            sb.Append(tags.tagTrade.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_CONTRATO));
            sb.Append(tags.tagVersion.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_VERSAO));
            sb.Append(tags.tagChaveProcessoFuncional.Replace(tags.VALOR_VARIAVEL_1, fpml.CHAVE_PROCESSO_FUNCIONAL));
            sb.Append(tags.tagChaveNatural.Replace(tags.VALOR_VARIAVEL_1, fpml.CHAVE_NATURAL));
            //<linkId linkIdScheme="" id="StructuredTradeId">XXXX</linkId>
            sb.Append(tags.tagItauTradeId.Replace(tags.VALOR_VARIAVEL_1, fpml.CHAVE_EKM_OPERACAO_VINCULO));
            sb.Append(tags.tagPartyTradeInformation.Replace(tags.VALOR_VARIAVEL_1, fpml.FUNCIONAL_OPERADOR));
            sb.Append(tags.tagTimestamps);
            sb.Append(tags.tagTimestampSystem.Replace(tags.VALOR_VARIAVEL_1, FormatarTimestamp()));
            sb.Append(tags.tagTimestampsEnd);
            //<executionVenueType>DMA</executionVenueType>
            sb.Append(tags.tagFigura.Replace(tags.VALOR_VARIAVEL_1, fpml.CODIGO_FIGURA));
            sb.Append(tags.tagPortfolio.Replace(tags.VALOR_VARIAVEL_1, fpml.ID_PORTFOLIO));
            sb.Append(tags.tagTradeDate.Replace(tags.VALOR_VARIAVEL_1, fpml.DATA_NEGOCIACAO));

            sb.Append(tags.tagItauSwap);

            sb.Append(tags.tagItauSwapLeg.Replace(tags.VALOR_VARIAVEL_1, "1")); //Confirmar composição da tag (verificar se deve ter xsi:type="itau:InterestRateStream")
            sb.Append(tags.tagPayer.Replace(tags.VALOR_VARIAVEL_1, "Parte"));
            sb.Append(tags.tagReceiver.Replace(tags.VALOR_VARIAVEL_1, "Contraparte"));
            sb.Append(tags.tagDataEfetivaLegSwap.Replace(tags.VALOR_VARIAVEL_1, "1"));
            sb.Append(tags.tagUnadjustedDate.Replace(tags.VALOR_VARIAVEL_1, FormatarData(fpml.DATA_INICIO_VALORIZACAO)));
            sb.Append(tags.tagDataEfetivaLegSwapEnd);
            sb.Append(tags.tagDataLiquidacao);
            sb.Append(tags.tagUnadjustedDate.Replace(tags.VALOR_VARIAVEL_1, FormatarData(fpml.DATA_PREVISTA_VENCIMENTO)));
            sb.Append(tags.tagDataLiquidacaoEnd);
            sb.Append(tags.tagCalculationPeriodAdjustementEFrequency);
            sb.Append(tags.tagCalculationPeriodDatesEnd);
            sb.Append(tags.tagCalculationPeriodDatesReference.Replace(tags.VALOR_VARIAVEL_1, "1"));
            sb.Append(tags.tagsConstantesENotionalStepSchedule.Replace(tags.VALOR_VARIAVEL_1, FormatarValor(fpml.VALOR_PRECO_UNITARIO_OPERACAO)));
            sb.Append(tags.tagMoedaSwap.Replace(tags.VALOR_VARIAVEL_1, fpml.CODIGO_MOEDA));

            //Dados Indexador - Parte
            sb.Append(tags.tagFloatingRateCalculationSwap.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_PARTE_CODIGO_SERIE));
            sb.Append(tags.tagSpreadSchedule.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_PARTE_PERCENTUAL_INDEXADOR));

            sb.Append(tags.tagCapRateSchedule.Replace(tags.VALOR_VARIAVEL_1, fpml.FATOR_LIMITADOR_SUPERIOR));
            sb.Append(tags.tagFloorRateSchedule.Replace(tags.VALOR_VARIAVEL_1, fpml.FATOR_LIMITADOR_INFERIOR));

            sb.Append(tags.tagTaxaPre.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_PARTE_TAXA_PRE));
            sb.Append(tags.tagFloatingRateIndex.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_PARTE_QUANTIDADE_FIXING_COTACAO_LIQUIDACAO));
            sb.Append(tags.tagReferenciaCotacao.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_PARTE_REFERENCIA_COTACAO));
            sb.Append(tags.tagCodigoAtributo.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_PARTE_CODIGO_ATRIBUTO));
            sb.Append(tags.tagCodigoTipoAtributo.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_PARTE_CODIGO_TIPO_ATRIBUTO));
            sb.Append(tags.tagCodigoFamilia.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_PARTE_CODIGO_FAMILIA));
            sb.Append(tags.tagCodigoTipoCotacaoLiquidacao.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_PARTE_TIPO_COTACAO_LIQUIDACAO));
            sb.Append(tags.tagValorCotacaoInicial.Replace(tags.VALOR_VARIAVEL_1, FormatarValor(fpml.INDEXADOR_PARTE_VALOR_COTACAO_INICIAL)));
            sb.Append(tags.tagDataFixingContratacao.Replace(tags.VALOR_VARIAVEL_1, FormatarData(fpml.INDEXADOR_PARTE_DATA_FIXING_CONTRATACAO)));
            sb.Append(tags.tagQuantidadeFixingContratacao.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_PARTE_QUANTIDADE_FIXING_CONTRATACAO));
            sb.Append(tags.tagDataFixingVencimento.Replace(tags.VALOR_VARIAVEL_1, FormatarData(fpml.INDEXADOR_PARTE_DATA_FIXING_VENCIMENTO)));
            sb.Append(tags.tagQuantidadeFixingVencimento.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_PARTE_QUANTIDADE_FIXING_VENCIMENTO));
            sb.Append(tags.tagFixingIndexEnd);
            sb.Append(tags.tagFloatingRateCalculationEnd);
            sb.Append(tags.tagDayCountFraction.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_PARTE_BASE_TAXA));
            sb.Append(tags.tagCalculationEnd);
            sb.Append(tags.tagCashflows);
            sb.Append(tags.tagItauSwapLegEnd);

            sb.Append(tags.tagItauSwapLeg.Replace(tags.VALOR_VARIAVEL_1, "2"));
            sb.Append(tags.tagPayer.Replace(tags.VALOR_VARIAVEL_1, "Contraparte"));
            sb.Append(tags.tagReceiver.Replace(tags.VALOR_VARIAVEL_1, "Parte"));
            sb.Append(tags.tagDataEfetivaLegSwap.Replace(tags.VALOR_VARIAVEL_1, "2"));
            sb.Append(tags.tagUnadjustedDate.Replace(tags.VALOR_VARIAVEL_1, FormatarData(fpml.DATA_INICIO_VALORIZACAO)));
            sb.Append(tags.tagDataEfetivaLegSwapEnd);
            sb.Append(tags.tagDataLiquidacao);
            sb.Append(tags.tagUnadjustedDate.Replace(tags.VALOR_VARIAVEL_1, FormatarData(fpml.DATA_PREVISTA_VENCIMENTO)));
            sb.Append(tags.tagDataLiquidacaoEnd);
            sb.Append(tags.tagCalculationPeriodAdjustementEFrequency);
            sb.Append(tags.tagCalculationPeriodDatesEnd);
            sb.Append(tags.tagCalculationPeriodDatesReference.Replace(tags.VALOR_VARIAVEL_1, "2"));
            sb.Append(tags.tagsConstantesENotionalStepSchedule.Replace(tags.VALOR_VARIAVEL_1, fpml.VALOR_PRECO_UNITARIO_OPERACAO));
            sb.Append(tags.tagMoedaSwap.Replace(tags.VALOR_VARIAVEL_1, fpml.CODIGO_MOEDA));

            //Dados Indexador - Contraparte
            sb.Append(tags.tagFloatingRateCalculationSwap.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_CONTRAPARTE_CODIGO_SERIE));
            sb.Append(tags.tagSpreadSchedule.Replace(tags.VALOR_VARIAVEL_1, contraparteValorizacao.Percentual));

            sb.Append(tags.tagCapRateSchedule.Replace(tags.VALOR_VARIAVEL_1, fpml.FATOR_LIMITADOR_SUPERIOR));
            sb.Append(tags.tagFloorRateSchedule.Replace(tags.VALOR_VARIAVEL_1, fpml.FATOR_LIMITADOR_INFERIOR));
            sb.Append(tags.tagTaxaPre.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_CONTRAPARTE_TAXA_PRE));
            sb.Append(tags.tagFloatingRateIndex.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_CONTRAPARTE_QUANTIDADE_FIXING_COTACAO_LIQUIDACAO));
            sb.Append(tags.tagReferenciaCotacao.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_CONTRAPARTE_REFERENCIA_COTACAO));
            sb.Append(tags.tagCodigoAtributo.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_CONTRAPARTE_CODIGO_ATRIBUTO));
            sb.Append(tags.tagCodigoTipoAtributo.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_CONTRAPARTE_CODIGO_TIPO_ATRIBUTO));
            sb.Append(tags.tagCodigoFamilia.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_CONTRAPARTE_CODIGO_FAMILIA));
            sb.Append(tags.tagCodigoTipoCotacaoLiquidacao.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_CONTRAPARTE_TIPO_COTACAO_LIQUIDACAO));
            sb.Append(tags.tagValorCotacaoInicial.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_CONTRAPARTE_VALOR_COTACAO_INICIAL));
            sb.Append(tags.tagDataFixingContratacao.Replace(tags.VALOR_VARIAVEL_1, FormatarData(fpml.INDEXADOR_CONTRAPARTE_DATA_FIXING_CONTRATACAO)));
            sb.Append(tags.tagQuantidadeFixingContratacao.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_CONTRAPARTE_QUANTIDADE_FIXING_CONTRATACAO));
            sb.Append(tags.tagDataFixingVencimento.Replace(tags.VALOR_VARIAVEL_1, FormatarData(fpml.INDEXADOR_CONTRAPARTE_DATA_FIXING_VENCIMENTO)));
            sb.Append(tags.tagQuantidadeFixingVencimento.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_CONTRAPARTE_QUANTIDADE_FIXING_VENCIMENTO));
            sb.Append(tags.tagFixingIndexEnd);
            sb.Append(tags.tagFloatingRateCalculationEnd);
            sb.Append(tags.tagDayCountFraction.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_CONTRAPARTE_BASE_TAXA));
            sb.Append(tags.tagCalculationEnd);
            sb.Append(tags.tagCashflows); //Fluxos - Não utilizada para Swap de COE

            if (fpml.PRODUTO_BARREIRA) // Swap com barreira
            {
                sb.Append(MontaFPML_Barreira(fpml,"Parte"));
                sb.Append(MontaFPML_Barreira(fpml, "Contraparte"));
            }

            sb.Append(tags.tagItauSwapLegEnd);
            sb.Append(tags.tagPremio); //Prêmio - Não utilizada para Swap de COE

            if (!string.IsNullOrEmpty(fpml.INDEXADOR_TERMO_CODIGO_ATRIBUTO_PERIODO) ||
                !string.IsNullOrEmpty(fpml.INDEXADOR_TERMO_CODIGO_FAMILIA_PERIODO) ||
                !string.IsNullOrEmpty(fpml.INDEXADOR_TERMO_CODIGO_SERIE_PERIODO) ||
                !string.IsNullOrEmpty(fpml.INDEXADOR_TERMO_CODIGO_TIPO_ATRIBUTO_PERIODO) ||
                !string.IsNullOrEmpty(fpml.INDEXADOR_TERMO_DATA_FIXING) ||
                !string.IsNullOrEmpty(fpml.INDEXADOR_TERMO_QUANTIDADE_FIXING))
            {
                //Tags Termo
                sb.Append(tags.tagSwapInitialPrice);
                sb.Append(tags.tagSwapATermo);
                sb.Append(tags.tagFloatingRateParte.Replace(tags.VALOR_VARIAVEL_1, parteTermo.Serie));
                sb.Append(tags.tagPercentualIndexador.Replace(tags.VALOR_VARIAVEL_1, parteTermo.Percentual));
                sb.Append(tags.tagTaxaPre.Replace(tags.VALOR_VARIAVEL_1, parteTermo.TaxaPre));
                sb.Append(tags.tagDataFixing.Replace(tags.VALOR_VARIAVEL_1, FormatarData(parteTermo.DataFixingTermo)));
                sb.Append(tags.tagFloatingRateIndex.Replace(tags.VALOR_VARIAVEL_1, parteTermo.QuantidadeFixingTermo));
                sb.Append(tags.tagReferenciaCotacao.Replace(tags.VALOR_VARIAVEL_1, parteTermo.ReferenciaCotacao));
                sb.Append(tags.tagCodigoAtributo.Replace(tags.VALOR_VARIAVEL_1, parteTermo.Atributo));
                sb.Append(tags.tagCodigoTipoAtributo.Replace(tags.VALOR_VARIAVEL_1, parteTermo.TipoAtributo));
                sb.Append(tags.tagCodigoFamilia.Replace(tags.VALOR_VARIAVEL_1, parteTermo.Familia));
                sb.Append(tags.tagFloatingRateCalculationEnd);
                sb.Append(tags.tagBaseTaxa.Replace(tags.VALOR_VARIAVEL_1, parteTermo.BaseTaxa));
                sb.Append(tags.tagSwapATermoEnd);
            }

            sb.Append(tags.tagItauSwapEnd);
            sb.Append(tags.tagTradeEnd);

            sb.Append(tags.tagNumeroClienteParte.Replace(tags.VALOR_VARIAVEL_1, fpml.ID_CLIENTE_PARTE));
            sb.Append(tags.tagGrupoEconomico.Replace(tags.VALOR_VARIAVEL_1, fpml.GRUPO_ECONOMICO_PARTE));
            sb.Append(tags.tagPartyEnd);
            sb.Append(tags.tagNumeroClienteContraparte.Replace(tags.VALOR_VARIAVEL_1, fpml.ID_CLIENTE_CONTRAPARTE));
            sb.Append(tags.tagGrupoEconomico.Replace(tags.VALOR_VARIAVEL_1, fpml.GRUPO_ECONOMICO_CONTRAPARTE));
            sb.Append(tags.tagSegmentoCliente.Replace(tags.VALOR_VARIAVEL_1, fpml.CODIGO_SEGMENTO_CLIENTE));
            sb.Append(tags.tagPartyEnd);

            sb.Append(MontarDadosConta(fpml, "Parte"));
            sb.Append(MontarDadosConta(fpml, "Contraparte"));

            return sb.ToString();
        }

        public string MontaFpMLSwap_Liquidacao(FPMLObject fpml)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(tags.tagTermination);
            sb.Append(tags.tagPartyReference.Replace(tags.VALOR_VARIAVEL_1, "Parte"));
            sb.Append(tags.tagVersionedTradeId.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_CONTRATO));
            sb.Append(tags.tagVersion.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_VERSAO));
            sb.Append(tags.tagOriginatingTradeId);
            sb.Append(tags.tagPartyReference.Replace(tags.VALOR_VARIAVEL_1, "Parte"));
            sb.Append(tags.tagVersionedTradeId.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_CONTRATO));
            sb.Append(tags.tagVersion.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_VERSAO));
            sb.Append(tags.tagOriginatingTradeIdEnd);
            sb.Append(tags.tagTradeIdentifierEnd);
            sb.Append(tags.tagAgreementDate.Replace(tags.VALOR_VARIAVEL_1, string.Concat(DateTime.Today.Year, "-", DateTime.Today.Month, "-", DateTime.Today.Day)));
            sb.Append(tags.tagDataEfetivaVencimento.Replace(tags.VALOR_VARIAVEL_1, FormatarData(fpml.DATA_EFETIVA_VENCIMENTO)));
            sb.Append(tags.tagPayment);
            sb.Append(tags.tagComprador.Replace(tags.VALOR_VARIAVEL_1, fpml.COMPRADOR));
            sb.Append(tags.tagVendedor.Replace(tags.VALOR_VARIAVEL_1, fpml.COMPRADOR.Equals("Parte") ? "Contraparte" : "Parte"));
            sb.Append(tags.tagMoedaLiquidacao.Replace(tags.VALOR_VARIAVEL_1, fpml.CODIGO_MOEDA));
            sb.Append(tags.tagValor.Replace(tags.VALOR_VARIAVEL_1, fpml.VALOR_FINANCEIRO_LIQUIDO));
            sb.Append(tags.tagPaymentEnd);

            sb.Append(tags.tagSizeChange);
            sb.Append(tags.tagItauTradeInformation);
            sb.Append(tags.tagPartyReference);
            sb.Append(tags.tagUnit);
            sb.Append(tags.tagTrader);
            sb.Append(tags.tagTimestamps);
            sb.Append(tags.tagTimestampSystem.Replace(tags.VALOR_VARIAVEL_1, string.Concat(DateTime.Today.Year, "-", DateTime.Today.Month, "-", DateTime.Today.Day)));
            sb.Append(tags.tagTimestampsEnd);
            sb.Append(tags.tagItauTradeInformationEnd);
            sb.Append(tags.tagQuantidadeAtivosNegociado.Replace(tags.VALOR_VARIAVEL_1, fpml.QUANTIDADE_ATIVOS_LIQUIDADOS));

            if (fpml.TIPO_IMPOSTO != null && fpml.TIPO_IMPOSTO.Count > 0)
            {
                for (int i = 0; i < fpml.TIPO_IMPOSTO.Count; i++)
                {
                    sb.Append(tags.tagTaxAmount.Replace(tags.VALOR_VARIAVEL_1, fpml.TIPO_IMPOSTO[i].TipoImposto).Replace(tags.VALOR_VARIAVEL_2, fpml.TIPO_IMPOSTO[i].ValorImposto));
                }
            }
            sb.Append(tags.tagTerminationEnd);
            sb.Append(tags.tagNumeroClienteParte.Replace(tags.VALOR_VARIAVEL_1, fpml.ID_CLIENTE_PARTE));
            sb.Append(tags.tagPartyEnd);
            sb.Append(tags.tagNumeroClienteContraparte.Replace(tags.VALOR_VARIAVEL_1, fpml.ID_CLIENTE_CONTRAPARTE));
            sb.Append(tags.tagPartyEnd);

            sb.Append(MontarDadosConta(fpml, "Parte"));
            sb.Append(MontarDadosConta(fpml, "Contraparte"));

            return sb.ToString();
        }

        public string MontaFpMLSwap_Cancelamento(FPMLObject fpml)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(tags.tagDeclear);
            sb.Append(tags.tagPartyReference.Replace(tags.VALOR_VARIAVEL_1, "Parte"));
            sb.Append(tags.tagVersionedTradeId.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_CONTRATO));
            sb.Append(tags.tagVersion.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_VERSAO));
            sb.Append(tags.tagOriginatingTradeId);
            sb.Append(tags.tagPartyReference.Replace(tags.VALOR_VARIAVEL_1, "Parte"));
            sb.Append(tags.tagVersionedTradeId.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_CONTRATO));
            sb.Append(tags.tagVersion.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_VERSAO));
            sb.Append(tags.tagOriginatingTradeIdEnd);
            sb.Append(tags.tagTradeIdentifierEnd);
            sb.Append(tags.tagDataEfetivaVencimento.Replace(tags.VALOR_VARIAVEL_1, FormatarData(fpml.DATA_EFETIVA_VENCIMENTO)));
            sb.Append(tags.tagItauTradeInformation);
            sb.Append(tags.tagPartyReference.Replace(tags.VALOR_VARIAVEL_1, "Parte"));
            sb.Append(tags.tagTrader.Replace(tags.VALOR_VARIAVEL_1, fpml.FUNCIONAL_OPERADOR));
            sb.Append(tags.tagTimestamps);
            sb.Append(tags.tagTimestampCreation.Replace(tags.VALOR_VARIAVEL_1, FormatarTimestamp()));
            sb.Append(tags.tagTimestampSystem.Replace(tags.VALOR_VARIAVEL_1, FormatarTimestamp()));
            sb.Append(tags.tagTimestampsEnd);
            sb.Append(tags.tagComments);
            sb.Append(tags.tagItauTradeInformationEnd);
            sb.Append(tags.tagDeclearEnd);
            sb.Append(tags.tagNumeroClienteParte.Replace(tags.VALOR_VARIAVEL_1, fpml.ID_CLIENTE_PARTE));
            sb.Append(tags.tagGrupoEconomico.Replace(tags.VALOR_VARIAVEL_1, fpml.GRUPO_ECONOMICO_PARTE));
            sb.Append(tags.tagPartyEnd);

            return sb.ToString();
        }

        private string MontaFPML_Barreira(FPMLObject fpml, string parteOuContraparte)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(tags.tagBarreira);

            if (parteOuContraparte.Equals("Parte"))
            {
                if (!string.IsNullOrEmpty(fpml.BARREIRA_PARTE_TIPO))
                {
                    if (fpml.BARREIRA_PARTE_TIPO.Equals("Acima"))
                    {
                        sb.Append(tags.tagBarreiraAlta);
                        sb.Append(PreencherDadosBarreira(fpml, parteOuContraparte));
                        sb.Append(tags.tagBarreiraAltaEnd);
                    }
                    else
                    {
                        sb.Append(tags.tagBarreiraBaixa);
                        sb.Append(PreencherDadosBarreira(fpml, parteOuContraparte));
                        sb.Append(tags.tagBarreiraBaixaEnd);
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(fpml.BARREIRA_CONTRAPARTE_TIPO))
                {
                    if (fpml.BARREIRA_CONTRAPARTE_TIPO.Equals("Acima"))
                    {
                        sb.Append(tags.tagBarreiraAlta);
                        sb.Append(PreencherDadosBarreira(fpml, parteOuContraparte));
                        sb.Append(tags.tagBarreiraAltaEnd);
                    }
                    else
                    {
                        sb.Append(tags.tagBarreiraBaixa);
                        sb.Append(PreencherDadosBarreira(fpml, parteOuContraparte));
                        sb.Append(tags.tagBarreiraBaixaEnd);
                    }
                }
                else
                {
                    return string.Empty;
                }
            }

            sb.Append(tags.tagBarreiraEnd);

            return sb.ToString();
        }

        private string PreencherDadosBarreira(FPMLObject fpml, string parteOuContraparte)
        {
            StringBuilder sb = new StringBuilder();

            if(parteOuContraparte.Equals("Parte"))
            {
                sb.Append(tags.tagMomentoVerificacaoBarreira);
                sb.Append(tags.tagData.Replace(tags.VALOR_VARIAVEL_1, FormatarData(fpml.BARREIRA_PARTE_DATA_VERIFICACAO)));
                sb.Append(tags.tagMomentoVerificacaoBarreiraEnd);
                sb.Append(tags.tagValorBarreira.Replace(tags.VALOR_VARIAVEL_1, fpml.BARREIRA_PARTE_VALOR));
                sb.Append(tags.tagRebate);
                sb.Append(tags.tagPayer.Replace(tags.VALOR_VARIAVEL_1, "Parte"));
                sb.Append(tags.tagReceiver.Replace(tags.VALOR_VARIAVEL_1, "Contraparte"));
                sb.Append(tags.tagValor);
                sb.Append(tags.tagMoedaRebate.Replace(tags.VALOR_VARIAVEL_1, fpml.BARREIRA_PARTE_MOEDA_REBATE));
                sb.Append(tags.tagRebateData);
                sb.Append(tags.tagRebateDataFixing.Replace(tags.VALOR_VARIAVEL_1, FormatarData(fpml.BARREIRA_PARTE_DATA_FIXING_REBATE)));
                sb.Append(tags.tagRebateDataEnd);
                sb.Append(tags.tagTipoDataPagamentoRebate.Replace(tags.VALOR_VARIAVEL_1, fpml.BARREIRA_PARTE_TIPO_DATA_PAGAMENTO_REBATE)); // No vencimento ou no acionamento da barreira
                sb.Append(tags.tagFloatingRateIndex.Replace(tags.VALOR_VARIAVEL_1, fpml.BARREIRA_PARTE_QUANTIDADE_DIAS_FIXING_REBATE));
                sb.Append(tags.tagRebateEnd);
                sb.Append(tags.tagTipoVerificacaoBarreira.Replace(tags.VALOR_VARIAVEL_1, fpml.BARREIRA_PARTE_TIPO_VERIFICACAO));
                sb.Append(tags.tagTipoDirecaoBarreira.Replace(tags.VALOR_VARIAVEL_1, fpml.BARREIRA_PARTE_TIPO));
            }
            else
            {
                sb.Append(tags.tagMomentoVerificacaoBarreira);
                sb.Append(tags.tagData.Replace(tags.VALOR_VARIAVEL_1, FormatarData(fpml.BARREIRA_CONTRAPARTE_DATA_VERIFICACAO)));
                sb.Append(tags.tagMomentoVerificacaoBarreiraEnd);
                sb.Append(tags.tagValorBarreira.Replace(tags.VALOR_VARIAVEL_1, fpml.BARREIRA_CONTRAPARTE_VALOR));
                sb.Append(tags.tagRebate);
                sb.Append(tags.tagPayer.Replace(tags.VALOR_VARIAVEL_1, "Contraparte"));
                sb.Append(tags.tagReceiver.Replace(tags.VALOR_VARIAVEL_1, "Parte"));
                sb.Append(tags.tagValor);
                sb.Append(tags.tagMoedaRebate.Replace(tags.VALOR_VARIAVEL_1, fpml.BARREIRA_CONTRAPARTE_MOEDA_REBATE));
                sb.Append(tags.tagRebateData);
                sb.Append(tags.tagRebateDataFixing.Replace(tags.VALOR_VARIAVEL_1, FormatarData(fpml.BARREIRA_CONTRAPARTE_DATA_FIXING_REBATE)));
                sb.Append(tags.tagRebateDataEnd);
                sb.Append(tags.tagTipoDataPagamentoRebate.Replace(tags.VALOR_VARIAVEL_1, fpml.BARREIRA_CONTRAPARTE_TIPO_DATA_PAGAMENTO_REBATE)); // No vencimento ou no acionamento da barreira
                sb.Append(tags.tagFloatingRateIndex.Replace(tags.VALOR_VARIAVEL_1, fpml.BARREIRA_CONTRAPARTE_QUANTIDADE_DIAS_FIXING_REBATE));
                sb.Append(tags.tagRebateEnd);
                sb.Append(tags.tagTipoVerificacaoBarreira.Replace(tags.VALOR_VARIAVEL_1, fpml.BARREIRA_CONTRAPARTE_TIPO_VERIFICACAO));
                sb.Append(tags.tagTipoDirecaoBarreira.Replace(tags.VALOR_VARIAVEL_1, fpml.BARREIRA_CONTRAPARTE_TIPO));
            }

            return sb.ToString();
        }

        #endregion

        #region MontarFpML Renda Fixa COE

        public string MontaFpMLRendaFixaCOE(FPMLObject fpml)
        {
            StringBuilder sb = new StringBuilder();
            string CODIGO_N3_SWAP = string.Empty;

            sb.Append(MontaHeader(fpml).Replace(tags.N1N2N3, "23:145:2000491"));
            sb.Append(tags.tagIsCorrection.Replace(tags.VALOR_VARIAVEL_1, fpml.ALTERACAO.ToString().ToLower()));

            if (fpml.EVENTO.Equals("0"))
            {
                sb.Append(MontaFpMLRendaFixaCOE_Contratacao(fpml));
            }
            else if (fpml.EVENTO.Equals("1"))
            {
                sb.Append(MontaFpMLRendaFixaCOE_Liquidacao(fpml));
            }
            else if (fpml.EVENTO.Equals("2"))
            {
                sb.Append(MontaFpMLRendaFixaCOE_Cancelamento(fpml));
            }
            else
            {
                return "Evento não informado.";
            }

            sb.Append(tags.tagRequestConfirmationEnd);

            return sb.ToString();
        }

        public string MontaFpMLRendaFixaCOE_Contratacao(FPMLObject fpml)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(tags.tagTrade.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_CONTRATO));
            sb.Append(tags.tagVersion.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_VERSAO));
            sb.Append(tags.tagChaveProcessoFuncional.Replace(tags.VALOR_VARIAVEL_1, fpml.CHAVE_PROCESSO_FUNCIONAL));
            sb.Append(tags.tagChaveNatural.Replace(tags.VALOR_VARIAVEL_1, fpml.CHAVE_NATURAL));
            sb.Append(tags.tagStructureId);
            sb.Append(tags.tagItauTradeId.Replace(tags.VALOR_VARIAVEL_1, fpml.CHAVE_EKM_OPERACAO_VINCULO));
            sb.Append(tags.tagPartyTradeInformation.Replace(tags.VALOR_VARIAVEL_1, fpml.FUNCIONAL_OPERADOR));
            sb.Append(tags.tagTimestamps);
            sb.Append(tags.tagTimestampSystem.Replace(tags.VALOR_VARIAVEL_1, FormatarTimestamp()));
            sb.Append(tags.tagTimestampsEnd);
            sb.Append(tags.tagExecutionVenueType);
            sb.Append(tags.tagFigura.Replace(tags.VALOR_VARIAVEL_1, fpml.CODIGO_FIGURA));
            sb.Append(tags.tagPortfolio.Replace(tags.VALOR_VARIAVEL_1, fpml.ID_PORTFOLIO));
            sb.Append(tags.tagTradeDate.Replace(tags.VALOR_VARIAVEL_1, fpml.DATA_NEGOCIACAO));
            sb.Append(tags.tagBondTransaction);
            sb.Append(tags.tagValorOperacao.Replace(tags.VALOR_VARIAVEL_1, fpml.VALOR_OPERACAO));
            sb.Append(tags.tagPrice);
            sb.Append(tags.tagBond.Replace(tags.VALOR_VARIAVEL_1, fpml.CAMARA_REGISTRO));
            sb.Append(tags.tagCoupon);
            sb.Append(tags.tagDataPrevistaVencimento.Replace(tags.VALOR_VARIAVEL_1, FormatarData(fpml.DATA_PREVISTA_VENCIMENTO)));
            sb.Append(tags.tagValorPrecoUnitario.Replace(tags.VALOR_VARIAVEL_1, FormatarValor(fpml.VALOR_PRECO_UNITARIO_OPERACAO)));
            sb.Append(tags.tagBaseTaxa.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_PARTE_BASE_TAXA));
            sb.Append(tags.tagFloatingRateParte.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_PARTE_CODIGO_SERIE));
            sb.Append(tags.tagPercentualIndexador.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_PARTE_PERCENTUAL_INDEXADOR));
            sb.Append(tags.tagTaxaPre.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_PARTE_TAXA_PRE));
            sb.Append(tags.tagReferenciaCotacao); //TODO: CRIAR CAMPO NO FORM
            sb.Append(tags.tagCodigoAtributo.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_PARTE_CODIGO_ATRIBUTO));
            sb.Append(tags.tagCodigoTipoAtributo.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_PARTE_CODIGO_TIPO_ATRIBUTO));
            sb.Append(tags.tagCodigoFamilia.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_PARTE_CODIGO_FAMILIA));
            sb.Append(tags.tagCodigoTipoCotacaoLiquidacao.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_PARTE_TIPO_COTACAO_LIQUIDACAO));
            sb.Append(tags.tagValorCotacaoInicial.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_PARTE_VALOR_COTACAO_INICIAL));
            sb.Append(tags.tagDataFixingContratacao.Replace(tags.VALOR_VARIAVEL_1, FormatarData(fpml.INDEXADOR_PARTE_DATA_FIXING_CONTRATACAO)));
            sb.Append(tags.tagQuantidadeFixingContratacao.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_PARTE_QUANTIDADE_FIXING_CONTRATACAO));
            sb.Append(tags.tagDataFixingVencimento.Replace(tags.VALOR_VARIAVEL_1, FormatarData(fpml.INDEXADOR_PARTE_DATA_FIXING_VENCIMENTO)));
            sb.Append(tags.tagQuantidadeFixingVencimento.Replace(tags.VALOR_VARIAVEL_1, fpml.INDEXADOR_PARTE_QUANTIDADE_FIXING_VENCIMENTO));
            sb.Append(tags.tagFixingIndexEnd);
            sb.Append(tags.tagFloatingRateCalculationEnd);
            sb.Append(tags.tagBondEnd);
            sb.Append(tags.tagDataInicioValorizacao.Replace(tags.VALOR_VARIAVEL_1, FormatarData(fpml.DATA_INICIO_VALORIZACAO)));
            sb.Append(tags.tagDataCarencia.Replace(tags.VALOR_VARIAVEL_1, FormatarData(fpml.DATA_CARENCIA)));
            sb.Append(tags.tagTipoCondicaoMercadoLiquidacao.Replace(tags.VALOR_VARIAVEL_1, fpml.CODIGO_TIPO_CONDICAO_MERCADO_LIQUIDACAO));
            sb.Append(tags.tagQuantidadeAtivosNegociado.Replace(tags.VALOR_VARIAVEL_1, fpml.QUANTIDADE_ATIVOS_NEGOCIADOS));
            sb.Append(tags.tagModalidadeProduto.Replace(tags.VALOR_VARIAVEL_1, fpml.MODALIDADE));
            //sb.Append(tagIsAvailableTermination);
            sb.Append(tags.tagPrecoReferencia);
            sb.Append(tags.tagItauBondTransactionEnd);
            sb.Append(tags.tagTradeEnd);

            sb.Append(tags.tagNumeroClienteParte.Replace(tags.VALOR_VARIAVEL_1, fpml.ID_CLIENTE_PARTE));
            sb.Append(tags.tagGrupoEconomico.Replace(tags.VALOR_VARIAVEL_1, fpml.GRUPO_ECONOMICO_PARTE));
            sb.Append(tags.tagPartyEnd);
            sb.Append(tags.tagNumeroClienteContraparte.Replace(tags.VALOR_VARIAVEL_1, fpml.ID_CLIENTE_CONTRAPARTE));
            sb.Append(tags.tagGrupoEconomico.Replace(tags.VALOR_VARIAVEL_1, fpml.GRUPO_ECONOMICO_CONTRAPARTE));
            sb.Append(tags.tagSegmentoCliente.Replace(tags.VALOR_VARIAVEL_1, fpml.CODIGO_SEGMENTO_CLIENTE));
            sb.Append(tags.tagPartyEnd);

            sb.Append(MontarDadosConta(fpml, "Parte"));
            sb.Append(MontarDadosConta(fpml, "Contraparte"));

            return sb.ToString();
        }

        public string MontaFpMLRendaFixaCOE_Liquidacao(FPMLObject fpml)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(tags.tagTermination);
            sb.Append(tags.tagPartyReference.Replace(tags.VALOR_VARIAVEL_1, "Parte"));
            sb.Append(tags.tagVersionedTradeId.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_CONTRATO));
            sb.Append(tags.tagVersion.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_VERSAO));
            sb.Append(tags.tagOriginatingTradeId);
            sb.Append(tags.tagPartyReference.Replace(tags.VALOR_VARIAVEL_1, "Parte"));
            sb.Append(tags.tagVersionedTradeId.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_CONTRATO));
            sb.Append(tags.tagVersion.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_VERSAO));
            sb.Append(tags.tagOriginatingTradeIdEnd);
            sb.Append(tags.tagTradeIdentifierEnd);
            sb.Append(tags.tagAgreementDate.Replace(tags.VALOR_VARIAVEL_1, string.Concat(DateTime.Today.Year, "-", DateTime.Today.Month, "-", DateTime.Today.Day)));
            sb.Append(tags.tagDataEfetivaVencimento.Replace(tags.VALOR_VARIAVEL_1, FormatarData(fpml.DATA_EFETIVA_VENCIMENTO)));
            sb.Append(tags.tagPayment);
            sb.Append(tags.tagComprador.Replace(tags.VALOR_VARIAVEL_1, fpml.COMPRADOR));
            sb.Append(tags.tagVendedor.Replace(tags.VALOR_VARIAVEL_1, fpml.COMPRADOR.Equals("Parte") ? "Contraparte" : "Parte"));
            sb.Append(tags.tagMoedaLiquidacao.Replace(tags.VALOR_VARIAVEL_1, fpml.CODIGO_MOEDA));
            sb.Append(tags.tagValor.Replace(tags.VALOR_VARIAVEL_1, FormatarValor(fpml.VALOR_FINANCEIRO_LIQUIDO)));
            sb.Append(tags.tagPaymentEnd);

            sb.Append(tags.tagSizeChange);
            sb.Append(tags.tagItauTradeInformation);
            sb.Append(tags.tagPartyReference);
            sb.Append(tags.tagUnit);
            sb.Append(tags.tagTrader);
            sb.Append(tags.tagTimestamps);
            sb.Append(tags.tagTimestampSystem.Replace(tags.VALOR_VARIAVEL_1, string.Concat(DateTime.Today.Year, "-", DateTime.Today.Month, "-", DateTime.Today.Day)));
            sb.Append(tags.tagTimestampsEnd);
            sb.Append(tags.tagItauTradeInformationEnd);
            sb.Append(tags.tagQuantidadeAtivosNegociado.Replace(tags.VALOR_VARIAVEL_1, fpml.QUANTIDADE_ATIVOS_LIQUIDADOS));

            if (fpml.TIPO_IMPOSTO != null && fpml.TIPO_IMPOSTO.Count > 0)
            {
                for(int i = 0; i < fpml.TIPO_IMPOSTO.Count; i++)
                {
                    sb.Append(tags.tagTaxAmount.Replace(tags.VALOR_VARIAVEL_1, fpml.TIPO_IMPOSTO[i].TipoImposto).Replace(tags.VALOR_VARIAVEL_2, fpml.TIPO_IMPOSTO[i].ValorImposto));
                }
            }

            sb.Append(tags.tagTerminationEnd);
            sb.Append(tags.tagNumeroClienteParte.Replace(tags.VALOR_VARIAVEL_1, fpml.ID_CLIENTE_PARTE));
            sb.Append(tags.tagPartyEnd);
            sb.Append(tags.tagNumeroClienteContraparte.Replace(tags.VALOR_VARIAVEL_1, fpml.ID_CLIENTE_CONTRAPARTE));
            sb.Append(tags.tagPartyEnd);

            sb.Append(MontarDadosConta(fpml, "Parte"));
            sb.Append(MontarDadosConta(fpml, "Contraparte"));

            return sb.ToString();
        }

        public string MontaFpMLRendaFixaCOE_Cancelamento(FPMLObject fpml)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(tags.tagDeclear);
            sb.Append(tags.tagPartyReference.Replace(tags.VALOR_VARIAVEL_1, "Parte"));
            sb.Append(tags.tagVersionedTradeId.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_CONTRATO));
            sb.Append(tags.tagVersion.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_VERSAO));
            sb.Append(tags.tagOriginatingTradeId);
            sb.Append(tags.tagPartyReference.Replace(tags.VALOR_VARIAVEL_1, "Parte"));
            sb.Append(tags.tagVersionedTradeId.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_CONTRATO));
            sb.Append(tags.tagVersion.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_VERSAO));
            sb.Append(tags.tagOriginatingTradeIdEnd);
            sb.Append(tags.tagTradeIdentifierEnd);
            sb.Append(tags.tagDataEfetivaVencimento.Replace(tags.VALOR_VARIAVEL_1, FormatarData(fpml.DATA_EFETIVA_VENCIMENTO)));
            sb.Append(tags.tagItauTradeInformation);
            sb.Append(tags.tagPartyReference.Replace(tags.VALOR_VARIAVEL_1, "Parte"));
            sb.Append(tags.tagTrader.Replace(tags.VALOR_VARIAVEL_1, fpml.FUNCIONAL_OPERADOR));
            sb.Append(tags.tagTimestamps);
            sb.Append(tags.tagTimestampCreation.Replace(tags.VALOR_VARIAVEL_1, FormatarTimestamp()));
            sb.Append(tags.tagTimestampSystem.Replace(tags.VALOR_VARIAVEL_1, FormatarTimestamp()));
            sb.Append(tags.tagTimestampsEnd);
            sb.Append(tags.tagComments);
            sb.Append(tags.tagItauTradeInformationEnd);
            sb.Append(tags.tagDeclearEnd);
            sb.Append(tags.tagNumeroClienteParte.Replace(tags.VALOR_VARIAVEL_1, fpml.ID_CLIENTE_PARTE));
            sb.Append(tags.tagGrupoEconomico.Replace(tags.VALOR_VARIAVEL_1, fpml.GRUPO_ECONOMICO_PARTE));
            sb.Append(tags.tagPartyEnd);

            return sb.ToString();
        }

        #endregion

        #region Métodos Públicos

        /// <summary>
        /// Monta o Header do FpML
        /// </summary>
        /// <param name="fpml">Objeto FpML com os dados informados</param>
        /// <returns>Tags do header do FpML</returns>
        public string MontaHeader(FPMLObject fpml)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(tags.HEADER);
            sb.Append(tags.tagRequestConfirmation);
            sb.Append(tags.tagHeader);
            sb.Append(tags.tagMessageId.Replace(tags.VALOR_VARIAVEL_1, string.Concat(fpml.SIGLA_SISTEMA, ":", fpml.NUMERO_CONTRATO, ":", "1")));
            sb.Append(tags.tagSentBy.Replace(tags.VALOR_VARIAVEL_1, fpml.SIGLA_SISTEMA));
            sb.Append(tags.tagTimestamp.Replace(tags.VALOR_VARIAVEL_1, FormatarTimestamp()));
            sb.Append(tags.tagMonitoringKey);
            sb.Append(tags.tagEKM.Replace(tags.VALOR_VARIAVEL_1, fpml.CHAVE_EKM));
            sb.Append(tags.tagProductInformation);
            return sb.ToString();
        }

        #endregion

        #region Métodos Privados

        /// <summary>
        /// Monta as tags que armazenam os dados de conta de liquidação
        /// </summary>
        /// <param name="fpml">Objeto FpML com os dados informados</param>
        /// <param name="ParteOuContraparte">Identificador de que a conta é da Parte ou da Contraparte</param>
        /// <returns>Tags de conta preenchidas</returns>
        private string MontarDadosConta(FPMLObject fpml, string ParteOuContraparte)
        {
            StringBuilder sb = new StringBuilder();

            if (ParteOuContraparte.Equals("Parte"))
            {
                //Conta correntista
                if (fpml.TIPO_CONTA_LIQUIDACAO_PARTE.Equals("0"))
                {
                    sb.Append(tags.tagSecaoConta.Replace(tags.VALOR_VARIAVEL_1, "SETTLEMENTACC1"));
                    sb.Append(tags.tagAccountIdScheme);
                    sb.Append(tags.tagTipoConta.Replace(tags.VALOR_VARIAVEL_1, "CC"));
                    sb.Append(tags.tagTipoParteConta.Replace(tags.VALOR_VARIAVEL_1, "Parte"));
                    sb.Append(tags.tagSecaoContaBancariaNumAgencia.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_AGENCIA_CONTA_LIQUIDACAO_PARTE));
                    sb.Append(tags.tagSecaoContaBancariaNumConta.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_CONTA_LIQUIDACAO_PARTE));
                    sb.Append(tags.tagSecaoContaBancariaNumDV.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_DIGITO_VERIFICADOR_CONTA_LIQUIDACAO_PARTE));
                    sb.Append(tags.tagNumeroBanco.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_BANCO_LIQUIDACAO_PARTE));
                    sb.Append(tags.tagEmpresaConglomerado.Replace(tags.VALOR_VARIAVEL_1, fpml.CODIGO_EMPRESA_CONGLOMERADO_LIQUIDACAO_PARTE));
                    sb.Append(tags.tagSecaoContaEnd);
                }
                //Conta não correntista
                else if (fpml.TIPO_CONTA_LIQUIDACAO_PARTE.Equals("1"))
                {
                    sb.Append(tags.tagSecaoConta.Replace(tags.VALOR_VARIAVEL_1, "SETTLEMENTACC1"));
                    sb.Append(tags.tagAccountIdScheme);
                    sb.Append(tags.tagTipoConta.Replace(tags.VALOR_VARIAVEL_1, "NCC"));
                    sb.Append(tags.tagTipoParteConta.Replace(tags.VALOR_VARIAVEL_1, "Parte"));
                    sb.Append(tags.tagSecaoContaBancariaNumAgencia.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_AGENCIA_CONTA_LIQUIDACAO_PARTE));
                    sb.Append(tags.tagSecaoContaBancariaNumConta.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_CONTA_LIQUIDACAO_PARTE));
                    sb.Append(tags.tagSecaoContaBancariaNumDV.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_DIGITO_VERIFICADOR_CONTA_LIQUIDACAO_PARTE));
                    sb.Append(tags.tagNumeroBanco.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_BANCO_LIQUIDACAO_PARTE));
                    sb.Append(tags.tagSecaoContaEnd);
                }
                //Conta câmara
                else if (fpml.TIPO_CONTA_LIQUIDACAO_PARTE.Equals("2"))
                {
                    sb.Append(tags.tagSecaoConta.Replace(tags.VALOR_VARIAVEL_1, "CETIPACC1"));
                    sb.Append(tags.tagNumeroContaCamara.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_CONTA_LIQUIDACAO_PARTE));
                    sb.Append(tags.tagSecaoContaEnd);
                }
            }
            else
            {
                //Conta correntista
                if (fpml.TIPO_CONTA_LIQUIDACAO_CONTRAPARTE.Equals("0"))
                {
                    sb.Append(tags.tagSecaoConta.Replace(tags.VALOR_VARIAVEL_1, "SETTLEMENTACC2"));
                    sb.Append(tags.tagAccountIdScheme);
                    sb.Append(tags.tagTipoConta.Replace(tags.VALOR_VARIAVEL_1, "CC"));
                    sb.Append(tags.tagTipoParteConta.Replace(tags.VALOR_VARIAVEL_1, "Contraparte"));
                    sb.Append(tags.tagSecaoContaBancariaNumAgencia.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_AGENCIA_CONTA_LIQUIDACAO_CONTRAPARTE));
                    sb.Append(tags.tagSecaoContaBancariaNumConta.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_CONTA_LIQUIDACAO_CONTRAPARTE));
                    sb.Append(tags.tagSecaoContaBancariaNumDV.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_DIGITO_VERIFICADOR_CONTA_LIQUIDACAO_CONTRAPARTE));
                    sb.Append(tags.tagNumeroBanco.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_BANCO_LIQUIDACAO_CONTRAPARTE));
                    sb.Append(tags.tagEmpresaConglomerado.Replace(tags.VALOR_VARIAVEL_1, fpml.CODIGO_EMPRESA_CONGLOMERADO_LIQUIDACAO_CONTRAPARTE));
                    sb.Append(tags.tagSecaoContaEnd);
                }

                //Conta não correntista
                else if (fpml.TIPO_CONTA_LIQUIDACAO_CONTRAPARTE.Equals("1"))
                {
                    sb.Append(tags.tagSecaoConta.Replace(tags.VALOR_VARIAVEL_1, "SETTLEMENTACC2"));
                    sb.Append(tags.tagAccountIdScheme);
                    sb.Append(tags.tagTipoConta.Replace(tags.VALOR_VARIAVEL_1, "NCC"));
                    sb.Append(tags.tagTipoParteConta.Replace(tags.VALOR_VARIAVEL_1, "Contraparte"));
                    sb.Append(tags.tagSecaoContaBancariaNumAgencia.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_AGENCIA_CONTA_LIQUIDACAO_CONTRAPARTE));
                    sb.Append(tags.tagSecaoContaBancariaNumConta.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_CONTA_LIQUIDACAO_CONTRAPARTE));
                    sb.Append(tags.tagSecaoContaBancariaNumDV.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_DIGITO_VERIFICADOR_CONTA_LIQUIDACAO_CONTRAPARTE));
                    sb.Append(tags.tagNumeroBanco.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_BANCO_LIQUIDACAO_CONTRAPARTE));
                    sb.Append(tags.tagSecaoContaEnd);
                }
                //Conta câmara
                else if (fpml.TIPO_CONTA_LIQUIDACAO_CONTRAPARTE.Equals("2"))
                {
                    sb.Append(tags.tagSecaoConta.Replace(tags.VALOR_VARIAVEL_1, "CETIPACC2"));
                    sb.Append(tags.tagNumeroContaCamara.Replace(tags.VALOR_VARIAVEL_1, fpml.NUMERO_CONTA_LIQUIDACAO_CONTRAPARTE));
                    sb.Append(tags.tagSecaoContaEnd);
                }             
            }

            return sb.ToString();
        }
    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fpml"></param>
        private void MontarIndexadores(FPMLObject fpml)
        {
            foreach (Indexador i in fpml.INDEXADORES)
            {
                if (i.ParteOperacao.Equals("Parte"))
                {
                    if (i.PeriodoIndexador.Equals("0"))
                    {
                        indexadorParteValorizacao = i;
                    }
                    else
                    {
                        parteTermo = i;
                    }
                }
                else
                {
                    if (i.PeriodoIndexador.Equals("0"))
                    {
                        contraparteValorizacao = i;
                    }
                    else
                    {
                        contraparteTermo = i;
                    }
                }
            }
        }

        private string FormatarTimestamp()
        {
            DateTime hoje = DateTime.Today;
            return string.Concat(hoje.Date.Year, "-", hoje.Month.ToString().PadLeft(2,'0'), "-", hoje.Day.ToString().PadLeft(2,'0'), "T", hoje.TimeOfDay, "-03:00");
        }

        private string FormatarData(string DataSemFormatacao)
        {
            string DataFormatada = string.Empty;

            if(!string.IsNullOrEmpty(DataSemFormatacao))
            {
                string ano = DataSemFormatacao.Substring(6,4);
                string mes = DataSemFormatacao.Substring(3,2);
                string dia = DataSemFormatacao.Substring(0,2);
                DataFormatada = string.Concat(dia, "-", mes, "-", ano);
            }

            return DataFormatada;
        }

        private string FormatarValor(string ValorSemFormatacao)
        {
            string ValorFormatado = string.Empty;

            if(!string.IsNullOrEmpty(ValorSemFormatacao))
            {
                ValorFormatado = ValorSemFormatacao.Replace(",","").Replace(".","");
                
                if(ValorFormatado.Length < 3)
                {
                    ValorFormatado = ValorFormatado.PadLeft(3, '0');
                }

                ValorFormatado = ValorFormatado.Insert(ValorFormatado.Length - 2, ".");
            }

            return ValorFormatado;
        }

        #endregion
    }
}
