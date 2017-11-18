using SheepAspect.Framework;
using SheepAspect.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SheepAspectLab.Aspects;

namespace SheepAspectLab
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _mainForm = this; // for: Log Mechanism                  
        }

        #region Log Mechanism

        static private Form1 _mainForm = null;

        static public void WriteLog(string message)
        {
            if (_mainForm != null)
                _mainForm.txtMessage.AppendText(message + "\r\n");
        }

        #endregion

        [WaitCursor]
        [CatchAndLog]
        private void button1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(3000);
            label1.Text = DateTime.Now.ToString("ss");
        }

        [CatchAndLog]
        [WaitCursor]
        private void button2_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(3000);
            throw new ApplicationException("我錯了");
        }
    }

}
