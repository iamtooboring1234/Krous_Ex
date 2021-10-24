﻿using System;
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
                    SelectCommand.Parameters.AddWithValue("@ICNo", Email);
                    SelectCommand.Parameters.AddWithValue("@StaffGUID", UserGUID);
                }
                else if (UserType == "Student")
                {
                    SelectCommand = new SqlCommand("SELECT * FROM STUDENT WHERE Email = @Email AND StudentGUID NOT IN (@StudentGUID)", con);
                    SelectCommand.Parameters.AddWithValue("@NRIC", Email);
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



    }
}