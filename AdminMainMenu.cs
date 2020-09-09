using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPQR_Session5_9_9
{
    public partial class AdminMainMenu : Form
    {
        public AdminMainMenu()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Hide();
            (new Login()).ShowDialog();
            Close();
        }

        private void btnEnterMarks_Click(object sender, EventArgs e)
        {
            Hide();
            (new EnterMarks()).ShowDialog();
            Close();
        }

        private void btnViewResults_Click(object sender, EventArgs e)
        {
            Hide();
            (new ViewResults()).ShowDialog();
            Close();
        }

        private void btnAnalyzeResults_Click(object sender, EventArgs e)
        {
            Hide();
            (new AnalyzeResults()).ShowDialog();
            Close();
        }
    }
}
