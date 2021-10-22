using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Krous_Ex
{
    public class clsValidation
    {
        public static bool IsEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return regex.IsMatch(email);
        }

        public static bool CheckPasswordFormat(string password)
        {
            Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,20}$");
            return regex.IsMatch(password);
        }

        public static bool CheckDuplicateICNo(string UserType, string ICNo, Guid UserGUID)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);

                con.Open();
                var SelectCommand = new SqlCommand();
                if (UserType == "Staff")
                {
                    SelectCommand = new SqlCommand("SELECT * FROM STAFF WHERE NRIC = @ICNo AND StaffStatus <> 'Terminated' AND StaffGUID NOT IN (@StaffGUID)", con);
                    SelectCommand.Parameters.AddWithValue("@ICNo", ICNo);
                    SelectCommand.Parameters.AddWithValue("@StaffGUID", UserGUID);
                }
                else if (UserType == "Citizen")
                {
                    SelectCommand = new SqlCommand("SELECT * FROM STUDENT WHERE NRIC = @NRIC AND StudentGUID NOT IN (@StudentGUID)", con);
                    SelectCommand.Parameters.AddWithValue("@NRIC", ICNo);
                    SelectCommand.Parameters.AddWithValue("@StudentGUID", UserGUID);
                }

                SqlDataReader reader = SelectCommand.ExecuteReader();
                DataTable dtFound = new DataTable();
                dtFound.Load(reader);
                con.Close();
                if (dtFound.Rows.Count != 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool CheckDuplicateProgrammeName(string ProgrammeName)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);

                con.Open();
                var SelectCommand = new SqlCommand();

                SelectCommand = new SqlCommand("SELECT * FROM Programme WHERE ProgrammeName = @ProgrammeName ", con);
                SelectCommand.Parameters.AddWithValue("@ProgrammeName", ProgrammeName);

                SqlDataReader reader = SelectCommand.ExecuteReader();
                DataTable dtFound = new DataTable();
                dtFound.Load(reader);
                con.Close();
                if (dtFound.Rows.Count != 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool CheckDuplicateProgrammeAbbrv(string ProgrammeAbbrv)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);

                con.Open();
                var SelectCommand = new SqlCommand();

                SelectCommand = new SqlCommand("SELECT * FROM Programme WHERE ProgrammeAbbrv = @ProgrammeAbbrv ", con);
                SelectCommand.Parameters.AddWithValue("@ProgrammeAbbrv", ProgrammeAbbrv);

                SqlDataReader reader = SelectCommand.ExecuteReader();
                DataTable dtFound = new DataTable();
                dtFound.Load(reader);
                con.Close();
                if (dtFound.Rows.Count != 0)
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool CheckDuplicateMasterOrDoctor(string ProgrammeCategory, string ProgrammeName)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);

                con.Open();
                var SelectCommand = new SqlCommand();

                SelectCommand = new SqlCommand("SELECT * FROM Programme WHERE ProgrammeCategory = @ProgrammeCategory AND ProgrammeName = @ProgrammeName ", con);
                SelectCommand.Parameters.AddWithValue("@ProgrammeCategory", ProgrammeCategory);
                SelectCommand.Parameters.AddWithValue("@ProgrammeName", ProgrammeName);

                SqlDataReader reader = SelectCommand.ExecuteReader();
                DataTable dtFound = new DataTable();
                dtFound.Load(reader);
                con.Close();

                if (ProgrammeCategory == "Master" || ProgrammeCategory == "Doctor of Philosophy")
                {
                    if (dtFound.Rows.Count == 2)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}