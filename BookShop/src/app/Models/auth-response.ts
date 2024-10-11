import { iUserLoginDto } from "../Dto/user-login-dto"

export interface iAuthResponse {
  user: iUserLoginDto
  token: string
  tokenExpiration: string
}
