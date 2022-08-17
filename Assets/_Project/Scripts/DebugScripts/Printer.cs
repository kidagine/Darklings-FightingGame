public static class Printer
{

	private static DebugMenu _debugMenu;


	public static void Log(object message)
	{
		_debugMenu.Log(message);
	}

	public static void LogWarning(object message)
	{
		_debugMenu.Log(message);

	}

	public static void LogError(object message)
	{
		_debugMenu.Log(message);
	}

	public static void SetLoaded(DebugMenu debugMenu)
	{
		_debugMenu = debugMenu;
	}
}