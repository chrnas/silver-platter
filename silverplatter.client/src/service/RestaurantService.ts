import type { Restaurant } from "../Types/Restaurant";

export const restaurantService = {
    getAll: async (): Promise<Restaurant[]> => {
        const response = await fetch(`api/restaurant`, {
            headers: {
                "Content-Type": "application/json"
            }
        });

        if (!response.ok) {
            const message = await response.text();
            throw new Error(message || "API error");
        }

        return response.json();
    },

    getById: async (id: number): Promise<Restaurant> => {
        const response = await fetch(`api/restaurant/${id}`, {
            headers: {
                "Content-Type": "application/json"
            }
        });

        if (!response.ok) {
            const message = await response.text();
            throw new Error(message || "API error");
        }

        return response.json();
    },

    create: async (entry : Omit<Restaurant, "id">): Promise<Restaurant> => {
        const response = await fetch(`api/restaurant`, {
            headers: {
                "Content-Type": "application/json"
            }, 
            method: "POST",
            body: JSON.stringify(entry),

        });

        if (!response.ok) {
            const message = await response.text();
            throw new Error(message || "API error");
        }

        return response.json();
    },
    
    update: async (entry: Restaurant): Promise<Restaurant> => {
        const response = await fetch(`api/restaurant/${entry.id}`, {
            headers: {
                "Content-Type": "application/json"
            },
            method: "PUT",
            body: JSON.stringify(entry),

        });

        if (!response.ok) {
            const message = await response.text();
            throw new Error(message || "API error");
        }

        return response.json();
    },
        

    delete: async (id: number): Promise<void> => {
        const response = await fetch(`api/restaurant/${id}`, {
            headers: {
                "Content-Type": "application/json"
            },
            method: "DELETE"
        });

        if (!response.ok) {
            const message = await response.text();
            throw new Error(message || "API error");
        }

        return response.json();
    },
};

// Needs to be checked and tested so that formatting is correct and everything works, especially the paths 