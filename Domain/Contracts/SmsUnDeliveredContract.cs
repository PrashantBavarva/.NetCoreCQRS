namespace Domain.Contracts;

public record SmsUnDeliveredContract(string SmsId) : ISmsContract;