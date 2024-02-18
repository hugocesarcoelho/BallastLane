using System.Collections.Generic;
using System.Linq;

namespace BallastLane.Domain.ValueObjects
{
    internal static class ResultExtension
    {
        public static void AddError(this IDictionary<string, List<string>> errorList, string errorCode, string errorMessage)
        {
            if (errorList is not null && errorList.ContainsKey(errorCode))
            {
                errorList.First(x => x.Key == errorCode).Value.Add(errorMessage);
                return;
            }

            errorList.Add(errorCode, new List<string> { errorMessage });
            return;
        }

    }
}
