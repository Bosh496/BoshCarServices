namespace BoshCarServices.ViewModels
{
    public class ServiceResultVM
    {
        public int ServiceId { get; set; }

        public string Name { get; set; }

        public string Mobile { get; set; }

        public string RegNum { get; set; }
        public string ServiceTypes { get; set; }
        public decimal TotalBill { get; set; }
        public DateTime ServiceDate { get; set; }

        public int RewardPoints { get; set; }
        public byte[]? FileData { get; set; }
        public string? ContentType { get; set; }
        public string? FileName { get; set; }


    }
}
