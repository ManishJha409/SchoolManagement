using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SchoolManagementAPI.Models
{
    public class UserAuthentication
    {
        AdoHelper helper = new AdoHelper();

        public bool CheckExistingUser(string userName, string passWord)
        {
            var isValidUser = false;

            List<SqlParameter> sp = new List<SqlParameter>()
            {
                new SqlParameter() {ParameterName = "@userName", Value= userName},
                new SqlParameter() {ParameterName = "@password", Value = passWord},
            };

            var result = helper.ExecDataSetProc("sp_checkExistingLogin", sp);
            if (result.Tables.Count > 0)
            {
                var record = result.Tables[0];
                if (record != null && record.Rows.Count > 0)
                {
                    isValidUser = true;
                }
            }

            return isValidUser;
        }


    }
}