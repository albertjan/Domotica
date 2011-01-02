namespace HAL
{
    public interface IHardwareEndpointIndentifier
    {
        string ID { get; set; }
        HardwareEndpointType Type { get; set; }
    }

    public enum HardwareEndpointType
    {
        Input,
        Output
    }
}