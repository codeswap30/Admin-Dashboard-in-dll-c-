using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminAdminView
{
    public interface IModelAdmin
    {
        IEnumerable<AdminModel> GetRecords();
        IEnumerable<AdminModel> GetRecords(string username);
        void UpdateRecord();
        void DeleteRecord();

    }
}
