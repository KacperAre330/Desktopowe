using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace Funkcje
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                string fileContent = File.ReadAllText(filePath, Encoding.UTF8);
                OriginalContentTextBox.Text = fileContent;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                File.WriteAllText(filePath, ModifiedContentTextBox.Text, Encoding.UTF8);
            }
        }

        private void OptionRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;

            if (radioButton == Option1RadioButton)
            {
                string inputText = OriginalContentTextBox.Text;
                char[] charArray = inputText.ToCharArray();
                for (int i = 0; i < charArray.Length; i++)
                {
                    if (i % 2 == 1)
                    {
                        charArray[i] = char.ToUpper(charArray[i]);
                    }
                }
                ModifiedContentTextBox.Text = new string(charArray);
            }
            else if (radioButton == Option2RadioButton)
            {
                string inputText = OriginalContentTextBox.Text;
                string[] words = inputText.Split(' ');
                for (int i = 0; i < words.Length; i++)
                {
                    if (!string.IsNullOrEmpty(words[i]))
                    {
                        words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1);
                    }
                }
                ModifiedContentTextBox.Text = string.Join(" ", words);
            }
        }

        private void ChangeCaseButton_Click(object sender, RoutedEventArgs e)
        {
            string inputText = OriginalContentTextBox.Text;
            char[] charArray = inputText.ToCharArray();
            for (int i = 0; i < charArray.Length; i++)
            {
                if (i % 2 == 1)
                {
                    charArray[i] = char.IsUpper(charArray[i]) ? char.ToLower(charArray[i]) : char.ToUpper(charArray[i]);
                }
            }
            ModifiedContentTextBox.Text = new string(charArray);
        }

        private void CapitalizeWordsButton_Click(object sender, RoutedEventArgs e)
        {
            string inputText = OriginalContentTextBox.Text;
            string[] words = inputText.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (!string.IsNullOrEmpty(words[i]))
                {
                    words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1);
                }
            }
            ModifiedContentTextBox.Text = string.Join(" ", words);
        }

        private void RemovePolishCharsButton_Click(object sender, RoutedEventArgs e)
        {
            string inputText = OriginalContentTextBox.Text;
            string withoutPolishChars = new string(inputText.Select(c => c == 'ą' ? 'a' : c == 'ć' ? 'c' : c == 'ę' ? 'e' : c == 'ł' ? 'l' : c == 'ń' ? 'n' : c == 'ó' ? 'o' : c == 'ś' ? 's' : c == 'ź' ? 'z' : c == 'ż' ? 'z' : c).ToArray());
            ModifiedContentTextBox.Text = withoutPolishChars;
        }

        private void RemoveStringButton_Click(object sender, RoutedEventArgs e)
        {
            string inputText = OriginalContentTextBox.Text;
            string textToRemove = SearchTextBox.Text;
            string newText = inputText.Replace(textToRemove, string.Empty);
            ModifiedContentTextBox.Text = newText;
        }

        private void ReverseTextButton_Click(object sender, RoutedEventArgs e)
        {
            string inputText = OriginalContentTextBox.Text;
            char[] charArray = inputText.ToCharArray();
            Array.Reverse(charArray);
            ModifiedContentTextBox.Text = new string(charArray);
        }

        private void ShiftCharsButton_Click(object sender, RoutedEventArgs e)
        {
            string inputText = OriginalContentTextBox.Text;
            int shiftAmount = int.Parse(SearchTextBox.Text);
            char[] charArray = inputText.ToCharArray();
            for (int i = 0; i < charArray.Length; i++)
            {
                if (char.IsLetter(charArray[i]))
                {
                    char newChar = (char)(charArray[i] + shiftAmount);
                    charArray[i] = newChar;
                }
            }
            ModifiedContentTextBox.Text = new string(charArray);
        }

        private void SaveWordsToFileButton_Click(object sender, RoutedEventArgs e)
        {
            string inputText = OriginalContentTextBox.Text;
            string[] words = inputText.Split(' ');
            File.WriteAllLines("output.txt", words, Encoding.UTF8);
        }

        private void FindFirstWordButton_Click(object sender, RoutedEventArgs e)
        {
            string inputText = OriginalContentTextBox.Text;
            string wordToFind = SearchTextBox.Text;
            int index = inputText.IndexOf(wordToFind, StringComparison.OrdinalIgnoreCase);
            if (index >= 0)
            {
                ModifiedContentTextBox.Text = "Znaleziono w pozycji: " + index.ToString();
            }
            else
            {
                ModifiedContentTextBox.Text = "Nie znaleziono.";
            }
        }

        private void CountWordOccurrencesButton_Click(object sender, RoutedEventArgs e)
        {
            string inputText = OriginalContentTextBox.Text;
            string wordToCount = SearchTextBox.Text;
            int count = inputText.Split(new string[] { wordToCount }, StringSplitOptions.None).Length - 1;
            ModifiedContentTextBox.Text = "Liczba wystąpień: " + count.ToString();
        }

        private void TruncateTextButton_Click(object sender, RoutedEventArgs e)
        {
            string inputText = OriginalContentTextBox.Text;
            int maxLength = int.Parse(SearchTextBox.Text);
            if (inputText.Length > maxLength)
            {
                ModifiedContentTextBox.Text = inputText.Substring(0, maxLength);
            }
            else
            {
                ModifiedContentTextBox.Text = inputText;
            }
        }

        private void DisplayAndSaveInfoButton_Click(object sender, RoutedEventArgs e)
        {
            string info = ModifiedContentTextBox.Text;
            ModifiedContentTextBox.Text += Environment.NewLine + "Informacja: " + info;
            File.WriteAllText("info.txt", info, Encoding.UTF8);
        }
    }
}
