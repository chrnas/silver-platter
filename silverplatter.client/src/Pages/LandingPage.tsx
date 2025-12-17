import { useNavigate } from 'react-router-dom';
import PopupRestaurantComp from '../Components/PopupRestaurantComp';
import type { Restaurant } from '../Types/Restaurant';
import { useEffect, useState } from 'react';
import { restaurantService } from '../service/RestaurantService';
import './css/LandingPage.css'
import { userService } from '../service/UserService';

const VISITORS : number[] = []

function LandingPage() {
    const [randomRestaurant, setRandomRestaurant] = useState<Restaurant>();
    const [skip, setSkip] = useState<boolean>(false);
    const [showWelcome, setShowWelcome] = useState<boolean>(true);
    const [noFit, setNoFit] = useState<boolean>(false)

    useEffect(() => {
        restaurantService.getAll().then(data => {
            userService.getById(1).then(user => {
                let rating = user.preferedRating;
                let budget = user.budget;
                let randomId: number = 0;
                let id = 0;
                while(id < 10) {
                    id++;
                    randomId = Math.floor(Math.random() * data.length);
                    console.log(data[randomId])
                    if(data[randomId].budget <= budget && data[randomId].rating >= rating) {
                        if(data[randomId].id != randomRestaurant?.id || randomRestaurant.id == null) {
                            console.log("Found fitting choice")
                            setRandomRestaurant(data[randomId])
                            break;
                        }
                    }
                }
                setNoFit(id >= 10);
            })
        }) 
    }, [skip])

    let nav = useNavigate();

return (
    <main className="LandingPage">
        {showWelcome && VISITORS.length == 0 ? (
            <div className="Content" role="dialog" aria-label="Welcome">
                <div className="Welcome-inner">
                    <img src="src/assets/silverplatter_logo.png" alt=""/>
                    <h1>Welcome to SilverPlatter</h1>
                    <p style={{marginTop: "-2rem"}}>Discover nearby restaurants curated for you.</p>
                    <div className="Welcome-actions">
                        <button className="Button" onClick={() => {setShowWelcome(false); VISITORS.push(1)}}>Log In</button>
                    </div>
                </div>
            </div>
        ) : (
            <>
                <div className="Content">
                    <button className='Button' onClick={() => setSkip(!skip)} disabled={noFit}>Skip</button>
                    <PopupRestaurantComp restaurant={randomRestaurant || {name: "", id: 0, description: "", address: "", budget: 0, rating: 0}}/>
                    <button className='Button' onClick={() => nav("/Restaurants/"+randomRestaurant?.name)}>Go to</button>
                </div>
            </>
        )}
    <div className="Background">
        <video autoPlay muted loop> 
            <source src="src/assets/LandingBackground.mp4" type="video/mp4"/>
        </video>
    </div>
    </main>
)
}

export default LandingPage;