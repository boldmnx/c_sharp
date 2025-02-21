using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace lab4.Controllers
{
    public class WorkerController : Controller
    {
        string connectionString = "Server=.\\SQLEXPRESS; Database=lect2; User Id=sa; Password=admin123; TrustServerCertificate=True;";
        List<object> workers = new List<object>();

        // GET: WorkerController
        public ActionResult Index()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT w.[wid] as wid, w.[wname] as wname, w.[bid], b.[bname] as bname FROM [dbo].[worker] w INNER JOIN [dbo].[branch] b ON w.[bid] = b.[bid]; ";

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            workers.Add(new
                            {
                                Id = reader["wid"],
                                Name = reader["wname"],
                                BranchName = reader["bname"]
                            });
                        }
                        ViewBag.workers = workers;
                        return View();
                    }
                    else
                    {
                        ViewBag.workers = "500 - Сервер алдаа!";
                        return View();
                    }
                }
            }
        }

        // GET: WorkerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WorkerController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WorkerController/Edit/5
        public ActionResult Edit(int id)
        {
            dynamic branch = null;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string workerQuery = @"
        SELECT w.[wid], w.[wname], w.[bid], b.[bname] 
        FROM [dbo].[worker] w 
        INNER JOIN [dbo].[branch] b ON w.[bid] = b.[bid] 
        WHERE w.[wid] = @id";

                SqlCommand cmd = new SqlCommand(workerQuery, con);
                cmd.Parameters.AddWithValue("@id", id);



                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows && reader.Read())
                    {
                        branch = new
                        {
                            Id = reader["wid"],
                            Name = reader["wname"],
                            BranchName = reader["bname"],
                            Bid = reader["bid"]
                        };
                    }
                }

                // Бүх branch-уудыг авах SQL
                string branchesQuery = "SELECT bid, bname FROM dbo.branch";
                SqlCommand branchCmd = new SqlCommand(branchesQuery, con);

                List<dynamic> branches = new List<dynamic>();

                using (SqlDataReader reader = branchCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        branches.Add(new
                        {
                            Id = reader["bid"],
                            Name = reader["bname"]
                        });
                    }
                }

                ViewBag.Worker = branch;
                ViewBag.Branches = branches;
            }


            if (branch != null)
            {
                return View(branch);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: WorkerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(IFormCollection collection)
        {
            int id = Convert.ToInt32(collection["id"]);
            int bid = Convert.ToInt32(collection["bid"]);
            string workerName = collection["workerName"];


            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {

                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = " UPDATE [dbo].[worker]  SET [wname] = @wname,  [bid] = @bid WHERE wid=@wid ";
                    cmd.Parameters.AddWithValue("@wid", id);
                    cmd.Parameters.AddWithValue("@bid", bid);
                    cmd.Parameters.AddWithValue("@wname", workerName);

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

        // GET: WorkerController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = "DELETE FROM dbo.worker WHERE wid = @id;";
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
}
