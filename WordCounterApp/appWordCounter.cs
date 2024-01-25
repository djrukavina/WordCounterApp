using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WordCounterApp
{
    public partial class appWordCounter : Form
    {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        string selectedFilePath = "";

        static string[] SplitTextToWords(string text)
        {
            return Regex.Split(text, @"\W+").Where(word => !string.IsNullOrEmpty(word)).ToArray();
        }
        static Dictionary<string, int> CountWords(string[] words)
        {
            Dictionary<string, int> numAppearing = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            foreach(string word in words)
            {
                string currentWord = word.ToLower();
                if (numAppearing.ContainsKey(currentWord))
                {
                    numAppearing[currentWord]++;
                }
                else
                {
                    numAppearing[currentWord] = 1;
                }
            }
            return numAppearing;
        }

        public appWordCounter()
        {
            InitializeComponent();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            DialogResult result = openFileDialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                selectedFilePath = openFileDialog.FileName;
                btnStart.Enabled = true;
            }
            textBox.Text = (selectedFilePath != "") ? selectedFilePath : "File not selected!";
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (File.Exists(selectedFilePath))
            {
                string textData = File.ReadAllText(selectedFilePath);
                string[] words = SplitTextToWords(textData);
                Dictionary<string, int> numAppearing = CountWords(words);
                var sortedWords = numAppearing.OrderByDescending(word => word.Value);

                richTextBox.Clear();
                richTextBox.AppendText("Count\t\tWord\n");
                foreach(var kvp in sortedWords)
                {
                    richTextBox.AppendText($"{kvp.Value}\t\t{kvp.Key}\n");
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
