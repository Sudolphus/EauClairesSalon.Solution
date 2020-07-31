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
      IEnumerable<Stylist> stylistList = _db.Stylists.ToList().OrderBy(stylists => stylists.Name);
      ViewBag.ClientList = clientList;
      ViewBag.StylistList = stylistList;
      ViewBag.ClientCount = clientList.Count();
      ViewBag.StylistCount = stylistList.Count();
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
      return RedirectToAction("Details", new { id = appointment.AppointmentId });
    }

    public ActionResult Edit(int id)
    {
      Appointment appointment = _db.Appointments.Include(apps => apps.Stylist).Include(apps => apps.Client).FirstOrDefault(apps => apps.AppointmentId == id);
      return View(appointment);
    }

    [HttpPost]
    public ActionResult Edit(Appointment appointment)
    {
      _db.Entry(appointment).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = appointment.AppointmentId });
    }

    public ActionResult Delete(int id)
    {
      Appointment appointment = _db.Appointments.FirstOrDefault(apps => apps.AppointmentId == id);
      return View(appointment);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Appointment appointment = _db.Appointments.FirstOrDefault(apps => apps.AppointmentId == id);
      _db.Appointments.Remove(appointment);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}