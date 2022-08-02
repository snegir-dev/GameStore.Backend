namespace GameStore.Domain;

public class Log
{
    public long Id { get; set; }
    public string Application { get; set; }
    public string Logged { get; set; }
    public string Level { get; set; }
    public string Message { get; set; }
    public string Logger { get; set; }
    public string CallSite { get; set; }
    public string Exception { get; set; }
}