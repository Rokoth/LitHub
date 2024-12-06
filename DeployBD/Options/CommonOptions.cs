namespace DeployBD.Options
{
    public class CommonOptions
    {
        public PGConnection Connection { get; set; }
        public string Test { get; set; }
    }

    public class PGConnection
    {
        public string Server { get; set; }
        public string Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}
