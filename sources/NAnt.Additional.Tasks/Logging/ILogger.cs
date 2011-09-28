using NAnt.Core;

namespace NAnt.Additional.Tasks.Logging
{
	public interface ILogger
	{
		void Log(Level level, string message);
	}
}