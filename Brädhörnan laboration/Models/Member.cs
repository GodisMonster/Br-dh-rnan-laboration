using Brädhörnan_laboration.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Brädhörnan_laboration.Models;

public class Member
{
    public int MemberNumber { get; private set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime RegistrationDate { get; private set; }
    public MemberRoleEnum Role { get; set; }
    public MemberStatusEnum Status { get; set; }

    public Member(int memberNumber, string firstName, string lastName, string email, string phone = "")
    {

        if (string.IsNullOrEmpty(firstName))
            throw new ArgumentNullException("Får inte vara tom");
        firstName = firstName.Trim();
        if (firstName.Length < 2 || firstName.Length > 50)
            throw new ArgumentException("First must be 2-50 characters.");



        if (string.IsNullOrEmpty(lastName))
            throw new ArgumentNullException("Får inte vara tom");

        lastName = lastName.Trim();
        if (lastName.Length < 2 || lastName.Length > 50)
            throw new ArgumentException("Last name must be 2-50 characters.");

        if (string.IsNullOrEmpty(email))
            throw new ArgumentNullException("Får inte vara tom");
        email = email.Trim();
        if (email.Length == 0 || email.Length > 254)
            throw new ArgumentException("Email must be 1-254 characters.");

        if (phone?.Length > 20)
            throw new ArgumentException("Phone number must be 20 characters or less");

        phone = Regex.Replace(phone ?? "", @"[^\d+() -]", "");

        if (memberNumber <= 0)
            throw new ArgumentOutOfRangeException(nameof(memberNumber), "Member number must be positive.");


        MemberNumber = memberNumber;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        RegistrationDate = DateTime.Now;
        Role = MemberRoleEnum.Member;
        Status = MemberStatusEnum.Active;


    }
}