using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SchoolManagementAPI.Models
{
    public class Admin
    {
        AdoHelper helper = new AdoHelper();
        ExceptionHandle exception = new ExceptionHandle();

        //Add Subject details and show class list
        public bool CRUDSubject(Subjects objSubject)
        {
            var status = false;
            try
            {
                if (objSubject != null)
                {
                    List<SqlParameter> sp = new List<SqlParameter>()
                    {
                        new SqlParameter() {ParameterName = "@subjectname", Value= objSubject.SubjectName},
                        new SqlParameter() {ParameterName = "@subjecttype", Value= objSubject.SubjectType},
                        new SqlParameter() {ParameterName = "@operation", Value= objSubject.Operation},
                        new SqlParameter() {ParameterName = "@id", Value= Convert.ToInt32(objSubject.Id)},
                        new SqlParameter() {ParameterName = "@inserted_by", Value= 0},
                    };

                    var spParams = sp.Cast<object>().ToArray();
                    var response = helper.ExecNonQueryProc("usp_Academics_AddSubject", spParams);
                    if (response > 0)
                        status = true;
                }
            }
            catch(Exception ex)
            {
                exception.SendExcepToDB(ex);
            }

            return status;
        }

        public List<Subjects> GetSubjectList()
        {
            List<Subjects> subList = new List<Subjects>();
            try
            {
                var response = helper.ExecDataSetProc("usp_Academics_GetSubject");
                if (response != null && response.Tables.Count > 0)
                {
                    var subObj = response.Tables[0];
                    if (subObj.Rows.Count > 0)
                    {
                        foreach (DataRow row in subObj.Rows)
                        {
                            Subjects sub = new Subjects
                            {
                                SubjectName = row["SubjectName"].ToString(),
                                SubjectType = row["SubjectType"].ToString(),
                                InsertedBy = row["Inserted_By"].ToString(),
                                Id = row["Id"].ToString()
                            };
                            subList.Add(sub);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exception.SendExcepToDB(ex);
            }

            return subList;
        }

        //Add Section details and show class list
        public bool CRUDSection(Section objSection)
        {
            var status = false;
            try
            {
                if (objSection != null)
                {
                    List<SqlParameter> sp = new List<SqlParameter>()
                    {
                        new SqlParameter() {ParameterName = "@sectionname", Value= objSection.SectionName},
                        new SqlParameter() {ParameterName = "@inserted_by", Value= 0},
                        new SqlParameter() {ParameterName = "@id", Value= objSection.Id},
                        new SqlParameter() {ParameterName = "@operation", Value= objSection.Operation}
                    };
                    var spParams = sp.Cast<object>().ToArray();
                    var response = helper.ExecNonQueryProc("usp_Academics_AddSection", spParams);
                    if (response > 0)
                        status = true;
                }
            }
            catch (Exception ex)
            {
                exception.SendExcepToDB(ex);
            }

            return status;
        }

        public List<Section> GetSectionList()
        {
            List<Section> secList = new List<Section>();
            try
            {
                var response = helper.ExecDataSetProc("usp_Academics_GetSection");
                if (response != null && response.Tables.Count > 0)
                {
                    var secObj = response.Tables[0];
                    if (secObj.Rows.Count > 0)
                    {
                        foreach (DataRow row in secObj.Rows)
                        {
                            Section sec = new Section
                            {
                                SectionName = secObj.Columns["DivisionName"].ToString(),
                                Inserted_By = int.Parse(secObj.Columns["Inserted_By"].ToString()),
                                Id = int.Parse(secObj.Columns["DivisionId"].ToString())
                            };
                            secList.Add(sec);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exception.SendExcepToDB(ex);
            }

            return secList;
        }

        //Add class details and show class list
        public bool AddClass(string className)
        {
            var status = false;
            try
            {
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@sectionname", Value= className},
                    new SqlParameter() {ParameterName = "@inserted_by", Value= 0},

                };
                var response = helper.ExecNonQueryProc("usp_Academics_AddClass", sp);
                if (response > 0)
                    status = true;
            }
            catch (Exception ex)
            {
                exception.SendExcepToDB(ex);
            }

            return status;
        }

        public List<Class> GetClassList()
        {
            List<Class> classList = new List<Class>();
            try
            {
                var response = helper.ExecDataSetProc("usp_Academics_GetClass");
                if (response != null && response.Tables.Count > 0)
                {
                    var subObj = response.Tables[0];
                    if (subObj.Rows.Count > 0)
                    {
                        foreach (DataRow row in subObj.Rows)
                        {
                            Class _class = new Class();
                            _class.ClassName = row["ClassName"].ToString();
                            _class.Inserted_By = int.Parse(row["Inserted_By"].ToString());
                            _class.ClassId = int.Parse(row["Id"].ToString());
                            classList.Add(_class);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exception.SendExcepToDB(ex);
            }

            return classList;
        }

        //Save And get Class Timetable
        public bool AddClassTimetable(Class objClass)
        {
            var status = false;
            try
            {
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@classid", Value= objClass.ClassId},
                    new SqlParameter() {ParameterName = "@sectionid", Value= objClass.SectionId},
                    new SqlParameter() {ParameterName = "@subjectid", Value= objClass.SubjectId},
                    new SqlParameter() {ParameterName = "@day", Value= objClass._Day},
                    new SqlParameter() {ParameterName = "@starttime", Value= objClass.StartTime},
                    new SqlParameter() {ParameterName = "@endtime", Value= objClass.EndTime},
                    new SqlParameter() {ParameterName = "@roomno", Value= objClass.RoomNo},
                    new SqlParameter() {ParameterName = "@inserted_by", Value= objClass.Inserted_By},
                };
                var response = helper.ExecNonQueryProc("usp_Academics_AddClassTimetable", sp);
                if (response > 0)
                    status = true;
            }
            catch (Exception ex)
            {
                exception.SendExcepToDB(ex);
            }

            return status;
        }

        public List<Class> GetClassTimeTableList()
        {
            List<Class> classList = new List<Class>();
            try
            {
                var response = helper.ExecDataSetProc("usp_Academics_GetClassTimeTable");
                if (response != null && response.Tables.Count > 0)
                {
                    var subObj = response.Tables[0];
                    if (subObj.Rows.Count > 0)
                    {
                        foreach (DataRow row in subObj.Rows)
                        {
                            Class _class = new Class();
                            _class.SubjectName= row["SubjectName"].ToString();
                            //_class._Day = Enum.Parse(DayOfWeek, row["Day"]);
                            _class.ClassId = int.Parse(row["Id"].ToString());
                            _class.ClassName = row["ClassName"].ToString();
                            _class.SectionName = row["SectionName"].ToString();
                            _class.StartTime = Convert.ToDateTime(row["StartTime"].ToString());
                            _class.EndTime = Convert.ToDateTime(row["EndTime"].ToString());
                            _class.RoomNo = int.Parse(row["SubjectName"].ToString());
                            _class.Inserted_By = int.Parse(row["Inserted_By"].ToString());
                            classList.Add(_class);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exception.SendExcepToDB(ex);
            }

            return classList;
        }

        //Assign Subjects to Teachers
        public bool AssignSubjects(Class objClass)
        {
            var status = false;
            try
            {
                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@classid", Value= objClass.ClassId},
                    new SqlParameter() {ParameterName = "@sectionid", Value= objClass.SectionId},
                    new SqlParameter() {ParameterName = "@subjectid", Value= objClass.SubjectId},
                    new SqlParameter() {ParameterName = "@teacherid", Value= objClass.TeacherId},
                    new SqlParameter() {ParameterName = "@inserted_by", Value= objClass.Inserted_By},
                };
                var response = helper.ExecNonQueryProc("usp_Academics_AssignSubjects", sp);
                if (response > 0)
                    status = true;
            }
            catch (Exception ex)
            {
                exception.SendExcepToDB(ex);
            }

            return status;
        }

        public List<Class> GetAssignedSubjects()
        {
            List<Class> classList = new List<Class>();
            try
            {
                var response = helper.ExecDataSetProc("usp_Academics_GetAssignSubjects");
                if (response != null && response.Tables.Count > 0)
                {
                    var subObj = response.Tables[0];
                    if (subObj.Rows.Count > 0)
                    {
                        foreach (DataRow row in subObj.Rows)
                        {
                            Class _class = new Class();
                            _class.SubjectName = row["SubjectName"].ToString();
                            _class.ClassId = int.Parse(row["Id"].ToString());
                            _class.SectionId = int.Parse(row["SectionId"].ToString());
                            _class.ClassName = row["ClassName"].ToString();
                            _class.SectionName = row["SectionName"].ToString();
                            _class.TeacherName = row["TeacherName"].ToString();
                            _class.Inserted_By = int.Parse(row["Inserted_By"].ToString());
                            classList.Add(_class);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                exception.SendExcepToDB(ex);
            }

            return classList;
        }

    }

    public class Subjects
    {
        public string Id { get; set; }
        public string SubjectName { get; set; }
        public string SubjectType { get; set; }
        public string InsertedBy { get; set; }
        public int Operation { get; set; }
    }

    public class Section
    {
        public int Id { get; set; }
        public string SectionName { get; set; }
        public int Inserted_By { get; set; }
        public int Operation { get; set; }
    }

    public class Class
    {
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public int SubjectId { get; set; }
        public string ClassName { get; set; }
        public string SubjectName { get; set; }
        public string SectionName { get; set; }
        public int Inserted_By { get; set; }
        public DayOfWeek _Day { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int RoomNo { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
    }
}