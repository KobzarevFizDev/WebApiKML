using WebApiKML.DTO;
using WebApiKML.Exceptions;
using WebApiKML.Models;

namespace WebApiKML.Services
{
    public class KMLService
    {
        private const string CENTROIDS_FILE_SECTION = "PathToCentroids";
        private const string FIELDS_FILE_SECTION = "PathToFields";
        private const string EXCEPTION_MESSAGE = "Not found path to kml files in config file";

        private CentroidsRepository _centroidsRepository;
        private FieldsRepository _fieldsRepository;

        public KMLService(IConfiguration configuration) 
        {
            string? pathToCentroids = configuration.GetSection(CENTROIDS_FILE_SECTION).Value;
            if (pathToCentroids == null)
                throw new ArgumentException(EXCEPTION_MESSAGE);

            string? pathToFields = configuration.GetSection(FIELDS_FILE_SECTION).Value;
            if (pathToFields == null)
                throw new ArgumentException(EXCEPTION_MESSAGE);

            _centroidsRepository = new CentroidsRepository(pathToCentroids);
            _fieldsRepository = new FieldsRepository(pathToFields);
        }

        public Centroid GetCenterById(int id)
        {
            return _centroidsRepository.GetItemById(id);
        }

        public List<MapItemDTO> GetMapItems()
        {
            List<MapItemDTO> mapItems = new List<MapItemDTO>();

            int numberOfFields = _fieldsRepository.Items.Count;
            int numberOfCentroids = _centroidsRepository.Items.Count;

            if (numberOfFields != numberOfCentroids)
                throw new IncorrectKMLData("Number of fields != number of centroids");

            for (int i = 0; i < numberOfFields; i++)
            {
                Field field = _fieldsRepository.Items[i];
                Centroid centroid = _centroidsRepository.Items[i];

                WGS84PointDTO centroidDTO = new WGS84PointDTO()
                {
                    Latitude = centroid.Center.Latitude,
                    Longitude = centroid.Center.Longitude
                };

                List<WGS84PointDTO> polygonDTO = field.Polygon
                    .Select(p => new WGS84PointDTO()
                    {
                        Latitude = p.Latitude,
                        Longitude = p.Longitude
                    }).ToList();
         
                MapItemDTO mapItem = new MapItemDTO
                {
                    Id = field.Fid,
                    Name = field.Name,
                    Size = field.Size,
                    Location = new LocationDTO
                    {
                        Center = centroidDTO,
                        Polygon = polygonDTO
                    }
                };

                mapItems.Add(mapItem);
            }

            return mapItems;
        }

        public int GetSizeOfMapItemById(int id)
        {
            Field field = _fieldsRepository.GetItemById(id);
            return field.Size;
        }

        public double GetAreaOfMapItemById(int id)
        {
            Field field = _fieldsRepository.GetItemById(id);
            double area = field.GetAreaOfPolygon();
            return area;
        }

        public double GetDistance(int mapItemId, WGS84Point fromPoint)
        {
            const int R = 6_371_000;
            Centroid centroid = _centroidsRepository.GetItemById(mapItemId);
            double deltaLatitude = centroid.Center.LatitudeAsRad - fromPoint.LatitudeAsRad;
            double deltaLongitude = centroid.Center.LongitudeAsRad - fromPoint.LongitudeAsRad;
            double sinLatitude = Math.Sin(deltaLatitude / 2);
            double sinLongitude = Math.Sin(deltaLongitude / 2);
            sinLatitude *= sinLatitude;
            sinLongitude *= sinLongitude;
            double a = sinLatitude + Math.Cos(fromPoint.LatitudeAsRad) * Math.Cos(fromPoint.LongitudeAsRad) * sinLongitude;
            double distance = 2 * R * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return distance;
        }

        public bool TryFindMapItemContainPoint(WGS84Point point, out Field? containsField)
        {
            containsField = null;
            foreach(Field field in _fieldsRepository.Items)
            {
                if (field.IsPointInPolygon(point))
                {
                    containsField = field;
                    return true;
                }
            }
            return false;
        }
    }
}
