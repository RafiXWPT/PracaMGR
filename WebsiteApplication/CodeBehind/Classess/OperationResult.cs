namespace WebsiteApplication.CodeBehind.Classess
{
    public class OperationResult
    {
        public OperationResult(bool result) : this(result, null, null)
        {
        }

        public OperationResult(bool result, string message) : this(result, message, null)
        {
        }

        public OperationResult(bool result, string message, object data)
        {
            Success = result;
            Message = message;
            Data = data;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public static OperationResult SuccessResult()
        {
            return new OperationResult(true);
        }

        public static OperationResult FailureResult()
        {
            return new OperationResult(false);
        }

        public static OperationResult FailureResult(string message)
        {
            return new OperationResult(false, message);
        }
    }
}