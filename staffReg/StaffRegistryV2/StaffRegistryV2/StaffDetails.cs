using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffRegistryV2
{
    [Serializable]
    public class StaffDetails
    {
        public string StaffID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }

        public StaffDetails(string staffID, string firstName, string lastName, string contactNumber, string email)
        {
            StaffID = staffID;
            FirstName = firstName;
            LastName = lastName;
            ContactNumber = contactNumber;
            Email = email;



        }

    }
}
