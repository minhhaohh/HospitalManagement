namespace Hospital.Domain.Models
{
    public class Patient
    {
        public string ChartNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime Dob { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string WardCode { get; set; }

        public Ward Ward { get; set; }

        public string DistrictCode { get; set; }

        public District District { get; set; }

        public string ProvinceCode { get; set; }

        public Province Province { get; set; }

        public string ZipCode { get; set; }

        public Patient() { }

        public Patient(string chartNumber, string firstName, string lastName, string gender,
            DateTime dob, string phone, string email, string address, string wardCode,
            string districtCode, string provinceCode, string zipCode)
        {
            ChartNumber = chartNumber;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Phone = phone;
            Email = email;
            Address = address;
            WardCode = wardCode;
            DistrictCode = districtCode;
            ProvinceCode = provinceCode;
            ZipCode = zipCode;
        }
    }
}
