namespace StoreApp.Infrastructure.Extensions
{
    public static class HttpRequestExtension
    {
        public static string PathAndQuery(this HttpRequest request)
        {
            //herhangibir parametre vermden, HttpRequest üzerinden çağrılır bu metod

            return request.QueryString.HasValue
                ? $"{request.Path}{request.QueryString}"
                : request.Path.ToString();
        }

    }
}
