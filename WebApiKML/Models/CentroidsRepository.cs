using SharpKml.Dom;
using SharpKml.Engine;
using WebApiKML.Exceptions;

namespace WebApiKML.Models
{

    public class CentroidsRepository : BaseKMLRepository<Centroid>
    {
        public CentroidsRepository(string pathTo) : base(pathTo) { }
        protected override Centroid CreateItem(Placemark placemark)
        {
            return new Centroid(placemark);
        }
    }
}
