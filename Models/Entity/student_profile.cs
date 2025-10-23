using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KsiProject.Entities
{
    [Table("student_profile")]
    public class StudentProfile
    {
        [Key]
        [Column("student_id")]
        public int student_id { get; set; }

        [Column("name")]
        public string? name { get; set; }

        [Column("roll_number")]
        public string roll_number { get; set; }

        [Column("password_hash")]
        public string password_hash { get; set; }

        [Column("department")]
        public string department { get; set; }

        [Column("section")]
        public string section { get; set; }

        [Column("dob")]
        public DateTime? dob { get; set; }

        [Column("contact_number")]
        public string? contact_number { get; set; }

        [Column("address")]
        public string address { get; set; }

        [Column("father_name")]
        public string father_name { get; set; }

        [Column("mother_name")]
        public string mother_name { get; set; }

        [Column("got_placed")]
        public string got_placed { get; set; } // 'yes' or 'no'

        [Column("company_name")]
        public string company_name { get; set; }

        [Column("domain")]
        public string domain { get; set; }

        [Column("email")]
        public string? email { get; set; }

        [Column("is_active")]
        public bool? is_active { get; set; }

        [Column("created_date")]
        public DateTime? created_date { get; set; }

        [Column("created_by")]
        public string created_by { get; set; }

        [Column("updated_date")]
        public DateTime? updated_date { get; set; }

        [Column("updated_by")]
        public string updated_by { get; set; }

        [Column("deleted_date")]
        public DateTime? deleted_date { get; set; }

        [Column("deleted_by")]
        public string deleted_by { get; set; }
    }
}
