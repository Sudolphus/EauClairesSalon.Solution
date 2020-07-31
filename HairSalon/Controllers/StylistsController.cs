using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using HairSalon.Models;

namespace HairSalon.Controllers
{
  public class StylistsController : Controller
  {
    private readonly HairSalonContext _db;

    public StylistsController(HairSalonContext db)
    {
      _db = db;
    }

    public ActionResult Index(string stylist)
    {
      IQueryable<Stylist> stylistQuery = _db.Stylists;
      if (!string.IsNullOrEmpty(stylist))
      {
        Regex search = new Regex(stylist, RegexOptions.IgnoreCase);
        stylistQuery = stylistQuery.Where(stylists => search.IsMatch(stylists.Name));
      }
      IEnumerable<Stylist> model = stylistQuery.ToList().OrderBy(stylists => stylists.Name);
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Stylist stylist)
    {
      _db.Stylists.Add(stylist);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      Stylist stylist = _db.Stylists.Include(stylists => stylists.Clients).FirstOrDefault(stylists => stylists.StylistId == id);
      return View(stylist);
    }

    public ActionResult Edit(int id)
    {
      Stylist stylist = _db.Stylists.FirstOrDefault(stylists => stylists.StylistId == id);
      return View(stylist);
    }

    [HttpPost]
    public ActionResult Edit(Stylist stylist)
    {
      _db.Entry(stylist).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Details", new { id = stylist.StylistId });
    }

    public ActionResult Delete(int id)
    {
      Stylist stylist = _db.Stylists.FirstOrDefault(stylists => stylists.StylistId == id);
      return View(stylist);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Stylist stylist = _db.Stylists.FirstOrDefault(stylists => stylists.StylistId == id);
      _db.Stylists.Remove(stylist);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}