using Brädhörnan_laboration.Enum;
using System;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Markup.Localizer;


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
    public DateTime RegistrationDate { get; init; } // Borde inte kunna ändras alls?
    public MemberRoleEnum Role { get; private set; }
    public MemberStatusEnum Status { get; private set; }

    public Member(
        int memberNumber,
        string firstName,
        string lastName,
        string email,
        string phone = "",
        string status = "",
        string roll= "")
    {

        if (memberNumber <= 0) 
            throw new ArgumentOutOfRangeException(
                nameof(memberNumber),
                "Medlemsnummer måste vara positivt");

        MemberNumber = memberNumber;


        FirstName = ValidateName(firstName, nameof(firstName));
        LastName = ValidateName(lastName, nameof(lastName));
        Email = ValidateEmail(email);
        Phone = ValidatePhone(phone);
        RegistrationDate = DateTime.UtcNow;

        switch (roll)
        {
            case "Admin": Role = MemberRoleEnum.Admin; break;

            case "Member": Role = MemberRoleEnum.Member; break;

            case "Organizer": Role = MemberRoleEnum.Organizer; break;

            default:
                break;

        }
        switch (status)
        {
            case "Actice": Status = MemberStatusEnum.Active; break;
            case "Inactive": Status = MemberStatusEnum.Inactive; break;
        }
        

    }
    public string ValidateName(string value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(paramName,
                "Får inte vara tom");

        value = value.Trim();

        if (value.Length < NameMinLength ||
            value.Length > NameMaxLength)
        {
            throw new ArgumentOutOfRangeException(
                paramName,
                $"Namn måste vara {NameMinLength}-{NameMaxLength} tecken.");  
        }
        return value;
    }
    public string ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentNullException(nameof(email),
                "Får inte vara tom");

        email = email.Trim();

        if (email.Length > EmailMaxLength)
            throw new ArgumentOutOfRangeException(
                nameof(email),
                "Email är för långt");

        if (!Regex.IsMatch(
            email,
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {

            throw new ArgumentException(
                $"Email-adressen '{email}' har ogiltigt format.",
                nameof(email));
        }
        return email;  
    }
    public string ValidatePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            return "";

        phone = Regex.Replace(phone, @"[^\d+() -]", "").Trim();

        if (phone.Length > PhoneMaxLength)
            throw new ArgumentOutOfRangeException(
                nameof(phone),
                $"Telefonnummer får max vara {PhoneMaxLength} tecken.");

        if (phone.Length < 8)  
            throw new ArgumentOutOfRangeException(
                nameof(phone),
                "Telefonnummer måste ha minst 8 siffror.");

        return phone;
    }
    public override string ToString()
    {
        return $"ID: {MemberNumber} Medlem sedan: {RegistrationDate} - Förnamn: {FirstName} Efternamn: {LastName} Status: ({Status}) Roll: {Role}";
    }

    public void UpdateName(string firstName, string lastName)
    {
        firstName = ValidateName(firstName, nameof(firstName));
        lastName = ValidateName(lastName, nameof(lastName));
    }
    public void UpdateEmail(string email)
    {
        {
            email = ValidateEmail(email);
        }
    }
    public void UpdatePhone(string phone)
    {
        Phone=ValidatePhone(phone);
    }
    public void UpdateRole(MemberRoleEnum role)
    {
        Role = role;
    }
    public void UpdateStatus(MemberStatusEnum status)
    {
        Status =status;
    }
}