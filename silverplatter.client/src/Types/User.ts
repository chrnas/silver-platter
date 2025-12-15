
export interface User {
    id: number;
    name: string;
    restaurantFavorites: number[]; // Might need to be optional same for all but name and id
    budget: number;
    allergies: string[];
    preferedRating: number; 
}