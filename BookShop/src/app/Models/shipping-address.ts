import { iUser } from "./user";

export interface iShippingAddress {
    shippingAddressId: number;
    streetAddress: string;
    city: string;
    zipCode: number;
    country: string;
    userId: number;
    user: iUser;
}
