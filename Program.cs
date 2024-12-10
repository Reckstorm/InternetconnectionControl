using System.Management;

void Enable(ManagementObject obj) => obj.InvokeMethod("Enable", null);
void Disable(ManagementObject obj) => obj.InvokeMethod("Disable", null);

try
{
    var query = new SelectQuery("Win32_NetworkAdapter");
    using (var searcher = new ManagementObjectSearcher(query))
    {
        foreach (ManagementObject obj in searcher.Get())
        {
            if (obj["NetEnabled"] != null)
                Disable(obj);
        }

        Task.Delay(5000).Wait();

        foreach (ManagementObject obj in searcher.Get())
        {
            if (obj["NetEnabled"] != null && !(bool)obj["NetEnabled"])
                Enable(obj);
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine("Error: " + ex.Message);
}