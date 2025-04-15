namespace ReceiptReimbursementApi.Models
{
    //Model binding used for form data submission
    public class ReceiptSubmission
    {
        public int Id {get; set;}
        //Names in form submission is the same, check Index.html "names"
        public DateTime Date {get; set;}
        public decimal Amount {get; set;}
        public string Description {get; set;}
        public string FilePath {get; set;}
    }
}