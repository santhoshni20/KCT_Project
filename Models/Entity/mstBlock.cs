using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ksi.Models.Entity
{
    [Table("mstBlock")]
    public class mstBlock
    {
        [Key]
        public int blockId { get; set; }
        public string blockName { get; set; }
        public bool isActive { get; set; } = true;
        public int createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public int? updatedBy { get; set; }
        public DateTime? updatedDate { get; set; }
        public int? deletedBy { get; set; }
        public DateTime? deletedDate { get; set; }
    }

}
