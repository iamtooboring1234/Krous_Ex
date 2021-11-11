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
    public partial class ExaminationSubmissionListings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack != true)
            {
                loadGV();
            }
        }

        private void loadGV()
        {
            try
            {
                SqlConnection con;
                SqlCommand loadInfoCmd;
                DataTable dt = new DataTable();

                using (con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString))
                {
                    con.Open();

                    loadInfoCmd = new SqlCommand("SELECT * FROM ExamSubmission ex LEFT JOIN Student S ON ex.StudentGUID = S.StudentGUID ", con);

                    SqlDataReader dtrLoad = loadInfoCmd.ExecuteReader();

                    dt = new DataTable();
                    dt.Load(dtrLoad);

                    con.Close();

                    if (dt.Rows.Count != 0)
                    {
                        gvSubmission.DataSource = dt;
                        gvSubmission.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                clsFunction.DisplayAJAXMessage(this, ex.Message);
            }
        }

        protected void gvSubmission_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HyperLink hyperLink = e.Row.FindControl("hlView") as HyperLink;
                LinkButton linkButton = e.Row.FindControl("lbDownload") as LinkButton;
                if (hyperLink != null)
                {
                    hyperLink.Attributes["href"] = ResolveUrl("~/Uploads/ExaminationSubmissionFolder/" + DataBinder.Eval(e.Row.DataItem, "ExamTimetableGUID") + "/" + DataBinder.Eval(e.Row.DataItem, "ExamSubmissionGUID") + "/" + DataBinder.Eval(e.Row.DataItem, "SubmissionFile"));
                    linkButton.Attributes["href"] = ResolveUrl("~/Uploads/ExaminationSubmissionFolder/" + DataBinder.Eval(e.Row.DataItem, "ExamTimetableGUID") + "/" + DataBinder.Eval(e.Row.DataItem, "ExamSubmissionGUID") + "/" + DataBinder.Eval(e.Row.DataItem, "SubmissionFile"));
                    linkButton.Attributes["download"] = "" + DataBinder.Eval(e.Row.DataItem, "SubmissionFile");
                }
            }
        }
    }
}