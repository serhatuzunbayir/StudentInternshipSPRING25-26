namespace StudentWeb.Models;

public class ProfileEditViewModel
{
    public string FullName { get; set; } = string.Empty;
    public string? Skills { get; set; }
    public string? Education { get; set; }
    public string? Experience { get; set; }
    public string? Phone { get; set; }
    public string? AboutMe { get; set; }
}

