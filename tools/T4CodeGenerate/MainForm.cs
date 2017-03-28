using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualStudio.TextTemplating;
using T4CodeGenerate.Code;

namespace T4CodeGenerate
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            cbTmpType.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           var templateFileName = @"C:\资料\Person\项目\Codeplex\Person-Blog\版本二\tests\Cotide.Tests\Cotide.T4\Templates\default.tt";
           CustomCmdLineHost host = new CustomCmdLineHost();
           Engine engine = new Engine();
           host.TemplateFileValue = templateFileName;
           //Read the text template.
           string input = File.ReadAllText(templateFileName);
           //Transform the text template.
           string output = engine.ProcessTemplate(input, host);
           string outputFileName = Path.GetFileNameWithoutExtension(templateFileName);
           outputFileName = Path.Combine(Path.GetDirectoryName(templateFileName), outputFileName);
           outputFileName = outputFileName + "1" + host.FileExtension;
           File.WriteAllText(outputFileName, output, host.FileEncoding);

           foreach (CompilerError error in host.Errors)
           {
               Console.WriteLine(error.ToString());
           }
        }

     
    }
}
