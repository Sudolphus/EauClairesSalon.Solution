using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class AppointmentsController : Controller
  {
    private readonly HairSalonContext _db;

    public AppointmentsController(HairSalonContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      IEnumerable<Appointment> model = _db.Appointments.Include(apps => apps.Stylist).Include(apps => apps.Client).ToList().OrderBy(apps => apps.Stylist.Name).ThenBy(apps => apps.Time);
      return View(model);
    }
  }
}