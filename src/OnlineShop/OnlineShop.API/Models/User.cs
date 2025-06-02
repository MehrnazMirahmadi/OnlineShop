using OnlineShop.API.Exceptions;

namespace OnlineShop.API.Models;

public class User
{
    public int Id { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string NationalCode { get; private set; } = string.Empty;
    public string PhoneNumber { get; private set; } = string.Empty;
    public string Password { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }
   // public bool IsDelete { get; private set; }
    public string TrackingCode { get; private set; } = string.Empty;

    private User(string firstName, string lastName, string nationalCode, string phoneNumber, string password, string trackingCode)
    {
        SetFirstName(firstName);
        SetLastName(lastName);
        SetNationalCode(nationalCode);
        SetPhoneNumber(phoneNumber);
        SetPassword(password);
        SetIsActive(true);
       // SetIsDeleted(false);
        SetTrackingCode(trackingCode);

    }
    // متد ساخت شیء (Factory Method)
    public static User Create(string firstName, string lastName, string nationalCode, string phoneNumber, string password, string trackingCode)
    {
        return new User(firstName, lastName, nationalCode, phoneNumber, password,trackingCode);
    }

    // متد بروزرسانی اطلاعات کاربر
    public void Update(string firstName, string lastName, string phoneNumber, string nationalCode, bool isActive,bool isDelete)
    {
        SetFirstName(firstName);
        SetLastName(lastName);
        SetPhoneNumber(phoneNumber);
        SetNationalCode(nationalCode);
        SetIsActive(isActive);
        //SetIsDeleted(isDelete);
       
    }

    // متد تغییر رمز عبور
    public void ChangePassword(string newPassword)
    {
        SetPassword(newPassword);
    }

    // متدهای اعتبارسنجی داخلی
    private void SetFirstName(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new BadRequestException("نام نمی‌تواند خالی باشد.");
        FirstName = firstName;
    }

    private void SetLastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
            throw new BadRequestException("نام خانوادگی نمی‌تواند خالی باشد.");
        LastName = lastName;
    }

    private void SetNationalCode(string code)
    {
        if (!System.Text.RegularExpressions.Regex.IsMatch(code, @"^\d{10}$"))
            throw new BadRequestException("کد ملی نامعتبر است.");
        NationalCode = code;
    }

    private void SetPhoneNumber(string phone)
    {
        if (!System.Text.RegularExpressions.Regex.IsMatch(phone, @"^(?:\+98|0098|0)?9\d{9}$"))
            throw new BadRequestException("شماره موبایل نامعتبر است.");
        PhoneNumber = phone;
    }

    private void SetPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password) || password.Length < 6)
            throw new BadRequestException("رمز عبور باید حداقل ۶ کاراکتر باشد.");
        Password = password;
    }

    private void SetIsActive(bool isActive)
    {
        IsActive = isActive;
    }
   /* private void SetIsDeleted(bool isDelete)
    {
        IsDelete = isDelete;
    }
    public void SoftDelete()
    {
        IsDelete = true;
    }*/
    private void SetTrackingCode(string trackingCode)
    {
        TrackingCode = trackingCode;
    }
}
