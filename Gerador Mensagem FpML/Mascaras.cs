using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Gerador_Mensagem_FpML
{
    class Mascaras
    {
        public TextBox AplicarMascaraNumericaSimples(KeyEventArgs e, TextBox caixaTexto)
        {
            if (e.Key != Key.D0 && e.Key != Key.D1 && e.Key != Key.D2 && e.Key != Key.D3 && e.Key != Key.D4 &&
                e.Key != Key.D5 && e.Key != Key.D6 && e.Key != Key.D7 && e.Key != Key.D8 && e.Key != Key.D9 &&
                e.Key != Key.Tab && e.Key != Key.LeftAlt && e.Key != Key.Home && e.Key != Key.End && 
                e.Key != Key.LeftShift && e.Key != Key.RightShift)
            {
                if (caixaTexto.Text.Length > 0)
                {
                    caixaTexto.Text = caixaTexto.Text.Remove(caixaTexto.Text.Length - 1);
                    caixaTexto.CaretIndex = caixaTexto.Text.Length > 0 ? caixaTexto.Text.Length : 0;
                }
            }

            return caixaTexto;
        }

        public TextBox AplicarMascaraAlfanumericaSimples(KeyEventArgs e, TextBox caixaTexto)
        {
            if (e.Key == Key.D0 || e.Key == Key.D1 || e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 ||
                e.Key == Key.D5 || e.Key == Key.D6 || e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9 )
            {
                if (caixaTexto.Text.Length > 0)
                {
                    caixaTexto.Text = caixaTexto.Text.Remove(caixaTexto.Text.Length - 1);
                    caixaTexto.CaretIndex = caixaTexto.Text.Length > 0 ? caixaTexto.Text.Length : 0;
                }
            }

            return caixaTexto;
        }

    }
}
