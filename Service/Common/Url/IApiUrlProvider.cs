namespace Immowert4You.Service.Common.Url
{
    /// <summary>
    /// Provides an url to the running backend
    /// </summary>
    public interface IApiUrlProvider
    {
        /// <summary>
        /// Url pointing to host - not a specific endpoint
        /// </summary>
        string Url { get; }
    }
}
