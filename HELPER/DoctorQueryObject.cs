namespace Cliniq.HELPER
{
    public class DoctorQueryObject
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Specialization { get; set; }
        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;
        public bool SortDescending
        {
            get => IsDescending;
            set => IsDescending = value;
        }
    }
}
