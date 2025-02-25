using System.Collections.Generic;

namespace HairSalon.Models
{
  public class Client
  {
    public int ClientId { get; set; }
    public string Name { get; set; }
    public int StylistId { get; set; }
    public Stylist Stylist { get; set; }
    public ICollection<Appointment> Appointments { get; set; }

    public Client()
    {
      this.Appointments = new HashSet<Appointment>();
    }
  }
}