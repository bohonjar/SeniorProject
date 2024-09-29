namespace SeniorProject.Models // Defining the namespace for the Models
{
    public class ErrorViewModel // Defining the ErrorViewModel class
    {
        public string? RequestId { get; set; } // Property to store the request ID, nullable to indicate it may not have a value

        // Property to determine if the RequestId should be shown in the view
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId); // Returns true if RequestId is not null or empty
    }
}