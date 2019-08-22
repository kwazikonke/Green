using System;
using System.ComponentModel.DataAnnotations;

namespace Green.Models
{
    public class IdNumberValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (Convert.ToString(value).Length==13)
            {
                Idnumber bj = new Idnumber();
                // int days = DateTime.DaysInMonth(year, month)
                string idnumber = Convert.ToString(value);
                int year = Convert.ToInt32(idnumber.Substring(0, 2));
                int month = Convert.ToInt32(idnumber.Substring(2, 2));
                int days = Convert.ToInt32(idnumber.Substring(4, 2));

                if (year > 30)
                    year = Convert.ToInt32("19" + year);

                else
                    year = Convert.ToInt32("20" + year);


                if ( month <= 12 )
                {
                    int daysvalue = DateTime.DaysInMonth(year, month);
                    if (daysvalue >= days)
                        return true;
                    else
                        return false;
                }
                else
                    return false;

            }
                return false;
        }
    }
}
