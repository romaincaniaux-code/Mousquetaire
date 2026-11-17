using System.Windows;

namespace Mousquetaire
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(LoginTextBox.Text) || string.IsNullOrWhiteSpace(PasswordInput.Password))
            {
                StatusText.Text = "Veuillez renseigner votre identifiant et votre mot de passe.";
                StatusText.Visibility = Visibility.Visible;
                return;
            }

            StatusText.Text = "La connexion n'est pas encore reliée à un service d'authentification.";
            StatusText.Visibility = Visibility.Visible;
        }
    }
}