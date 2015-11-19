using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

Namespace StaffReg
{
    [Serializable]
     public Class staffReg.cs

   {    
             string staffId;
             string staffFirstName;
             string staffLastName;
             string contactNumber;
             string email;
             int bedNumber;


            public entrydata(string txtstaffIdtxtbox, string txtstaffFirstNametxtbox, string staffLastNametxtbox, string ContactNumbertxtbox, string emailTxtBox, int bedNumber)
            {
                staffId = txtstaffIdtxtbox;
                this.StaffFirstName = txtstaffFirstNametxtbox;
                this.staffLastName = staffLastNametxtbox;
                this.ContactNumber = ContactNumbertxtbox;
                this.Email = emailTxtBox;
                this.bedNumber = bedNumber;

        StreamWriter outputStream = File.AppendText(@"staffReg.txt");
                        string staffDetails = entry.staffId + "," + entry.staffFirstName + "," + entry.staffLastName + "," + entry.ContactNumber + "," + entry.Email + "," + entry.bedNumber;
                        outputStream.WriteLine(guestdetails);
                        outputStream.Close();
            }

       
    }Class Application

