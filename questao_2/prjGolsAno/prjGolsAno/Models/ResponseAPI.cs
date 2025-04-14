using System.Text.Json.Serialization;

namespace prjGolsAno.Models;
public class ResponseAPI {
    public int total_pages { get; set; }
    public List<Match> data { get; set; }
}
