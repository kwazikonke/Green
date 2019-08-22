﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Green.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        //[Key]
        //public int PatID { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [MyBirthDateValidation(ErrorMessage = "You cannot be from the Future!")]
        [Column(TypeName = "datetime2")]
        public DateTime BirthDate { get; set; }

        [Required]
        public Enums.Sex Sex { get; set; }

        [Display(Name = "Block User")]
        public Boolean Blocked { get; set; }

        public virtual List<AppointmentModel> Appointments { get; set; }

        public virtual List<Lab> Lab { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

  


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Green.Models.LabPractitioner> LabPractitioners { get; set; }
        public System.Data.Entity.DbSet<Green.Models.Patient> Patients { get; set; }
        public System.Data.Entity.DbSet<Green.Models.Invoice> Invoices { get; set; }
        public System.Data.Entity.DbSet<Green.Models.Lab> Labs { get; set; }
        public DbSet<AdministrationModel> Administrations { get; set; }
        public DbSet<AppointmentModel> Appointments { get; set; }
        public DbSet<MyRooms> MyRooms { get; set; }
        
        public DbSet<DoctorModel> Doctors { get; set; }
        public DbSet<Procedure> Procedures { get; set; }

        //  public System.Data.Entity.DbSet<Green.Models.Procedure> Procedures { get; set; }
        public System.Data.Entity.DbSet<Green.Models.Rooms> Rooms { get; set; }
        public System.Data.Entity.DbSet<Green.Models.ContactUs> ContactUs { get; set; }
        

        public System.Data.Entity.DbSet<Green.ViewModels.LabViewModel> LabViewModels { get; set; }

        public System.Data.Entity.DbSet<Green.ViewModels.PatientRecordViewModel> PatientRecordViewModels { get; set; }

        public System.Data.Entity.DbSet<Green.ViewModels.InvoiceViewModel> InvoiceViewModels { get; set; }

        public System.Data.Entity.DbSet<Green.Models.Prescription> Prescriptions { get; set; }

        public System.Data.Entity.DbSet<Green.Models.Medicine> Medicines { get; set; }

        public System.Data.Entity.DbSet<Green.Models.Cart> Carts { get; set; }

        public System.Data.Entity.DbSet<Green.Models.Category> Categories { get; set; }


        public System.Data.Entity.DbSet<Green.Models.Product> Products { get; set; }
    }
}