using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminAdminView
{
    public interface IAdminModel
    {
        IEnumerable<AdminModel> GetAllData();
        IEnumerable<AdminModel> GetSelectData(AdminModel adminModel);
        void Delete(AdminModel adminModel);
        void Update(AdminModel adminModel);
        void Insert(AdminModel adminModel);
        void ModifySchoolInformation(SchoolInfo schoolInfo);
        SchoolInfo FetchSchoolInfo();
    }
}
