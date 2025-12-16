import { useEffect, useState } from 'react';
import type { Restaurant } from '../Types/Restaurant';
import RestaurantButton from '../Components/RestaurantButton';
import './css/Browse.css'
import { restaurantService } from '../service/RestaurantService';

function Browse() {

    const [restaurants, setRestaurants] = useState<Restaurant[]>([]);

    useEffect(() => {
        restaurantService.getAll().then(data => {
            setRestaurants(data);
            console.log(restaurants);
            console.log(data?.sort((a, b) => a.rating - b.rating));
        });
    }, []);

    return (
        <div className='Browse'>
            <div className='Search'>
                <h1>Can't find what you're looking for?</h1>
                <div style={{display: "flex", flexDirection: "row", gap: "1rem"}}>
                    <input type="search" name="RestaurantSearch" id="RestaurantSearch" placeholder={"I need McDonalds?"} />
                    <button type="button">Go</button>
                </div>
            </div>
            <div className='Categories'>
                <section id="HighestRated" className='Category'>
                    <h2 className='CategoryName'>Highest Rated Restaurants in Linköping</h2>
                    <div className='RList'>
                        {restaurants?.sort((a, b) => a.rating - b.rating).map(restaurant => {
                            return (
                                <RestaurantButton restaurant={restaurant}/>
                            );
                        })}
                    </div>
                </section>

                <section id="Budget" className='Category'>
                    <h2 className='CategoryName'>Most Budget Friendly</h2>
                    <div className='RList'>
                        {restaurants?.sort((a, b) => b.budget - a.budget).map(restaurant => {
                            return (
                                <RestaurantButton restaurant={restaurant}/>
                            );
                        })}
                    </div>
                </section>

                <section id="Diet" className='Category'>
                    <h2 className='CategoryName'>Allergy Friendly Restaurants</h2>
                    <div className='RList'>
                        {restaurants?.map(restaurant => {
                            return (
                                <RestaurantButton restaurant={restaurant}/>
                            );
                        })}
                    </div>
                </section>
            </div>
        </div>
    )
}

export default Browse;