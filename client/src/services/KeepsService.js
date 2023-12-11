import { AppState } from "../AppState"
import { Keep } from "../models/Keep"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService {

    async getKeeps() {
        const res = await api.get('api/keeps')
        logger.log('keeps', res.data)
        AppState.keeps = res.data.map(k => new Keep(k))
    }

    async setActiveKeep(keepId) {
        const res = await api.get(`api/keeps/${keepId}`)
        AppState.activeKeep = new Keep(res.data)
    }

    async getProfileKeeps(profileId) {

        const res = await api.get(`api/profiles/${profileId}/keeps`)
        logger.log('keeps', res.data)
        AppState.keeps = res.data.map(k => new Keep(k))
    }

    async deleteKeep(keepId) {
        await api.delete(`api/keeps/${keepId}`)
        AppState.keeps = AppState.keeps.filter(k => k.id != keepId)
    }

}

export const keepsService = new KeepsService()