namespace ksi.Models.Entity
{
    public class mstEventDetails
    {
        public int mstEventId { get; set; }

        public string eventName { get; set; }
        public string organisedBy { get; set; }
        public DateTime registrationDeadline { get; set; }
        public string brochureImagePath { get; set; }
        public DateTime eventDate { get; set; }
        public string contactNumber { get; set; }

        public bool isActive { get; set; } = true;

        public int createdBy { get; set; }
        public DateTime createdDate { get; set; }

        public int? updatedBy { get; set; }
        public DateTime? updatedDate { get; set; }

        public int? deletedBy { get; set; }
        public DateTime? deletedDate { get; set; }
    }
}
