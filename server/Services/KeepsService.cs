



namespace Keepr.Services;

public class KeepsService
{
    private readonly KeepsRepository _repo;

    public KeepsService(KeepsRepository repo)
    {
        _repo = repo;
    }

    internal Keep CreateKeep(Keep keepData)
    {
        Keep keep = _repo.CreateKeep(keepData);
        return keep;
    }

    internal string DestroyKeep(int keepId, string userId)
    {
        Keep keep = GetKeepById(keepId);
        if (keep.CreatorId != userId)
        {
            throw new Exception("You Cannot Delete This");
        }
        _repo.DestroyKeep(keepId);
        return $"deleted {keep.Name}";
    }

    internal Keep EditKeep(Keep keepData, string userId, int keepId)
    {
        Keep oldKeep = GetKeepById(keepId);
        if (oldKeep.CreatorId != userId)
        {
            throw new Exception("NOT YOUR KEEP");
        }
        oldKeep.Name = keepData.Name ?? oldKeep.Name;
        oldKeep.Description = keepData.Description ?? oldKeep.Description;
        Keep keep = _repo.EditKeep(oldKeep);
        return keep;

    }

    internal List<Keep> GetAllKeeps()
    {
        List<Keep> keeps = _repo.GetAllKeeps();
        return keeps;
    }

    internal Keep GetKeepById(int keepId)
    {
        Keep keep = _repo.GetKeepById(keepId);
        if (keep == null)
        {
            throw new Exception($"invalid id:{keepId}");
        }
        return keep;
    }

    internal Keep GetKeepByIdAndIncrement(int keepId)
    {
        Keep keep = _repo.GetKeepByIdAndIncrement(keepId);
        if (keep == null)
        {
            throw new Exception($"invalid id:{keepId}");
        }
        return keep;
    }

    internal List<Keep> GetKeepsByProfileId(string profileId)
    {
        List<Keep> keeps = _repo.GetKeepsByProfileId(profileId);
        return keeps;

    }
}