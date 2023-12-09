import { AppState } from "../AppState"
import { Vault } from "../models/Vault"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService {

    async getProfileVaults(vaultId) {
        const res = await api.get(`api/profiles/${vaultId}/vaults`)
        logger.log('vaults', res.data)
        AppState.vaults = res.data.map(v => new Vault(v))
    }

}

export const vaultsService = new VaultsService()