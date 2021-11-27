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
        private string strMessage;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                if (Session["SentNotification"] != null)
                {
                    if (Session["SentNotification"].ToString() == "Yes")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showNotificationSentSuccess(); ", true);
                        Session["SentNotification"] = null;
                    }
                }


                panelNotification.Visible = true;
                panelStaff.Visible = false;
                panelStudent.Visible = false;
            }
        }

        protected void radNotificationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNotificationContent.Text = "";
            txtNotificationSubject.Text = "";
            cbBranchAll.Checked = false;
            cbStudentBranchAll.Checked = false;
            cbFacultyAll.Checked = false;
            cbStudentFacultyAll.Checked = false;
            cbStudentSessionAll.Checked = false;

            if (radNotificationType.SelectedValue == "1")
            {
                panelNotification.Visible = true;
                panelStaff.Visible = false;
                panelStudent.Visible = false;
            }
            else if (radNotificationType.SelectedValue == "2")
            {
                panelNotification.Visible = true;
                panelStaff.Visible = true;
                panelStudent.Visible = false;
                loadBranches();
                loadFaculty();
            }
            else if (radNotificationType.SelectedValue == "3")
            {
                panelNotification.Visible = true;
                panelStaff.Visible = false;
                panelStudent.Visible = true;
                loadBranches();
                loadFaculty();
                loadSession();
            }
        }

        private void loadBranches()
        {
            cbBranch.Items.Clear();
            cbStudentBranch.Items.Clear();

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


                if (radNotificationType.SelectedValue == "2")
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        oList = new ListItem();
                        oList.Text = dt.Rows[i]["BranchesName"].ToString();
                        oList.Value = dt.Rows[i]["BranchesGUID"].ToString();
                        cbBranch.Items.Add(oList);
                    }
                }
                else if (radNotificationType.SelectedValue == "3")
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        oList = new ListItem();
                        oList.Text = dt.Rows[i]["BranchesName"].ToString();
                        oList.Value = dt.Rows[i]["BranchesGUID"].ToString();
                        cbStudentBranch.Items.Add(oList);
                    }
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
            cbStudentFaculty.Items.Clear();

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

                if (radNotificationType.SelectedValue == "2")
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        oList = new ListItem();
                        oList.Text = dt.Rows[i]["FacultyName"].ToString() + " (" + dt.Rows[i]["FacultyAbbrv"].ToString() + ")";
                        oList.Value = dt.Rows[i]["FacultyGUID"].ToString();
                        cbFaculty.Items.Add(oList);
                    }
                }
                else if (radNotificationType.SelectedValue == "3")
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        oList = new ListItem();
                        oList.Text = dt.Rows[i]["FacultyName"].ToString() + " (" + dt.Rows[i]["FacultyAbbrv"].ToString() + ")";
                        oList.Value = dt.Rows[i]["FacultyGUID"].ToString();
                        cbStudentFaculty.Items.Add(oList);
                    }
                }
            }

            catch (Exception)
            {
                clsFunction.DisplayAJAXMessage(this, "Error loading faculty.");
            }
        }

        private void loadSession()
        {
            cbStudentSession.Items.Clear();

            try
            {
                ListItem oList = new ListItem();

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT ss.SessionGUID, ss.SessionYear, ss.SessionMonth From Student S LEFT JOIN Session ss ON S.SessionGUID = ss.SessionGUID WHERE S.StudyStatus = 'Studying' GROUP BY ss.SessionGUID, ss.SessionYear, ss.SessionMonth ORDER BY ss.SessionYear, ss.SessionMonth ", con);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);
                con.Close();


                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    oList = new ListItem();
                    oList.Text = dt.Rows[i]["SessionYear"].ToString() + dt.Rows[i]["SessionMonth"].ToString().PadLeft(2, '0');
                    oList.Value = dt.Rows[i]["SessionGUID"].ToString();
                    cbStudentSession.Items.Add(oList);
                }
                
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
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

        protected void cbBranchAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbBranchAll.Checked == true)
            {
                foreach (ListItem item in cbBranch.Items)
                {
                    item.Selected = true;
                }
            }
            else
            {
                foreach (ListItem item in cbBranch.Items)
                {
                    //panelProgramme.Visible = false;
                    item.Selected = false;
                }
            }
        }

        protected void cbFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataTable dt = new DataTable();
            //panelProgramme.Visible = false;
            //cbProgramme.Items.Clear();
            bool isAllChecked = true;
            foreach (ListItem item in cbFaculty.Items)
            {
                if (!item.Selected)
                {
                    isAllChecked = false;
                    break;
                } 
            }

            //foreach (ListItem item in cbFaculty.Items)
            //{
            //    if (item.Selected)
            //    {
            //        cbProgramme.Items.Clear();

            //        panelProgramme.Visible = true;

            //        try
            //        {
            //            ListItem oList = new ListItem();

            //            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
            //            con.Open();
            //            SqlCommand GetCommand = new SqlCommand("SELECT * FROM Programme WHERE FacultyGUID = @FacultyGUID ORDER BY ProgrammeName", con);

            //            GetCommand.Parameters.AddWithValue("@FacultyGUID", item.Value);

            //            SqlDataReader reader = GetCommand.ExecuteReader();

            //            dt.Load(reader);
            //            con.Close();

            //            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            //            {
            //                oList = new ListItem();
            //                oList.Text = dt.Rows[i]["ProgrammeName"].ToString() + " (" + dt.Rows[i]["ProgrammeAbbrv"].ToString() + ")";
            //                oList.Value = dt.Rows[i]["ProgrammeGUID"].ToString();
            //                cbProgramme.Items.Add(oList);
            //            }
            //        }

            //        catch (Exception)
            //        {
            //            clsFunction.DisplayAJAXMessage(this, "Error loading faculty.");
            //        }
            //    } 
            //}

            cbFacultyAll.Checked = isAllChecked;
        }

        protected void cbFacultyAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFacultyAll.Checked == true)
            {
                foreach (ListItem item in cbFaculty.Items)
                {
                    item.Selected = true;
                }

                //DataTable dt = new DataTable();

                //cbProgramme.Items.Clear();

                //panelProgramme.Visible = true;

                //try
                //{
                //    ListItem oList = new ListItem();

                //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                //    con.Open();
                //    SqlCommand GetCommand = new SqlCommand("SELECT * FROM Programme ORDER BY ProgrammeName", con);

                //    SqlDataReader reader = GetCommand.ExecuteReader();

                //    dt.Load(reader);
                //    con.Close();

                //    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                //    {
                //        oList = new ListItem();
                //        oList.Text = dt.Rows[i]["ProgrammeName"].ToString() + " (" + dt.Rows[i]["ProgrammeAbbrv"].ToString() + ")";
                //        oList.Value = dt.Rows[i]["ProgrammeGUID"].ToString();
                //        cbProgramme.Items.Add(oList);
                //    }
                //}

                //catch (Exception)
                //{
                //    clsFunction.DisplayAJAXMessage(this, "Error loading faculty.");
                //}

            } else
            {
                foreach (ListItem item in cbFaculty.Items)
                {
                    //panelProgramme.Visible = false;
                    item.Selected = false;
                }
            }
        }

        protected void cbStudentFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isAllChecked = true;
            foreach (ListItem item in cbStudentFaculty.Items)
            {
                if (!item.Selected)
                {
                    isAllChecked = false;
                    break;
                }
            }

            cbStudentFacultyAll.Checked = isAllChecked;
        }

        protected void cbStudentFacultyAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbStudentFacultyAll.Checked == true)
            {
                foreach (ListItem item in cbStudentFaculty.Items)
                {
                    item.Selected = true;
                }
            }
            else
            {
                foreach (ListItem item in cbStudentFaculty.Items)
                {
                    item.Selected = false;
                }
            }
        }

        protected void cbStudentBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isAllChecked = true;
            foreach (ListItem item in cbStudentBranch.Items)
            {
                if (!item.Selected)
                {
                    isAllChecked = false;
                    break;
                }
            }

            cbStudentBranchAll.Checked = isAllChecked;
        }

        protected void cbStudentBranchAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbStudentBranchAll.Checked == true)
            {
                foreach (ListItem item in cbStudentBranch.Items)
                {
                    item.Selected = true;
                }
            }
            else
            {
                foreach (ListItem item in cbStudentBranch.Items)
                {
                    item.Selected = false;
                }
            }
        }

        protected void cbStudentSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isAllChecked = true;
            foreach (ListItem item in cbStudentSession.Items)
            {
                if (!item.Selected)
                {
                    isAllChecked = false;
                    break;
                }
            }

            cbStudentSessionAll.Checked = isAllChecked;
        }

        protected void cbStudentSessionAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbStudentSessionAll.Checked == true)
            {
                foreach (ListItem item in cbStudentSession.Items)
                {
                    item.Selected = true;
                }
            }
            else
            {
                foreach (ListItem item in cbStudentSession.Items)
                {
                    item.Selected = false;
                }
            }
        }

        private bool SentNotification()
        {
            try
            {
                string sqlQuery = "";
                string notificationDesc = "This notification is sent to ";

                if (radNotificationType.SelectedValue == "1")
                {
                    sqlQuery += "INSERT INTO Notification(NotificationGUID,UserGUID,NotificationSubject,NotificationContent,ReadFlag,SentDate,SentBy,NotificationDescription) ";
                    sqlQuery += "SELECT newid(),StaffGUID,@Subject,@Content,'N',@SendDate,@SendBy,@NotificationDesc ";
                    sqlQuery += "FROM Staff S ";
                    sqlQuery += "INSERT INTO Notification(NotificationGUID,UserGUID,NotificationSubject,NotificationContent,ReadFlag,SentDate,SentBy,NotificationDescription) ";
                    sqlQuery += "SELECT newid(),StudentGUID,@Subject,@Content,'N',@SendDate,@SendBy,@NotificationDesc ";
                    sqlQuery += "FROM Student S ";
                }
                else if (radNotificationType.SelectedValue == "2")
                {
                    sqlQuery += "INSERT INTO Notification(NotificationGUID,UserGUID,NotificationSubject,NotificationContent,ReadFlag,SentDate,SentBy,NotificationDescription) ";
                    sqlQuery += "SELECT newid(),StaffGUID,@Subject,@Content,'N',@SendDate,@SendBy,@NotificationDesc ";
                    sqlQuery += "FROM Staff S ";
                    sqlQuery += "LEFT JOIN Branches B ON ";
                    sqlQuery += "S.BranchesGUID = B.BranchesGUID ";
                    sqlQuery += "LEFT JOIN Faculty F ON ";
                    sqlQuery += "S.FacultyGUID = F.FacultyGUID WHERE ";

                    //to check at least one checkbox is checked
                    if (cbBranch.Items.Cast<ListItem>().Any(li => li.Selected))
                    {
                        bool isMultipleSelected = false;
                        foreach (ListItem item in cbBranch.Items)
                        {
                            if (item.Selected)
                            {
                                if (isMultipleSelected)
                                {
                                    sqlQuery += " OR S.BranchesGUID = '" + item.Value + "'";
                                }
                                else
                                {
                                    sqlQuery += " S.BranchesGUID = '" + item.Value + "'";
                                    isMultipleSelected = true;
                                }

                                notificationDesc += item.Text + ", ";
                            }
                        }

                        sqlQuery += " AND ";
                    }
                    else
                    {
                        clsFunction.DisplayAJAXMessage(this, "Please check at least one branch.");
                    }

                    if (cbFaculty.Items.Cast<ListItem>().Any(li => li.Selected))
                    {
                        bool isMultipleSelected = false;
                        foreach (ListItem item in cbFaculty.Items)
                        {
                            if (item.Selected)
                            {
                                if (isMultipleSelected)
                                {
                                    sqlQuery += " OR S.FacultyGUID = '" + item.Value + "'";
                                }
                                else
                                {
                                    sqlQuery += " S.FacultyGUID = '" + item.Value + "'";
                                    isMultipleSelected = true;
                                }

                                notificationDesc += item.Text + ", ";
                            }
                        }

                        sqlQuery += " AND ";

                    }
                    else
                    {
                        clsFunction.DisplayAJAXMessage(this, "Please check at least one faculty.");
                    }

                    sqlQuery += "S.StaffRole='" + ddlStaffRole.SelectedValue + "'";
                    sqlQuery += " AND S.StaffStatus = 'Active'";

                }
                else
                {
                    sqlQuery += "INSERT INTO Notification(NotificationGUID,UserGUID,NotificationSubject,NotificationContent,ReadFlag,SentDate,SentBy,NotificationDescription) ";
                    sqlQuery += "SELECT newid(),StudentGUID,@Subject,@Content,'N',@SendDate,@SendBy,@NotificationDesc ";
                    sqlQuery += "FROM Student S ";
                    sqlQuery += "LEFT JOIN Branches B ON ";
                    sqlQuery += "S.BranchesGUID = B.BranchesGUID ";
                    sqlQuery += "LEFT JOIN Faculty F ON ";
                    sqlQuery += "S.FacultyGUID = F.FacultyGUID WHERE (";

                    //to check at least one checkbox is checked
                    if (cbStudentBranch.Items.Cast<ListItem>().Any(li => li.Selected))
                    {
                        bool isMultipleSelected = false;
                        foreach (ListItem item in cbStudentBranch.Items)
                        {
                            if (item.Selected)
                            {
                                if (isMultipleSelected)
                                {
                                    sqlQuery += " OR S.BranchesGUID = '" + item.Value + "'";
                                }
                                else
                                {
                                    sqlQuery += " S.BranchesGUID = '" + item.Value + "'";
                                    isMultipleSelected = true;
                                }

                                notificationDesc += item.Text + ", ";
                            }
                        }

                        sqlQuery += ") AND (";
                    }
                    else
                    {
                        clsFunction.DisplayAJAXMessage(this, "Please check at least one branch.");
                    }

                    if (cbStudentFaculty.Items.Cast<ListItem>().Any(li => li.Selected))
                    {
                        bool isMultipleSelected = false;
                        foreach (ListItem item in cbStudentFaculty.Items)
                        {
                            if (item.Selected)
                            {
                                if (isMultipleSelected)
                                {
                                    sqlQuery += " OR S.FacultyGUID = '" + item.Value + "'";
                                }
                                else
                                {
                                    sqlQuery += " S.FacultyGUID = '" + item.Value + "'";
                                    isMultipleSelected = true;
                                }

                                notificationDesc += item.Text + ", ";
                            }
                        }

                        sqlQuery += ") AND (";

                    }
                    else
                    {
                        clsFunction.DisplayAJAXMessage(this, "Please check at least one faculty.");
                    }

                    if (cbStudentSession.Items.Cast<ListItem>().Any(li => li.Selected))
                    {
                        bool isMultipleSelected = false;
                        foreach (ListItem item in cbStudentSession.Items)
                        {
                            if (item.Selected)
                            {
                                if (isMultipleSelected)
                                {
                                    sqlQuery += " OR S.SessionGUID = '" + item.Value + "'";
                                }
                                else
                                {
                                    sqlQuery += " S.SessionGUID = '" + item.Value + "'";
                                    isMultipleSelected = true;
                                }

                                notificationDesc += item.Text + ", ";
                            }
                        }

                        sqlQuery += ") AND ";

                    }
                    else
                    {
                        clsFunction.DisplayAJAXMessage(this, "Please check at least one session.");
                    }

                    sqlQuery += " S.StudyStatus = 'Studying' ";
                }

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand InsertCommand = new SqlCommand(sqlQuery, con);

                testcmd.Text = sqlQuery;

                InsertCommand.Parameters.AddWithValue("@Subject", txtNotificationSubject.Text);
                InsertCommand.Parameters.AddWithValue("@Content", txtNotificationContent.Text);
                InsertCommand.Parameters.AddWithValue("@SendDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                InsertCommand.Parameters.AddWithValue("@SendBy", clsLogin.GetLoginUserName());
                InsertCommand.Parameters.AddWithValue("@NotificationDesc", notificationDesc);

                InsertCommand.ExecuteNonQuery();

                con.Close();

                return true;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                return false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (isEmptyField())
            {
                if (SentNotification())
                {
                    Session["SentNotification"] = "Yes";
                    Response.Redirect("NotificationEntry");
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Unable to insert. Failed to create.");
                }
            } else
            {
                clsFunction.DisplayAJAXMessage(this, strMessage);
            }
        }

        private bool isEmptyField()
        {
            if (radNotificationType.SelectedValue == "2")
            {
                if (!cbBranch.Items.Cast<ListItem>().Any(li => li.Selected))
                {
                    strMessage += "- Please check at least one branch(es) \\n";
                }

                if (!cbFaculty.Items.Cast<ListItem>().Any(li => li.Selected))
                {
                    strMessage += "- Please check at least one faculty(es) \\n";
                }
            }
            else if (radNotificationType.SelectedValue == "3")
            {
                if (!cbStudentBranch.Items.Cast<ListItem>().Any(li => li.Selected))
                {
                    strMessage += "- Please check at least one branch(es) \\n";
                }

                if (!cbStudentFaculty.Items.Cast<ListItem>().Any(li => li.Selected))
                {
                    strMessage += "- Please check at least one faculty(es) \\n";
                }

                if (!cbStudentSession.Items.Cast<ListItem>().Any(li => li.Selected))
                {
                    strMessage += "- Please check at least one session(s) \\n";
                }
            }

            if (string.IsNullOrEmpty(txtNotificationSubject.Text))
            {
                strMessage += "- Notification subject cannot be null \\n";
            }

            if (string.IsNullOrEmpty(txtNotificationContent.Text))
            {
                strMessage += "- Notification content cannot be null \\n";
            }

            if (!string.IsNullOrEmpty(strMessage))
            {
                string tempMessage = "Please complete all the required field as below : \\n" + strMessage;
                strMessage = tempMessage;
                return false;
            }

            return true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("NotificationEntry");
        }
    }
}