using Brädhörnan_laboration.Enum;
using System;
using System.Text;
using System.Text.RegularExpressions;


namespace Brädhörnan_laboration.Models;

public class Member
{
    private const int NameMinLength = 2;
    private const int NameMaxLength = 50;
    private const int EmailMaxLength = 254;
    private const int PhoneMaxLength = 20;



    public int MemberNumber { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public DateTime RegistrationDate { get; private set; }
    public MemberRoleEnum Role { get; private set; }
    public MemberStatusEnum Status { get; private set; }

    public Member(int memberNumber, string firstName, string lastName, string email, string phone = "")
    {

        if (memberNumber <= 0) throw new ArgumentOutOfRangeException(nameof(memberNumber), "Medlemsnummer måste vara positivt");

        MemberNumber = memberNumber;


        FirstName = ValidateName(firstName, nameof(firstName));
        LastName = ValidateName(lastName, nameof(lastName));
        Email = ValidateEmail(email);
        Phone = ValidatePhone(phone);
        RegistrationDate = DateTime.UtcNow;
        Role = MemberRoleEnum.Member;
        Status = MemberStatusEnum.Active;

    }
    private string ValidateName(string value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(paramName, "Får inte vara tom");

        value = value.Trim();

        if (value.Length < NameMinLength || value.Length > NameMaxLength)
            throw new ArgumentOutOfRangeException(paramName, $"Namn måste vara {NameMinLength}-{NameMaxLength} tecken.");
        return value;
    }
    private string ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentNullException(nameof(email), "Får inte vara tom");

                email = email.Trim();

        if (email.Length > EmailMaxLength)
            throw new ArgumentOutOfRangeException(nameof(email), "Email är för lång");

        if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {

            throw new ArgumentException($"Email-adressen '{email}' har ogiltigt format.", nameof(email));
        }
        return email;  
    }
    private string ValidatePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            return "";

        phone = Regex.Replace(phone, @"[^\d+() -]", "").Trim();

        if (phone.Length > PhoneMaxLength)
            throw new ArgumentOutOfRangeException(nameof(phone), $"Telefonnummer får max vara {PhoneMaxLength} tecken.");

        if (phone.Length < 8)  
            throw new ArgumentOutOfRangeException(nameof(phone), "Telefonnummer måste ha minst 8 siffror.");

     
        return phone;
    }


}