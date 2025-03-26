namespace WebApiKML.DTO
{
    public class GetMapItemSizeResponseDTO
    {
        public int Size { private set; get; }   
        public GetMapItemSizeResponseDTO(int size) 
        {
            Size = size;
        }
    }
}
