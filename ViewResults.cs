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
    public partial class ViewResults : Form
    {
        public ViewResults()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Hide();
            (new AdminMainMenu()).ShowDialog();
            Close();
        }

        private void ViewResults_Load(object sender, EventArgs e)
        {
            LoadSkills();
        }

        private void LoadSkills()
        {
            cbSkill.Items.Clear();
            using (var context = new Session5Entities())
            {
                var getSkills = (from x in context.Skills
                                 select x.skillName).ToArray();
                cbSkill.Items.AddRange(getSkills);
            }
        }

        private void cbSkill_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            dataGridView1.Rows.Clear();
        }
    }
}
