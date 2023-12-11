import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { Profile } from '../models/Profile'
import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }
  setAccountInAppState() {
    logger.log(AppState.account)
    AppState.activeProfile = new Profile(AppState.account)
  }
}

export const accountService = new AccountService()
