using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TPQR_Session5_9_9
{
    public partial class AssignSeats : Form
    {
        public AssignSeats()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Hide();
            (new AdminMainMenu()).ShowDialog();
            Close();
        }

        private void AssignSeats_Load(object sender, EventArgs e)
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
            lbUnassigned.Items.Clear();
            lblAssigned.Text = 0.ToString();
            lblUnassigned.Text = 0.ToString();
            dataGridView1.Rows.Clear();
            using (var context = new Session5Entities())
            {
                var getSkill = (from x in context.Skills
                                where x.skillName == cbSkill.SelectedItem.ToString()
                                select x).FirstOrDefault();
                var c1 = 1;
                var c2 = 2;
                if (getSkill.noOfCompetitors % 2 == 0)
                {
                    for (int i = 0; i < getSkill.noOfCompetitors / 2; i++)
                    {
                        var newRow = new List<string>() { c1.ToString(), c2.ToString() };
                        dataGridView1.Rows.Add(newRow.ToArray());
                        c1 += 2;
                        c2 += 2;
                    }

                }
                else
                {
                    for (int i = 0; i < (getSkill.noOfCompetitors + 1) / 2; i++)
                    {
                        if (i == ((getSkill.noOfCompetitors + 1) / 2) - 1)
                        {
                            var newRow = new List<string>() { c1.ToString(), "" };
                            dataGridView1.Rows.Add(newRow.ToArray());
                        }
                        else
                        {
                            var newRow = new List<string>() { c1.ToString(), c2.ToString() };
                            dataGridView1.Rows.Add(newRow.ToArray());
                            c1 += 2;
                            c2 += 2;
                        }

                    }
                }
                var getUCompetitors = (from x in context.Competitors
                                       where x.assignedSeat == 0 && x.skillIdFK == getSkill.skillId
                                       select x);
                var getACompetitors = (from x in context.Competitors
                                       where x.assignedSeat != 0 && x.skillIdFK == getSkill.skillId
                                       select x);
                lblUnassigned.Text = getUCompetitors.Count().ToString();
                foreach (var item in getUCompetitors)
                {
                    lbUnassigned.Items.Add($"{item.competitorName}, {item.competitorCountry}");
                }
                lblAssigned.Text = getACompetitors.Count().ToString();
                foreach (var item in getACompetitors)
                {
                    var boolCheck = false;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        foreach (DataGridViewColumn cell in dataGridView1.Columns)
                        {
                            if (dataGridView1[cell.Index, row.Index].Value.ToString() == item.assignedSeat.ToString())
                            {
                                var sb = new StringBuilder(dataGridView1[cell.Index, row.Index].Value.ToString());
                                sb.Append($"\n{item.competitorId}");
                                dataGridView1[cell.Index, row.Index].Value = sb.ToString();
                                dataGridView1[cell.Index, row.Index].Style.BackColor = Color.Blue;
                                dataGridView1[cell.Index, row.Index].Style.ForeColor = Color.White;
                                boolCheck = true;
                            }
                            if (boolCheck)
                            {
                                break;
                            }
                        }
                        if (boolCheck)
                        {
                            break;
                        }
                    }
                }
            }
        }

        private void btnManual_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null || lbUnassigned.SelectedItem == null)
            {
                MessageBox.Show("Please select a seat and a competitor!");
            }
            else
            {
                if (dataGridView1.CurrentCell.Value.ToString() == "")
                {
                    MessageBox.Show("Unable to assign to a non-existent seat!");
                }
                else if (dataGridView1.CurrentCell.Style.BackColor == Color.Blue)
                {
                    var getCurrentID = dataGridView1.CurrentCell.Value.ToString().Split('\n')[1];
                    var getSeatNumber = int.Parse(dataGridView1.CurrentCell.Value.ToString().Split('\n')[0]);
                    using (var context = new Session5Entities())
                    {
                        var getSkill = (from x in context.Skills
                                        where x.skillName == cbSkill.SelectedItem.ToString()
                                        select x).FirstOrDefault();
                        var getAssigned = (from x in context.Competitors
                                           where x.competitorId == getCurrentID && x.skillIdFK == getSkill.skillId
                                           select x).FirstOrDefault();
                        var getToAssign = (from x in context.Competitors
                                           where lbUnassigned.SelectedItem.ToString().Contains(x.competitorName + ", " + x.competitorCountry) && x.skillIdFK == getSkill.skillId
                                           select x).FirstOrDefault();
                        var rowIndex = dataGridView1.CurrentCell.RowIndex;
                        var boolCheck = true;
                        if (rowIndex == dataGridView1.RowCount - 1)
                        {
                            var getAbove = dataGridView1[dataGridView1.CurrentCell.ColumnIndex, rowIndex - 1].Value.ToString();
                            var getAboveCountry = (from x in context.Competitors
                                                   where x.skillIdFK == getSkill.skillId && getAbove.Contains(x.competitorId)
                                                   select x.competitorCountry).FirstOrDefault();
                            if (getAboveCountry == getToAssign.competitorCountry)
                            {
                                boolCheck = false;
                            }
                        }
                        else if (rowIndex == 0)
                        {
                            var getBelow = dataGridView1[dataGridView1.CurrentCell.ColumnIndex, rowIndex + 1].Value.ToString();
                            var getBelowCountry = (from x in context.Competitors
                                                   where x.skillIdFK == getSkill.skillId && getBelow.Contains(x.competitorId)
                                                   select x.competitorCountry).FirstOrDefault();
                            if (getBelowCountry == getToAssign.competitorCountry)
                            {
                                boolCheck = false;
                            }
                        }
                        else
                        {
                            var getAbove = dataGridView1[dataGridView1.CurrentCell.ColumnIndex, rowIndex - 1].Value.ToString();
                            var getBelow = dataGridView1[dataGridView1.CurrentCell.ColumnIndex, rowIndex + 1].Value.ToString();
                            var getAboveCountry = (from x in context.Competitors
                                                   where x.skillIdFK == getSkill.skillId && getAbove.Contains(x.competitorId)
                                                   select x.competitorCountry).FirstOrDefault();
                            var getBelowCountry = (from x in context.Competitors
                                                   where x.skillIdFK == getSkill.skillId && getBelow.Contains(x.competitorId)
                                                   select x.competitorCountry).FirstOrDefault();
                            if (getAboveCountry == getToAssign.competitorCountry || getBelowCountry == getToAssign.competitorCountry)
                            {
                                boolCheck = false;
                            }
                        }
                        if (boolCheck)
                        {
                            var sb = new StringBuilder(getSeatNumber.ToString());
                            sb.Append($"\n{getToAssign.competitorId}");
                            dataGridView1.CurrentCell.Value = sb.ToString();
                            lbUnassigned.Items.Add($"{getAssigned.competitorName}, {getAssigned.competitorCountry}");
                            lbUnassigned.Items.Remove(lbUnassigned.SelectedItem);
                        }
                        else
                        {
                            MessageBox.Show("Unable to assign seat as front and/or back has a competitor of the same country!");
                        }
                    }
                }
                else
                {
                    var getSeatNumber = int.Parse(dataGridView1.CurrentCell.Value.ToString());
                    using (var context = new Session5Entities())
                    {
                        var getSkill = (from x in context.Skills
                                        where x.skillName == cbSkill.SelectedItem.ToString()
                                        select x).FirstOrDefault();
                        var getToAssign = (from x in context.Competitors
                                           where lbUnassigned.SelectedItem.ToString().Contains(x.competitorName + ", " + x.competitorCountry) && x.skillIdFK == getSkill.skillId
                                           select x).FirstOrDefault();
                        var rowIndex = dataGridView1.CurrentCell.RowIndex;
                        var boolCheck = true;
                        if (rowIndex == dataGridView1.RowCount - 1)
                        {
                            var getAbove = dataGridView1[dataGridView1.CurrentCell.ColumnIndex, rowIndex - 1].Value.ToString();
                            var getAboveCountry = (from x in context.Competitors
                                                   where x.skillIdFK == getSkill.skillId && getAbove.Contains(x.competitorId)
                                                   select x.competitorCountry).FirstOrDefault();
                            if (getAboveCountry == getToAssign.competitorCountry)
                            {
                                boolCheck = false;
                            }
                        }
                        else if (rowIndex == 0)
                        {
                            var getBelow = dataGridView1[dataGridView1.CurrentCell.ColumnIndex, rowIndex + 1].Value.ToString();
                            var getBelowCountry = (from x in context.Competitors
                                                   where x.skillIdFK == getSkill.skillId && getBelow.Contains(x.competitorId)
                                                   select x.competitorCountry).FirstOrDefault();
                            if (getBelowCountry == getToAssign.competitorCountry)
                            {
                                boolCheck = false;
                            }
                        }
                        else
                        {
                            var getAbove = dataGridView1[dataGridView1.CurrentCell.ColumnIndex, rowIndex - 1].Value.ToString();
                            var getBelow = dataGridView1[dataGridView1.CurrentCell.ColumnIndex, rowIndex + 1].Value.ToString();
                            var getAboveCountry = (from x in context.Competitors
                                                   where x.skillIdFK == getSkill.skillId && getAbove.Contains(x.competitorId)
                                                   select x.competitorCountry).FirstOrDefault();
                            var getBelowCountry = (from x in context.Competitors
                                                   where x.skillIdFK == getSkill.skillId && getBelow.Contains(x.competitorId)
                                                   select x.competitorCountry).FirstOrDefault();
                            if (getAboveCountry == getToAssign.competitorCountry || getBelowCountry == getToAssign.competitorCountry)
                            {
                                boolCheck = false;
                            }
                        }
                        if (boolCheck)
                        {
                            var sb = new StringBuilder(getSeatNumber.ToString());
                            sb.Append($"\n{getToAssign.competitorId}");
                            dataGridView1.CurrentCell.Value = sb.ToString();
                            dataGridView1.CurrentCell.Style.BackColor = Color.Blue;
                            dataGridView1.CurrentCell.Style.ForeColor = Color.White;
                            lbUnassigned.Items.Remove(lbUnassigned.SelectedItem);
                            lblAssigned.Text = (int.Parse(lblAssigned.Text) + 1).ToString();
                            lblUnassigned.Text = (int.Parse(lblUnassigned.Text) - 1).ToString();
                        }
                        else
                        {
                            MessageBox.Show("Unable to assign seat as front and/or back has a competitor of the same country!");
                        }
                    }
                }
            }
        }

        private void btnSwap_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count != 2)
            {
                MessageBox.Show("Please select 2 seats to swap!");
            }
            else
            {
                var checkSeats = true;
                foreach (DataGridViewCell item in dataGridView1.SelectedCells)
                {
                    if (item.Style.BackColor != Color.Blue)
                    {
                        checkSeats = false;
                    }
                }
                if (checkSeats)
                {
                    var pos = new Dictionary<DataGridViewCell, string>();
                    foreach (DataGridViewCell item in dataGridView1.SelectedCells)
                    {
                        pos.Add(item, item.Value.ToString().Split('\n')[1]);
                    }
                    var competitor2ID = pos.ElementAt(1).Value;
                    var competitor1ID = pos.ElementAt(0).Value;
                    using (var context = new Session5Entities())
                    {
                        var getSkill = (from x in context.Skills
                                        where x.skillName == cbSkill.SelectedItem.ToString()
                                        select x).FirstOrDefault();
                        var check2NewRowIndex = pos.ElementAt(0).Key.RowIndex;
                        var check1NewRowIndex = pos.ElementAt(1).Key.RowIndex;
                        var get2Country = (from x in context.Competitors
                                           where x.competitorId == competitor2ID && x.skillIdFK == getSkill.skillId
                                           select x.competitorCountry).FirstOrDefault();
                        var get1Country = (from x in context.Competitors
                                           where x.competitorId == competitor1ID && x.skillIdFK == getSkill.skillId
                                           select x.competitorCountry).FirstOrDefault();
                        var boolCheck = true;
                        if (check2NewRowIndex == dataGridView1.RowCount - 1)
                        {
                            var getAbove = dataGridView1[pos.ElementAt(0).Key.ColumnIndex, check2NewRowIndex - 1].Value.ToString();
                            var getAboveCountry = (from x in context.Competitors
                                                   where x.skillIdFK == getSkill.skillId && getAbove.Contains(x.competitorId)
                                                   select x.competitorCountry).FirstOrDefault();
                            if (getAboveCountry == get2Country)
                            {
                                boolCheck = false;
                            }
                        }
                        else if (check2NewRowIndex == 0)
                        {
                            var getBelow = dataGridView1[pos.ElementAt(0).Key.ColumnIndex, check2NewRowIndex + 1].Value.ToString();
                            var getBelowCountry = (from x in context.Competitors
                                                   where x.skillIdFK == getSkill.skillId && getBelow.Contains(x.competitorId)
                                                   select x.competitorCountry).FirstOrDefault();
                            if (getBelowCountry == get2Country)
                            {
                                boolCheck = false;
                            }
                        }
                        else
                        {
                            var getAbove = dataGridView1[pos.ElementAt(0).Key.ColumnIndex, check2NewRowIndex - 1].Value.ToString();
                            var getBelow = dataGridView1[pos.ElementAt(0).Key.ColumnIndex, check2NewRowIndex + 1].Value.ToString();
                            var getAboveCountry = (from x in context.Competitors
                                                   where x.skillIdFK == getSkill.skillId && getAbove.Contains(x.competitorId)
                                                   select x.competitorCountry).FirstOrDefault();
                            var getBelowCountry = (from x in context.Competitors
                                                   where x.skillIdFK == getSkill.skillId && getBelow.Contains(x.competitorId)
                                                   select x.competitorCountry).FirstOrDefault();
                            if (getAboveCountry == get2Country || getBelowCountry == get2Country)
                            {
                                boolCheck = false;
                            }
                        }
                        if (check1NewRowIndex == dataGridView1.RowCount - 1)
                        {
                            var getAbove = dataGridView1[pos.ElementAt(1).Key.ColumnIndex, check1NewRowIndex - 1].Value.ToString();
                            var getAboveCountry = (from x in context.Competitors
                                                   where x.skillIdFK == getSkill.skillId && getAbove.Contains(x.competitorId)
                                                   select x.competitorCountry).FirstOrDefault();
                            if (getAboveCountry == get1Country)
                            {
                                boolCheck = false;
                            }
                        }
                        else if (check1NewRowIndex == 0)
                        {
                            var getBelow = dataGridView1[pos.ElementAt(1).Key.ColumnIndex, check1NewRowIndex + 1].Value.ToString();
                            var getBelowCountry = (from x in context.Competitors
                                                   where x.skillIdFK == getSkill.skillId && getBelow.Contains(x.competitorId)
                                                   select x.competitorCountry).FirstOrDefault();
                            if (getBelowCountry == get1Country)
                            {
                                boolCheck = false;
                            }
                        }
                        else
                        {
                            var getAbove = dataGridView1[pos.ElementAt(1).Key.ColumnIndex, check1NewRowIndex - 1].Value.ToString();
                            var getBelow = dataGridView1[pos.ElementAt(1).Key.ColumnIndex, check1NewRowIndex + 1].Value.ToString();
                            var getAboveCountry = (from x in context.Competitors
                                                   where x.skillIdFK == getSkill.skillId && getAbove.Contains(x.competitorId)
                                                   select x.competitorCountry).FirstOrDefault();
                            var getBelowCountry = (from x in context.Competitors
                                                   where x.skillIdFK == getSkill.skillId && getBelow.Contains(x.competitorId)
                                                   select x.competitorCountry).FirstOrDefault();
                            if (getAboveCountry == get1Country || getBelowCountry == get1Country)
                            {
                                boolCheck = false;
                            }
                        }
                        if (boolCheck)
                        {
                            var sb1 = new StringBuilder(pos.ElementAt(1).Key.Value.ToString().Split('\n')[0]);
                            var sb2 = new StringBuilder(pos.ElementAt(0).Key.Value.ToString().Split('\n')[0]);
                            sb1.Append($"\n{pos.ElementAt(0).Value}");
                            sb2.Append($"\n{pos.ElementAt(1).Value}");
                            dataGridView1[pos.ElementAt(0).Key.ColumnIndex, pos.ElementAt(0).Key.RowIndex].Value = sb2.ToString();
                            dataGridView1[pos.ElementAt(1).Key.ColumnIndex, pos.ElementAt(1).Key.RowIndex].Value = sb1.ToString();
                        }
                        else
                        {
                            MessageBox.Show("Unable to swap seats as one of the competitors will be in front or behind of one of their country's competitor!");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Seats must first be occupied before swap!");
                }


            }
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            var boolCheck = random();
            if (!boolCheck)
            {
                random();
            }
        }

        private bool random()
        {
            var unassignedSeats = new List<string>();
            var listToRemove = new List<string>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {

                foreach (DataGridViewColumn cell in dataGridView1.Columns)
                {
                    if (dataGridView1[cell.Index, row.Index].Style.BackColor != Color.Blue)
                    {
                        unassignedSeats.Add(dataGridView1[cell.Index, row.Index].Value.ToString());
                    }
                }
            }
            var rand = new Random();
            if (lbUnassigned.Items.Count > unassignedSeats.Count)
            {
                MessageBox.Show("Illegal amount of competitors!");
                return true;
            }
            else
            {

                using (var context = new Session5Entities())
                {
                    var getSkill = (from x in context.Skills
                                    where x.skillName == cbSkill.SelectedItem.ToString()
                                    select x).FirstOrDefault();
                    foreach (var item in lbUnassigned.Items)
                    {
                        var randIndex = rand.Next(0, unassignedSeats.Count - 1);
                        var getSeat = unassignedSeats[randIndex];
                        var getToAssign = (from x in context.Competitors
                                           where item.ToString().Contains(x.competitorName + ", " + x.competitorCountry) && x.skillIdFK == getSkill.skillId
                                           select x).FirstOrDefault();
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            foreach (DataGridViewColumn cell in dataGridView1.Columns)
                            {
                                var boolCheck = true;
                                if (dataGridView1[cell.Index, row.Index].Value.ToString() == getSeat)
                                {
                                    if (row.Index == dataGridView1.RowCount - 1)
                                    {
                                        var getAbove = dataGridView1[cell.Index, row.Index - 1].Value.ToString();
                                        var getAboveCountry = (from x in context.Competitors
                                                               where x.skillIdFK == getSkill.skillId && getAbove.Contains(x.competitorId)
                                                               select x.competitorCountry).FirstOrDefault();
                                        if (getAboveCountry == getToAssign.competitorCountry)
                                        {
                                            boolCheck = false;
                                        }
                                    }
                                    else if (row.Index == 0)
                                    {
                                        var getBelow = dataGridView1[cell.Index, row.Index + 1].Value.ToString();
                                        var getBelowCountry = (from x in context.Competitors
                                                               where x.skillIdFK == getSkill.skillId && getBelow.Contains(x.competitorId)
                                                               select x.competitorCountry).FirstOrDefault();
                                        if (getBelowCountry == getToAssign.competitorCountry)
                                        {
                                            boolCheck = false;
                                        }
                                    }
                                    else
                                    {
                                        var getAbove = dataGridView1[cell.Index, row.Index - 1].Value.ToString();
                                        var getBelow = dataGridView1[cell.Index, row.Index + 1].Value.ToString();
                                        var getAboveCountry = (from x in context.Competitors
                                                               where x.skillIdFK == getSkill.skillId && getAbove.Contains(x.competitorId)
                                                               select x.competitorCountry).FirstOrDefault();
                                        var getBelowCountry = (from x in context.Competitors
                                                               where x.skillIdFK == getSkill.skillId && getBelow.Contains(x.competitorId)
                                                               select x.competitorCountry).FirstOrDefault();
                                        if (getAboveCountry == getToAssign.competitorCountry || getBelowCountry == getToAssign.competitorCountry)
                                        {
                                            boolCheck = false;
                                        }
                                    }
                                    if (boolCheck)
                                    {
                                        var sb = new StringBuilder(getSeat);
                                        sb.Append($"\n{getToAssign.competitorId}");
                                        dataGridView1[cell.Index, row.Index].Value = sb.ToString();
                                        listToRemove.Add(item.ToString());
                                        unassignedSeats.Remove(getSeat);
                                        dataGridView1[cell.Index, row.Index].Style.BackColor = Color.Blue;
                                        dataGridView1[cell.Index, row.Index].Style.ForeColor = Color.White;
                                        lblAssigned.Text = (int.Parse(lblAssigned.Text) + 1).ToString();
                                        lblUnassigned.Text = (int.Parse(lblUnassigned.Text) - 1).ToString();
                                    }
                                }
                            }
                        }
                    }
                }

            }
            foreach (var item in listToRemove)
            {
                lbUnassigned.Items.Remove(item);
            }
            if (lbUnassigned.Items.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            using (var context = new Session5Entities())
            {
                if (dataGridView1[e.ColumnIndex, e.RowIndex].Style.BackColor == Color.Blue)
                {
                    var getSkill = (from x in context.Skills
                                    where x.skillName == cbSkill.SelectedItem.ToString()
                                    select x).FirstOrDefault();
                    var ID = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString().Split('\n')[1];
                    var getID = (from x in context.Competitors
                                 where x.skillIdFK == getSkill.skillId && x.competitorId == ID
                                 select x).FirstOrDefault();
                    if (getID != null)
                    {
                        dataGridView1[e.ColumnIndex, e.RowIndex].ToolTipText = $"{getID.competitorName}, {getID.competitorCountry}";
                    }
                   
                }

            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            using (var context = new Session5Entities())
            {
                var getSkill = (from x in context.Skills
                                where x.skillName == cbSkill.SelectedItem.ToString()
                                select x).FirstOrDefault();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    foreach (DataGridViewColumn cell in dataGridView1.Columns)
                    {
                        if (dataGridView1[cell.Index, row.Index].Style.BackColor == Color.Blue)
                        {
                            var seat = int.Parse(dataGridView1[cell.Index, row.Index].Value.ToString().Split('\n')[0]);
                            var competitorID = dataGridView1[cell.Index, row.Index].Value.ToString().Split('\n')[1];
                            var getCompetitor = (from x in context.Competitors
                                                 where x.skillIdFK == getSkill.skillId && x.competitorId == competitorID
                                                 select x).FirstOrDefault();
                            getCompetitor.assignedSeat = seat;

                        }
                    }
                }

                foreach (var item in lbUnassigned.Items)
                {
                    var getCompetitor = (from x in context.Competitors
                                         where x.skillIdFK == getSkill.skillId && item.ToString().Contains(x.competitorName + ", " + x.competitorCountry)
                                         select x).FirstOrDefault();
                    getCompetitor.assignedSeat = 0;
                }
                context.SaveChanges();
                MessageBox.Show("Completed seat assignment!");
            }
        }
    }
}
