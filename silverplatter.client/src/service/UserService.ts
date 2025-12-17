import type { User } from "../Types/User";

export const userService = {
    getAll: async (): Promise<User[]> => {
        const response = await fetch(`/api/user`, {
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

    getById: async (id: number): Promise<User> => {
        const response = await fetch(`/api/user/id=${id}`, {
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

    create: async (entry : Omit<User, "id">): Promise<User> => {
        const response = await fetch(`/api/user`, {
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
    
    update: async (entry: User): Promise<User> => {
        const response = await fetch(`/api/user/id=${entry.id}`, {
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
        const response = await fetch(`/api/user/id=${id}`, {
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
}