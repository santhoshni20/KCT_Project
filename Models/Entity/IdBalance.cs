using KSI_Project.Models.Entity;

namespace KSI_Project.Models.Entity
{
    public class IdBalance
    {
        public int RollNo { get; set; }
        public string StudName { get; set; }
        public decimal BalanceAmt { get; set; }
        public string PreviousArrearBill { get; set; } // JSON stored as string

        public Student Student { get; set; }
    }
}
