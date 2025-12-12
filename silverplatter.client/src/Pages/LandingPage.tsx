import { useEffect, useState } from 'react';
import PopupRestaurantComp from '../Components/PopupRestaurantComp';
import { restaurantService } from '../service/RestaurantService';
import './css/LandingPage.css'
import type { Restaurant } from '../Types/Restaurant';

function LandingPage() {

    const [restaurant, setRestaurant] = useState<Restaurant>();

    useEffect(() => {
        restaurantService.getRandom().then((data: any) => {
            setRestaurant(data);
        }).catch(console.error)
    }, []);

    return (
        <main className="LandingPage">
            <div className="Content">
                {restaurant && <PopupRestaurantComp restaurant={restaurant}/>}
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