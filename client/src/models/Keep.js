export class Keep {
    constructor(data) {
        this.id = data.id
        this.creatorId = data.creatorId
        this.name = data.name || data.keep.name
        this.description = data.description || data.keep.description
        this.img = data.img || data.keep.img
        this.kept = data.kept
        this.views = data.views
        this.creator = data.creator
        this.vaultKeepId = data.vaultKeepId || null
    }
}