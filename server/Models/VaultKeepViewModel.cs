namespace Keepr.Models;

public class VaultKeepViewModel
{
    public int Id { get; set; }
    public Profile Creator { get; set; }
    public int VaultKeepId { get; set; }
    public Keep Keep { get; set; }


}