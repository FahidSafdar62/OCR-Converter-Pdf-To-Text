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
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;



namespace PDF_2_Text
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            string filePath;
            dlg.Filter = "PDF Files(*.PDF)|*.PDF|All files (*.*)|*.*";
           
          
            if(dlg.ShowDialog()==DialogResult.OK)
            {
                filePath = dlg.FileName.ToString();
          
            string strText = string.Empty;
            try
            {
                PdfReader reader = new PdfReader(filePath);
                for(int page=1;page<=reader.NumberOfPages;page++)
                {
                    ITextExtractionStrategy its = new iTextSharp.text.pdf.parser.LocationTextExtractionStrategy();
                    string s = PdfTextExtractor.GetTextFromPage(reader, page, its);
                    s = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(s)));
                    strText = strText + s;
                    richTextBox1.Text = strText;
                }
                reader.Close();
            }

            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            }

            StreamWriter File = new StreamWriter("PDF_to_Text.rtf");
            File.Write(richTextBox1.Text);
            File.Close();
        }

       
        
    }
}
