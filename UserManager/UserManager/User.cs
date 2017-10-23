namespace UserManager
{
    

    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Privilege Privilege { get; set; }


        public User(string name, string email)
        {
            Name = name;
            Email = email;
            Privilege = Privilege.User;
        }        
        
        public string GetUSerInfo()
        {
            return ($"{Name} + {Email}");
        }
    }

    public enum Privilege { User, Admin }
}
