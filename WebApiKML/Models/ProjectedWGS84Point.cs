using ProjNet.CoordinateSystems;

namespace WebApiKML.Models
{
    public class ProjectedWGS84Point
    {
        public double X { get; }
        public double Y { get; }
        private ProjectedWGS84Point(WGS84Point point)
        {
            var transform = new ProjNet.CoordinateSystems.Transformations.CoordinateTransformationFactory()
                .CreateFromCoordinateSystems(GeographicCoordinateSystem.WGS84, ProjectedCoordinateSystem.WebMercator);
            double[] transformedCoord = transform.MathTransform.Transform(new double[] { point.Longitude, point.Latitude });
            X = transformedCoord[0];
            Y = transformedCoord[1];
        }

        public static ProjectedWGS84Point CreateFromWGS84Point(WGS84Point point)
        {
            if (point == null)
                throw new ArgumentException("WGS84Point cannot be null");


            return new ProjectedWGS84Point(point);
        }
    }
}
