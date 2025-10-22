using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ksi.Models.Entity
{
    [Table("canteen")]
    public class canteen
    {
        [Key]
        public int item_id { get; set; }

        [ForeignKey("canteen_id")]
        public int canteen_id { get; set; }

        public string dish_name { get; set; }
        public string availability { get; set; }
        public decimal price { get; set; }
        public bool morning { get; set; }
        public bool afternoon { get; set; }
        public bool evening { get; set; }
        public bool snacks { get; set; }

        public bool is_active { get; set; }
        public DateTime created_date { get; set; }
        public string? created_by { get; set; }
        public DateTime? updated_date { get; set; }
        public string? updated_by { get; set; }
        public DateTime? deleted_date { get; set; }
        public string? deleted_by { get; set; }
    }
}
