import type { RestaurantFavorite } from "../Types/RestaurantFavorite";

export const restaurantFavoriteService = {
    getAll: async (): Promise<RestaurantFavorite[]> => {
        const response = await fetch(`/api/restaurantfavorite`, {
            headers: { "Content-Type": "application/json" }
        });

        if (!response.ok) {
            throw new Error(await response.text() || "API error");
        }

        return response.json();
    },

    getById: async (id: number): Promise<RestaurantFavorite> => {
        const response = await fetch(`/api/restaurantfavorite/id=${id}`, {
            headers: { "Content-Type": "application/json" }
        });

        if (!response.ok) {
            throw new Error(await response.text() || "API error");
        }

        return response.json();
    },

    getByUserId: async (userId: number): Promise<RestaurantFavorite[]> => {
        const response = await fetch(`/api/restaurantfavorite/user/${userId}`, {
            headers: { "Content-Type": "application/json" }
        });

        if (!response.ok) {
            throw new Error(await response.text() || "API error");
        }

        return response.json();
    },

    create: async (favorite: Omit<RestaurantFavorite, "id">): Promise<RestaurantFavorite> => {
        const response = await fetch(`/api/restaurantfavorite`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(favorite)
        });

        if (!response.ok) {
            throw new Error(await response.text() || "API error");
        }

        return response.json();
    },

    update: async (favorite: RestaurantFavorite): Promise<RestaurantFavorite> => {
        const response = await fetch(`/api/restaurantfavorite/id=${favorite.id}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(favorite)
        });

        if (!response.ok) {
            throw new Error(await response.text() || "API error");
        }

        return response.json();
    },

    delete: async (id: number): Promise<void> => {
        const response = await fetch(`api/restaurantfavorite/id=${id}`, {
            method: "DELETE",
            headers: { "Content-Type": "application/json" }
        });

        if (!response.ok) {
            throw new Error(await response.text() || "API error");
        }
    },

    deleteByUserAndRestaurant: async (userId: number, restaurantId: number): Promise<void> => {
        const response = await fetch(`api/restaurantfavorite/${userId}/${restaurantId}`, {
            method: "DELETE",
            headers: { "Content-Type": "application/json" }
        });

        if (!response.ok) {
            throw new Error(await response.text() || "API error");
        }
    }
};
