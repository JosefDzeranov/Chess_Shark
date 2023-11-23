namespace Sachy_Obrazky
{
    internal struct AuthenticationData
    {
        public AuthenticationData(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; }
        public string LastName { get; set; }
    }
}