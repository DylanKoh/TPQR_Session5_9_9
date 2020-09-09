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
    public partial class CalculateBonus : Form
    {
        public CalculateBonus()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Hide();
            (new AdminMainMenu()).ShowDialog();
            Close();
        }

        private void CalculateBonus_Load(object sender, EventArgs e)
        {
            LoadSkills();
        }
        private void LoadSkills()
        {
            cbSkill.Items.Clear();
            cbCompetitors.Items.Clear();
            using (var context = new Session5Entities())
            {
                var getSkills = (from x in context.Skills
                                 select x.skillName).ToArray();
                cbSkill.Items.AddRange(getSkills);
            }
        }

        private void cbSkill_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbCompetitors.Items.Clear();
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
            dataGridView1.Rows.Clear();
            using (var context = new Session5Entities())
            {
                var getSkill = (from x in context.Skills
                                where x.skillName == cbSkill.SelectedItem.ToString()
                                select x).FirstOrDefault();
                var getCompetitons = (from x in context.Competitions
                                      where x.skillIdFK == getSkill.skillId
                                      select x);
                var bonus = 0;
                foreach (var item in getCompetitons)
                {
                    var getCompetitorResult = (from x in context.Results
                                               where x.Competitor.skillIdFK == getSkill.skillId && x.Competitor.competitorName == cbCompetitors.SelectedItem.ToString()
                                               where x.competitionIdFK == item.competitionId
                                               select x).FirstOrDefault();
                    double totalMark = item.q1MaxMarks + item.q2MaxMarks + item.q3MaxMarks + item.q4MaxMarks;
                    double q1Worth = (item.q1MaxMarks / totalMark) * 100;
                    double q2Worth = (item.q2MaxMarks / totalMark) * 100;
                    double q3Worth = (item.q3MaxMarks / totalMark) * 100;
                    double q4Worth = (item.q4MaxMarks / totalMark) * 100;

                    if (getCompetitorResult == null)
                    {
                        var newRow = new List<string>() { $"Session {item.sessionNo} (Total Marks = {0}/{totalMark})" };
                        dataGridView1.Rows.Add(newRow.ToArray());
                        if (item.q1MaxMarks != 0)
                        {
                            var questionRow = new List<string>() { "Question 1", 0.ToString(),
                            item.q1MaxMarks.ToString(), 0.ToString()};
                            dataGridView1.Rows.Add(questionRow.ToArray());
                        }
                        if (item.q2MaxMarks != 0)
                        {
                            var questionRow = new List<string>() { "Question 2", 0.ToString(),
                            item.q2MaxMarks.ToString(), 0.ToString()};
                            dataGridView1.Rows.Add(questionRow.ToArray());
                        }
                        if (item.q3MaxMarks != 0)
                        {
                            var questionRow = new List<string>() { "Question 3", 0.ToString(),
                            item.q3MaxMarks.ToString(), 0.ToString()};
                            dataGridView1.Rows.Add(questionRow.ToArray());
                        }
                        if (item.q4MaxMarks != 0)
                        {
                            var questionRow = new List<string>() { "Question 4", 0.ToString(),
                            item.q4MaxMarks.ToString(), 0.ToString()};
                            dataGridView1.Rows.Add(questionRow.ToArray());
                        }

                    }
                    else
                    {
                        var newRow = new List<string>() { $"Session {item.sessionNo} (Total Marks = {getCompetitorResult.totalMarks}/{totalMark})" };
                        dataGridView1.Rows.Add(newRow.ToArray());
                        if (item.q1MaxMarks != 0)
                        {
                            var questionRow = new List<string>() { "Question 1", getCompetitorResult.q1Marks.ToString(),
                            item.q1MaxMarks.ToString(),
                                Math.Round(getCompetitorResult.q1Marks / item.q1MaxMarks * q1Worth, 2).ToString()};
                            dataGridView1.Rows.Add(questionRow.ToArray());
                        }
                        if (item.q2MaxMarks != 0)
                        {
                            var questionRow = new List<string>() { "Question 2", getCompetitorResult.q2Marks.ToString(),
                            item.q2MaxMarks.ToString(),
                                Math.Round(getCompetitorResult.q2Marks / item.q2MaxMarks * q2Worth, 2).ToString()};
                            dataGridView1.Rows.Add(questionRow.ToArray());
                        }
                        if (item.q3MaxMarks != 0)
                        {
                            var questionRow = new List<string>() { "Question 3", getCompetitorResult.q3Marks.ToString(),
                            item.q3MaxMarks.ToString(), 
                                Math.Round(getCompetitorResult.q3Marks / item.q3MaxMarks * q3Worth, 2).ToString()};
                            dataGridView1.Rows.Add(questionRow.ToArray());
                        }
                        if (item.q4MaxMarks != 0)
                        {
                            var questionRow = new List<string>() { "Question 4", getCompetitorResult.q4Marks.ToString(),
                            item.q4MaxMarks.ToString(), 
                                Math.Round(getCompetitorResult.q4Marks / item.q4MaxMarks * q4Worth, 2).ToString()};
                            dataGridView1.Rows.Add(questionRow.ToArray());
                        }
                        if (getCompetitorResult.totalMarks > totalMark * 0.75)
                        {
                            bonus += 5;
                        }
                    }
                    

                }
                var getTotalCompetitorMarks = (from x in context.Results
                                               where x.Competitor.skillIdFK == getSkill.skillId && x.Competitor.competitorName == cbCompetitors.SelectedItem.ToString()
                                               select x.totalMarks).ToList().Sum();
                var medianMark = getSkill.expectedMedianMark * context.Competitions.Where(x => x.skillIdFK == getSkill.skillId).Count();
                if (getTotalCompetitorMarks > medianMark)
                {
                    bonus += 10;
                }
                var totalAmount = 0.0;
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    totalAmount += Convert.ToDouble(dataGridView1[3, item.Index].Value);
                }
                lblAmount.Text = (totalAmount + bonus).ToString();
                lblBonus.Text = bonus.ToString();
            }
        }
    }
}
