


namespace Keepr.Services;

public class VaultsService
{
    private readonly VaultsRepository _repo;

    public VaultsService(VaultsRepository repo)
    {
        _repo = repo;
    }

    internal Vault CreateVault(Vault vaultData)
    {
        Vault vault = _repo.CreateVault(vaultData);
        return vault;
    }

    internal string DestroyVault(string userId, int vaultId)
    {
        Vault vault = GetVaultById(vaultId, userId);
        if (vault.CreatorId != userId)
        {
            throw new Exception("NOT YOUR VAULT");
        }
        _repo.DestroyVault(vaultId);
        return $"{vault.Name} deleted";
    }

    internal Vault EditVault(int vaultId, Vault vaultData, string id)
    {
        Vault oldVault = GetVaultById(vaultId, id);
        if (oldVault.CreatorId != id)
        {
            throw new Exception("NOT YOUR VAULT");
        }
        oldVault.Name = vaultData.Name ?? oldVault.Name;
        oldVault.IsPrivate = vaultData.IsPrivate ?? oldVault.IsPrivate;
        Vault vault = _repo.EditVault(oldVault);
        return vault;

    }

    internal Vault GetVaultById(int vaultId, string userId)
    {
        Vault vault = _repo.GetVaultById(vaultId);
        if (vault == null)
        {
            throw new Exception($"invalid id{vaultId}");
        }
        if (vault.IsPrivate == true && userId != vault.CreatorId)
        {
            throw new Exception("Private Vault");
        }
        return vault;
    }
}