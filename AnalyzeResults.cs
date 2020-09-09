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
    public partial class AnalyzeResults : Form
    {
        public AnalyzeResults()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Hide();
            (new AdminMainMenu()).ShowDialog();
            Close();
        }

        private void AnalyzeResults_Load(object sender, EventArgs e)
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
            lblTotalMarks.Text = "Total Marks";
            lblEasiest.Text = "Session no (Marks range)";
            lblToughest.Text = "Session no (Marks range)";
            lblMedian.Text = "Median amrk";
            pbCountry.Image = null;
            pbUp.Visible = true;
            pbDown.Visible = false;
            chart1.Series.Clear();
            LoadData();
        }

        private void LoadData()
        {
            using (var context = new Session5Entities())
            {
                var getSkill = (from x in context.Skills
                                where x.skillName == cbSkill.SelectedItem.ToString()
                                select x).FirstOrDefault();
                var getBestCountry = (from x in context.Results
                                      where x.Competitor.skillIdFK == getSkill.skillId
                                      group x by x.Competitor.competitorCountry into y
                                      orderby y.Average(x => x.totalMarks) descending
                                      select y).FirstOrDefault();
                if (getBestCountry != null)
                {
                    pbCountry.Image = ReturnFlag(getBestCountry.Key);
                    lblTotalMarks.Text = getBestCountry.Average(x => x.totalMarks).ToString();
                    var getSessions = (from x in context.Results
                                       where x.Competitor.skillIdFK == getSkill.skillId
                                       group x by x.Competition.sessionNo into y
                                       orderby y.Sum(x => x.totalMarks) descending
                                       select y).ToList();
                    lblEasiest.Text = $"Session {getSessions.First().Key} ({getSessions.First().Min(x => x.totalMarks)}-{getSessions.First().Max(x => x.totalMarks)})";
                    lblToughest.Text = $"Session {getSessions.Last().Key} ({getSessions.Last().Min(x => x.totalMarks)}-{getSessions.Last().Max(x => x.totalMarks)})";
                    var getAllResults = (from x in context.Results
                                         where x.Competitor.skillIdFK == getSkill.skillId
                                         orderby x.totalMarks
                                         select x.totalMarks).ToList();
                    if (getAllResults.Count() % 2 != 0)
                    {
                        var medianPoint = getAllResults.Count() / 2;
                        lblMedian.Text = getAllResults[medianPoint].ToString();
                        if (getSkill.expectedMedianMark > getAllResults[medianPoint])
                        {
                            pbUp.Visible = false;
                        }
                        else
                        {
                            pbDown.Visible = false;
                        }
                    }
                    else
                    {
                        var medianPoint1 = getAllResults.Count() / 2;
                        var medianPoint2 = (getAllResults.Count() / 2) + 1;
                        var trueMedian = (getAllResults[medianPoint1] + getAllResults[medianPoint2]) / 2;
                        lblMedian.Text = trueMedian.ToString();
                        if (getSkill.expectedMedianMark > trueMedian)
                        {
                            pbUp.Visible = false;
                        }
                        else
                        {
                            pbDown.Visible = false;
                        }
                    }

                    var getCompetitorsResults = (from x in context.Results
                                                 where x.Competitor.skillIdFK == getSkill.skillId
                                                 group x by x.Competitor.competitorName into y
                                                 select y);
                    var getCompetitions = (from x in context.Competitions
                                           where x.skillIdFK == getSkill.skillId
                                           orderby x.sessionNo
                                           select x);
                    foreach (var competitor in getCompetitorsResults)
                    {
                        chart1.Series.Add(competitor.Key);
                        chart1.Series[competitor.Key].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                        foreach (var sessions in getCompetitions)
                        {
                            if (competitor.Where(x => x.competitionIdFK == sessions.competitionId).Select(x => x).FirstOrDefault() == null)
                            {
                                var idx = chart1.Series[competitor.Key].Points.AddXY($"Session {sessions.sessionNo}", 0);
                                chart1.Series[competitor.Key].Points[idx].BorderWidth = 3;

                            }
                            else
                            {
                                var idx = chart1.Series[competitor.Key].Points.AddXY($"Session {sessions.sessionNo}", competitor.Where(x => x.competitionIdFK == sessions.competitionId).Select(x => x.totalMarks).FirstOrDefault());
                                chart1.Series[competitor.Key].Points[idx].BorderWidth = 3;
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
    }
}
