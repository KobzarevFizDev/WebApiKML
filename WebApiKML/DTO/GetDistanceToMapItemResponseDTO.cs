namespace WebApiKML.DTO
{
    public class GetDistanceToMapItemResponseDTO
    {
        public double Distance { get; set; }
        public string Unit { get; set; }

        public GetDistanceToMapItemResponseDTO(double distance, string unit = "M^2") 
        {
            Distance = distance;
            Unit = unit;
        }
    }
}
