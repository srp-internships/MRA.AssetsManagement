namespace MRA.AssetsManagement.Domain.Entities
{
    public record Properties
    {
        public string Label { get; set; } = null!;
        public string Value { get; set; } = null!;
    }
}
