using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace OGL.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Advertistments = new HashSet<Advertisement>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        #region not mapped properties

        [NotMapped]
        [Display(Name = "Pani/Pan: ")]
        public string FullName
        {
            get { return string.Format("{0}{1}{2}", FirstName, " ", LastName); }
        }

        #endregion
        public virtual ICollection<Advertisement> Advertistments { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}