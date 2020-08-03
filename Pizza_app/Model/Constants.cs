using Validations;

namespace Pizza_app.Model
{
    class Constants
    {
        private const string passwordEmployee = "Zaposleni";
        internal readonly string passwordEmployeeHashed = SecurePasswordHasher.Hash(passwordEmployee);
        private const string passwordGuest = "Gost";
        internal readonly string passwordGuestHashed = SecurePasswordHasher.Hash(passwordGuest);
    }
}
