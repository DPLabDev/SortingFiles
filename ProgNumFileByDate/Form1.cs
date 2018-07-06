using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgNumFileByDate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            button2.Enabled = false;
            button3.Enabled = false;
            button6.Enabled = false;
            //Set tags
            textBox1.Tag = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browse = new FolderBrowserDialog();
            if (browse.ShowDialog() == System.Windows.Forms.DialogResult.OK)

                // select path with browser object
                textBox1.Text = browse.SelectedPath;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            
            string targetDirectory = textBox1.Text;

            if (!Directory.Exists(targetDirectory + @"\output"))
            {

                string[] fileNames = Directory.GetFiles(targetDirectory, "*.*");

                // read the creation time for each file
                DateTime[] creationTimes = new DateTime[fileNames.Length];
                for (int i = 0; i < fileNames.Length; i++)
                    creationTimes[i] = new FileInfo(fileNames[i]).CreationTime;

                // sort it
                Array.Sort(creationTimes, fileNames);

                textBox2.Clear();

                //Console.WriteLine("Files ordered by creation time");
                for (int i = 0; i < fileNames.Length; i++)
                {
                    textBox2.AppendText(creationTimes[i] + " " + Path.GetFileName(fileNames[i]) + Environment.NewLine);

                    if (!Directory.Exists(targetDirectory + @"\output")) Directory.CreateDirectory(targetDirectory + @"\output");
                    File.Copy(fileNames[i], targetDirectory + @"\output" + "\\" + (i + 1) + " - " + creationTimes[i].ToString("dd-MM-yyyy-HH-mm-ss") + " - " + Path.GetFileName(fileNames[i]));
                }
            }

            else
            {
                textBox2.Clear();
                textBox2.AppendText("The output folder already exists! Please, if you want to delete the folder, click reset.");
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            string targetDirectory = textBox1.Text;
            if (Directory.Exists(targetDirectory + @"\output")) Directory.Delete(targetDirectory + @"\output", true);
            textBox2.Clear();
            textBox2.AppendText("The output foder and its content have been deleted.");
            if (!Directory.Exists(targetDirectory + @"\output"))
            {
                textBox2.Clear();
                textBox2.AppendText("The output folder does not exist anymore.");
            }

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text.Length == 0)
                textBox.Tag = false;
            else
                textBox.Tag = true;
            ValidateButton();
        }

        private void ValidateButton()
        {
            this.button2.Enabled = (bool)textBox1.Tag;
            this.button3.Enabled = (bool)textBox1.Tag;
            this.button6.Enabled = (bool)textBox1.Tag;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button6_Click(object sender, EventArgs e)
        {
            string targetDirectory = textBox1.Text;

            if (Directory.Exists(targetDirectory + @"\output"))
            {
                System.Diagnostics.Process.Start(targetDirectory + @"\output");
            }
            else
            {
                textBox2.Clear();
                textBox2.AppendText("There is no output folder, yet.");
            }
                
        }
    }
}
