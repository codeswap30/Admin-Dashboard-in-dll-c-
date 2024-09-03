using DbHelperClass;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminAdminView
{
    public class AdminReposities : IAdminModel
    {
        private Database DbHeper;

        public AdminReposities(string connection)
        {
            DbHeper = new MySqlDatabaseFactory().ProductFactory(connection);
        }
        public void Delete(string username)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AdminModel> GetRecords()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<AdminModel> GetRecords(string username)
        {
            string squery = $"SELECT * FROM Admin WHERE username LIKE '%{username}%'";
            var result = DbHeper.QueryData(squery, new System.Collections.Hashtable());
            var adminlist = new List<AdminModel>();

            int counter = 1;
            foreach(DataTable i in result)
            {
                var adminModel = new AdminModel();
                adminModel.Id = counter;
                adminModel.FullName = result["Fullname"];
                adminModel.Username = result["username"];
                adminModel.Password = result["Password"];
                adminModel.Active = result["Active"];

                adminlist.Add(adminModel);
            }

            return adminlist;
        }

        public void Update(string username)
        {
            throw new NotImplementedException();
        }
    }
}
