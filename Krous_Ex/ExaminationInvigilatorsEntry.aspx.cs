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
    public partial class ExaminationInvigilatorsEntry : System.Web.UI.Page
    {
        private string strMessage;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                if (Session["InsertInvigilators"] != null)
                {
                    if (Session["InsertInvigilators"].ToString() == "Yes")
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showAddSuccessToast(); ", true);
                        Session["InsertInvigilators"] = null;
                    }
                }

                loadSession();
                loadStaff();

                if (!String.IsNullOrEmpty(Request.QueryString["ExamTimetableGUID"]))
                {
                    loadData();
                    btnSave.Visible = false;
                    btnBack.Visible = true;
                    btnUpdate.Visible = true;
                    btnDelete.Visible = true;
                }
                else
                {
                    loadCourse();
                    btnSave.Visible = true;
                    btnBack.Visible = false;
                    btnUpdate.Visible = false;
                    btnDelete.Visible = false;
                }
            }
        }

        private void loadData()
        {
            try
            { 
                ListItem oList = new ListItem();

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT * FROM ExamInvigilatorsList ei LEFT JOIN ExamTimetable et ON ei.ExamTimetableGUID = et.ExamTimetableGUID LEFT JOIN Course C ON et.CourseGUID = C.CourseGUID LEFT JOIN Staff S ON ei.StaffGUID = S.StaffGUID WHERE et.ExamTimetableGUID = @ExamTimetableGUID ", con);

                GetCommand.Parameters.AddWithValue("@ExamTimetableGUID", Request.QueryString["ExamTimetableGUID"]);

                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtLoad = new DataTable();
                dtLoad.Load(reader);
                con.Close();

                if (dtLoad.Rows.Count != 0)
                {
                    ddlCourseExam.Enabled = false;
                    oList = new ListItem();
                    oList.Text = dtLoad.Rows[0]["CourseName"].ToString() + " (" + dtLoad.Rows[0]["CourseAbbrv"].ToString() + ")";
                    oList.Value = dtLoad.Rows[0]["CourseGUID"].ToString();
                    ddlCourseExam.Items.Add(oList);
                    hdCourseExam.Value = dtLoad.Rows[0]["ExamTimetableGUID"].ToString();
                    txtExamStartDateTime.Text = dtLoad.Rows[0]["ExamStartDateTime"].ToString();
                    txtExamEndDateTime.Text = dtLoad.Rows[0]["ExamEndDateTime"].ToString();

                    panelSelectedStaff.Visible = true;
                    for (int i = 0; i < dtLoad.Rows.Count; i++)
                    {
                        blSelectedStaff.Items.Add(dtLoad.Rows[i]["StaffFullName"].ToString());
                    }
                }
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        private void loadSession()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT S.SessionGUID, S.SessionMonth, S.SessionYear FROM AcademicCalender A, Session S WHERE S.SessionGUID = A.SessionGUID AND GetDate() BETWEEN A.SemesterStartDate AND A.SemesterEndDate;", con);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtSession = new DataTable();
                dtSession.Load(reader);
                con.Close();

                if (dtSession.Rows.Count != 0)
                {
                    txtSession.Text = dtSession.Rows[0]["SessionYear"].ToString() + dtSession.Rows[0]["SessionMonth"].ToString().PadLeft(2, '0');
                    hdSession.Value = dtSession.Rows[0]["SessionGUID"].ToString();
                }
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        private void loadCourse()
        {
            try
            {
                ddlCourseExam.Items.Clear();

                ListItem oList = new ListItem();
                oList.Text = "";
                oList.Value = "";
                ddlCourseExam.Items.Add(oList);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT * FROM ExamTimetable et LEFT JOIN Course C ON et.CourseGUID = c.CourseGUID WHERE SessionGUID = @SessionGUID ", con);

                GetCommand.Parameters.AddWithValue("@SessionGUID", hdSession.Value);

                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtCourse = new DataTable();
                dtCourse.Load(reader);


                for (int i = 0; i <= dtCourse.Rows.Count - 1; i++)
                {
                    oList = new ListItem();
                    oList.Text = dtCourse.Rows[i]["CourseName"].ToString() + " (" + dtCourse.Rows[i]["CourseAbbrv"].ToString() + ")";
                    oList.Value = dtCourse.Rows[i]["CourseGUID"].ToString();
                    ddlCourseExam.Items.Add(oList);
                }

                con.Close();
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        private void loadStaff()
        {
            try
            {
                ddlStaff.Items.Clear();

                ListItem oList = new ListItem();

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT * FROM Staff ", con);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtStaff = new DataTable();
                dtStaff.Load(reader);
                con.Close();

                for (int i = 0; i <= dtStaff.Rows.Count - 1; i++)
                {
                    oList = new ListItem();
                    oList.Text = dtStaff.Rows[i]["StaffFullName"].ToString();
                    oList.Value = dtStaff.Rows[i]["StaffGUID"].ToString();
                    ddlStaff.Items.Add(oList);
                }
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (isEmptyField())
            {
                if (isDuplicateStaff())
                {
                    if (isTimeClashed())
                    {
                        if (insertInvigilators())
                        {
                            Session["InsertInvigilators"] = "Yes";
                            Response.Redirect("ExaminationInvigilatorsEntry");
                        }
                        else
                        {
                            clsFunction.DisplayAJAXMessage(this, "Error! Unable to insert.");
                        }
                    }
                    else
                    {
                        clsFunction.DisplayAJAXMessage(this, "Error! The selected staff may clashed with other examination time.");
                    }
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Error! Some selected staff are already in-charged. Please proceed to listings to manage.");
                }
            } else
            {
                clsFunction.DisplayAJAXMessage(this, strMessage);
            }
        }

        private bool isEmptyField()
        {
            if (string.IsNullOrEmpty(ddlCourseExam.SelectedValue)) 
            {
                strMessage += "- Please select one exam to manage it \\n";
            }

            if (string.IsNullOrEmpty(hdSelectedStaff.Value))
            {
                strMessage += "- Please select at least one staff \\n";
            }

            if (!string.IsNullOrEmpty(strMessage))
            {
                string tempMessage = "Please complete all the required field as below : \\n" + strMessage;
                strMessage = tempMessage;
                return false;
            }

            return true;
        }

        private bool isDuplicateStaff()
        {
            try
            {
                string staffGUID = string.Empty;
                string keyWords = hdSelectedStaff.Value;
                string[] arrayVal = keyWords.Trim().Split(',');
                for (int i = 0; i < arrayVal.Length; i++)
                {
                    staffGUID = arrayVal[i];
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT * FROM ExamTimetable et, ExamInvigilatorsList ei, Staff S WHERE et.ExamTimeTableGUID = ei.ExamTimeTableGUID AND ei.StaffGUID = s.StaffGUID AND S.StaffGUID = @StaffGUID AND et.ExamTimeTableGUID = @ExamTimeTableGUID ", con))
                        {
                            con.Open();
                            cmd.Parameters.AddWithValue("@StaffGUID", staffGUID);
                            cmd.Parameters.AddWithValue("@ExamTimeTableGUID", hdCourseExam.Value);
                            SqlDataReader reader = cmd.ExecuteReader();
                            DataTable dtStaff = new DataTable();
                            dtStaff.Load(reader);
                            con.Close();
                            
                            if (dtStaff.Rows.Count != 0)
                            {
                                return false;
                            } else 
                            {
                                return true;
                            }
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
                return false;
            }
        }

        private bool isTimeClashed()
        {
            try
            {
                string staffGUID = string.Empty;
                string keyWords = hdSelectedStaff.Value;
                string[] arrayVal = keyWords.Trim().Split(',');
                for (int i = 0; i < arrayVal.Length; i++)
                {
                    staffGUID = arrayVal[i];
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString))
                    {
                        SqlCommand GetCommand = new SqlCommand("SELECT * FROM ExamTimetable et, ExamInvigilatorsList ei, Staff S WHERE et.ExamTimeTableGUID = ei.ExamTimeTableGUID AND ei.StaffGUID = s.StaffGUID AND s.StaffGUID = @StaffGUID  ", con);

                        con.Open();
                        GetCommand.Parameters.AddWithValue("@StaffGUID", staffGUID);
                        SqlDataReader reader = GetCommand.ExecuteReader();
                        DataTable dtStaff = new DataTable();
                        dtStaff.Load(reader);
                        con.Close();

                        if (dtStaff.Rows.Count != 0)
                        {
                            for (int j = 0; j < dtStaff.Rows.Count; j++)
                            {
                                DateTime selectedExamStartTime = DateTime.Parse(txtExamStartDateTime.Text);
                                DateTime selectedExamEndTime = DateTime.Parse(txtExamEndDateTime.Text);

                                DateTime test1 = DateTime.Parse(dtStaff.Rows[j]["ExamStartDateTime"].ToString());

                                if (selectedExamEndTime >= test1) { return false;  } else { return true; }
                            }
                        }
                        else
                        {
                            return true;
                        }
                        
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
                return false;
            }
        }

        private bool insertInvigilators()
        {
            try
            {
                string staffGUID = string.Empty;
                string keyWords = hdSelectedStaff.Value;
                string[] arrayVal = keyWords.Trim().Split(',');
                for (int i = 0; i < arrayVal.Length; i++)
                {
                    staffGUID = arrayVal[i];
                    using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO [ExamInvigilatorsList] VALUES(NewID(), @StaffGUID, @ExamTimeTableGUID)", con))
                        {
                            con.Open();
                            cmd.Parameters.AddWithValue("@StaffGUID", staffGUID);
                            cmd.Parameters.AddWithValue("@ExamTimeTableGUID", hdCourseExam.Value);
                            cmd.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                }

                return true;

            } catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
                return false;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (isEmptyField())
            {

                    if (updateInvigilators())
                    {
                        Session["UpdateInvigilators"] = "Yes";
                        Response.Redirect("ExaminationInvigilatorsListings");
                    }
                    else
                    {
                        clsFunction.DisplayAJAXMessage(this, "Error! Unable to update.");
                    }
                
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, strMessage);
            }
        }

        private bool updateInvigilators()
        {
            Guid ExamTimetableGUID = Guid.Parse(Request.QueryString["ExamTimetableGUID"]);

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand deleteCommand = new SqlCommand("DELETE FROM ExamInvigilatorsList WHERE ExamTimetableGUID = @ExamTimetableGUID;", con);

                deleteCommand.Parameters.AddWithValue("@ExamTimetableGUID", ExamTimetableGUID);

                deleteCommand.ExecuteNonQuery();

                con.Close();

                string staffGUID = string.Empty;
                string keyWords = hdSelectedStaff.Value;
                string[] arrayVal = keyWords.Trim().Split(',');
                for (int i = 0; i < arrayVal.Length; i++)
                {
                    staffGUID = arrayVal[i];
                    
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO [ExamInvigilatorsList] VALUES(NewID(), @StaffGUID, @ExamTimetableGUID)", con))
                    {
                        con.Open();
                        cmd.Parameters.AddWithValue("@StaffGUID", staffGUID);
                        cmd.Parameters.AddWithValue("@ExamTimeTableGUID", ExamTimetableGUID);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    
                }

                return true;
            }
            catch (Exception ex)
            {
                Response.Write(ex);
                return false;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (DeleteInvigilatorsListings())
            {
                Session["DeleteInvigilators"] = "Yes";
                Response.Redirect("ExaminationInvigilatorsListings");
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Error! Unable to delete.");
            }
        }

        private bool DeleteInvigilatorsListings()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand deleteCommand = new SqlCommand("DELETE FROM ExamInvigilatorsList WHERE ExamTimetableGUID = @ExamTimetableGUID;", con);

                deleteCommand.Parameters.AddWithValue("@ExamTimetableGUID", Request.QueryString["ExamTimetableGUID"]);

                deleteCommand.ExecuteNonQuery();

                con.Close();

                return true;
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
                return false;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {

        }

        protected void ddlExam_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT * FROM ExamTimeTable WHERE CourseGUID = @CourseGUID AND SessionGUID = @SessionGUID", con);

                GetCommand.Parameters.AddWithValue("@CourseGUID", ddlCourseExam.SelectedValue);
                GetCommand.Parameters.AddWithValue("@SessionGUID", hdSession.Value);

                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtExam = new DataTable();
                dtExam.Load(reader);

                if (dtExam.Rows.Count != 0)
                {
                    txtExamStartDateTime.Text = dtExam.Rows[0]["ExamStartDateTime"].ToString();
                    txtExamEndDateTime.Text = dtExam.Rows[0]["ExamEndDateTime"].ToString();
                    hdCourseExam.Value = dtExam.Rows[0]["ExamTimeTableGUID"].ToString();
                }


                con.Close();
            }

            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }
    }
}