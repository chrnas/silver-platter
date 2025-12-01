

export interface Restaurant {
    Id : number;
    name: string;
    Description: string;
    Address: string;
}

export interface MenuItem {
    Id: number;
    Name: string;
    Description: string;
    RestaurantId: number;
}

export interface BookableTable {
    Id: number;
    Name: string;
    Description: string;
    Places: number;
    RestaurantId: number;
}