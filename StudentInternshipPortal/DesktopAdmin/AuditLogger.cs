namespace DesktopAdmin;

// This class handles basic audit logging of administrator actions.
public class AuditLogger
{
	// Define a custom delegate type. It takes a string representing what the admin did.
	public delegate void AdminActionEventHandler(string action);

	// This event will trigger when an admin performs an action. It uses our delegate above.
	public event AdminActionEventHandler? AdminActionPerformed;

	// Invokes the event so any registered listeners (like the logging listbox) get notified.
	public void Log(string action)
	{
		AdminActionPerformed?.Invoke(action);
	}
}