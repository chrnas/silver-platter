import { useEffect, useMemo, useState } from 'react';
import RestaurantButton from '../Components/RestaurantButton';
import type {Restaurant} from '../Types/Restaurant'
import './css/MyPage.css'

const myRestaurants: Restaurant[] = [];
const OPTIONS = ['Vegetarian', 'Vegan', 'Gluten-free', 'Halal'];


function MyPage() {

    const [sliderA, setSliderA] = useState<number>(200);
    const [sliderB, setSliderB] = useState<number>(3);
    const [selected, setSelected] = useState<string[]>([]);

    function toggleOption(opt: string) {
        setSelected(prev => prev.includes(opt) ? prev.filter(p => p !== opt) : [...prev, opt]);
    }

    function reset() {
        setSliderA(200);
        setSliderB(3);
        setSelected([]);
    }

    useEffect(() => {
        
    }, [])

    useMemo(() => {

        let tempRestaurant : Restaurant = {
            Id: 0,
            Name: "Temporary Restaurant",
            Description: "Discover the best temp in temp, all at the small price of 12.99 Temp, you couldn't dream of a better dream than that!",
            Address: "tempytemptemptemp"
        }
        if(!myRestaurants.some(r => r.id === tempRestaurant.id)) {
            myRestaurants.push(tempRestaurant);
        }
    }, [])

    return (
        <div style={{height: "calc(100vh - 5rem)"}}>
            <div id="MyPageBody">
                <section className='FavoriteRestaurants'>
                    <h1>My Favorite Restaurants</h1>
                    {myRestaurants.map((restaurant) => (
                        <RestaurantButton key={restaurant.id} restaurant={restaurant}/>
                    ))}
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
                    </div>
                </section>
            </div>
        </div>
    )
}

export default MyPage;