






namespace Keepr.Repositories;

public class KeepsRepository
{
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Keep CreateKeep(Keep keepData)
    {
        string sql = @"
        INSERT INTO keeps (creatorId, name, description, img, views)
        VALUES(@CreatorId, @Name, @Description, @Img, 0);
        SELECT 
        keeps.*,
        COUNT(vaultKeeps.id) AS kept,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creatorId = accounts.id 
        LEFT JOIN vaultKeeps ON vaultKeeps.keepId = keeps.id
        WHERE keeps.id = LAST_INSERT_ID()
        GROUP BY(keeps.id);";
        Keep keep = _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
        {
            k.Creator = p;
            return k;
        }, keepData).FirstOrDefault();
        return keep;

    }

    internal void DestroyKeep(int keepId)
    {
        string sql = @"DELETE FROM keeps WHERE id = @keepId LIMIT 1;";
        _db.Execute(sql, new { keepId });
    }

    internal Keep EditKeep(Keep keepData)
    {
        string sql = @"
        UPDATE keeps
        SET name = @Name,
        description = @Description
        WHERE keeps.id = @Id;
        SELECT 
        keeps.*,
        COUNT(vaultKeeps.id) AS kept,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creatorId = accounts.id 
        LEFT JOIN vaultKeeps ON vaultKeeps.keepId = keeps.id
        WHERE keeps.id = @Id
        GROUP BY(keeps.id);";
        Keep keep = _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
       {
           k.Creator = p;
           return k;
       }, keepData).FirstOrDefault();
        return keep;
    }

    internal List<Keep> GetAllKeeps()
    {
        string sql = @"
         SELECT 
        keeps.*,
        COUNT(vaultKeeps.id) AS kept,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creatorId = accounts.id 
        LEFT JOIN vaultKeeps ON vaultKeeps.keepId = keeps.id
        GROUP BY(keeps.id)
        ;";

        List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
        {
            k.Creator = p;
            return k;
        }).ToList();
        return keeps;
    }

    internal Keep GetKeepById(int keepId)
    {
        string sql = @"
        SELECT 
        keeps.*,
        COUNT(vaultKeeps.id) AS kept,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creatorId = accounts.id 
        LEFT JOIN vaultKeeps ON vaultKeeps.keepId = keeps.id
        WHERE keeps.id = @keepId
        GROUP BY(keeps.id)
        ;";
        Keep keep = _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
        {
            k.Creator = p;
            return k;
        }, new { keepId }).FirstOrDefault();
        return keep;
    }

    internal Keep GetKeepByIdAndIncrement(int keepId)
    {
        string sql = @"
        UPDATE keeps 
        SET views = views +1
        WHERE keeps.id = @keepId
        LIMIT 1;
         SELECT 
        keeps.*,
        COUNT(vaultKeeps.id) AS kept,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creatorId = accounts.id 
        LEFT JOIN vaultKeeps ON vaultKeeps.keepId = keeps.id
        WHERE keeps.id = @keepId
        GROUP BY(keeps.id);";
        Keep keep = _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
       {
           k.Creator = p;
           return k;
       }, new { keepId }).FirstOrDefault();
        return keep;
    }

    internal List<Keep> GetKeepsByProfileId(string profileId)
    {
        string sql = @"
        SELECT 
        keeps.*,
        COUNT(vaultKeeps.id) AS kept,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creatorId = accounts.id 
        LEFT JOIN vaultKeeps ON vaultKeeps.keepId = keeps.id
        WHERE keeps.creatorId = @profileId
        GROUP BY(keeps.id)
        ;";
        List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, (k, p) =>
        {
            k.Creator = p;
            return k;
        }, new { profileId }).ToList();
        return keeps;
    }
}