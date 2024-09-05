import { iUserDto } from "../Dto/user-dto"

export interface iAuthResponse {
  user: iUserDto
  token: string
  tokenExpiration: string
}
