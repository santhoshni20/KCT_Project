using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ksi.Models.Entity
{
    [Table("mstRoom")]
    public class mstRoom
    {
        [Key]
        public int roomId { get; set; }
        public string roomNumber { get; set; }
        public bool isActive { get; set; }
        public int createdBy { get; set; }
        public DateTime createdDate { get; set; }
    }

}
