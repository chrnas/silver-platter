import type { MenuItem } from "../Types/MenuItem";
import http from "./http";

export const menuEntryService = {
    getAll: async (): Promise<MenuItem[]> => http<MenuItem[]>("menuentry"),

    getById: async (id: number): Promise<MenuItem> => http<MenuItem>(`menuentry/id=${id}`),

    create: async (entry : Omit<MenuItem, "id">): Promise<MenuItem> => http<MenuItem>("menuentry", {
        method: "POST",
        body: JSON.stringify(entry),
    }),
    
    update: async (entry: MenuItem): Promise<MenuItem> => http<MenuItem>(`menuentry/id=${entry.Id}`, {
        method: "PUT",
        body: JSON.stringify(entry),
    }),

    delete: async (id: number): Promise<void> => http<void>(`menuentry/id=${id}`, {
        method: "DELETE"
    })
};

// Needs to be checked and tested so that formatting is correct and everything works, especially the paths 