using SharpKml.Dom;
using WebApiKML.Exceptions;


namespace WebApiKML.Models
{
    public class Centroid : BaseKMLItem
    {
        public WGS84Point Center { private set; get; }

        public Centroid(Placemark placemark) : base(placemark)
        {
            Center = new WGS84Point();

            var point = placemark.Geometry as Point;
            if (point == null)
                throw new ParseKMLException();
            else
            {
                Center.Longitude = point.Coordinate.Longitude;
                Center.Latitude = point.Coordinate.Latitude;
            }
        }
    }
}
