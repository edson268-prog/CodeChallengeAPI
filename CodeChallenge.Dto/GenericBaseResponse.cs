using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge.Dto
{
   
    public class GenericBaseResponse<T> : IGenericBaseResponse<T>
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public int Code { get; set; } = 200;
        public T Data { get; set; }


        public GenericBaseResponse()
        {
            Success = true;
        }


        public GenericBaseResponse(T data)
        {
            Success = true;
            Data = data;
        }

        public GenericBaseResponse(T data, T errorMessage)
        {
            Success = true;
            Data = data;
        }


        public static GenericBaseResponse<T> Error(int code, string errorMessage = null)
        {
            return new GenericBaseResponse<T>()
            {
                Code = code,
                Success = false,
                ErrorMessage = errorMessage
            };
        }

        public static GenericBaseResponse<T> Ok(T data)
        {
            return new GenericBaseResponse<T>()
            {
                Data = data,
                Code = 200,
                Success = true
            };
        }
    }

    public class GenericBaseResponse<T, U> : IGenericBaseResponse<T, U>
    {
        public T Data { get; set; }
        public U Metadata { get; set; }


        public GenericBaseResponse(T data, U metadata)
        {

            Data = data;
            Metadata = metadata;
        }
    }

}
