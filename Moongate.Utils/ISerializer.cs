namespace Moongate.Utils
{
	public interface ISerializer
	{
		byte[] Serialize<T> (T o);
		T Deserialize<T> (byte[] b);
	}
}
