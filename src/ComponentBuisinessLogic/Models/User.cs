#nullable disable

namespace ComponentBuisinessLogic
{
    public class User
    {
        public User(string _login = "",
            string _password_ = "",
            string _name_ = "",
            string _surname = null)
        {
            Login = _login;
            Password_ = _password_;
            Name_ = _name_;
            Surname = _surname;
        }

        public string Login { get; set; }
        public string Password_ { get; set; }
        public string Name_ { get; set; }
        public string Surname { get; set; }
    }
}
