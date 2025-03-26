using WebApiKML.Models;

namespace WebApiKML.DTO
{
    public class MapItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Size { get; set; }
        public required LocationDTO Location { get; set; }

    }

    public class LocationDTO
    {
        public required WGS84Point Center { get; set; }
        public required List<WGS84Point> Polygon { get; set; }
    }

}
