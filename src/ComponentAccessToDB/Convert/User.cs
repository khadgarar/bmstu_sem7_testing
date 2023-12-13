using System;
using System.Collections.Generic;
using ComponentBuisinessLogic;

#nullable disable

namespace ComponentAccessToDB
{
    public partial class UserConv
    {
        public static UserDB BltoDB(User a_bl)
        {
            return new UserDB
            {
                Login = a_bl.Login,
                Password_ = a_bl.Password_,
                Name_ = a_bl.Name_,
                Surname = a_bl.Surname
            };
        }

        public static User DBtoBL(UserDB a_bl)
        {
            return new User
            (
                _login: a_bl.Login,
                _password_: a_bl.Password_,
                _name_: a_bl.Name_,
                _surname: a_bl.Surname
            );
        }
    }
}
