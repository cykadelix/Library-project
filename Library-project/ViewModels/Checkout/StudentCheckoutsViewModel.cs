namespace Library_project.ViewModels.Checkout
{
    public class StudentCheckoutsViewModel
    {
        public string? MediaType { get; set; }
        public string? Brand { get; set; }
        public string? SerialNo { get; set; }
        public string? Title { get; set; }
        public string? CheckoutDate { get; set; }
        public string? ReturnDate { get; set; }
        public bool? Returned { get; set; }
        public string? ReturnedDate { get; set; }
    }
}
