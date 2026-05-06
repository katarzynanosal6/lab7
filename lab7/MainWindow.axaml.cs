using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lab7
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void ButtonClicked(object? source, RoutedEventArgs args)
        {
            var inputBox = this.FindControl<TextBox>("InputTextBox");
            var resultBlock = this.FindControl<TextBlock>("ResultTextBlock");

            if (inputBox == null || resultBlock == null) return;

            string sequence = inputBox.Text?.ToUpper().Trim() ?? "";

            sequence = new string(sequence.Where(c => c == 'A' || c == 'C' || c == 'G' || c == 'T').ToArray());

            if (sequence.Length < 4)
            {
                resultBlock.Text = "Wprowadzona sekwencja jest za krótka. Podaj minimum 4 nukleotydy.";
                return;
            }

            Dictionary<string, int> kmersCount = new Dictionary<string, int>();

            for (int i = 0; i <= sequence.Length - 4; i++)
            {
                string kmer = sequence.Substring(i, 4);

                if (kmersCount.ContainsKey(kmer))
                {
                    kmersCount[kmer]++;
                }
                else
                {
                    kmersCount[kmer] = 1;
                }
            }
            StringBuilder resultText = new StringBuilder();
            var sortedKmers = kmersCount.OrderByDescending(x => x.Value);

            foreach (var item in sortedKmers)
            {
                resultText.AppendLine($"{item.Key} : {item.Value}");
            }

            resultBlock.Text = resultText.ToString();
        }
    }
}