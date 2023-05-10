namespace jQueryDatatableServerSideNetCore.Services.JsonService
{
    public interface IJsonService
    {
        byte[] Write<T>(IList<T> registers);
    }
}
