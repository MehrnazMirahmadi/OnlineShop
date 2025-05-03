namespace OnlineShop.API.Extensions
{
    public static class NationalCodeHelper
    {
        public static string NormalizeNationalCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                return code;

            // فقط عدد نگه دار
            var digitsOnly = new string(code.Where(char.IsDigit).ToArray());

            // اگر کمتر از 10 رقم بود با صفر پر کن
            return digitsOnly.PadLeft(10, '0');
        }
    }
}
