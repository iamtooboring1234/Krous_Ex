using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class NotificationEntry : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                loadBranches();
                loadFaculty();
            }
        }

        protected void radNotificationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radNotificationType.SelectedValue == "1")
            {
                panelAll.Visible = true;
                panelStaff.Visible = false;
                panelStudent.Visible = false;
            }
            else if (radNotificationType.SelectedValue == "2")
            {
                panelAll.Visible = false;
                panelStaff.Visible = true;
                panelStudent.Visible = false;
            }
            else if (radNotificationType.SelectedValue == "3")
            {
                panelAll.Visible = false;
                panelStaff.Visible = false;
                panelStudent.Visible = true;
            }
        }

        private void loadBranches()
        {
            cbBranch.Items.Clear();

            try
            {
                ListItem oList = new ListItem();

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT * FROM Branches ORDER BY BranchesName", con);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);
                con.Close();

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    oList = new ListItem();
                    oList.Text = dt.Rows[i]["BranchesName"].ToString();
                    oList.Value = dt.Rows[i]["BranchesGUID"].ToString();
                    cbBranch.Items.Add(oList);
                }
            }

            catch (Exception)
            {
                clsFunction.DisplayAJAXMessage(this, "Error loading branches.");
            }
        }

        private void loadFaculty()
        {
            cbFaculty.Items.Clear();

            try
            {
                ListItem oList = new ListItem();

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT * FROM Faculty ORDER BY FacultyName", con);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);
                con.Close();

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    oList = new ListItem();
                    oList.Text = dt.Rows[i]["FacultyName"].ToString() + " (" + dt.Rows[i]["FacultyAbbrv"].ToString() + ")";
                    oList.Value = dt.Rows[i]["FacultyGUID"].ToString();
                    cbFaculty.Items.Add(oList);
                }
            }

            catch (Exception)
            {
                clsFunction.DisplayAJAXMessage(this, "Error loading faculty.");
            }
        }

        private void loadProgramme()
        {
            ddlProgramme.Items.Clear();

            try
            {
                ListItem oList = new ListItem();

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT * FROM Faculty ORDER BY FacultyName", con);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);
                con.Close();

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    oList = new ListItem();
                    oList.Text = dt.Rows[i]["FacultyName"].ToString() + " (" + dt.Rows[i]["FacultyAbbrv"].ToString() + ")";
                    oList.Value = dt.Rows[i]["FacultyGUID"].ToString();
                    ddlProgramme.Items.Add(oList);
                }
            }

            catch (Exception)
            {
                clsFunction.DisplayAJAXMessage(this, "Error loading faculty.");
            }
        }

        protected void cbBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isAllChecked = true;
            foreach (ListItem item in cbBranch.Items)
            {
                if (!item.Selected)
                {
                    isAllChecked = false;
                    break;
                }
            }

            cbBranchAll.Checked = isAllChecked;
        }

        protected void cbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isAllChecked = true;
            foreach (ListItem item in cbFaculty.Items)
            {
                if (!item.Selected)
                {
                    isAllChecked = false;
                    break;
                }
            }

            cbFacultyAll.Checked = isAllChecked;
        }

        protected void cbFacultyAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem item in cbFaculty.Items)
            {
                item.Selected = true;
            }
        }

        protected void cbBranchAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListItem item in cbBranch.Items)
            {
                item.Selected = true;
            }
        }
    }
}