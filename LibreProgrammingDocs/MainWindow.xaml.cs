using System.IO;
using System.Windows;
using System.Windows.Input;

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
            webbrowser.NavigationCompleted += Webbrowser_NavigationCompleted;
        }

        private void Webbrowser_NavigationCompleted(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            sourceLocationTB.Text = webbrowser.Source.ToString();
            if (webbrowser.Source == new Uri("https://chatgpt.com/"))
            {
                this.WindowState = WindowState.Maximized; //When resizing, the message when editing the textbox will disappear on resize, so to give the best experience, just make it max so it fits all the text
                EditChatGPTRequestTextbox();
            }
        }

        async Task EditChatGPTRequestTextbox()
        {
            // Delay for 1 second asynchronously
            await Task.Delay(1000);

            // Define the request message using verbatim string literal
            string issue = "<issue>";  // Replace with actual issue description
            string request = $@"
        Act as an AI Documentation Helper, and help the user with the documentation of a programming language they specify, and give straightforward answers, and explain in detail how the issue is and how to solve it.

        The Issue is: {issue}
    ".Trim();

            // Execute JavaScript to set value of the chat request textarea
            string script = $"document.getElementById('prompt-textarea').value = `{request}`;";
            await webbrowser.CoreWebView2.ExecuteScriptAsync(script);

            string cssScript = @"
        var textarea = document.getElementById('prompt-textarea');
        var topmost = document.getElementsByClassName(""relative flex h-full max-w-full flex-1 flex-col"")[1];

        var newDiv = document.createElement(""div"");

        newDiv.textContent = ""Replace the <issue> with whatever issue you need help with :)"";

        newDiv.style.backgroundColor = ""#444444"";
        newDiv.style.padding = ""10px"";
        newDiv.style.marginTop = ""10px"";

        topmost.appendChild(newDiv);

        textarea.style.width = 'auto';
        textarea.style.height = '200px';
        textarea.style.backgroundColor = '#202020';
        
    ";
            await webbrowser.CoreWebView2.ExecuteScriptAsync(cssScript);
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
            if (path.EndsWith("/"))
            {
                MessageBox.Show("Illegal Move!");
                return;
            }
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

        private void sourceLocationTB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoadPage(sourceLocationTB.Text);
            }
        }

        private void backBTN_Click(object sender, RoutedEventArgs e)
        {
            webbrowser.GoBack();
        }

        private void forwardBTN_Click(object sender, RoutedEventArgs e)
        {
            webbrowser.GoForward();
        }

        private void chatgptBTN_Click(object sender, RoutedEventArgs e)
        {
            LoadPage("https://www.chatgpt.com");
        }
    }
}