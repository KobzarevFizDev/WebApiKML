namespace WebApiKML.DTO
{
    public class CheckContainsPointSuccessResponseDTO
    {
        public int Id { private set; get; }
        public string Name { private set; get; }

        public CheckContainsPointSuccessResponseDTO(int id, string name) 
        {
            Id = id;
            Name = name;
        }
    }
}
