namespace OPSExporter.Data.Entity;

public class Node {
    public string? DeviceName { get; set; }

    public DateTime ActualTime { get; set; }

    public DateTime Time { get; set; }

    public double ValueDouble { get; set; }

    public long ValueInteger { get; set; }

    public long ValueUInt { get; set; }
}