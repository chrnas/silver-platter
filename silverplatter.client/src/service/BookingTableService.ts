import type { BookableTable } from "../Types/BookableTable";

export const bookingTableService = {
    getAll: async (): Promise<BookableTable[]> => {
        const response = await fetch(`api/bookingtable`, {
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

    getById: async (id: number): Promise<BookableTable> => {
        const response = await fetch(`api/bookingtable/${id}`, {
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

    create: async (entry : Omit<BookableTable, "id">): Promise<BookableTable> => {
        const response = await fetch(`api/bookingtable`, {
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
    
    update: async (entry: BookableTable): Promise<BookableTable> => {
        const response = await fetch(`api/bookingtable/${entry.id}`, {
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
        const response = await fetch(`api/bookingtable/${id}`, {
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
