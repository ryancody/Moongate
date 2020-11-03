namespace Moongate.Utils
{
	public interface ISerializer
	{
		byte[] Serialize (object o);
		T Deserialize<T> (byte[] b);
	}
}
