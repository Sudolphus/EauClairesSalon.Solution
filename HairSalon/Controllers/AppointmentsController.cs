using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

    public ActionResult Index(string stylist, string client)
    {
      IQueryable<Appointment> appointmentQuery = _db.Appointments
        .Include(app => app.Client)
        .Include(app => app.Stylist);
      if (!string.IsNullOrEmpty(stylist))
      {
        Regex stySearch = new Regex(stylist, RegexOptions.IgnoreCase);
        appointmentQuery = appointmentQuery.Where(app => stySearch.IsMatch(app.Stylist.Name));
      }
      if (!string.IsNullOrEmpty(client))
      {
        Regex cliSearch = new Regex(client, RegexOptions.IgnoreCase);
        appointmentQuery = appointmentQuery.Where(app => cliSearch.IsMatch(app.Client.Name));
      }
      IEnumerable<Appointment> appointmentList = appointmentQuery
        .ToList()
        .OrderBy(app => app.DayTime)
        .ThenBy(app => app.Stylist.Name);
      return View(appointmentList);
    }
  }
}
