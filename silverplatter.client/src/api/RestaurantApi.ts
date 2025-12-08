import type { Restaurant } from "../Types/Restaurant";
import http from "./http";

export const restaurantApi = {
    getAll: async (): Promise<Restaurant[]> => http<Restaurant[]>(`restaurant`),

    getById: async (id: number): Promise<Restaurant> => http<Restaurant>(`restaurant/id=${id}`),

    create: async (entry: Omit<Restaurant, "id">): Promise<Restaurant> => http<Restaurant>(`restaurant`, {
        method: "POST",
        body: JSON.stringify(entry),
    }),

    update: async (entry: Restaurant): Promise<Restaurant> => http<Restaurant>(`restaurant/id=${entry.Id}`, {
        method: "PUT",
        body: JSON.stringify(entry),
    }),

    delete: async (id: number): Promise<void> => http<void>(`restaurant/id=${id}`, {
        method: "DELETE",
    }),
}