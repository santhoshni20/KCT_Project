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
        public bool isActive { get; set; }
        public int createdBy { get; set; }
        public DateTime createdDate { get; set; }
    }

}
