using System.ComponentModel.DataAnnotations;
using Basic;

namespace Domain;

public class Customer : BaseEntity<int>
{
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = null!;

    [MaxLength(50)]
    public string? FatherName { get; set; } = null!;

    /// <summary>
    ///     Shomare shenasname
    /// </summary>
    public int? CertificateNumber { get; set; }

    [Range(10000000, 9999999999)]
    public long NationalCode { get; set; }

    public DateOnly? BirthDate { get; set; }

    [Required]
    public string Mobile { get; set; } = null!;

    [MaxLength(1000)]
    public string Address { get; set; } = null!;
}