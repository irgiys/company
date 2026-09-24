using company.Models;
using Microsoft.Data.SqlClient;

namespace company.Repositories
{
    public class EmployeeRepository
    {
        private readonly string _connectionString;
        public EmployeeRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("Conn") ?? throw new ArgumentNullException("Connection string not found");
        }
        public List<EmployeeModel> GetAll()
        {
            var list = new List<EmployeeModel>();
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SELECT * FROM Employee", conn);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new EmployeeModel
                {
                    id = (long)reader["id"],
                    nik = reader["nik"].ToString()!,
                    name = reader["name"].ToString()!,
                    email = reader["email"].ToString()!,
                    division = reader["division"].ToString()!,
                    salary = (decimal)reader["salary"],
                    updated_at = (DateTime)reader["updated_at"]
                });
            }
            return list;
        }
        public EmployeeModel GetByNIK (string nik)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SELECT * FROM Employee WHERE nik=@nik", conn);
            cmd.Parameters.AddWithValue("@nik", nik);
            conn.Open();
            using var reader = cmd.ExecuteReader();
            if (!reader.Read()) return null;

            return new EmployeeModel
            {
                id = (long)reader["id"],
                nik = reader["nik"].ToString()!,
                name = reader["name"].ToString()!,
                email = reader["email"].ToString()!,
                division = reader["division"].ToString()!,
                salary = (decimal)reader["salary"],
                updated_at = (DateTime)reader["updated_at"]
            };
        }
    }
}
