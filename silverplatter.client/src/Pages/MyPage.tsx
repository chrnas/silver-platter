import { useEffect, useState } from 'react';
import RestaurantButton from '../Components/RestaurantButton';
import type {Restaurant} from '../Types/Restaurant'
import './css/MyPage.css'
import { restaurantFavoriteService } from '../service/RestaurantFavoriteService';
import { restaurantService } from '../service/RestaurantService';
import { userService } from '../service/UserService';
import { allergyService } from '../service/AllergyService';
import type { User } from '../Types/User';

const OPTIONS = ['Vegetarian', 'Vegan', 'Gluten', 'Lactose', 'Halal'];

function MyPage() {

    const [sliderA, setSliderA] = useState<number>(200);
    const [sliderB, setSliderB] = useState<number>(3);
    const [selected, setSelected] = useState<string[]>([]);
    const [favoriteIds, setFavoriteIds] = useState<number[]>([])
    const [myRestaurants, setMyRestaurants] = useState<Restaurant[]>([])
    const [user, setUser] = useState<User>();

    useEffect(() => {
        restaurantFavoriteService.getByUserId(1).then(data => {
            setFavoriteIds(data.map(favs => favs.restaurantId))
        })

        userService.getById(1).then(userSetting => {
            setSliderA(userSetting.budget);
            setSliderB(userSetting.preferedRating);
            setUser(userSetting);
        })

        allergyService.getByUserId(1).then(allergies => {
            setSelected(allergies.map(allergy => allergy.name))
        })
    }, [])

    useEffect(() => {
        let mounted = true;
        (async () => {
            try {
                const promises = favoriteIds.map(id => restaurantService.getById(id));
                const settled = await Promise.allSettled(promises);
                const restaurants: Restaurant[] = settled
                    .filter(r => r.status === 'fulfilled')
                    .map(r => (r as PromiseFulfilledResult<Restaurant>).value);
                if (mounted) setMyRestaurants(restaurants);
            } catch (err) {
                console.error("Failed to load restaurants", err);
                if (mounted) setMyRestaurants([]);
            }
        })();
    }, [favoriteIds])

    function toggleOption(opt: string) {
        setSelected(prev => prev.includes(opt) ? prev.filter(p => p !== opt) : [...prev, opt]);
    }

    function reset() {
        userService.getById(1).then(userSetting => {
            setSliderA(userSetting.budget);
            setSliderB(userSetting.preferedRating);
        })

        allergyService.getByUserId(1).then(allergies => {
            setSelected(allergies.map(allergy => allergy.name))
        })
    }

    function save() {
        console.log(user);
        if(user) {
            user.budget = sliderA;
            user.preferedRating = sliderB;
            userService.update(user) 
        }
        allergyService.getByUserId(1).then(allergies => {
            allergies.forEach(allergy => allergyService.delete(allergy.id));
            selected ? selected.forEach(allergy => {
            console.log(allergy)
            allergyService.create({
                name: allergy,
                userId: 1
            });
        }) : console.log("No Allergies")})  
    }


    return (
        <div style={{height: "calc(100vh - 5rem)"}}>
            <div id="MyPageBody">
                <section className='FavoriteRestaurants'>
                    <h1>My Favorite Restaurants</h1>
                    {myRestaurants.length > 0 ? myRestaurants.map(restaurant => {
                        console.log("Restaurant", restaurant.name); 
                        return (
                            <RestaurantButton key={restaurant.id} restaurant={restaurant}/>
                        )
                    }) : <h3>You have no restaurants saved as favorite</h3>}
                </section>

                <section className='Profile'>
                    <div id="ProfileHeader">
                        <h1>Adjust Preferences</h1>
                        <p>These adjustments regulate what restaurants are recommended to you when on the home screen. They also filter the Browse and Favorite lists so you don't have to. </p>
                    </div>
                    <div id='ProfileSettings'>
                        <nav>
                            <div className="ss-section">
                                <label htmlFor="sliderA" className="ss-label">Maximum Budget (SEK)</label>
                                <div className="ss-control">
                                <input
                                    id="sliderA"
                                    type="range"
                                    min={0}
                                    max={1000}
                                    value={sliderA}
                                    onChange={(e) => setSliderA(Number(e.target.value))}
                                    aria-valuemin={0}
                                    aria-valuemax={1000}
                                    aria-valuenow={sliderA}
                                />
                                <output className="ss-value" aria-live="polite">{sliderA} Kr</output>
                                </div>
                            </div>

                            <div className="ss-section">
                                <label htmlFor="sliderB" className="ss-label">Minimum Rating (Adjusted)</label>
                                <div className="ss-control">
                                <input
                                    id="sliderB"
                                    type="range"
                                    min={1}
                                    max={5}
                                    value={sliderB}
                                    onChange={(e) => setSliderB(Number(e.target.value))}
                                    aria-valuemin={1}
                                    aria-valuemax={5}
                                    aria-valuenow={sliderB}
                                />
                                <output className="ss-value" aria-live="polite">{sliderB} Stars</output>
                                </div>
                            </div>

                            <div className="ss-section">
                                <div className="ss-label">Dietary preferences</div>
                                <div className="ss-options" role="group" aria-label="Dietary preferences">
                                {OPTIONS.map(opt => {
                                    const active = selected.includes(opt);
                                    return (
                                    <button
                                        key={opt}
                                        type="button"
                                        className={`ss-option ${active ? 'active' : ''}`}
                                        onClick={() => toggleOption(opt)}
                                        aria-pressed={active}
                                    >
                                        {opt}
                                    </button>
                                    );
                                })}
                                </div>
                            </div>
                        </nav>
                        <button className='ss-reset' onClick={() => reset()}>
                            Reset Preferences
                        </button>
                        <button className='ss-save' onClick={() => save()}>
                            Save Preferences
                        </button>
                    </div>
                </section>
            </div>
        </div>
    )
}

export default MyPage;