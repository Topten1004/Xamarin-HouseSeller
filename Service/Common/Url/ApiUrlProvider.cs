namespace Immowert4You.Service.Common.Url
{
    public class ApiUrlProvider : IApiUrlProvider
    {
        private const string _localUrl = "http://5.189.167.37:8443"; //"http://192.168.1.30:44344";
        private const string _stagingUrl = "http://5.189.167.37:8443"; //"http://5.189.167.37:5000";
        public string Url
        {
            get
            {
#if RELEASE
                return _stagingUrl;
#else
                return _localUrl;
#endif
            }
        }
    }
}