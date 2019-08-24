using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SchoolManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SchoolManagementAPI.Controllers
{
    public class AdminController : ApiController
    {
        ExceptionHandle e = new ExceptionHandle();

        //Academics Related

        // For Subject Module
        [HttpPost]
        public JObject CRUDSubject(JObject objSubject)
        {
            Subjects sub = null;
            if (objSubject != null)
            {
                var subJson = objSubject["objSubject"].ToString();
                sub = JsonConvert.DeserializeObject<Subjects>(subJson);
            }
            var jo = new JObject();
            try
            {
                Admin admin = new Admin();
                if (sub != null)
                {
                    var status = admin.CRUDSubject(sub);
                    if (status)
                        jo["status"] = 1;
                    else
                        jo["status"] = -1;
                }
                else
                {
                    jo["status"] = 0;
                }
            }
            catch(Exception ex)
            {
                ExceptionHandle e = new ExceptionHandle();
                e.SendExcepToDB(ex);
                jo["status"] = -1;
            }

            return jo;
        }

        [HttpGet]
        public List<Subjects> GetSubjectList()
        {
            var subjects = new List<Subjects>();
            try
            {
                Admin admin = new Admin();
                subjects = admin.GetSubjectList();
            }
            catch (Exception ex)
            {
                e.SendExcepToDB(ex);
            }

            return subjects;
        }

        [HttpPost]
        public JObject AddSection(string sectionName)
        {
            var jo = new JObject();
            try
            {
                Admin admin = new Admin();
                if (!string.IsNullOrEmpty(sectionName))
                {
                    var status = admin.AddSection(sectionName);
                    if (status)
                        jo["status"] = 1;
                }
                else
                {
                    jo["message"] = "Please enter Section name";
                    jo["status"] = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandle e = new ExceptionHandle();
                e.SendExcepToDB(ex);
                jo["status"] = -1;
            }

            return jo;
        }

        [HttpPost]
        public List<Section> GetSectionList()
        {
            var sections = new List<Section>();
            try
            {
                Admin admin = new Admin();
                sections = admin.GetSectionList();
            }
            catch (Exception ex)
            {
                e.SendExcepToDB(ex);
            }

            return sections;
        }

        [HttpPost]
        public JObject AddClass(string className)
        {
            var jo = new JObject();
            try
            {
                Admin admin = new Admin();
                if (!string.IsNullOrEmpty(className))
                {
                    var status = admin.AddClass(className);
                    if (status)
                        jo["status"] = 1;
                }
                else
                {
                    jo["message"] = "Please enter Class name";
                    jo["status"] = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandle e = new ExceptionHandle();
                e.SendExcepToDB(ex);
                jo["status"] = -1;
            }

            return jo;
        }

        [HttpPost]
        public List<Class> GetClassList()
        {
            var _classList = new List<Class>();
            try
            {
                Admin admin = new Admin();
                _classList = admin.GetClassList();
            }
            catch (Exception ex)
            {
                e.SendExcepToDB(ex);
            }

            return _classList;
        }

        [HttpPost]
        public JObject AddClassTimetable(Class objClass)
        {
            var jo = new JObject();
            try
            {
                Admin admin = new Admin();
                if (objClass != null)
                {
                    var status = admin.AddClassTimetable(objClass);
                    if (status)
                        jo["status"] = 1;
                }
                else
                {
                    jo["message"] = "Please enter Class name";
                    jo["status"] = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandle e = new ExceptionHandle();
                e.SendExcepToDB(ex);
                jo["status"] = -1;
            }

            return jo;
        }

        [HttpPost]
        public List<Class> GetClassTimetableList()
        {
            var _classList = new List<Class>();
            try
            {
                Admin admin = new Admin();
                _classList = admin.GetClassTimeTableList();
            }
            catch (Exception ex)
            {
                e.SendExcepToDB(ex);
            }

            return _classList;
        }

        [HttpPost]
        public JObject AssignSubject(Class objClass)
        {
            var jo = new JObject();
            try
            {
                Admin admin = new Admin();
                if (objClass != null)
                {
                    var status = admin.AssignSubjects(objClass);
                    if (status)
                        jo["status"] = 1;
                }
                else
                {
                    jo["status"] = 0;
                }
            }
            catch (Exception ex)
            {
                ExceptionHandle e = new ExceptionHandle();
                e.SendExcepToDB(ex);
                jo["status"] = -1;
            }

            return jo;
        }

        [HttpPost]
        public List<Class> GetAssignedSubjects()
        {
            var _classList = new List<Class>();
            try
            {
                Admin admin = new Admin();
                _classList = admin.GetAssignedSubjects();
            }
            catch (Exception ex)
            {
                e.SendExcepToDB(ex);
            }

            return _classList;
        }
    }
}