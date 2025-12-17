export interface BookableTable {
    id: number;
    name: string;
    description: string;
    places: number;
    booked: boolean;
    restaurantId: number;
}