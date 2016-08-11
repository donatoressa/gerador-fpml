using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using System.Xml;
using System.Xml.Linq;

namespace Gerador_Mensagem_FpML
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Utilitarios u = new Utilitarios();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Deseja realmente sair?", "Atenção", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void DePara_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Uri uri = new Uri("Inventario.pdf", UriKind.Relative);
                System.Diagnostics.Process.Start(uri.LocalPath);
            }
            catch
            {
               MessageBoxResult mb = MessageBox.Show("Arquivo inválido ou não encontrado.", "Erro", MessageBoxButton.OK);
            }
        }

        private void Config_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Importar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Exportar_Click(object sender, RoutedEventArgs e)
        {
            ScrollViewer x = ((ScrollViewer) tabPanel.SelectedContent);
            
        }

        static Stream GetResourceStream(string resourceName)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
        }

        private void Sobre_Click(object sender, RoutedEventArgs e)
        {
            Sobre s = new Sobre();
            s.Show();
        }
    }
}
