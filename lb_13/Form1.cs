namespace Lb_13
{
    using Microsoft.Office.Interop.Excel;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using Word = Microsoft.Office.Interop.Word;
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Word.Application word;
        Word.Document doc;
        Word.Range r;
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            word = new Word.Application();
            word.Visible = true;
            doc = word.Documents.Add();
            Word.Selection currentSelection = word.Application.Selection;
            currentSelection.TypeText(textBox1.Text + ",  " + comboBox1.Text + ",  " + comboBox2.Text);
            int cur_pos = comboBox1.Text.Length + textBox1.Text.Length + comboBox2.Text.Length + 6;

            r = doc.Range(0, cur_pos);
            r.Bold = 1;
            r.Font.Name = "Times New Roman";
            r.Font.Size = 16;
            r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;


            currentSelection.TypeParagraph();

            r.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            r = doc.Range(cur_pos + 1, cur_pos + 1);
            Word.Table table = doc.Tables.Add(r, dataGridView1.RowCount, dataGridView1.ColumnCount);
            table.Borders.Enable = 1;
            for (int j = 0; j < dataGridView1.ColumnCount; j++)
            {
                currentSelection.TypeText(dataGridView1.Columns[j].HeaderText);
                currentSelection.MoveRight();
            }

            for (int row = 0; row < dataGridView1.RowCount; row++)
            {
                for (int col = 0; col < dataGridView1.ColumnCount; col++)
                {
                    if (dataGridView1.Rows[row].Cells[col].Value != null)
                    {
                        currentSelection.TypeText(dataGridView1.Rows[row].Cells[col].Value.ToString());
                        currentSelection.MoveRight();
                    }

                }
          
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                word = new Word.Application();
                doc = word.Documents.Open(openFileDialog1.FileName);

                Word.Paragraph firstParagraph = doc.Paragraphs[1];
                string paragraphText = firstParagraph.Range.Text.Trim();

                string[] parts = paragraphText.Split(new string[] { ",  " }, StringSplitOptions.None);

                    textBox1.Text = parts[0];
                    comboBox1.Text = parts[1];
                    comboBox2.Text = parts[2];

                Word.Table table = doc.Tables[1];
                dataGridView1.RowCount = table.Rows.Count;
                dataGridView1.ColumnCount = table.Columns.Count;

                for (int row = 2; row <= table.Rows.Count; row++)
                {
                    for (int col = 1; col <= table.Columns.Count; col++)
                    {
                        string cellText = table.Cell(row, col).Range.Text.TrimEnd('\r', '\a');
                        dataGridView1.Rows[row - 2].Cells[col - 1].Value = cellText;
                    }
                }

            }
            word.Quit();
        }
    }
}
