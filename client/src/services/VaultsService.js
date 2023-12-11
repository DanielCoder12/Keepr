import { variationPlacements } from "@popperjs/core"
import { AppState } from "../AppState"
import { Vault } from "../models/Vault"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"
import { Keep } from "../models/Keep"

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
        AppState.keeps = res.data.map(k => new Keep(res.data))
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

}

export const vaultsService = new VaultsService()