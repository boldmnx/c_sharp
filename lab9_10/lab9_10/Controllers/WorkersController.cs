using System.Data;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace lab9_10.Controllers
{
    public class WorkersController : Controller
    {
        string connectionString = "Server=.\\sqlexpress;Database=lect2;User Id=sa; Password=admin123;TrustServerCertificate=True";

        // GET: WorkersController
        [HttpGet("workers")]
        public ActionResult Index(string? wname, int? bid)
        {
            List<Dictionary<string, object>> workers = new List<Dictionary<string, object>>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();  // Холболтыг нээх

                // Ажилчдын мэдээллийг авах
                SqlCommand cmd = new SqlCommand(@"
                        SELECT w.[wid], w.[wname], b.[bname]
                        FROM [dbo].[worker] w 
                        INNER JOIN [dbo].[branch] b ON b.[bid] = w.[bid];", conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var worker = new Dictionary<string, object>
                {
                    { "wname", reader["wname"] },
                    { "bname", reader["bname"] }
                };
                        workers.Add(worker);
                    }
                }

                // Салбарын мэдээллийг авах
                SqlCommand cmd2 = new SqlCommand(@"
                                    SELECT [bid], [bname]
                                    FROM [dbo].[branch];", conn);

                using (SqlDataReader reader2 = cmd2.ExecuteReader())
                {
                    while (reader2.Read())
                    {
                        var branch = new Dictionary<string, object>
                        {
                            { "bid", reader2["bid"] },
                            { "branchName", reader2["bname"] }
                        };
                        workers.Add(branch);
                    }
                }



                if (wname != null)
                {

                    SqlCommand insertStored = new SqlCommand(@"InsertEmployee", conn);
                    insertStored.CommandType = CommandType.StoredProcedure;

                    insertStored.Parameters.AddWithValue("@wname", wname);
                    insertStored.Parameters.AddWithValue("@bid", bid);

                    insertStored.ExecuteNonQuery();
                    return Redirect("workers");
                }

            }

            ViewBag.workers = workers;
            foreach (var i in workers)
            {
                if (i.ContainsKey("bid")) Console.WriteLine($"#########################################10: {JsonSerializer.Serialize(i["bid"])}");
            }
            //Console.WriteLine($"#########################################2: {JsonSerializer.Serialize(workers[0]["bid"])}");
            return View();
        }

        // GET: MovieController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //// GET: MovieController1/Create
        //public ActionResult Create()
        //{
        //    using (SqlConnection conn = new SqlConnection(connectionString))
        //    {
        //        SqlCommand cmd = new SqlCommand("InsertMovie", conn);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        // Параметрүүдийг нэмэх
        //        cmd.Parameters.AddWithValue("@movie_name", "Тамирчин");
        //        cmd.Parameters.AddWithValue("@released", "2025");

        //        conn.Open();
        //        cmd.ExecuteNonQuery();
        //    }

        //    return View();
        //}
        // GET: WorkersController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: WorkersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkersController/Create
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

        // GET: WorkersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WorkersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: WorkersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WorkersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
