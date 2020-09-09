using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TPQR_Session5_9_9.Properties;

namespace TPQR_Session5_9_9
{
    public partial class ViewResults : Form
    {
        List<string> moreGold = new List<string>();
        List<double> moreGoldResults = new List<double>();
        List<string> moreSilver = new List<string>();
        List<double> moreSilverResults = new List<double>();
        List<string> moreBronze = new List<string>();
        List<double> moreBronzeResults = new List<double>();
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
            btnGold.Visible = false;
            btnSilver.Visible = false;
            btnBronze.Visible = false;
            pbGold1.Image = null;
            pbGold2.Image = null;
            pbSilver1.Image = null;
            pbSilver2.Image = null;
            pbBronze1.Image = null;
            pbBronze2.Image = null;
            moreGold.Clear();
            moreGoldResults.Clear();
            moreSilver.Clear();
            moreSilverResults.Clear();
            moreBronze.Clear();
            moreBronzeResults.Clear();
            lblCompletedSessions.Text = 0.ToString();
            lblTotalSession.Text = 0.ToString();
            using (var context = new Session5Entities())
            {
                var getSkill = (from x in context.Skills
                                where x.skillName == cbSkill.SelectedItem.ToString()
                                select x).FirstOrDefault();
                var getTotalSessions = (from x in context.Competitions
                                        where x.skillIdFK == getSkill.skillId
                                        select x);
                lblTotalSession.Text = getTotalSessions.Count().ToString();
                var completed = 0;
                var totalMarks = Convert.ToDouble(getTotalSessions.Sum(x => x.q1MaxMarks + x.q2MaxMarks + x.q3MaxMarks + x.q4MaxMarks));
                foreach (var item in getTotalSessions)
                {
                    var getTotalResults = (from x in context.Results
                                           where x.competitionIdFK == item.competitionId
                                           select x).Count();
                    if (getTotalResults == getSkill.noOfCompetitors)
                    {
                        completed += 1;
                    }

                }
                lblCompletedSessions.Text = completed.ToString();

                var getResults = (from x in context.Results
                                  where x.Competition.skillIdFK == getSkill.skillId
                                  group x by x.Competitor.competitorName into y
                                  orderby y.Key
                                  orderby y.Sum(x => x.totalMarks) descending
                                  select y);
                foreach (var item in getResults)
                {
                    var newRow = new List<string>() { item.Key,
                        context.Competitors.Where(x => x.competitorName == item.Key && x.skillIdFK == getSkill.skillId).Select(x => x.competitorCountry).FirstOrDefault(),
                    item.Sum(x => x.totalMarks).ToString()};
                    dataGridView1.Rows.Add(newRow.ToArray());
                }
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    if (Convert.ToDouble(dataGridView1[2, item.Index].Value) > 0.8 * totalMarks)
                    {
                        if (moreGoldResults.Count == 0)
                        {
                            pbGold1.Image = ReturnFlag(dataGridView1[1, item.Index].Value.ToString());
                            moreGold.Add(dataGridView1[1, item.Index].Value.ToString());
                            moreGoldResults.Add(Convert.ToDouble(dataGridView1[2, item.Index].Value));
                        }
                        else if (moreGoldResults.Count > 0 && moreGoldResults[0] - Convert.ToDouble(dataGridView1[2, item.Index].Value) <= moreGoldResults.Count * 2)
                        {
                            if (moreGoldResults.Count == 1)
                            {
                                pbGold2.Image = ReturnFlag(dataGridView1[1, item.Index].Value.ToString());
                                moreGold.Add(dataGridView1[1, item.Index].Value.ToString());
                                moreGoldResults.Add(Convert.ToDouble(dataGridView1[2, item.Index].Value));
                            }
                            else
                            {
                                moreGold.Add(dataGridView1[1, item.Index].Value.ToString());
                                moreGoldResults.Add(Convert.ToDouble(dataGridView1[2, item.Index].Value));
                                btnGold.Visible = true;

                            }
                        }
                        else
                        {
                            if (Convert.ToDouble(dataGridView1[2, item.Index].Value) > 0.75 * totalMarks)
                            {
                                if (moreSilverResults.Count == 0)
                                {
                                    pbSilver1.Image = ReturnFlag(dataGridView1[1, item.Index].Value.ToString());
                                    moreSilver.Add(dataGridView1[1, item.Index].Value.ToString());
                                    moreSilverResults.Add(Convert.ToDouble(dataGridView1[2, item.Index].Value));
                                }
                                else if (moreSilverResults.Count > 0 && moreSilverResults[0] - Convert.ToDouble(dataGridView1[2, item.Index].Value) <= moreGoldResults.Count * 2)
                                {
                                    if (moreSilverResults.Count == 1)
                                    {
                                        pbSilver2.Image = ReturnFlag(dataGridView1[1, item.Index].Value.ToString());
                                        moreSilver.Add(dataGridView1[1, item.Index].Value.ToString());
                                        moreSilverResults.Add(Convert.ToDouble(dataGridView1[2, item.Index].Value));
                                    }
                                    else
                                    {
                                        moreSilver.Add(dataGridView1[1, item.Index].Value.ToString());
                                        moreSilverResults.Add(Convert.ToDouble(dataGridView1[2, item.Index].Value));
                                        btnSilver.Visible = true;
                                    }
                                }
                                else
                                {
                                    if (Convert.ToDouble(dataGridView1[2, item.Index].Value) > 0.71 * totalMarks)
                                    {
                                        if (moreBronzeResults.Count == 0)
                                        {
                                            pbBronze1.Image = ReturnFlag(dataGridView1[1, item.Index].Value.ToString());
                                            moreBronze.Add(dataGridView1[1, item.Index].Value.ToString());
                                            moreBronzeResults.Add(Convert.ToDouble(dataGridView1[2, item.Index].Value));
                                        }
                                        else if (moreBronzeResults.Count > 0 && moreBronzeResults[0] - Convert.ToDouble(dataGridView1[2, item.Index].Value) <= moreGoldResults.Count * 2)
                                        {
                                            if (moreBronzeResults.Count == 1)
                                            {
                                                pbBronze2.Image = ReturnFlag(dataGridView1[1, item.Index].Value.ToString());
                                                moreBronze.Add(dataGridView1[1, item.Index].Value.ToString());
                                                moreBronzeResults.Add(Convert.ToDouble(dataGridView1[2, item.Index].Value));
                                            }
                                            else
                                            {
                                                moreBronze.Add(dataGridView1[1, item.Index].Value.ToString());
                                                moreBronzeResults.Add(Convert.ToDouble(dataGridView1[2, item.Index].Value));
                                                btnBronze.Visible = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    else if (Convert.ToDouble(dataGridView1[2, item.Index].Value) > 0.75 * totalMarks)
                    {
                        if (moreSilverResults.Count == 0)
                        {
                            pbSilver1.Image = ReturnFlag(dataGridView1[1, item.Index].Value.ToString());
                            moreSilver.Add(dataGridView1[1, item.Index].Value.ToString());
                            moreSilverResults.Add(Convert.ToDouble(dataGridView1[2, item.Index].Value));
                        }
                        else if (moreSilverResults.Count > 0 && moreSilverResults[0] - Convert.ToDouble(dataGridView1[2, item.Index].Value) <= moreGoldResults.Count * 2)
                        {
                            if (moreSilverResults.Count == 1)
                            {
                                pbSilver2.Image = ReturnFlag(dataGridView1[1, item.Index].Value.ToString());
                                moreSilver.Add(dataGridView1[1, item.Index].Value.ToString());
                                moreSilverResults.Add(Convert.ToDouble(dataGridView1[2, item.Index].Value));
                            }
                            else
                            {
                                moreSilver.Add(dataGridView1[1, item.Index].Value.ToString());
                                moreSilverResults.Add(Convert.ToDouble(dataGridView1[2, item.Index].Value));
                                btnSilver.Visible = true;
                            }
                        }
                        else
                        {
                            if (Convert.ToDouble(dataGridView1[2, item.Index].Value) > 0.71 * totalMarks)
                            {
                                if (moreBronzeResults.Count == 0)
                                {
                                    pbBronze1.Image = ReturnFlag(dataGridView1[1, item.Index].Value.ToString());
                                    moreBronze.Add(dataGridView1[1, item.Index].Value.ToString());
                                    moreBronzeResults.Add(Convert.ToDouble(dataGridView1[2, item.Index].Value));
                                }
                                else if (moreBronzeResults.Count > 0 && moreBronzeResults[0] - Convert.ToDouble(dataGridView1[2, item.Index].Value) <= moreGoldResults.Count * 2)
                                {
                                    if (moreBronzeResults.Count == 1)
                                    {
                                        pbBronze2.Image = ReturnFlag(dataGridView1[1, item.Index].Value.ToString());
                                        moreBronze.Add(dataGridView1[1, item.Index].Value.ToString());
                                        moreBronzeResults.Add(Convert.ToDouble(dataGridView1[2, item.Index].Value));
                                    }
                                    else
                                    {
                                        moreBronze.Add(dataGridView1[1, item.Index].Value.ToString());
                                        moreBronzeResults.Add(Convert.ToDouble(dataGridView1[2, item.Index].Value));
                                        btnBronze.Visible = true;
                                    }
                                }
                            }
                        }

                    }
                    else if (Convert.ToDouble(dataGridView1[2, item.Index].Value) > 0.71 * totalMarks)
                    {
                        if (moreBronzeResults.Count == 0)
                        {
                            pbBronze1.Image = ReturnFlag(dataGridView1[1, item.Index].Value.ToString());
                            moreBronze.Add(dataGridView1[1, item.Index].Value.ToString());
                            moreBronzeResults.Add(Convert.ToDouble(dataGridView1[2, item.Index].Value));
                        }
                        else if (moreBronzeResults.Count > 0 && moreBronzeResults[0] - Convert.ToDouble(dataGridView1[2, item.Index].Value) <= moreGoldResults.Count * 2)
                        {
                            if (moreBronzeResults.Count == 1)
                            {
                                pbBronze2.Image = ReturnFlag(dataGridView1[1, item.Index].Value.ToString());
                                moreBronze.Add(dataGridView1[1, item.Index].Value.ToString());
                                moreBronzeResults.Add(Convert.ToDouble(dataGridView1[2, item.Index].Value));
                            }
                            else
                            {
                                moreBronze.Add(dataGridView1[1, item.Index].Value.ToString());
                                moreBronzeResults.Add(Convert.ToDouble(dataGridView1[2, item.Index].Value));
                                btnBronze.Visible = true;
                            }
                        }
                    }



                }
            }

        }
        private Bitmap ReturnFlag(string countryName)
        {
            var countryLower = countryName.ToLower();
            switch (countryLower)
            {
                case "australia":
                    return Resources.australia_flg;
                case "brunei":
                    return Resources.brunei_flag;
                case "china":
                    return Resources.chinaflag;
                case "cambodia":
                    return Resources.flag_cambodia;
                case "russia":
                    return Resources.flag_russia;
                case "malaysia":
                    return Resources.flagmalaysia;
                case "india":
                    return Resources.flg_india;
                case "philippines":
                    return Resources.flg_philippine1;
                case "qatar":
                    return Resources.flg_qatar;
                case "thailand":
                    return Resources.flg_thailand;
                case "vietnam":
                    return Resources.flg_vietnam_new;
                case "indonesia":
                    return Resources.indonesia2;
                case "laos":
                    return Resources.laos_newflg;
                case "maldives":
                    return Resources.maldivesfg;
                case "myanmar":
                    return Resources.myanmar3;
                case "new zealand":
                    return Resources.newzealand_flg5;
                case "old greece":
                    return Resources.old_greece;
                case "singapore":
                    return Resources.singapore_flag1;
                case "south korea":
                    return Resources.southkorea_flag_new;
                case "switzerland":
                    return Resources.switzerland_old;
                default:
                    return null;
            }
        }

        private void btnGold_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"All Gold Countries: {string.Join(",", moreGold)}", "All Gold");
        }

        private void btnSilver_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"All Silver Countries: {string.Join(",", moreSilver)}", "All Silver");
        }

        private void btnBronze_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"All Bronze Countries: {string.Join(",", moreBronze)}", "All Bronze");
        }
    }
}
