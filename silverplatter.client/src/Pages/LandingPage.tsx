import { useNavigate } from 'react-router-dom';
import PopupRestaurantComp from '../Components/PopupRestaurantComp';
import type { Restaurant } from '../Types/Restaurant';
import { useEffect, useState } from 'react';
import { restaurantService } from '../service/RestaurantService';
import './css/LandingPage.css'

function LandingPage() {
    const [randomRestaurant, setRandomRestaurant] = useState<Restaurant>();
    const [skip, setSkip] = useState<boolean>(false);

    useEffect(() => {
        restaurantService.getAll().then(data => {
            console.log("API Response: ", data);
            let randomId = Math.floor(Math.random() * data.length);
            setRandomRestaurant(data[randomId])
        }) 
    }, [skip])

    let nav = useNavigate();

    return (
        <main className="LandingPage">
            <div className="Content">
                <button className='Button' onClick={() => setSkip(!skip)}>Skip</button>
                <PopupRestaurantComp restaurant={randomRestaurant || {name: "", id: 0, description: "", address: "", budget: 0, rating: 0}}/>
                <button className='Button' onClick={() => nav("/Restaurants/"+randomRestaurant?.name)}>Go to</button>
            </div>

            <div className="Background">
                <video autoPlay muted loop> 
                    <source src="src/assets/LandingBackground.mp4" type="video/mp4"/>
                </video>
            </div>
        </main>
    )
}

export default LandingPage;