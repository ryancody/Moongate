namespace Moongate.MessageProcessor
{
	interface IMessageProcessor
	{
		void Process (ITransmittable t);
	}
}
