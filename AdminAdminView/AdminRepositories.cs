using CdwErrorMessage;
using DbHelperClass;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AdminAdminView
{
    public class AdminRepositories : IAdminModel
    {
        private Database DbHelper;
        private DataTable query;
        private int level;
        public AdminRepositories(string connection, bool enableError=false,int level=0)
        {
            this.level = level;
            this.DbHelper = new MySqlDatabaseFactory().ProductFactory(connection,enableError,level);
            
        }
        public void Delete(AdminModel adminModel)
        {
            try
            {
                Hashtable ht = new Hashtable();
                if (string.IsNullOrEmpty(adminModel.Username))
                    DbHelper.DeleteData("DELETE FROM admin", ht);
                else
                {
                    ht.Add("@username", adminModel.Username);
                    DbHelper.DeleteData("DELETE FROM admin WHERE Username=@username", ht);
                }
            }
            catch(Exception err)
            {
                MainClassLogger.LoadLog(this.level, 2).LogMessage(err.Message);
            }
            
        }

        public bool CheckExistAdmin(string username)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("@username", username);

                if (this.DbHelper.CountData("SELECT COUNT(*) FROM admin WHERE Username=@username", ht) > 0)
                    return true;


            }
            catch(Exception err)
            {
                MainClassLogger.LoadLog(this.level, 2).LogMessage(err.Message);
            }
            return false;
        }

        public IEnumerable<AdminModel> GetAllData()
        {
            
            var adminlist = new List<AdminModel>();
            string squery = "SELECT * FROM admin";
            Hashtable ht = null;

            try
            {
                query = DbHelper.QueryData(squery, ht);

                if (query != null)
                {
                    int counter = 1;
                    foreach (DataRow row in query.Rows)
                    {
                        var adminModels = new AdminModel();
                        adminModels.Id = counter;
                        adminModels.FullName = row["FullName"].ToString();
                        adminModels.Username = row["Username"].ToString();
                        adminModels.Password = row["Password"].ToString();
                        adminModels.Active = int.Parse(row["Active"].ToString()) == 0 ? "New" : "Update";
                        counter++;
                        adminlist.Add(adminModels);
                    }
                }
            }
            catch(Exception err)
            {
                MainClassLogger.LoadLog(this.level, 2).LogMessage(err.Message);
            }
            
           
            return adminlist;
        }

        public IEnumerable<AdminModel> GetSelectData(AdminModel adminModel)
        {

            string squery = $"SELECT * FROM admin WHERE Username LIKE '%{adminModel.Username}%'";
            query = DbHelper.QueryData(squery, new Hashtable());
            var adminlist = new List<AdminModel>();
            try
            {
                int counter = 1;
                foreach (DataRow row in query.Rows)
                {
                    var adminModels = new AdminModel();
                    adminModels.Id = counter;
                    adminModels.FullName = row["FullName"].ToString();
                    adminModels.Username = row["Username"].ToString();
                    adminModels.Password = row["Password"].ToString();
                    adminModels.Active = int.Parse(row["Active"].ToString()) == 0 ? "New" : "Update";
                    counter++;
                    adminlist.Add(adminModels);
                }
            }
            catch(Exception err)
            {
                MainClassLogger.LoadLog(this.level, 2).LogMessage(err.Message);
            }
            
            return adminlist;
        }

        public void Insert(AdminModel adminModel)
        {
            try
            {
                Hashtable ht = new Hashtable();

                ht.Add("@fullname", adminModel.FullName);
                ht.Add("@username", adminModel.Username);
                ht.Add("@password", adminModel.Password);
                ht.Add("@active", adminModel.Active == "New" ? 0 : 1);

                DbHelper.InsertData("INSERT INTO admin(Fullname,Username,Password,Active) VALUES(@fullname,@username,@password,@active)", ht);
            }
            catch(Exception err)
            {
                MainClassLogger.LoadLog(this.level, 2).LogMessage(err.Message);
            }
            
        }

        public void Update(AdminModel adminModel)
        {
            try
            {
                if (!string.IsNullOrEmpty(adminModel.Username))
                {
                    Hashtable ht = new Hashtable();
                    ht.Add("@fullname", adminModel.FullName);
                    ht.Add("@username", adminModel.Username);
                    ht.Add("@password", adminModel.Password);
                    ht.Add("@active", adminModel.Active);

                    DbHelper.UpdateData("UPDATE admin SET Fullname=@fullname,Username=@username,Password=@password,Active=@active WHERE Username=@username", ht);
                }
            }
            catch(Exception err)
            {
                MainClassLogger.LoadLog(this.level, 2).LogMessage(err.Message);
            }
            
        }

        public void ModifySchoolInformation(SchoolInfo schoolInfo)
        {
            try
            {
                Hashtable ht = new Hashtable();
                ht.Add("@Schoolname", schoolInfo.SchoolName);
                ht.Add("@Phone", schoolInfo.PhoneNumber);
                ht.Add("@Email", schoolInfo.Email);
                ht.Add("@Address", schoolInfo.Address);

                if (this.DbHelper.CountData("SELECT COUNT(*) FROM schoolinfo", new Hashtable()) == 0)
                    DbHelper.InsertData("INSERT INTO schoolinfo(Schoolname,Phone,Email,Address) VALUES(@Schoolname,@Phone,@Email,@Address)", ht);
                else
                    DbHelper.UpdateData("UPDATE schoolinfo SET Schoolname=@Schoolname,Phone=@Phone,Email=@Email,Address=@Address WHERE Schoolid=1", ht);
            }
            catch(Exception err)
            {
                MainClassLogger.LoadLog(this.level, 2).LogMessage(err.Message);
            }
            
                
        }

        public SchoolInfo FetchSchoolInfo()
        {
            var schoolinfo = new SchoolInfo();
            string squery = "SELECT * FROM schoolinfo Where Schoolid=1";
            Hashtable ht = null;

            try
            {
                query = DbHelper.QueryData(squery, ht);

                if (query != null)
                {
                    foreach (DataRow row in query.Rows)
                    {

                        schoolinfo.SchoolName = row["Schoolname"].ToString();
                        schoolinfo.PhoneNumber = row["Phone"].ToString();
                        schoolinfo.Email = row["Email"].ToString();
                        schoolinfo.Address = row["Address"].ToString();

                    }
                }
            }
            catch(Exception err)
            {
                MainClassLogger.LoadLog(this.level, 2).LogMessage(err.Message);
            }


            return schoolinfo;
        }
    }
}
