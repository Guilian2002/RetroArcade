namespace RetroArcade.BlazorWebAssembly.Models.RetroArcade.Manager
{
    public class ManagerViewModel
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; } = string.Empty;
        public string Lastname { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName => $"{Firstname} {Lastname}";
    }
}
