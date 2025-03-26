using NetTopologySuite.Algorithm;
using SharpKml.Dom;
using SharpKml.Engine;
using System.IO;
using WebApiKML.Exceptions;

namespace WebApiKML.Models
{
    public abstract class BaseKMLRepository<T> where T : BaseKMLItem
    {
        protected KmlFile _kmlFile;

        public List<T> Items { private set; get; }

        public BaseKMLRepository(string pathTo)
        {
            Items = new List<T>();
            using (var reader = File.OpenRead(pathTo))
            {
                _kmlFile = KmlFile.Load(reader);
                foreach (Placemark placemark in _kmlFile.Root.Flatten().OfType<Placemark>().ToList())
                {
                    T item = CreateItem(placemark);
                    Items.Add(item);
                }
            }
        }

        public T GetItemById(int id)
        {
            T? item = Items.FirstOrDefault(i => i.Fid == id);
            if(item == null)
                throw new NotFoundKMLElement($"Not found centroid with id = {id}");
            return item;
        }

        protected abstract T CreateItem(Placemark placemark);
    }
}
