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

    public ActionResult Details(int id)
    {
      Appointment appointment = _db.Appointments.Include(apps => apps.Stylist).Include(apps => apps.Client).FirstOrDefault(apps => apps.AppointmentId == id);
      return View(appointment);
    }

    public ActionResult Create()
    {
      IEnumerable<Client> clientList = _db.Clients.ToList().OrderBy(clients => clients.Name);
      ViewBag.ClientList = clientList;
      ViewBag.ClientCount = clientList.Count();
      return View();
    }

    [HttpPost]
    public ActionResult Create(Appointment appointment, string clientId)
    {
      Client client = _db.Clients.FirstOrDefault(clients => clients.ClientId == int.Parse(clientId));
      Stylist stylist = client.Stylist;
      client.Appointments.Add(appointment);
      stylist.Appointments.Add(appointment);
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = appointment.id });
    }
  }
}