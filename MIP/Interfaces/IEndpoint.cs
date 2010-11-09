namespace MIP.Interfaces
{
    public interface IEndpoint
    {
        IConnection ConnectedTo { get; set; }
        string Name { get; set; }
    }
}