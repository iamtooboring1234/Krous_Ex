using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class StaffCreateAssessment : System.Web.UI.Page
    {
        Guid userGuid;
        protected void Page_Load(object sender, EventArgs e)
        {
            userGuid = Guid.Parse(clsLogin.GetLoginUserGUID());

            if (Session["CreateAssessment"] != null)
            {
                if (Session["CreateAssessment"].ToString() == "Yes")
                {
                    ClientScript.RegisterStartupScript(GetType(), "Javascript", "javascript:showAddSuccessToast(); ", true);
                    Session["CreateAssessment"] = null;
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Unable to create new assessment!");
                    Session["CreateAssessment"] = null;
                }
            }

            if (IsPostBack != true)
            {
                loadGroup();
                loadSessionMonth();
                loadCurrentSession();
                loadSemester();
                loadProgramme();
                loadProgrammeCategory();
                loadCourse();

            }
        }

        private void loadGroup() //load group student list you de group
        {
            try
            {
                ddlGroups.Items.Clear();

                ListItem oList = new ListItem();
                oList = new ListItem();
                oList.Text = "";
                oList.Value = "";
                ddlGroups.Items.Add(oList);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT g.GroupGUID, g.GroupNo FROM [Group] g LEFT JOIN GroupStudentList gsl ON g.GroupGUID = gsl.GroupGUID GROUP BY g.GroupGUID, g.GroupNo ORDER BY g.GroupNo", con);
                SqlDataReader readerGrp = GetCommand.ExecuteReader();

                DataTable dtGroup = new DataTable();
                dtGroup.Load(readerGrp);
                con.Close();

                for (int i = 0; i <= dtGroup.Rows.Count - 1; i++)
                {
                    oList = new ListItem();
                    oList.Text = dtGroup.Rows[i]["GroupNo"].ToString();
                    oList.Value = dtGroup.Rows[i]["GroupGUID"].ToString();
                    ddlGroups.Items.Add(oList);
                }
            }
            catch (Exception)
            {
                clsFunction.DisplayAJAXMessage(this, "Error loading group list.");
            }
        }

        private void loadSessionMonth() //load xian zai group student list li mian you de 
        {
            try
            {
                ddlSessionMonth.Items.Clear();
                ListItem oList = new ListItem();

                oList = new ListItem();
                oList.Text = "";
                oList.Value = "";
                ddlSessionMonth.Items.Add(oList);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT SessionMonth FROM Session S LEFT JOIN Student st ON S.SessionGUID = st.SessionGUID LEFT JOIN GroupStudentList gsl ON st.StudentGUID = gsl.StudentGUID WHERE GroupGUID = @GroupGUID GROUP BY SessionMonth ", con);
                GetCommand.Parameters.AddWithValue("@GroupGUID", ddlGroups.SelectedValue);
                SqlDataReader readerSessionMnth = GetCommand.ExecuteReader();
                DataTable dtSessionMnth = new DataTable();
                dtSessionMnth.Load(readerSessionMnth);
                con.Close();

                for (int i = 0; i <= dtSessionMnth.Rows.Count - 1; i++)
                {
                    oList = new ListItem();

                    oList.Text = dtSessionMnth.Rows[i]["SessionMonth"].ToString().PadLeft(2, '0');
                    oList.Value = dtSessionMnth.Rows[i]["SessionMonth"].ToString();
                    ddlSessionMonth.Items.Add(oList);
                }
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        private void loadCurrentSession() //load current session semester li mian you de sessionGUID
        {
            try
            {
                ddlCurrentSession.Items.Clear();
                ListItem oList = new ListItem();

                oList = new ListItem();
                oList.Text = "";
                oList.Value = "";
                ddlCurrentSession.Items.Add(oList);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT css.SessionGUID, s.SessionYear, s.SessionMonth FROM CurrentSessionSemester css LEFT JOIN [Session] s ON css.SessionGUID = s.SessionGUID GROUP BY css.SessionGUID, s.SessionYear, s.SessionMonth ORDER BY s.SessionYear, s.SessionMonth ", con);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtSession = new DataTable();
                dtSession.Load(reader);
                con.Close();

                for (int i = 0; i <= dtSession.Rows.Count - 1; i++)
                {
                    oList = new ListItem();

                    oList.Text = dtSession.Rows[i]["SessionYear"].ToString() + dtSession.Rows[i]["SessionMonth"].ToString().PadLeft(2, '0');
                    oList.Value = dtSession.Rows[i]["SessionGUID"].ToString();
                    ddlCurrentSession.Items.Add(oList);
                }
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

       
        private void loadSemester() //load all semester
        {
            try
            {
                ddlSemester.Items.Clear();
                ListItem oList = new ListItem();

                oList = new ListItem();
                oList.Text = "";
                oList.Value = "";
                ddlSemester.Items.Add(oList);

                string sqlQuery = "SELECT * FROM SEMESTER ORDER BY SemesterYear, SemesterSem ";

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                SqlCommand GetCommand = new SqlCommand(sqlQuery, con);

                SqlDataReader reader = GetCommand.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                con.Close();

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    oList = new ListItem();
                    oList.Text = "Year " + dt.Rows[i]["SemesterYear"].ToString() + " Sem " + dt.Rows[i]["SemesterSem"].ToString();
                    oList.Value = dt.Rows[i]["SemesterGUID"].ToString();
                    ddlSemester.Items.Add(oList);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        private void loadProgrammeCategory()
        {
            try
            {
                ddlProgrammCategory.Items.Clear();

                ListItem oList = new ListItem();

                oList = new ListItem();
                oList.Text = "";
                oList.Value = "";
                ddlProgrammCategory.Items.Add(oList);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                SqlCommand GetCommand = new SqlCommand("SELECT ProgrammeCategory FROM Programme GROUP BY ProgrammeCategory ORDER BY ProgrammeCategory", con);
                SqlDataReader reader = GetCommand.ExecuteReader();

                DataTable dtProgCat = new DataTable();
                dtProgCat.Load(reader);
                con.Close();

                for (int i = 0; i <= dtProgCat.Rows.Count - 1; i++)
                {
                    oList = new ListItem();
                    oList.Text = dtProgCat.Rows[i]["ProgrammeCategory"].ToString();
                    oList.Value = dtProgCat.Rows[i]["ProgrammeCategory"].ToString();
                    ddlProgrammCategory.Items.Add(oList);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        private void loadProgramme() //load programme based on category and the programme that register by the student in the group student list
        {
            try
            {
                ddlProgramme.Items.Clear();
                ListItem oList = new ListItem();

                oList = new ListItem();
                oList.Text = "";
                oList.Value = "";
                ddlProgramme.Items.Add(oList);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                string programme;
                programme = "SELECT spr.ProgrammeGUID, p.ProgrammeName, p.ProgrammeAbbrv FROM Student st INNER JOIN Student_Programme_Register spr ON st.StudentGUID = spr.StudentGUID ";
                programme += "INNER JOIN GroupStudentList gsl ON st.StudentGUID = gsl.StudentGUID ";
                programme += "LEFT JOIN Programme p ON spr.ProgrammeGUID = p.ProgrammeGUID ";
                programme += "WHERE p.ProgrammeCategory = @ProgrammeCategory ";
                programme += "GROUP BY spr.ProgrammeGUID, p.ProgrammeName, p.ProgrammeAbbrv ";

                SqlCommand programmeCmd = new SqlCommand(programme, con);
                programmeCmd.Parameters.AddWithValue("@ProgrammeCategory", ddlProgrammCategory.SelectedValue);
                SqlDataReader reader = programmeCmd.ExecuteReader();
                DataTable dtProg = new DataTable();
                dtProg.Load(reader);
                con.Close();

                for (int i = 0; i <= dtProg.Rows.Count - 1; i++)
                {
                    oList = new ListItem();
                    oList.Text = dtProg.Rows[i]["ProgrammeName"].ToString() + " (" + dtProg.Rows[i]["ProgrammeAbbrv"].ToString() + ")";
                    oList.Value = dtProg.Rows[i]["ProgrammeGUID"].ToString();
                    ddlProgramme.Items.Add(oList);
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
                ddlCourse.Items.Clear();
                ListItem oList = new ListItem();

                oList = new ListItem();
                oList.Text = "";
                oList.Value = "";
                ddlCourse.Items.Add(oList);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();

                string course;
                course = "SELECT C.CourseGUID, C.CourseName, C.CourseAbbrv FROM Programme p ";
                course += "LEFT JOIN ProgrammeCourse pc ON pc.ProgrammeGUID = p.ProgrammeGUID ";
                course += "LEFT JOIN Semester sm ON pc.SemesterGUID = sm.SemesterGUID ";
                course += "LEFT JOIN Course C ON pc.CourseGUID = C.CourseGUID ";
                course += "LEFT JOIN CurrentSessionSemester css ON sm.SemesterGUID = css.SemesterGUID ";
                course += "LEFT JOIN Student S ON css.StudentGUID = S.StudentGUID ";
                course += "LEFT JOIN GroupStudentList gsl ON S.StudentGUID = gsl.StudentGUID ";
                course += "WHERE sm.SemesterGUID = @SemesterGUID AND ";
                course += "pc.ProgrammeGUID = @ProgrammeGUID AND ";
                course += "pc.SessionMonth = @SessionMonth ";
                course += "AND css.SessionGUID = @CurrentSession ";
                course += "AND GroupGUID = @GroupGUID ";
                course += "GROUP BY C.CourseGUID, C.CourseName, C.CourseAbbrv ";

                SqlCommand courseCmd = new SqlCommand(course, con);
                courseCmd.Parameters.AddWithValue("@SemesterGUID", ddlSemester.SelectedValue);
                courseCmd.Parameters.AddWithValue("@ProgrammeGUID", ddlProgramme.SelectedValue);
                courseCmd.Parameters.AddWithValue("@SessionMonth", ddlSessionMonth.SelectedValue);
                courseCmd.Parameters.AddWithValue("@CurrentSession", ddlCurrentSession.SelectedValue);
                courseCmd.Parameters.AddWithValue("@GroupGUID", ddlGroups.SelectedValue);
                SqlDataReader readerCourse = courseCmd.ExecuteReader();
                DataTable dtCourse = new DataTable();
                dtCourse.Load(readerCourse);
                con.Close();

                for (int i = 0; i <= dtCourse.Rows.Count - 1; i++)
                {
                    oList = new ListItem();
                    oList.Text = dtCourse.Rows[i]["CourseName"].ToString() + " (" + dtCourse.Rows[i]["CourseAbbrv"].ToString() + ")";
                    oList.Value = dtCourse.Rows[i]["CourseGUID"].ToString();
                    ddlCourse.Items.Add(oList);
                }
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }


        protected bool createAssessment()
        {
            try
            {
                Guid AssessmentGUID = Guid.NewGuid();
                SqlConnection con = new SqlConnection();
                SqlCommand createCmd = new SqlCommand();

                string filename = "Assessment_" + Path.GetFileName(AsyncUploadMaterial.FileName);
                string folderName = "~/Uploads/AssessmentFolder/" + AssessmentGUID + "/";

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                if (!Directory.Exists(folderName))
                {
                    Directory.CreateDirectory(Server.MapPath(folderName));
                }

                if (filename != "")
                {
                    createCmd = new SqlCommand("INSERT INTO Assessment (AssessmentGUID, StaffGUID, GroupGUID, SessionGUID, ProgrammeGUID, SemesterGUID, CourseGUID, AssessmentTitle, AssessmentDesc, DueDate, CreatedDate, UploadMaterials) VALUES (@AssessmentGUID, @StaffGUID, @GroupGUID, @SessionGUID, @ProgrammeGUID, @SemesterGUID, @CourseGUID, @AssessmentTitle, @AssessmentDesc, @DueDate, @CreatedDate, @UploadMaterials)", con);
                    createCmd.Parameters.AddWithValue("@AssessmentGUID", AssessmentGUID);
                    createCmd.Parameters.AddWithValue("@StaffGUID", userGuid);
                    createCmd.Parameters.AddWithValue("@GroupGUID", ddlGroups.SelectedValue);
                    createCmd.Parameters.AddWithValue("@SessionGUID", ddlCurrentSession.SelectedValue);
                    createCmd.Parameters.AddWithValue("@ProgrammeGUID", ddlProgramme.SelectedValue);
                    createCmd.Parameters.AddWithValue("@SemesterGUID", ddlSemester.SelectedValue);
                    createCmd.Parameters.AddWithValue("@CourseGUID", ddlCourse.SelectedValue);
                    createCmd.Parameters.AddWithValue("@AssessmentTitle", txtAssTitle.Text);
                    createCmd.Parameters.AddWithValue("@AssessmentDesc", txtDesc.Text);

                    if(txtDueDate.Text != "")
                    {
                        createCmd.Parameters.AddWithValue("@DueDate", DateTime.ParseExact(txtDueDate.Text, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture));
                    }
                    else
                    {
                        createCmd.Parameters.AddWithValue("@DueDate", "");
                    }

                    createCmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                    if (AsyncUploadMaterial.HasFile)
                    {
                        createCmd.Parameters.AddWithValue("@UploadMaterials", filename);
                    }
                    else
                    {
                        createCmd.Parameters.AddWithValue("@UploadMaterials", "none");
                    }

                    createCmd.ExecuteNonQuery();

                    AsyncUploadMaterial.SaveAs(Server.MapPath(folderName) + filename);
                }
               
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }
        }

        protected void btnCreate_Click(object sender, EventArgs e)
        {
            if (assessmentValidation())
            {
                if (createAssessment())
                {
                    Session["CreateAssessment"] = "Yes";
                    Response.Redirect("StaffCreateAssessment");
                }
                else
                {
                    clsFunction.DisplayAJAXMessage(this, "Unable to create assessment.");
                }
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter all the required information.");
            }   
        }

        private bool assessmentValidation()
        {

            if(txtAssTitle.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter the assessment title.");
                return false;
            }

            if(txtDesc.Text == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please enter the assessment description.");
                return false;
            }

            if (ddlGroups.SelectedValue == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please select the group.");
                return false;
            }

            if (ddlSessionMonth.SelectedValue == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please select the session month.");
                return false;
            }

            if (ddlCurrentSession.SelectedValue == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please select the session.");
                return false;
            }

            if (ddlSemester.SelectedValue == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please select the semester.");
                return false;
            }

            if (ddlProgrammCategory.SelectedValue == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please select the programme category.");
                return false;
            }

            if (ddlProgramme.SelectedValue == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please select the programme name.");
                return false;
            }

            if (ddlCourse.SelectedValue == "")
            {
                clsFunction.DisplayAJAXMessage(this, "Please select the course.");
                return false;
            }

            return true;
        }

        protected void ddlGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlGroups.SelectedValue != "")
            {
                loadSessionMonth();
                ddlSessionMonth.Enabled = true;
            }
        }

        protected void ddlSessionMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlSessionMonth.SelectedValue != "")
            {
                loadCurrentSession();
                ddlCurrentSession.Enabled = true;
            }
        }

        protected void ddlCurrentSession_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlCurrentSession.SelectedValue != "")
            {
                loadSemester();
                ddlSemester.Enabled = true;
            }
        }

        protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlSemester.SelectedValue != "")
            {
                loadProgrammeCategory();
                ddlProgrammCategory.Enabled = true;
            }
        }

        protected void ddlProgrammCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProgrammCategory.SelectedValue != "")
            {
                loadProgramme();
                ddlProgramme.Enabled = true;
            }
        }

        protected void ddlProgramme_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlProgramme.SelectedValue != "")
            {
                loadCourse();
                ddlCourse.Enabled = true;
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("StaffDashboard");
        }

        
    }
}

