using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge.Dto
{
    public interface IGenericBaseResponse
    {

        bool Success { get; set; }

        string ErrorMessage { get; set; }

        int Code { get; set; }
      
    }
    public interface IGenericBaseResponse<T> : IGenericBaseResponse
    {
        T Data { get; set; }
    }
    public interface IGenericBaseResponse<T, U>
    {
        T Data { get; set; }

        U Metadata { get; set; }

    }
}
