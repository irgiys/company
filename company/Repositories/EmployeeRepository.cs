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
        public EmployeeModel GetById(long id)
        {
            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand("SELECT * FROM Employee WHERE id=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);
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
        public bool Create(EmployeeModel emp)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                using var cmd = new SqlCommand(@"
                    INSERT INTO employee (nik,name,email,division,salary)
                    VALUES (@nik,@name,@email,@division,@salary)
                    ", conn);
                cmd.Parameters.AddWithValue("@nik", emp.nik);
                cmd.Parameters.AddWithValue("@name", emp.name);
                cmd.Parameters.AddWithValue("@email", emp.email);
                cmd.Parameters.AddWithValue("@division", emp.division);
                cmd.Parameters.AddWithValue("@salary", emp.salary);
                conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Delete(long id)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                using var cmd = new SqlCommand("DELETE employee where id=@id", conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Update(EmployeeModel emp)
        {
            try
            {
                using var conn = new SqlConnection(_connectionString);
                using var cmd = new SqlCommand(@"
                UPDATE employee SET nik = @nik, name = @name, email = @email, 
                division = @division, salary = @salary WHERE id = @id",conn);
                
                cmd.Parameters.AddWithValue("@nik", emp.nik);
                cmd.Parameters.AddWithValue("@name", emp.name);
                cmd.Parameters.AddWithValue("@email", emp.email);
                cmd.Parameters.AddWithValue("@division", emp.division);
                cmd.Parameters.AddWithValue("@salary", emp.salary);
                cmd.Parameters.AddWithValue("@id", emp.id);

                conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
