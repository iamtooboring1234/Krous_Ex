using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Krous_Ex
{
    public partial class StudentProgrammeRegisterUpdate : System.Web.UI.Page
    {
        Guid RegisterGUID;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                if (!String.IsNullOrEmpty(Request.QueryString["RegisterGUID"]))
                {
                    RegisterGUID = Guid.Parse(Request.QueryString["RegisterGUID"]);
                    loadStudentDetails();
                    loadSemester();
                }
                else
                {
                    btnBack.Visible = false;
                    btnApprove.Visible = false;
                    btnReject.Visible = false;
                }

            }
        }

        private void loadSemester()
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

        private void loadStudentDetails()
        {
            try
            {
                RegisterGUID = Guid.Parse(Request.QueryString["RegisterGUID"]);
                SqlConnection con = new SqlConnection();
                SqlCommand loadInfoCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                loadInfoCmd = new SqlCommand("SELECT s.StudentFullName, s.NRIC, p.ProgrammeName, spr.UploadIcImage, spr.UploadResult, spr.UploadMedical, spr.Status FROM Student_Programme_Register spr LEFT JOIN Student s ON spr.StudentGUID = s.StudentGUID LEFT JOIN Programme p ON spr.ProgrammeGUID = p.ProgrammeGUID WHERE spr.RegisterGUID = @RegisterGUID", con);
                loadInfoCmd.Parameters.AddWithValue("@RegisterGUID", RegisterGUID);
                SqlDataReader dtrLoad = loadInfoCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtrLoad);

                SqlCommand selectSemester = new SqlCommand("SELECT s.SemesterYear, s.SemesterSem FROM SEMESTER s LEFT JOIN Student_Programme_Register spr ON spr.SemesterGUID = s.SemesterGUID WHERE spr.RegisterGUID = @RegisterGUID ORDER BY SemesterYear, SemesterSem", con);
                selectSemester.Parameters.AddWithValue("@RegisterGUID", RegisterGUID);
                SqlDataReader readerSem = selectSemester.ExecuteReader();
                DataTable dtSem = new DataTable();
                dtSem.Load(readerSem);

                if (dt.Rows.Count != 0)
                { 
                    txtStudName.Text = dt.Rows[0]["StudentFullName"].ToString();
                    txtStudNRIC.Text = dt.Rows[0]["NRIC"].ToString();
                    txtProgName.Text = dt.Rows[0]["ProgrammeName"].ToString();

                    string status = dt.Rows[0]["Status"].ToString();
                    if (status == "Approved")
                    {
                        ddlSemester.Visible = false;
                        lblSemester.Visible = true;
                        btnApprove.Visible = false;
                        btnReject.Visible = false;
                        lblSelectSemester.Text = "Semester";
                        lblSemester.Text = "Year " + dtSem.Rows[0]["SemesterYear"].ToString() + " Sem " + dtSem.Rows[0]["SemesterSem"].ToString(); ;
                    }
                    else
                    {
                        ddlSemester.Visible = true;
                        lblSemester.Visible = false;
                    }

                    if (dt.Rows[0]["UploadMedical"].ToString() == "none")
                    {
                        string icFilePath = "~/Uploads/StudentRegisterFile/" + Request.QueryString["RegisterGUID"] + "/" + dt.Rows[0]["UploadIcImage"].ToString();
                        hlIcFile.Text = dt.Rows[0]["UploadIcImage"].ToString();
                        hlIcFile.Attributes["href"] = ResolveUrl(icFilePath);
                        lbIc.Attributes["href"] = ResolveUrl(icFilePath);
                        lbIc.Attributes["download"] = dt.Rows[0]["UploadIcImage"].ToString();

                        string resultFilePath = "~/Uploads/StudentRegisterFile/" + Request.QueryString["RegisterGUID"] + "/" + dt.Rows[0]["UploadResult"].ToString();
                        hlResultFile.Text = dt.Rows[0]["UploadResult"].ToString();
                        hlResultFile.Attributes["href"] = ResolveUrl(resultFilePath);
                        lbResult.Attributes["href"] = ResolveUrl(resultFilePath);
                        lbResult.Attributes["download"] = dt.Rows[0]["UploadResult"].ToString();

                        lblNoMedicalFile.Visible = true;
                        lbMedical.Visible = false;
                        lblNoMedicalFile.Text = "No medical file is uploaded by the student.";
                    }
                    else
                    {
                        string icFilePath = "~/Uploads/StudentRegisterFile/" + Request.QueryString["RegisterGUID"] + "/" + dt.Rows[0]["UploadIcImage"].ToString();
                        hlIcFile.Text = dt.Rows[0]["UploadIcImage"].ToString();
                        hlIcFile.Attributes["href"] = ResolveUrl(icFilePath);
                        lbIc.Attributes["href"] = ResolveUrl(icFilePath);
                        lbIc.Attributes["download"] = dt.Rows[0]["UploadIcImage"].ToString();

                        string resultFilePath = "~/Uploads/StudentRegisterFile/" + Request.QueryString["RegisterGUID"] + "/" + dt.Rows[0]["UploadResult"].ToString();
                        hlResultFile.Text = dt.Rows[0]["UploadResult"].ToString();
                        hlResultFile.Attributes["href"] = ResolveUrl(resultFilePath);
                        lbResult.Attributes["href"] = ResolveUrl(resultFilePath);
                        lbResult.Attributes["download"] = dt.Rows[0]["UploadResult"].ToString();

                        string medicalFilePath = "~/Uploads/StudentRegisterFile/" + Request.QueryString["RegisterGUID"] + "/" + dt.Rows[0]["UploadMedical"].ToString();
                        hlMedicalFile.Text = dt.Rows[0]["UploadMedical"].ToString();
                        hlMedicalFile.Attributes["href"] = ResolveUrl(medicalFilePath);
                        lbMedical.Attributes["href"] = ResolveUrl(medicalFilePath);
                        lbMedical.Attributes["download"] = dt.Rows[0]["UploadMedical"].ToString();
                    }

                }
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }
       
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (approveStudent())
            {
                if (insertRegisterPayment())
                {
                    //Session["UpdateStatus"] = "Yes";
                    clsFunction.DisplayAJAXMessage(this, "Student has been approved.");
                    Response.Redirect("StudentProgrammeRegisterListings");
                } else
                {
                    clsFunction.DisplayAJAXMessage(this, "Failed to insert payment.");
                }
                
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Error");
                Response.Redirect("StudentProgrammeRegisterListings");
            }
        }

        private bool approveStudent() 
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                RegisterGUID = Guid.Parse(Request.QueryString["RegisterGUID"]);

                //set student status to approved 
                SqlCommand updateCmd = new SqlCommand("UPDATE Student_Programme_Register SET Status = @Status, SemesterGUID = @SemesterGUID WHERE RegisterGUID = @RegisterGUID", con);
                updateCmd.Parameters.AddWithValue("@Status", "Approved");
                updateCmd.Parameters.AddWithValue("SemesterGUID", ddlSemester.SelectedValue);
                updateCmd.Parameters.AddWithValue("@RegisterGUID", RegisterGUID);
                updateCmd.ExecuteNonQuery();

                //select SessionGUID and update to student table
                SqlCommand selectSession = new SqlCommand("SELECT spr.BranchesGUID, s.SessionGUID, spr.ProgrammeGUID FROM Session s LEFT JOIN Student_Programme_Register spr on spr.SessionGUID = s.SessionGUID LEFT JOIN Branches b ON spr.BranchesGUID = b.BranchesGUID WHERE spr.RegisterGUID = @RegisterGUID", con);
                selectSession.Parameters.AddWithValue("@RegisterGUID", RegisterGUID);
                SqlDataReader dtrSelect = selectSession.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtrSelect);

                string SessionGUID = dt.Rows[0]["SessionGUID"].ToString();
                string ProgrammeGUID = dt.Rows[0]["ProgrammeGUID"].ToString();
                string BranchesGUID = dt.Rows[0]["BranchesGUID"].ToString();

                //get the faculty of the programme chosen
                SqlCommand selectFaculty = new SqlCommand("SELECT f.FacultyGUID, p.ProgrammeName, p.ProgrammeGUID FROM Programme p LEFT JOIN Faculty f ON p.FacultyGUID = f.FacultyGUID LEFT JOIN Student_Programme_Register spr ON spr.ProgrammeGUID = p.ProgrammeGUID WHERE spr.RegisterGUID = @RegisterGUID ", con);
                selectFaculty.Parameters.AddWithValue("@RegisterGUID", RegisterGUID);
                SqlDataReader dtrFaculty = selectFaculty.ExecuteReader();
                DataTable dtFaculty = new DataTable();
                dtFaculty.Load(dtrFaculty);

                string FacultyGUID = dtFaculty.Rows[0]["FacultyGUID"].ToString();

                SqlCommand updateSession = new SqlCommand("UPDATE Student SET SessionGUID = @SessionGUID, StudyStatus = @StudyStatus, FacultyGUID = @FacultyGUID, BranchesGUID = @BranchesGUID FROM Student s LEFT JOIN Student_Programme_Register spr ON spr.StudentGUID = s.StudentGUID WHERE spr.RegisterGUID = @RegisterGUID", con);
                updateSession.Parameters.AddWithValue("@StudyStatus", "Studying");
                updateSession.Parameters.AddWithValue("@SessionGUID", SessionGUID);
                updateSession.Parameters.AddWithValue("@FacultyGUID", FacultyGUID);
                updateSession.Parameters.AddWithValue("@BranchesGUID", BranchesGUID);
                updateSession.Parameters.AddWithValue("@RegisterGUID", RegisterGUID);
                updateSession.ExecuteNonQuery();

                //assign student into groupstudentlist
                SqlCommand selectCmd = new SqlCommand("SELECT s.StudentGUID FROM Student s LEFT JOIN Student_Programme_Register spr on spr.StudentGUID = s.StudentGUID WHERE spr.RegisterGUID = @RegisterGUID", con);
                selectCmd.Parameters.AddWithValue("@RegisterGUID", RegisterGUID);
                SqlDataReader dtr = selectCmd.ExecuteReader();
                DataTable dtS = new DataTable();
                dtS.Load(dtr);

                string StudentGUID = dtS.Rows[0]["StudentGUID"].ToString();
                Guid groupStudentListGUID = Guid.NewGuid();

                string hasGroupQuery = " SELECT G.GroupGUID, G.GroupCapacity, G.GroupNo, S.SessionGUID, spr.ProgrammeGUID, Count(Gs.studentGUID) as 'TotalNumberStudent' ";
                hasGroupQuery += "FROM [Group] G LEFT JOIN GroupStudentList Gs ON G.GroupGUID = Gs.GroupGUID ";
                hasGroupQuery += "LEFT JOIN Student S ON Gs.StudentGUID = S.StudentGUID ";
                hasGroupQuery += "LEFT JOIN Student_Programme_Register spr ON S.StudentGUID = spr.StudentGUID ";
                hasGroupQuery += "WHERE S.SessionGUID = @SessionGUID ";
                hasGroupQuery += "AND spr.ProgrammeGUID = @ProgrammeGUID ";
                hasGroupQuery += "GROUP BY G.GroupGUID, G.GroupNo, G.GroupCapacity, S.SessionGUID, spr.ProgrammeGUID ORDER BY G.GroupNo ";

                SqlCommand hasGroupCmd = new SqlCommand(hasGroupQuery, con);

                hasGroupCmd.Parameters.AddWithValue("@SessionGUID", SessionGUID);
                hasGroupCmd.Parameters.AddWithValue("@ProgrammeGUID", ProgrammeGUID);

                SqlDataReader dtrHasGroup = hasGroupCmd.ExecuteReader();
                DataTable dtHasGroup = new DataTable();
                dtHasGroup.Load(dtrHasGroup);

                if (dtHasGroup.Rows.Count != 0)
                {
                    bool newGroup = false;
                    int count = 1;

                    for (int i = 0; i < dtHasGroup.Rows.Count; i++)
                    {
                        int test1 = int.Parse(dtHasGroup.Rows[i]["GroupCapacity"].ToString());
                        int test2 = int.Parse(dtHasGroup.Rows[i]["TotalNumberStudent"].ToString());
                        if (int.Parse(dtHasGroup.Rows[i]["GroupCapacity"].ToString()) == int.Parse(dtHasGroup.Rows[i]["TotalNumberStudent"].ToString()))
                        {
                            newGroup = true;
                            count++;
                        }
                        else
                        {
                            newGroup = false;
                        }
                    }

                    if (newGroup == false)
                    {
                        string assignQuery = "SELECT G.GroupGUID, G.GroupNo, G.GroupCapacity, Count(Gs.StudentGUID), S.SessionGUID, spr.ProgrammeGUID ";
                        assignQuery += "FROM[Group] G LEFT JOIN GroupStudentList Gs ON G.GroupGUID = Gs.GroupGUID ";
                        assignQuery += "LEFT JOIN Student S ON Gs.StudentGUID = S.StudentGUID ";
                        assignQuery += "LEFT JOIN Student_Programme_Register spr ON S.StudentGUID = spr.StudentGUID ";
                        assignQuery += "WHERE S.SessionGUID = @SessionGUID OR S.SessionGUID IS NULL ";
                        assignQuery += "AND spr.ProgrammeGUID = @ProgrammeGUID OR spr.ProgrammeGUID IS NULL ";
                        assignQuery += "GROUP BY G.GroupGUID, G.GroupNo, G.GroupCapacity, S.SessionGUID, spr.ProgrammeGUID HAVING G.GroupCapacity > Count(Gs.StudentGUID) ORDER BY G.GroupNo ";

                        SqlCommand assignCmd = new SqlCommand(assignQuery, con);

                        assignCmd.Parameters.AddWithValue("@SessionGUID", SessionGUID);
                        assignCmd.Parameters.AddWithValue("@ProgrammeGUID", ProgrammeGUID);

                        SqlDataReader dtrAssign = assignCmd.ExecuteReader();
                        DataTable dtAssign = new DataTable();
                        dtAssign.Load(dtrAssign);

                        string GroupGUID = dtAssign.Rows[0]["GroupGUID"].ToString();

                        SqlCommand assignGroup = new SqlCommand("INSERT INTO GroupStudentList VALUES (@GroupStudentListGUID, @GroupGUID, @StudentGUID)", con);
                        assignGroup.Parameters.AddWithValue("@GroupStudentListGUID", groupStudentListGUID);
                        assignGroup.Parameters.AddWithValue("@GroupGUID", GroupGUID);
                        assignGroup.Parameters.AddWithValue("@StudentGUID", StudentGUID);
                        assignGroup.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand assignCmd = new SqlCommand("SELECT TOP 1 * FROM [GROUP] WHERE GroupNo=" + count + " ", con);

                        SqlDataReader dtrAssign = assignCmd.ExecuteReader();
                        DataTable dtAssign = new DataTable();
                        dtAssign.Load(dtrAssign);

                        Guid newGroupGUID = Guid.NewGuid();
                        string GroupGUID = dtAssign.Rows[0]["GroupGUID"].ToString();

                        if (dtAssign.Rows.Count == 0)
                        {
                            SqlCommand newGroupNo = new SqlCommand("INSERT INTO [Group] VALUES (@GroupGUID, @GroupNo, @GroupCapacity)", con);
                            newGroupNo.Parameters.AddWithValue("@GroupGUID", newGroupGUID);
                            newGroupNo.Parameters.AddWithValue("@GroupNo", count);
                            newGroupNo.Parameters.AddWithValue("@GroupCapacity", 20);
                            newGroupNo.ExecuteNonQuery();

                            SqlCommand assignGroup = new SqlCommand("INSERT INTO GroupStudentList VALUES (@GroupStudentListGUID, @GroupGUID, @StudentGUID)", con);
                            assignGroup.Parameters.AddWithValue("@GroupStudentListGUID", groupStudentListGUID);
                            assignGroup.Parameters.AddWithValue("@GroupGUID", newGroupGUID);
                            assignGroup.Parameters.AddWithValue("@StudentGUID", StudentGUID);
                            assignGroup.ExecuteNonQuery();
                        }
                        else
                        {
                            SqlCommand assignGroup = new SqlCommand("INSERT INTO GroupStudentList VALUES (@GroupStudentListGUID, @GroupGUID, @StudentGUID)", con);
                            assignGroup.Parameters.AddWithValue("@GroupStudentListGUID", groupStudentListGUID);
                            assignGroup.Parameters.AddWithValue("@GroupGUID", GroupGUID);
                            assignGroup.Parameters.AddWithValue("@StudentGUID", StudentGUID);
                            assignGroup.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    SqlCommand assignCmd = new SqlCommand("SELECT TOP 1 * FROM [GROUP] ORDER BY GroupNo", con);

                    SqlDataReader dtrAssign = assignCmd.ExecuteReader();
                    DataTable dtAssign = new DataTable();
                    dtAssign.Load(dtrAssign);

                    string GroupGUID = dtAssign.Rows[0]["GroupGUID"].ToString();

                    SqlCommand assignGroup = new SqlCommand("INSERT INTO GroupStudentList VALUES (@GroupStudentListGUID, @GroupGUID, @StudentGUID)", con);
                    assignGroup.Parameters.AddWithValue("@GroupStudentListGUID", groupStudentListGUID);
                    assignGroup.Parameters.AddWithValue("@GroupGUID", GroupGUID);
                    assignGroup.Parameters.AddWithValue("@StudentGUID", StudentGUID);
                    assignGroup.ExecuteNonQuery();
                }

                SqlCommand currentSemester = new SqlCommand("INSERT INTO CurrentSessionSemester VALUES (NEWID(), @SessionGUID, @SemesterGUID, @StudentGUID, NULL, NULL)", con);
                currentSemester.Parameters.AddWithValue("@SessionGUID", SessionGUID);
                currentSemester.Parameters.AddWithValue("@SemesterGUID", ddlSemester.SelectedValue);
                currentSemester.Parameters.AddWithValue("@StudentGUID", StudentGUID);
                currentSemester.ExecuteNonQuery();

                sendApprovedEmail(RegisterGUID);
                return true;
              
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            if (rejectStudent())
            {
                clsFunction.DisplayAJAXMessage(this, "The application of this student has been rejected.");
                Response.Redirect("StudentProgrammeRegisterUpdate");
            }
        }

        private bool rejectStudent()
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
                con.Open();
                RegisterGUID = Guid.Parse(Request.QueryString["RegisterGUID"]);

                SqlCommand updateCmd = new SqlCommand("UPDATE Student_Programme_Register SET Status = @Status WHERE RegisterGUID = @RegisterGUID", con);
                updateCmd.Parameters.AddWithValue("@Status", "Rejected");
                updateCmd.Parameters.AddWithValue("@RegisterGUID", RegisterGUID);
                updateCmd.ExecuteNonQuery();

                sendRejectEmail(RegisterGUID);

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }
        }


        //insert into payment table based on the credit hour 
        private bool insertRegisterPayment()
        {
            try
            {
                int fixedAmountPerCourse = 259;
                Guid paymentGUID = Guid.NewGuid();

                SqlConnection con = new SqlConnection();
                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                SqlCommand registerStatus = new SqlCommand("SELECT StudentGUID, ProgrammeGUID, Status FROM Student_Programme_Register WHERE RegisterGUID = @RegisterGUID", con);
                registerStatus.Parameters.AddWithValue("@RegisterGUID", RegisterGUID);
                SqlDataReader dtrStatus = registerStatus.ExecuteReader();
                DataTable dtStatus = new DataTable();
                dtStatus.Load(dtrStatus);

                string status = dtStatus.Rows[0]["Status"].ToString();
                Guid programmeGUID = Guid.Parse(dtStatus.Rows[0]["ProgrammeGUID"].ToString());
                Guid studentGUID = Guid.Parse(dtStatus.Rows[0]["StudentGUID"].ToString());

                string creditCmd;

                creditCmd = "SELECT c.CreditHour, c.CourseGUID FROM ProgrammeCourse pc ";
                creditCmd += "LEFT JOIN Course c ON pc.CourseGUID = c.CourseGUID ";
                creditCmd += "LEFT JOIN Student_Programme_Register spr ON pc.ProgrammeGUID = spr.ProgrammeGUID ";
                creditCmd += "LEFT JOIN Student st ON spr.StudentGUID = st.StudentGUID ";
                creditCmd += "WHERE pc.SemesterGUID = @SemesterGUID ";
                creditCmd += "AND Pc.ProgrammeGUID = @ProgrammeGUID ";
                creditCmd += "AND pc.SessionMonth = (SELECT s.SessionMonth FROM Session S LEFT JOIN Student st ON S.SessionGUID = st.SessionGUID ";
                creditCmd += "WHERE StudentGUID = @StudentGUID) ";
                creditCmd += "GROUP BY C.CourseGUID,  C.CreditHour ";

                SqlCommand getCreditCmd = new SqlCommand(creditCmd, con);
                getCreditCmd.Parameters.AddWithValue("@StudentGUID", studentGUID);
                getCreditCmd.Parameters.AddWithValue("@RegisterGUID", RegisterGUID);
                getCreditCmd.Parameters.AddWithValue("@SemesterGUID", ddlSemester.SelectedValue);
                getCreditCmd.Parameters.AddWithValue("@ProgrammeGUID", programmeGUID);
                SqlDataReader dtrCredit = getCreditCmd.ExecuteReader();
                DataTable dtCredit = new DataTable();
                dtCredit.Load(dtrCredit);

                //calculation
                string paymentNo = "P" + DateTime.Now.ToString("yyyyMMddHHmmss");
                int creditHour = int.Parse(dtCredit.Rows[0]["CreditHour"].ToString());
                int eachCourse = 0;

                if (dtCredit.Rows.Count != 0)
                {
                    for (int i = 0; i < dtCredit.Rows.Count; i++)
                    {
                        eachCourse += creditHour * fixedAmountPerCourse;
                    }
                }

                DateTime overdue = DateTime.Now.Date.AddDays(31);
                SqlCommand insertPayCmd = new SqlCommand("INSERT INTO Payment(PaymentGUID, PaymentNo, StudentGUID, PaymentStatus, TotalAmount, DateIssued, DateOverdue) VALUES(@PaymentGUID, @PaymentNo, @StudentGUID, @PaymentStatus, @TotalAmount, @DateIssued, @DateOverdue)", con);
                insertPayCmd.Parameters.AddWithValue("@PaymentGUID", paymentGUID);
                insertPayCmd.Parameters.AddWithValue("@PaymentNo", paymentNo);
                insertPayCmd.Parameters.AddWithValue("@StudentGUID", studentGUID);
                insertPayCmd.Parameters.AddWithValue("@PaymentStatus", "Pending");
                insertPayCmd.Parameters.AddWithValue("@TotalAmount", eachCourse);
                insertPayCmd.Parameters.AddWithValue("@DateIssued", DateTime.Now);
                insertPayCmd.Parameters.AddWithValue("@DateOverdue", overdue);
                insertPayCmd.ExecuteNonQuery();

                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }
        }

        private bool sendApprovedEmail(Guid RegisterGUID)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
            con.Open();

            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("krousExnoreply@gmail.com", "krousex2021");

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("krousExnoreply@gmail.com");

            cmd = new SqlCommand("SELECT s.Email, s.StudentFullName FROM Student_Programme_Register spr LEFT JOIN Student s ON spr.StudentGUID = s.StudentGUID WHERE RegisterGUID = @RegisterGUID", con);
            cmd.Parameters.AddWithValue("@RegisterGUID", RegisterGUID);
            SqlDataReader dtrSelect = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dtrSelect);
            con.Close();
            string fullname = dt.Rows[0]["StudentFullName"].ToString();

            String body = "Hello " + fullname + ", <br />We are here to notify you, that your programme registration has been successfully approved by the staff.<br /><br />Thank You.<br /><br />Best Regards,<br />Krous Ex";
            mail.To.Add(dt.Rows[0]["Email"].ToString());
            mail.Subject = "Programme Registration Success";
            mail.IsBodyHtml = true;
            mail.Body = body;

            client.Send(mail);

            return true;
        }

        private bool sendRejectEmail(Guid RegisterGUID)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
            con.Open();

            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "smtp.gmail.com";     
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("krousExnoreply@gmail.com", "krousex2021");

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("krousExnoreply@gmail.com");

            cmd = new SqlCommand("SELECT s.Email, s.StudentFullName FROM Student_Programme_Register spr LEFT JOIN Student s ON spr.StudentGUID = s.StudentGUID WHERE RegisterGUID = @RegisterGUID", con);
            cmd.Parameters.AddWithValue("@RegisterGUID", RegisterGUID);
            SqlDataReader dtrSelect = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dtrSelect);
            con.Close();
            string fullname = dt.Rows[0]["StudentFullName"].ToString();

            String body = "Hello " + fullname + ", <br />We are here to notify you, that your programme registration has been rejected by the staff due to some reasons. Please make a call to the office to more enquiry.<br /><br />Thank You.<br /><br />Best Regards,<br />Krous Ex";
            mail.To.Add(dt.Rows[0]["Email"].ToString());
            mail.Subject = "Programme Registration Rejected";
            mail.IsBodyHtml = true;
            mail.Body = body;

            client.Send(mail);

            return true;
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentProgrammeRegisterListings");
        }
    }
}