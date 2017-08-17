using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteApplication.CodeBehind.Classess
{
    public class OperationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public OperationResult(bool result) : this(result, null, null) { }

        public OperationResult(bool result, string message) : this (result, message, null) { }

        public OperationResult(bool result, string message, object data)
        {
            Success = result;
            Message = message;
            Data = data;
        }

        public static OperationResult SuccessResult() => new OperationResult(true);
        public static OperationResult FailureResult() => new OperationResult(false);
        public static OperationResult FailureResult(string message) => new OperationResult(false, message);
    }
}