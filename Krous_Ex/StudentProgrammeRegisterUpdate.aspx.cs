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
                    btnBack.Visible = true;
                    btnApprove.Visible = true;
                    btnReject.Visible = true;
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

                loadInfoCmd = new SqlCommand("SELECT s.StudentFullName, s.NRIC, p.ProgrammeName, spr.UploadIcImage, spr.UploadResult, spr.UploadMedical FROM Student_Programme_Register spr LEFT JOIN Student s ON spr.StudentGUID = s.StudentGUID LEFT JOIN Programme p ON spr.ProgrammeGUID = p.ProgrammeGUID WHERE spr.RegisterGUID = @RegisterGUID", con);
                loadInfoCmd.Parameters.AddWithValue("@RegisterGUID", RegisterGUID);
                SqlDataReader dtrLoad = loadInfoCmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtrLoad);

                if (dt.Rows.Count != 0)
                { 
                    txtStudName.Text = dt.Rows[0]["StudentFullName"].ToString();
                    txtStudNRIC.Text = dt.Rows[0]["NRIC"].ToString();
                    txtProgName.Text = dt.Rows[0]["ProgrammeName"].ToString();

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
                Session["UpdateStatus"] = "Yes";
                Response.Redirect("StudentProgrammeRegisterListings");
            }
            else
            {
                clsFunction.DisplayAJAXMessage(this, "Error");
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
                SqlCommand updateCmd = new SqlCommand("UPDATE Student_Programme_Register SET Status = @Status WHERE RegisterGUID = @RegisterGUID", con);
                updateCmd.Parameters.AddWithValue("@Status", "Approved");
                updateCmd.Parameters.AddWithValue("@RegisterGUID", RegisterGUID);
                updateCmd.ExecuteNonQuery();

                //select SessionGUID and update to student table
                SqlCommand selectSession = new SqlCommand("SELECT s.SessionGUID, spr.ProgrammeGUID FROM Session s LEFT JOIN Student_Programme_Register spr on spr.SessionGUID = s.SessionGUID WHERE spr.RegisterGUID = @RegisterGUID", con);
                selectSession.Parameters.AddWithValue("@RegisterGUID", RegisterGUID);
                SqlDataReader dtrSelect = selectSession.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dtrSelect);

                string SessionGUID = dt.Rows[0]["SessionGUID"].ToString();
                string ProgrammeGUID = dt.Rows[0]["ProgrammeGUID"].ToString();

                SqlCommand updateSession = new SqlCommand("UPDATE Student SET SessionGUID = @SessionGUID, StudyStatus = @StudyStatus FROM Student s LEFT JOIN Student_Programme_Register spr ON spr.StudentGUID = s.StudentGUID WHERE spr.RegisterGUID = @RegisterGUID", con);
                updateSession.Parameters.AddWithValue("@StudyStatus", "Studying");
                updateSession.Parameters.AddWithValue("@SessionGUID", SessionGUID);
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

                        string GroupGUID = dtAssign.Rows[0]["GroupGUID"].ToString();

                        SqlCommand assignGroup = new SqlCommand("INSERT INTO GroupStudentList VALUES (@GroupStudentListGUID, @GroupGUID, @StudentGUID)", con);
                        assignGroup.Parameters.AddWithValue("@GroupStudentListGUID", groupStudentListGUID);
                        assignGroup.Parameters.AddWithValue("@GroupGUID", GroupGUID);
                        assignGroup.Parameters.AddWithValue("@StudentGUID", StudentGUID);
                        assignGroup.ExecuteNonQuery();
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
                sendEmail(RegisterGUID);
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
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }
        }

        private bool sendEmail(Guid RegisterGUID)
        {
            //send email
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
    }
}