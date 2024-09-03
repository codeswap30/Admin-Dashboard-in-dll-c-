using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminAdminView
{
    public class AdminModel
    {
        [DisplayName("#")]
        public int Id { get; set; }
        [DisplayName("Admin Name")]
        [Required(ErrorMessage = "Admin Name is require")]
        public string FullName { get; set; }
        [DisplayName("Username")]
        [Required(ErrorMessage = "Username is require")]
        public string Username { get; set; }
        [DisplayName("Password")]
        [Required(ErrorMessage = "Password is require")]
        public string Password { get; set; }
        [DisplayName("Active")]
        [Required(ErrorMessage = "Please select active user or inactive user")]
        public string Active { get; set; }
    }
}
