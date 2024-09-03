using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminAdminView
{
    public interface Vadmin
    {
        string SearchText { get; set; }

        event EventHandler SearchButton;
        event EventHandler AddButton;
        event EventHandler Delete;
        event EventHandler Updates;

        bool IsError { get; set; }
        int ErrorLevel { get; set; }
        string ErrorMessage { get; set; }
        Form Forms { get; }

        void ShowRecord(BindingSource mode);
    }
}
