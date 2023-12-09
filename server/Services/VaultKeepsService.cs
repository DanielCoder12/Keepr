


namespace Keepr.Services;

public class VaultKeepsService
{
    private readonly VaultKeepsRepository _repo;
    private readonly VaultsService _vaultsService;
    public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository, VaultsService vaultsService)
    {
        _repo = vaultKeepsRepository;
        _vaultsService = vaultsService;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
        Vault vault = _vaultsService.GetVaultById(vaultKeepData.VaultId, vaultKeepData.CreatorId);
        if (vault.CreatorId != vaultKeepData.CreatorId)
        {
            throw new Exception("YOU CANNOT ADD TO THIS VAULT");
        }
        VaultKeep vaultKeep = _repo.CreateVaultKeep(vaultKeepData);
        return vaultKeep;
    }

    internal string DeleteVaultKeep(int vaultKeepId, string id)
    {
        VaultKeep vaultKeep = GetVaultKeepById(vaultKeepId);
        if (vaultKeep.CreatorId != id)
        {
            throw new Exception("YOU CANNOT DELETE THIS");
        }
        _repo.DeleteVaultKeep(vaultKeepId);
        return "vaultKeep Deleted";

    }

    internal VaultKeep GetVaultKeepById(int vaultKeepId)
    {
        VaultKeep vaultKeep = _repo.GetVaultKeepById(vaultKeepId);
        if (vaultKeep == null)
        {
            throw new Exception($"Invalid id{vaultKeepId}");
        }
        return vaultKeep;
    }

}