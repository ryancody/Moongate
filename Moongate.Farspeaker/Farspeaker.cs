namespace Moongate.IO
{
	public class Farspeaker
	{
		public Input Input { get; }
		public Output Output { get; }

		public Farspeaker (Input input, Output output)
		{
			Input = input;
			Output = output;
		}
	}
}
