import { useEffect, useMemo, useState } from 'react';
import type { Restaurant } from '../Types/Restaurant';
import RestaurantButton from '../Components/RestaurantButton';
import './css/Browse.css'
import { restaurantService } from '../service/RestaurantService';

const restaurantLists : Map<string, Array<Restaurant>> = new Map();

function Browse() {

    const [restaurants, setRestaurants] = useState<Restaurant[]>([]);

    useEffect(() => {
            restaurantService.getAll()
                .then(data => setRestaurants(data))
                .catch(console.error);
        }, []);

    useMemo(() => {
        let tempRestaurant : Restaurant = {
            id: 0,
            name: "Temporary Restaurant",
            description: "Discover the best temp in temp, all at the small price of 12.99 Temp, you couldn't dream of a better dream than that!",
            address: "tempytemptemptemp"
        }
        if(!restaurantLists.has("Rating")) {
            restaurantLists.set("Rating", []);
            restaurantLists.set("Budget", []);
            restaurantLists.set("Diet", []);
        }
        if(restaurantLists.get("Rating")?.length == 0) {
            restaurantLists.get("Rating")?.push(tempRestaurant);
            restaurantLists.get("Rating")?.push(tempRestaurant);
            restaurantLists.get("Rating")?.push(tempRestaurant);
            restaurantLists.get("Rating")?.push(tempRestaurant);
            restaurantLists.get("Rating")?.push(tempRestaurant);
            restaurantLists.get("Rating")?.push(tempRestaurant);

        }
    }, [])
    return (
        <div className='Browse'>
            <div className='Search'>
                <h1>Can't find what you're looking for?</h1>
                <input type="search" name="RestaurantSearch" id="RestaurantSearch" placeholder={"I need McDonalds?"} />
            </div>
            <div className='Categories'>
                <section id="HighestRated" className='Category'>
                    <h2 className='CategoryName'>Highest Rated Restaurants in Linköping</h2>
                    <div className='RList'>
                        {restaurantLists.get("Rating")?.map(restaurant => {
                            return (
                                <RestaurantButton restaurant={restaurant}/>
                            );
                        })}
                    </div>
                </section>

                <section id="Budget" className='Category'>
                    <h2 className='CategoryName'>Most Budget Friendly</h2>
                    <div className='RList'>
                        {restaurantLists.get("Rating")?.map(restaurant => {
                            return (
                                <RestaurantButton restaurant={restaurant}/>
                            );
                        })}
                    </div>
                </section>

                <section id="Diet" className='Category'>
                    <h2 className='CategoryName'>Allergy Friendly Restaurants</h2>
                    <div className='RList'>
                        {restaurantLists.get("Rating")?.map(restaurant => {
                            return (
                                <RestaurantButton restaurant={restaurant}/>
                            );
                        })}
                    </div>
                </section>

                <section id="Budget" className='Category'>
                    <h2 className='CategoryName'>All restaurants</h2>
                    <div className='RList'>
                         {restaurants.map(restaurant => (
                            <RestaurantButton
                                key={restaurant.id}
                                restaurant={restaurant}
                            />
                        ))}
                    </div>
                </section>
            </div>
        </div>
    )
}

export default Browse;