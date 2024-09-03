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
    public partial class AddAdminForm : SampleAdd, VadminForm
    {
        private static AddAdminForm Instance = null;
        public AddAdminForm()
        {
            InitializeComponent();
            RaiseFunction();
        }

        public string Fullname { get => TxtFullname.Texts; set => TxtFullname.Texts = value; }
        public string Username { get => TxtUsername.Texts; set => TxtUsername.Texts = value; }
        public string Password { get => TxtPassword.Texts; set => TxtPassword.Texts = value; }
        public string Active { get => TxtActive.Text; set => TxtActive.Text = value; }

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
        public Form Forms { get => this;}

        public event EventHandler Save;
        public event EventHandler close;

        public static AddAdminForm GetInstance()
        {
            if (Instance == null || Instance.IsDisposed)
                Instance = new AddAdminForm();
            else
            {
                if (Instance.WindowState == FormWindowState.Minimized)
                    Instance.WindowState = FormWindowState.Normal;
                
            }

            return Instance;
        }

        private void LoadError()
        {
            if (isErr == true)
            {
                MainClassLogger.LoadLog(level, 2).LogMessage(messageerror);
                isErr = false;
            }

        }

        private void RaiseFunction()
        {
            BtnSave.Click += delegate
            {
                Save?.Invoke(this, EventArgs.Empty);
            };

            BtnCLose.Click += delegate
            {
                close?.Invoke(this, EventArgs.Empty);
            };
        }

        public void HideForm()
        {
            this.Dispose();
        }

        public void ShowForm()
        {
            this.ShowDialog();
        }

        private void AddAdminForm_Load(object sender, EventArgs e)
        {

        }

        private void TxtActive_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
