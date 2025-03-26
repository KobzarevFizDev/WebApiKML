using SharpKml.Dom;
using SharpKml.Engine;
using WebApiKML.Exceptions;

namespace WebApiKML.Models
{
    public class FieldsRepository : BaseKMLRepository<Field>
    {
        public FieldsRepository(string pathTo) : base(pathTo) { }

        protected override Field CreateItem(Placemark placemark)
        {
            return new Field(placemark);
        }
    }
}
