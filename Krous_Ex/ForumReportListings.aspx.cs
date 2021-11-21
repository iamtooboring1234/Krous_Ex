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
    public partial class ForumReportListings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                if (Session["ApprovedReport"] != null)
                {
                    if (Session["ApprovedReport"].ToString() == "Yes")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showForumReportApprovedSuccessfully(); ", true);
                        Session["ApprovedReport"] = null;
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showForumReportApprovedUnsuccessfully(); ", true);
                        Session["RejectedReport"] = null;
                    }
                }

                if (Session["RejectedReport"] != null)
                {
                    if (Session["RejectedReport"].ToString() == "Yes")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showForumReportRejectedSuccesfully(); ", true);
                        Session["RejectedReport"] = null;
                    } else
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showForumReportRejectedUnsuccesfully(); ", true);
                        Session["RejectedReport"] = null;
                    }
                }

                loadGV();
                loadFAQReason();
            }
        }

        private void loadGV()
        {
            try
            {
                string sqlQuery = "SELECT F.ForumReportGUID, R.ReplyContent, R.ReplyBy, F.ReportReason, F.ReportBy, F.ReportStatus FROM ForumReport F JOIN Replies R On F.ReplyGUID = R.ReplyGUID WHERE ReportStatus='In Progress' ";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);
                
                SqlDataReader reader = GetCommand.ExecuteReader();
                DataTable dtForumReport = new DataTable();
                dtForumReport.Load(reader);
                con.Close();

                if (dtForumReport.Rows.Count != 0)
                {
                    gvForumReport.DataSource = dtForumReport;
                    gvForumReport.DataBind();
                    gvForumReport.Visible = true;
                    lblNoData.Visible = false;
                    panelButton.Visible = true;
                }
                else
                {
                    lblNoData.Visible = true;
                    gvForumReport.Visible = false;
                    panelButton.Visible = false;
                }
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        private void loadFAQReason()
        {
            try
            {
                ddlReportReason.Items.Clear();

                ListItem oList = new ListItem();


                oList = new ListItem();
                oList.Text = "";
                oList.Value = "";
                ddlReportReason.Items.Add(oList);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT ReportReason FROM ForumReport GROUP BY ReportReason ORDER BY ReportReason", con);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtFAQ = new DataTable();
                dtFAQ.Load(reader);
                con.Close();

                for (int i = 0; i <= dtFAQ.Rows.Count - 1; i++)
                {
                    oList = new ListItem();
                    oList.Text = dtFAQ.Rows[i]["ReportReason"].ToString();
                    oList.Value = dtFAQ.Rows[i]["ReportReason"].ToString();
                    ddlReportReason.Items.Add(oList);
                }
            }

            catch (Exception)
            {
                clsFunction.DisplayAJAXMessage(this, "Error, unable to load forum reason.");
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvForumReport.Rows) //Running all lines of grid
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);

                    if (chkRow.Checked)
                    {
                        try
                        {
                            Guid ForumReportGUID = Guid.Parse(gvForumReport.DataKeys[row.RowIndex].Value.ToString());

                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                            con.Open();

                            SqlCommand updateCommand = new SqlCommand("UPDATE ForumReport SET ReportStatus = 'Approved' WHERE ForumReportGUID = @ForumReportGUID ", con);

                            updateCommand.Parameters.AddWithValue("@ForumReportGUID", ForumReportGUID);

                            updateCommand.ExecuteNonQuery();

                            con.Close();

                            string sqlQuery = "SELECT * FROM ForumReport WHERE ForumReportGUID = @ForumReportGUID ";

                            con.Open();

                            SqlCommand GetCommand = new SqlCommand(sqlQuery, con);
                            GetCommand.Parameters.AddWithValue("@ForumReportGUID", ForumReportGUID);
                            SqlDataReader reader = GetCommand.ExecuteReader();
                            DataTable dtReply = new DataTable();
                            dtReply.Load(reader);
                            con.Close();

                            if (dtReply.Rows.Count != 0)
                            {
                                con.Open();

                                updateCommand = new SqlCommand("DELETE FROM Replies WHERE ReplyGUID = @ReplyGUID ", con);

                                updateCommand.Parameters.AddWithValue("@ReplyGUID", dtReply.Rows[0]["ReplyGUID"]);

                                updateCommand.ExecuteNonQuery();

                                con.Close();
                            }

                            Session["ApprovedReport"] = "Yes";
                        }
                        catch (Exception ex)
                        {
                            Session["ApprovedReport"] = "No";
                            clsFunction.DisplayAJAXMessage(this, "Unable to update.");
                            Response.Write(ex);
                        }

                        Response.Redirect("ForumReportListings", true);
                    }
                    else
                    {
                        clsFunction.DisplayAJAXMessage(this, "Please checked at least ONE checkbox.");
                    }
                }
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvForumReport.Rows) //Running all lines of grid
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = (row.Cells[0].FindControl("chkRow") as CheckBox);

                    if (chkRow.Checked)
                    {
                        try
                        {
                            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                            con.Open();

                            SqlCommand updateCommand = new SqlCommand("UPDATE ForumReport SET ReportStatus = 'Rejected' WHERE ForumReportGUID = @ForumReportGUID ", con);

                            updateCommand.Parameters.AddWithValue("@ForumReportGUID", Guid.Parse(gvForumReport.DataKeys[row.RowIndex].Value.ToString()));

                            updateCommand.ExecuteNonQuery();

                            con.Close();

                            Session["RejectedReport"] = "Yes";
                        }
                        catch (Exception ex)
                        {
                            Session["RejectedReport"] = "No";
                            clsFunction.DisplayAJAXMessage(this, "Unable to update.");
                            Response.Write(ex);
                        }

                        Response.Redirect("ForumReportListings", true);
                    }
                    else
                    {
                        clsFunction.DisplayAJAXMessage(this, "Please checked at least ONE checkbox.");
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlQuery = "";

                sqlQuery = "SELECT F.ForumReportGUID, F.ReplyContent, F.ReplyBy, F.ReportReason, F.ReportBy, F.ReportStatus FROM ForumReport F LEFT JOIN Replies R On F.ReplyGUID = R.ReplyGUID ";
                sqlQuery += "WHERE CASE WHEN @ReportReason = '' then '' ELSE ReportReason END = @ReportReason AND ";
                sqlQuery += "CASE WHEN @ReportStatus = '' then @ReportStatus ELSE ReportStatus END = @ReportStatus ";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);

                GetCommand.Parameters.AddWithValue("@ReportReason", ddlReportReason.SelectedValue);
                GetCommand.Parameters.AddWithValue("@ReportStatus", ddlReportStatus.SelectedValue);

                SqlDataReader reader = GetCommand.ExecuteReader();
                DataTable dtForumReport = new DataTable();
                dtForumReport.Load(reader);
                con.Close();

                if (dtForumReport.Rows.Count != 0)
                {
                    gvForumReport.DataSource = dtForumReport;
                    gvForumReport.DataBind();
                    gvForumReport.Visible = true;
                    lblNoData.Visible = false;
                    panelButton.Visible = true;
                }
                else
                {
                    lblNoData.Visible = true;
                    gvForumReport.Visible = false;
                    panelButton.Visible = false;
                }
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        protected void gvForumReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkRow = e.Row.Cells[0].FindControl("chkRow") as CheckBox;

                    if (gvForumReport.DataSource != null)
                    {
                        string test = e.Row.Cells[6].Text;

                        if (e.Row.Cells[6].Text == "Approved")
                        {
                            chkRow.Visible = false;
                        } else if (e.Row.Cells[6].Text == "Rejected")
                        {
                            chkRow.Visible = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }
    }
}