using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AddNum
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        //This function gets the current timestamp for a later function
        public static String GetTimestamp(DateTime value)
        {
            return value.ToString("yyyy/MM/dd/HH/mm/ssffff");
        }
        //using a running total to add up the numbers
        double total = 0;
        //a previous total holder for an undo
        double previoustotal = 0;
        //how many times something has been added
        int shims = 0;
        //another previous for shims
        int previousshims = 0;

        private void button3_Click(object sender, EventArgs e)
        {
            //Displays all the information in a new window and resets the app
            //Gets a current timestamp
            String timeStamp = GetTimestamp(DateTime.Now);
            // Create a string array with the lines of text
            string[] lines = { timeStamp, "Total weight = " + total + Environment.NewLine + "Total shims: " + shims};
            // Set a variable to the Documents path.
            string docPath =
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            // Write the string array to a new file named "WriteLines.txt".
            using (StreamWriter outputFile = new
           StreamWriter(Path.Combine(docPath, "AddNumTotal.txt")))
            {
                foreach (string line in lines)
                    outputFile.WriteLine(line);
            }
            MessageBox.Show("Total weight = " + total + Environment.NewLine + "Total shims: " + shims, "Totals");
            total = 0;
            label4.Text = "" + total;
            textBox1.Text = "";
            shims = 0;
            label5.Text = "" + shims;
            previousshims = 0;
            previoustotal = 0;
            label6.Text = "";
            textBox1.Select();
            textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //adds the number in the textbox to the total and displays the new total
            //same for shims
            try
            {
                double add = Convert.ToDouble(this.textBox1.Text);
                previoustotal = total;
                previousshims = shims;
                total = total + add;
                label4.Text = "" + total;
                shims += 1;
                label5.Text = "" + shims;
                textBox1.Text = "";
                label6.Text = "";
                //Re-enables or keeps the undo button active
                button2.Enabled = true;
                textBox1.Select();
                textBox1.Focus();
            }
            catch (Exception ex) //To prevent errors that would give users a heart attack
            {
                label6.Text = "Error: Don't do what you just attempted";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //goes back one interation
            total = previoustotal;
            shims = previousshims;
            label4.Text = "" + total;
            label5.Text = "" + shims;
            label6.Text = "";
            //Gray out and disable the undo button
            button2.Enabled = false;
            textBox1.Select();
            textBox1.Focus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //resets the totals and clears input box
            total = 0;
            label4.Text = "" + total;
            textBox1.Text = "";
            shims = 0;
            label5.Text = "" + shims;
            previousshims = 0;
            previoustotal = 0;
            label6.Text = "";
            button2.Enabled = true;
            textBox1.Select();
            textBox1.Focus();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Displays helpful information for the user when they click on the Help menu
            MessageBox.Show("This is a tool to add things together." + Environment.NewLine + "Input each number you want to add one at a time, press Enter/Add, and the total will be displayed", "Help");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //focus on textbox for input
            textBox1.Select();
            textBox1.Focus();
        }
    }
}
