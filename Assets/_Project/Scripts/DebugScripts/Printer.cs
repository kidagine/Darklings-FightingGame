public static class Printer
{

	private static DebugMenu _debugMenu;


	public static void Log(object message)
	{
		if (_debugMenu != null)
			_debugMenu.Log(message);
	}

	public static void LogWarning(object message)
	{
		if (_debugMenu != null)
			_debugMenu.Log(message);

	}

	public static void LogError(object message)
	{
		if (_debugMenu != null)
			_debugMenu.Log(message);
	}

	public static void SetLoaded(DebugMenu debugMenu)
	{
		_debugMenu = debugMenu;
	}
}