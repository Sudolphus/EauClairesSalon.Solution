using System.Collections.Generic;

namespace HairSalon.Models
{
  public class Stylist
  {
    public int StylistId { get; set; }
    public string Name { get; set; }
    public ICollection<Client> Clients { get; set; }
    public ICollection<Appointment> Appointments { get; set; }
  

    public Stylist()
    {
      this.Clients = new HashSet<Client>();
      this.Appointments = new HashSet<Appointment>();
    }
  }
}