using Microsoft.VisualBasic;
using System;

namespace AMAPG4.Models
{
    public class Individual
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public DateTime InscriptionDate { get; set; }
        public bool IsContributionPaid { get; set; }
        public bool IsVolunteer { get; set; }
        UserAccount Account { get; set; }
    }
}
