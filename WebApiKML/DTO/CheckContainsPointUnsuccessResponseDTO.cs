namespace WebApiKML.DTO
{
    public class CheckContainsPointUnsuccessResponseDTO
    {
        public bool IsContains { private set; get; }
        public CheckContainsPointUnsuccessResponseDTO() 
        {
            IsContains = false;
        }
    }
}
