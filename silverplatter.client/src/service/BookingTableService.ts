import type { BookableTable } from "../Types/BookableTable";
import http from "./http";

// Check names name in frontend of type BookavbleTable, backend bookingtablecontroller

export const bookingTableService = {
    getAll: async (): Promise<BookableTable[]> => http<BookableTable[]>("bookingtable"),
    
    getById: async (id: number): Promise<BookableTable> => http<BookableTable>(`bookingtable/id=${id}`),

    create: async (entry: Omit<BookableTable, "id">): Promise<BookableTable> => http<BookableTable>(`bookingtable`, {
        method: "POST",
        body: JSON.stringify(entry),
    }),

    update: async (entry: BookableTable): Promise<BookableTable> => http<BookableTable>(`bookingtable/id=${entry.Id}`, {
        method: "PUT",
        body: JSON.stringify(entry),
    }),

    delete: async (id: number): Promise<void> => http<void>(`bookingtable/id=${id}`, {
        method: "DELETE"
    }),
}
