using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using Telekocsi.Datadirectory;
using Telekocsi.Models;
using System.Security.Cryptography;
using System.Globalization;

namespace Telekocsi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;



        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            DataCTX context = new();
            List<Lines> l = context.Lines.ToList();

            ViewData["session"] = HttpContext.Session.GetString("admin");


            return View(l);

        }
        
        [HttpGet]
        public IActionResult Details(int id)
        {
            ViewData["session"] = HttpContext.Session.GetString("admin");

            DataCTX context = new();

            List<Cars> c = context.Cars.ToList();

            if (id==0) return Redirect("~/Home/Index");


            List<Lines> l = context.Lines.Where(x => x.Id == id).ToList();
            if (l.Count == 0) return Redirect("~/Home");

            ViewData["line"] = l.FirstOrDefault();

            c = c.Where(c => c.LineId == id).ToList();
            c = c.OrderBy(x => x.Start).ToList();
            //return View(c.Where(c => c.LineId == id).ToList());
            return View(c);
        }


        [HttpGet]
        public IActionResult Reserve(int id)
        {

            if (id == 0) return Redirect("~/Home/Index"); 



            ViewData["session"] = HttpContext.Session.GetString("admin");

            DataCTX context = new();
            List<Cars> c = context.Cars.ToList();

            int i = id;


            Cars car = c.FirstOrDefault(x => x.Id == id);

            if (car == null)
            {
                return Redirect("~/Home/Index");
            }



            // ViewData["reservationCar"] = c.FirstOrDefault();

            List<Lines> l = context.Lines.Where(x => x.Id == car.LineId).ToList(); // hibakezelés



            Lines line = l.FirstOrDefault();

            ViewData["reservationLine"] = line;
            ViewData["carId"] = i;

            if (line.Capacity - car.OccupiedSpots <= 0)
            {
                return Redirect("~/Home/Index");
            }


            return View(car);

        }

        [HttpPost]
        public IActionResult Reserve(Contact cont)
        {

            ViewData["session"] = HttpContext.Session.GetString("admin");

            DataCTX context = new();
            List<Cars> c = context.Cars.Where(x => x.Id == Convert.ToInt32(cont.CarId)).ToList();
            Cars car = c.FirstOrDefault();

            List<Lines> l = context.Lines.Where(x => x.Id == car.LineId).ToList();
            Lines line = l.FirstOrDefault();
            ViewData["reservationLine"] = line;

            if (!ModelState.IsValid)
            {


                if (String.IsNullOrEmpty(cont.Name))
                {
                    ViewData["nameError"] = "Name is empty!";
                }
                if (String.IsNullOrEmpty(cont.Address))
                {
                    ViewData["addressError"] = "Address is empty!";
                }
                if (String.IsNullOrEmpty(cont.Email))
                {
                    ViewData["emailError"] = "Email is empty!";
                }
                if (String.IsNullOrEmpty(cont.Phone))
                {
                    ViewData["phoneError"] = "Phone is empty!";
                }

                return View(car);
            }

            

            car.OccupiedSpots++;

            context.Cars.Update(car);
            context.SaveChanges();

            HttpContext.Session.SetString("success", $"{car.Id}");

            return Redirect("~/Home/Succes/" + car.Id); 
                                                        

        }
        [HttpGet]
        public IActionResult Search(string arrival, string departure, string date)
        {

            ViewData["session"] = HttpContext.Session.GetString("admin");

            DataCTX context = new();
            List<Cars> c = context.Cars.ToList();
            List<Lines> l = context.Lines.ToList();

            if (!String.IsNullOrEmpty(departure))
            {
                l = l.Where(x => x.Start.Contains(departure)).ToList();
            }

            if (!String.IsNullOrEmpty(arrival))
            {
                l = l.Where(x => x.Arrival.Contains(arrival)).ToList();
            }

            if (!String.IsNullOrEmpty(date))
            {
                DateTime d = DateTime.Parse(date);
                c = c.Where(x => x.Start.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)
                                == d.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture)).ToList();

                List<int> lineId = new();
                foreach (var car in c)
                {
                    if (!lineId.Contains(car.LineId))
                    {
                        lineId.Add(car.LineId);
                    }
                }
                l = l.Where(x => lineId.Contains(x.Id)).ToList();

            }

            ViewData["searchLines"] = l;
            c = c.OrderBy(x => x.Start).ToList();
            return View(c);
        }

        [HttpGet]
        public IActionResult Succes(int id) 
        {
            if (Convert.ToInt32(HttpContext.Session.GetString("success"))!=id)
            {
                return Redirect("~/Home/Details");
            }

            ViewData["session"] = HttpContext.Session.GetString("admin");

            DataCTX context = new();
            List<Cars> c = context.Cars.ToList();

            int i = id;
            if (id == 0) return Redirect("~/Home/Details");

            Cars car = c.FirstOrDefault(x => x.Id == id);

            if (car == null)
            {
                return Redirect("~/Home/Details");
            }

            List<Lines> l = context.Lines.Where(x => x.Id == car.LineId).ToList();



            Lines line = l.FirstOrDefault();

            ViewData["reservationLine"] = line;
            ViewData["carId"] = i;




            return View(car);

        }

        public IActionResult Login()
        {
            ViewData["session"] = HttpContext.Session.GetString("admin");

            return View();
        }



        [HttpPost]
        public IActionResult Login(Admin a)
        {
            ViewData["session"] = HttpContext.Session.GetString("admin");
            if (!ModelState.IsValid)
            {

                if (String.IsNullOrEmpty(a.Name))
                {
                    ViewData["adminNameError"] = "Name is empty!";
                }
                if (String.IsNullOrEmpty(a.Pwd))
                {
                    ViewData["adminPwdError"] = "Password is empty!";
                }

                return View();
            }
            byte[] name = new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.ASCII.GetBytes(a.Name));

            byte[] pw = new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.ASCII.GetBytes(a.Pwd));
            byte[] ok = new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.ASCII.GetBytes("admin")); // majd adatbázisból jön az értéke

            if (!pw.SequenceEqual(ok) || !name.SequenceEqual(ok))
            {
                ViewData["adminPwdError"] = "Username or Password is wrong!";
                return View();
            }

            


            HttpContext.Session.SetString("admin", "admin");

            return Redirect("~/Home/Index");

        }

        public IActionResult Logout()
        {
            HttpContext.Session.SetString("admin", "");

            return Redirect("~/Home/Details");
        }

        public IActionResult Addline()
        {
            ViewData["session"] = HttpContext.Session.GetString("admin");
            if (String.IsNullOrEmpty(ViewData["session"] as string))
            {
                return Redirect("~/Home/Index");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Addline(Lines l)
        {
            ViewData["session"] = HttpContext.Session.GetString("admin");
            if (!ModelState.IsValid)
            {

                if (String.IsNullOrEmpty(l.Start))
                {
                    ViewData["lineStartError"] = "Start is empty!";
                }
                if (String.IsNullOrEmpty(l.Arrival))
                {
                    ViewData["lineArrivalError"] = "Arrival is empty!";
                }
                if (l.Distance == 0)
                {
                    ViewData["lineDistanceError"] = "Distance is empty!";
                }
                if (l.Capacity == 0)
                {
                    ViewData["lineCapacityError"] = "Capacity is empty!";
                }

                double d = l.Distance;

                return View();
            }


            DataCTX context = new();
            context.Lines.Add(l);
            context.SaveChanges();


            //return View();
            return Redirect("~/Home/Index");
        }

        public IActionResult Addcar(int lid) 
        {

            ViewData["session"] = HttpContext.Session.GetString("admin");
            if (String.IsNullOrEmpty(ViewData["session"] as string))
            {
                return Redirect("~/Home/Index");
            }



            DataCTX context = new();
            Lines l = context.Lines.Where(x => x.Id == lid).FirstOrDefault();

            if (l == null)
            {
                return Redirect("~/Home/Index");
            }

  
            return View(l);
        }

        [HttpPost]
        public IActionResult Addcar(Cars c)
        {
            ViewData["session"] = HttpContext.Session.GetString("admin");
            DataCTX context = new();
            Lines l = context.Lines.Where(x => x.Id == c.LineId).FirstOrDefault();

            if (!ModelState.IsValid)
            {
                DateTime date1 = new DateTime(2000, 1, 1, 1, 1, 1);

                if (DateTime.Compare(c.Start, date1) < 0)
                {
                    ViewData["carStartError"] = "Start is empty, or wrong!";
                }


                double d = l.Distance;

                return View(l);
            }

            context.Cars.Add(c);
            context.SaveChanges();

            return Redirect("~/Home/Index");
            //return View(l);
        }

        public IActionResult Modifyline(int id)
        {

            ViewData["session"] = HttpContext.Session.GetString("admin");
            if (String.IsNullOrEmpty(ViewData["session"] as string))
            {
                return Redirect("~/Home/Index");
            }

            DataCTX context = new();
            Lines l = context.Lines.Where(x => x.Id == id).FirstOrDefault();

            if (l == null)
            {
                return Redirect("~/Home/Index");
            }

            return View(l);
        }

        [HttpPost]
        public IActionResult Modifyline(Lines l)
        {
            ViewData["session"] = HttpContext.Session.GetString("admin");
            if (!ModelState.IsValid)
            {

                if (String.IsNullOrEmpty(l.Start))
                {
                    ViewData["lineStartError"] = "Start is empty!";
                }
                if (String.IsNullOrEmpty(l.Arrival))
                {
                    ViewData["lineArrivalError"] = "Arrival is empty!";
                }
                if (l.Distance == 0)
                {
                    ViewData["lineDistanceError"] = "Distance is empty!";
                }
                if (l.Capacity == 0)
                {
                    ViewData["lineCapacityError"] = "Capacity is empty!";
                }

                double d = l.Distance;

                return View(l);
                
            }


            DataCTX context = new();

            List<Cars> cars = context.Cars.Where(x => x.LineId == l.Id).ToList();
            foreach (var car in cars)
            {
                if (car.OccupiedSpots > l.Capacity)
                {
                    car.OccupiedSpots = l.Capacity;
                }
                context.Cars.Update(car);
            }


            context.Lines.Update(l);
            context.SaveChanges();

            return Redirect("~/Home/Index");
            //return View(l);
        }

        public IActionResult Deleteline(int id)
        {

            ViewData["session"] = HttpContext.Session.GetString("admin");
            if (String.IsNullOrEmpty(ViewData["session"] as string))
            {
                return Redirect("~/Home/Index");
            }

            DataCTX context = new();
            Lines l = context.Lines.Where(x => x.Id == id).FirstOrDefault();

            if (l == null)
            {
                return Redirect("~/Home/Index");
            }

            List<Cars> c = context.Cars.Where(x => x.LineId == l.Id).ToList();



            context.Lines.Remove(l);
            foreach (var car in c)
            {
                context.Cars.Remove(car);
            }
            context.SaveChanges();

            return Redirect("~/Home/Index");
            //return View(l);
        }

        public IActionResult Modifycar(int id)
        {

            ViewData["session"] = HttpContext.Session.GetString("admin");
            if (String.IsNullOrEmpty(ViewData["session"] as string))
            {
                return Redirect("~/Home/Index");
            }

            DataCTX context = new();
            Cars c = context.Cars.Where(x => x.Id == id).FirstOrDefault();

            if (c == null)
            {
                return Redirect("~/Home/Index");
            }

            return View(c);
        }

        [HttpPost]
        public IActionResult Modifycar(Cars c)
        {
            ViewData["session"] = HttpContext.Session.GetString("admin");
            DataCTX context = new();

            Lines l = context.Lines.Where(x => x.Id == c.LineId).FirstOrDefault();

            DateTime date1 = new DateTime(2000, 1, 1, 1, 1, 1);

            if (DateTime.Compare(c.Start, date1) < 0)
            {
                ViewData["carStartError"] = "Start is empty, or wrong!";
                return View(c);
            }

            if (c.OccupiedSpots > l.Capacity)
            {
                ViewData["carOSError"] = "Occupied spots can't be higher than the line capacity";
                return View(c);
            }



            context.Cars.Update(c);
            context.SaveChanges();

            return Redirect("~/Home/Index");
            //return View(c);
        }

        public IActionResult Deletecar(int id)
        {

            ViewData["session"] = HttpContext.Session.GetString("admin");
            if (String.IsNullOrEmpty(ViewData["session"] as string))
            {
                return Redirect("~/Home/Index");
            }

            DataCTX context = new();
            Cars c = context.Cars.Where(x => x.Id == id).FirstOrDefault();

            if (c == null)
            {
                return Redirect("~/Home/Index");
            }

            context.Cars.Remove(c);
            context.SaveChanges();

            
            return Redirect("~/Home/Index");
            //return View(c);
        }

        [HttpGet]
        public IActionResult Statistics(string departure, string arrival, string from, string to)
        {
            ViewData["session"] = HttpContext.Session.GetString("admin");
            if (String.IsNullOrEmpty(ViewData["session"] as string))
            {
                return Redirect("~/Home/Index");
            }

            DataCTX context = new();
            List<Cars> c = context.Cars.ToList();
            List<Lines> l = context.Lines.ToList();
            Dictionary<string, int> data = new();

            if (!String.IsNullOrEmpty(departure))
            {
                l = l.Where(x => x.Start.Contains(departure)).ToList();
            }

            if (!String.IsNullOrEmpty(arrival))
            {
                l = l.Where(x => x.Arrival.Contains(arrival)).ToList();
            }

            if (!String.IsNullOrEmpty(from))
            {
                DateTime d = DateTime.Parse(from);
                c = c.Where(x => DateTime.Compare(x.Start.Date, d.Date) >= 0).ToList();
            }

            if (!String.IsNullOrEmpty(to))
            {
                DateTime d = DateTime.Parse(to);
                c = c.Where(x => DateTime.Compare(x.Start.Date, d.Date) <= 0).ToList();
            }


            int count;
            foreach (var line in l)
            {
                count = 0;
                foreach (var car in c)
                {
                    if (car.LineId == line.Id)
                    {
                        count += car.OccupiedSpots;
                    }
                }
                data.Add($"{line.Start} - {line.Arrival}", count);
            }



            ViewData["searchLines"] = l;

            return View(data);
        }

        public IActionResult Calendar(int week)
        {

            ViewData["session"] = HttpContext.Session.GetString("admin");

            if (week==0)
            {
                CultureInfo myCI = new CultureInfo("en-US");
                Calendar ca = myCI.Calendar;
                CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
                DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;
                DateTime now = DateTime.Now;
                week = ca.GetWeekOfYear(now, myCWR, myFirstDOW)-1;
                
            }

            DateTime jan1 = new DateTime(DateTime.Now.Year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;


            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = week;

            if (firstWeek == 1)
            {
                weekNum -= 1;
            }
            var result = firstThursday.AddDays(weekNum * 7);

            var monday = result.AddDays(-3);

            DataCTX context = new();
            List<Cars> c = context.Cars.ToList();
            List<Lines> l = context.Lines.ToList();
            Dictionary<Lines, List<Cars>> data = new();
            foreach (Lines line in l)
            {
                data.Add(line, new List<Cars>());
                foreach (Cars car in c)
                {
                    if (car.LineId==line.Id)
                    {
                        data[line].Add(car);
                    }
                }
            }
            ViewData["week"] = week;
            ViewData["monday"] = monday;

            return View(data);
        }
        
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}