using EntityFrameworkCore.EncryptColumn.Attribute;
using System;

namespace DatabaseEncryption.Entity
{
    public class User
    {
        public int? ID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string EmailAddress { get; set; }
        public string IdentityNumber { get; set; }
        public double Height { get; set; }
        public int LuckeyNumber { get; set; }
        public DateTime Birthday { get; set; }  
        public User(string firstname, string lastname, string emailAddress, string identityNumber, double height, int luckeyNumber, DateTime birthday, int? id=null)
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

        public EncryptedEntity.EncryptedUser Encrypted()
        {
            string encrypted_firstname = AESCryptography.Encrypt(Firstname);
            string encrypted_lastname = AESCryptography.Encrypt(Lastname);
            string encrypted_emailaddress = AESCryptography.Encrypt(EmailAddress);
            string encrypted_identityNumber = AESCryptography.Encrypt(IdentityNumber);
            string encrypted_height = AESCryptography.EncryptDouble(Height);
            string encrypted_luckeyNumber = AESCryptography.EncryptInt(LuckeyNumber);
            string encrypted_birthday = AESCryptography.EncryptDateTime(Birthday);

            return new EncryptedEntity.EncryptedUser(encrypted_firstname, encrypted_lastname, encrypted_emailaddress, encrypted_identityNumber, encrypted_height, encrypted_luckeyNumber, encrypted_birthday);
        }
    }
}
