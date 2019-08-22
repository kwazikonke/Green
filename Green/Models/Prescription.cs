using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Green.Models
{
    public class Prescription
    {
        [Key]
        public int PrescriptionID { get; set; }
        public int AppointmentID { get; set; }
        public int DoctorID { get; set; }
        public string MedicineName { get; set; }
        [Display(Name = "Picture")]
        public byte[] Picture { get; set; }
        public string Dosage_Information { get; set; }
        public string UserID { get; set; }        
        public virtual ICollection<Medicine> Medicines { get; set; }
        [ForeignKey("AppointmentID")]

        public virtual AppointmentModel AppointmentModel { get; set; }

    }
    public class Medicine
    {
        [Key]
        public int MedicineID { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 3)]
        [Display(Name = "Medicine's Name")]
        public string MedicineName { get; set; }
        [Required]
        public Enums.Group Group { get; set; }

        [Display(Name = "Picture")]
        public byte[] Picture { get; set; }
        [Display(Name = "Item Price")]
        [DataType(DataType.Currency)]
        public decimal MedPrice { get; set; }
        [DisplayName("Quantity on Hand")]
        public int QOH { get; set; }
        [Display(Name = "Image")]
        public byte[] image { get; set; }

        public string Dosage_Information { get; set; }
        public int PrescriptionID { get; set; }
        public virtual Prescription Prescription { get; set; }

        //public virtual List<Prescription> Prescription { get; set; }
        //public virtual List<AppointmentModel> AppointmentModel { get; set; }
        //public virtual List<Rooms> Rooms { get; set; }
    }

}