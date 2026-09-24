namespace company.Models
{
    public class EmployeeModel
    {
        public long id { get; set; }
        public string nik { get; set; }

        public string name { get; set; }
        public string email { get; set; }
        public string division { get; set; }
        public decimal salary { get; set; }
        public DateTime updated_at { get; set; }
    }
}
