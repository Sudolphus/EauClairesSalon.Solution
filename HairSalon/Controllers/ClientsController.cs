using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class ClientsController : Controller
  {
    private readonly HairSalonContext _db;

    public ClientsController(HairSalonContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Client> model = _db.Clients.Include(clients => clients.Stylist).ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.StylistList = _db.Stylists.ToList();
      return View();
    }

    [HttpPost]
    public ActionResult Create(Client client, string stylistId)
    {
      Stylist stylist = _db.Stylists.FirstOrDefault(stylists => stylists.StylistId == int.Parse(stylistId));
      stylist.Clients.Add(client);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Client client = _db.Clients.Include(clients => clients.Stylist).FirstOrDefault(clients => clients.ClientId == id);
      return View(client);
    }

    public ActionResult Edit(int id)
    {
      Client client = _db.Clients.Include(clients => clients.Stylist).FirstOrDefault(clients => clients.ClientId == id);
      ViewBag.StylistList = _db.Stylists.ToList();
      return View(client);
    }

    [HttpPost]
    public ActionResult Edit(Client client, string stylistId)
    {
      if (int.Parse(stylistId) != client.StylistId)
      {
        Stylist stylist = _db.Stylists.FirstOrDefault(stylists => stylists.StylistId == int.Parse(stylistId));
        stylist.Clients.Add(client);
      }
      _db.Entry(client).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = client.ClientId});
    }

    public ActionResult Delete(int id)
    {
      Client client = _db.Clients.FirstOrDefault(clients => clients.ClientId == id);
      return View(client);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Client client = _db.Clients.FirstOrDefault(clients => clients.ClientId == id);
      _db.Clients.Remove(client);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}