namespace jQueryDatatableServerSideNetCore.Services.XmlService
{
    public interface IXmlService
    {
        byte[] Write<T>(IList<T> registers);
    }
}
