using Brädhörnan_laboration.Enum;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;


namespace Brädhörnan_laboration.Models;

public class Member
{
    private Member()
    {

    }

    private const int NameMinLength = 2;
    private const int NameMaxLength = 50;
    private const int EmailMaxLength = 254;
    private const int PhoneMaxLength = 20;

    [Key]
    public int MemberNumber { get; set; }
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string Phone { get; private set; } = null!;
    public DateTime RegistrationDate { get; init; }
    public MemberRoleEnum Role { get; private set; }
    public MemberStatusEnum Status { get; private set; }

    public string FullName => $"{FirstName} {LastName}";
    public Member(
        int memberNumber,
        string firstName,
        string lastName,
        string email,   
        MemberStatusEnum status,
        MemberRoleEnum role,
        string phone = "")
    {
        //if (memberNumber <= 0) 
        //    throw new ArgumentOutOfRangeException(
        //        nameof(memberNumber),
        //        "Medlemsnummer måste vara positivt");

        MemberNumber = memberNumber;

        FirstName = ValidateName(firstName, nameof(firstName));
        LastName = ValidateName(lastName, nameof(lastName));
        Email = ValidateEmail(email);
        Phone = ValidatePhone(phone);
        RegistrationDate = DateTime.UtcNow;
        Role = role;
        Status = status;
    }
    private string ValidateName(string value, string paramName)
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
        if (!Regex.IsMatch(value, @"^[a-zA-ZåäöÅÄÖ\s-]+$"))
        {
            throw new ArgumentException(
                "Namn får endast innehålla bokstäver.");
        }
        return value;
    }
    private string ValidateEmail(string email)
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
    private string ValidatePhone(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            return "";

        phone = Regex.Replace(phone, @"[^\d+() -]", "").Trim();

        if (phone.Length > PhoneMaxLength)
            throw new ArgumentOutOfRangeException(
                nameof(phone),
                $"Telefonnummer får max vara {PhoneMaxLength} tecken.");
        return phone;
    }
  
    public void UpdateName(string firstName, string lastName)
    {
        FirstName = ValidateName(firstName, nameof(firstName));
        LastName = ValidateName(lastName, nameof(lastName));
    }
    public void UpdateEmail(string email)
    { 
        Email = ValidateEmail(email);       
    }
    public void UpdatePhone(string phone)
    {
        Phone = ValidatePhone(phone);
    }
    public void UpdateRole(MemberRoleEnum role)
    {
        Role = role;
    }
    public void UpdateStatus(MemberStatusEnum status)
    {
        Status = status;
    }
    public override string ToString()
    {
        return $"ID: {MemberNumber} Medlem sedan: {RegistrationDate} - Förnamn: {FirstName} - Efternamn: {LastName} Status: ({Status}) - Roll: {Role}";
    }
}