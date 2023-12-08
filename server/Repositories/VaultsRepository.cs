



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
}