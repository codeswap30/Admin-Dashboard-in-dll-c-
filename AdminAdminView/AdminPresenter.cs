using CdwErrorMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminAdminView
{
    public class AdminPresenter
    {
        private Vadmin adminview;
        private AdminRepositories adminrespositories;
        private BindingSource adminBindingSource;
        private IEnumerable<AdminModel> adminlist;
        private int level;

        public AdminPresenter(Vadmin vadmin,AdminRepositories adminRepositories, int level=0)
        {
            try
            {
                this.level = level;
                adminBindingSource = new BindingSource();

                this.adminview = vadmin;
                this.adminrespositories = adminRepositories;

                this.adminview.ShowRecord(adminBindingSource);
                this.adminview.SearchButton += SearchAdmin;
                this.adminview.AddButton += AddAdmin;
                this.adminview.Updates += UpdateAdmin;
                this.adminview.Delete += DeleteAdmin;

                DisplayAdminList();
            }
            catch(Exception err)
            {
                if (this.level == 2 || this.level == 4 || this.level == 5)
                {
                    this.adminview.IsError = true;
                    this.adminview.ErrorLevel = this.level;
                    this.adminview.ErrorMessage = err.Message;
                }
                else
                {
                    MainClassLogger.LoadLog(this.level, 2).LogMessage(err.Message);
                }
            }
        }


        
        private void AddAdmin(object sender, EventArgs e)
        {
            try
            {
                VadminForm updateAdmin = AddAdminForm.GetInstance();
                var adminpdate = new AddAdminPresenter(adminrespositories, updateAdmin, adminBindingSource,0, this.level);
                adminpdate.LoadUpdateForm(this.adminview);
                adminpdate.ShowAdminForm();
            }
            catch(Exception err)
            {
                if (this.level == 2 || this.level == 4 || this.level == 5)
                {
                    this.adminview.IsError = true;
                    this.adminview.ErrorLevel = this.level;
                    this.adminview.ErrorMessage = err.Message;
                }
                else
                {
                    MainClassLogger.LoadLog(this.level, 2).LogMessage(err.Message);
                }
            }
            
        }

        private void DeleteAdmin(object sender, EventArgs e)
        {
            try
            {
                DisplayAdminList();
                var currentForm = (AdminModel)adminBindingSource.Current;

                if (currentForm == null)
                    throw new Exception("Something went wrong");

                adminrespositories.Delete(new AdminModel()
                {
                    Username = currentForm.Username
                });

                if(MessageBox.Show("User deleted","Complete",MessageBoxButtons.OK,MessageBoxIcon.Information) == DialogResult.OK) 
                    DisplayAdminList();
            }
            catch(Exception err)
            {
                if (this.level == 2 || this.level == 4 || this.level == 5)
                {
                    this.adminview.IsError = true;
                    this.adminview.ErrorLevel = this.level;
                    this.adminview.ErrorMessage = err.Message;
                }
                else
                {
                    MainClassLogger.LoadLog(this.level, 2).LogMessage(err.Message);
                }
            }
            
        }

        private void UpdateAdmin(object sender, EventArgs e)
        {
            try
            {
                DisplayAdminList();
                VadminForm updateAdmin = AddAdminForm.GetInstance();
                var adminpdate = new AddAdminPresenter(adminrespositories, updateAdmin, adminBindingSource,1, this.level);
                adminpdate.LoadUpdateForm((AdminModel)adminBindingSource.Current, this.adminview);
                adminpdate.ShowAdminForm();
                //DisplayAdminList();
            }
            catch(Exception err)
            {
                if (this.level == 2 || this.level == 4 || this.level == 5)
                {
                    this.adminview.IsError = true;
                    this.adminview.ErrorLevel = this.level;
                    this.adminview.ErrorMessage = err.Message;
                }
                else
                {
                    MainClassLogger.LoadLog(this.level, 2).LogMessage(err.Message);
                }
            }

        }

        private void SearchAdmin(object o,EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.adminview.SearchText))
                    throw new Exception("Empty field can not be submitted");
                    adminlist = adminrespositories.GetSelectData(new AdminModel()
                    {
                        Username = this.adminview.SearchText
                    });
                DisplayAdminList(adminlist);
            }
            catch(Exception err)
            {
                if (this.level == 2 || this.level == 4 || this.level == 5)
                {
                    this.adminview.IsError = true;
                    this.adminview.ErrorLevel = this.level;
                    this.adminview.ErrorMessage = err.Message;
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
                this.adminview.ShowRecord(adminBindingSource);
                adminlist = adminrespositories.GetAllData();
                adminBindingSource.DataSource = adminlist;
            }
            catch(Exception err)
            {
                MainClassLogger.LoadLog(this.level, 2).LogMessage(err.Message);
            }

        }

        private void DisplayAdminList(IEnumerable<AdminModel> adminModels)
        {
            try
            {
                this.adminview.ShowRecord(adminBindingSource);
                adminBindingSource.DataSource = adminModels;
            }
            catch (Exception err)
            {
                MainClassLogger.LoadLog(this.level, 2).LogMessage(err.Message);
            }
            
        }
    }
}
