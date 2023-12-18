namespace Assessment.Streaming.WebApi.ErrorHandling
{
    public class ErrorHandling
    {
        public List<ErrorMessage> Messages { get; set; } = new List<ErrorMessage>();
        public String ErrorDescription = "Erro de requisicao";
    }
    public class ErrorMessage
    {
        public string Message { get; set; }
        public string ErrorName { get; set; }
    }
}
