namespace Application.Customers.Dto;

public class CustomerDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? FatherName { get; set; } = null!;
    public int? CertificateNumber { get; set; }
    public long NationalCode { get; set; }
    public DateOnly? BirthDate { get; set; }
    public string Mobile { get; set; } = null!;
    public string Address { get; set; } = null!;
}