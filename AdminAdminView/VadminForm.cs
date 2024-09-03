using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminAdminView
{
    public interface VadminForm
    {
        string Fullname { get; set; }
        string Username { get; set; }
        string Password { get; set; }
        string Active { get; set; }

        bool IsError { get; set; }
        int ErrorLevel { get; set; }
        string ErrorMessage { get; set; }

        Form Forms { get;}
        event EventHandler Save;
        event EventHandler close;
        void ShowForm();
        void HideForm();
    }
}
