namespace Domain.Abstractions
{
    public interface IHasBankName
    {
        public string? BankName { get; set; }
        public Guid BankId { get; set; }
    }
}
