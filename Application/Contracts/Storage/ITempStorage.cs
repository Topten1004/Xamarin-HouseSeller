namespace Immowert4You.Application.Contracts.Storage
{
    public interface ITempStorage
    {
        void Save<T>(T property) where T : class;
        T Read<T>(bool cleanAfter = false) where T : class;
    }
}
