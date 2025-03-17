namespace IdentecSolutions.Application.Contracts
{
    public sealed record ErrorResponse
    {
        public ErrorResponse()
        {

        }
        public ErrorResponse(string code, string reason)
        {
            Code = code;
            Reason = reason;
        }

        public ErrorResponse(string code, IReadOnlyDictionary<string, string[]>? reasons)
        {
            Code = code;
            Reasons = reasons;

        }
        public string Code { get; set; }
        public string? Reason { get; set; }
        public IReadOnlyDictionary<string, string[]>? Reasons { get; set; }
    }
}
