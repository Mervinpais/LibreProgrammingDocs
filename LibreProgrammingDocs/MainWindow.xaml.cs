using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibreProgrammingDocs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeWebView();
            LoadHomePage();
        }

        private void LoadHomePage()
        {
            LoadPage(@"Docs\Home.html", true);
        }

        private async void InitializeWebView()
        {
            // Ensure the WebView2 control is initialized
            await webbrowser.EnsureCoreWebView2Async(null);

        }
        public void LoadPage(string path, bool relative = false)
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            string fullPath = path;

            if (relative)
            {
                fullPath = System.IO.Path.Combine(currentDirectory, path);
            }

            Uri fileUri = new Uri(fullPath);

            webbrowser.Source = fileUri;
        }

        private void homeBTN_Click(object sender, RoutedEventArgs e)
        {
            LoadPage(@"Docs\Home.html", true);
        }

        private void settingsBTN_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("""
                This has not been implemented yet :/
                """, "", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void creditsBTN_Click(object sender, RoutedEventArgs e)
        {
            LoadPage(@"Docs\Credits.html", true);
            /* MessageBox.Show("""
                 CREDITS:
                     - Mpais14
                     - (!) Microsoft Documentation (C#, F#, Visual Basic, etc.)
                     - (!) Google Documentation (GoLang)
                     - ... And more!

                     (!) - Not implemented
             """, "", MessageBoxButton.OK, MessageBoxImage.Information);*/
        }
    }
}