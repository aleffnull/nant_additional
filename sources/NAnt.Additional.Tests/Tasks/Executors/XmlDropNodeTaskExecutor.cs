using NAnt.Additional.Tasks;

namespace NAnt.Additional.Tests.Tasks.Executors
{
	internal class XmlDropNodeTaskExecutor : XmlDropNodeTask
	{
		public void CallExecuteTask()
		{
			ExecuteTask();
		}
	}
}