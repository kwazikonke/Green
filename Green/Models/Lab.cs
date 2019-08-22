using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Green.Models
{
    public class Lab
    {
        [Key]

        public int LabID { get; set; }

        public string LabName { get; set; }
        [Required]
        public string ProcedureName { get; set; }
    //    public virtual Procedure Procedure { get; set; }




        public List<LabPractitioner> LabPractitioner { get; set; }
        





        //public  byte[] SamplePicture  { get; set; }

        //public string SampleDescription { get; set; }





    }
    
}