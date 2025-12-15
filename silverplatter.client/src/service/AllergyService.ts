import type { Allergy } from "../Types/Allergy";

export const allergyService = {
    getAll: async (): Promise<Allergy[]> => {
        const response = await fetch(`api/allergy`, {
            headers: { "Content-Type": "application/json" }
        });

        if (!response.ok) {
            throw new Error(await response.text() || "API error");
        }

        return response.json();
    },

    getById: async (id: number): Promise<Allergy> => {
        const response = await fetch(`api/allergy/id=${id}`, {
            headers: { "Content-Type": "application/json" }
        });

        if (!response.ok) {
            throw new Error(await response.text() || "API error");
        }

        return response.json();
    },

    getByUserId: async (userId: number): Promise<Allergy[]> => {
        const response = await fetch(`api/allergy/user/${userId}`, {
            headers: { "Content-Type": "application/json" }
        });

        if (!response.ok) {
            throw new Error(await response.text() || "API error");
        }

        return response.json();
    },

    create: async (allergy: Omit<Allergy, "id">): Promise<Allergy> => {
        const response = await fetch(`api/allergy`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(allergy)
        });

        if (!response.ok) {
            throw new Error(await response.text() || "API error");
        }

        return response.json();
    },

    update: async (allergy: Allergy): Promise<Allergy> => {
        const response = await fetch(`api/allergy/id=${allergy.id}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(allergy)
        });

        if (!response.ok) {
            throw new Error(await response.text() || "API error");
        }

        return response.json();
    },

    delete: async (id: number): Promise<void> => {
        const response = await fetch(`api/allergy/id=${id}`, {
            method: "DELETE",
            headers: { "Content-Type": "application/json" }
        });

        if (!response.ok) {
            throw new Error(await response.text() || "API error");
        }
    }
};
