using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Tools.Cqs.Results
{
    public class CqsResult
    {
        public static implicit operator CqsResult(Error error)
        {
            return Failure(error.ErrorMessage);
        }

        public static implicit operator CqsResult(Exception exception)
        {
            return Failure(exception.Message);
        }

        public static CqsResult Success()
        {
            return new CqsResult(true);
        }

        public static CqsResult Failure(string errorMessage)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(errorMessage);

            return new CqsResult(false, errorMessage);
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public string? ErrorMessage { get; }

        private CqsResult(bool isSuccess, string? errorMessage = null)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }
    }

    public class CqsResult<TResult>
    {
        public static implicit operator CqsResult<TResult>(Error error)
        {
            return Failure(error.ErrorMessage);
        }

        public static implicit operator CqsResult<TResult>(Exception exception)
        {
            return Failure(exception.Message);
        }

        public static implicit operator CqsResult<TResult>(TResult result)
        {
            return Success(result);
        }

        public static CqsResult<TResult> Success(TResult data)
        {
            return new CqsResult<TResult>(true, data);
        }

        public static CqsResult<TResult> Failure(string errorMessage)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(errorMessage);

            return new CqsResult<TResult>(false, default!, errorMessage);
        }

        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public string? ErrorMessage { get; }
        public TResult Data { get; }

        private CqsResult(bool isSuccess, TResult data, string? errorMessage = null)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            Data = data;
        }
    }
}
