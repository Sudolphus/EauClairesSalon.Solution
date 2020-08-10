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

    public ActionResult Create()
    {
      IEnumerable<Stylist> stylistList = _db.Stylists
        .ToList()
        .OrderBy(sty => sty.Name);
      IEnumerable<Client> clientList = _db.Clients
        .ToList()
        .OrderBy(cli => cli.Name);
      ViewBag.StylistCount = stylistList.Count();
      ViewBag.ClientCount = clientList.Count();
      ViewBag.StylistId = new SelectList(stylistList, "StylistId", "Name");
      ViewBag.ClientId = new SelectList(clientList, "ClientId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Appointment appointment, string StylistId, string ClientId)
    {
      Stylist stylist = _db.Stylists.FirstOrDefault(stylists => stylists.StylistId == int.Parse(StylistId));
      Client client = _db.Clients.FirstOrDefault(clients => clients.ClientId == int.Parse(ClientId));
      stylist.Appointments.Add(appointment);
      client.Appointments.Add(appointment);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Appointment appointment = _db.Appointments
        .Include(app => app.Stylist)
        .Include(app => app.Client)
        .First(app => app.AppointmentId == id);
      return View(appointment);
    }
  }
}
