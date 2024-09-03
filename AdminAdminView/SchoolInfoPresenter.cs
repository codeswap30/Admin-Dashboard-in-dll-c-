using CdwErrorMessage;
using CodedwapSystemEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminAdminView
{
    public class SchoolInfoPresenter
    {
        private VSchoolInformation adminview;
        private AdminRepositories adminrespositories;
        private int level;
        public SchoolInfoPresenter(VSchoolInformation vadmin, AdminRepositories adminRepositories, int level)
        {
            try
            {
                this.level = level;
                this.adminview = vadmin;
                this.adminrespositories = adminRepositories;

                this.adminview.UpdateSchool += Update;

                DisplaySchoolInformation();
            }
            catch (Exception err)
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

        private void Update(object sender, EventArgs e)
        {
            try
            {
                if (FormValidation.Validations(this.adminview.Forms))
                {
                    var schoolinfo = new SchoolInfo();
                    schoolinfo.SchoolName = this.adminview.Schoolname;
                    schoolinfo.PhoneNumber = this.adminview.Phonenumber;
                    schoolinfo.Email = this.adminview.Emailaddress;
                    schoolinfo.Address = this.adminview.Address;

                    this.adminrespositories.ModifySchoolInformation(schoolinfo);
                    MessageBox.Show("School information update");

                }
            }catch(Exception err)
            {
               this.adminview.IsError = true;
               this.adminview.ErrorLevel = 2;
               this.adminview.ErrorMessage = err.Message;
                
            }
        }

        private void DisplaySchoolInformation()
        {
            try
            {
                var schoolInfos = this.adminrespositories.FetchSchoolInfo();
                this.adminview.Schoolname = schoolInfos.SchoolName;
                this.adminview.Phonenumber = schoolInfos.PhoneNumber;
                this.adminview.Emailaddress = schoolInfos.Email;
                this.adminview.Address = schoolInfos.Address;
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

    }
}
