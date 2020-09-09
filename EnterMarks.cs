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
    public partial class EnterMarks : Form
    {
        public EnterMarks()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Hide();
            (new AdminMainMenu()).ShowDialog();
            Close();
        }

        private void EnterMarks_Load(object sender, EventArgs e)
        {
            LoadSkills();
        }

        private void LoadSkills()
        {
            cbSkill.Items.Clear();
            cbSession.Items.Clear();
            cbCompetitors.Items.Clear();
            dataGridView1.Rows.Clear();
            lblTotalMarks.Text = 0.ToString();
            using (var context = new Session5Entities())
            {
                var getSkills = (from x in context.Skills
                                 select x.skillName).ToArray();
                cbSkill.Items.AddRange(getSkills);
            }
        }

        private void cbSkill_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            cbSession.Items.Clear();
            cbCompetitors.Items.Clear();
            lblTotalMarks.Text = 0.ToString();
            using (var context = new Session5Entities())
            {
                var getSkillID = (from x in context.Skills
                                  where x.skillName == cbSkill.SelectedItem.ToString()
                                  select x.skillId).FirstOrDefault();
                var getSessions = (from x in context.Competitions
                                   where x.skillIdFK == getSkillID
                                   select x.sessionNo);
                foreach (var item in getSessions)
                {
                    cbSession.Items.Add(item);
                }
            }
        }

        private void cbSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            cbCompetitors.Items.Clear();
            lblTotalMarks.Text = 0.ToString();
            using (var context = new Session5Entities())
            {
                var getSkillID = (from x in context.Skills
                                  where x.skillName == cbSkill.SelectedItem.ToString()
                                  select x.skillId).FirstOrDefault();
                var getCompetitors = (from x in context.Competitors
                                      where x.skillIdFK == getSkillID
                                      select x.competitorName).ToArray();
                cbCompetitors.Items.AddRange(getCompetitors);
            }
        }

        private void cbCompetitors_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblTotalMarks.Text = 0.ToString();
            LoadData();
        }

        private void LoadData()
        {
            dataGridView1.Rows.Clear();
            using (var context = new Session5Entities())
            {
                var getSkillID = (from x in context.Skills
                                  where x.skillName == cbSkill.SelectedItem.ToString()
                                  select x.skillId).FirstOrDefault();
                var getSession = (from x in context.Competitions
                                  where x.sessionNo.ToString() == cbSession.SelectedItem.ToString() && x.skillIdFK == getSkillID
                                  select x).FirstOrDefault();
                if (getSession.q1MaxMarks != 0)
                {
                    var newRow = new List<string>() { "Question 1", "", getSession.q1MaxMarks.ToString(), "0" };
                    dataGridView1.Rows.Add(newRow.ToArray());
                }
                if (getSession.q2MaxMarks != 0)
                {
                    var newRow = new List<string>() { "Question 2", "", getSession.q2MaxMarks.ToString(), "0" };
                    dataGridView1.Rows.Add(newRow.ToArray());
                }
                if (getSession.q3MaxMarks != 0)
                {
                    var newRow = new List<string>() { "Question 3", "", getSession.q3MaxMarks.ToString(), "0" };
                    dataGridView1.Rows.Add(newRow.ToArray());
                }
                if (getSession.q4MaxMarks != 0)
                {
                    var newRow = new List<string>() { "Question 4", "", getSession.q4MaxMarks.ToString(), "0" };
                    dataGridView1.Rows.Add(newRow.ToArray());
                }
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                var getTotalMarks = Convert.ToDouble(dataGridView1[2, e.RowIndex].Value);
                if (dataGridView1[1, e.RowIndex].Value.ToString() == "Gd")
                {
                    dataGridView1[3, e.RowIndex].Value = getTotalMarks;
                }
                else if (dataGridView1[1, e.RowIndex].Value.ToString() == "Av")
                {
                    dataGridView1[3, e.RowIndex].Value = getTotalMarks * 0.65;
                }
                else if (dataGridView1[1, e.RowIndex].Value.ToString() == "Pr")
                {
                    dataGridView1[3, e.RowIndex].Value = getTotalMarks * 0.2;
                }
                var total = 0.0;
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    total += Convert.ToDouble(dataGridView1[3, item.Index].Value);
                }
                lblTotalMarks.Text = total.ToString();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            LoadSkills();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            using (var context = new Session5Entities())
            {
                var getSkillID = (from x in context.Skills
                                  where x.skillName == cbSkill.SelectedItem.ToString()
                                  select x.skillId).FirstOrDefault();
                var getSession = (from x in context.Competitions
                                  where x.sessionNo.ToString() == cbSession.SelectedItem.ToString() && x.skillIdFK == getSkillID
                                  select x).FirstOrDefault();
                var getCompetitor = (from x in context.Competitors
                                     where x.competitorName == cbCompetitors.SelectedItem.ToString() && x.skillIdFK == getSkillID
                                     select x.recordsId).FirstOrDefault();
                var findResults = (from x in context.Results
                                   where x.recordsIdFK == getCompetitor && x.competitionIdFK == getSession.competitionId
                                   select x).FirstOrDefault();
                if (findResults != null)
                {
                    foreach (DataGridViewRow item in dataGridView1.Rows)
                    {
                        if (dataGridView1[0, item.Index].Value.ToString() == "Question 1")
                        {
                            findResults.q1Marks = Convert.ToDouble(dataGridView1[3, item.Index].Value);
                        }
                        else if (dataGridView1[0, item.Index].Value.ToString() == "Question 2")
                        {
                            findResults.q2Marks = Convert.ToDouble(dataGridView1[3, item.Index].Value);
                        }
                        else if (dataGridView1[0, item.Index].Value.ToString() == "Question 3")
                        {
                            findResults.q3Marks = Convert.ToDouble(dataGridView1[3, item.Index].Value);
                        }
                        else
                        {
                            findResults.q4Marks = Convert.ToDouble(dataGridView1[3, item.Index].Value);
                        }
                        findResults.totalMarks = double.Parse(lblTotalMarks.Text);

                    }
                }
                else
                {
                    var newResults = new Result();
                    newResults.competitionIdFK = getSession.competitionId;
                    newResults.recordsIdFK = getCompetitor;
                    newResults.totalMarks = double.Parse(lblTotalMarks.Text);

                    if (getSession.q2MaxMarks == 0)
                    {
                        newResults.q1Marks = Convert.ToDouble(dataGridView1[3, 0].Value);
                        newResults.q2Marks = 0;
                        newResults.q3Marks = 0;
                        newResults.q4Marks = 0;
                    }
                    else if (getSession.q3MaxMarks == 0)
                    {
                        newResults.q1Marks = Convert.ToDouble(dataGridView1[3, 0].Value);
                        newResults.q2Marks = Convert.ToDouble(dataGridView1[3, 1].Value);
                        newResults.q3Marks = 0;
                        newResults.q4Marks = 0;
                    }
                    else if (getSession.q4MaxMarks == 0)
                    {
                        newResults.q1Marks = Convert.ToDouble(dataGridView1[3, 0].Value);
                        newResults.q2Marks = Convert.ToDouble(dataGridView1[3, 1].Value);
                        newResults.q3Marks = Convert.ToDouble(dataGridView1[3, 2].Value);
                        newResults.q4Marks = 0;
                    }
                    else
                    {
                        newResults.q1Marks = Convert.ToDouble(dataGridView1[3, 0].Value);
                        newResults.q2Marks = Convert.ToDouble(dataGridView1[3, 1].Value);
                        newResults.q3Marks = Convert.ToDouble(dataGridView1[3, 2].Value);
                        newResults.q4Marks = Convert.ToDouble(dataGridView1[3, 3].Value);
                    }
                    context.Results.Add(newResults);
                }
                context.SaveChanges();
                MessageBox.Show("Results saved!");
                btnClear_Click(null, null);

            }
        }
    }
}
