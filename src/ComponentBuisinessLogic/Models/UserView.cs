using System;
using System.Collections.Generic;
using ComponentBuisinessLogic;

#nullable disable

namespace ComponentBuisinessLogic
{
    public class UserView
    {
        public UserView(string _login = "",
            string _name_ = "",
            string _surname = null)
        {
            Login = _login;
            Name_ = _name_;
            Surname = _surname;
        }

        public string Login { get; set; }
        public string Name_ { get; set; }
        public string Surname { get; set; }
    }
}
