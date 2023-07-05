
namespace Shared
{
    public class ItemType
    {
        public ItemType(string Name, string Type, string Path, long Size)
        {
            this.Name = Name;
            this.Type = Type;
            this.Path = Path;
            this.Size = Size;
        }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
    }

    public class ProcessInfo
    {
        public ProcessInfo(int Pid, string Name, string Description, long Size)
        {
            this.Pid = Pid;
            this.Name = Name;
            this.Description = Description;
            this.Size = Size;
        }
        public int Pid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long Size { get; set; }
    }

    public class SystemInfo
    {
        public SystemInfo(PlatformID OSVersion, string MachineName, bool Is64BitOperatingSystem, string UserName)
        {
            this.OSVersion = OSVersion;
            this.MachineName = MachineName;
            this.Is64BitOperatingSystem = Is64BitOperatingSystem;
            this.UserName = UserName;
        }
        public PlatformID OSVersion { get; set; }
        public string MachineName { get; set; }
        public bool Is64BitOperatingSystem { get; set; }
        public string UserName { get; set; }
    }
    public class Types
    {
        public static readonly string EOS = "<END>";

        private static readonly IDictionary<SocketCommands, string> _listCommands = new Dictionary<SocketCommands, string>()
        {
            {SocketCommands.NotifyMessage, "<NotifyMessage>" },
            {SocketCommands.ExplorePath, "<ExplorePath>" },
            {SocketCommands.UploadFile, "<NotifyMSG>" },
            {SocketCommands.DownLoadFile, "<UploadFile>" },
            {SocketCommands.NotifyShellCommand, "<NotifyShellCommand>" },
            {SocketCommands.NotifyOSInformations, "<NotifyOSInformations>" },
            {SocketCommands.RemoteShutdown, "<RemoteShutdown>" },
            {SocketCommands.PingTarget, "<PingTarget>" },
            {SocketCommands.ExecFile, "<ExecFile>" },
            {SocketCommands.GetProcesses, "<GetProcesses>" }
        };

        public static string SocketCommand(SocketCommands SocketCommand) => _listCommands[SocketCommand];
    }
}
