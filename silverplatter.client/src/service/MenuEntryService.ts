import type { MenuItem } from "../Types/MenuItem";

export const menuEntryService = {
    getAll: async (): Promise<MenuItem[]> => {
        const response = await fetch(`/api/menuentry`, {
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

    getById: async (id: number): Promise<MenuItem> => {
        const response = await fetch(`/api/menuentry/id=${id}`, {
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

    getByRestaurantId: async (id: number): Promise<MenuItem[]> => {
        const response = await fetch(`/api/menuentry/restaurant/${id}`, {
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

    create: async (entry : Omit<MenuItem, "id">): Promise<MenuItem> => {
        const response = await fetch(`/api/menuentry`, {
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
    
    update: async (entry: MenuItem): Promise<MenuItem> => {
        const response = await fetch(`/api/menuentry/id=${entry.id}`, {
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
        const response = await fetch(`/api/menuentry/id=${id}`, {
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