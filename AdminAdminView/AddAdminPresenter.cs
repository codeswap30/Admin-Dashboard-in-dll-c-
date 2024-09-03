using CdwErrorMessage;
using CodedwapSystemEngine;
using DbHelperClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminAdminView
{
    public class AddAdminPresenter
    {
        private AdminRepositories adminRepositories;
        private VadminForm VadminForm;
        private Vadmin admin;
        private BindingSource adminBindingSource;
        private IEnumerable<AdminModel> adminlist;
        private int active;
        private int level;
        public AddAdminPresenter(AdminRepositories adminRepositories, VadminForm vadminForm, BindingSource bindingSource, int active,int level=0)
        {
            try
            {
                this.level = level;
                this.adminBindingSource = bindingSource;

                this.adminRepositories = adminRepositories;
                this.VadminForm = vadminForm;
                this.active = active;

                this.VadminForm.Active = this.active == 1 ? "Update" : "New";
                this.VadminForm.Save += Save;
                this.VadminForm.close += Closes;
            }catch(Exception err)
            {
                if (this.level == 2 || this.level == 4 || this.level == 5)
                {
                    this.VadminForm.IsError = true;
                    this.VadminForm.ErrorLevel = this.level;
                    this.VadminForm.ErrorMessage = err.Message;
                }
                else
                {
                    MainClassLogger.LoadLog(this.level, 2).LogMessage(err.Message);
                }
            }
            
        }

        private void Closes(object sender, EventArgs e)
        {
            try
            {
                this.HideAdminForm();
            }
            catch(Exception err)
            {
                if (this.level == 2 || this.level == 4 || this.level == 5)
                {
                    this.VadminForm.IsError = true;
                    this.VadminForm.ErrorLevel = this.level;
                    this.VadminForm.ErrorMessage = err.Message;
                }
                else
                {
                    MainClassLogger.LoadLog(this.level, 2).LogMessage(err.Message);
                }
            }
            
        }

        private void DisplayAdminList()
        {
            try
            {
                adminlist = adminRepositories.GetAllData();
                adminBindingSource.DataSource = adminlist;
            }
            catch(Exception err)
            {
                if (this.level == 2 || this.level == 4 || this.level == 5)
                {
                    this.VadminForm.IsError = true;
                    this.VadminForm.ErrorLevel = this.level;
                    this.VadminForm.ErrorMessage = err.Message;
                }
                else
                {
                    MainClassLogger.LoadLog(this.level, 2).LogMessage(err.Message);
                }
            }
        }

        public void LoadUpdateForm(Vadmin vadmin)
        {
            try
            {
                this.admin = vadmin;
            }
            catch(Exception err)
            {
                if (this.level == 2 || this.level == 4 || this.level == 5)
                {
                    this.VadminForm.IsError = true;
                    this.VadminForm.ErrorLevel = this.level;
                    this.VadminForm.ErrorMessage = err.Message;
                }
                else
                {
                    MainClassLogger.LoadLog(this.level, 2).LogMessage(err.Message);
                }
            }

        }

        public void LoadUpdateForm(AdminModel current,Vadmin vadmin)
        {
            try
            {
                this.admin = vadmin;

                if (current == null)
                    throw new Exception("error occured");

                var currentAdmin = current;
                this.VadminForm.Fullname = currentAdmin.FullName;
                this.VadminForm.Username = currentAdmin.Username;
                this.VadminForm.Password = currentAdmin.Password;
            }
            catch(Exception err)
            {
                if (this.level == 2 || this.level == 4 || this.level == 5)
                {
                    this.VadminForm.IsError = true;
                    this.VadminForm.ErrorLevel = this.level;
                    this.VadminForm.ErrorMessage = err.Message;
                }
                else
                {
                    MainClassLogger.LoadLog(this.level, 2).LogMessage(err.Message);
                }
            }
            
        }

        private void Save(object sender, EventArgs e)
        {
            try
            {
                var adminModel = new AdminModel();

                this.admin.ShowRecord(this.adminBindingSource);

                adminModel.FullName = this.VadminForm.Fullname;
                adminModel.Username = this.VadminForm.Username;
                adminModel.Password = this.VadminForm.Password;
                adminModel.Active = this.VadminForm.Active;
                //ModelValidator.ValidationMethod(adminModel);

                if (this.adminRepositories.CheckExistAdmin(this.VadminForm.Username))
                    throw new Exception($"{this.VadminForm.Username} already exist");

                if(adminModel.Active == "New")
                {
                    if (FormValidation.Validations(this.VadminForm.Forms))
                    {
                        this.adminRepositories.Insert(adminModel);
                        MessageBox.Show("Data Inserted!");
                        FormValidation.Reset_Form(this.VadminForm.Forms);
                    }
                        
                }
                else
                {
                    if (FormValidation.Validations(this.VadminForm.Forms))
                    {
                        this.adminRepositories.Update(adminModel);
                        MessageBox.Show("Record Updated!");
                        FormValidation.Reset_Form(this.VadminForm.Forms);
                    }
                    
                }
                DisplayAdminList();
            }
            catch(Exception err)
            {
                
               this.VadminForm.IsError = true;
               this.VadminForm.ErrorLevel = 2;
               this.VadminForm.ErrorMessage = err.Message;
            }
        }

        public void ShowAdminForm()
        {
            try
            {
                this.VadminForm.ShowForm();
            }
            catch(Exception err)
            {
                if (this.level == 2 || this.level == 4 || this.level == 5)
                {
                    this.VadminForm.IsError = true;
                    this.VadminForm.ErrorLevel = this.level;
                    this.VadminForm.ErrorMessage = err.Message;
                }
                else
                {
                    MainClassLogger.LoadLog(this.level, 2).LogMessage(err.Message);
                }
            }
            
        }

        public void HideAdminForm()
        {
            try
            {
                this.VadminForm.HideForm();
            }catch(Exception err)
            {
                if (this.level == 2 || this.level == 4 || this.level == 5)
                {
                    this.VadminForm.IsError = true;
                    this.VadminForm.ErrorLevel = this.level;
                    this.VadminForm.ErrorMessage = err.Message;
                }
                else
                {
                    MainClassLogger.LoadLog(this.level, 2).LogMessage(err.Message);
                }
            }

        }
    }
}
