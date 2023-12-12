import { variationPlacements } from "@popperjs/core"
import { AppState } from "../AppState"
import { Vault } from "../models/Vault"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"
import { Keep } from "../models/Keep"
import { VaultKeep } from "../models/VaultKeep"

class VaultsService {

    async getProfileVaults(vaultId) {
        const res = await api.get(`api/profiles/${vaultId}/vaults`)
        logger.log('vaults', res.data)
        AppState.vaults = res.data.map(v => new Vault(v))
    }

    async getAccountVaults() {
        const res = await api.get('account/vaults')
        logger.log(res.data)
        // AppState.vaults = res.data.map(v => new Vault(v))
        AppState.accountVaults = res.data.map(v => new Vault(v))
    }

    async getKeepsByVaultId(vaultId) {
        const res = await api.get(`api/vaults/${vaultId}/keeps`)
        logger.log('keeps in vault', res.data)
        AppState.keeps = res.data.map(k => new Keep(k))
    }

    async getVaultById(vaultId) {
        const res = await api.get(`api/vaults/${vaultId}`)
        logger.log('vault', res.data)
        AppState.activeVault = new Vault(res.data)
    }

    clearAppState() {
        AppState.activeVault = {}
        AppState.activeProfile = {}
        AppState.keeps = []
        AppState.vaults = []
    }

    async saveKeepToVault(vaultId, keepId) {
        const vaultKeepData = { vaultId, keepId }
        const res = await api.post('api/vaultkeeps', vaultKeepData)
        logger.log('vault keep created', res.data)
        AppState.vaultsWithActiveKeep.push(new VaultKeep({ id: null, vaultId, keepId, creatorId: AppState.account.id }))
    }

    async deleteVault(vaultId) {
        await api.delete(`api/vaults/${vaultId}`)
        AppState.accountVaults = AppState.accountVaults.filter(v => v.id != vaultId)
    }

    async createVault(vaultData) {
        logger.log(vaultData)
        const res = await api.post('api/vaults', vaultData)
        AppState.accountVaults.push(new Vault(res.data))
        return new Vault(res.data)
    }

    async getVaultIdsKeepsAreIn(keepId) {
        const res = await api.get(`api/keeps/${keepId}/vaultKeeps`)
        logger.log('vault keeps', res.data)
        AppState.vaultsWithActiveKeep = res.data.map(vk => new VaultKeep(vk))
    }
    async removeKeepFromVault(vaultKeepId) {
        await api.delete(`api/vaultkeeps/${vaultKeepId}`)
        AppState.keeps = AppState.keeps.filter(k => k.vaultKeepId != vaultKeepId)
    }

    changeEditStatus() {
        AppState.isEditing = true
    }
    changeEditStatusFalse() {
        AppState.isEditing = false
    }
}

export const vaultsService = new VaultsService()