using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Context = System.Web.HttpContext;

namespace SchoolManagementAPI.Models
{
    public class ExceptionHandle
    {
        private static String exepurl;
        AdoHelper helper = new AdoHelper();

        public bool SendExcepToDB(Exception ex)
        {
            var result = false;
            try
            {
                exepurl = Context.Current.Request.Url.ToString();

                List<SqlParameter> sp = new List<SqlParameter>()
                {
                    new SqlParameter() {ParameterName = "@exceptionmsg", Value= ex.Message},
                    new SqlParameter() {ParameterName = "@exceptiontype", Value = ex.GetType().Name.ToString()},
                    new SqlParameter() {ParameterName = "@exceptionurl", Value = exepurl},
                    new SqlParameter() {ParameterName = "@exceptionsource", Value = ex.StackTrace.ToString()},
                    new SqlParameter() {ParameterName = "@class", Value = exepurl},
                    new SqlParameter() {ParameterName = "@method", Value = exepurl},
                    new SqlParameter() {ParameterName = "@inserted_by", Value = 0},
                    new SqlParameter() {ParameterName = "@userrole", Value = 0},
                };
                var spParams = sp.Cast<object>().ToArray();
                var response = helper.ExecNonQueryProc("usp_HandleException", spParams);
                if (response > 0)
                    result = true;
            }
            catch(Exception _ex)
            {

            }
            return result;
        }
    }
}