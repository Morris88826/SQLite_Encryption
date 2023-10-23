using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEncryption.EncryptedEntity
{
    public class EncryptedUser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? ID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EmailAddress { get; set; }
        public string IdentityNumber { get; set; }
        public string Height { get; set; }
        public string LuckeyNumber { get; set; }
        public string Birthday { get; set; }

        // Parameterless constructor
        public EncryptedUser()
        { }

        public EncryptedUser(string firstname, string lastname, string emailAddress, string identityNumber, string height, string luckeyNumber, string birthday, int? id=null)
        {
            Firstname = firstname;
            Lastname = lastname;
            EmailAddress = emailAddress;
            IdentityNumber = identityNumber;
            Height = height;
            LuckeyNumber = luckeyNumber;
            Birthday = birthday;

            ID = id;
            
        }

        public Entity.User Decrypted()
        {
            string decrypted_firstname = AESCryptography.Decrypt(Firstname);
            string decrypted_lastname = AESCryptography.Decrypt(Lastname);
            string decrypted_emailaddress = AESCryptography.Decrypt(EmailAddress);
            string decrypted_identityNumber = AESCryptography.Decrypt(IdentityNumber);
            double decrypted_height = AESCryptography.DecryptDouble(Height);
            int decrypted_luckeyNumber = AESCryptography.DecryptInt(LuckeyNumber);
            DateTime decrypted_birthday = AESCryptography.DecryptDateTime(Birthday);

            return new Entity.User(decrypted_firstname, decrypted_lastname, decrypted_emailaddress, decrypted_identityNumber, decrypted_height, decrypted_luckeyNumber, decrypted_birthday, ID);
        }
    }
}
