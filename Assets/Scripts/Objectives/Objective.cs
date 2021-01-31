public class Objective
{
	public CollectibleDefinition Target { get; private set; }
	public string Description { get; private set; }
	public bool Found { get; set; } = false;

	public Objective(CollectibleDefinition target, string description)
	{
		this.Target = target;
		this.Description = description;
	}
}
