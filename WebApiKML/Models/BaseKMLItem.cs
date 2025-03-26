using SharpKml.Dom;
using WebApiKML.Exceptions;

namespace WebApiKML.Models
{
    public abstract class BaseKMLItem
    {
        public string Name { private set; get; }
        public int Fid { private set; get; }
        public int Size { private set; get; }

        public BaseKMLItem(Placemark placemark) 
        {
            Name = placemark.Name;

            var extendedData = placemark.ExtendedData;

            if (extendedData == null)
                throw new ParseKMLException();

            SchemaData? schemaData = placemark.ExtendedData.SchemaData.OfType<SchemaData>().FirstOrDefault();

            if (schemaData == null)
                throw new ParseKMLException();

            SimpleData? fidData = schemaData.SimpleData.FirstOrDefault(x => x.Name == "fid");
            SimpleData? sizeData = schemaData.SimpleData.FirstOrDefault(x => x.Name == "size");

            if (fidData == null)
                throw new ParseKMLException();

            if (sizeData == null)
                throw new ParseKMLException();

            if (int.TryParse(fidData.Text, out int fid))
                Fid = fid;
            else
                throw new ParseKMLException();

            if (int.TryParse(sizeData.Text, out int size))
                Size = size;
            else
                throw new ParseKMLException();
        }
    }
}
