using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace HogerLagerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BrushConverter bc = new BrushConverter();

        int guess;
        int countPoging;
        int points;
        int bestGuess = 0;
        float totalGuesses = 0;

        Random rnd = new Random();
        int rndNumber;

        public MainWindow()
        {
            InitializeComponent();

            BtnMinimize.Background = (Brush)bc.ConvertFrom("#EEEEEE");
            BtnClose.Background = (Brush)bc.ConvertFrom("#D50000");

            // Create a 'random' number (as far as random goes)
            rndNumber = rnd.Next(0, 1000);
        }

        private void BtnClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnClose_MouseEnter(object sender, MouseEventArgs e)
        {
            BtnClose.Opacity = 0.5;
        }

        private void BtnClose_MouseLeave(object sender, MouseEventArgs e)
        {
            BtnClose.Opacity = 1;
        }

        private void BtnMinimize_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnMinimize_MouseEnter(object sender, MouseEventArgs e)
        {
            BtnMinimize.Background = Brushes.LightGray;
            txtMinimize.Foreground = Brushes.White;
        }

        private void BtnMinimize_MouseLeave(object sender, MouseEventArgs e)
        {
            BtnMinimize.Background = (Brush)bc.ConvertFrom("#EEEEEE");
            txtMinimize.Foreground = Brushes.LightGray;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtGuesss_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtGuesss.Text != "")
            {
                guess = Convert.ToInt16(txtGuesss.Text);
            }

            if (e.Key == Key.Return && guess != 0)
            {
                // Add 1 to guesses to current game
                countPoging++;

                // Show the guess
                lblGuess.Content = "Jouw gok: " + guess;

                // Show quantity of quesses of the current game
                txbPoging.Text = "Hoeveelheid pogingen: " + countPoging;

                if (guess > rndNumber)
                {
                    // Show guess too high
                    lblHogerLager.Content = "Lager!";
                }
                else if (guess < rndNumber)
                {
                    // Show guess too low
                    lblHogerLager.Content = "Hoger!";
                }
                else
                {
                    points++;
                    lblHogerLager.Content = "Correct!";
                    txbPoints.Text = "Punten: " + points;

                    if (bestGuess == 0)
                    {
                        bestGuess = countPoging;
                    }
                    else if (bestGuess > countPoging)
                    {
                        bestGuess = countPoging;
                    }

                    txbBestGuess.Text = "Beste poging: " + bestGuess;

                    // Get quantity of guesses
                    if (totalGuesses == 0)
                    {
                        totalGuesses = countPoging;
                    }
                    else
                    {
                        totalGuesses = totalGuesses + countPoging;
                    }

                    // Calculate average quantity of guesses
                    float avgGuess = totalGuesses / points;

                    txbAvgGuesses.Text = "Gemiddeld aantal pogingen: " + Math.Round(avgGuess, 0) + " ("+ Math.Round(avgGuess, 1) + ")";

                    // Quantity of guesses reset
                    countPoging = 0;
                    txbPoging.Text = "Hoeveelheid pogingen: " + countPoging;

                    // Get new rnd number (start new game)
                    rndNumber = rnd.Next(0, 1000);
                }

                // Reset
                txtGuesss.Text = "";
                guess = 0;
            }
        }
    }
}
