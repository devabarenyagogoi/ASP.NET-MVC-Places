namespace FirstDotNETApp.Models
{
    public class ErrorViewModel
    {
        // Creates a property: RequestId
        // String data type. ? -> nullable
        // get: read value
        // set: assign value
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
