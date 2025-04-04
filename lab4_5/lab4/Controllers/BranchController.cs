using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

public class BranchController : Controller
{
    //Console.OutputEncoding = System.Text.Encoding.UTF8;
    string connectionString = "Server=.\\SQLEXPRESS; Database=lect2; User Id=sa; Password=admin123; TrustServerCertificate=True;";
    List<object> branches = new List<object>();

    // GET: BranchController
    public ActionResult Index()
    {

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT bid, bname FROM dbo.branch;";



            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        branches.Add(new
                        {
                            Id = reader["bid"],
                            Name = reader["bname"]
                        });
                    }
                    ViewBag.branches = branches;
                    return View();
                }
                else
                {
                    ViewBag.branches = "500 - Сервер алдаа!";
                    return View();
                }
            }
        }
    }

    // GET: BranchController/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }

    // GET: BranchController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: BranchController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(IFormCollection collection)
    {
        try
        {
            string branchName = collection["branchName"];

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "INSERT INTO dbo.branch (bname) VALUES (@bname);";
                cmd.Parameters.AddWithValue("@bname", branchName);
                cmd.ExecuteNonQuery();
            }

            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }


    // GET: BranchController/Edit/5
    public ActionResult Edit(int id)
    {
        object branch = null;
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "SELECT bid, bname FROM dbo.branch WHERE bid = @id;";
            cmd.Parameters.AddWithValue("@id", id);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.HasRows && reader.Read())
                {
                    branch = new
                    {
                        Id = reader["bid"],
                        Name = reader["bname"]
                    };
                }
            }
        }

        if (branch != null)
        {
            return View(branch);
        }

        // Redirect to Index if branch not found
        return RedirectToAction(nameof(Index));
    }


    // POST: BranchController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(IFormCollection collection)  // You can directly get parameters here
    {
        int id = Convert.ToInt32(collection["id"]);
        string branchName = collection["branchName"];


        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "UPDATE dbo.branch SET bname = @bname WHERE bid = @id;";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@bname", branchName);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Branch update failed.");
                    return View();
                }
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "An error occurred while updating the branch: " + ex.Message);
            return View();
        }
    }



    // GET: BranchController/Delete/5
    public ActionResult Delete(int id)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "DELETE FROM dbo.branch WHERE bid = @id;";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }

            // Амжилттай устгасны дараа Index руу шилжих
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            // Алдаа гарвал хэрэглэгчид алдааг мэдэгдэх
            return View();
        }
    }
}
