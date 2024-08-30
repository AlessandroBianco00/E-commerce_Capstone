import { iUser } from "./user"

export interface iAuthResponse {
  user: iUser
  token: string
  tokenExpiration: string
}
