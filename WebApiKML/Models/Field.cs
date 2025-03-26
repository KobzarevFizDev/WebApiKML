using SharpKml.Dom;
using WebApiKML.Exceptions;
using NetTopologySuite.Geometries;

namespace WebApiKML.Models
{
    public class Field : BaseKMLItem
    {
        public List<WGS84Point> Polygon { private set; get; }
        public List<ProjectedWGS84Point> ProjectedPolygon { private set; get; }

        public Field(Placemark placemark) : base(placemark)
        {
            Polygon = new List<WGS84Point>();
            ProjectedPolygon = new List<ProjectedWGS84Point>();

            var polygon = placemark.Geometry as SharpKml.Dom.Polygon;
            if (polygon == null)
                throw new ParseKMLException();
            else
            {
                foreach (var coord in polygon.OuterBoundary.LinearRing.Coordinates)
                {
                    WGS84Point p = new WGS84Point();
                    p.Longitude = coord.Longitude;
                    p.Latitude = coord.Latitude;
                    Polygon.Add(p);

                    var projectedMapPoint = ProjectedWGS84Point.CreateFromWGS84Point(p);
                    ProjectedPolygon.Add(projectedMapPoint);
                }
            }
        }

        public double GetAreaOfPolygon()
        {
            double S1 = 0;
            double S2 = 0;

            int N = Polygon.Count;

            for (int i = 0; i < N; i++)
            {
                double x = ProjectedPolygon[i].X;
                double y = ProjectedPolygon[(i + 1) % N].Y;
                S1 += x * y;
            }

            for (int i = 0; i < N; i++)
            {
                double x = ProjectedPolygon[(i + 1) % N].X;
                double y = ProjectedPolygon[i].Y;
                S2 += x * y;
            }

            double S = Math.Abs(S1 - S2) / 2;
            return S;
        }

        public bool IsPointInPolygon(WGS84Point point)
        {
            var coordinates = Polygon
                .Select(p => new Coordinate(p.Longitude, p.Latitude))
                .ToArray();

            var polygon = new NetTopologySuite.Geometries.Polygon(new NetTopologySuite.Geometries.LinearRing(coordinates));

            return polygon.Contains(new NetTopologySuite.Geometries.Point(point.Longitude, point.Latitude));
        }
    }
}
