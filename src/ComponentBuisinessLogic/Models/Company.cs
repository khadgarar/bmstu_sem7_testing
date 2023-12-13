#nullable disable

namespace ComponentBuisinessLogic
{
    public class Company
    {
        public Company(int _companyid = 1, string _title = "", int _foundationyear = 0)
        {
            Companyid = _companyid;
            Title = _title;
            Foundationyear = _foundationyear;
        }

        public int Companyid { get; }
        public string Title { get; }
        public int Foundationyear { get; }
    }
}
