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

        public static bool IsICNo(string ic)
        {
            Regex regex = new Regex(@"(([[0-9]{2})(0[1-9]|1[0-2])(0[1-9]|[12][0-9]|3[01]))-([0-9]{2})-([0-9]{4})");
            return regex.IsMatch(ic);
        }

        public static bool IsPhoneNumber(string phoneNumber)
        {
            Regex regex = new Regex(@"^(01)[0-46-9][0-9]{7,8}$");
            return regex.IsMatch(phoneNumber);
        }

        public static bool CheckPriceFormat(string price)
        {
            Regex regex = new Regex(@"^\d+(,\d{3})*(\.\d{1,2})?$");
            return regex.IsMatch(price);
        }

        public static bool CheckPasswordFormat(string password)
        {
            Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,20}$");
            return regex.IsMatch(password);
        }

        //update profile
        public static bool CheckDuplicateEmail(string UserType, string Email, Guid UserGUID)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);

                con.Open();
                var SelectCommand = new SqlCommand();
                if (UserType == "Staff")
                {
                    SelectCommand = new SqlCommand("SELECT * FROM STAFF WHERE Email = @Email AND StaffStatus <> 'Terminated' AND StaffGUID NOT IN (@StaffGUID)", con);
                    SelectCommand.Parameters.AddWithValue("@Email", Email);
                    SelectCommand.Parameters.AddWithValue("@StaffGUID", UserGUID);
                }
                else if (UserType == "Student")
                {
                    SelectCommand = new SqlCommand("SELECT * FROM STUDENT WHERE Email = @Email AND StudentGUID NOT IN (@StudentGUID)", con);
                    SelectCommand.Parameters.AddWithValue("@Email", Email);
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

        public static bool CheckStaffEntryDuplicateEmail(string UserType, string Email)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);

                con.Open();
                var SelectCommand = new SqlCommand();
                if (UserType == "Staff")
                {
                    SelectCommand = new SqlCommand("SELECT * FROM STAFF WHERE Email = @Email AND StaffStatus <> 'Terminated'", con);
                    SelectCommand.Parameters.AddWithValue("@Email", Email);
                }
                else if (UserType == "Student")
                {
                    SelectCommand = new SqlCommand("SELECT * FROM STUDENT WHERE Email = @Email)", con);
                    SelectCommand.Parameters.AddWithValue("@Email", Email);
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
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }
        }



        //register acc check email
        public static bool CheckRegisterDuplicateEmail(string UserType, string Email)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);

                con.Open();
                var SelectCommand = new SqlCommand();
                if (UserType == "Staff")
                {
                    SelectCommand = new SqlCommand("SELECT * FROM STAFF WHERE Email = @Email AND StaffStatus <> 'Terminated'", con);
                    SelectCommand.Parameters.AddWithValue("@Email", Email);
                }
                else if (UserType == "Student")
                {
                    SelectCommand = new SqlCommand("SELECT * FROM STUDENT WHERE Email = @Email", con);
                    SelectCommand.Parameters.AddWithValue("@Email", Email);
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

        //register check
        public static bool CheckDuplicateICNo(string UserType, string ICNo)
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
                }
                else if (UserType == "Student")
                {
                    SelectCommand = new SqlCommand("SELECT * FROM STUDENT WHERE NRIC = @NRIC AND StudentGUID NOT IN (@StudentGUID)", con);
                    SelectCommand.Parameters.AddWithValue("@NRIC", ICNo);
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
                System.Diagnostics.Trace.WriteLine(ex.Message);
                return false;
            }
        }

        public static bool CheckStaffEntryDuplicateICNo(string UserType, string ICNo, Guid UserGUID)
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
                else if (UserType == "Student")
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
                System.Diagnostics.Trace.WriteLine(ex.Message);
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

        public static bool CheckDuplicateCourseName(string CourseName)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);

                con.Open();
                var SelectCommand = new SqlCommand();
                SelectCommand = new SqlCommand("SELECT * FROM Course WHERE CourseName = @CourseName ", con);
                SelectCommand.Parameters.AddWithValue("@CourseName", CourseName);
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

        public static bool CheckDuplicateCourseAbbrv(string CourseAbbrv)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);

                con.Open();
                var SelectCommand = new SqlCommand();
                SelectCommand = new SqlCommand("SELECT * FROM Course WHERE CourseAbbrv = @CourseAbbrv ", con);
                SelectCommand.Parameters.AddWithValue("@CourseAbbrv", CourseAbbrv);
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

        public static bool CheckDuplicateBranchName(string BranchesName)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);

                con.Open();
                var SelectCommand = new SqlCommand();

                SelectCommand = new SqlCommand("SELECT * FROM Branches WHERE BranchesName = @BranchesName ", con);
                SelectCommand.Parameters.AddWithValue("@BranchesName", BranchesName);
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

        public static bool CheckDuplicateBranchEmail(string BranchesEmail)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);

                con.Open();
                var SelectCommand = new SqlCommand();

                SelectCommand = new SqlCommand("SELECT * FROM Branches WHERE BranchesEmail = @BranchesEmail ", con);
                SelectCommand.Parameters.AddWithValue("@BranchesEmail", BranchesEmail);
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

        public static bool CheckDuplicateFacultyName(string FacultyName)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);

                con.Open();
                var SelectCommand = new SqlCommand();

                SelectCommand = new SqlCommand("SELECT * FROM Faculty WHERE FacultyName = @FacultyName ", con);
                SelectCommand.Parameters.AddWithValue("@FacultyName", FacultyName);
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

        public static bool CheckDuplicateFacultyAbbrv(string FacultyAbbrv)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);

                con.Open();
                var SelectCommand = new SqlCommand();
                SelectCommand = new SqlCommand("SELECT * FROM Faculty WHERE FacultyAbbrv = @FacultyAbbrv ", con);
                SelectCommand.Parameters.AddWithValue("@FacultyAbbrv", FacultyAbbrv);
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

        //public static bool CheckDuplicateMasterOrDoctor(string ProgrammeCategory, string ProgrammeName)
        //{
        //    try
        //    {
        //        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Krous_Ex"].ConnectionString);
        //        con.Open();
        //        var SelectCommand = new SqlCommand();

        //        SelectCommand = new SqlCommand("SELECT * FROM Programme WHERE ProgrammeCategory = @ProgrammeCategory AND ProgrammeName = @ProgrammeName ", con);
        //        SelectCommand.Parameters.AddWithValue("@ProgrammeCategory", ProgrammeCategory);
        //        SelectCommand.Parameters.AddWithValue("@ProgrammeName", ProgrammeName);

        //        SqlDataReader reader = SelectCommand.ExecuteReader();
        //        DataTable dtFound = new DataTable();
        //        dtFound.Load(reader);
        //        con.Close();

        //        if (ProgrammeCategory == "Master" || ProgrammeCategory == "Doctor of Philosophy")
        //        {
        //            if (dtFound.Rows.Count == 2)
        //            {
        //                return true;
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }

        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //}


    }
}