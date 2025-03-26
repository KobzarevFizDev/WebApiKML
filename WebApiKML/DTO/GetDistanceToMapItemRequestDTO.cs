namespace WebApiKML.DTO
{
    public class GetDistanceToMapItemRequestDTO
    {
        public int MapItemId { get; set; }
        public double FromPointLatitude { get; set; }
        public double FromPointLongitude { get; set; }
    }
}
