export class VaultKeep {
    constructor(data) {
        this.id = data.id || null
        this.keepId = data.keepId
        this.vaultId = data.vaultId
        this.creatorId = data.creatorId
    }
}