





using System.Data.Common;

namespace Keepr.Repositories;

public class VaultsRepository
{
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Vault CreateVault(Vault vaultData)
    {
        string sql = @"
        INSERT INTO vaults(creatorId, name, description, isPrivate, img)
        VALUES(@CreatorId, @Name, @Description, @IsPrivate, @Img)
        ;
        SELECT 
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts
        WHERE vaults.id = LAST_INSERT_ID()
        ;";

        Vault vault = _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
        {
            v.Creator = p;
            return v;
        }, vaultData).FirstOrDefault();
        return vault;
    }

    internal void DestroyVault(int vaultId)
    {
        string sql = @"
        DELETE FROM vaults WHERE vaults.id = @vaultId
        ;";
        _db.Execute(sql, new { vaultId });
    }

    internal Vault EditVault(Vault oldVault)
    {
        string sql = @"
         UPDATE vaults
        SET name = @Name,
        isPrivate = @IsPrivate
        WHERE vaults.id = @Id
        ;
        SELECT 
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts ON accounts.id = vaults.creatorId
        WHERE vaults.id = @Id;";

        Vault vault = _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
        {
            v.Creator = p;
            return v;
        }, oldVault).FirstOrDefault();
        return vault;
    }

    internal List<VaultKeepViewModel> GetKeepsInVault(int vaultId)
    {
        string sql = @"
         SELECT
        vaultKeeps.*,
        accounts.*,
        keeps.*,
        COUNT(vk.id) AS kept
        FROM vaultKeeps 
        JOIN accounts ON accounts.id = vaultKeeps.creatorId
        JOIN keeps ON keeps.id = vaultKeeps.keepId
        LEFT JOIN vaultKeeps vk ON vk.keepId = keeps.id
        WHERE vaultKeeps.vaultId = @vaultId
        GROUP BY(keeps.id)
        ;";

        List<VaultKeepViewModel> vaultKeep = _db.Query<VaultKeepViewModel, Profile, Keep, VaultKeepViewModel>(sql, (vk, p, k) =>
        {
            vk.Keep = k;
            vk.Creator = p;
            vk.VaultKeepId = vk.Id;
            vk.Id = k.Id;
            return vk;
        }, new { vaultId }).ToList();
        return vaultKeep;
    }

    internal Vault GetVaultById(int vaultId)
    {
        string sql = @"
        SELECT 
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts ON accounts.id = vaults.creatorId
        WHERE vaults.id = @vaultId;";
        Vault vault = _db.Query<Vault, Profile, Vault>(sql, (v, p) =>
        {
            v.Creator = p;
            return v;
        }, new { vaultId }).FirstOrDefault();
        return vault;
    }

    internal List<Vault> GetVaultsByAccountId(string id)
    {
        string sql = @"
        SELECT * FROM vaults where vaults.creatorId = @id
        ;";
        List<Vault> vaults = _db.Query<Vault>(sql, new { id }).ToList();
        return vaults;
    }

    internal List<Vault> GetVaultsByProfileId(string profileId)
    {
        string sql = "SELECT * FROM vaults WHERE vaults.creatorId = @profileId;";
        List<Vault> vaults = _db.Query<Vault>(sql, new { profileId }).ToList();
        return vaults;
    }
}