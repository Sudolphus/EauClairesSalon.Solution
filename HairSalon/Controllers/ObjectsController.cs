using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ProjectName.Models;

namespace ProjectName.Controllers
{
  public class ObjectsController : Controller
  {
    private readonly ProjectNameContext _db;

    public ObjectsController(ProjectNameContext db)
    {
      _db = db;
    }
  }
}