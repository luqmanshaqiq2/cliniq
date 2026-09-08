using Cliniq.MODELS.ENUM;

namespace Cliniq.MODELS.DTO;

public class PageResult<T>
{
    public IReadOnlyList<T> Items { get; set; } = Array.Empty<T>();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public class CreateTreatmentDto
{
    public string Diagnosis { get; set; } = string.Empty;
    public TreatmentType TreatmentType { get; set; }
    public string? Prescription { get; set; }
    public string? SurgeryDetails { get; set; }
    public string? Notes { get; set; }
}
