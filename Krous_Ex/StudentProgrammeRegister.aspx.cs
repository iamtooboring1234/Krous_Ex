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
    public partial class StudentCourseRegister : System.Web.UI.Page
    {
        Guid userGUID = Guid.Parse(clsLogin.GetLoginUserGUID());
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                if(userGUID != null)
                {
                    loadDiploma();
                    loadFoundation();
                    loadDegree();
                }
              
            }
        }


        protected void loadFoundation()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand loadCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                loadCmd = new SqlCommand("SELECT p.ProgrammeName FROM Programme p, ProgrammeCourse pc WHERE pc.ProgrammeGUID = p.ProgrammeGUID AND p.ProgrammeCategory = 'Foundation' GROUP BY p.ProgrammeName ORDER BY p.ProgrammeName", con);
                SqlDataAdapter da = new SqlDataAdapter(loadCmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                rblFoundation.DataSource = ds;
                rblFoundation.DataTextField = "ProgrammeName";
                rblFoundation.DataValueField = "ProgrammeName";
                rblFoundation.DataBind();
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        protected void loadDiploma()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand loadCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                loadCmd = new SqlCommand("SELECT p.ProgrammeName FROM Programme p, ProgrammeCourse pc WHERE pc.ProgrammeGUID = p.ProgrammeGUID AND p.ProgrammeCategory = 'Diploma' GROUP BY p.ProgrammeName ORDER BY p.ProgrammeName", con);
                SqlDataAdapter da = new SqlDataAdapter(loadCmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                rblDiploma.DataSource = ds;
                rblDiploma.DataTextField = "ProgrammeName";
                rblDiploma.DataValueField = "ProgrammeName";
                rblDiploma.DataBind();
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        protected void loadDegree()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand loadCmd = new SqlCommand();

                string strCon = ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString;
                con = new SqlConnection(strCon);
                con.Open();

                loadCmd = new SqlCommand("SELECT p.ProgrammeName FROM Programme p, ProgrammeCourse pc WHERE pc.ProgrammeGUID = p.ProgrammeGUID AND p.ProgrammeCategory = 'Bachelor Degree' GROUP BY p.ProgrammeName ORDER BY p.ProgrammeName", con);
                SqlDataAdapter da = new SqlDataAdapter(loadCmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                rblDegree.DataSource = ds;
                rblDegree.DataTextField = "ProgrammeName";
                rblDegree.DataValueField = "ProgrammeName";
                rblDegree.DataBind();
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message);
            }
        }

        //can add bachelor degree
        //do upload ic, and result slip like spm / o-level 
        //add icImage, resultSlip into Student_Programme_Register table to save the uploaded file

    }
}