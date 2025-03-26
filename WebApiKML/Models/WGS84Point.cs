namespace WebApiKML.Models
{
    public class WGS84Point
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public double LatitudeAsRad
        {
            get
            {
                return Latitude * Math.PI / 180f;
            }
        }

        public double LongitudeAsRad
        {
            get
            {
                return Longitude * Math.PI / 180f;
            }
        }
    }
}
