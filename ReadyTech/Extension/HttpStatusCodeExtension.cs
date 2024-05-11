using System.Net;

namespace ReadyTech.Extension
{
    public static class HttpStatusCodeExtension
    {
        public static HttpStatusCode ImATeapot => (HttpStatusCode)418;
    }
}
