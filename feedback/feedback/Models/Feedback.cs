namespace feedback.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string? Message { get; set; }
        public DateTime? CreatedAt { get; set; }
        public FeedbackStatus? Status { get; set; } // Шинэ, шийдвэрлэсэн
        public string? Response { get; set; } // Хариу
    }

    public enum FeedbackStatus
    {
        New,
        Resolved
    }

}
