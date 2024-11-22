using Crud.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Crud.Controllers
{
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            List<Customer> cust = new List<Customer>();

            var conString = "Server=localhost;Database=crud;Trusted_Connection=True;MultipleActiveResultSets=true";

            using (SqlConnection conn = new SqlConnection(conString))
            {
                string query = "select * from Customer1";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cust.Add(new Customer
                    {
                        id = (int)reader["id"],
                        name = (string)reader["name"],
                        email = (string)reader["email"],
                        address = (string)reader["address"],

                    });
                }
            }
            //return Ok(cust);
            return View(cust);

        }
        [HttpPost]
        public IActionResult Create(Customer cust)
        {
            var conString = "Server=localhost;Database=crud;Trusted_Connection=True;MultipleActiveResultSets=true";
            using (SqlConnection conn = new SqlConnection(conString))
            {
                string query = "INSERT INTO Customer1 (name, email, address) VALUES (@name, @email, @address)";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@name", cust.name);
                cmd.Parameters.AddWithValue("@email", cust.email);
                cmd.Parameters.AddWithValue("@address", cust.address);

                conn.Open();

                cmd.ExecuteNonQuery();
            }

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
           

            var conString = "Server=localhost;Database=crud;Trusted_Connection=True;MultipleActiveResultSets=true";
            Customer customer = null;

            using (SqlConnection conn = new SqlConnection(conString))
            {
                string query = "select * from Customer1 where id =@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    customer = new Customer
                    {
                        id = (int)reader["id"],
                        name = (string)reader["name"],
                        email = (string)reader["email"],
                        address = (string)reader["address"],

                    };

                }
            }
            return View(customer);

        }
        [HttpPost]
        public IActionResult Edit(Customer cust)
        {
            var conString = "Server=localhost;Database=crud;Trusted_Connection=True;MultipleActiveResultSets=true";

            using (SqlConnection conn = new SqlConnection(conString))
            {
                string query = "UPDATE Customer1 SET email = @email, address = @address WHERE id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@Email", cust.email);
                cmd.Parameters.AddWithValue("@Address", cust.address);
                cmd.Parameters.AddWithValue("@Id", cust.id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Delete(int id)
        {
            var conString = "Server=localhost;Database=crud;Trusted_Connection=True;MultipleActiveResultSets=true";
            using (SqlConnection conn = new SqlConnection(conString))
            {
                string query = "delete from Customer1 where id=@id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction(nameof(Index));
        }

    }


}
