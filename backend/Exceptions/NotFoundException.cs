namespace backend.Exceptions
{
    public class NotFoundException: SystemException
    {
        public string DetailInfo { get; set; }

        public NotFoundException()
        {
            DetailInfo = "Not Found"; 
        }

        public NotFoundException(string message, string info): base(message)
        {
            DetailInfo = info; 
        }
    }
}
