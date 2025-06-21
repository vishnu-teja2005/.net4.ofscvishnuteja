using system;
namespace SingletonPattern
{
	public class Logger
	{
		private static Logger Instance;
		private Logger()
		{
			Console.WriteLine("Logger instance created");
		}
		public static Logger GetInstance()
		{
			if(instance==null)
			{
				instance = new Logger();
			}
			return instance;
		}
		public void DisplayMsg(String msg)
		{
			Console.WriteLine("Msg:"+msg);
		}
	}
public class Test
{
	static void main(String args[])
	{
		Logger obj1=Logger.GetInstance();
		Logger obj2=Logger.GetInstance();
		obj1.DisplayMsg("this is first message");
		obj1.DisplayMsg("this is second message");
	}
}
}
