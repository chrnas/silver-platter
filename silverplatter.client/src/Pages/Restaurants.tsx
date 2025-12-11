import { Outlet } from 'react-router-dom';
import RestaurantButton from '../Components/RestaurantButton';
import './css/Browse.css';
import { useEffect, useState } from 'react';
import { restaurantService } from '../service/RestaurantService';
import type { Restaurant } from '../Types/Restaurant';

function Restaurants() {
    const [restaurants, setRestaurants] = useState<Restaurant[]>([]);

    useEffect(() => {
        restaurantService.getAll()
            .then(data => setRestaurants(data))
            .catch(console.error);
    }, []);

    return (
        <div>
            {restaurants.map(restaurant => (
                <RestaurantButton
                    key={restaurant.id}
                    restaurant={restaurant}
                />
            ))}
            <Outlet />
        </div>
    );
}

export default Restaurants;
