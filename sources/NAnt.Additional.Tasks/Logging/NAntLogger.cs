using NAnt.Core;

namespace NAnt.Additional.Tasks.Logging
{
	internal class NAntLogger : ILogger
	{
		#region Fields

		private readonly Task task;

		#endregion Fields

		#region Constructors

		public NAntLogger(Task task)
		{
			this.task = task;
		}

		#endregion Constructors

		#region ILogger implementation

		public void Log(Level level, string message)
		{
			task.Log(level, message);
		}

		#endregion ILogger implementation
	}
}