import { iUser } from "./user";

export interface iRole {
    roleId: number;
    roleName: string;
    users: iUser[];
}
