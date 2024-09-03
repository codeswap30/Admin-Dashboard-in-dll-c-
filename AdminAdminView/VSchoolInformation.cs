using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminAdminView
{
    public interface VSchoolInformation
    {
        Form Forms { get; }

        event EventHandler UpdateSchool;
        //school email information 
        string Schoolname { get; set; }
        string Phonenumber { get; set; }
        string Emailaddress { get; set; }
        string Address { get; set; }

        bool IsError { get; set; }
        int ErrorLevel { get; set; }
        string ErrorMessage { get; set; }

    }
}
