
using Domain.Enums;

namespace Domain.Dto
{
    public class SearchSmsQueryResponse
    {

        public string Id { get; set; }
        public string ClientId { get; set; }
        public string MessageId { get; set; }
        public string RefId { get; set; }
        public string Message { get; set; }
        public string Recipient { get; set; }
        public string SenderId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Error { get; set; }
        public SmsStatus Status { get; set; }
        public string Provider { get; set; }
    }
}
