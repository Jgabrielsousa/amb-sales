namespace Ambev.DeveloperEvaluation.Application
{
    //public class Result {
    //    public bool Success { get; set; }
    //    public string Message { get; set; }
    //    public object Data { get; set; }
    //} 

    public record Result(bool Success,
         string Message,
         object Data);
}
