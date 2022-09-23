namespace My_Blog_Website.Areas.Admin.Models
{
    public class Authors
    {
        public Authors(string firstName, string lastName, string username, string email, string aboutAuth)
        {
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            Email = email;
            AboutAuth = aboutAuth;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string AboutAuth { get; set; }
    }
}
