namespace MIP
{
    public interface IConnection
    {
        IInput Input { get; set; }
        IEndpoint Output { get; set; }
    }
}