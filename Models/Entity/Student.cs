using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KSI_Project.Models.Entity
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string RollNo { get; set; } = string.Empty;

        [Required]
        public string Name { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal Balance { get; set; }

        // Navigation property
        public List<TransactionHistory> Transactions { get; set; } = new List<TransactionHistory>();
    }

    public class TransactionHistory
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Student")]
        public int StudentId { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        // Navigation back to student
        public Student? Student { get; set; }
    }
}