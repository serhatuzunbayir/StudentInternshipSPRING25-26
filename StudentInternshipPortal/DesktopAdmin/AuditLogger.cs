namespace DesktopAdmin;

public class AuditLogger
{
	public delegate void AdminActionEventHandler(string action);
	public event AdminActionEventHandler? AdminActionPerformed;

	public void Log(string action)
	{
		AdminActionPerformed?.Invoke(action);
	}
}