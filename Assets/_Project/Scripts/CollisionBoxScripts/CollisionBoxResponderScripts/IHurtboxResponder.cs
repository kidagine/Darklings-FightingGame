public interface IHurtboxResponder
{
	bool TakeDamage(AttackSO attackSO);
	public bool BlockingLow { get; set; }
	public bool BlockingHigh { get; set; }
	public bool BlockingMiddair { get; set; }

}
