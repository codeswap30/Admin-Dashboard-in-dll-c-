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
    public partial class SchoolInformation : SampleAdd, VSchoolInformation
    {
        public SchoolInformation()
        {
            InitializeComponent();
            FunctionButtonEvent();
        }

        private static SchoolInformation schoolInstance = null;

        public static SchoolInformation GetSchoolInformationInstance()
        {
            if (schoolInstance == null || schoolInstance.IsDisposed)
                schoolInstance = new SchoolInformation();
            else
            {
                if (schoolInstance.WindowState == FormWindowState.Minimized)
                    schoolInstance.WindowState = FormWindowState.Normal;

                schoolInstance.BringToFront();
            }

            return schoolInstance;
        }

        public string Schoolname { get => TxtFullname.Texts; set => TxtFullname.Texts = value; }
        public string Phonenumber { get => TxtPhoneNumber.Texts; set => TxtPhoneNumber.Texts = value; }
        public string Emailaddress { get => TxtEmailAddress.Texts; set => TxtEmailAddress.Texts = value; }
        public string Address { get => TxtAddress.Texts; set => TxtAddress.Texts = value; }

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

        public event EventHandler UpdateSchool;

        public void ShowRecord(BindingSource mode)
        {
            throw new NotImplementedException();
        }

        private void LoadError()
        {
            if (isErr == true)
            {
                MainClassLogger.LoadLog(level, 2).LogMessage(messageerror);
                isErr = false;
            }

        }

        private void FunctionButtonEvent()
        {
            BtnSave.Click += delegate
            {
                UpdateSchool?.Invoke(this, EventArgs.Empty);
            };

            BtnCLose.Click += delegate
            {
                this.Close();
            };
        }
    }
}
