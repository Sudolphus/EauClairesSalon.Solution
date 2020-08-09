using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

    public ActionResult Index(string client)
    {
      IQueryable<Client> clientQuery = _db.Clients.Include(clients => clients.Stylist);
      if (!string.IsNullOrEmpty(client))
      {
        Regex search = new Regex(client, RegexOptions.IgnoreCase);
        clientQuery = clientQuery.Where(clients => search.IsMatch(clients.Name));
      }
      IEnumerable<Client> model = clientQuery
        .ToList()
        .OrderBy(clients => clients.Stylist.Name)
        .ThenBy(clients => clients.Name);
      return View(model);
    }

    public ActionResult Create()
    {
      IEnumerable<Stylist> stylistList = _db.Stylists.ToList().OrderBy(stylists => stylists.Name);
      ViewBag.StylistList = stylistList;
      ViewBag.StylistCount = stylistList.Count();
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
      return View(client);
    }

    [HttpPost]
    public ActionResult Edit(Client client)
    {
      _db.Entry(client).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = client.ClientId });
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