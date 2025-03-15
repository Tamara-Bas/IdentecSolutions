using System.Collections;

namespace IdentecSolutions.Application.Contracts
{
    public class Response<T> : ICacheableResponse
    {
        public T? Data { get; set; }

        public bool HasData()
        {
            if(Data is IList list)
            {
                return list.Count > 0;
            }
            else if (Data is IEnumerable enumerable)
            {
                var e = enumerable.GetEnumerator();
                return e.MoveNext();
            }

            return Data != null;
        }
    }
}
