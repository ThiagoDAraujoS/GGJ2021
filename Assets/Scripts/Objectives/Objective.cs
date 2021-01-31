public class Objective
{
	public CollectibleDefinition Target { get; private set; }
	public bool Found { get; set; } = false;

	public Objective(CollectibleDefinition target)
	{
		this.Target = target;
	}
}
