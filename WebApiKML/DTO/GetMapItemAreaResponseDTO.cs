namespace WebApiKML.DTO
{
    public class GetMapItemAreaResponseDTO
    {
        public double Area { private set; get; }
        public string Unit { private set; get; }
        public GetMapItemAreaResponseDTO(double area, string unit = "M^2")
        {
            Area = area;
            Unit = unit;
        }
    }
}
