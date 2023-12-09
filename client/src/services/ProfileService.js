import { AppState } from "../AppState"
import { Profile } from "../models/Profile"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class ProfileService {

    async getProfile(keepId) {
        const res = await api.get(`api/profiles/${keepId}`)
        AppState.activeProfile = new Profile(res.data)
    }

}

export const profileService = new ProfileService()