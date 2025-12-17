import { useEffect, useState } from 'react';
import type { Restaurant } from '../Types/Restaurant';
import RestaurantButton from '../Components/RestaurantButton';
import './css/Browse.css'
import { restaurantService } from '../service/RestaurantService';
import { userService } from '../service/UserService';
import type { User } from '../Types/User';
import { menuEntryService } from '../service/MenuEntryService';
import type { MenuItem } from '../Types/MenuItem';
import { allergyService } from '../service/AllergyService';
import type { Allergy } from '../Types/Allergy';

function Browse() {

    const [restaurants, setRestaurants] = useState<Restaurant[]>([]);
    const [filteredRestaurants, setFilteredRestaurants] = useState<Restaurant[]>([]);
    const [user, setUser] = useState<User>();
    const [menuEntries, setMenuEntries] = useState<MenuItem[]>([]);
    const [allergies, setAllergies] = useState<Allergy[]>([]);

    useEffect(() => {
        restaurantService.getAll().then(data => {
            setRestaurants(data);
            setFilteredRestaurants(data);
        });

        userService.getById(1).then(user => {
            setUser(user);
        });

        menuEntryService.getAll().then(entries => {
            setMenuEntries(entries);
        });

        allergyService.getAll().then(allergies => {
            setAllergies(allergies);
        });
    }, []);

    function checkIfAllergiesExist(restaurant: Restaurant): boolean {
        let userAllergies = allergies.filter(allergy => allergy.userId === user?.id);
        let restaurantEntries = menuEntries.filter(entry => entry.restaurantId === restaurant.id);
        const exists = restaurantEntries.some(entry => userAllergies.some(allergy => allergy.name === entry.allergy));
        return exists;
    }

    function handleFilterChange(event: React.ChangeEvent<HTMLInputElement>) {
        const query = event.target.value.toLowerCase();
        const filteredRestaurants = restaurants.filter(restaurant => restaurant.name.toLowerCase().includes(query));
        setFilteredRestaurants(filteredRestaurants);
    }

    return (
        <div className='Browse'>
            <div className='Search'>
                <h1>Can't find what you're looking for?</h1>
                <div style={{display: "flex", flexDirection: "row", gap: "1rem"}}>
                    <input onChange={handleFilterChange} type="search" name="RestaurantSearch" id="RestaurantSearch" placeholder={"I need McDonalds?"} />
                </div>
            </div>
            <div className='Categories'>
                <section id="HighestRated" className='Category'>
                    <h2 className='CategoryName'>Highest Rated Restaurants in Linköping</h2>
                    <div className='RList'>
                        {filteredRestaurants?.sort((a, b) => b.rating - a.rating).map(restaurant => {
                            return (
                                    <RestaurantButton restaurant={restaurant}/>
                            );
                        })}
                    </div>
                </section>

                <section id="Budget" className='Category'>
                    <h2 className='CategoryName'>Most Budget Friendly</h2>
                    <div className='RList'>
                        {filteredRestaurants?.sort((a, b) => b.budget - a.budget).map(restaurant => {
                            return (
                                    <RestaurantButton restaurant={restaurant}/>
                            );
                        })}
                    </div>
                </section>

                <section id="Diet" className='Category'>
                    <h2 className='CategoryName'>Allergy Friendly Restaurants</h2>
                    <div className='RList'>
                        {filteredRestaurants?.filter(restaurant => checkIfAllergiesExist(restaurant)).map(restaurant => {
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