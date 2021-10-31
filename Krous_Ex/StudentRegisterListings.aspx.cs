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
    public partial class StudentRegisterListings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UpdateStatus"] != null)
            {
                if (Session["UpdateStatus"].ToString() == "Yes")
                {
                    clsFunction.DisplayAJAXMessage(this, "Hi");
                    Session["UpdateStatus"] = null;
                }
            }

            if (IsPostBack != true)
            {

                loadStudListGV();

            }
        }

        protected bool updateStatus()
        {
            Label lblStatus = new Label();
            Label lblRegisterGUID = new Label();
            DropDownList ddlStatus = new DropDownList();
            int updateCount = int.Parse(lblUpdate.Text);

            try
            {
                foreach (GridViewRow gvr in gvCourse.Rows)
                {
                    if (updateCount == 0)
                    {
                        break;
                    }

                    lblStatus = gvr.FindControl("lblStatus") as Label;
                    lblRegisterGUID = gvr.FindControl("lblRegisterGUID") as Label;
                    ddlStatus = gvr.FindControl("ddlStatus") as DropDownList;

                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                    con.Open();

                    if (lblStatus.Text != ddlStatus.SelectedValue)
                    {
                        if (lblStatus.Text != "Rejected")
                        {
                            SqlCommand updateCmd = new SqlCommand("UPDATE Student_Programme_Register SET Status = @Status WHERE RegisterGUID = @RegisterGUID", con);
                            updateCmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);
                            updateCmd.Parameters.AddWithValue("@RegisterGUID", Guid.Parse(lblRegisterGUID.Text));
                            updateCmd.ExecuteNonQuery();
                            updateCount = -1;
                        }
                    }
                    con.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }
        }

        protected void loadStudListGV()
        {
            try
            {
                string loadQuery;
                loadQuery = "SELECT spr.RegisterGUID, s.StudentGUID, p.ProgrammeGUID, s.StudentFullName, s.NRIC, p.ProgrammeName, spr.Status FROM Student_Programme_Register spr ";
                loadQuery += "LEFT JOIN Student s ON spr.StudentGUID = s.StudentGUID ";
                loadQuery += "LEFT JOIN Programme p ON spr.ProgrammeGUID = p.ProgrammeGUID ";
                loadQuery += "WHERE CASE WHEN @StudentFullName = '' THEN @StudentFullName ELSE s.StudentFullName END LIKE '%'+@StudentFullName+'%' AND ";
                loadQuery += "CASE WHEN @NRIC = '' THEN @NRIC ELSE s.NRIC END LIKE '%'+@NRIC+'%' ";
                loadQuery += "ORDER BY s.StudentFullName";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand loadGVCmd = new SqlCommand(loadQuery, con);
                loadGVCmd.Parameters.AddWithValue("@StudentFullName", txtStudName.Text);
                loadGVCmd.Parameters.AddWithValue("@NRIC", txtNRIC.Text);

                SqlDataReader dtGV = loadGVCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtGV);
                con.Close();

                if (dt.Rows.Count != 0)
                {
                    gvCourse.DataSource = dt;
                    gvCourse.DataBind();
                    gvCourse.Visible = true;
                    lblNoData.Visible = false;
                }
                else
                {
                    lblNoData.Visible = true;
                    gvCourse.Visible = false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string searchQuery;

                searchQuery = "SELECT spr.RegisterGUID, s.StudentGUID, p.ProgrammeGUID, s.StudentFullName, s.NRIC, p.ProgrammeName, spr.Status FROM Student_Programme_Register spr ";
                searchQuery += "LEFT JOIN Student s ON spr.StudentGUID = s.StudentGUID ";
                searchQuery += "LEFT JOIN Programme p ON spr.ProgrammeGUID = p.ProgrammeGUID ";
                searchQuery += "WHERE CASE WHEN @StudentFullName = '' THEN @StudentFullName ELSE s.StudentFullName END LIKE '%'+@StudentFullName+'%' AND "; //ddl
                searchQuery += "CASE WHEN @NRIC = '' THEN @NRIC ELSE s.NRIC END LIKE '%'+@NRIC+'%' "; //text
                searchQuery += "ORDER BY s.StudentFullName";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand searchCmd = new SqlCommand(searchQuery, con);

                searchCmd.Parameters.AddWithValue("@StudentFullName", txtStudName.Text);
                searchCmd.Parameters.AddWithValue("@NRIC", txtNRIC.Text);
                SqlDataReader dtSearch = searchCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtSearch);
                con.Close();

                if (dt.Rows.Count != 0)
                {
                    gvCourse.DataSource = dt;
                    gvCourse.DataBind();
                    gvCourse.Visible = true;
                    lblNoData.Visible = false;
                    lblUpdate.Text = "0";
                    btnUpdate.Visible = false;
                }
                else
                {
                    lblNoData.Visible = true;
                    gvCourse.Visible = false;
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

            Session["UpdateStatus"] = "Yes";
            Response.Redirect("StudentRegisterListings", true);

        }

        protected void gvCourse_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    con.Open();
                    DropDownList ddlStatus = e.Row.Cells[6].FindControl("ddlStatus") as DropDownList;
                    Label lblStatus = e.Row.Cells[6].FindControl("lblStatus") as Label;

                    if (lblStatus.Text == "Rejected")
                    {
                        lblStatus.Visible = true;
                        ddlStatus.Visible = false;
                    }
                    else
                    {
                        lblStatus.Visible = false;
                        ddlStatus.Visible = true;
                        ddlStatus.SelectedValue = lblStatus.Text;
                    }
                    //SqlCommand searchCmd = new SqlCommand("SELECT * FROM Student_Programme_Register", con);
                    //SqlDataAdapter da = new SqlDataAdapter(searchCmd);
                    //DataTable dt = new DataTable();
                    //da.Fill(dt);
                    //con.Close();

                    //ddlStatus.DataSource = dt;
                    //ddlStatus.DataTextField = "Status";
                    //ddlStatus.DataValueField = "Status";
                    //ddlStatus.DataBind();
                    //string selectedCity = DataBinder.Eval(e.Row.DataItem, "Status").ToString();
                    //ddlStatus.Items.FindByValue(selectedCity).Selected = true;

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }

        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int updateCount = Convert.ToInt16(lblUpdate.Text);
                DropDownList ddlStatus = sender as DropDownList;
                GridViewRow gvr = ddlStatus.NamingContainer as GridViewRow;
                Label lblStatus = gvr.Cells[6].FindControl("lblStatus") as Label;
                if (lblStatus.Text != ddlStatus.SelectedValue)
                {
                    updateCount += 1;
                }
                else
                {
                    updateCount -= 1;
                }

                if (updateCount != 0)
                {
                    btnUpdate.Visible = true;
                    btnUpdate.Enabled = true;
                }
                else
                {
                    btnUpdate.Enabled = false;
                }

                lblUpdate.Text = updateCount.ToString();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }


    }
}