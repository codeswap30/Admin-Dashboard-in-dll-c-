using CdwErrorMessage;
using CdwGeneralForm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminAdminView
{
    public partial class Admin : SampleUsers, Vadmin
    {
        private static Admin Instance = null;
        public Admin()
        {
            InitializeComponent();
            PraiseButtonEvent();
        }

        public static Admin GetInstance()
        {
            if (Instance == null || Instance.IsDisposed)
                Instance = new Admin();
            else
            {
                if (Instance.WindowState == FormWindowState.Minimized)
                    Instance.WindowState = FormWindowState.Normal;
            }

            return Instance;
        }
        public string SearchText { get => TxtSearch.Texts; set => TxtSearch.Texts = value; }

        public Form Forms => this;

        private bool isErr;
        private int level;
        private string messageerror;
        public bool IsError { get => isErr; set => isErr = value; }
        public int ErrorLevel { get => level; set => level = value; }
        public string ErrorMessage
        {
            get
            {
                //LoadError();
                return messageerror;
            }
            set
            {
                messageerror = value;
                LoadError();
            }
        }

        public event EventHandler SearchButton;
        public event EventHandler AddButton;
        public event EventHandler Delete;
        public event EventHandler Updates;

        private void LoadError()
        {
            if (isErr == true)
            {
                MainClassLogger.LoadLog(level, 2).LogMessage(messageerror);
                isErr = false;
            }

        }
        private void PraiseButtonEvent()
        {
            BtnSearch.Click += delegate
            {
                SearchButton?.Invoke(this, EventArgs.Empty);
            };
            BtnAdd.Click += delegate
            {
                AddButton?.Invoke(this, EventArgs.Empty);
            };
            BtnDel.Click += delegate
            {
                if(MessageBox.Show("Are you sure, you want to delete this user information!","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                    Delete?.Invoke(this, EventArgs.Empty);
            };
            BtnUpdate.Click += delegate
            {
                Updates?.Invoke(this, EventArgs.Empty);
            };
        }

        public void ShowRecord(BindingSource mode)
        {
            this.ViewAdmin.DataSource = mode;
        }

        private void ViewAdmin_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
