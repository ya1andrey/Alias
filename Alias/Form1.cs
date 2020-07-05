using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;

namespace Alias
{

    public partial class Form1 : Form
    {

        int index;
        static List<Word> dictionary = new List<Word>(); //лист экземпляров класса ворд 

        public Form1()
        {
            InitializeComponent();
            dictionary.Add(new Word("Apple", "Яблоко"));//первое слово, которое добавляется в лист вордов при открытии программы.

        }

        static public void openTxtFile()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.InitialDirectory = "C: \\Users\\Я\\Desktop";
                ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                ofd.FilterIndex = 2;
                ofd.RestoreDirectory = true;

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string filePath = ofd.FileName;
                    string[] NewFile = File.ReadAllLines(filePath);
                    foreach (string str in NewFile)
                    {
                        try
                        {
                            string eng = null;
                            string rus = null;
                            string[] words = str.Split(new char[] { '.', ',', '!', '?', ';', ':', ' ', '-' }, StringSplitOptions.RemoveEmptyEntries);
                            foreach (string s in words)
                            {
                                bool test = s.Any<char>(x => x >= 'A' && x <= 'z');
                                if (test) eng += s;
                                else rus += s+' ';
                                
                            }
                            dictionary.Add(new Word(eng, rus));
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
            }
        }

        public string showWord()
        {
            Random rnd = new Random(); //новый элемент класса рандом
            index = rnd.Next(dictionary.Count);
            return dictionary[index].e;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            KeyPreview = true;  //чтобы кнопка была активна уже при открытии программы 
            label1.Text = showWord();
            label2.Text = null; //очистить лэйбл перевода

        }

        
        private void добавитьСловаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            openTxtFile();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            label2.Text = dictionary[index].r; //r - перевод на русский
        }
    }
}
