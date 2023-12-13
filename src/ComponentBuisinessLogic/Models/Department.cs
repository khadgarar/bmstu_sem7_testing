#nullable disable

namespace ComponentBuisinessLogic
{
    public class Department
    {
        public Department(
            int _departmentid = 1,
            string _title = "",
            int _company = 1,
            int _foundationyear = 0,
            string _activityfield = "")
        {
            Departmentid = _departmentid;
            Title = _title;
            Company = _company;
            Foundationyear = _foundationyear;
            Activityfield = _activityfield;
        }

        public int Departmentid { get; }
        public string Title { get; }
        public int Company { get; }
        public int Foundationyear { get; }
        public string Activityfield { get; }
    }
}
