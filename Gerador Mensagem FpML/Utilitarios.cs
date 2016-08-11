using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace Gerador_Mensagem_FpML
{
    public class Utilitarios
    {
        List<XElement> saida = new List<XElement>();

        public string PreencherTipoCotacao(ComboBox cb)
        {
            if (cb.SelectedIndex >= 0)
            {
                string entrada = cb.SelectedItem.ToString().Substring(38);

                if (entrada.Equals("Fechamento"))
                {
                    return "Close";
                }
                else if (entrada.Equals("Média"))
                {
                    return "Avg";
                }
                else if (entrada.Equals("Máxima"))
                {
                    return "High";
                }
                else if (entrada.Equals("Mínima"))
                {
                    return "Low";
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        public string PreencherTipoConta(ComboBox cb)
        {
            if (cb.SelectedIndex >= 0)
            {
                if (cb.SelectedItem.ToString().Substring(38).Equals("Correntista"))
                {
                    return "CC";
                }
                else if (cb.SelectedItem.ToString().Substring(38).Equals("Não Correntista"))
                {
                    return "NCC";
                }
                else if (cb.SelectedItem.ToString().Substring(38).Equals("Câmara"))
                {
                    return "CAMARA";
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }

        public void LimparFormGrid(Grid g)
        {
            foreach (object c in g.Children)
            {
                if(c is StackPanel)
                {
                    LimparFormStackPanel((StackPanel)c);
                }
            }
        }

        public void LimparFormStackPanel(StackPanel stackpanel)
        {
            foreach (object sp in stackpanel.Children)
            {
                if (sp is TextBox)
                {
                    ((TextBox)sp).Clear();
                }
                else if (sp is ComboBox)
                {
                    ((ComboBox)sp).SelectedIndex = -1;
                }
                else if (sp is DatePicker)
                {
                    ((DatePicker)sp).Text = string.Empty;
                }
                else if (sp is CheckBox)
                {
                    ((CheckBox)sp).IsChecked = false;
                }
                else if(sp is DataGrid)
                {
                    ((DataGrid)sp).DataContext = null;
                    ((DataGrid)sp).Items.Refresh();
                }
                else if (sp is StackPanel)
                {
                    LimparFormStackPanel((StackPanel)sp);
                }
            }
        }

        public void DesabilitarTudo(Grid g)
        {
            foreach (object c in g.Children)
            {
                if (c is StackPanel)
                {
                    DesabilitarStackPanel((StackPanel)c);
                }
            }
        }

        public void DesabilitarStackPanel(StackPanel stackpanel)
        {
            foreach (object sp in stackpanel.Children)
            {
                if (sp is TextBox || sp is CheckBox || sp is ComboBox || sp is DatePicker || sp is DataGrid)
                {
                    Control a = (Control)sp;
                    a.IsEnabled = false;
                }
                else if(sp is StackPanel)
                {
                    DesabilitarStackPanel((StackPanel) sp);
                }
            }
        }

        public void HabilitarTudo(Grid g)
        {
            foreach (object c in g.Children)
            {
                if (c is StackPanel)
                {
                    HabilitarStackPanel((StackPanel)c);
                }
            }
        }

        public void HabilitarStackPanel(StackPanel stackpanel)
        {
            foreach (object sp in stackpanel.Children)
            {
                if (sp is TextBox || sp is CheckBox || sp is ComboBox || sp is DatePicker || sp is DataGrid)
                {
                    Control a = (Control)sp;
                    a.IsEnabled = true;
                }
                else if(sp is StackPanel)
                {
                    HabilitarStackPanel((StackPanel)sp);
                }
            }
        }

        public List<XElement> GerarTemplateExportacao(Grid g)
        {
            foreach (object c in g.Children)
            {
                if (c is StackPanel)
                {
                    GerarTemplateExportacaoStackPanel((StackPanel)c);
                }
            }

            return saida;
        }

        public List<XElement> GerarTemplateExportacaoStackPanel(StackPanel stackpanel)
        {
            foreach (object sp in stackpanel.Children)
            {
                if (sp is TextBox) 
                {
                    TextBox a = (TextBox) sp;
                    saida.Add(new XElement(a.Name, a.Text));
                }
                else if(sp is CheckBox)
                {
                    CheckBox a = (CheckBox) sp;
                    saida.Add(new XElement(a.Name, a.IsChecked));
                }
                else if(sp is ComboBox)
                {
                    ComboBox a = (ComboBox) sp;
                    saida.Add(new XElement(a.Name, a.SelectedIndex));
                }
                else if(sp is DatePicker)
                {
                    DatePicker a = (DatePicker)sp;
                    saida.Add(new XElement(a.Name, a.SelectedDate));
                }
                else if(sp is DataGrid)
                {
                    //DataGrid a = (DataGrid) sp;
                    //saida.Add(new XElement(a.Name));
                    //for (int i = 0; i < a.Items.Count; i++)
                    //{
                    //    saida.Add(new XElement(((DataGridRow) a.Items[i]).Name,((DataGridRow) a.Items[i]).)
                    //}
                }
                else if (sp is StackPanel)
                {
                    GerarTemplateExportacaoStackPanel((StackPanel)sp);
                }
            }
            return saida;
        }
    }
}
