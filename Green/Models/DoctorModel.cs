﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Green.Models
{
    public class DoctorModel
    {
        [Key]
        public int DoctorID { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "Doctor's Name")]
        public string Name { get; set; }
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [MyBirthDateValidation(ErrorMessage = "Date of Birth cannot be from the future!")]
        public DateTime BirthDate { get; set; }

        [Required]
        public Enums.Sex Sex { get; set; }

        [Required]
        public Enums.DepartmentDentistry Department { get; set; }
        public Enums.Qualification Degree { get; set; }

        [Display(Name = "New Appointments Disabled")]
        public Boolean DisableNewAppointments { get; set; }

        [Display(Name = "Picture")]
        public byte[] Picture { get; set; }

        public virtual List<AppointmentModel> Appointments { get; set; }
        public virtual List<Rooms> Rooms { get; set; }

       
        public string GeneratePassword()
        {
            //I started with creating three string variables.
            //This one tells you how many characters the string will contain.
            int PasswordLength = 5;

            //This one, is empty for now - but will ultimately hold the finished randomly generated password
            string NewPassword =  "";

            //This one tells you which characters are allowed in this new password
            string allowedChars = "";
            allowedChars = "1,2,3,4,5,6,7,8,9,0";
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            allowedChars += "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";

            //Then working with an array...

            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);

            string IDString = "";
            string temp = "";

            //utilize the "random" class
            Random rand = new Random();

            //and lastly - loop through the generation process...
            for (int i = 0; i < (PasswordLength); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                IDString += temp;
                NewPassword = IDString;

            }

            return NewPassword;
        }
        public ApplicationUser GetUser()
        {
            var user = new ApplicationUser()
            {
                Email = this.Email,
                UserName = this.Email,
                Name = this.Name,
                BirthDate = this.BirthDate,
                Sex = this.Sex,
                Appointments = this.Appointments,
               // Blocked = this.Blocked,
            };
            return user;
        }

    }
}
