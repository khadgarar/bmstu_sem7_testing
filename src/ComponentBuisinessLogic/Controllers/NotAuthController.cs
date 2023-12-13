namespace ComponentBuisinessLogic
{
    public class NotAuthController
    {
        protected IUserRepository UserRepository;
        public NotAuthController(IUserRepository UserRep)
        {
            UserRepository = UserRep;
        }
        public User GetUserByLogin(string login)
        {
            return UserRepository.GetUserByLogin(login);
        }
        public bool AddUser(string login, string password_, string name_, string surname)
        {
            User user = new User(_login: login,
                                 _password_: password_,
                                 _name_: name_,
                                 _surname: surname);
            UserRepository.Add(user);
            return true;
        }
    }
}
